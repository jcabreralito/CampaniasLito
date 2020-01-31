using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class RegionesController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Regiones
        public ActionResult Index()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var regions = db.Regions.Where(c => c.CompañiaId == usuario.CompañiaId);
            return View(regions.ToList());
        }

        // GET: Regiones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = db.Regions.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // GET: Regiones/Create
        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var regiones = new Region { CompañiaId = usuario.CompañiaId, };

            return PartialView(regiones);
        }

        // POST: Regiones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Region region)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Regions.Add(region);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return PartialView(region);
        }

        // GET: Regiones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var region = db.Regions.Find(id);
            
            if (region == null)
            {
                return HttpNotFound();
            }

            return PartialView(region);
        }

        // POST: Regiones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Region region)
        {
            if (ModelState.IsValid)
            {
                db.Entry(region).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return View(region);
        }

        // GET: Regiones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var region = db.Regions.Find(id);
            
            if (region == null)
            {
                return HttpNotFound();
            }
            
            return PartialView(region);
        }

        // POST: Regiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var region = db.Regions.Find(id);
            db.Regions.Remove(region);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return PartialView(region);
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
