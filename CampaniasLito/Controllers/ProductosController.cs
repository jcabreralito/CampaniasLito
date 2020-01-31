using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Models;
using CampaniasLito.Classes;

namespace CampaniasLito.Controllers
{
    [Authorize(Roles = "User, Admin, SuperAdmin")]

    public class ProductosController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        public ActionResult Index()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var productos = db.Productos.Include(p => p.Categoria).Where(c => c.CompañiaId == usuario.CompañiaId);
            return View(productos.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = db.Productos.Find(id);

            if (producto == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }

        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var productos = new Producto { CompañiaId = usuario.CompañiaId, };

            ViewBag.CategoriaId = new SelectList(CombosHelper.GetCategorias(usuario.CompañiaId, true), "CategoriaId", "Descripcion");

            return PartialView(productos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Productos.Add(producto);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    if (producto.ImagenFile != null)
                    {
                        var folder = "~/Content/Productos";
                        var file = string.Format("{0}.jpg", producto.ProductoId);
                        var responseLogo = FilesHelper.UploadPhoto(producto.ImagenFile, folder, file);
                        if (responseLogo)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            producto.Imagen = pic;
                            db.Entry(producto).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.CategoriaId = new SelectList(CombosHelper.GetCategorias(usuario.CompañiaId, true), "CategoriaId", "Descripcion", producto.CategoriaId);

            return PartialView(producto);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = db.Productos.Find(id);

            if (producto == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoriaId = new SelectList(CombosHelper.GetCategorias(producto.CompañiaId, true), "CategoriaId", "Descripcion", producto.CategoriaId);

            return PartialView(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    if (producto.ImagenFile != null)
                    {
                        var folder = "~/Content/Productos";
                        var file = string.Format("{0}.jpg", producto.ProductoId);
                        var responseLogo = FilesHelper.UploadPhoto(producto.ImagenFile, folder, file);
                        if (responseLogo)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            producto.Imagen = pic;
                            db.Entry(producto).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.CategoriaId = new SelectList(CombosHelper.GetCategorias(producto.CompañiaId, true), "CategoriaId", "Descripcion", producto.CategoriaId);
            return PartialView(producto);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = db.Productos.Find(id);

            if (producto == null)
            {
                return HttpNotFound();
            }

            return PartialView(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return PartialView(producto);
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
