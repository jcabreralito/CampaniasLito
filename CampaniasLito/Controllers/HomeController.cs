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