using CampaniasLito.Models;
using System.Linq;
using System.Web.Mvc;

namespace CampaniasLito.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly CampaniasLitoContext db = new CampaniasLitoContext();

        public RedirectToRouteResult IndexL()
        {
            Session["Mensaje"] = string.Empty;
            return RedirectToRoute("Login");
        }

        public ActionResult Index()
        {
            Session["iconoTitulo"] = "fas fa-calendar-alt";
            Session["homeB"] = "active";
            Session["rolesB"] = string.Empty;
            Session["compañiasB"] = string.Empty;
            Session["usuariosB"] = string.Empty;
            Session["regionesB"] = string.Empty;
            Session["ciudadesB"] = string.Empty;
            Session["restaurantesB"] = string.Empty;
            Session["familiasB"] = string.Empty;
            Session["materialesB"] = string.Empty;
            Session["campañasB"] = string.Empty;
            Session["reglasB"] = string.Empty;
            Session["bitacoraB"] = string.Empty;

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            return View(usuario);
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult IndexAdmin()
        {
            Session["iconoTitulo"] = "fas fa-calendar-alt";
            Session["homeB"] = "active";
            Session["rolesB"] = string.Empty;
            Session["compañiasB"] = string.Empty;
            Session["usuariosB"] = string.Empty;
            Session["regionesB"] = string.Empty;
            Session["ciudadesB"] = string.Empty;
            Session["restaurantesB"] = string.Empty;
            Session["familiasB"] = string.Empty;
            Session["materialesB"] = string.Empty;
            Session["campañasB"] = string.Empty;

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            return View(usuario);
        }
    }
}