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
    public class TiendaProductoController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: TiendaProducto
        public ActionResult Index()
        {
            return View(db.TiendaProductos.ToList());
        }

        // GET: TiendaProducto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaProducto tiendaProducto = db.TiendaProductos.Find(id);
            if (tiendaProducto == null)
            {
                return HttpNotFound();
            }
            return View(tiendaProducto);
        }

        // GET: TiendaProducto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiendaProducto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TiendaProductoId,TiendaId,TerceraReceta,Arroz,Hamburgesas,Ensalada,PET2Litros,Postres,BisquetMiel")] TiendaProducto tiendaProducto)
        {
            if (ModelState.IsValid)
            {
                db.TiendaProductos.Add(tiendaProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tiendaProducto);
        }

        // GET: TiendaProducto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaProducto tiendaProducto = db.TiendaProductos.Find(id);
            if (tiendaProducto == null)
            {
                return HttpNotFound();
            }
            return View(tiendaProducto);
        }

        // POST: TiendaProducto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TiendaProductoId,TiendaId,TerceraReceta,Arroz,Hamburgesas,Ensalada,PET2Litros,Postres,BisquetMiel")] TiendaProducto tiendaProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiendaProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tiendaProducto);
        }

        // GET: TiendaProducto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaProducto tiendaProducto = db.TiendaProductos.Find(id);
            if (tiendaProducto == null)
            {
                return HttpNotFound();
            }
            return View(tiendaProducto);
        }

        // POST: TiendaProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiendaProducto tiendaProducto = db.TiendaProductos.Find(id);
            db.TiendaProductos.Remove(tiendaProducto);
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
