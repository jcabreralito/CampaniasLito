using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class ArticulosKFCController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: ArticulosKFC
        public ActionResult Index()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var articuloKFCs = db.ArticuloKFCs.Where(a => a.CompañiaId == usuario.CompañiaId);
            return View(articuloKFCs.ToList());
        }

        // GET: ArticulosKFC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticuloKFC articuloKFC = db.ArticuloKFCs.Find(id);
            if (articuloKFC == null)
            {
                return HttpNotFound();
            }
            return PartialView(articuloKFC);
        }

        // GET: ArticulosKFC/Create
        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var articulos = new ArticuloKFC { CompañiaId = usuario.CompañiaId, };

            return PartialView(articulos);
        }

        // POST: ArticulosKFC/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticuloKFC articuloKFC)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.ArticuloKFCs.Add(articuloKFC);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return PartialView(articuloKFC);
        }

        // GET: ArticulosKFC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var articuloKFC = db.ArticuloKFCs.Find(id);
            
            if (articuloKFC == null)
            {
                return HttpNotFound();
            }

            return PartialView(articuloKFC);
        }

        // POST: ArticulosKFC/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticuloKFC articuloKFC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articuloKFC).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return PartialView(articuloKFC);
        }

        // GET: ArticulosKFC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var articuloKFC = db.ArticuloKFCs.Find(id);
            
            if (articuloKFC == null)
            {
                return HttpNotFound();
            }
            
            return View(articuloKFC);
        }

        // POST: ArticulosKFC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var articulo = db.ArticuloKFCs.Find(id);
            db.ArticuloKFCs.Remove(articulo);
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
