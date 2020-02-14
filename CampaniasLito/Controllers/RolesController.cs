using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class RolesController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        public ActionResult Index()
        {
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

                Session["Compañia"] = "Litoprocess";
                TempData["msgRolCreado"] = "ROL AGREGADO";

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
                TempData["msgRolEditado"] = "ROL EDITADO";

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
            TempData["msgRolEliminado"] = "ROL ELIMINADO";

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
