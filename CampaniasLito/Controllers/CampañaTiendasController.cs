using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class CampañaTiendasController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: CampañaTiendas
        public ActionResult Index()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var campañaTienda = db.CampañaTiendas.Include(c => c.Campaña).Include(c => c.Tienda).Where(c => c.CompañiaId == usuario.CompañiaId);
            return View(campañaTienda.ToList());

        }

        // GET: CampañaTiendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var campañaTienda = db.CampañaTiendas.Find(id);

            if (campañaTienda == null)
            {
                return HttpNotFound();
            }

            return PartialView(campañaTienda);
        }

        // GET: CampañaTiendas/Create
        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var campañaTiendas = new CampañaTienda { CompañiaId = usuario.CompañiaId, };

            ViewBag.CampañaId = new SelectList(db.Campañas, "CampañaId", "Nombre");
            ViewBag.CompañiaId = new SelectList(db.Compañias, "CompañiaId", "Nombre");
            ViewBag.TiendaId = new SelectList(db.Tiendas, "TiendaId", "Clasificacion");

            return PartialView(campañaTiendas);




            //ViewBag.CampañaId = new SelectList(db.Campaña, "CampañaId", "Nombre");
            //ViewBag.CompañiaId = new SelectList(db.Compañia, "CompañiaId", "Nombre");
            //ViewBag.TiendaId = new SelectList(db.Tiendas, "TiendaId", "Clasificacion");
            //return View();
        }

        // POST: CampañaTiendas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CampañaTienda campañaTienda)
        {
            if (ModelState.IsValid)
            {
                db.CampañaTiendas.Add(campañaTienda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampañaId = new SelectList(db.Campañas, "CampañaId", "Nombre", campañaTienda.CampañaId);
            ViewBag.CompañiaId = new SelectList(db.Compañias, "CompañiaId", "Nombre", campañaTienda.CompañiaId);
            ViewBag.TiendaId = new SelectList(db.Tiendas, "TiendaId", "Clasificacion", campañaTienda.TiendaId);
            return View(campañaTienda);
        }

        // GET: CampañaTiendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var campañaTienda = db.CampañaTiendas.Find(id);

            if (campañaTienda == null)
            {
                return HttpNotFound();
            }

            ViewBag.CampañaId = new SelectList(db.Campañas, "CampañaId", "Nombre", campañaTienda.CampañaId);
            ViewBag.CompañiaId = new SelectList(db.Compañias, "CompañiaId", "Nombre", campañaTienda.CompañiaId);
            ViewBag.TiendaId = new SelectList(db.Tiendas, "TiendaId", "Clasificacion", campañaTienda.TiendaId);

            return View(campañaTienda);
        }

        // POST: CampañaTiendas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CampañaTienda campañaTienda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campañaTienda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampañaId = new SelectList(db.Campañas, "CampañaId", "Nombre", campañaTienda.CampañaId);
            ViewBag.CompañiaId = new SelectList(db.Compañias, "CompañiaId", "Nombre", campañaTienda.CompañiaId);
            ViewBag.TiendaId = new SelectList(db.Tiendas, "TiendaId", "Clasificacion", campañaTienda.TiendaId);

            return View(campañaTienda);
        }

        // GET: CampañaTiendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var campañaTienda = db.CampañaTiendas.Find(id);
            
            if (campañaTienda == null)
            {
                return HttpNotFound();
            }
            
            return View(campañaTienda);
        }

        // POST: CampañaTiendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var campañaTienda = db.CampañaTiendas.Find(id);
            db.CampañaTiendas.Remove(campañaTienda);
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
