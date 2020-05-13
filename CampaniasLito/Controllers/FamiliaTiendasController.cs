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
    public class FamiliaTiendasController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: FamiliaTiendas
        public ActionResult Index()
        {
            var familiaTiendas = db.FamiliaTiendas.Include(f => f.Familia).Include(f => f.Tienda);
            return View(familiaTiendas.ToList());
        }

        // GET: FamiliaTiendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamiliaTienda familiaTienda = db.FamiliaTiendas.Find(id);
            if (familiaTienda == null)
            {
                return HttpNotFound();
            }
            return View(familiaTienda);
        }

        // GET: FamiliaTiendas/Create
        public ActionResult Create()
        {
            ViewBag.FamiliaId = new SelectList(db.Familias, "FamiliaId", "Descripcion");
            ViewBag.TiendaId = new SelectList(db.Tiendas, "TiendaId", "Clasificacion");
            return View();
        }

        // POST: FamiliaTiendas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FamiliaTiendaId,FamiliaId,TiendaId")] FamiliaTienda familiaTienda)
        {
            if (ModelState.IsValid)
            {
                db.FamiliaTiendas.Add(familiaTienda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FamiliaId = new SelectList(db.Familias, "FamiliaId", "Descripcion", familiaTienda.FamiliaId);
            ViewBag.TiendaId = new SelectList(db.Tiendas, "TiendaId", "Clasificacion", familiaTienda.TiendaId);
            return View(familiaTienda);
        }

        // GET: FamiliaTiendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamiliaTienda familiaTienda = db.FamiliaTiendas.Find(id);
            if (familiaTienda == null)
            {
                return HttpNotFound();
            }
            ViewBag.FamiliaId = new SelectList(db.Familias, "FamiliaId", "Descripcion", familiaTienda.FamiliaId);
            ViewBag.TiendaId = new SelectList(db.Tiendas, "TiendaId", "Clasificacion", familiaTienda.TiendaId);
            return View(familiaTienda);
        }

        // POST: FamiliaTiendas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FamiliaTiendaId,FamiliaId,TiendaId")] FamiliaTienda familiaTienda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familiaTienda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FamiliaId = new SelectList(db.Familias, "FamiliaId", "Descripcion", familiaTienda.FamiliaId);
            ViewBag.TiendaId = new SelectList(db.Tiendas, "TiendaId", "Clasificacion", familiaTienda.TiendaId);
            return View(familiaTienda);
        }

        // GET: FamiliaTiendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FamiliaTienda familiaTienda = db.FamiliaTiendas.Find(id);
            if (familiaTienda == null)
            {
                return HttpNotFound();
            }
            return View(familiaTienda);
        }

        // POST: FamiliaTiendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FamiliaTienda familiaTienda = db.FamiliaTiendas.Find(id);
            db.FamiliaTiendas.Remove(familiaTienda);
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
