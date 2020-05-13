using CampaniasLito.Models;
using System.Linq;
using System.Web.Mvc;

namespace CampaniasLito.Controllers
{
    public class GenericController : Controller
    {
        private readonly CampaniasLitoContext db = new CampaniasLitoContext();

        public JsonResult GetCiudadesXRegion(int regionId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var ciudades = db.Ciudads.Where(d => d.RegionId == regionId);
            return Json(new { Data = ciudades }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetColonias(int delegacionId)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var colonias = db.Colonias.Where(c => c.MunicipioDelegacionId == delegacionId);
        //    return Json(colonias);
        //}

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