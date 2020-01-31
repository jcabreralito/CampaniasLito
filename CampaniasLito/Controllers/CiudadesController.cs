using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class CiudadesController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Ciudades
        public ActionResult Index()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var ciudads = db.Ciudads.Include(c => c.Region).Where(c => c.CompañiaId == usuario.CompañiaId);
            return View(ciudads.ToList());
        }

        // GET: Ciudades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ciudad = db.Ciudads.Find(id);
            
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            return PartialView(ciudad);
        }

        // GET: Ciudades/Create
        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var ciudades = new Ciudad { CompañiaId = usuario.CompañiaId, };

            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(usuario.CompañiaId), "RegionId", "Nombre");

            return PartialView(ciudades);
        }

        // POST: Ciudades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ciudad ciudad)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Ciudads.Add(ciudad);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(usuario.CompañiaId, true), "RegionId", "Nombre", ciudad.RegionId);

            return PartialView(ciudad);
        }

        // GET: Ciudades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ciudad = db.Ciudads.Find(id);
            
            if (ciudad == null)
            {
                return HttpNotFound();
            }

            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(ciudad.CompañiaId, true), "RegionId", "Nombre", ciudad.RegionId);

            return PartialView(ciudad);
        }

        // POST: Ciudades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ciudad ciudad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ciudad).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }


            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(ciudad.CompañiaId, true), "RegionId", "Nombre", ciudad.RegionId);

            return View(ciudad);
        }

        // GET: Ciudades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ciudad = db.Ciudads.Find(id);
            
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            
            return PartialView(ciudad);
        }

        // POST: Ciudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ciudad = db.Ciudads.Find(id);
            db.Ciudads.Remove(ciudad);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return PartialView(ciudad);
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
