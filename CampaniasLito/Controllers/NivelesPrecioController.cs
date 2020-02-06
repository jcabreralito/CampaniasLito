using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class NivelesPrecioController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: NivelesPrecio
        public ActionResult Index()
        {
            return View(db.NivelPrecios.ToList());
        }

        // GET: NivelesPrecio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var nivelPrecio = db.NivelPrecios.Find(id);
            
            if (nivelPrecio == null)
            {
                return HttpNotFound();
            }
            
            return View(nivelPrecio);
        }

        // GET: NivelesPrecio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NivelesPrecio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NivelPrecio nivelPrecio)
        {
            if (ModelState.IsValid)
            {
                db.NivelPrecios.Add(nivelPrecio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nivelPrecio);
        }

        // GET: NivelesPrecio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var nivelPrecio = db.NivelPrecios.Find(id);
            
            if (nivelPrecio == null)
            {
                return HttpNotFound();
            }
            
            return View(nivelPrecio);
        }

        // POST: NivelesPrecio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NivelPrecio nivelPrecio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nivelPrecio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nivelPrecio);
        }

        // GET: NivelesPrecio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var nivelPrecio = db.NivelPrecios.Find(id);
            
            if (nivelPrecio == null)
            {
                return HttpNotFound();
            }
            
            return View(nivelPrecio);
        }

        // POST: NivelesPrecio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var nivelPrecio = db.NivelPrecios.Find(id);
            
            db.NivelPrecios.Remove(nivelPrecio);
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
