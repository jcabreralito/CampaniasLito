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
    public class TiendaMedidaEspecialController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: TiendaMedidaEspecial
        public ActionResult Index()
        {
            return View(db.TiendaMedidaEspeciales.ToList());
        }

        // GET: TiendaMedidaEspecial/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaMedidaEspecial tiendaMedidaEspecial = db.TiendaMedidaEspeciales.Find(id);
            if (tiendaMedidaEspecial == null)
            {
                return HttpNotFound();
            }
            return View(tiendaMedidaEspecial);
        }

        // GET: TiendaMedidaEspecial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiendaMedidaEspecial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TiendaMedidaEspecialId,TiendaId,WCMedidaEspecial60_8x85cm,WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm,WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm,WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm,WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm,MedidaEspecialPanelDeComplementos,MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm,MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm,MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm,MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm,MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm,MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm,MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm,MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm,MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm")] TiendaMedidaEspecial tiendaMedidaEspecial)
        {
            if (ModelState.IsValid)
            {
                db.TiendaMedidaEspeciales.Add(tiendaMedidaEspecial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tiendaMedidaEspecial);
        }

        // GET: TiendaMedidaEspecial/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaMedidaEspecial tiendaMedidaEspecial = db.TiendaMedidaEspeciales.Find(id);
            if (tiendaMedidaEspecial == null)
            {
                return HttpNotFound();
            }
            return View(tiendaMedidaEspecial);
        }

        // POST: TiendaMedidaEspecial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TiendaMedidaEspecialId,TiendaId,WCMedidaEspecial60_8x85cm,WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm,WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm,WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm,WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm,MedidaEspecialPanelDeComplementos,MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm,MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm,MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm,MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm,MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm,MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm,MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm,MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm,MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm")] TiendaMedidaEspecial tiendaMedidaEspecial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiendaMedidaEspecial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tiendaMedidaEspecial);
        }

        // GET: TiendaMedidaEspecial/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiendaMedidaEspecial tiendaMedidaEspecial = db.TiendaMedidaEspeciales.Find(id);
            if (tiendaMedidaEspecial == null)
            {
                return HttpNotFound();
            }
            return View(tiendaMedidaEspecial);
        }

        // POST: TiendaMedidaEspecial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiendaMedidaEspecial tiendaMedidaEspecial = db.TiendaMedidaEspeciales.Find(id);
            db.TiendaMedidaEspeciales.Remove(tiendaMedidaEspecial);
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
