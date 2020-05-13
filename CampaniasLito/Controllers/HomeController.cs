using CampaniasLito.Models;
using System.Linq;
using System.Web.Mvc;

namespace CampaniasLito.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        public ActionResult Index()
        {
            //var f11 = Session["F11"].ToString();

            //if (string.IsNullOrEmpty(f11))
            //{
            //    Session["F11"] = "NO";
            //}
            //else if(f11 == "SI")
            //{
            //    SendKeys.SendWait("{F11}");
            //    Session["F11"] = "NO";
            //}
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

        public ActionResult About()
        {
            ViewBag.Message = "Descripción de la Aplicación.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacto de la Aplicación.";

            return View();
        }
    }
}