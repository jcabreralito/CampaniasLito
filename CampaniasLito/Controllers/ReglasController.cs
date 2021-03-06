﻿using CampaniasLito.Classes;
using CampaniasLito.Filters;
using CampaniasLito.Models;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace CampaniasLito.Controllers
{
    [Authorize]
    public class ReglasController : Controller
    {
        private readonly CampaniasLitoContext db = new CampaniasLitoContext();

        public string modulo = "Reglas";
        public string movimiento = string.Empty;

        public class spReglas
        {
            public int ReglaId { get; set; }
            public int ArticuloKFCId { get; set; }
            public string Material { get; set; }
            public string Regla { get; set; }
        }

        public class spReglasCaracteristicas
        {
            public int ReglaCaracteristicaId { get; set; }
            public int ReglaId { get; set; }
            public int ReglaCatalogoId { get; set; }
            public string Caracteristica { get; set; }
            public string Valor { get; set; }
            public string Regla { get; set; }
            public bool Seleccionado { get; set; }
            public bool IsTrue { get; set; }
            public bool IsFalse { get; set; }
        }


        public class spReglaMateriales
        {
            public int ReglaMaterialId { get; set; }
            public int ReglaId { get; set; }
            public int ArticuloKFCId { get; set; }
            public string Descripcion { get; set; }
        }

        public class spReglaCaracteristicasEliminar
        {
            public int ReglaCaracteristicaId { get; set; }
            public int ReglaId { get; set; }
            public int ReglaCatalogoId { get; set; }
            public string EquityFranquicia { get; set; }
        }

        // GET: Reglas
        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index()
        {
            Session["iconoTitulo"] = "fas fa-file-contract";
            Session["homeB"] = string.Empty;
            Session["equityB"] = "active";
            Session["franquiciasB"] = string.Empty;
            Session["reglasCatalogoB"] = string.Empty;
            Session["stockB"] = string.Empty;
            Session["materialesB"] = string.Empty;
            Session["Mensaje"] = string.Empty;

            return View();
        }
        public ActionResult GetDataEquity()
        {
            var reglasList = db.Database.SqlQuery<spReglas>("spGetReglasAll").ToList();

            return Json(new { data = reglasList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataFranquicias()
        {
            var reglasList = db.Database.SqlQuery<spReglas>("spGetReglasFranquicias").ToList();

            return Json(new { data = reglasList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataCatalogo()
        {
            var reglasList = db.Database.SqlQuery<ReglaCatalogo>("spGetReglasCatalogo").ToList().OrderBy(x => x.ReglaCatalogoId);

            return Json(new { data = reglasList }, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUser(idOperacion: 1)]
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.ArticuloKFCId = new SelectList(CombosHelper.GetMateriales("", true), "ArticuloKFCId", "Descripcion");

                return PartialView(new Regla());
            }
            else
            {
                var articuloId = db.Reglas.Where(x => x.ReglaId == id).FirstOrDefault().ArticuloKFCId;

                ViewBag.ArticuloKFCId = new SelectList(CombosHelper.GetMateriales("", true), "ArticuloKFCId", "Descripcion", articuloId);
                return PartialView(db.Reglas.Where(x => x.ReglaId == id).FirstOrDefault());
            }
        }

        [AuthorizeUser(idOperacion: 1)]
        [HttpPost]
        public ActionResult AddOrEdit(Regla regla)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault().UsuarioId;

            if (regla.ReglaId == 0)
            {
                movimiento = "Agregando regla";
                MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                db.Reglas.Add(regla);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    var reglaCatalogoId = 0;

                    var reglaId = regla.ReglaId;

                    var reglasCatalogoId = db.ReglasCaracteristicas.Where(x => x.ReglaId == reglaId).FirstOrDefault();

                    if (reglasCatalogoId == null)
                    {
                        reglaCatalogoId = 0;
                    }
                    else
                    {
                        reglaCatalogoId = reglasCatalogoId.ReglaCatalogoId;
                    }
                    var cat = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == regla.ArticuloKFCId).FirstOrDefault().EquityFranquicia;

                    MovementsHelper.AgregarReglasCaracteristicas(cat);

                    movimiento = "Agregar Regla " + regla.ReglaId + " " + regla.NombreRegla + " / " + regla.ArticuloKFC.Descripcion;
                    MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                    return Json(new { success = true, message = "REGLA AGREGADA" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = response.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                movimiento = "Actualizando regla";
                MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                db.Entry(regla).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    var reglaId = regla.ReglaId;
                    var reglaCatalogoId = db.ReglasCaracteristicas.Where(x => x.ReglaId == reglaId).FirstOrDefault().ReglaCatalogoId;
                    var cat = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == regla.ArticuloKFCId).FirstOrDefault().EquityFranquicia;

                    MovementsHelper.AgregarReglasCaracteristicas(reglaCatalogoId, cat);

                    movimiento = "Actualizar Regla " + regla.ReglaId + " " + regla.NombreRegla + " / " + regla.ArticuloKFC.Descripcion;
                    MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                    return Json(new { success = true, message = "REGLA ACTUALIZADA" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = response.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [AuthorizeUser(idOperacion: 1)]
        [HttpGet]
        public ActionResult AddOrEditCat(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.Categoria = new SelectList(CombosHelper.GetTipoCampañas(true), "Nombre", "Nombre");
                ViewBag.TipoConfiguracionId = new SelectList(CombosHelper.GetTipoConfiguracion(true), "TipoConfiguracionId", "Nombre");

                return PartialView(new ReglaCatalogo());
            }
            else
            {
                var tipo = db.ReglasCatalogo.Where(x => x.ReglaCatalogoId == id).FirstOrDefault().Categoria;
                var tipoCon = db.ReglasCatalogo.Where(x => x.ReglaCatalogoId == id).FirstOrDefault().TipoConfiguracionId;

                ViewBag.Categoria = new SelectList(CombosHelper.GetTipoCampañas(true), "Nombre", "Nombre", tipo);
                ViewBag.TipoConfiguracionId = new SelectList(CombosHelper.GetTipoConfiguracion(true), "TipoConfiguracionId", "Nombre", tipoCon);

                return PartialView(db.ReglasCatalogo.Where(x => x.ReglaCatalogoId == id).FirstOrDefault());
            }
        }

        [AuthorizeUser(idOperacion: 1)]
        [HttpPost]
        public ActionResult AddOrEditCat(ReglaCatalogo reglaCatalogo, FormCollection fcol)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault().UsuarioId;

            string[] fc = fcol.GetValues("FC");
            string[] fs = fcol.GetValues("FS");
            string[] il = fcol.GetValues("IL");
            string[] sb = fcol.GetValues("SB");

            var fcTipo = 0;
            var fsTipo = 0;
            var ilTipo = 0;
            var sbTipo = 0;

            if (fc != null)
            {
                fcTipo = 1;
            }

            if (fs != null)
            {
                fsTipo = 2;
            }
            if (il != null)
            {
                ilTipo = 3;
            }
            if (sb != null)
            {
                sbTipo = 4;
            }

            if (reglaCatalogo.ReglaCatalogoId == 0)
            {
                movimiento = "Agregando Característica";
                MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                if (string.IsNullOrEmpty(reglaCatalogo.Valor))
                {
                    reglaCatalogo.SiNo = true;
                    reglaCatalogo.Valor = "SI / NO";
                }
                else
                {
                    reglaCatalogo.SiNo = false;
                }

                db.ReglasCatalogo.Add(reglaCatalogo);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    var cat = reglaCatalogo.Categoria;

                    var reglaIdTienda = reglaCatalogo.ReglaCatalogoId;

                    MovementsHelper.AgregarReglasCaracteristicas(reglaIdTienda, cat);

                    MovementsHelper.AgregarTiendasCaracteristicas(reglaIdTienda, cat, fcTipo, fsTipo, ilTipo, sbTipo);

                    movimiento = "Agregar Característica " + reglaIdTienda + " " + reglaCatalogo.Nombre + " / " + cat;
                    MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                    return Json(new { success = true, message = "CARACTERÍSTICA AGREGADA" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = response.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                movimiento = "Actualizando Característica";
                MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                if (string.IsNullOrEmpty(reglaCatalogo.Valor))
                {
                    reglaCatalogo.SiNo = true;
                    reglaCatalogo.Valor = "SI / NO";
                }
                else
                {
                    reglaCatalogo.SiNo = false;
                }

                db.Entry(reglaCatalogo).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    var cat = reglaCatalogo.Categoria;

                    var reglaIdTienda = reglaCatalogo.ReglaCatalogoId;

                    MovementsHelper.AgregarReglasCaracteristicas(reglaIdTienda, cat);

                    if (cat == "EQUITY")
                    {
                        cat = "FRANQUICIAS";
                    }
                    else if (cat == "FRANQUICIAS")
                    {
                        cat = "EQUITY";
                    }
                    else
                    {
                        cat = string.Empty;
                    }

                    var caracteristicasEliminar = db.Database.SqlQuery<spReglaCaracteristicasEliminar>("spGetReglasCaracteristicasAEliminar @Categoria, @ReglaCatalogoId",
                        new SqlParameter("@Categoria", cat),
                        new SqlParameter("@ReglaCatalogoId", reglaIdTienda)).ToList();

                    if (caracteristicasEliminar.Count > 0)
                    {
                        foreach (var caracteristicaEliminar in caracteristicasEliminar)
                        {
                            var reglaCaracteristica = db.ReglasCaracteristicas.Find(caracteristicaEliminar.ReglaCaracteristicaId);
                            db.ReglasCaracteristicas.Remove(reglaCaracteristica);
                            db.SaveChanges();
                        }
                    }

                    MovementsHelper.AgregarTiendasCaracteristicas(reglaIdTienda, cat, fcTipo, fsTipo, ilTipo, sbTipo);

                    movimiento = "Actualizar Característica " + reglaCatalogo.ReglaCatalogoId + " " + reglaCatalogo.Nombre + " / " + reglaCatalogo.Categoria;
                    MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                    return Json(new { success = true, message = "CARACTERÍSTICA ACTUALIZADA" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = response.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpGet]
        public ActionResult Caracteristicas(int? id)
        {
            Session["reglaid"] = id;

            var reglasList = db.Database.SqlQuery<spReglasCaracteristicas>("spReglasCaracteristicas @ReglaId",
                new SqlParameter("@ReglaId", id)).OrderBy(x => x.ReglaCatalogoId).ToList();

            if (reglasList.Count == 0)
            {
                var reglaIdTienda = 0;
                var artId = db.Reglas.Where(x => x.ReglaId == id).FirstOrDefault().ArticuloKFCId;
                var cat = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == artId).FirstOrDefault().EquityFranquicia;
                int reglaId = (int)id;
                var reglasIdTienda = reglasList.FirstOrDefault();

                if (reglasIdTienda == null)
                {
                    reglaIdTienda = 0;
                }
                else
                {
                    reglaIdTienda = reglasIdTienda.ReglaCatalogoId;
                }

                MovementsHelper.AgregarReglasCaracteristicas(reglaIdTienda, cat);
            }

            if (reglasList == null)
            {
                return HttpNotFound();
            }

            ViewBag.Regla = db.Reglas.Where(t => t.ReglaId == id).FirstOrDefault().NombreRegla;

            return PartialView(reglasList);
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        public ActionResult Caracteristicas(FormCollection fc)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault().UsuarioId;
            var id = Session["reglaid"];

            var reglasListCurrent = db.Database.SqlQuery<spReglasCaracteristicas>("spReglasCaracteristicas @ReglaId",
                new SqlParameter("@ReglaId", id)).OrderBy(x => x.ReglaCatalogoId).ToList();

            var textMovimiento = string.Empty;

            var reglaNombre = string.Empty;

            foreach (var item in reglasListCurrent)
            {
                reglaNombre = item.Regla;
            }

            movimiento = "Modificando Características Regla : " + reglaNombre;
            MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

            string[] reglaCaractersiticaId = fc.GetValues("ReglaCaractersiticaId");
            //string[] seleccionado = fc.GetValues("Seleccionado");
            string[] isTrue = fc.GetValues("IsTrue");
            string[] isFalse = fc.GetValues("IsFalse");

            var selec = false;

            for (var i = 0; i < reglaCaractersiticaId.Length; i++)
            {
                ReglaCaracteristica reglaCaracteristica = db.ReglasCaracteristicas.Find(Convert.ToInt32(reglaCaractersiticaId[i]));

                //var reglaId = reglaCaracteristica.ReglaId;

                if (isTrue == null)
                {
                    selec = false;

                    reglaCaracteristica.IsTrue = selec;

                    db.Entry(reglaCaracteristica).State = EntityState.Modified;

                    db.SaveChanges();
                }
                else
                {
                    for (var j = 0; j < isTrue.Length; j++)
                    {
                        if (reglaCaractersiticaId[i] == isTrue[j])
                        {
                            selec = true;

                            reglaCaracteristica.IsTrue = selec;

                            db.Entry(reglaCaracteristica).State = EntityState.Modified;
                            db.SaveChanges();

                            break;
                        }
                        else
                        {
                            selec = false;

                            reglaCaracteristica.IsTrue = selec;

                            db.Entry(reglaCaracteristica).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    if (!selec)
                    {
                        selec = false;

                        reglaCaracteristica.IsTrue = selec;

                        db.Entry(reglaCaracteristica).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                if (isFalse == null)
                {
                    selec = false;

                    reglaCaracteristica.IsFalse = selec;

                    db.Entry(reglaCaracteristica).State = EntityState.Modified;

                    db.SaveChanges();
                }
                else
                {
                    for (var j = 0; j < isFalse.Length; j++)
                    {
                        if (reglaCaractersiticaId[i] == isFalse[j])
                        {
                            selec = true;

                            reglaCaracteristica.IsFalse = selec;

                            db.Entry(reglaCaracteristica).State = EntityState.Modified;
                            db.SaveChanges();

                            break;
                        }
                        else
                        {
                            selec = false;

                            reglaCaracteristica.IsFalse = selec;

                            db.Entry(reglaCaracteristica).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    if (!selec)
                    {
                        selec = false;

                        reglaCaracteristica.IsFalse = selec;

                        db.Entry(reglaCaracteristica).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            var regla = db.ReglasCaracteristicas.Find(Convert.ToInt32(reglaCaractersiticaId[1]));

            var reglaId = regla.ReglaId;

            var articuloId = db.Reglas.Find(reglaId);

            var articuloKFCId = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == articuloId.ArticuloKFCId).FirstOrDefault().ArticuloKFCId;

            var restauranteId = 0;

            var categoria = string.Empty;

            var campaña = db.Campañas.Where(x => x.Generada == "NO").FirstOrDefault();

            EliminarMateriales(articuloKFCId, campaña);

            MovementsHelper.AgregarMaterialesTiendaCampañaExiste(articuloKFCId, restauranteId, categoria);

            var material = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == articuloId.ArticuloKFCId).FirstOrDefault();

            if (campaña != null)
            {
                var campañaId = campaña.CampañaId;

                MovementsHelper.AgregarArticuloCampañas(material, campañaId);
            }

            var reglasListActualizado = db.Database.SqlQuery<spReglasCaracteristicas>("spReglasCaracteristicas @ReglaId",
                new SqlParameter("@ReglaId", id)).OrderBy(x => x.ReglaCatalogoId).ToList();

            textMovimiento = string.Empty;

            reglaNombre = string.Empty;

            var valorIsTrue = string.Empty;
            var valorIsFalse = string.Empty;

            for (int i = 0; i < reglasListCurrent.Count; i++)
            {
                var caracteristica = reglasListActualizado.Where(x => x.ReglaCatalogoId == reglasListCurrent[i].ReglaCatalogoId).FirstOrDefault();

                if (caracteristica.IsFalse != reglasListCurrent[i].IsFalse || caracteristica.IsTrue != reglasListCurrent[i].IsTrue)
                {
                    if (caracteristica.IsTrue == true)
                    {
                        valorIsTrue = "ACTIVADO";
                    }
                    else
                    {
                        valorIsTrue = "DEACTIVADO";
                    }

                    if (caracteristica.IsFalse == true)
                    {
                        valorIsFalse = "ACTIVADO";
                    }
                    else
                    {
                        valorIsFalse = "DESACTIVADO";
                    }

                    textMovimiento += " " + caracteristica.Caracteristica + " SI : " + valorIsTrue + " - NO : " + valorIsFalse + " / ";
                }

                reglaNombre = caracteristica.Regla;
            }

            if (textMovimiento == string.Empty)
            {
                textMovimiento = "Sin Modificaciones";
            }

            movimiento = "Características Asignadas Regla : " + reglaNombre + " / " + textMovimiento.ToString();
            MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

            Session["reglaid"] = string.Empty;

            return Json(new { success = true, message = "CARACTERÍSTICAS ASIGNADAS" }, JsonRequestBehavior.AllowGet);

        }

        private void EliminarMateriales(int id, Campaña campaña)
        {
            db.Database.ExecuteSqlCommand(
            "spEliminarMaterialTiendas @ArticuloKFCId",
            new SqlParameter("@ArticuloKFCId", id));

            if (campaña != null)
            {
                db.Database.ExecuteSqlCommand(
                "spEliminarMaterialCampaniasTiendas @ArticuloKFCId, @CampaniaId",
                new SqlParameter("@ArticuloKFCId", id),
                new SqlParameter("@CampaniaId", campaña.CampañaId));
            }
        }

        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault().UsuarioId;

            movimiento = "Eliminando Regla";
            MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

            Regla regla = db.Reglas.Where(x => x.ReglaId == id).FirstOrDefault();
            db.Reglas.Remove(regla);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                db.Database.ExecuteSqlCommand("spEliminarReglasCaracteristicas @ReglaId",
                new SqlParameter("@ReglaId", id));

                movimiento = "Eliminar Regla " + regla.ReglaId + " " + regla.NombreRegla;
                MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                return Json(new { success = true, message = "REGLA ELIMINADA" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, message = response.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        public ActionResult DeleteCat(int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault().UsuarioId;

            movimiento = "Eliminando Característica";
            MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

            var tiendaCaracteristica = db.TiendaCaracteristicas.Where(x => x.ReglaCatalogoId == id).ToList();

            db.TiendaCaracteristicas.RemoveRange(tiendaCaracteristica);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                ReglaCatalogo reglaCatalogo = db.ReglasCatalogo.Where(x => x.ReglaCatalogoId == id).FirstOrDefault();
                db.ReglasCatalogo.Remove(reglaCatalogo);
                var response2 = DBHelper.SaveChanges(db);
                if (response2.Succeeded)
                {
                    var reglaCaracteristica = db.ReglasCaracteristicas.Where(x => x.ReglaCatalogoId == id).ToList();
                    db.ReglasCaracteristicas.RemoveRange(reglaCaracteristica);
                    DBHelper.SaveChanges(db);

                    ActualizarTodo();

                    movimiento = "Eliminar Característica " + reglaCatalogo.ReglaCatalogoId + " " + reglaCatalogo.Nombre + " / " + reglaCatalogo.Categoria;
                    MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                    return Json(new { success = true, message = "CARACTERÍSTICA ELIMINADA" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = response2.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = true, message = response.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [AuthorizeUser(idOperacion: 1)]
        [HttpPost]
        public ActionResult ActualizarTodo()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault().UsuarioId;

            movimiento = "Actualizando Todo";
            MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

            var restauranteId = 0;

            var materialId = 0;

            var campaña = db.Campañas.Where(x => x.Generada == "NO").FirstOrDefault();


            EliminarTodo();

            var categorias = db.TipoCampanias.Where(x => x.Nombre != "STOCK" && x.Nombre != "EQUITY / FRANQUICIAS").ToList();

            var materialiesActivos = db.Database.SqlQuery<ArticuloKFC>("spGetMaterialesActivos").ToList();

            foreach (var materialActivo in materialiesActivos)
            {
                MovementsHelper.AgregarMaterialesTiendaCampañaExiste(materialActivo.ArticuloKFCId, restauranteId, materialActivo.EquityFranquicia);
            }

            if (campaña != null)
            {
                var campañaId = campaña.CampañaId;

                MovementsHelper.AgregarArticulosNuevaCampaña(campañaId);
            }

            movimiento = "Actualizar Todo";
            MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

            return Json(new { success = true, message = "MATERIAL ACTUALIZADO" }, JsonRequestBehavior.AllowGet);
        }

        private void EliminarTodo()
        {
            db.Database.ExecuteSqlCommand(
            "spEliminarTodosArticulosTiendas");

            var campañas = db.Campañas.Where(x => x.Generada == "NO").ToList();

            if (campañas != null)
            {
                foreach (var campaña in campañas)
                {
                    var campId = campaña.CampañaId;

                    db.Database.ExecuteSqlCommand(
                    "spEliminarCampaña @CampañaId",
                    new SqlParameter("@CampañaId", campId));
                }
            }
        }


    }
}