using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Models;
using CampaniasLito.Classes;
using System;

namespace CampaniasLito.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]

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

            Session["homeB"] = string.Empty;
            Session["rolesB"] = string.Empty;
            Session["compañiasB"] = string.Empty;
            Session["usuariosB"] = "active";
            Session["regionesB"] = string.Empty;
            Session["ciudadesB"] = string.Empty;
            Session["restaurantesB"] = string.Empty;
            Session["familiasB"] = string.Empty;
            Session["materialesB"] = string.Empty;
            Session["campañasB"] = string.Empty;

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
        public async System.Threading.Tasks.Task<ActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    var rol = db.Roles.Where(r => r.RolId == usuario.RolId).FirstOrDefault();

                    //UsuariosHelper.CreateUserASP(usuario.NombreUsuario, rol.Nombre);

                    var random = new Random();
                    //var password = usuario.NombreUsuario;

                    var password = string.Format("{0}{1}{2:04}*",
                        usuario.Nombres.Trim().ToUpper().Substring(0, 1),
                        usuario.Apellidos.Trim().ToLower().Substring(0, 1) + "Lt",
                        random.Next(10000));

                    UsuariosHelper.CreateUserASP(usuario.NombreUsuario, rol.Nombre, password);

                    var subject = "Bienvenido a la Plataforma de Campañas";
                    var body = string.Format(@"
                    <h1>Bienvenido a la Plataforma de Campañas</h1>
                    <p>Tu Usuario es: <strong>{1}</strong></p>
                    <p>Tu password es: <strong>{0}</strong></p>
                    <p>Link de la Plataforma: <a href='portal.litoprocess.com/Campanias'>portal.litoprocess.com/Campanias</a>",
                    password, usuario.NombreUsuario);

                    //await MailHelper.SendMail("jesuscabrerag@yahoo.com.mx", subject, body);

                    await MailHelper.SendMail(usuario.NombreUsuario, "jesuscabrerag@yahoo.com.mx", subject, body);

                    //var rigistrar = db.Usuarios.Where(r => r.Registrado == null).Where(u => u.UsuarioId == usuario.UsuarioId).FirstOrDefault();

                    //rigistrar.Registrado = "SI";
                    //db.Entry(rigistrar).State = EntityState.Modified;
                    //db.SaveChanges();

                    UsuariosHelper.AddRole(usuario.NombreUsuario, rol.Nombre, password);

                    TempData["mensajeLito"] = "USUARIO AGREGADO";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.CompañiaId = new SelectList(CombosHelper.GetCompañias(true), "CompañiaId", "Nombre", usuario.CompañiaId);
            ViewBag.RolId = new SelectList(CombosHelper.GetRoles(true), "RolId", "Nombre", usuario.RolId);

            return View(usuario);
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

            Session["UsuarioEditado"] = usuario.NombreUsuario.ToLower();
            Session["RolEditado"] = usuario.RolId;

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
                    var currentUser = Session["UsuarioEditado"].ToString();
                    var currentRol = (int)Session["RolEditado"];
                    var currentRolNombre = db.Roles.Where(r => r.RolId == currentRol).FirstOrDefault();
                    var newRol = db.Roles.Where(r => r.RolId == usuario.RolId).FirstOrDefault();

                    if (currentUser == usuario.NombreUsuario || currentRol == newRol.RolId)
                    {
                        UsuariosHelper.UpdateUserName(currentUser, usuario.NombreUsuario.ToLower(), currentRolNombre.Nombre, newRol.Nombre);
                    }


                    Session["Compañia"] = "Litoprocess";
                    TempData["mensajeLito"] = "USUARIO EDITADO";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.CompañiaId = new SelectList(CombosHelper.GetCompañias(), "CompañiaId", "Nombre", usuario.CompañiaId);
            ViewBag.RolId = new SelectList(CombosHelper.GetRoles(), "RolId", "Nombre", usuario.RolId);

            return PartialView(usuario);
        }

        public ActionResult Perfil()
        {
            var nombre = User.Identity.Name;
            var usuarioActual = db.Usuarios.Where(u => u.NombreUsuario == nombre).FirstOrDefault();
            int? id = usuarioActual.UsuarioId;
            var perfilUser = usuarioActual.RolId;
            var fecha = DateTime.Now;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = db.Usuarios.Find(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            ViewBag.RolId = new SelectList(CombosHelper.GetRoles(), "RolId", "Nombre", usuario.RolId);
            ViewBag.CompañiaId = new SelectList(CombosHelper.GetCompañias(), "CompañiaId", "Nombre", usuario.CompañiaId);

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
            var currentRol = db.Roles.Where(r => r.RolId == usuario.RolId).FirstOrDefault();

            db.Usuarios.Remove(usuario);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                UsuariosHelper.DeleteUser(usuario.NombreUsuario);

                Session["Compañia"] = "Litoprocess";
                TempData["mensajeLito"] = "USUARIO ELIMINADO";

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
