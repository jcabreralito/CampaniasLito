using CampaniasLito.Filters;
using CampaniasLito.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CampaniasLito.Controllers
{

    [Authorize]
    public class BitacoraController : Controller
    {
        private readonly CampaniasLitoContext db = new CampaniasLitoContext();

        public class spBitacora
        {
            public int BitacoraId { get; set; }

            public string Fecha { get; set; }

            public string NombreUsuario { get; set; }

            public string Modulo { get; set; }

            public string Movimiento { get; set; }
        }

        // GET: Bitacora
        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index()
        {
            Session["iconoTitulo"] = "fas fa-atlas";
            Session["homeB"] = string.Empty;
            Session["rolesB"] = string.Empty;
            Session["compañiasB"] = string.Empty;
            Session["usuariosB"] = string.Empty;
            Session["regionesB"] = string.Empty;
            Session["ciudadesB"] = string.Empty;
            Session["restaurantesB"] = string.Empty;
            Session["materialesAcB"] = string.Empty;
            Session["familiasB"] = string.Empty;
            Session["materialesB"] = string.Empty;
            Session["campañasB"] = string.Empty;
            Session["reglasB"] = string.Empty;
            Session["bitacoraB"] = "active";
            Session["Mensaje"] = string.Empty;

            return View();
        }

        public ActionResult GetData()
        {
            var bitacoraList = db.Database.SqlQuery<spBitacora>("spGetBitacora").ToList();

            return Json(new { data = bitacoraList }, JsonRequestBehavior.AllowGet);
        }

    }
}