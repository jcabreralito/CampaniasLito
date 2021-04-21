using CampaniasLito.Classes;
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
    public class MaterialesController : Controller
    {
        public string modulo = "Materiales";
        public string movimiento = string.Empty;

        public class MaterialesCampaña
        {
            public int ArticuloKFCId { get; set; }
            public string ArticuloKFC { get; set; }
            public string Campaña { get; set; }
            public int CampañaId { get; set; }
            public double Cantidad { get; set; }
            public int TiendaId { get; set; }
            public bool Habilitado { get; set; }
            public int ProveedorId { get; set; }
            public int FamiliaId { get; set; }
        }

        public class TiendasCampaña
        {
            public int TiendaId { get; set; }
            public string Restaurante { get; set; }
            public string Clasificacion { get; set; }
            public string CC { get; set; }
            public string TipoTienda { get; set; }
            public string Region { get; set; }
            public string Ciudad { get; set; }
            public string Direccion { get; set; }
            //00000000000000000000000000 GENERALES 0000000000000000000000000000000000
            public string Tipo { get; set; }
            public string NuevoNivelDePrecio { get; set; }
            public bool MenuDigital { get; set; }
            public string CantidadDePantallas { get; set; }
            ////00000000000000000000000000 POR PRODUCTO 0000000000000000000000000000000000
            public bool TerceraReceta { get; set; }
            public bool Arroz { get; set; }
            public bool Hamburgesas { get; set; }
            public bool Ensalada { get; set; }
            public bool PET2Litros { get; set; }
            public bool Postres { get; set; }
            public bool BisquetMiel { get; set; }
            public bool KeCono { get; set; }
            public bool KREAMBALL { get; set; }

            ////00000000000000000000000000 MATERIALES ESPECIFICOS 0000000000000000000000000000000000

            public bool MenuBackLigth { get; set; }
            public bool Autoexpress { get; set; }
            public bool CopeteAERemodelado { get; set; }
            public bool CopeteAETradicional { get; set; }
            public bool PanelDeInnovacion { get; set; }
            public bool DisplayDeBurbuja { get; set; }
            public bool Delivery { get; set; }
            public bool MERCADO_DE_PRUEBA { get; set; }
            public bool AreaDeJuegos { get; set; }
            public bool COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS { get; set; }
            public bool COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L { get; set; }
            public bool DisplayDePiso { get; set; }
            public bool WCNACIONAL67X100cm { get; set; }

            ////00000000000000000000000000 MEDIDAS ESPECIALES 0000000000000000000000000000000000

            public bool WCMedidaEspecial60_8x85cm { get; set; }
            public bool WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm { get; set; }
            public bool WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm { get; set; }
            public bool WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm { get; set; }
            public bool WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm { get; set; }
            public bool MedidaEspecialPanelDeComplementos { get; set; }
            public bool MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm { get; set; }


            //00000000000000000000000000 POR EQUIPO EN EL RESTAURANTE 0000000000000000000000000000000000

            public string TipoDeCaja { get; set; }
            public string AcomodoDeCajas { get; set; }
            public string NoMesaDeAreaComedor { get; set; }
            public string NoMesaDeAreaDeJuegos { get; set; }
            public string NumeroDeVentanas { get; set; }
            public string UbicacionPantallas { get; set; }

        }

        public class MaterialesTiendasCampaña
        {
            public int ArticuloKFCId { get; set; }
            public string ArticuloKFC { get; set; }
            public string Campaña { get; set; }
            public int CampañaId { get; set; }
            public double Cantidad { get; set; }
            public int TiendaId { get; set; }
            public string Restaurante { get; set; }
            public string Clasificacion { get; set; }
            public string CC { get; set; }
            public string Region { get; set; }
            public string Ciudad { get; set; }
            public string Direccion { get; set; }
            public bool Habilitado { get; set; }
            public string TipoTienda { get; set; }
            public string Tipo { get; set; }
            public string NuevoNivelDePrecio { get; set; }
            public bool MenuDigital { get; set; }
            public string CantidadDePantallas { get; set; }
            public bool TerceraReceta { get; set; }
            public bool Arroz { get; set; }
            public bool Hamburgesas { get; set; }
            public bool Ensalada { get; set; }
            public bool PET2Litros { get; set; }
            public bool Postres { get; set; }
            public bool BisquetMiel { get; set; }
            public bool KeCono { get; set; }
            public bool KREAMBALL { get; set; }
            public bool MenuBackLigth { get; set; }
            public bool Autoexpress { get; set; }
            public bool CopeteAERemodelado { get; set; }
            public bool CopeteAETradicional { get; set; }
            public bool PanelDeInnovacion { get; set; }
            public bool DisplayDeBurbuja { get; set; }
            public bool Delivery { get; set; }
            public bool MERCADO_DE_PRUEBA { get; set; }
            public bool AreaDeJuegos { get; set; }
            public bool COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS { get; set; }
            public bool COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L { get; set; }
            public bool DisplayDePiso { get; set; }
            public bool WCNACIONAL67X100cm { get; set; }
            public bool WCMedidaEspecial60_8x85cm { get; set; }
            public bool WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm { get; set; }
            public bool WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm { get; set; }
            public bool WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm { get; set; }
            public bool WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm { get; set; }
            public bool MedidaEspecialPanelDeComplementos { get; set; }
            public bool MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm { get; set; }
            public bool MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm { get; set; }
            public string TipoDeCaja { get; set; }
            public string AcomodoDeCajas { get; set; }
            public string NoMesaDeAreaComedor { get; set; }
            public string NoMesaDeAreaDeJuegos { get; set; }
            public string NumeroDeVentanas { get; set; }
            public string UbicacionPantallas { get; set; }
            public int ProveedorId { get; set; }
            public int FamiliaId { get; set; }
        }

        public class spArticulosTiendas
        {
            public long TiendaArticuloId { get; set; }
            public int ArticuloKFCId { get; set; }
            public int TiendaId { get; set; }
            public bool Seleccionado { get; set; }
            public int CantidadDefault { get; set; }
            public string Restaurante { get; set; }
            public string Material { get; set; }
            public string EquityFranquicia { get; set; }
            public string CCoFranquicia { get; set; }

        }

        public class spArticuloKFC
        {
            public int ArticuloKFCId { get; set; }
            public string Descripcion { get; set; }
            public string Proveedor { get; set; }
            public string Familia { get; set; }
            public int CantidadDefault { get; set; }
            public string EquityFranquicia { get; set; }
            public string Observaciones { get; set; }
            public bool Eliminado { get; set; }
            public bool Activo { get; set; }
            public bool Todo { get; set; }
            public string Imagen { get; set; }

        }

        private readonly CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Materiales
        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index()
        {
            Session["iconoTitulo"] = "fas fa-barcode";
            Session["homeB"] = string.Empty;
            Session["rolesB"] = string.Empty;
            Session["compañiasB"] = string.Empty;
            Session["usuariosB"] = string.Empty;
            Session["regionesB"] = string.Empty;
            Session["ciudadesB"] = string.Empty;
            Session["restaurantesB"] = string.Empty;
            Session["familiasB"] = string.Empty;
            Session["materialesB"] = "active";
            Session["materialesAcB"] = string.Empty;
            Session["campañasB"] = string.Empty;
            Session["reglasB"] = string.Empty;
            Session["bitacoraB"] = string.Empty;
            Session["Mensaje"] = string.Empty;

            return View();
        }

        public ActionResult GetData(JqueryDatatableParam param)
        {
            //var employees = GetEmployees();
            var equityList = db.Database.SqlQuery<spArticuloKFC>("spGetMaterialesAll").ToList();

            equityList.ToList().ForEach(x => x.ArticuloKFCId = x.ArticuloKFCId);

            //var equityList = db.Database.SqlQuery<spArticuloKFC>("spGetMaterialesAll").ToList();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                equityList = equityList.Where(x => x.Descripcion.ToLower().Contains(param.sSearch.ToLower())
                                              || x.EquityFranquicia.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Familia.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Proveedor.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Observaciones.ToLower().Contains(param.sSearch.ToLower())
                                              || x.ArticuloKFCId.ToString().Contains(param.sSearch.ToLower())
                                              || x.Activo.ToString().Contains(param.sSearch.ToLower())
                                              || x.Todo.ToString().Contains(param.sSearch.ToLower())
                                              || x.CantidadDefault.ToString().Contains(param.sSearch.ToLower())
                                              || x.Eliminado.ToString().Contains(param.sSearch.ToLower())).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            //if (sortColumnIndex == 3)
            //{
            //    equityList = sortDirection == "asc" ? equityList.OrderBy(c => c.CantidadDefault) : equityList.OrderByDescending(c => c.Descripcion);
            //}
            //else if (sortColumnIndex == 4)
            //{
            //    equityList = sortDirection == "asc" ? equityList.OrderBy(c => c.StartDate) : equityList.OrderByDescending(c => c.StartDate);
            //}
            //else if (sortColumnIndex == 5)
            //{
            //    equityList = sortDirection == "asc" ? equityList.OrderBy(c => c.Salary) : equityList.OrderByDescending(c => c.Salary);
            //}
            //else
            //{
            //    Func<ArticuloKFC, string> orderingFunction = e => sortColumnIndex == 0 ? e.Name : sortColumnIndex == 1 ? e.Position : e.Location;
            //    employees = sortDirection == "asc" ? employees.OrderBy(orderingFunction) : employees.OrderByDescending(orderingFunction);
            //}

            var displayResult = equityList.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();
            var totalRecords = equityList.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = equityList
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetDataEquity()
        {
            //var equityList = db.Database.SqlQuery<spArticuloKFC>("spGetMaterialesActivos").ToList();
            var equityList = db.Database.SqlQuery<spArticuloKFC>("spGetMaterialesAll").ToList();

            return Json(new { data = equityList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataFranquicias()
        {
            var equityList = db.Database.SqlQuery<spArticuloKFC>("spGetMaterialesFranquicias").ToList();

            return Json(new { data = equityList }, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUser(idOperacion: 1)]
        [HttpGet]
        public ActionResult AddOrEdit(int? cat, int id = 0)
        {
            var categoria = string.Empty;
            if (id == 0)
            {
                if (cat == 1)
                {
                    categoria = "EQUITY";
                }
                else if (cat == 2)
                {
                    categoria = "FRANQUICIAS";
                }

                Session["Categoria"] = categoria;

                ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre");
                ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Descripcion");
                ViewBag.EquityFranquicia = new SelectList(CombosHelper.GetTipoCampañasMat(true), "Nombre", "Nombre");

                return PartialView(new ArticuloKFC());
            }
            else
            {
                var tipo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == id).FirstOrDefault().EquityFranquicia;
                Session["Categoria"] = tipo;

                var proveedorId = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == id).FirstOrDefault().ProveedorId;
                var familiaId = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == id).FirstOrDefault().FamiliaId;

                ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre", proveedorId);
                ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Descripcion", familiaId);
                ViewBag.EquityFranquicia = new SelectList(CombosHelper.GetTipoCampañasMat(true), "Nombre", "Nombre", tipo);

                return PartialView(db.ArticuloKFCs.Where(x => x.ArticuloKFCId == id).FirstOrDefault());
            }
        }

        public ActionResult ActivarDesactivar(int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault().UsuarioId;
            var restauranteId = 0;
            var campaña = db.Campañas.Where(x => x.Generada == "NO").FirstOrDefault();

            var material = db.Database.SqlQuery<ArticuloKFC>("spGetMaterialesXId @ArticuloKFCId",
                new SqlParameter("@ArticuloKFCId", id)).FirstOrDefault();

            var activo = material.Activo;

            var materialEditado = db.ArticuloKFCs.Find(id);

            var Mensaje = string.Empty;

            if (material.FamiliaId != 22)
            {
                movimiento = "Desactivando Material";
                MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                if (activo == true)
                {
                    materialEditado.Activo = false;
                    Mensaje = "Material Desactivado ";

                    db.Entry(materialEditado).State = EntityState.Modified;
                    DBHelper.SaveChanges(db);

                    EliminarMateriales(id, campaña);
                }
                else
                {
                    movimiento = "Activando Material";
                    MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                    materialEditado.Activo = true;
                    Mensaje = "Material Activado ";

                    db.Entry(materialEditado).State = EntityState.Modified;
                    DBHelper.SaveChanges(db);

                    ActualizarMaterial(id);

                    //EliminarMateriales(id, campaña);

                    //MovementsHelper.AgregarMaterialesTiendaCampañaExiste(material.ArticuloKFCId, restauranteId);
                    //if (campaña != null)
                    //{
                    //    var campañaId = campaña.CampañaId;

                    //    MovementsHelper.AgregarArticuloCampañas(materialEditado, campañaId);
                    //}

                }
            }
            else
            {
                if (activo == true)
                {

                    movimiento = "Desactivando Material";
                    MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                    materialEditado.Activo = false;
                    Mensaje = "Material Desactivado ";

                    db.Entry(materialEditado).State = EntityState.Modified;
                    DBHelper.SaveChanges(db);

                    EliminarMaterialesMoto(id, campaña);

                    if (campaña != null)
                    {
                        var campañaId = campaña.CampañaId;

                        MovementsHelper.AgregarArticuloCampañas(material, campañaId);
                    }

                }
                else
                {
                    movimiento = "Activando Material";
                    MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                    materialEditado.Activo = true;
                    Mensaje = "Material Activado ";

                    db.Entry(materialEditado).State = EntityState.Modified;
                    DBHelper.SaveChanges(db);

                    EliminarMaterialesMoto(id, campaña);

                    var categoria = string.Empty;

                    MovementsHelper.AgregarMaterialesTiendaCampañaExiste(material.ArticuloKFCId, restauranteId, categoria);

                    if (campaña != null)
                    {
                        var campañaId = campaña.CampañaId;

                        MovementsHelper.AgregarArticuloCampañas(material, campañaId);
                    }
                }
            }

            movimiento = Mensaje + id + " " + material.Descripcion + " / " + material.EquityFranquicia;
            MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

            return Json(new { success = true, message = Mensaje }, JsonRequestBehavior.AllowGet);

        }

        [AuthorizeUser(idOperacion: 1)]
        [HttpPost]
        public ActionResult AddOrEdit(ArticuloKFC material)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault().UsuarioId;
            var restauranteId = 0;
            if (material.ArticuloKFCId == 0)
            {
                movimiento = "Agregando Material";
                MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                var tipo = Session["Categoria"].ToString();

                material.Activo = true;
                material.EquityFranquicia = tipo;

                if (material.Observaciones == null)
                {
                    material.Observaciones = string.Empty;
                }

                db.ArticuloKFCs.Add(material);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    CargarImagen(material);

                    var categoria = string.Empty;

                    MovementsHelper.AgregarMaterialesTiendaCampañaExiste(material.ArticuloKFCId, restauranteId, categoria);

                    var campaña = db.Campañas.Where(x => x.Generada == "NO").FirstOrDefault();

                    if (campaña != null)
                    {
                        var campañaId = campaña.CampañaId;

                        MovementsHelper.AgregarArticuloCampañas(material, campañaId);
                    }

                    movimiento = "Material Agregado" + material.ArticuloKFCId + " " + material.Descripcion + " / " + material.EquityFranquicia;
                    MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                    return Json(new { success = true, message = "MATERIAL AGREGADO" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = response.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                movimiento = "Actualizando Material";
                MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                var tipo = Session["Categoria"].ToString();

                //material.EquityFranquicia = tipo;

                if (material.Observaciones == null)
                {
                    material.Observaciones = string.Empty;
                }

                db.Entry(material).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    CargarImagen(material);

                    var campaña = db.Campañas.Where(x => x.Generada == "NO").FirstOrDefault();

                    var id = material.ArticuloKFCId;

                    if (material.FamiliaId != 22)
                    {
                        if (material.Activo == true)
                        {

                            EliminarMateriales(id, campaña);

                            var categoria = string.Empty;

                            MovementsHelper.AgregarMaterialesTiendaCampañaExiste(material.ArticuloKFCId, restauranteId, categoria);

                            if (campaña != null)
                            {
                                var campañaId = campaña.CampañaId;

                                MovementsHelper.AgregarArticuloCampañas(material, campañaId);
                            }

                        }
                        else
                        {
                            EliminarMateriales(id, campaña);
                        }
                    }
                    else
                    {
                        if (material.Activo == true)
                        {

                            EliminarMaterialesMoto(id, campaña);

                            var categoria = string.Empty;

                            MovementsHelper.AgregarMaterialesTiendaCampañaExiste(material.ArticuloKFCId, restauranteId, categoria);

                            if (campaña != null)
                            {
                                var campañaId = campaña.CampañaId;

                                MovementsHelper.AgregarArticuloCampañas(material, campañaId);
                            }

                        }
                        else
                        {
                            EliminarMaterialesMoto(id, campaña);
                        }
                    }

                    movimiento = "Actualizar Material " + material.ArticuloKFCId + " " + material.Descripcion + " / " + material.EquityFranquicia;
                    MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                    return Json(new { success = true, message = "MATERIAL ACTUALIZADO" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = true, message = response.Message }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [AuthorizeUser(idOperacion: 1)]
        [HttpPost]
        public ActionResult ActualizarMaterial(int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault().UsuarioId;
            var restauranteId = 0;

            var material = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == id).FirstOrDefault();
            //material.EquityFranquicia = tipo;

            if (material.Observaciones == null)
            {
                material.Observaciones = string.Empty;
            }

            movimiento = "Actualizando Material";
            MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

            db.Entry(material).State = EntityState.Modified;
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                CargarImagen(material);

                var campaña = db.Campañas.Where(x => x.Generada == "NO").FirstOrDefault();

                if (material.FamiliaId != 22)
                {
                    if (material.Activo == true)
                    {

                        EliminarMateriales(id, campaña);

                        var categoria = string.Empty;

                        MovementsHelper.AgregarMaterialesTiendaCampañaExiste(material.ArticuloKFCId, restauranteId, categoria);

                        if (campaña != null)
                        {
                            var campañaId = campaña.CampañaId;

                            MovementsHelper.AgregarArticuloCampañas(material, campañaId);
                        }

                    }
                    else
                    {
                        EliminarMateriales(id, campaña);
                    }
                }
                else
                {
                    if (material.Activo == true)
                    {

                        EliminarMaterialesMoto(id, campaña);

                        if (campaña != null)
                        {
                            var campañaId = campaña.CampañaId;

                            MovementsHelper.AgregarArticuloCampañas(material, campañaId);
                        }

                    }
                    else
                    {
                        EliminarMaterialesMoto(id, campaña);
                    }
                }

                movimiento = "Actualizar Material " + material.ArticuloKFCId + " " + material.Descripcion + " / " + material.EquityFranquicia;
                MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                return Json(new { success = true, message = "MATERIAL ACTUALIZADO" }, JsonRequestBehavior.AllowGet);
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

        private void EliminarMaterialesMoto(int id, Campaña campaña)
        {
            if (campaña != null)
            {
                db.Database.ExecuteSqlCommand(
                "spEliminarMaterialCampaniasTiendas @ArticuloKFCId, @CampaniaId",
                new SqlParameter("@ArticuloKFCId", id),
                new SqlParameter("@CampaniaId", campaña.CampañaId));
            }
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

        private void CargarImagen(ArticuloKFC articuloKFC)
        {
            if (articuloKFC.ImagenFile != null)
            {
                var folder = "~/Content/images/Productos";
                var local = "/";
                var file = string.Format("{0}.jpg", articuloKFC.Descripcion);
                var responseLogo = FilesHelper.UploadPhoto(articuloKFC.ImagenFile, folder, file);
                if (responseLogo)
                {
                    var pic = string.Format("{0}/{1}", local, file);
                    articuloKFC.Imagen = pic;
                    db.Entry(articuloKFC).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpGet]
        public ActionResult Restaurantes(int? id, string cat)
        {

            var restaurantesList = db.Database.SqlQuery<spArticulosTiendas>("spGetArticuloTiendas @ArticuloKFCId, @EquityFranquicia",
                new SqlParameter("@ArticuloKFCId", id),
                new SqlParameter("@EquityFranquicia", cat)).ToList();

            if (restaurantesList == null)
            {
                return HttpNotFound();
            }

            ViewBag.Campañas = db.Campañas.Where(x => x.Generada == "NO").ToList();
            ViewBag.Material = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == id).FirstOrDefault().Descripcion;

            return PartialView(restaurantesList);
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        public ActionResult Restaurantes(FormCollection fc)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault().UsuarioId;

            movimiento = "Asignando Restaurantes";
            MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

            string[] campañaId = fc.GetValues("Campaña");
            string[] articuloKFCTMPId = fc.GetValues("TiendaArticuloId");
            string[] seleccionado = fc.GetValues("Seleccionado");

            var selec = false;
            var cantidad = 0;
            var campId = 0;

            if (campañaId == null)
            {
                for (var i = 0; i < articuloKFCTMPId.Length; i++)
                {
                    TiendaArticulo tiendaArticulo = db.TiendaArticulos.Find(Convert.ToInt32(articuloKFCTMPId[i]));

                    var tiendaId = tiendaArticulo.TiendaId;
                    var articuloId = tiendaArticulo.ArticuloKFCId;

                    if (seleccionado == null)
                    {
                        selec = false;
                        cantidad = 0;

                        tiendaArticulo.Seleccionado = selec;

                        db.Entry(tiendaArticulo).State = EntityState.Modified;

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
                            db.SaveChanges();

                        }
                    }
                }

            }
            else
            {
                for (int c = 0; c < campañaId.Length; c++)
                {
                    Campaña campañaArtId = db.Campañas.Find(Convert.ToInt32(campañaId[c]));
                    campId = campañaArtId.CampañaId;

                    for (var i = 0; i < articuloKFCTMPId.Length; i++)
                    {
                        TiendaArticulo tiendaArticulo = db.TiendaArticulos.Find(Convert.ToInt32(articuloKFCTMPId[i]));

                        var tiendaId = tiendaArticulo.TiendaId;
                        var articuloId = tiendaArticulo.ArticuloKFCId;

                        var campañas = db.Campañas.Where(ct => ct.Generada == "NO" && ct.CampañaId == campId).OrderBy(ct => ct.CampañaId).FirstOrDefault();

                        if (campañas == null)
                        {

                            if (seleccionado == null)
                            {
                                selec = false;
                                cantidad = 0;

                                tiendaArticulo.Seleccionado = selec;

                                db.Entry(tiendaArticulo).State = EntityState.Modified;

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
                                    db.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            campId = campañas.CampañaId;
                            CampañaArticuloTMP campañaArticulo = db.CampañaArticuloTMPs.Where(ta => ta.TiendaId == tiendaId && ta.ArticuloKFCId == articuloId && ta.CampañaId == campId).FirstOrDefault();

                            selec = false;
                            cantidad = 0;
                            if (campañaArticulo == null)
                            {
                                var codigo = 0;

                                db.Database.ExecuteSqlCommand(
                                "spAgregarMaterialCAmpanias @ArticuloKFCId, @TiendaId, @CampañaId, @Habilitado, @Cantidad, @Codigo",
                                new SqlParameter("@ArticuloKFCId", articuloId),
                                new SqlParameter("@TiendaId", tiendaId),
                                new SqlParameter("@CampañaId", campId),
                                new SqlParameter("@Habilitado", false),
                                new SqlParameter("@Cantidad", cantidad),
                                new SqlParameter("@Codigo", codigo));

                            }
                            else
                            {
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
                        }
                    }
                }
            }

            TiendaArticulo articulo = db.TiendaArticulos.Find(Convert.ToInt32(articuloKFCTMPId[0]));

            movimiento = "Asignar Restaurantes / " + articulo.ArticuloKFCId + " / " + articulo.ArticuloKFC.Descripcion;
            MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

            return Json(new { success = true, message = "RESTAURANTES ASIGNADOS" }, JsonRequestBehavior.AllowGet);

            //return RedirectToAction("Index");

        }

        // GET: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Cantidades(int? id, int cam)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tipo = Session["TipoRestaurante"].ToString();

            var tipoTienda = tipo;

            var tiendasSeleccionadas = db.TiendaArticulos.Where(t => t.ArticuloKFCId == id).ToList();

            //var materialesCampaña = db.CampañaArticuloTMPs.Where(t => t.ArticuloKFCId == id && t.ArticuloKFC.EquityFranquicia == tipoTienda && t.Habilitado == true).OrderBy(t => t.TiendaId).ToList();

            var campaña = db.Campañas.Where(x => x.CampañaId == cam).FirstOrDefault();

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
                           CampañaId = x.Key.CampañaId,
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
                                x.materiales.CampañaId,
                                x.materiales.Habilitado,
                                x.tiendas.TipoTienda,
                            })
                                    .Select(x => new MaterialesTiendasCampaña()
                                    {
                                        ArticuloKFCId = x.Key.ArticuloKFCId,
                                        ArticuloKFC = x.Key.ArticuloKFC,
                                        Campaña = x.Key.Campaña,
                                        CampañaId = x.Key.CampañaId,
                                        Cantidad = x.Key.Cantidad,
                                        CC = x.Key.CC,
                                        Restaurante = x.Key.Restaurante,
                                        TiendaId = x.Key.TiendaId,
                                        Habilitado = x.Key.Habilitado,
                                        TipoTienda = x.Key.TipoTienda,
                                    });

            var materialesTiendasCampaña = materialesCampaña.Where(m => m.Habilitado == true && m.CampañaId == campañaId).ToList();

            //if (materialesTiendasCampaña.Count == 0)
            //{
            //    materialesTiendasCampaña = materialesCampaña.ToList();
            //}

            //ViewBag.Campañas = db.Campañas.Where(x => x.Generada == "NO").ToList();
            ViewBag.Material = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == id).FirstOrDefault().Descripcion;

            return PartialView(materialesTiendasCampaña);
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        public ActionResult Cantidades(FormCollection fc)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault().UsuarioId;

            movimiento = "Asignando Cantidades";
            MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

            string[] tiendaCampañaID = fc.GetValues("TiendaId");
            string[] articuloKFCId = fc.GetValues("ArticuloKFCId");
            string[] cantidadInput = fc.GetValues("CantidadInput");
            string[] campañaId = fc.GetValues("CampañaId");
            string[] seleccionado = fc.GetValues("Seleccionado");

            var cantidad = 0;
            var tiendaId = 0;
            var articuloId = 0;
            var campId = 0;

            for (var i = 0; i < tiendaCampañaID.Length; i++)
            {
                tiendaId = Convert.ToInt32(tiendaCampañaID[i]);
                articuloId = Convert.ToInt32(articuloKFCId[i]);
                cantidad = Convert.ToInt32(cantidadInput[i]);
                campId = Convert.ToInt32(campañaId[i]);

                //var campañaIdSesion = db.CampañaArticuloTMPs.Where(c => c.ArticuloKFCId == articuloId).FirstOrDefault().CampañaId;

                //Session["CampañaId"] = campañaIdSesion;

                CampañaArticuloTMP campañaArticulo = db.CampañaArticuloTMPs.Where(ta => ta.TiendaId == tiendaId && ta.ArticuloKFCId == articuloId && ta.CampañaId == campId).FirstOrDefault();

                if (campañaArticulo.Cantidad != cantidad)
                {
                    campañaArticulo.Cantidad = cantidad;

                    db.Entry(campañaArticulo).State = EntityState.Modified;

                    db.SaveChanges();
                }

            }

            var articulo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == articuloId).FirstOrDefault().Descripcion;

            movimiento = "Asignar Cantidades " + articulo;
            MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

            return Json(new { success = true, message = "CANTIDADES ASIGNADAS" }, JsonRequestBehavior.AllowGet);

        }

        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault().UsuarioId;

            movimiento = "Eliminando Material";
            MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

            var material = db.ArticuloKFCs.Find(id);

            material.Eliminado = true;
            material.Activo = false;

            db.Entry(material).State = EntityState.Modified;
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                var campaña = db.Campañas.Where(x => x.Generada == "NO").FirstOrDefault();

                EliminarMateriales(id, campaña);

                //db.Database.ExecuteSqlCommand(
                //"spEliminarMaterialTiendas @ArticuloKFCId",
                //new SqlParameter("@ArticuloKFCId", id));


                //if (campaña != null)
                //{
                //    db.Database.ExecuteSqlCommand(
                //    "spEliminarMaterialCampaniasTiendas @ArticuloKFCId, @CampaniaId",
                //    new SqlParameter("@ArticuloKFCId", id),
                //    new SqlParameter("@CampaniaId", campaña.CampañaId));
                //}

                //MovementsHelper.AgregarArticuloCampañas(material);

                movimiento = "Eliminar Material " + material.ArticuloKFCId + " " + material.Descripcion + " / " + material.EquityFranquicia;
                MovementsHelper.MovimientosBitacora(usuario, modulo, movimiento);

                return Json(new { success = true, message = "MATERIAL ELIMINADO" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, message = response.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}