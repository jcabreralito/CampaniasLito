using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Filters;
using CampaniasLito.Models;
using PagedList;

namespace CampaniasLito.Controllers
{
    public class CotizacionDetalleTotales
    {
        public int CampañaId { get; set; }
        public int ArticuloId { get; set; }
        public double Cantidad { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public double TotalCantidad { get; set; }

    }
    public class CampañasController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Campañas
        public ActionResult GetList()
        {
            var campañasList = db.Campañas.ToList<Campaña>();
            return Json(new { data = campañasList }, JsonRequestBehavior.AllowGet);

        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index(string campaña)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(campaña))
            {
                Session["campañaFiltro"] = string.Empty;
            }
            else
            {
                Session["campañaFiltro"] = campaña;
            }

            var filtro = Session["campañaFiltro"].ToString();

            var campañas = db.Campañas.Where(c => c.CompañiaId == usuario.CompañiaId);

            if (!string.IsNullOrEmpty(campaña))
            {
                return View(campañas.Where(a => a.Nombre.Contains(filtro) || a.Generada.Contains(filtro) || a.Descripcion.Contains(filtro)).ToList());
            }
            else
            {
                return View(campañas.ToList());
            }

        }

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 6)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var campaña = db.Campañas.Find(id);

            if (campaña == null)
            {
                return HttpNotFound();
            }

            return PartialView(campaña);
        }

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult CreateCampArt(string tiendaCampania, int? id, int? page = null)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrEmpty(tiendaCampania))
            {
                Session["tiendaCampaniaFiltro"] = string.Empty;
            }
            else
            {
                Session["tiendaCampaniaFiltro"] = tiendaCampania.ToUpper();
            }

            var filtro = Session["tiendaCampaniaFiltro"].ToString();

            var tiendas = db.Tiendas.ToList();

            page = (page ?? 1);

            List<CampañaTiendaTMP> campañaTiendas = db.CampañaTiendaTMPs.Where(ct => ct.CampañaId == id).ToList();

            var campañas = db.Campañas.Where(ct => ct.Generada == "NO").OrderBy(ct => ct.CampañaId).FirstOrDefault().CampañaId;

            if (campañaTiendas == null)
            {
                return HttpNotFound();
            }

            Session["campaniaId"] = id;

            if (campañas < id)
            {
                TempData["mensajeLito"] = "HAY UNA CAMPAÑA EN PROCESO";
                return RedirectToAction("Index");
            }

            var articulosTMPsId = db.CampañaArticuloTMPs.Where(cdt => cdt.CampañaTiendaTMPId == id).ToList();

            if (articulosTMPsId.Count == 0)
            {
                foreach (var item in campañaTiendas)
                {
                    var response = MovementsHelper.AgregarArticulosTiendas(usuario.Compañia.CompañiaId, usuario.NombreUsuario, item.TiendaId, (int)id);

                    if (!response.Succeeded)
                    {
                        TempData["mensajeLito"] = "ERROR AL AGREGAR ARTICULOS A LA CAMPAÑA";
                        return RedirectToAction("Index");
                    }
                }
            }

            var numeroPaginas = 15;

            if (!string.IsNullOrEmpty(tiendaCampania))
            {
                return PartialView(campañaTiendas.Where(a => a.Tienda.Restaurante.Contains(filtro)).ToPagedList((int)page, numeroPaginas));
            }
            else
            {
                return PartialView(campañaTiendas.ToPagedList((int)page, numeroPaginas));
            }



            //ViewBag.TotalPrice = totalImporte.ToString("C2");
            //var totalImporte = detalleCampaña.Sum(m => m.Importe);
            //ViewBag.FechaCotizacion = cotizacion.FechaCotizacion;
            //ViewBag.Detalles = detalleCotizaciones;
            //ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NombreCliente", cotizacion.ClienteId);
            //ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Descripcion", cotizacion.StatusId);
            //return View(cotizacion);

        }

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult NuevaCampaña(int campañaId)
        {
            TiendasArticulosView campañas = new TiendasArticulosView();

            campañas.Campañas = db.Campañas.Where(cat => cat.CampañaId == campañaId).ToList();


            var detalleCampaña = db.CampañaArticuloTMPs.Where(c => c.CampañaTiendaTMPId == campañaId).ToList();

            ViewBag.TotalCantidad = detalleCampaña.Sum(m => m.Cantidad).ToString("N0");

            if (campañas == null)
            {
                return HttpNotFound();
            }

            return PartialView(campañas);
        }

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult ArticulosCampañas(int tiendaId, int campañaId, TiendasArticulosView campañaArticulos)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            //TiendasArticulosView campañaArticulos = new TiendasArticulosView();

            campañaArticulos.CampañaArticuloTMPs = db.CampañaArticuloTMPs.Where(cat => cat.TiendaId == tiendaId && cat.CampañaTiendaTMPId == campañaId).OrderBy(cat => cat.ArticuloKFC.Familia.Codigo).ThenBy(cat => cat.ArticuloKFCId).ToList();
            campañaArticulos.ArticuloKFCs = db.ArticuloKFCs.Where(cat => cat.CompañiaId == usuario.CompañiaId).OrderBy(cat => cat.Familia.Codigo).ThenBy(cat => cat.ArticuloKFCId).ToList();

            if (campañaArticulos == null)
            {
                return HttpNotFound();
            }

            return PartialView(campañaArticulos);
        }

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult ArticulosCampañasDescripcion()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            TiendasArticulosView campañaArticulos = new TiendasArticulosView();

            campañaArticulos.ArticuloKFCs = db.ArticuloKFCs.Where(cat => cat.CompañiaId == usuario.CompañiaId).OrderBy(cat => cat.Familia.Codigo).ThenBy(cat => cat.ArticuloKFCId).ToList();
            //campañaArticulos.ArticuloKFCs = db.ArticuloKFCs.Where(cat => cat.CompañiaId == usuario.CompañiaId).ToList();

            //var detalleArticulosCampaña = db.CampañaArticuloTMPs.Where(c => c.CampañaTiendaTMPId == campañaId).ToList();

            //var totales = (from dc in detalleArticulosCampaña
            //               group new { detalleCot = dc }
            //               by new { dc.CampañaTiendaTMPId, dc.ArticuloKFCId, dc.Cantidad, } into grupo
            //               orderby grupo.Key.ArticuloKFCId
            //               select new CotizacionDetalleTotales
            //               {
            //                   ArticuloId = grupo.Key.ArticuloKFCId,
            //                   TotalCantidad = grupo.Sum(x => x.detalleCot.Cantidad),
            //               }).ToList();

            //var ta = (from s in totales
            //          group s by s.ArticuloId into g
            //          select new CotizacionDetalleTotales
            //          {
            //              ArticuloId = g.Key,
            //              TotalCantidad = g.Sum(p => p.TotalCantidad),
            //          }).ToList();

            //ViewBag.Totales = ta.ToList();

            if (campañaArticulos == null)
            {
                return HttpNotFound();
            }

            return PartialView(campañaArticulos);
        }

        [AuthorizeUser(idOperacion: 2)]
        public ActionResult ArticulosCampañasTotales(int campañaId)
        {
            TiendasArticulosView campañaArticulos = new TiendasArticulosView();

            campañaArticulos.CampañaArticuloTMPs = db.CampañaArticuloTMPs.Where(cat => cat.CampañaTiendaTMPId == campañaId).ToList();

            campañaArticulos.CampañaArticuloTMPs.Sum(tc => tc.Cantidad);

            if (campañaArticulos == null)
            {
                return HttpNotFound();
            }

            return PartialView(campañaArticulos);
        }

        // GET: Campañas/Create
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult CreateCamp(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var response = MovementsHelper.AgregarTiendas(usuario.Compañia.CompañiaId, usuario.NombreUsuario, (int)id);

            if (response.Succeeded)
            {
                //TempData["mensajeLito"] = "CAMPAÑA AGREGADA";
            }

            ModelState.AddModelError(string.Empty, response.Message);

            var view = new NuevaCampañaView
            {
                CampañaId = db.NuevaCampañaViews.Where(cdt => cdt.CampañaId == (int)id).FirstOrDefault().CampañaId,
                Descripcion = db.NuevaCampañaViews.Where(cdt => cdt.CampañaId == (int)id).FirstOrDefault().Descripcion,
                Nombre = db.NuevaCampañaViews.Where(cdt => cdt.CampañaId == (int)id).FirstOrDefault().Nombre,
                Detalles = db.CampañaTiendaTMPs.Where(cdt => cdt.Usuario == usuario.NombreUsuario).ToList(),
            };

            Session["CampañaId"] = view.CampañaId;

            return View(view);


        }

        //=================================================================================================================================
        //=                                                                                                                               =
        //=                                                                                                                               =
        //=                                                                                                                               =
        //=================================================================================================================================

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 8)]
        public ActionResult CodesCampArt(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index");
            }

            var response = MovementsHelper.GenerarCodigos(id, usuario.CompañiaId);

            if (response.Succeeded)
            {
                //TempData["mensajeLito"] = "CAMPAÑA AGREGADA";
            }

            return RedirectToAction("Index");
        }

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult CloseCampArt(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var campaña = db.Campañas.Find(id);

            if (campaña == null)
            {
                return HttpNotFound();
            }

            return View(campaña);
        }

        // POST: Campañas/Delete/5
        [AuthorizeUser(idOperacion: 4)]
        [HttpPost, ActionName("CloseCampArt")]
        [ValidateAntiForgeryToken]
        public ActionResult CloseCampArtConfirmed(int id)
        {
            var campaña = db.Campañas.Find(id);
            campaña.Generada = "SI";
            db.Entry(campaña).State = EntityState.Modified;
            var response = DBHelper.SaveChanges(db);

            if (response.Succeeded)
            {
                TempData["mensajeLito"] = "CAMPAÑA CERRADA";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return PartialView(campaña);
        }

        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        public JsonResult EditarCantidad(int? id, int? q)
        {
            var result = false;
            try
            {
                CampañaArticuloTMP campañaArticuloTMP = db.CampañaArticuloTMPs.SingleOrDefault(x => x.CampañaArticuloTMPId == id);

                campañaArticuloTMP.Cantidad = (int)q;
                db.SaveChanges();
                result = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUser(idOperacion: 2)]
        public ActionResult CreateCampArt2(int? tiendaId, int? campId, TiendasArticulosView view)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var response = MovementsHelper.AgregarArticulosTiendas(usuario.Compañia.CompañiaId, usuario.NombreUsuario, (int)tiendaId, (int)campId);

            if (response.Succeeded)
            {
                //TempData["mensajeLito"] = "CAMPAÑA AGREGADA";
            }



            ModelState.AddModelError(string.Empty, response.Message);

            //var view = db.CampañaArticuloTMPs.Where(cat => cat.CampañaTiendaTMPId == campId && cat.TiendaId == id).ToList();
            view.Tiendas = db.Tiendas.Where(c => c.CompañiaId == usuario.CompañiaId).ToList();
            view.ArticuloKFCs = db.ArticuloKFCs.Where(c => c.CompañiaId == usuario.CompañiaId).ToList();
            view.Campañas = db.Campañas.Where(c => c.CampañaId == campId).ToList();
            view.CampañaArticuloTMPs = db.CampañaArticuloTMPs.Where(cat => cat.CampañaTiendaTMPId == campId && cat.TiendaId == tiendaId).ToList();

            ViewBag.Tienda = db.Tiendas.Where(t => t.TiendaId == tiendaId).FirstOrDefault().Restaurante;

            return PartialView(view);
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCampañaArticulos(FormCollection fc)
        {

            var nombre = User.Identity.Name;
            var usuarioActual = db.Usuarios.Where(u => u.NombreUsuario == nombre).FirstOrDefault();

            string[] articuloKFCTMPId = fc.GetValues("articuloId");
            string[] cantidad = fc.GetValues("cantidad");

            //var habilitado = false;
            var cantidadNumero = 0;

            for (var i = 0; i < articuloKFCTMPId.Length; i++)
            {

                CampañaArticuloTMP campañaArticulo = db.CampañaArticuloTMPs.Find(Convert.ToInt32(articuloKFCTMPId[i]));

                //habilitado = false;
                cantidadNumero = 0;

                if (campañaArticulo.Cantidad != Convert.ToInt32(cantidad[i]))
                {
                    cantidadNumero = Convert.ToInt32(cantidad[i]);
                    campañaArticulo.Cantidad = cantidadNumero;

                    db.Entry(campañaArticulo).State = EntityState.Modified;
                    db.SaveChanges();

                }
            }

            TempData["mensajeLito"] = "CANTIDADES ACTUALIZADAS";
            return RedirectToAction("Index", "Campañas");
        }

        // POST: Campañas/Create
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCamp(NuevaCampañaView campaña)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (!string.IsNullOrEmpty(campaña.Descripcion) && !string.IsNullOrEmpty(campaña.Nombre))
            {
                campaña.Detalles = db.CampañaTiendaTMPs.Where(cdt => cdt.Usuario == usuario.NombreUsuario).ToList();

                if (campaña.Detalles.Count != 0)
                {
                    if (ModelState.IsValid)
                    {
                        //var response = MovementsHelper.NuevaCampaña(campaña, usuario.NombreUsuario, usuario.Compañia.CompañiaId);
                        //if (response.Succeeded)
                        //{
                        //    TempData["mensajeLito"] = "CAMPAÑA AGREGADA";

                        return RedirectToAction("Index");
                        //}

                        //ModelState.AddModelError(string.Empty, response.Message);
                    }
                }
                else
                {
                    TempData["mensajeLito"] = "AUN NO SE HAN AGREGADO TIENDAS";
                }
            }
            else
            {
                TempData["mensajeLito"] = "CAPTURAR LOS DATOS DE LA CAMPAÑA";
            }

            return View(campaña);

        }

        // GET: Campañas/Create
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var campañas = new Campaña { CompañiaId = usuario.CompañiaId, };

            campañas.Generada = "NO";

            return PartialView(campañas);


        }

        // POST: Campañas/Create
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Campaña campaña)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            campaña.Generada = "NO";

            if (ModelState.IsValid)
            {

                db.Campañas.Add(campaña);

                var response = DBHelper.SaveChanges(db);

                if (response.Succeeded)
                {
                    MovementsHelper.AgregarTiendas(usuario.Compañia.CompañiaId, usuario.NombreUsuario, campaña.CampañaId);

                    TempData["mensajeLito"] = "CAMPAÑA CREADA";

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);

            }

            return PartialView(campaña);

        }

        // GET: Campañas/Edit/5
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var campaña = db.Campañas.Find(id);

            if (campaña == null)
            {
                return HttpNotFound();
            }

            return PartialView(campaña);
        }

        // POST: Campañas/Edit/5
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Campaña campaña)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(campaña).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    var response2 = MovementsHelper.AgregarTiendas(usuario.Compañia.CompañiaId, usuario.NombreUsuario, campaña.CampañaId);

                    TempData["mensajeLito"] = "CAMPAÑA EDITADA";

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return View(campaña);

        }

        // GET: Campañas/Delete/5
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var campaña = db.Campañas.Find(id);

            if (campaña == null)
            {
                return HttpNotFound();
            }

            return View(campaña);
        }

        // POST: Campañas/Delete/5
        [AuthorizeUser(idOperacion: 4)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var detalleArticulos = db.CampañaArticuloTMPs.Where(da => da.CampañaTiendaTMPId == id).ToList();
            var detalleTiendas = db.CampañaTiendaTMPs.Where(da => da.CampañaId == id).ToList();
            var detalleCodigos = db.CodigosCampaña.Where(da => da.CampañaId == id).ToList();

            if (detalleArticulos != null)
            {
                db.CampañaArticuloTMPs.RemoveRange(detalleArticulos);
                db.SaveChanges();
            }

            if (detalleTiendas != null)
            {
                db.CampañaTiendaTMPs.RemoveRange(detalleTiendas);
                db.SaveChanges();
            }

            if (detalleCodigos != null)
            {
                db.CodigosCampaña.RemoveRange(detalleCodigos);
                db.SaveChanges();
            }

            var campaña = db.Campañas.Find(id);
            db.Campañas.Remove(campaña);
            var response = DBHelper.SaveChanges(db);

            if (response.Succeeded)
            {
                TempData["mensajeLito"] = "CAMPAÑA ELIMINADA";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return PartialView(campaña);
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
