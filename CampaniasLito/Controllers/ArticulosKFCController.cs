using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Filters;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class ArticulosKFCController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        public ActionResult GetList()
        {
            var artList = db.ArticuloKFCs.ToList<ArticuloKFC>();
            return Json(new { data = artList }, JsonRequestBehavior.AllowGet);

        }

        // GET: ArticulosKFC
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult Index(string articulo)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(articulo))
            {
                Session["articuloFiltro"] = string.Empty;
            }
            else
            {
                Session["articuloFiltro"] = articulo;
            }

            var filtro = Session["articuloFiltro"].ToString();

            var articuloKFCs = db.ArticuloKFCs.Where(a => a.CompañiaId == usuario.CompañiaId).OrderBy(a => a.Familia).ThenBy(a => a.Descripcion);

            if (!string.IsNullOrEmpty(articulo))
            {
                return View(articuloKFCs.Where(a => a.Descripcion.Contains(filtro) || a.Familia.Contains(filtro) || a.Proveedor.Nombre.Contains(filtro)).ToList());
            }
            else
            {
                return View(articuloKFCs.ToList());
            }

        }

        // GET: ArticulosKFC/Details/5
        [AuthorizeUser(idOperacion: 9)]
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
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var articulos = new ArticuloKFC { CompañiaId = usuario.CompañiaId, };

            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre");

            return PartialView(articulos);
        }

        // POST: ArticulosKFC/Create
        [AuthorizeUser(idOperacion: 1)]
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

                    TempData["msgArticuloCreado"] = "ARTICULO AGREGADO";

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre", articuloKFC.ProveedorId);

            return PartialView(articuloKFC);
        }

        // GET: ArticulosKFC/Edit/5
        [AuthorizeUser(idOperacion: 2)]
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

            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre", articuloKFC.ProveedorId);

            return PartialView(articuloKFC);
        }

        // POST: ArticulosKFC/Edit/5
        [AuthorizeUser(idOperacion: 2)]
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

                    TempData["msgArticuloEditado"] = "ARTICULO EDITADO";
                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre", articuloKFC.ProveedorId);

            return PartialView(articuloKFC);
        }

        // GET: ArticulosKFC/Delete/5
        [AuthorizeUser(idOperacion: 3)]
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

            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre", articuloKFC.ProveedorId);

            return View(articuloKFC);
        }

        // POST: ArticulosKFC/Delete/5
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var articulo = db.ArticuloKFCs.Find(id);
            db.ArticuloKFCs.Remove(articulo);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                TempData["msgArticuloEliminado"] = "ARTICULO ELIMINADO";

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
