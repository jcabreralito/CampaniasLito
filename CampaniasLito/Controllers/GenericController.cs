using CampaniasLito.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampaniasLito.Controllers
{
    public class GenericController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        //public JsonResult GetMunicipios(int ciudadId)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var municipios = db.DelegacionMunicipios.Where(d => d.CiudadId == ciudadId);
        //    return Json(municipios);
        //}

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