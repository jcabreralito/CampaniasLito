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
    public class CampañaTiendaTMPsController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: CampañaTiendaTMPs
        public ActionResult Index()
        {
            var campañaTiendaTMPs = db.CampañaTiendaTMPs.Include(c => c.Tienda);
            return PartialView(campañaTiendaTMPs.ToList());
        }

        // GET: CampañaTiendaTMPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampañaTiendaTMP campañaTiendaTMP = db.CampañaTiendaTMPs.Find(id);
            if (campañaTiendaTMP == null)
            {
                return HttpNotFound();
            }
            return View(campañaTiendaTMP);
        }

        // GET: CampañaTiendaTMPs/Create
        public ActionResult Create()
        {
            ViewBag.TiendaId = new SelectList(db.Tiendas, "TiendaId", "Clasificacion");
            return View();
        }

        // POST: CampañaTiendaTMPs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CampañaTiendaTMPId,Usuario,Compañia,TiendaId,Seleccionada")] CampañaTiendaTMP campañaTiendaTMP)
        {
            if (ModelState.IsValid)
            {
                db.CampañaTiendaTMPs.Add(campañaTiendaTMP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TiendaId = new SelectList(db.Tiendas, "TiendaId", "Clasificacion", campañaTiendaTMP.TiendaId);
            return View(campañaTiendaTMP);
        }

        // GET: CampañaTiendaTMPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampañaTiendaTMP campañaTiendaTMP = db.CampañaTiendaTMPs.Find(id);
            if (campañaTiendaTMP == null)
            {
                return HttpNotFound();
            }
            ViewBag.TiendaId = new SelectList(db.Tiendas, "TiendaId", "Clasificacion", campañaTiendaTMP.TiendaId);
            return View(campañaTiendaTMP);
        }

        // POST: CampañaTiendaTMPs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CampañaTiendaTMPId,Usuario,Compañia,TiendaId,Seleccionada")] CampañaTiendaTMP campañaTiendaTMP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campañaTiendaTMP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TiendaId = new SelectList(db.Tiendas, "TiendaId", "Clasificacion", campañaTiendaTMP.TiendaId);
            return View(campañaTiendaTMP);
        }

        // GET: CampañaTiendaTMPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampañaTiendaTMP campañaTiendaTMP = db.CampañaTiendaTMPs.Find(id);
            if (campañaTiendaTMP == null)
            {
                return HttpNotFound();
            }
            return View(campañaTiendaTMP);
        }

        // POST: CampañaTiendaTMPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CampañaTiendaTMP campañaTiendaTMP = db.CampañaTiendaTMPs.Find(id);
            db.CampañaTiendaTMPs.Remove(campañaTiendaTMP);
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
