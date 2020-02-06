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
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class AcomodoCajasController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: AcomodoCajas
        public ActionResult Index()
        {
            return View(db.AcomodoCajas.ToList());
        }

        // GET: AcomodoCajas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var acomodoCaja = db.AcomodoCajas.Find(id);
            
            if (acomodoCaja == null)
            {
                return HttpNotFound();
            }
            
            return View(acomodoCaja);
        }

        // GET: AcomodoCajas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AcomodoCajas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AcomodoCaja acomodoCaja)
        {
            if (ModelState.IsValid)
            {
                db.AcomodoCajas.Add(acomodoCaja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(acomodoCaja);
        }

        // GET: AcomodoCajas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var acomodoCaja = db.AcomodoCajas.Find(id);
            
            if (acomodoCaja == null)
            {
                return HttpNotFound();
            }
            
            return View(acomodoCaja);
        }

        // POST: AcomodoCajas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AcomodoCaja acomodoCaja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acomodoCaja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(acomodoCaja);
        }

        // GET: AcomodoCajas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var acomodoCaja = db.AcomodoCajas.Find(id);
            
            if (acomodoCaja == null)
            {
                return HttpNotFound();
            }
            
            return View(acomodoCaja);
        }

        // POST: AcomodoCajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var acomodoCaja = db.AcomodoCajas.Find(id);
            db.AcomodoCajas.Remove(acomodoCaja);
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
