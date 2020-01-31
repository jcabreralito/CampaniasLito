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
    [Authorize(Roles = "SuperAdmin")]

    public class UsuariosController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Compañia);
            return View(usuarios.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = db.Usuarios.Find(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        public ActionResult Create()
        {
            ViewBag.CompañiaId = new SelectList(CombosHelper.GetCompañias(true), "CompañiaId", "Nombre");
            ViewBag.RolId = new SelectList(CombosHelper.GetRoles(true), "RolId", "Nombre");

            var usuario = new Usuario { };
            return PartialView(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    var rol = db.Roles.Where(r => r.RolId == usuario.RolId).FirstOrDefault();

                    UsuariosHelper.CreateUserASP(usuario.NombreUsuario, rol.Nombre);

                    if (usuario.FotoFile != null)
                    {
                        var folder = "~/Content/Fotos";
                        var file = string.Format("{0}.jpg", usuario.UsuarioId);
                        var responseFoto = FilesHelper.UploadPhoto(usuario.FotoFile, folder, file);
                        if (responseFoto)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            usuario.Foto = pic;
                            db.Entry(usuario).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.CompañiaId = new SelectList(CombosHelper.GetCompañias(true), "CompañiaId", "Nombre", usuario.CompañiaId);
            ViewBag.RolId = new SelectList(CombosHelper.GetRoles(true), "RolId", "Nombre", usuario.RolId);

            return PartialView(usuario);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = db.Usuarios.Find(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            var currentUsuario = User.Identity.Name;

            if (currentUsuario.ToString() == usuario.NombreUsuario)
            {
                return RedirectToAction("Index");
            }

            ViewBag.CompañiaId = new SelectList(CombosHelper.GetCompañias(), "CompañiaId", "Nombre", usuario.CompañiaId);
            ViewBag.RolId = new SelectList(CombosHelper.GetRoles(), "RolId", "Nombre", usuario.RolId);

            return PartialView(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    if (usuario.FotoFile != null)
                    {
                        var folder = "~/Content/Fotos";
                        var file = string.Format("{0}.jpg", usuario.UsuarioId);
                        var responseFoto = FilesHelper.UploadPhoto(usuario.FotoFile, folder, file);
                        if (responseFoto)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            usuario.Foto = pic;
                            db.Entry(usuario).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }


                    var db2 = new CampaniasLitoContext();
                    var currentUser = db2.Usuarios.Find(usuario.UsuarioId);
                    var currentRol = db2.Roles.Where(r => r.RolId == currentUser.RolId).FirstOrDefault();
                    var newRol = db.Roles.Where(r => r.RolId == usuario.RolId).FirstOrDefault();

                    if (currentUser.NombreUsuario != usuario.NombreUsuario || currentRol.RolId != newRol.RolId)
                    {
                        UsuariosHelper.UpdateUserName(currentUser.NombreUsuario, usuario.NombreUsuario, currentRol.Nombre, newRol.Nombre);
                    }

                    db2.Dispose();


                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.CompañiaId = new SelectList(CombosHelper.GetCompañias(), "CompañiaId", "Nombre", usuario.CompañiaId);
            ViewBag.RolId = new SelectList(CombosHelper.GetRoles(), "RolId", "Nombre", usuario.RolId);

            return PartialView(usuario);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = db.Usuarios.Find(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }


            var currentUsuario = User.Identity.Name;

            if (currentUsuario.ToString() == usuario.NombreUsuario)
            {
                return RedirectToAction("Index");
            }

            return PartialView(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return PartialView(usuario);
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
