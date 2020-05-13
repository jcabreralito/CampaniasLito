using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class RolesController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        public ActionResult Index()
        {
            Session["homeB"] = string.Empty;
            Session["rolesB"] = "active";
            Session["compañiasB"] = string.Empty;
            Session["usuariosB"] = string.Empty;
            Session["regionesB"] = string.Empty;
            Session["ciudadesB"] = string.Empty;
            Session["restaurantesB"] = string.Empty;
            Session["familiasB"] = string.Empty;
            Session["materialesB"] = string.Empty;
            Session["campañasB"] = string.Empty;

            return View(db.Roles.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var rol = db.Roles.Find(id);

            if (rol == null)
            {
                return HttpNotFound();
            }

            return PartialView(rol);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Rol rol)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(rol);
                db.SaveChanges();

                UsuariosHelper.CrearRoles(rol.Nombre);

                Session["Compañia"] = "Litoprocess";
                TempData["mensajeLito"] = "ROL AGREGADO";

                return RedirectToAction("Index");
            }

            return PartialView(rol);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var rol = db.Roles.Find(id);

            if (rol == null)
            {
                return HttpNotFound();
            }

            return PartialView(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rol rol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rol).State = EntityState.Modified;
                db.SaveChanges();

                Session["Compañia"] = "Litoprocess";
                TempData["mensajeLito"] = "ROL EDITADO";

                return RedirectToAction("Index");
            }

            return PartialView(rol);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var rol = db.Roles.Find(id);

            if (rol == null)
            {
                return HttpNotFound();
            }

            return PartialView(rol);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var rol = db.Roles.Find(id);
            db.Roles.Remove(rol);
            db.SaveChanges();

            Session["Compañia"] = "Litoprocess";
            TempData["mensajeLito"] = "ROL ELIMINADO";

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
