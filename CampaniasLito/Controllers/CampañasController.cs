using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class CampañasController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Campañas
        public ActionResult GetList()
        {
            var campañasList = db.Campañas.ToList<Campaña>();
            return Json(new { data = campañasList }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Index(string campaña)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(campaña))
            {
                Session["campañaFiltro"] = string.Empty;
            }
            else
            {
                Session["campañaFiltro"] = campaña;
            }

            var filtro = Session["campañaFiltro"].ToString();

            var campañas = db.Campañas.Where(c => c.CompañiaId == usuario.CompañiaId);

            if (!string.IsNullOrEmpty(campaña))
            {
                return View(campañas.Where(a => a.Nombre.Contains(filtro) || a.Generada.Contains(filtro) || a.Descripcion.Contains(filtro)).ToList());
            }
            else
            {
                return View(campañas.ToList());
            }

        }

        // GET: Campañas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var campaña = db.Campañas.Find(id);

            if (campaña == null)
            {
                return HttpNotFound();
            }

            return PartialView(campaña);
        }

        // GET: Campañas/Create
        public ActionResult CreateCamp(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

                var response = MovementsHelper.AgregarTiendas(usuario.Compañia.CompañiaId, usuario.NombreUsuario, (int)id);

                if (response.Succeeded)
                {
                    //TempData["msgCampañaCreada"] = "CAMPAÑA AGREGADA";
                }

                ModelState.AddModelError(string.Empty, response.Message);

            var view = new NuevaCampañaView
            {
                CampañaId = db.NuevaCampañaViews.Where(cdt => cdt.CampañaId == (int)id).FirstOrDefault().CampañaId,
                Descripcion = db.NuevaCampañaViews.Where(cdt => cdt.CampañaId == (int)id).FirstOrDefault().Descripcion,
                Nombre = db.NuevaCampañaViews.Where(cdt => cdt.CampañaId == (int)id).FirstOrDefault().Nombre,
                Detalles = db.CampañaTiendaTMPs.Where(cdt => cdt.Usuario == usuario.NombreUsuario).ToList(),
            };

            return View(view);


        }

        // POST: Campañas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCamp(NuevaCampañaView campaña)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (!string.IsNullOrEmpty(campaña.Descripcion) && !string.IsNullOrEmpty(campaña.Nombre))
            {
                campaña.Detalles = db.CampañaTiendaTMPs.Where(cdt => cdt.Usuario == usuario.NombreUsuario).ToList();

                if (campaña.Detalles.Count != 0)
                {
                    if (ModelState.IsValid)
                    {
                        var response = MovementsHelper.NuevaCampaña(campaña, usuario.NombreUsuario, usuario.Compañia.CompañiaId);
                        if (response.Succeeded)
                        {
                            TempData["msgCampañaCreada"] = "CAMPAÑA AGREGADA";

                            return RedirectToAction("Index");
                        }

                        ModelState.AddModelError(string.Empty, response.Message);
                    }
                }
                else
                {
                    TempData["msgDetalles"] = "AUN NO SE HAN AGREGADO TIENDAS";
                }
            }
            else
            {
                TempData["msgCampaña"] = "CAPTURAR LOS DATOS DE LA CAMPAÑA";
            }

            return View(campaña);

        }

        // GET: Campañas/Create
        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var campañas = new Campaña { CompañiaId = usuario.CompañiaId, };

            campañas.Generada = "NO";

            return PartialView(campañas);


        }

        // POST: Campañas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Campaña campaña)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            campaña.Generada = "NO";

            if (ModelState.IsValid)
            {

                db.Campañas.Add(campaña);

                var response = DBHelper.SaveChanges(db);

                if (response.Succeeded)
                {
                    TempData["msgCampañaCreada"] = "CAMPAÑA CREADA";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return PartialView(campaña);

        }

        // GET: Campañas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var campaña = db.Campañas.Find(id);

            if (campaña == null)
            {
                return HttpNotFound();
            }

            return PartialView(campaña);
        }

        // POST: Campañas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Campaña campaña)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campaña).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {

                    TempData["msgCampañaEditada"] = "CAMPAÑA EDITADA";

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return View(campaña);

        }

        // GET: Campañas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var campaña = db.Campañas.Find(id);

            if (campaña == null)
            {
                return HttpNotFound();
            }

            return View(campaña);
        }

        // POST: Campañas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var campaña = db.Campañas.Find(id);
            db.Campañas.Remove(campaña);
            var response = DBHelper.SaveChanges(db);

            if (response.Succeeded)
            {
                TempData["msgCampañaEliminada"] = "CAMPAÑA ELIMINADA";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return PartialView(campaña);
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
