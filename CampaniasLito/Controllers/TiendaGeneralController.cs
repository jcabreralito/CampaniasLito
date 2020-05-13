using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class TiendaGeneralController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: TiendaGeneral
        public ActionResult Index()
        {
            return View(db.TiendaGenerales.ToList());
        }

        // GET: TiendaGeneral/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaGeneral tiendaGeneral = db.TiendaGenerales.Find(id);
            if (tiendaGeneral == null)
            {
                return HttpNotFound();
            }
            return View(tiendaGeneral);
        }

        // GET: TiendaGeneral/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiendaGeneral/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TiendaGeneralId,TiendaId,TipoId,NuevoNivelDePrecioId,MenuDigital,CantidadDePantallas")] TiendaGeneral tiendaGeneral)
        {
            if (ModelState.IsValid)
            {
                db.TiendaGenerales.Add(tiendaGeneral);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tiendaGeneral);
        }

        // GET: TiendaGeneral/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaGeneral tiendaGeneral = db.TiendaGenerales.Find(id);
            if (tiendaGeneral == null)
            {
                return HttpNotFound();
            }
            return View(tiendaGeneral);
        }

        // POST: TiendaGeneral/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TiendaGeneralId,TiendaId,TipoId,NuevoNivelDePrecioId,MenuDigital,CantidadDePantallas")] TiendaGeneral tiendaGeneral)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiendaGeneral).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tiendaGeneral);
        }

        // GET: TiendaGeneral/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaGeneral tiendaGeneral = db.TiendaGenerales.Find(id);
            if (tiendaGeneral == null)
            {
                return HttpNotFound();
            }
            return View(tiendaGeneral);
        }

        // POST: TiendaGeneral/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiendaGeneral tiendaGeneral = db.TiendaGenerales.Find(id);
            db.TiendaGenerales.Remove(tiendaGeneral);
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
