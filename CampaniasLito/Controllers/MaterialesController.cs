using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Filters;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class MaterialesController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        public ActionResult GetList()
        {
            var artList = db.ArticuloKFCs.ToList<ArticuloKFC>();
            return Json(new { data = artList }, JsonRequestBehavior.AllowGet);

        }

        // GET: ArticulosKFC
        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Session["homeB"] = string.Empty;
            Session["rolesB"] = string.Empty;
            Session["compañiasB"] = string.Empty;
            Session["usuariosB"] = string.Empty;
            Session["regionesB"] = string.Empty;
            Session["ciudadesB"] = string.Empty;
            Session["restaurantesB"] = string.Empty;
            Session["familiasB"] = string.Empty;
            Session["materialesB"] = "active";
            Session["campañasB"] = string.Empty;

            return View();

        }

        //[AuthorizeUser(idOperacion: 5)]
        //public ActionResult MaterialesStock()
        //{
        //    string tipo = "STOCK";

        //    var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

        //    if (usuario == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    var articuloKFCs = db.ArticuloKFCs.Where(a => a.EquityFranquicia == tipo).OrderBy(a => a.Descripcion).ThenBy(a => a.Familia.Descripcion).ToList();

        //    return View(articuloKFCs.ToList());
        //}

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult MaterialesFranquicias()
        {
            string tipo = "FRANQUICIAS";

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var articuloKFCs = db.ArticuloKFCs.Where(a => a.EquityFranquicia == tipo).OrderBy(a => a.Descripcion).ThenBy(a => a.Familia.Descripcion).ToList();

            return View(articuloKFCs.ToList());
        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult MaterialesEquity(string articulo)
        {
            string tipo = "EQUITY";

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var articuloKFCs = db.ArticuloKFCs.Where(a => a.EquityFranquicia == tipo).OrderBy(a => a.Descripcion).ThenBy(a => a.Familia.Descripcion).ToList();

            return View(articuloKFCs.ToList());
        }

        // GET: ArticulosKFC/Details/5
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticuloKFC articuloKFC = db.ArticuloKFCs.Find(id);
            if (articuloKFC == null)
            {
                return HttpNotFound();
            }
            return PartialView(articuloKFC);
        }

        // GET: ArticulosKFC/Create
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult Create(int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == 1)
            {
                Session["tipoArticulo"] = "EQUITY";
            }
            else if (id == 2)
            {
                Session["tipoArticulo"] = "FRANQUICIAS";
            }
            else if (id == 3)
            {
                Session["tipoArticulo"] = "STOCK";
            }

            var articulos = new ArticuloKFC { };

            ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Descripcion");
            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre");

            return PartialView(articulos);
        }

        // POST: ArticulosKFC/Create
        [AuthorizeUser(idOperacion: 1)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticuloKFC articuloKFC)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            articuloKFC.EquityFranquicia = Session["tipoArticulo"].ToString();

            if (ModelState.IsValid)
            {
                db.ArticuloKFCs.Add(articuloKFC);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    MovementsHelper.AgregarArticuloTiendas(articuloKFC.ArticuloKFCId);
                    MovementsHelper.AgregarArticuloCampañas(articuloKFC.ArticuloKFCId);

                    if (articuloKFC.ImagenFile != null)
                    {
                        var folder = "~/Content/Productos";
                        var file = string.Format("{0}.jpg", articuloKFC.Descripcion);
                        var responseLogo = FilesHelper.UploadPhoto(articuloKFC.ImagenFile, folder, file);
                        if (responseLogo)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            articuloKFC.Imagen = pic;
                            db.Entry(articuloKFC).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    TempData["mensajeLito"] = "MATERIAL AGREGADO";

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Descripcion", articuloKFC.FamiliaId);
            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre", articuloKFC.ProveedorId);

            return PartialView(articuloKFC);
        }

        // GET: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult AsignarTiendasCampañas(int? id, int tipo)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Session["Tipo"] = tipo;

            if (tipo == 1)
            {
                Session["tipoArticulo"] = "EQUITY";
            }
            else if (tipo == 2)
            {
                Session["tipoArticulo"] = "FRANQUICIAS";
            }
            else if (tipo == 3)
            {
                Session["tipoArticulo"] = "STOCK";
            }

            var tipoTienda = Session["tipoArticulo"].ToString();

            var tiendasSeleccionadas = db.TiendaArticulos.Where(t => t.ArticuloKFCId == id).ToList();

            //var materialesCampaña = db.CampañaArticuloTMPs.Where(t => t.ArticuloKFCId == id && t.ArticuloKFC.EquityFranquicia == tipoTienda && t.Habilitado == true).OrderBy(t => t.TiendaId).ToList();

            var campaña = db.Campañas.Where(x => x.Generada == "NO").FirstOrDefault();

            var campañaId = campaña.CampañaId;

            var articulosTMP = db.CampañaArticuloTMPs
                       .Where(x => x.ArticuloKFCId == id)
                       .GroupBy(x => new
                       {
                           x.ArticuloKFCId,
                           x.ArticuloKFC.Descripcion,
                           x.CampañaId,
                           x.Cantidad,
                           x.TiendaId,
                           x.Habilitado,
                       })
                       .Select(x => new MaterialesCampaña()
                       {
                           ArticuloKFCId = x.Key.ArticuloKFCId,
                           Campaña = campaña.Nombre,
                           ArticuloKFC = x.Key.Descripcion,
                           Cantidad = x.Key.Cantidad,
                           TiendaId = x.Key.TiendaId,
                           Habilitado = x.Key.Habilitado,
                       });

            var tiendasCampaña = db.Tiendas
                            .Where(x => x.EquityFranquicia == tipoTienda)
                            .GroupBy(x => new { x.Restaurante, x.CCoFranquicia, x.TiendaId })
                            .Select(x => new TiendasCampaña()
                            {
                                Restaurante = x.Key.Restaurante,
                                TiendaId = x.Key.TiendaId,
                                CC = x.Key.CCoFranquicia,
                                TipoTienda = tipoTienda,
                            });

            var materialesCampaña = articulosTMP.Join(tiendasCampaña,
                                 artCamp => artCamp.TiendaId,
                                 tienCamp => tienCamp.TiendaId,
                                 (artCamp, tienCamp) => new { tiendas = tienCamp, materiales = artCamp })
                            .Where(x => x.tiendas.TiendaId == x.materiales.TiendaId)
                            .Where(x => x.tiendas.TipoTienda == tipoTienda)
                            .GroupBy(x => new
                            {
                                x.tiendas.Restaurante,
                                x.tiendas.CC,
                                x.tiendas.TiendaId,
                                x.materiales.ArticuloKFCId,
                                x.materiales.ArticuloKFC,
                                x.materiales.Cantidad,
                                x.materiales.Campaña,
                                x.materiales.Habilitado,
                                x.tiendas.TipoTienda,
                            })
                                    .Select(x => new MaterialesTiendasCampaña()
                                    {
                                        ArticuloKFCId = x.Key.ArticuloKFCId,
                                        ArticuloKFC = x.Key.ArticuloKFC,
                                        Campaña = x.Key.Campaña,
                                        Cantidad = x.Key.Cantidad,
                                        CC = x.Key.CC,
                                        Restaurante = x.Key.Restaurante,
                                        TiendaId = x.Key.TiendaId,
                                        Habilitado = x.Key.Habilitado,
                                        TipoTienda = x.Key.TipoTienda,
                                    });

            var materialesTiendasCampaña = materialesCampaña.Where(m => m.Habilitado == true).ToList();

            if (materialesTiendasCampaña.Count == 0)
            {
                materialesTiendasCampaña = materialesCampaña.ToList();
            }

            return PartialView(materialesTiendasCampaña);
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarTiendasCampañas(FormCollection fc)
        {
            var nombre = User.Identity.Name;
            var usuarioActual = db.Usuarios.Where(u => u.NombreUsuario == nombre).FirstOrDefault();

            string[] tiendaCampañaID = fc.GetValues("TiendaId");
            string[] articuloKFCId = fc.GetValues("ArticuloKFCId");
            string[] cantidadInput = fc.GetValues("CantidadInput");

            var cantidad = 0;
            var tiendaId = 0;
            var articuloId = 0;
            var campañaId = 0;
            var tipo = 0;

            for (var i = 0; i < tiendaCampañaID.Length; i++)
            {
                tiendaId = Convert.ToInt32(tiendaCampañaID[i]);
                articuloId = Convert.ToInt32(articuloKFCId[i]);
                cantidad = Convert.ToInt32(cantidadInput[i]);

                var campañaIdSesion = db.CampañaArticuloTMPs.Where(c => c.ArticuloKFCId == articuloId).FirstOrDefault().CampañaId;

                Session["CampañaId"] = campañaIdSesion;

                CampañaArticuloTMP campañaArticulo = db.CampañaArticuloTMPs.Where(ta => ta.TiendaId == tiendaId && ta.ArticuloKFCId == articuloId).FirstOrDefault();

                if (campañaArticulo.Cantidad != cantidad)
                {
                    campañaArticulo.Cantidad = cantidad;

                    db.Entry(campañaArticulo).State = EntityState.Modified;

                    db.SaveChanges();
                }

            }

            TempData["mensajeLito"] = "RESTAURANTES ASIGNADOS";

            campañaId = (int)Session["CampañaId"];
            tipo = (int)Session["Tipo"];

            return RedirectToAction("CreateCampArt", "Campañas", new { id = campañaId, Tipo = tipo });

        }

        // GET: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult AsignarTiendas(int? id, int tipo)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (tipo == 1)
            {
                Session["tipoArticulo"] = "EQUITY";
            }
            else if (tipo == 2)
            {
                Session["tipoArticulo"] = "FRANQUICIAS";
            }
            else if (tipo == 3)
            {
                Session["tipoArticulo"] = "STOCK";
            }

            var tipoTienda = Session["tipoArticulo"].ToString();

            var tiendaArticulos = db.TiendaArticulos.Where(t => t.ArticuloKFCId == id && t.Tienda.EquityFranquicia == tipoTienda).OrderBy(t => t.Tienda.Restaurante).ToList();


            if (tiendaArticulos == null)
            {
                return HttpNotFound();
            }


            ViewBag.Articulo = db.ArticuloKFCs.Where(t => t.ArticuloKFCId == id).FirstOrDefault().Descripcion;

            return PartialView(tiendaArticulos);
        }

        private bool Update(TiendaArticulo product)
        {
            return true;
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarTiendas(FormCollection fc)
        {
            var nombre = User.Identity.Name;
            var usuarioActual = db.Usuarios.Where(u => u.NombreUsuario == nombre).FirstOrDefault();

            string[] articuloKFCTMPId = fc.GetValues("TiendaArticuloId");
            string[] seleccionado = fc.GetValues("Seleccionado");

            var selec = false;
            var cantidad = 0;
            var campId = 0;

            var campañas = db.Campañas.Where(ct => ct.Generada == "NO").OrderBy(ct => ct.CampañaId).FirstOrDefault();

            if (campañas == null)
            {
                campId = 0;
            }
            else
            {
                campId = campañas.CampañaId;
            }

            for (var i = 0; i < articuloKFCTMPId.Length; i++)
            {
                TiendaArticulo tiendaArticulo = db.TiendaArticulos.Find(Convert.ToInt32(articuloKFCTMPId[i]));

                var tiendaId = tiendaArticulo.TiendaId;
                var articuloId = tiendaArticulo.ArticuloKFCId;

                CampañaArticuloTMP campañaArticulo = db.CampañaArticuloTMPs.Where(ta => ta.TiendaId == tiendaId && ta.ArticuloKFCId == articuloId && ta.CampañaId == campId).FirstOrDefault();

                selec = false;
                cantidad = 0;

                if (seleccionado == null)
                {
                    selec = false;
                    cantidad = 0;

                    tiendaArticulo.Seleccionado = selec;

                    db.Entry(tiendaArticulo).State = EntityState.Modified;

                    campañaArticulo.Habilitado = selec;
                    campañaArticulo.Cantidad = cantidad;

                    db.Entry(campañaArticulo).State = EntityState.Modified;

                    db.SaveChanges();
                }
                else
                {
                    for (var j = 0; j < seleccionado.Length; j++)
                    {
                        if (articuloKFCTMPId[i] == seleccionado[j])
                        {
                            selec = true;

                            tiendaArticulo.Seleccionado = selec;

                            db.Entry(tiendaArticulo).State = EntityState.Modified;

                            var articuloCantidadDefault = db.ArticuloKFCs.Where(a => a.ArticuloKFCId == campañaArticulo.ArticuloKFCId).FirstOrDefault().CantidadDefault;

                            cantidad = articuloCantidadDefault;
                            campañaArticulo.Cantidad = cantidad;
                            campañaArticulo.Habilitado = selec;

                            db.Entry(campañaArticulo).State = EntityState.Modified;
                            db.SaveChanges();

                            break;
                        }
                    }
                    if (!selec)
                    {
                        selec = false;
                        cantidad = 0;

                        tiendaArticulo.Seleccionado = selec;

                        db.Entry(tiendaArticulo).State = EntityState.Modified;

                        campañaArticulo.Habilitado = selec;
                        campañaArticulo.Cantidad = cantidad;

                        db.Entry(campañaArticulo).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                }
            }

            TempData["mensajeLito"] = "RESTAURANTES ASIGNADOS";

            return RedirectToAction("Index");

        }

        // GET: ArticulosKFC/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var articuloKFC = db.ArticuloKFCs.Find(id);

            if (articuloKFC == null)
            {
                return HttpNotFound();
            }

            ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Descripcion", articuloKFC.FamiliaId);
            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre", articuloKFC.ProveedorId);

            return PartialView(articuloKFC);
        }

        // POST: ArticulosKFC/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticuloKFC articuloKFC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articuloKFC).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    MovementsHelper.AgregarArticuloTiendas(articuloKFC.ArticuloKFCId);
                    MovementsHelper.AgregarArticuloCampañas(articuloKFC.ArticuloKFCId);

                    if (articuloKFC.ImagenFile != null)
                    {
                        var folder = "~/Content/Productos";
                        var file = string.Format("{0}.jpg", articuloKFC.ArticuloKFCId);
                        var responseLogo = FilesHelper.UploadPhoto(articuloKFC.ImagenFile, folder, file);
                        if (responseLogo)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            articuloKFC.Imagen = pic;
                            db.Entry(articuloKFC).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    TempData["mensajeLito"] = "MATERIAL EDITADO";
                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Descripcion", articuloKFC.FamiliaId);
            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre", articuloKFC.ProveedorId);

            return PartialView(articuloKFC);
        }

        // GET: ArticulosKFC/Delete/5
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var articuloKFC = db.ArticuloKFCs.Find(id);

            if (articuloKFC == null)
            {
                return HttpNotFound();
            }

            ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Descripcion", articuloKFC.FamiliaId);
            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre", articuloKFC.ProveedorId);

            return View(articuloKFC);
        }

        // POST: ArticulosKFC/Delete/5
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var articulo = db.ArticuloKFCs.Find(id);
            var articuloTienda = db.TiendaArticulos.Where(ta => ta.ArticuloKFCId == id).ToList();
            var articuloCampaña = db.CampañaArticuloTMPs.Where(ca => ca.ArticuloKFCId == id).ToList();

            foreach (var item in articuloTienda)
            {
                db.TiendaArticulos.Remove(item);
                db.SaveChanges();
            }

            foreach (var item2 in articuloCampaña)
            {
                db.CampañaArticuloTMPs.Remove(item2);
                db.SaveChanges();
            }


            db.ArticuloKFCs.Remove(articulo);

            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                TempData["mensajeLito"] = "MATERIAL ELIMINADO";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);

            return PartialView(articulo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
