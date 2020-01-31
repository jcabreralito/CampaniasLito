using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class ArticulosController : Controller
    {
        private readonly CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Articulos
        public ActionResult Index()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var articuloes = db.Articuloes.Include(a => a.Compañia);
            return View(articuloes.ToList());
        }

        // GET: Articulos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var articulo = db.Articuloes.Find(id);
            
            if (articulo == null)
            {
                return HttpNotFound();
            }

            return PartialView(articulo);
        }

        // GET: Articulos/Create
        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var articulos = new Articulo { CompañiaId = usuario.CompañiaId, };

            return PartialView(articulos);

        }

        // POST: Articulos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Articulo articulo)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Articuloes.Add(articulo);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return PartialView(articulo);
        }

        // GET: Articulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var articulo = db.Articuloes.Find(id);
            
            if (articulo == null)
            {
                return HttpNotFound();
            }

            return View(articulo);
        }

        // POST: Articulos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulo).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return PartialView(articulo);
        }

        // GET: Articulos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var articulo = db.Articuloes.Find(id);
            
            if (articulo == null)
            {
                return HttpNotFound();
            }
            
            return PartialView(articulo);
        }

        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var articulo = db.Articuloes.Find(id);
            db.Articuloes.Remove(articulo);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);

            return PartialView(articulo);
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
