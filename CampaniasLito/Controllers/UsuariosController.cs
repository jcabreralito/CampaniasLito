using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Models;
using CampaniasLito.Classes;

namespace CampaniasLito.Controllers
{
    [Authorize(Roles = "SuperAdmin")]

    public class UsuariosController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        public ActionResult GetList()
        {
            var userList = db.Usuarios.ToList<Usuario>();
            return Json(new { data = userList }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Index(string usuario)
        {
            if (string.IsNullOrEmpty(usuario))
            {
                Session["usuarioFiltro"] = string.Empty;
            }
            else
            {
                Session["usuarioFiltro"] = usuario;
            }

            var filtro = Session["usuarioFiltro"].ToString();

            var usuarios = db.Usuarios.Include(u => u.Compañia);

            if (!string.IsNullOrEmpty(usuario))
            {
                return View(usuarios.Where(a => a.NombreUsuario.Contains(filtro) || a.Nombres.Contains(filtro)).ToList());
            }
            else
            {
                return View(usuarios.ToList());
            }
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

                    Session["Compañia"] = "Litoprocess";
                    TempData["msgUsuarioCreado"] = "USUARIO AGREGADO";

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

                    var db2 = new CampaniasLitoContext();
                    var currentUser = db2.Usuarios.Find(usuario.UsuarioId);
                    var currentRol = db2.Roles.Where(r => r.RolId == currentUser.RolId).FirstOrDefault();
                    var newRol = db.Roles.Where(r => r.RolId == usuario.RolId).FirstOrDefault();

                    if (currentUser.NombreUsuario != usuario.NombreUsuario || currentRol.RolId != newRol.RolId)
                    {
                        UsuariosHelper.UpdateUserName(currentUser.NombreUsuario, usuario.NombreUsuario, currentRol.Nombre, newRol.Nombre);
                    }

                    db2.Dispose();


                    Session["Compañia"] = "Litoprocess";
                    TempData["msgUsuarioEditado"] = "USUARIO EDITADO";

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

            ViewBag.CompañiaId = new SelectList(CombosHelper.GetCompañias(), "CompañiaId", "Nombre", usuario.CompañiaId);
            ViewBag.RolId = new SelectList(CombosHelper.GetRoles(), "RolId", "Nombre", usuario.RolId);
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
                Session["Compañia"] = "Litoprocess";
                TempData["msgUsuarioEliminado"] = "USUARIO ELIMINADO";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);

            ViewBag.CompañiaId = new SelectList(CombosHelper.GetCompañias(), "CompañiaId", "Nombre", usuario.CompañiaId);
            ViewBag.RolId = new SelectList(CombosHelper.GetRoles(), "RolId", "Nombre", usuario.RolId);
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
