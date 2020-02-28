using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Filters;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class TiendasController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Tiendas
        public ActionResult GetList()
        {
            var campañasList = db.Campañas.ToList<Campaña>();
            return Json(new { data = campañasList }, JsonRequestBehavior.AllowGet);

        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index(string tienda)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(tienda))
            {
                Session["tiendaFiltro"] = string.Empty;
            }
            else
            {
                Session["tiendaFiltro"] = tienda;
            }

            var filtro = Session["tiendaFiltro"].ToString();

            var tiendas = db.Tiendas.Where(a => a.CompañiaId == usuario.CompañiaId);
            if (!string.IsNullOrEmpty(tienda))
            {
                return View(tiendas.Where(a => a.Restaurante.Contains(filtro) || a.Ciudad.Nombre.Contains(filtro) || a.CCoFranquicia.Contains(filtro)).ToList());
            }
            else
            {
                return View(tiendas.ToList());
            }
        }

        // GET: Tiendas/Details/5
        [AuthorizeUser(idOperacion: 6)]
        public ActionResult Details(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var tienda = db.Tiendas.Find(id);
            
            if (tienda == null)
            {
                return HttpNotFound();
            }

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(usuario.CompañiaId, true), "CiudadId", "Nombre", tienda.CiudadId);
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(usuario.CompañiaId, true), "RegionId", "Nombre", tienda.RegionId);
            ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);

            return PartialView(tienda);
        }

        // GET: Tiendas/Create
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tienda = new Tienda { CompañiaId = usuario.CompañiaId, };

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(usuario.CompañiaId, true), "CiudadId", "Nombre");
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(usuario.CompañiaId, true), "RegionId", "Nombre");
            ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion");
            ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo");
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion");
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion");

            return View(tienda);
        }

        // POST: Tiendas/Create
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tienda tienda)
        {


            if (ModelState.IsValid)
            {
                db.Tiendas.Add(tienda);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    var response2 = MovementsHelper.AgregarTiendaArticulos(tienda.TiendaId, tienda.CompañiaId);

                    if (response2.Succeeded)
                    {
                        //TempData["msgCampañaCreada"] = "CAMPAÑA AGREGADA";
                    }


                    TempData["msgTiendaCreada"] = "TIENDA AGREGADA";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(tienda.CompañiaId, true), "CiudadId", "Nombre");
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(tienda.CompañiaId, true), "RegionId", "Nombre");
            ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion");
            ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo");
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion");
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion");


            return PartialView(tienda);

        }

        // GET: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult AsignarArticulos(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tiendaArticulos = db.TiendaArticulos.Where(t => t.TiendaId == id).ToList();
            //var tiendaArticulos = db.TiendaArticulos.ToList();
            //var tiendaArticulos2 = db.TiendaArticulos.GroupBy(t => t.ArticuloKFCId).ToList();

            if (tiendaArticulos == null)
            {
                return HttpNotFound();
            }

            //ViewBag.Tiendas = db.Tiendas.ToList();

            //var lista = new List<TiendaArticulo>();

            //foreach (var item in tiendaArticulos2)
            //{
            //    lista.Add(new TiendaArticulo
            //    {
            //        ArticuloKFC = item.FirstOrDefault().ArticuloKFC,
            //        ArticuloKFCId = item.FirstOrDefault().ArticuloKFCId,
            //        Seleccionado = item.FirstOrDefault().Seleccionado,
            //        Tienda = item.FirstOrDefault().Tienda,
            //        TiendaId = item.FirstOrDefault().TiendaId
            //    });
            //}

            ViewBag.Tienda = db.Tiendas.Where(t => t.TiendaId == id).FirstOrDefault().Restaurante;

            return PartialView(tiendaArticulos);
        }

        // GET: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Edit(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(usuario.CompañiaId, true), "CiudadId", "Nombre", tienda.CiudadId);
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(usuario.CompañiaId, true), "RegionId", "Nombre", tienda.RegionId);
            ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);

            return PartialView(tienda);
        }

        // POST: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tienda tienda)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(tienda).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    var response2 = MovementsHelper.AgregarTiendaArticulos(tienda.TiendaId, tienda.CompañiaId);

                    if (response2.Succeeded)
                    {
                        //TempData["msgCampañaCreada"] = "CAMPAÑA AGREGADA";
                    }


                    TempData["msgTiendaEditada"] = "TIENDA EDITADA";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(usuario.CompañiaId, true), "CiudadId", "Nombre", tienda.CiudadId);
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(usuario.CompañiaId, true), "RegionId", "Nombre", tienda.RegionId);
            ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);

            return PartialView(tienda);
        }

        // GET: Tiendas/Delete/5
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult Delete(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tienda = db.Tiendas.Find(id);
            
            if (tienda == null)
            {
                return HttpNotFound();
            }

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(usuario.CompañiaId, true), "CiudadId", "Nombre", tienda.CiudadId);
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(usuario.CompañiaId, true), "RegionId", "Nombre", tienda.RegionId);
            ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);

            return PartialView(tienda);
        }

        // POST: Tiendas/Delete/5
        [AuthorizeUser(idOperacion: 4)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tienda = db.Tiendas.Find(id);
            db.Tiendas.Remove(tienda);
            db.SaveChanges();

            TempData["msgTiendaEliminada"] = "TIENDA ELIMINADA";

            return RedirectToAction("Index");
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
