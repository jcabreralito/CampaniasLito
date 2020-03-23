using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Filters;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class ArticulosKFCController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        public ActionResult GetList()
        {
            var artList = db.ArticuloKFCs.ToList<ArticuloKFC>();
            return Json(new { data = artList }, JsonRequestBehavior.AllowGet);

        }

        // GET: ArticulosKFC
        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index(string articulo)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(articulo))
            {
                Session["articuloFiltro"] = string.Empty;
            }
            else
            {
                Session["articuloFiltro"] = articulo;
            }

            var filtro = Session["articuloFiltro"].ToString();

            var articuloKFCs = db.ArticuloKFCs.Where(a => a.CompañiaId == usuario.CompañiaId).OrderBy(a => a.Familia).ThenBy(a => a.Descripcion);

            if (!string.IsNullOrEmpty(articulo))
            {
                return View(articuloKFCs.Where(a => a.Descripcion.Contains(filtro) || a.Familia.Contains(filtro) || a.Proveedor.Nombre.Contains(filtro)).ToList());
            }
            else
            {
                return View(articuloKFCs.ToList());
            }

        }

        // GET: ArticulosKFC/Details/5
        [AuthorizeUser(idOperacion: 6)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticuloKFC articuloKFC = db.ArticuloKFCs.Find(id);
            if (articuloKFC == null)
            {
                return HttpNotFound();
            }
            return PartialView(articuloKFC);
        }

        // GET: ArticulosKFC/Create
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var articulos = new ArticuloKFC { CompañiaId = usuario.CompañiaId, };

            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre");

            return PartialView(articulos);
        }

        // POST: ArticulosKFC/Create
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticuloKFC articuloKFC)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.ArticuloKFCs.Add(articuloKFC);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    MovementsHelper.AgregarArticuloTiendas(articuloKFC.ArticuloKFCId);
                    MovementsHelper.AgregarArticuloCampañas(articuloKFC.ArticuloKFCId, usuario.CompañiaId, usuario.NombreUsuario);

                    TempData["msgArticuloCreado"] = "ARTICULO AGREGADO";

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre", articuloKFC.ProveedorId);

            return PartialView(articuloKFC);
        }

        // GET: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult AsignarTiendas(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tiendaArticulos = db.TiendaArticulos.Where(t => t.ArticuloKFCId == id).OrderBy(t => t.TiendaId).ToList();

            if (tiendaArticulos == null)
            {
                return HttpNotFound();
            }


            ViewBag.Articulo = db.ArticuloKFCs.Where(t => t.ArticuloKFCId == id).FirstOrDefault().Descripcion;

            return PartialView(tiendaArticulos);
        }

        private bool Update(TiendaArticulo product)
        {
            return true;
        }

        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarTiendas(FormCollection fc)
        {
            var nombre = User.Identity.Name;
            var usuarioActual = db.Usuarios.Where(u => u.NombreUsuario == nombre).FirstOrDefault();

            string[] articuloKFCTMPId = fc.GetValues("TiendaArticuloId");
            string[] seleccionado = fc.GetValues("Seleccionado");

            var selec = false;
            var cantidad = 0;

            for (var i = 0; i < articuloKFCTMPId.Length; i++)
            {
                TiendaArticulo tiendaArticulo = db.TiendaArticulos.Find(Convert.ToInt32(articuloKFCTMPId[i]));

                var tiendaId = tiendaArticulo.TiendaId;
                var articuloId = tiendaArticulo.ArticuloKFCId;
                var campañas = db.Campañas.Where(ct => ct.Generada == "NO").OrderBy(ct => ct.CampañaId).FirstOrDefault().CampañaId;

                CampañaArticuloTMP campañaArticulo = db.CampañaArticuloTMPs.Where(ta => ta.TiendaId == tiendaId && ta.ArticuloKFCId == articuloId && ta.CampañaTiendaTMPId == campañas).FirstOrDefault();

                selec = false;
                cantidad = 0;

                for (var j = 0; j < seleccionado.Length; j++)
                {
                    if (articuloKFCTMPId[i] == seleccionado[j])
                    {
                        selec = true;

                        tiendaArticulo.Seleccionado = selec;

                        db.Entry(tiendaArticulo).State = EntityState.Modified;

                        if (campañaArticulo.Habilitado == false)
                        {
                            var articuloCantidadDefault = db.ArticuloKFCs.Where(a => a.ArticuloKFCId == campañaArticulo.ArticuloKFCId).FirstOrDefault().CantidadDefault;
                            cantidad = articuloCantidadDefault;
                            campañaArticulo.Cantidad = cantidad;
                        }

                        campañaArticulo.Habilitado = selec;
                        //campañaArticulo.Cantidad = cantidad;

                        db.Entry(campañaArticulo).State = EntityState.Modified;


                        db.SaveChanges();

                        break;
                    }
                }
                if (!selec)
                {
                    selec = false;
                    cantidad = 0;

                    tiendaArticulo.Seleccionado = selec;

                    db.Entry(tiendaArticulo).State = EntityState.Modified;

                    campañaArticulo.Habilitado = selec;
                    campañaArticulo.Cantidad = cantidad;

                    db.Entry(campañaArticulo).State = EntityState.Modified;

                    db.SaveChanges();
                }
            }

            TempData["msgTiendaEditada"] = "TIENDAS ASIGNADAS";

            return RedirectToAction("Index");

        }

        // GET: ArticulosKFC/Edit/5
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var articuloKFC = db.ArticuloKFCs.Find(id);
            
            if (articuloKFC == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre", articuloKFC.ProveedorId);

            return PartialView(articuloKFC);
        }

        // POST: ArticulosKFC/Edit/5
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticuloKFC articuloKFC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articuloKFC).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {

                    TempData["msgArticuloEditado"] = "ARTICULO EDITADO";
                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre", articuloKFC.ProveedorId);

            return PartialView(articuloKFC);
        }

        // GET: ArticulosKFC/Delete/5
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var articuloKFC = db.ArticuloKFCs.Find(id);
            
            if (articuloKFC == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProveedorId = new SelectList(CombosHelper.GetProveedores(true), "ProveedorId", "Nombre", articuloKFC.ProveedorId);

            return View(articuloKFC);
        }

        // POST: ArticulosKFC/Delete/5
        [AuthorizeUser(idOperacion: 4)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var articulo = db.ArticuloKFCs.Find(id);
            var articuloTienda = db.TiendaArticulos.Where(ta => ta.ArticuloKFCId == id).ToList();

            foreach (var item in articuloTienda)
            {
                db.TiendaArticulos.Remove(item);
                db.SaveChanges();
            }

            db.ArticuloKFCs.Remove(articulo);

            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                TempData["msgArticuloEliminado"] = "ARTICULO ELIMINADO";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);

            return PartialView(articulo);
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
