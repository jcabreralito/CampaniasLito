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
        public ActionResult Index()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tiendas = db.Tiendas.Where(a => a.CompañiaId == usuario.CompañiaId);
            return View(tiendas.ToList());
        }

        // GET: Tiendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var tienda = db.Tiendas.Find(id);
            
            if (tienda == null)
            {
                return HttpNotFound();
            }
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


            List<SelectListItem> lst = new List<SelectListItem>();

            lst.Add(new SelectListItem() { Text = "[Seleccionar...]", Value = "0" });
            lst.Add(new SelectListItem() { Text = "Alto", Value = "Alto" });
            lst.Add(new SelectListItem() { Text = "Bajo", Value = "Bajo" });

            ViewBag.Opciones = lst;


            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(usuario.CompañiaId), "CiudadId", "Nombre");
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(usuario.CompañiaId), "RegionId", "Nombre");

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
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tienda);
        }

        // GET: Tiendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tienda = db.Tiendas.Find(id);
            
            if (tienda == null)
            {
                return HttpNotFound();
            }
            return PartialView(tienda);
        }

        // POST: Tiendas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tienda tienda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tienda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tienda);
        }

        // GET: Tiendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tienda = db.Tiendas.Find(id);
            
            if (tienda == null)
            {
                return HttpNotFound();
            }
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
