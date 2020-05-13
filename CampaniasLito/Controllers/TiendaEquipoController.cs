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
    public class TiendaEquipoController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: TiendaEquipo
        public ActionResult Index()
        {
            return View(db.TiendaEquipos.ToList());
        }

        // GET: TiendaEquipo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaEquipo tiendaEquipo = db.TiendaEquipos.Find(id);
            if (tiendaEquipo == null)
            {
                return HttpNotFound();
            }
            return View(tiendaEquipo);
        }

        // GET: TiendaEquipo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiendaEquipo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TiendaEquipoId,TiendaId,TipoDeCajaId,AcomodoDeCajas,NoMesaDeAreaComedor,NoMesaDeAreaDeJuegos,NumeroDeVentanas")] TiendaEquipo tiendaEquipo)
        {
            if (ModelState.IsValid)
            {
                db.TiendaEquipos.Add(tiendaEquipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tiendaEquipo);
        }

        // GET: TiendaEquipo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaEquipo tiendaEquipo = db.TiendaEquipos.Find(id);
            if (tiendaEquipo == null)
            {
                return HttpNotFound();
            }
            return View(tiendaEquipo);
        }

        // POST: TiendaEquipo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TiendaEquipoId,TiendaId,TipoDeCajaId,AcomodoDeCajas,NoMesaDeAreaComedor,NoMesaDeAreaDeJuegos,NumeroDeVentanas")] TiendaEquipo tiendaEquipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiendaEquipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tiendaEquipo);
        }

        // GET: TiendaEquipo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaEquipo tiendaEquipo = db.TiendaEquipos.Find(id);
            if (tiendaEquipo == null)
            {
                return HttpNotFound();
            }
            return View(tiendaEquipo);
        }

        // POST: TiendaEquipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiendaEquipo tiendaEquipo = db.TiendaEquipos.Find(id);
            db.TiendaEquipos.Remove(tiendaEquipo);
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
