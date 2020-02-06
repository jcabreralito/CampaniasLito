using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class TiposTiendaController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: TiposTienda
        public ActionResult Index()
        {
            return View(db.TipoTiendas.ToList());
        }

        // GET: TiposTienda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipoTienda = db.TipoTiendas.Find(id);
            
            if (tipoTienda == null)
            {
                return HttpNotFound();
            }
            
            return View(tipoTienda);
        }

        // GET: TiposTienda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposTienda/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoTienda tipoTienda)
        {
            if (ModelState.IsValid)
            {
                db.TipoTiendas.Add(tipoTienda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoTienda);
        }

        // GET: TiposTienda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipoTienda = db.TipoTiendas.Find(id);
            
            if (tipoTienda == null)
            {
                return HttpNotFound();
            }
            
            return View(tipoTienda);
        }

        // POST: TiposTienda/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoTienda tipoTienda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoTienda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoTienda);
        }

        // GET: TiposTienda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipoTienda = db.TipoTiendas.Find(id);
            
            if (tipoTienda == null)
            {
                return HttpNotFound();
            }
            
            return View(tipoTienda);
        }

        // POST: TiposTienda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tipoTienda = db.TipoTiendas.Find(id);
            db.TipoTiendas.Remove(tipoTienda);
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
