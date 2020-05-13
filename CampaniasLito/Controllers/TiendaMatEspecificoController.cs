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
    public class TiendaMatEspecificoController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: TiendaMatEspecifico
        public ActionResult Index()
        {
            return View(db.TiendaMatEspecificos.ToList());
        }

        // GET: TiendaMatEspecifico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaMatEspecifico tiendaMatEspecifico = db.TiendaMatEspecificos.Find(id);
            if (tiendaMatEspecifico == null)
            {
                return HttpNotFound();
            }
            return View(tiendaMatEspecifico);
        }

        // GET: TiendaMatEspecifico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiendaMatEspecifico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TiendaMatEspecificoId,TiendaId,MenuBackLigth,Autoexpress,CopeteAERemodelado,CopeteAETradicional,PanelDeInnovacion,DisplayDeBurbuja,Delivery,MERCADO_DE_PRUEBA,AreaDeJuegos")] TiendaMatEspecifico tiendaMatEspecifico)
        {
            if (ModelState.IsValid)
            {
                db.TiendaMatEspecificos.Add(tiendaMatEspecifico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tiendaMatEspecifico);
        }

        // GET: TiendaMatEspecifico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaMatEspecifico tiendaMatEspecifico = db.TiendaMatEspecificos.Find(id);
            if (tiendaMatEspecifico == null)
            {
                return HttpNotFound();
            }
            return View(tiendaMatEspecifico);
        }

        // POST: TiendaMatEspecifico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TiendaMatEspecificoId,TiendaId,MenuBackLigth,Autoexpress,CopeteAERemodelado,CopeteAETradicional,PanelDeInnovacion,DisplayDeBurbuja,Delivery,MERCADO_DE_PRUEBA,AreaDeJuegos")] TiendaMatEspecifico tiendaMatEspecifico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiendaMatEspecifico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tiendaMatEspecifico);
        }

        // GET: TiendaMatEspecifico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaMatEspecifico tiendaMatEspecifico = db.TiendaMatEspecificos.Find(id);
            if (tiendaMatEspecifico == null)
            {
                return HttpNotFound();
            }
            return View(tiendaMatEspecifico);
        }

        // POST: TiendaMatEspecifico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiendaMatEspecifico tiendaMatEspecifico = db.TiendaMatEspecificos.Find(id);
            db.TiendaMatEspecificos.Remove(tiendaMatEspecifico);
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
