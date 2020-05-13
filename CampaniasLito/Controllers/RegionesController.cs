using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Filters;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class RegionesController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Regiones
        public ActionResult GetList()
        {
            var regionesList = db.Regions.ToList<Region>();
            return Json(new { data = regionesList }, JsonRequestBehavior.AllowGet);

        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index()
        {
            Session["homeB"] = string.Empty;
            Session["rolesB"] = string.Empty;
            Session["compañiasB"] = string.Empty;
            Session["usuariosB"] = string.Empty;
            Session["regionesB"] = "active";
            Session["ciudadesB"] = string.Empty;
            Session["restaurantesB"] = string.Empty;
            Session["familiasB"] = string.Empty;
            Session["materialesB"] = string.Empty;
            Session["campañasB"] = string.Empty;

            return View();
        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult RegionesStock(string region)
        {
            string tipo = "STOCK";

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(region))
            {
                Session["regionFiltro"] = string.Empty;
            }
            else
            {
                Session["regionFiltro"] = region;
            }

            var filtro = Session["regionFiltro"].ToString();

            var regions = db.Regions.Where(c => c.EquityFranquicia == tipo);

            if (!string.IsNullOrEmpty(region))
            {
                return View(regions.Where(a => a.Nombre.Contains(filtro)).ToList());
            }
            else
            {
                return View(regions.ToList());
            }
        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult RegionesFranquicias(string region)
        {
            string tipo = "FRANQUICIAS";

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(region))
            {
                Session["regionFiltro"] = string.Empty;
            }
            else
            {
                Session["regionFiltro"] = region;
            }

            var filtro = Session["regionFiltro"].ToString();

            var regions = db.Regions.Where(c => c.EquityFranquicia == tipo);

            if (!string.IsNullOrEmpty(region))
            {
                return View(regions.Where(a => a.Nombre.Contains(filtro)).ToList());
            }
            else
            {
                return View(regions.ToList());
            }
        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult RegionesEquity(string region)
        {
            string tipo = "EQUITY";

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(region))
            {
                Session["regionFiltroEquity"] = string.Empty;
            }
            else
            {
                Session["regionFiltroEquity"] = region;
            }

            var filtro = Session["regionFiltroEquity"].ToString();

            var regions = db.Regions.Where(c => c.EquityFranquicia == tipo).ToList();

            if (!string.IsNullOrEmpty(region))
            {
                return View(regions.Where(a => a.Nombre.Contains(filtro)).ToList());
            }
            else
            {
                return View(regions.ToList());
            }
        }

        // GET: Regiones/Details/5
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = db.Regions.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // GET: Regiones/Create
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult Create( int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == 1)
            {
                Session["tipoRegion"] = "EQUITY";
            }
            else if (id == 2)
            {
                Session["tipoRegion"] = "FRANQUICIAS";
            }
            else if (id == 3)
            {
                Session["tipoRegion"] = "STOCK";
            }

            var regiones = new Region { };

            return PartialView(regiones);
        }

        // POST: Regiones/Create
        [AuthorizeUser(idOperacion: 1)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Region region)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            region.EquityFranquicia = Session["tipoRegion"].ToString();

            if (ModelState.IsValid)
            {
                db.Regions.Add(region);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    TempData["mensajeLito"] = "REGION AGREGADA";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return PartialView(region);
        }

        // GET: Regiones/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var region = db.Regions.Find(id);
            
            if (region == null)
            {
                return HttpNotFound();
            }

            return PartialView(region);
        }

        // POST: Regiones/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Region region)
        {
            if (ModelState.IsValid)
            {
                db.Entry(region).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {

                    TempData["mensajeLito"] = "REGION EDITADA";

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return View(region);
        }

        // GET: Regiones/Delete/5
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var region = db.Regions.Find(id);
            
            if (region == null)
            {
                return HttpNotFound();
            }
            
            return PartialView(region);
        }

        // POST: Regiones/Delete/5
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var region = db.Regions.Find(id);
            db.Regions.Remove(region);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                TempData["mensajeLito"] = "REGION ELIMINADA";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return PartialView(region);
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
