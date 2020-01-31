using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CampaniasLito.Models;
using CampaniasLito.Classes;

namespace CampaniasLito.Controllers
{
    public class BodegasController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        public ActionResult Index()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            var bodegas = db.Bodegas.Where(b => b.CompañiaId == usuario.CompañiaId);
            return View(bodegas.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bodega = db.Bodegas.Find(id);

            if (bodega == null)
            {
                return HttpNotFound();
            }

            return View(bodega);
        }

        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            var bodega = new Bodega { CompañiaId = usuario.CompañiaId, };

            return View(bodega);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                db.Bodegas.Add(bodega);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bodega);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var  bodega = db.Bodegas.Find(id);

            if (bodega == null)
            {
                return HttpNotFound();
            }

            return View(bodega);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bodega).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bodega);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bodega = db.Bodegas.Find(id);

            if (bodega == null)
            {
                return HttpNotFound();
            }

            return View(bodega);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var bodega = db.Bodegas.Find(id);
            db.Bodegas.Remove(bodega);
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
