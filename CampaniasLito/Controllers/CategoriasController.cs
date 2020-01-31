using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CampaniasLito.Models;
using CampaniasLito.Classes;

namespace CampaniasLito.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class CategoriasController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        public ActionResult Index()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var categorias = db.Categorias.Where(c => c.CompañiaId == usuario.CompañiaId);
            return View(categorias.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var categoria = db.Categorias.Find(id);

            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var categoria = new Categoria { CompañiaId = usuario.CompañiaId, };

            return PartialView(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Categorias.Add(categoria);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return PartialView(categoria);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var categoria = db.Categorias.Find(id);

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return PartialView(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return PartialView(categoria);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var categoria = db.Categorias.Find(id);

            if (categoria == null)
            {
                return HttpNotFound();
            }

            return PartialView(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var categoria = db.Categorias.Find(id);
            db.Categorias.Remove(categoria);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return PartialView(categoria);
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
