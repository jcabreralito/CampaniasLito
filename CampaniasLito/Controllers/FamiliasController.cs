using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Filters;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class FamiliasController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Familias
        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index(string familia)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(familia))
            {
                Session["familia"] = string.Empty;
            }
            else
            {
                Session["familia"] = familia;
            }

            var filtro = Session["familia"].ToString();

            var familias = db.Familias.OrderBy(a => a.Descripcion).ThenBy(a => a.Codigo);

            if (!string.IsNullOrEmpty(familia))
            {
                return View(familias.Where(a => a.Descripcion.Contains(filtro) || a.Codigo.Contains(filtro)).ToList());
            }
            else
            {
                return View(familias.ToList());
            }

        }

        // GET: Familias/Details/5
        [AuthorizeUser(idOperacion: 6)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familia familia = db.Familias.Find(id);
            if (familia == null)
            {
                return HttpNotFound();
            }
            return View(familia);
        }

        // GET: Familias/Create
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Create()
        {
            return View();
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Familia familia)
        {
            if (ModelState.IsValid)
            {
                db.Familias.Add(familia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(familia);
        }

        // GET: Familias/Edit/5
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var familia = db.Familias.Find(id);
            
            if (familia == null)
            {
                return HttpNotFound();
            }
            
            return View(familia);
        }

        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Familia familia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(familia);
        }

        // GET: Familias/Delete/5
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var familia = db.Familias.Find(id);
            
            if (familia == null)
            {
                return HttpNotFound();
            }
            
            return View(familia);
        }

        // POST: Familias/Delete/5
        [AuthorizeUser(idOperacion: 4)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var familia = db.Familias.Find(id);
            db.Familias.Remove(familia);
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
