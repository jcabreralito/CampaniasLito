using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using CampaniasLito.Classes;
using CampaniasLito.Filters;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class RestaurantesController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        [HttpPost]
        public JsonResult TiendasList()
        {
            try
            {
                List<ArticuloKFC> tiendas = new List<ArticuloKFC>();
                tiendas = db.ArticuloKFCs.ToList();

                return Json(new { Result = "OK", Records = tiendas }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        // GET: Tiendas
        [AuthorizeUser(idOperacion: 5)]
        public ActionResult IndexTiendas()
        {
            return View();

        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult GetList()
        {
            var campañasList = db.Campañas.ToList<Campaña>();
            return Json(new { data = campañasList }, JsonRequestBehavior.AllowGet);

        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult ConfiguracionesIndex()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var configuraciones = db.TiendaConfiguracions.ToList();

            return View(configuraciones.ToList());
        }

        // GET: Ciudades/Create
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult CreateConfiguracion()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var configuraciones = new TiendaConfiguracion { };

            ViewBag.TipoConfiguracionId = new SelectList(CombosHelper.GetTipoConfiguracion(true), "TipoConfiguracionId", "Nombre");

            return PartialView(configuraciones);
        }

        // POST: Ciudades/Create
        [AuthorizeUser(idOperacion: 1)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfiguracion(TiendaConfiguracion tiendaConfiguracion)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.TiendaConfiguracions.Add(tiendaConfiguracion);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    TempData["mensajeLito"] = "CONFIGURACIÓN AGREGADA";

                    return RedirectToAction("ConfiguracionesIndex");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.TipoConfiguracionId = new SelectList(CombosHelper.GetTipoConfiguracion(true), "TipoConfiguracionId", "Nombre");


            return PartialView(tiendaConfiguracion);
        }

        // GET: Ciudades/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult EditConfiguracion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var configuraciones = db.TiendaConfiguracions.Find(id);

            if (configuraciones == null)
            {
                return HttpNotFound();
            }

            ViewBag.TipoConfiguracionId = new SelectList(CombosHelper.GetTipoConfiguracion(true), "TipoConfiguracionId", "Nombre", configuraciones.TipoConfiguracionId);

            return PartialView(configuraciones);
        }

        // POST: Ciudades/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfiguracion(TiendaConfiguracion tiendaConfiguracion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiendaConfiguracion).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {

                    TempData["mensajeLito"] = "CONFIGURACIÓN EDITADA";

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }


            ViewBag.TipoConfiguracionId = new SelectList(CombosHelper.GetTipoConfiguracion(true), "TipoConfiguracionId", "Nombre", tiendaConfiguracion.TipoConfiguracionId);

            return View(tiendaConfiguracion);
        }

        // GET: Ciudades/Delete/5
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult DeleteConfiguracion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var configuraciones = db.TiendaConfiguracions.Find(id);

            if (configuraciones == null)
            {
                return HttpNotFound();
            }

            return PartialView(configuraciones);
        }

        // POST: Ciudades/Delete/5
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost, ActionName("DeleteConfiguracion")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedConfiguracion(int id)
        {
            var configuracion = db.TiendaConfiguracions.Find(id);
            db.TiendaConfiguracions.Remove(configuracion);
            var response = DBHelper.SaveChanges(db);
            if (response.Succeeded)
            {
                TempData["mensajeLito"] = "CONFIGURACIÓN ELIMINADA";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);

            return PartialView(configuracion);
        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index()
        {
            Session["homeB"] = string.Empty;
            Session["rolesB"] = string.Empty;
            Session["compañiasB"] = string.Empty;
            Session["usuariosB"] = string.Empty;
            Session["regionesB"] = string.Empty;
            Session["ciudadesB"] = string.Empty;
            Session["restaurantesB"] = "active";
            Session["familiasB"] = string.Empty;
            Session["materialesB"] = string.Empty;
            Session["campañasB"] = string.Empty;

            return View();
        }

        //[AuthorizeUser(idOperacion: 5)]
        //public ActionResult ConfiguracionesGenerales()
        //{
        //    var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

        //    if (usuario == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    var configuraciones = db.TiendaGenerales.ToList();

        //        return View(configuraciones.ToList());
        //}

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult RestaurantesStock(string tienda)
        {
            string tipo = "STOCK";

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(tienda))
            {
                Session["tiendaFiltroStock"] = string.Empty;
            }
            else
            {
                Session["tiendaFiltroStock"] = tienda;
            }

            var filtro = Session["tiendaFiltroStock"].ToString();

            var tiendas = db.Tiendas.Where(a => a.EquityFranquicia == tipo);

            if (!string.IsNullOrEmpty(tienda))
            {
                return View(tiendas.Where(a => a.Restaurante.Contains(filtro) || a.Ciudad.Nombre.Contains(filtro) || a.CCoFranquicia.Contains(filtro)).ToList());
            }
            else
            {
                return View(tiendas.ToList());
            }
        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult RestaurantesFranquicias(string tienda)
        {
            string tipo = "FRANQUICIAS";

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(tienda))
            {
                Session["tiendaFiltro"] = string.Empty;
            }
            else
            {
                Session["tiendaFiltro"] = tienda;
            }

            var filtro = Session["tiendaFiltro"].ToString();

            var tiendas = db.Tiendas.Where(a => a.EquityFranquicia == tipo);

            if (!string.IsNullOrEmpty(tienda))
            {
                return View(tiendas.Where(a => a.Restaurante.Contains(filtro) || a.Ciudad.Nombre.Contains(filtro) || a.CCoFranquicia.Contains(filtro)).ToList());
            }
            else
            {
                return View(tiendas.ToList());
            }
        }

        //[AuthorizeUser(idOperacion: 5)]
        //public ActionResult RestaurantesEquity(string tienda)
        //{
        //    string tipo = "EQUITY";

        //    var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

        //    if (usuario == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    if (string.IsNullOrEmpty(tienda))
        //    {
        //        Session["tiendaFiltroEquity"] = string.Empty;
        //    }
        //    else
        //    {
        //        Session["tiendaFiltroEquity"] = tienda;
        //    }

        //    var filtro = Session["tiendaFiltroEquity"].ToString();

        //    var tiendas = db.Tiendas.Where(a => a.EquityFranquicia == tipo).ToList<Tienda>();

        //    if (!string.IsNullOrEmpty(tienda))
        //    {
        //        return View(tiendas.Where(a => a.Restaurante.Contains(filtro) || a.Ciudad.Nombre.Contains(filtro) || a.CCoFranquicia.Contains(filtro)).ToList());
        //    }
        //    else
        //    {
        //        return Json(new { data = tiendas }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult RestaurantesEquity(string tienda)
        {
            string tipo = "EQUITY";

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(tienda))
            {
                Session["tiendaFiltroEquity"] = string.Empty;
            }
            else
            {
                Session["tiendaFiltroEquity"] = tienda;
            }

            var filtro = Session["tiendaFiltroEquity"].ToString();

            var tiendas = db.Tiendas.Where(a => a.EquityFranquicia == tipo);

            if (!string.IsNullOrEmpty(tienda))
            {
                return View(tiendas.Where(a => a.Restaurante.Contains(filtro) || a.Ciudad.Nombre.Contains(filtro) || a.CCoFranquicia.Contains(filtro)).ToList());
            }
            else
            {
                return View(tiendas.ToList());
            }
        }

        // GET: Tiendas/Details/5
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult Details(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(true), "CiudadId", "Nombre", tienda.CiudadId);
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(true), "RegionId", "Nombre", tienda.RegionId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Descripcion", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // GET: Tiendas/Details/5
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult DetailsCG(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);

            return PartialView(tienda);
        }

        // GET: Tiendas/Details/5
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult DetailsCP(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            return PartialView(tienda);
        }

        // GET: Tiendas/Details/5
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult DetailsCME(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            return PartialView(tienda);
        }

        // GET: Tiendas/Details/5
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult DetailsCMEs(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            return PartialView(tienda);
        }

        // GET: Tiendas/Details/5
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult DetailsCER(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);

            return PartialView(tienda);
        }

        // GET: Tiendas/Create
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
                Session["tipoTienda"] = "EQUITY";
            }
            else if (id == 2)
            {
                Session["tipoTienda"] = "FRANQUICIAS";
            }
            else if (id == 3)
            {
                Session["tipoTienda"] = "STOCK";
            }

            var tienda = new Tienda { };

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(id, true), "CiudadId", "Nombre");
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(id, true), "RegionId", "Nombre");
            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion");
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo");
            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion");
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion");
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo");

            return View(tienda);
        }

        // GET: Tiendas/Create
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult CreateCG(int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == 1)
            {
                Session["tipoTienda"] = "EQUITY";
            }
            else if (id == 2)
            {
                Session["tipoTienda"] = "FRANQUICIAS";
            }
            else if (id == 3)
            {
                Session["tipoTienda"] = "STOCK";
            }

            var tienda = new Tienda { };

            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion");
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo");

            return View(tienda);
        }

        // GET: Tiendas/Create
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult CreateCP(int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == 1)
            {
                Session["tipoTienda"] = "EQUITY";
            }
            else if (id == 2)
            {
                Session["tipoTienda"] = "FRANQUICIAS";
            }
            else if (id == 3)
            {
                Session["tipoTienda"] = "STOCK";
            }

            var tienda = new Tienda { };

            return View(tienda);
        }

        // GET: Tiendas/Create
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult CreateCME(int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == 1)
            {
                Session["tipoTienda"] = "EQUITY";
            }
            else if (id == 2)
            {
                Session["tipoTienda"] = "FRANQUICIAS";
            }
            else if (id == 3)
            {
                Session["tipoTienda"] = "STOCK";
            }

            var tienda = new Tienda { };

            return View(tienda);
        }

        // GET: Tiendas/Create
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult CreateCMEs(int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == 1)
            {
                Session["tipoTienda"] = "EQUITY";
            }
            else if (id == 2)
            {
                Session["tipoTienda"] = "FRANQUICIAS";
            }
            else if (id == 3)
            {
                Session["tipoTienda"] = "STOCK";
            }

            var tienda = new Tienda { };

            return View(tienda);
        }

        // GET: Tiendas/Create
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult CreateCER(int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == 1)
            {
                Session["tipoTienda"] = "EQUITY";
            }
            else if (id == 2)
            {
                Session["tipoTienda"] = "FRANQUICIAS";
            }
            else if (id == 3)
            {
                Session["tipoTienda"] = "STOCK";
            }

            var tienda = new Tienda { };

            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion");
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion");

            return View(tienda);
        }

        // POST: Tiendas/Create
        [AuthorizeUser(idOperacion: 1)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tienda tienda)
        {

            tienda.EquityFranquicia = Session["tipoTienda"].ToString();

            if (ModelState.IsValid)
            {
                db.Tiendas.Add(tienda);
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    var responseATA = MovementsHelper.AgregarTiendaArticulos(tienda.TiendaId);

                    if (responseATA.Succeeded)
                    {
                        MovementsHelper.ReglasTiendaArticulos(tienda.TiendaId);
                    }


                    TempData["mensajeLito"] = "RESTAURANTE AGREGADO";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(true), "CiudadId", "Nombre");
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(true), "RegionId", "Nombre");
            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion");
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo");
            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion");
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion");
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo");


            return PartialView(tienda);

        }

        [AuthorizeUser(idOperacion: 2)]
        public ActionResult AsignarConfiguraciones(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var asignarConfiguraciones = db.AsignarConfiguracionTiendas.Where(t => t.TiendaId == id).OrderBy(t => t.TiendaConfiguracion.TipoConfiguracionId).ThenBy(t => t.TiendaConfiguracionId).ToList();

            if (asignarConfiguraciones == null)
            {
                return HttpNotFound();
            }

            ViewBag.Tienda = db.Tiendas.Where(t => t.TiendaId == id).FirstOrDefault().Restaurante;

            //=================================================ASIGNAR TODO=============================
            //var tiendas = db.Tiendas.ToList();

            //foreach (var tienda in tiendas)
            //{
            //    var tiendaConfiguracion = db.TiendaConfiguracions.ToList();

            //    foreach (var asignar in tiendaConfiguracion)
            //    {
            //        var configuraciones = new AsignarConfiguracionTienda
            //        {
            //            Seleccionado = false,
            //            TiendaId = tienda.TiendaId,
            //            TiendaConfiguracionId = asignar.TiendaConfiguracionId
            //        };

            //        db.AsignarConfiguracionTiendas.Add(configuraciones);
            //        db.SaveChanges();
            //    }

            //}

            return PartialView(asignarConfiguraciones);
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarConfiguraciones(FormCollection fc)
        {
            var nombre = User.Identity.Name;
            var usuarioActual = db.Usuarios.Where(u => u.NombreUsuario == nombre).FirstOrDefault();

            string[] tiendaConfiguracionId = fc.GetValues("TiendaConfiguracionId");
            string[] seleccionado = fc.GetValues("Seleccionado");

            var selec = false;

            for (var i = 0; i < tiendaConfiguracionId.Length; i++)
            {
                AsignarConfiguracionTienda configuracionTienda = db.AsignarConfiguracionTiendas.Find(Convert.ToInt32(tiendaConfiguracionId[i]));

                var tiendaId = configuracionTienda.TiendaId;
                var configuracioTiendaId = configuracionTienda.AsignarConfiguracionTiendaId;

                selec = false;

                if (seleccionado == null)
                {
                    selec = false;

                    configuracionTienda.Seleccionado = selec;

                    db.Entry(configuracionTienda).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    for (var j = 0; j < seleccionado.Length; j++)
                    {
                        if (tiendaConfiguracionId[i] == seleccionado[j])
                        {
                            selec = true;

                            configuracionTienda.Seleccionado = selec;

                            db.Entry(configuracionTienda).State = EntityState.Modified;
                            db.SaveChanges();

                            break;
                        }
                    }
                    if (!selec)
                    {
                        selec = false;

                        configuracionTienda.Seleccionado = selec;

                        db.Entry(configuracionTienda).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            TempData["mensajeLito"] = "CONFIGURACIONES ASIGNADAS";

            return RedirectToAction("Index");

        }

        // GET: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult AsignarArticulos(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tiendaArticulos = db.TiendaArticulos.Where(t => t.TiendaId == id).OrderBy(t => t.ArticuloKFC.Familia.Codigo).ThenBy(t => t.ArticuloKFCId).ToList();

            if (tiendaArticulos == null)
            {
                return HttpNotFound();
            }


            ViewBag.Tienda = db.Tiendas.Where(t => t.TiendaId == id).FirstOrDefault().Restaurante;

            return PartialView(tiendaArticulos);
        }

        private bool Update(TiendaArticulo product)
        {
            return true;
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AsignarArticulos(FormCollection fc)
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

                CampañaArticuloTMP campañaArticulo = db.CampañaArticuloTMPs.Where(ta => ta.TiendaId == tiendaId && ta.ArticuloKFCId == articuloId && ta.CampañaId == campañas).FirstOrDefault();

                selec = false;
                cantidad = 0;

                if (campañaArticulo != null)
                {
                    if (seleccionado == null)
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
                    else
                    {
                        for (var j = 0; j < seleccionado.Length; j++)
                        {
                            if (articuloKFCTMPId[i] == seleccionado[j])
                            {
                                selec = true;

                                tiendaArticulo.Seleccionado = selec;

                                db.Entry(tiendaArticulo).State = EntityState.Modified;

                                var articuloCantidadDefault = db.ArticuloKFCs.Where(a => a.ArticuloKFCId == campañaArticulo.ArticuloKFCId).FirstOrDefault().CantidadDefault;

                                cantidad = articuloCantidadDefault;
                                campañaArticulo.Cantidad = cantidad;
                                campañaArticulo.Habilitado = selec;

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
                }

            }

            TempData["mensajeLito"] = "MATERIALES ASIGNADOS";

            return RedirectToAction("Index");

        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        public JsonResult Editar(int? id, bool? sel)
        {
            var result = false;
            try
            {
                TiendaArticulo tiendaArticulo = db.TiendaArticulos.SingleOrDefault(x => x.TiendaArticuloId == id);

                tiendaArticulo.ArticuloKFC = db.TiendaArticulos.Where(a => a.TiendaArticuloId == id).FirstOrDefault().ArticuloKFC;
                tiendaArticulo.ArticuloKFCId = db.TiendaArticulos.Where(a => a.TiendaArticuloId == id).FirstOrDefault().ArticuloKFCId;
                tiendaArticulo.TiendaId = db.TiendaArticulos.Where(a => a.TiendaArticuloId == id).FirstOrDefault().TiendaId;
                tiendaArticulo.Tienda = db.TiendaArticulos.Where(a => a.TiendaArticuloId == id).FirstOrDefault().Tienda;
                tiendaArticulo.Seleccionado = (bool)sel;
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult ConfiguracionesGenerales(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            //int tipo = 0;

            //if (tienda.EquityFranquicia == "EQUITY")
            //{
            //    tipo = 1;
            //}
            //else if (tienda.EquityFranquicia == "FRANQUICIAS")
            //{
            //    tipo = 2;
            //}
            //else if (tienda.EquityFranquicia == "STOCK")
            //{
            //    tipo = 3;
            //}

            //ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(tipo, true), "CiudadId", "Nombre", tienda.CiudadId);
            //ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(tipo, true), "RegionId", "Nombre", tienda.RegionId);
            ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(), "TipoTiendaId", "Tipo", tienda.TipoId);
            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // POST: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfiguracionesGenerales(Tienda tienda)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(tienda).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    //var response2 = MovementsHelper.AgregarTiendaArticulos(tienda.TiendaId);

                    //if (response2.Succeeded)
                    //{
                    //    //TempData["mensajeLito"] = "CAMPAÑA AGREGADA";
                    //}


                    TempData["mensajeLito"] = "CARACTERISTICAS AGREGADAS";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            //ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(true), "CiudadId", "Nombre", tienda.CiudadId);
            //ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(true), "RegionId", "Nombre", tienda.RegionId);
            ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);
            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // GET: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult ConfiguracionesProducto(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            //int tipo = 0;

            //if (tienda.EquityFranquicia == "EQUITY")
            //{
            //    tipo = 1;
            //}
            //else if (tienda.EquityFranquicia == "FRANQUICIAS")
            //{
            //    tipo = 2;
            //}
            //else if (tienda.EquityFranquicia == "STOCK")
            //{
            //    tipo = 3;
            //}

            //ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(tipo, true), "CiudadId", "Nombre", tienda.CiudadId);
            //ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(tipo, true), "RegionId", "Nombre", tienda.RegionId);
            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(), "TipoTiendaId", "Tipo", tienda.TipoId);
            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // POST: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfiguracionesProducto(Tienda tienda)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(tienda).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    //var response2 = MovementsHelper.AgregarTiendaArticulos(tienda.TiendaId);

                    //if (response2.Succeeded)
                    //{
                    //    //TempData["mensajeLito"] = "CAMPAÑA AGREGADA";
                    //}


                    TempData["mensajeLito"] = "CARACTERISTICAS AGREGADAS";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            //ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(true), "CiudadId", "Nombre", tienda.CiudadId);
            //ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(true), "RegionId", "Nombre", tienda.RegionId);
            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);
            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // GET: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult ConfiguracionesCME(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            //int tipo = 0;

            //if (tienda.EquityFranquicia == "EQUITY")
            //{
            //    tipo = 1;
            //}
            //else if (tienda.EquityFranquicia == "FRANQUICIAS")
            //{
            //    tipo = 2;
            //}
            //else if (tienda.EquityFranquicia == "STOCK")
            //{
            //    tipo = 3;
            //}

            //ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(tipo, true), "CiudadId", "Nombre", tienda.CiudadId);
            //ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(tipo, true), "RegionId", "Nombre", tienda.RegionId);
            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(), "TipoTiendaId", "Tipo", tienda.TipoId);
            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // POST: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfiguracionesCME(Tienda tienda)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(tienda).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    //var response2 = MovementsHelper.AgregarTiendaArticulos(tienda.TiendaId);

                    //if (response2.Succeeded)
                    //{
                    //    //TempData["mensajeLito"] = "CAMPAÑA AGREGADA";
                    //}


                    TempData["mensajeLito"] = "CARACTERISTICAS AGREGADAS";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            //ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(true), "CiudadId", "Nombre", tienda.CiudadId);
            //ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(true), "RegionId", "Nombre", tienda.RegionId);
            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);
            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // GET: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult ConfiguracionesCMES(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            //int tipo = 0;

            //if (tienda.EquityFranquicia == "EQUITY")
            //{
            //    tipo = 1;
            //}
            //else if (tienda.EquityFranquicia == "FRANQUICIAS")
            //{
            //    tipo = 2;
            //}
            //else if (tienda.EquityFranquicia == "STOCK")
            //{
            //    tipo = 3;
            //}

            //ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(tipo, true), "CiudadId", "Nombre", tienda.CiudadId);
            //ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(tipo, true), "RegionId", "Nombre", tienda.RegionId);
            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(), "TipoTiendaId", "Tipo", tienda.TipoId);
            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // POST: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfiguracionesCMES(Tienda tienda)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(tienda).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    //var response2 = MovementsHelper.AgregarTiendaArticulos(tienda.TiendaId);

                    //if (response2.Succeeded)
                    //{
                    //    //TempData["mensajeLito"] = "CAMPAÑA AGREGADA";
                    //}


                    TempData["mensajeLito"] = "CARACTERISTICAS AGREGADAS";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            //ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(true), "CiudadId", "Nombre", tienda.CiudadId);
            //ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(true), "RegionId", "Nombre", tienda.RegionId);
            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);
            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // GET: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult ConfiguracionesCER(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            //int tipo = 0;

            //if (tienda.EquityFranquicia == "EQUITY")
            //{
            //    tipo = 1;
            //}
            //else if (tienda.EquityFranquicia == "FRANQUICIAS")
            //{
            //    tipo = 2;
            //}
            //else if (tienda.EquityFranquicia == "STOCK")
            //{
            //    tipo = 3;
            //}

            //ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(tipo, true), "CiudadId", "Nombre", tienda.CiudadId);
            //ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(tipo, true), "RegionId", "Nombre", tienda.RegionId);
            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(), "TipoTiendaId", "Tipo", tienda.TipoId);
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // POST: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfiguracionesCER(Tienda tienda)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(tienda).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    //var response2 = MovementsHelper.AgregarTiendaArticulos(tienda.TiendaId);

                    //if (response2.Succeeded)
                    //{
                    //    //TempData["mensajeLito"] = "CAMPAÑA AGREGADA";
                    //}


                    TempData["mensajeLito"] = "CARACTERISTICAS AGREGADAS";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            //ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(true), "CiudadId", "Nombre", tienda.CiudadId);
            //ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(true), "RegionId", "Nombre", tienda.RegionId);
            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);
            ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // GET: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Edit(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            int tipo = 0;

            if (tienda.EquityFranquicia == "EQUITY")
            {
                tipo = 1;
            }
            else if (tienda.EquityFranquicia == "FRANQUICIAS")
            {
                tipo = 2;
            }
            else if (tienda.EquityFranquicia == "STOCK")
            {
                tipo = 3;
            }

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(tipo, true), "CiudadId", "Nombre", tienda.CiudadId);
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(tipo, true), "RegionId", "Nombre", tienda.RegionId);
            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);
            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // POST: Tiendas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tienda tienda)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(tienda).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    //MovementsHelper.AgregarTiendaArticulosTodo();

                    var response2 = MovementsHelper.AgregarTiendaArticulos(tienda.TiendaId);

                    if (response2.Succeeded)
                    {
                        //TempData["mensajeLito"] = "CAMPAÑA AGREGADA";
                    }


                    TempData["mensajeLito"] = "RESTAURANTE EDITADO";

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(true), "CiudadId", "Nombre", tienda.CiudadId);
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(true), "RegionId", "Nombre", tienda.RegionId);
            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);
            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // GET: Tiendas/Delete/5
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Delete(int? id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tienda = db.Tiendas.Find(id);

            if (tienda == null)
            {
                return HttpNotFound();
            }

            ViewBag.CiudadId = new SelectList(CombosHelper.GetCiudades(true), "CiudadId", "Nombre", tienda.CiudadId);
            ViewBag.RegionId = new SelectList(CombosHelper.GetRegiones(true), "RegionId", "Nombre", tienda.RegionId);
            //ViewBag.NuevoNivelDePrecioId = new SelectList(CombosHelper.GetNivelesPrecio(true), "NivelPrecioId", "Descripcion", tienda.NuevoNivelDePrecioId);
            //ViewBag.TipoId = new SelectList(CombosHelper.GetTiposTienda(true), "TipoTiendaId", "Tipo", tienda.TipoId);
            //ViewBag.AcomodoDeCajas = new SelectList(CombosHelper.GetAcomodoCajas(true), "Descripcion", "Descripcion", tienda.AcomodoDeCajas);
            //ViewBag.TipoDeCajaId = new SelectList(CombosHelper.GetTiposCaja(true), "TipoCajaId", "Descripcion", tienda.TipoDeCajaId);
            //ViewBag.FamiliaId = new SelectList(CombosHelper.GetFamilias(true), "FamiliaId", "Codigo", tienda.FamiliaId);

            return PartialView(tienda);
        }

        // POST: Tiendas/Delete/5
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tienda = db.Tiendas.Find(id);
            db.Tiendas.Remove(tienda);
            db.SaveChanges();

            TempData["mensajeLito"] = "RESTAURANTE ELIMINADO";

            return RedirectToAction("Index");
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
