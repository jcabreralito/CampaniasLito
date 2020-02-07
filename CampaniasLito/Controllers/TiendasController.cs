using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class TiendasController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Tiendas
        public ActionResult GetList()
        {
            var campañasList = db.Campañas.ToList<Campaña>();
            return Json(new { data = campañasList }, JsonRequestBehavior.AllowGet);

        }

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
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "AcomodoCajaId", "Descripcion", tienda.AcomodoDeCajas);
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);

            return PartialView(tienda);
        }

        // GET: Tiendas/Create
        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tienda = new Tienda { CompañiaId = usuario.CompañiaId, };

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(usuario.CompañiaId), "CiudadId", "Nombre");
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(usuario.CompañiaId), "RegionId", "Nombre");
            ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(), "NivelPrecioId", "Descripcion");
            ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(), "TipoTiendaId", "Tipo");
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(), "AcomodoCajaId", "Descripcion");
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(), "TipoCajaId", "Descripcion");

            return View(tienda);
        }

        // POST: Tiendas/Create
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
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(tienda.CompañiaId), "CiudadId", "Nombre");
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(tienda.CompañiaId), "RegionId", "Nombre");
            ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(), "NivelPrecioId", "Descripcion");
            ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(), "TipoTiendaId", "Tipo");
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(), "AcomodoCajaId", "Descripcion");
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(), "TipoCajaId", "Descripcion");


            return PartialView(tienda);

        }

        // GET: Tiendas/Edit/5
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
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "AcomodoCajaId", "Descripcion", tienda.AcomodoDeCajas);
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);

            return PartialView(tienda);
        }

        // POST: Tiendas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tienda tienda)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(tienda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(usuario.CompañiaId, true), "CiudadId", "Nombre", tienda.CiudadId);
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(usuario.CompañiaId, true), "RegionId", "Nombre", tienda.RegionId);
            ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "AcomodoCajaId", "Descripcion", tienda.AcomodoDeCajas);
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);

            return PartialView(tienda);
        }

        // GET: Tiendas/Delete/5
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
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "AcomodoCajaId", "Descripcion", tienda.AcomodoDeCajas);
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);

            return PartialView(tienda);
        }

        // POST: Tiendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tienda = db.Tiendas.Find(id);
            db.Tiendas.Remove(tienda);
            db.SaveChanges();
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
