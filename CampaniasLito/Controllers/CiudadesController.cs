using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Filters;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class CiudadesController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Ciudades
        public ActionResult GetList()
        {
            var ciudadesList = db.Ciudads.ToList<Ciudad>();
            return Json(new { data = ciudadesList }, JsonRequestBehavior.AllowGet);

        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index(string ciudad)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(ciudad))
            {
                Session["ciudadFiltro"] = string.Empty;
            }
            else
            {
                Session["ciudadFiltro"] = ciudad;
            }

            var filtro = Session["ciudadFiltro"].ToString();

            var ciudads = db.Ciudads.Include(c => c.Region).Where(c => c.CompañiaId == usuario.CompañiaId).OrderBy(c => c.Nombre);

            if (!string.IsNullOrEmpty(ciudad))
            {
                return View(ciudads.Where(a => a.Nombre.Contains(filtro) || a.Region.Nombre.Contains(filtro)).ToList());
            }
            else
            {
                return View(ciudads.ToList());
            }
        }

        // GET: Ciudades/Details/5
        [AuthorizeUser(idOperacion: 6)]
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
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var ciudades = new Ciudad { CompañiaId = usuario.CompañiaId, };

            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(usuario.CompañiaId, true), "RegionId", "Nombre");

            return PartialView(ciudades);
        }

        // POST: Ciudades/Create
        [AuthorizeUser(idOperacion: 2)]
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
                    TempData["mensajeLito"] = "CIUDAD AGREGADA";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(usuario.CompañiaId, true), "RegionId", "Nombre", ciudad.RegionId);

            return PartialView(ciudad);
        }

        // GET: Ciudades/Edit/5
        [AuthorizeUser(idOperacion: 3)]
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
        [AuthorizeUser(idOperacion: 3)]
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

                    TempData["mensajeLito"] = "CIUDAD EDITADA";

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }


            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(ciudad.CompañiaId, true), "RegionId", "Nombre", ciudad.RegionId);

            return View(ciudad);
        }

        // GET: Ciudades/Delete/5
        [AuthorizeUser(idOperacion: 4)]
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
        [AuthorizeUser(idOperacion: 4)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ciudad = db.Ciudads.Find(id);
            db.Ciudads.Remove(ciudad);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                TempData["mensajeLito"] = "CIUDAD ELIMINADA";

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
