using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class TiposCajaController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: TiposCaja
        public ActionResult Index()
        {
            return View(db.TipoCajas.ToList());
        }

        // GET: TiposCaja/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var tipoCaja = db.TipoCajas.Find(id);
            
            if (tipoCaja == null)
            {
                return HttpNotFound();
            }
            
            return View(tipoCaja);
        }

        // GET: TiposCaja/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposCaja/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoCaja tipoCaja)
        {
            if (ModelState.IsValid)
            {
                db.TipoCajas.Add(tipoCaja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoCaja);
        }

        // GET: TiposCaja/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var tipoCaja = db.TipoCajas.Find(id);
            
            if (tipoCaja == null)
            {
                return HttpNotFound();
            }
            
            return View(tipoCaja);
        }

        // POST: TiposCaja/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoCaja tipoCaja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoCaja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoCaja);
        }

        // GET: TiposCaja/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipoCaja = db.TipoCajas.Find(id);
            
            if (tipoCaja == null)
            {
                return HttpNotFound();
            }
            
            return View(tipoCaja);
        }

        // POST: TiposCaja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tipoCaja = db.TipoCajas.Find(id);
            db.TipoCajas.Remove(tipoCaja);
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
