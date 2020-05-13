using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Filters;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class CiudadesController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Ciudades
        public ActionResult GetList()
        {
            var ciudadesList = db.Ciudads.ToList<Ciudad>();
            return Json(new { data = ciudadesList }, JsonRequestBehavior.AllowGet);

        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Session["homeB"] = string.Empty;
            Session["rolesB"] = string.Empty;
            Session["compañiasB"] = string.Empty;
            Session["usuariosB"] = string.Empty;
            Session["regionesB"] = string.Empty;
            Session["ciudadesB"] = "active";
            Session["restaurantesB"] = string.Empty;
            Session["familiasB"] = string.Empty;
            Session["materialesB"] = string.Empty;
            Session["campañasB"] = string.Empty;

            return View();
        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult CiudadesStock(string ciudad)
        {
            string tipo = "STOCK";

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(ciudad))
            {
                Session["ciudadFiltro"] = string.Empty;
            }
            else
            {
                Session["ciudadFiltro"] = ciudad;
            }

            var filtro = Session["ciudadFiltro"].ToString();

            var ciudads = db.Ciudads.Include(c => c.Region).Where(c => c.EquityFranquicia == tipo).OrderBy(c => c.Nombre);

            if (!string.IsNullOrEmpty(ciudad))
            {
                return View(ciudads.Where(a => a.Nombre.Contains(filtro) || a.Region.Nombre.Contains(filtro)).ToList());
            }
            else
            {
                return View(ciudads.ToList());
            }
        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult CiudadesFranquicias(string ciudad)
        {
            string tipo = "FRANQUICIAS";

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(ciudad))
            {
                Session["ciudadFiltro"] = string.Empty;
            }
            else
            {
                Session["ciudadFiltro"] = ciudad;
            }

            var filtro = Session["ciudadFiltro"].ToString();

            var ciudads = db.Ciudads.Include(c => c.Region).Where(c => c.EquityFranquicia == tipo).OrderBy(c => c.Nombre);

            if (!string.IsNullOrEmpty(ciudad))
            {
                return View(ciudads.Where(a => a.Nombre.Contains(filtro) || a.Region.Nombre.Contains(filtro)).ToList());
            }
            else
            {
                return View(ciudads.ToList());
            }
        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult CiudadesEquity(string ciudad)
        {
            string tipo = "EQUITY";

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(ciudad))
            {
                Session["ciudadFiltroEquity"] = string.Empty;
            }
            else
            {
                Session["ciudadFiltroEquity"] = ciudad;
            }

            var filtro = Session["ciudadFiltroEquity"].ToString();

            var ciudads = db.Ciudads.Include(c => c.Region).Where(c => c.EquityFranquicia == tipo).OrderBy(c => c.Nombre);

            if (!string.IsNullOrEmpty(ciudad))
            {
                return View(ciudads.Where(a => a.Nombre.Contains(filtro) || a.Region.Nombre.Contains(filtro)).ToList());
            }
            else
            {
                return View(ciudads.ToList());
            }
        }

        // GET: Ciudades/Details/5
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ciudad = db.Ciudads.Find(id);
            
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            return PartialView(ciudad);
        }

        // GET: Ciudades/Create
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult Create(int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == 1)
            {
                Session["tipoCiudad"] = "EQUITY";
            }
            else if (id == 2)
            {
                Session["tipoCiudad"] = "FRANQUICIAS";
            }
            else if (id == 3)
            {
                Session["tipoCiudad"] = "STOCK";
            }

            var ciudades = new Ciudad { };

            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(id, true), "RegionId", "Nombre");

            return PartialView(ciudades);
        }

        // POST: Ciudades/Create
        [AuthorizeUser(idOperacion: 1)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ciudad ciudad)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            ciudad.EquityFranquicia = Session["tipoCiudad"].ToString();

            if (ModelState.IsValid)
            {
                db.Ciudads.Add(ciudad);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    TempData["mensajeLito"] = "CIUDAD AGREGADA";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(true), "RegionId", "Nombre", ciudad.RegionId);

            return PartialView(ciudad);
        }

        // GET: Ciudades/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ciudad = db.Ciudads.Find(id);
            
            if (ciudad == null)
            {
                return HttpNotFound();
            }

            int tipo = 0;

            if (ciudad.EquityFranquicia == "EQUITY")
            {
                tipo = 1;
            }
            else if (ciudad.EquityFranquicia == "FRANQUICIAS")
            {
                tipo = 2;
            }
            else if (ciudad.EquityFranquicia == "STOCK")
            {
                tipo = 3;
            }

            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(tipo, true), "RegionId", "Nombre", ciudad.RegionId);

            return PartialView(ciudad);
        }

        // POST: Ciudades/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ciudad ciudad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ciudad).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {

                    TempData["mensajeLito"] = "CIUDAD EDITADA";

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }


            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(true), "RegionId", "Nombre", ciudad.RegionId);

            return View(ciudad);
        }

        // GET: Ciudades/Delete/5
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ciudad = db.Ciudads.Find(id);
            
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            
            return PartialView(ciudad);
        }

        // POST: Ciudades/Delete/5
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ciudad = db.Ciudads.Find(id);
            db.Ciudads.Remove(ciudad);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                TempData["mensajeLito"] = "CIUDAD ELIMINADA";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);

            return PartialView(ciudad);
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
