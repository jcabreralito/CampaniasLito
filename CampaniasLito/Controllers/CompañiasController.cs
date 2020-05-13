using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Models;
using CampaniasLito.Classes;

namespace CampaniasLito.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class CompañiasController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        public ActionResult Index()
        {
            Session["homeB"] = string.Empty;
            Session["rolesB"] = string.Empty;
            Session["compañiasB"] = "active";
            Session["usuariosB"] = string.Empty;
            Session["regionesB"] = string.Empty;
            Session["ciudadesB"] = string.Empty;
            Session["restaurantesB"] = string.Empty;
            Session["familiasB"] = string.Empty;
            Session["materialesB"] = string.Empty;
            Session["campañasB"] = string.Empty;

            //var compañia = db.Compañias.Include(c => c.Ciudad).Include(c => c.DelegacionMunicipio).Include(c => c.Colonia);
            return View(db.Compañias.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var compañia = db.Compañias.Find(id);

            if (compañia == null)
            {
                return HttpNotFound();
            }
            return PartialView(compañia);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Compañia compañia)
        {
            if (ModelState.IsValid)
            {
                db.Compañias.Add(compañia);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    if (compañia.LogoFile != null)
                    {
                        var folder = "~/Content/Logos";
                        var file = string.Format("{0}.jpg", compañia.CompañiaId);
                        var responseLogo = FilesHelper.UploadPhoto(compañia.LogoFile, folder, file);
                        if (responseLogo)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            compañia.Logo = pic;
                            db.Entry(compañia).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    Session["Compañia"] = "Litoprocess";
                    TempData["mensajeLito"] = "COMPAÑIA AGREGADA";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return PartialView(compañia);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var compañia = db.Compañias.Find(id);

            if (compañia == null)
            {
                return HttpNotFound();
            }

            return PartialView(compañia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Compañia compañia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compañia).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);

                if (response.Succeeded)
                {
                    if (compañia.LogoFile != null)
                    {
                        var folder = "~/Content/Logos";
                        var file = string.Format("{0}.jpg", compañia.CompañiaId);
                        var responseLogo = FilesHelper.UploadPhoto(compañia.LogoFile, folder, file);
                        if (responseLogo)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            compañia.Logo = pic;
                            db.Entry(compañia).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    Session["Compañia"] = "Litoprocess";
                    TempData["mensajeLito"] = "COMPAÑIA EDITADA";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return PartialView(compañia);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var compañia = db.Compañias.Find(id);

            if (compañia == null)
            {
                return HttpNotFound();
            }

            return PartialView(compañia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var compañia = db.Compañias.Find(id);
            db.Compañias.Remove(compañia);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                Session["Compañia"] = "Litoprocess";
                TempData["mensajeLito"] = "COMPAÑIA ELIMINADA";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return View(compañia);
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
