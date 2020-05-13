using CampaniasLito.Classes;
using CampaniasLito.Filters;
using CampaniasLito.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CampaniasLito.Controllers
{
    public class MaterialesTotales
    {
        public string ArticuloKFC { get; set; }
        public string Campaña { get; set; }
        public double Cantidad { get; set; }
        public string Proveedor { get; set; }
        public string Imagen { get; set; }
        public int TiendaId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public double TotalCantidad { get; set; }

    }

    public class MaterialesTotalesOrdenes
    {
        public int ArticuloKFCId { get; set; }
        public string Campaña { get; set; }
        public int TiendaId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public double Cantidad { get; set; }

    }

    public class MaterialesCampaña
    {
        public int ArticuloKFCId { get; set; }
        public string ArticuloKFC { get; set; }
        public string Campaña { get; set; }
        public double Cantidad { get; set; }
        public int TiendaId { get; set; }
        public bool Habilitado { get; set; }
    }

    public class TiendasCampaña
    {
        public int TiendaId { get; set; }
        public string Restaurante { get; set; }
        public string Clasificacion { get; set; }
        public string CC { get; set; }
        public string TipoTienda { get; set; }
        public string Region { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        //00000000000000000000000000 GENERALES 0000000000000000000000000000000000
        public string Tipo { get; set; }
        public string NuevoNivelDePrecio { get; set; }
        public bool MenuDigital { get; set; }
        public string CantidadDePantallas { get; set; }
        //00000000000000000000000000 POR PRODUCTO 0000000000000000000000000000000000
        public bool TerceraReceta { get; set; }
        public bool Arroz { get; set; }
        public bool Hamburgesas { get; set; }
        public bool Ensalada { get; set; }
        public bool PET2Litros { get; set; }
        public bool Postres { get; set; }
        public bool BisquetMiel { get; set; }
        public bool KeCono { get; set; }
        public bool KREAMBALL { get; set; }

        //00000000000000000000000000 MATERIALES ESPECIFICOS 0000000000000000000000000000000000

        public bool MenuBackLigth { get; set; }
        public bool Autoexpress { get; set; }
        public bool CopeteAERemodelado { get; set; }
        public bool CopeteAETradicional { get; set; }
        public bool PanelDeInnovacion { get; set; }
        public bool DisplayDeBurbuja { get; set; }
        public bool Delivery { get; set; }
        public bool MERCADO_DE_PRUEBA { get; set; }
        public bool AreaDeJuegos { get; set; }
        public bool COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS { get; set; }
        public bool COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L { get; set; }
        public bool DisplayDePiso { get; set; }
        public bool WCNACIONAL67X100cm { get; set; }

        //00000000000000000000000000 MEDIDAS ESPECIALES 0000000000000000000000000000000000

        public bool WCMedidaEspecial60_8x85cm { get; set; }
        public bool WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm { get; set; }
        public bool WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm { get; set; }
        public bool WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm { get; set; }
        public bool WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm { get; set; }
        public bool MedidaEspecialPanelDeComplementos { get; set; }
        public bool MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm { get; set; }


        //00000000000000000000000000 POR EQUIPO EN EL RESTAURANTE 0000000000000000000000000000000000

        public string TipoDeCaja { get; set; }
        public string AcomodoDeCajas { get; set; }
        public string NoMesaDeAreaComedor { get; set; }
        public string NoMesaDeAreaDeJuegos { get; set; }
        public string NumeroDeVentanas { get; set; }
        public string UbicacionPantallas { get; set; }

    }

    public class MaterialesTiendasCampaña
    {
        public int ArticuloKFCId { get; set; }
        public string ArticuloKFC { get; set; }
        public string Campaña { get; set; }
        public double Cantidad { get; set; }
        public int TiendaId { get; set; }
        public string Restaurante { get; set; }
        public string Clasificacion { get; set; }
        public string CC { get; set; }
        public string Region { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public bool Habilitado { get; set; }
        public string TipoTienda { get; set; }
        public string Tipo { get; set; }
        public string NuevoNivelDePrecio { get; set; }
        public bool MenuDigital { get; set; }
        public string CantidadDePantallas { get; set; }
        public bool TerceraReceta { get; set; }
        public bool Arroz { get; set; }
        public bool Hamburgesas { get; set; }
        public bool Ensalada { get; set; }
        public bool PET2Litros { get; set; }
        public bool Postres { get; set; }
        public bool BisquetMiel { get; set; }
        public bool KeCono { get; set; }
        public bool KREAMBALL { get; set; }
        public bool MenuBackLigth { get; set; }
        public bool Autoexpress { get; set; }
        public bool CopeteAERemodelado { get; set; }
        public bool CopeteAETradicional { get; set; }
        public bool PanelDeInnovacion { get; set; }
        public bool DisplayDeBurbuja { get; set; }
        public bool Delivery { get; set; }
        public bool MERCADO_DE_PRUEBA { get; set; }
        public bool AreaDeJuegos { get; set; }
        public bool COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS { get; set; }
        public bool COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L { get; set; }
        public bool DisplayDePiso { get; set; }
        public bool WCNACIONAL67X100cm { get; set; }
        public bool WCMedidaEspecial60_8x85cm { get; set; }
        public bool WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm { get; set; }
        public bool WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm { get; set; }
        public bool WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm { get; set; }
        public bool WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm { get; set; }
        public bool MedidaEspecialPanelDeComplementos { get; set; }
        public bool MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm { get; set; }
        public string TipoDeCaja { get; set; }
        public string AcomodoDeCajas { get; set; }
        public string NoMesaDeAreaComedor { get; set; }
        public string NoMesaDeAreaDeJuegos { get; set; }
        public string NumeroDeVentanas { get; set; }
        public string UbicacionPantallas { get; set; }
    }

    public class MaterialTotal
    {
        public int ArticuloKFCId { get; set; }
        public string ArticuloKFC { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public double TotalCantidad { get; set; }

    }

    public class TiendaTotal
    {
        public string Restaurante { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public double TotalCantidad { get; set; }

    }

    public class CodigosMateriales
    {
        public string ArticuloKFC { get; set; }
        public string Campaña { get; set; }
        public double Codigo { get; set; }
    }

    public class CodigosMaterialesOrdenes
    {
        public int ArticuloKFCId { get; set; }
        public string Campaña { get; set; }
        public double Codigo { get; set; }
    }

    public class CodigosMaterialesTotal
    {
        public string ArticuloKFC { get; set; }
        public string Campaña { get; set; }
        public double Codigo { get; set; }
        public string Proveedor { get; set; }
        public string Imagen { get; set; }
        public string TipoTienda { get; set; }
        public int TiendaId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public double TotalCantidad { get; set; }

    }

    public class CodigosMaterialesTotalOrdenes
    {
        public string ArticuloKFC { get; set; }
        public int ArticuloKFCId { get; set; }
        public string Campaña { get; set; }
        public double Codigo { get; set; }
        public string TipoTienda { get; set; }
        public int TiendaId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public double Cantidad { get; set; }

    }

    public class CodigosMaterialesTienda
    {
        public string ArticuloKFC { get; set; }
        public string Campaña { get; set; }
        public double Codigo { get; set; }
        public string Proveedor { get; set; }
        public string Imagen { get; set; }
        public string TipoTienda { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public double TotalCantidad { get; set; }

    }

    public class ProveedoresTotal
    {
        public string Proveedor { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public double TotalCantidad { get; set; }

    }

    public class TipoTiendaTotal
    {
        public string ArticuloKFC { get; set; }
        public string TipoTienda { get; set; }
        public double TotalCantidadTipo { get; set; }

    }

    public class CampañasController : Controller
    {
        private readonly CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Campañas
        public ActionResult GetList()
        {
            var campañasList = db.Campañas.ToList<Campaña>();
            return Json(new { data = campañasList }, JsonRequestBehavior.AllowGet);

        }

        [AuthorizeUser(idOperacion: 5)]
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

            var campañas = db.Campañas;

            Session["homeB"] = string.Empty;
            Session["rolesB"] = string.Empty;
            Session["compañiasB"] = string.Empty;
            Session["usuariosB"] = string.Empty;
            Session["regionesB"] = string.Empty;
            Session["ciudadesB"] = string.Empty;
            Session["restaurantesB"] = string.Empty;
            Session["familiasB"] = string.Empty;
            Session["materialesB"] = string.Empty;
            Session["campañasB"] = "active";

            if (!string.IsNullOrEmpty(campaña))
            {
                return View(campañas.Where(a => a.Nombre.Contains(filtro) || a.Generada.Contains(filtro) || a.Descripcion.Contains(filtro)).ToList());
            }
            else
            {
                return View(campañas.ToList());
            }

        }

        [AuthorizeUser(idOperacion: 5)]
        public ActionResult ResumenProveedor(int id)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var campaña = db.Campañas.Where(x => x.CampañaId == id).FirstOrDefault();

            var materialesCampaña = db.CampañaArticuloTMPs
                                   .Where(x => x.CampañaId == id)
                                   .GroupBy(x => new
                                   {
                                       x.ArticuloKFC.Descripcion,
                                       x.CampañaId,
                                       x.Cantidad,
                                       x.ArticuloKFC.Proveedor.Nombre,
                                       x.ArticuloKFC.Imagen,
                                       x.TiendaId,
                                   })
                                   .Select(x => new MaterialesTotales()
                                   {
                                       Campaña = campaña.Nombre,
                                       ArticuloKFC = x.Key.Descripcion,
                                       Imagen = x.Key.Imagen,
                                       Cantidad = x.Key.Cantidad,
                                       Proveedor = x.Key.Nombre,
                                       TiendaId = x.Key.TiendaId,
                                       TotalCantidad = x.Sum(t => t.Cantidad),
                                   });

            var tiendas = db.Tiendas
                            .GroupBy(x => new { x.TiendaId, x.EquityFranquicia })
                            .Select(x => new TiendasCampaña() { TiendaId = x.Key.TiendaId, TipoTienda = x.Key.EquityFranquicia });

            var materialesTipoTienda = tiendas.Join(materialesCampaña,
                                 tien => tien.TiendaId,
                                 mat => mat.TiendaId,
                                 (tien, mat) => new { tiendas = tien, materiales = mat })
                            .Where(x => x.tiendas.TiendaId == x.materiales.TiendaId)
                            .GroupBy(x => new
                            {
                                x.tiendas.TiendaId,
                                x.tiendas.TipoTienda,
                                x.materiales.Proveedor,
                                x.materiales.Imagen,
                                x.materiales.ArticuloKFC,
                                x.materiales.Campaña
                            })
                            .Select(x => new CodigosMaterialesTienda()
                            {
                                ArticuloKFC = x.Key.ArticuloKFC,
                                Campaña = x.Key.Campaña,
                                Proveedor = x.Key.Proveedor,
                                TipoTienda = x.Key.TipoTienda,
                                TotalCantidad = x.Sum(d => d.materiales.TotalCantidad),
                            });

            var totalTipoTienda = materialesTipoTienda
                               .GroupBy(x => new { x.TipoTienda, x.ArticuloKFC })
                               .Select(x => new TipoTiendaTotal()
                               {
                                   ArticuloKFC = x.Key.ArticuloKFC,
                                   TipoTienda = x.Key.TipoTienda,
                                   TotalCantidadTipo = x.Sum(t => t.TotalCantidad),
                               });

            ViewBag.TotalTiendaTipo = totalTipoTienda.ToList();


            var codigosCampaña = db.CodigosCampaña
                            .Where(x => x.CampañaId == id)
                            .GroupBy(x => new { x.ArticuloKFC.Descripcion, x.Codigo, x.CampañaId })
                            .Select(x => new CodigosMateriales()
                            {
                                ArticuloKFC = x.Key.Descripcion,
                                Campaña = campaña.Nombre,
                                Codigo = x.Key.Codigo,
                            });

            var materiales = codigosCampaña.Join(materialesCampaña,
                                 cali => cali.ArticuloKFC,
                                 caliInt => caliInt.ArticuloKFC,
                                 (cali, caliInt) => new { codigos = cali, materiales = caliInt })
                            .Where(x => x.codigos.ArticuloKFC == x.materiales.ArticuloKFC)
                            .GroupBy(x => new { x.codigos.ArticuloKFC, x.codigos.Campaña, x.codigos.Codigo, x.materiales.Proveedor, x.materiales.Imagen })
                            .Select(x => new CodigosMaterialesTotal()
                            {
                                ArticuloKFC = x.Key.ArticuloKFC,
                                Imagen = x.Key.Imagen,
                                Campaña = x.Key.Campaña,
                                Codigo = x.Key.Codigo,
                                Proveedor = x.Key.Proveedor,
                                TotalCantidad = x.Sum(d => d.materiales.TotalCantidad),
                            });


            var totalProv = materiales
                               .GroupBy(x => new { x.Proveedor })
                               .Select(x => new ProveedoresTotal()
                               {
                                   Proveedor = x.Key.Proveedor,
                                   TotalCantidad = x.Sum(t => t.TotalCantidad),
                               });

            ViewBag.TotalProveedor = totalProv.ToList();

            var totalMateriales = materiales.ToList().OrderBy(p => p.Proveedor).ThenBy(p => p.ArticuloKFC);

            return View(totalMateriales);
        }

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 4)]
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

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 2)]
        public async Task<ActionResult> CreateCampArt(int? id, int tipo)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tipoTienda = string.Empty;

            if (tipo == 1)
            {
                tipoTienda = "EQUITY";
            }
            else if (tipo == 2)
            {
                tipoTienda = "FRANQUICIAS";
            }
            else if (tipo == 3)
            {
                tipoTienda = "STOCK";
            }

            var campaña = db.Campañas.Where(x => x.CampañaId == id).FirstOrDefault();

            var campañaId = id;

            var articulosTMP = await db.CampañaArticuloTMPs
                                   .Where(x => x.CampañaId == campañaId)
                                   .GroupBy(x => new
                                   {
                                       x.ArticuloKFCId,
                                       x.ArticuloKFC.Descripcion,
                                       x.CampañaId,
                                       x.Cantidad,
                                       x.TiendaId,
                                       x.Habilitado,
                                   })
                                   .Select(x => new MaterialesCampaña()
                                   {
                                       ArticuloKFCId = x.Key.ArticuloKFCId,
                                       Campaña = campaña.Nombre + " / " + campaña.Descripcion,
                                       ArticuloKFC = x.Key.Descripcion,
                                       Cantidad = x.Key.Cantidad,
                                       TiendaId = x.Key.TiendaId,
                                       Habilitado = x.Key.Habilitado,
                                   }).ToListAsync();

            var misMateriales = articulosTMP.ToList();

            var tiendasCampaña = await db.Tiendas
                            .Where(x => x.EquityFranquicia == tipoTienda)
                            .GroupBy(x => new
                            {
                                x.Restaurante,
                                x.CCoFranquicia,
                                x.TiendaId,
                                x.TipoTienda.Tipo,
                                x.NivelPrecio.Descripcion,
                                x.MenuDigital,
                                x.CantidadDePantallas,
                                x.Clasificacion,
                                x.Region.Nombre,
                                NombreCiudad = x.Ciudad.Nombre,
                                x.Direccion,
                                x.TerceraReceta,
                                x.Arroz,
                                x.Hamburgesas,
                                x.Ensalada,
                                x.PET2Litros,
                                x.Postres,
                                x.BisquetMiel,
                                x.KeCono,
                                x.KREAMBALL,
                                x.MenuBackLigth,
                                x.Autoexpress,
                                x.CopeteAERemodelado,
                                x.CopeteAETradicional,
                                x.PanelDeInnovacion,
                                x.DisplayDeBurbuja,
                                x.Delivery,
                                x.MERCADO_DE_PRUEBA,
                                x.AreaDeJuegos,
                                x.COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS,
                                x.COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L,
                                x.DisplayDePiso,
                                x.WCNACIONAL67X100cm,
                                x.WCMedidaEspecial60_8x85cm,
                                x.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm,
                                x.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm,
                                x.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm,
                                x.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm,
                                x.MedidaEspecialPanelDeComplementos,
                                x.MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm,
                                x.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm,
                                x.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm,
                                x.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm,
                                x.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm,
                                x.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm,
                                x.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm,
                                x.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm,
                                x.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm,
                                TipoDeCajaNombre = x.TipoDeCaja.Descripcion,
                                x.AcomodoDeCajas,
                                x.NoMesaDeAreaComedor,
                                x.NoMesaDeAreaDeJuegos,
                                x.NumeroDeVentanas,
                                x.UbicacionPantallas,
                                x.Observaciones
                            })
                            .Select(x => new TiendasCampaña()
                            {
                                Restaurante = x.Key.Restaurante,
                                TiendaId = x.Key.TiendaId,
                                Clasificacion = x.Key.Clasificacion,
                                Region = x.Key.Nombre,
                                Ciudad = x.Key.NombreCiudad,
                                Direccion = x.Key.Direccion,
                                CC = x.Key.CCoFranquicia,
                                TipoTienda = tipoTienda,
                                CantidadDePantallas = x.Key.CantidadDePantallas,
                                MenuDigital = x.Key.MenuDigital,
                                Tipo = x.Key.Tipo,
                                NuevoNivelDePrecio = x.Key.Descripcion,
                                TerceraReceta = x.Key.TerceraReceta,
                                Arroz = x.Key.Arroz,
                                Hamburgesas = x.Key.Hamburgesas,
                                Ensalada = x.Key.Ensalada,
                                PET2Litros = x.Key.PET2Litros,
                                Postres = x.Key.Postres,
                                BisquetMiel = x.Key.BisquetMiel,
                                KeCono = x.Key.KeCono,
                                KREAMBALL = x.Key.KREAMBALL,
                                AcomodoDeCajas = x.Key.AcomodoDeCajas,
                                AreaDeJuegos = x.Key.AreaDeJuegos,
                                Autoexpress = x.Key.Autoexpress,
                                CopeteAERemodelado = x.Key.CopeteAERemodelado,
                                CopeteAETradicional = x.Key.CopeteAETradicional,
                                UbicacionPantallas = x.Key.UbicacionPantallas,
                                NumeroDeVentanas = x.Key.NumeroDeVentanas,
                                NoMesaDeAreaDeJuegos = x.Key.NoMesaDeAreaDeJuegos,
                                COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS = x.Key.COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS,
                                COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L = x.Key.COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L,
                                Delivery = x.Key.Delivery,
                                DisplayDeBurbuja = x.Key.DisplayDeBurbuja,
                                DisplayDePiso = x.Key.DisplayDePiso,
                                MedidaEspecialPanelDeComplementos = x.Key.MedidaEspecialPanelDeComplementos,
                                MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm = x.Key.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm,
                                MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm = x.Key.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm,
                                MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm = x.Key.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm,
                                MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm = x.Key.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm,
                                MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm = x.Key.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm,
                                MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm = x.Key.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm,
                                MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm = x.Key.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm,
                                MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm = x.Key.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm,
                                MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm = x.Key.MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm,
                                MenuBackLigth = x.Key.MenuBackLigth,
                                MERCADO_DE_PRUEBA = x.Key.MERCADO_DE_PRUEBA,
                                NoMesaDeAreaComedor = x.Key.NoMesaDeAreaComedor,
                                PanelDeInnovacion = x.Key.PanelDeInnovacion,
                                TipoDeCaja = x.Key.TipoDeCajaNombre,
                                WCMedidaEspecial60_8x85cm = x.Key.WCMedidaEspecial60_8x85cm,
                                WCNACIONAL67X100cm = x.Key.WCNACIONAL67X100cm,
                                WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm = x.Key.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm,
                                WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm = x.Key.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm,
                                WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm = x.Key.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm,
                                WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm = x.Key.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm,
                            }).ToListAsync();

            var misTiendas = tiendasCampaña.ToList();

            var materialesCampaña = articulosTMP.Join(tiendasCampaña,
                                 artCamp => artCamp.TiendaId,
                                 tienCamp => tienCamp.TiendaId,
                                 (artCamp, tienCamp) => new { tiendas = tienCamp, materiales = artCamp })
                            .Where(x => x.tiendas.TiendaId == x.materiales.TiendaId)
                            .Where(x => x.tiendas.TipoTienda == tipoTienda)
                            .GroupBy(x => new
                            {
                                x.tiendas.Restaurante,
                                x.tiendas.Clasificacion,
                                x.tiendas.CC,
                                x.tiendas.Region,
                                x.tiendas.Ciudad,
                                x.tiendas.Direccion,
                                x.tiendas.TiendaId,
                                x.tiendas.TipoTienda,
                                x.tiendas.NuevoNivelDePrecio,
                                x.tiendas.MenuDigital,
                                x.tiendas.Tipo,
                                x.tiendas.CantidadDePantallas,
                                x.tiendas.TerceraReceta,
                                x.tiendas.Arroz,
                                x.tiendas.Hamburgesas,
                                x.tiendas.Ensalada,
                                x.tiendas.PET2Litros,
                                x.tiendas.Postres,
                                x.tiendas.BisquetMiel,
                                x.tiendas.KeCono,
                                x.tiendas.KREAMBALL,
                                x.tiendas.AcomodoDeCajas,
                                x.tiendas.AreaDeJuegos,
                                x.tiendas.Autoexpress,
                                x.tiendas.CopeteAERemodelado,
                                x.tiendas.CopeteAETradicional,
                                x.tiendas.COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS,
                                x.tiendas.COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L,
                                x.tiendas.Delivery,
                                x.tiendas.DisplayDeBurbuja,
                                x.tiendas.DisplayDePiso,
                                x.tiendas.MedidaEspecialPanelDeComplementos,
                                x.tiendas.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm,
                                x.tiendas.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm,
                                x.tiendas.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm,
                                x.tiendas.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm,
                                x.tiendas.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm,
                                x.tiendas.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm,
                                x.tiendas.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm,
                                x.tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm,
                                x.tiendas.MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm,
                                x.tiendas.MenuBackLigth,
                                x.tiendas.MERCADO_DE_PRUEBA,
                                x.tiendas.NoMesaDeAreaComedor,
                                x.tiendas.NoMesaDeAreaDeJuegos,
                                x.tiendas.NumeroDeVentanas,
                                x.tiendas.PanelDeInnovacion,
                                x.tiendas.TipoDeCaja,
                                x.tiendas.UbicacionPantallas,
                                x.tiendas.WCMedidaEspecial60_8x85cm,
                                x.tiendas.WCNACIONAL67X100cm,
                                x.tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm,
                                x.tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm,
                                x.tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm,
                                x.tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm,
                                x.materiales.ArticuloKFCId,
                                x.materiales.ArticuloKFC,
                                x.materiales.Cantidad,
                                x.materiales.Campaña,
                                x.materiales.Habilitado
                            })
                            .Select(x => new MaterialesTiendasCampaña()
                            {
                                ArticuloKFCId = x.Key.ArticuloKFCId,
                                ArticuloKFC = x.Key.ArticuloKFC,
                                Campaña = x.Key.Campaña,
                                Cantidad = x.Key.Cantidad,
                                CC = x.Key.CC,
                                Clasificacion = x.Key.Clasificacion,
                                Restaurante = x.Key.Restaurante,
                                Ciudad = x.Key.Ciudad,
                                Direccion = x.Key.Direccion,
                                Region = x.Key.Region,
                                TiendaId = x.Key.TiendaId,
                                TipoTienda = x.Key.TipoTienda,
                                Habilitado = x.Key.Habilitado,
                                CantidadDePantallas = x.Key.CantidadDePantallas,
                                MenuDigital = x.Key.MenuDigital,
                                NuevoNivelDePrecio = x.Key.NuevoNivelDePrecio,
                                Tipo = x.Key.Tipo,
                                TerceraReceta = x.Key.TerceraReceta,
                                Arroz = x.Key.Arroz,
                                Hamburgesas = x.Key.Hamburgesas,
                                Ensalada = x.Key.Ensalada,
                                PET2Litros = x.Key.PET2Litros,
                                Postres = x.Key.Postres,
                                BisquetMiel = x.Key.BisquetMiel,
                                KeCono = x.Key.KeCono,
                                KREAMBALL = x.Key.KREAMBALL,
                                AcomodoDeCajas = x.Key.AcomodoDeCajas,
                                AreaDeJuegos = x.Key.AreaDeJuegos,
                                Autoexpress = x.Key.Autoexpress,
                                CopeteAERemodelado = x.Key.CopeteAERemodelado,
                                CopeteAETradicional = x.Key.CopeteAETradicional,
                                UbicacionPantallas = x.Key.UbicacionPantallas,
                                NumeroDeVentanas = x.Key.NumeroDeVentanas,
                                NoMesaDeAreaDeJuegos = x.Key.NoMesaDeAreaDeJuegos,
                                COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS = x.Key.COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS,
                                COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L = x.Key.COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L,
                                Delivery = x.Key.Delivery,
                                DisplayDeBurbuja = x.Key.DisplayDeBurbuja,
                                DisplayDePiso = x.Key.DisplayDePiso,
                                MedidaEspecialPanelDeComplementos = x.Key.MedidaEspecialPanelDeComplementos,
                                MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm = x.Key.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm,
                                MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm = x.Key.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm,
                                MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm = x.Key.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm,
                                MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm = x.Key.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm,
                                MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm = x.Key.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm,
                                MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm = x.Key.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm,
                                MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm = x.Key.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm,
                                MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm = x.Key.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm,
                                MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm = x.Key.MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm,
                                MenuBackLigth = x.Key.MenuBackLigth,
                                MERCADO_DE_PRUEBA = x.Key.MERCADO_DE_PRUEBA,
                                NoMesaDeAreaComedor = x.Key.NoMesaDeAreaComedor,
                                PanelDeInnovacion = x.Key.PanelDeInnovacion,
                                TipoDeCaja = x.Key.TipoDeCaja,
                                WCMedidaEspecial60_8x85cm = x.Key.WCMedidaEspecial60_8x85cm,
                                WCNACIONAL67X100cm = x.Key.WCNACIONAL67X100cm,
                                WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm = x.Key.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm,
                                WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm = x.Key.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm,
                                WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm = x.Key.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm,
                                WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm = x.Key.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm,
                            });

            var misMaterialesCampaña = materialesCampaña.ToList();

            var totalMaterial = materialesCampaña
                               .Where(x => x.TipoTienda == tipoTienda)
                               .GroupBy(x => new { x.ArticuloKFC, x.ArticuloKFCId })
                               .Select(x => new MaterialTotal()
                               {
                                   ArticuloKFCId = x.Key.ArticuloKFCId,
                                   ArticuloKFC = x.Key.ArticuloKFC,
                                   TotalCantidad = x.Sum(t => t.Cantidad),
                               });

            ViewBag.TotalMaterial = totalMaterial.OrderBy(x => x.ArticuloKFC).ToList();

            ViewBag.Total = totalMaterial.Sum(x => x.TotalCantidad);

            var totalRestaurante = materialesCampaña
                               .GroupBy(x => new { x.Restaurante })
                               .Select(x => new TiendaTotal()
                               {
                                   Restaurante = x.Key.Restaurante,
                                   TotalCantidad = x.Sum(t => t.Cantidad),
                               });

            ViewBag.TotalRestaurante = totalRestaurante.ToList();

            var totalMateriales = materialesCampaña.Where(x => x.TipoTienda == tipoTienda).ToList().OrderBy(p => p.TiendaId).ThenBy(p => p.ArticuloKFC).ToList();

            if (totalMateriales.Count == 0)
            {
                TempData["mensajeLito"] = "Aún no hay cantidades para la Campaña";

                return RedirectToAction("Index");
            }

            return PartialView(totalMateriales.ToList());

        }

        //// GET: Campañas/Details/5
        //[AuthorizeUser(idOperacion: 2)]
        //public ActionResult CreateCampArt(string tiendaCampania, int? id, int? page = null)
        //{
        //    var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

        //    if (usuario == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    if (string.IsNullOrEmpty(tiendaCampania))
        //    {
        //        Session["tiendaCampaniaFiltro"] = string.Empty;
        //    }
        //    else
        //    {
        //        Session["tiendaCampaniaFiltro"] = tiendaCampania.ToUpper();
        //    }

        //    var filtro = Session["tiendaCampaniaFiltro"].ToString();

        //    var tiendas = db.Tiendas.ToList();

        //    page = (page ?? 1);

        //    //List<TiendaArticulo> campañaArticulosTiendas = db.TiendaArticulos.ToList();

        //    List<CampañaArticuloTMP> campañaArticulosTiendas = db.CampañaArticuloTMPs.ToList();

        //    List<CampañaTiendaTMP> campañaTiendas = db.CampañaTiendaTMPs.Where(ct => ct.CampañaId == id).ToList();

        //    var campañas = db.Campañas.Where(ct => ct.Generada == "NO").OrderBy(ct => ct.CampañaId).FirstOrDefault().CampañaId;

        //    if (campañaArticulosTiendas == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    Session["campaniaId"] = id;

        //    if (campañas < id)
        //    {
        //        TempData["mensajeLito"] = "HAY UNA CAMPAÑA EN PROCESO";
        //        return RedirectToAction("Index");
        //    }

        //    //List<CampañaArticuloTMP> articulosTMPsId = db.CampañaArticuloTMPs.Where(cdt => cdt.CampañaId== id && cdt.Habilitado == true).ToList();

        //    //if (articulosTMPsId.Count != campañaArticulosTiendas.Count)
        //    //{

        //    //TODO: Asignacion de articulos por tienda

        //    //foreach (var item in tiendas)
        //    //{
        //    //    var response = MovementsHelper.AgregarArticulosTiendas(usuario.Compañia.CompañiaId, usuario.NombreUsuario, item.TiendaId, (int)id);

        //    //    if (!response.Succeeded)
        //    //    {
        //    //        TempData["mensajeLito"] = "ERROR AL AGREGAR ARTICULOS A LA CAMPAÑA";
        //    //        return RedirectToAction("Index");
        //    //    }
        //    //}


        //    //}

        //    var tiendasTMPs = db.CampañaTiendaTMPs.Where(cdt => cdt.CampañaId == id).ToList();

        //    if (tiendasTMPs.Count != tiendas.Count)
        //    {
        //        //foreach (var item in tiendas)
        //        //{
        //        var response = MovementsHelper.AgregarTiendas((int)id);

        //        if (!response.Succeeded)
        //        {
        //            TempData["mensajeLito"] = "ERROR AL AGREGAR ARTICULOS A LA CAMPAÑA";
        //            return RedirectToAction("Index");
        //        }
        //        //}
        //    }

        //    var numeroPaginas = 15;

        //    if (!string.IsNullOrEmpty(tiendaCampania))
        //    {
        //        return PartialView(campañaTiendas.Where(a => a.Tienda.Restaurante.Contains(filtro)).ToPagedList((int)page, numeroPaginas));
        //    }
        //    else
        //    {
        //        return PartialView(campañaTiendas.ToPagedList((int)page, numeroPaginas));
        //    }



        //    //ViewBag.TotalPrice = totalImporte.ToString("C2");
        //    //var totalImporte = detalleCampaña.Sum(m => m.Importe);
        //    //ViewBag.FechaCotizacion = cotizacion.FechaCotizacion;
        //    //ViewBag.Detalles = detalleCotizaciones;
        //    //ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NombreCliente", cotizacion.ClienteId);
        //    //ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Descripcion", cotizacion.StatusId);
        //    //return View(cotizacion);

        //}

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult NuevaCampaña(int campañaId, string tipo)
        {
            TiendasArticulosView campañas = new TiendasArticulosView
            {
                Campañas = db.Campañas.Where(cat => cat.CampañaId == campañaId).ToList()
            };

            var campaña = db.Campañas.Where(x => x.Nombre == campañaId.ToString()).FirstOrDefault();


            var articulosTMP = db.CampañaArticuloTMPs
                                   .Where(x => x.CampañaId == campaña.CampañaId)
                                   .GroupBy(x => new
                                   {
                                       x.ArticuloKFCId,
                                       x.ArticuloKFC.Descripcion,
                                       x.CampañaId,
                                       x.Cantidad,
                                       x.TiendaId,
                                       x.Habilitado,
                                   })
                                   .Select(x => new MaterialesCampaña()
                                   {
                                       ArticuloKFCId = x.Key.ArticuloKFCId,
                                       Campaña = campaña.Nombre,
                                       ArticuloKFC = x.Key.Descripcion,
                                       Cantidad = x.Key.Cantidad,
                                       TiendaId = x.Key.TiendaId,
                                       Habilitado = x.Key.Habilitado,
                                   });

            var misMateriales = articulosTMP.ToList();

            var tiendasCampaña = db.Tiendas
                            .Where(x => x.EquityFranquicia == tipo)
                            .GroupBy(x => new { x.Restaurante, x.CCoFranquicia, x.TiendaId })
                            .Select(x => new TiendasCampaña()
                            {
                                Restaurante = x.Key.Restaurante,
                                TiendaId = x.Key.TiendaId,
                                CC = x.Key.CCoFranquicia,
                                TipoTienda = tipo,
                            });

            var misTiendas = tiendasCampaña.ToList();

            var materialesCampaña = articulosTMP.Join(tiendasCampaña,
                                 artCamp => artCamp.TiendaId,
                                 tienCamp => tienCamp.TiendaId,
                                 (artCamp, tienCamp) => new { tiendas = tienCamp, materiales = artCamp })
                            .Where(x => x.tiendas.TiendaId == x.materiales.TiendaId)
                            .Where(x => x.tiendas.TipoTienda == tipo)
                            .GroupBy(x => new
                            {
                                x.tiendas.Restaurante,
                                x.tiendas.CC,
                                x.tiendas.TiendaId,
                                x.tiendas.TipoTienda,
                                x.materiales.ArticuloKFCId,
                                x.materiales.ArticuloKFC,
                                x.materiales.Cantidad,
                                x.materiales.Campaña,
                                x.materiales.Habilitado
                            })
                            .Select(x => new MaterialesTiendasCampaña()
                            {
                                ArticuloKFCId = x.Key.ArticuloKFCId,
                                ArticuloKFC = x.Key.ArticuloKFC,
                                Campaña = x.Key.Campaña,
                                Cantidad = x.Key.Cantidad,
                                CC = x.Key.CC,
                                Restaurante = x.Key.Restaurante,
                                TiendaId = x.Key.TiendaId,
                                TipoTienda = x.Key.TipoTienda,
                                Habilitado = x.Key.Habilitado,
                            });

            var misMaterialesCampaña = materialesCampaña.ToList();

            var totalMaterial = materialesCampaña
                               .Where(x => x.TipoTienda == tipo)
                               .GroupBy(x => new { x.ArticuloKFC, x.ArticuloKFCId })
                               .Select(x => new MaterialTotal()
                               {
                                   ArticuloKFCId = x.Key.ArticuloKFCId,
                                   ArticuloKFC = x.Key.ArticuloKFC,
                                   TotalCantidad = x.Sum(t => t.Cantidad),
                               });

            ViewBag.TotalCantidad = totalMaterial.Sum(x => x.TotalCantidad).ToString("N0");

            var totalRestaurante = materialesCampaña
                               .GroupBy(x => new { x.Restaurante })
                               .Select(x => new TiendaTotal()
                               {
                                   Restaurante = x.Key.Restaurante,
                                   TotalCantidad = x.Sum(t => t.Cantidad),
                               });

            ViewBag.TotalRestaurante = totalRestaurante.Sum(x => x.TotalCantidad).ToString("N0");


            var totalMateriales = materialesCampaña.Where(x => x.TipoTienda == tipo).ToList().OrderBy(p => p.TiendaId).ThenBy(p => p.ArticuloKFC).ToList();





            //var detalleCampaña = db.CampañaArticuloTMPs.Where(c => c.CampañaId == campañaId).ToList();

            //ViewBag.TotalCantidad = detalleCampaña.Sum(m => m.Cantidad).ToString("N0");

            if (campaña == null)
            {
                return HttpNotFound();
            }

            return PartialView(campaña);
        }

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult ArticulosCampañas(int tiendaId, int campañaId, TiendasArticulosView campañaArticulos)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            //TiendasArticulosView campañaArticulos = new TiendasArticulosView();

            campañaArticulos.CampañaArticuloTMPs = db.CampañaArticuloTMPs.Where(cat => cat.TiendaId == tiendaId && cat.CampañaId == campañaId).OrderBy(cat => cat.ArticuloKFC.Familia.Codigo).ThenBy(cat => cat.ArticuloKFCId).ToList();
            campañaArticulos.ArticuloKFCs = db.ArticuloKFCs.OrderBy(cat => cat.Familia.Codigo).ThenBy(cat => cat.ArticuloKFCId).ToList();

            if (campañaArticulos == null)
            {
                return HttpNotFound();
            }

            return PartialView(campañaArticulos);
        }

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult ArticulosCampañasDescripcion()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            TiendasArticulosView campañaArticulos = new TiendasArticulosView
            {
                ArticuloKFCs = db.ArticuloKFCs.OrderBy(cat => cat.Familia.Codigo).ThenBy(cat => cat.ArticuloKFCId).ToList()
            };
            //campañaArticulos.ArticuloKFCs = db.ArticuloKFCs.Where(cat => cat.CompañiaId == usuario.CompañiaId).ToList();

            //var detalleArticulosCampaña = db.CampañaArticuloTMPs.Where(c => c.CampañaTiendaTMPId == campañaId).ToList();

            //var totales = (from dc in detalleArticulosCampaña
            //               group new { detalleCot = dc }
            //               by new { dc.CampañaTiendaTMPId, dc.ArticuloKFCId, dc.Cantidad, } into grupo
            //               orderby grupo.Key.ArticuloKFCId
            //               select new CotizacionDetalleTotales
            //               {
            //                   ArticuloId = grupo.Key.ArticuloKFCId,
            //                   TotalCantidad = grupo.Sum(x => x.detalleCot.Cantidad),
            //               }).ToList();

            //var ta = (from s in totales
            //          group s by s.ArticuloId into g
            //          select new CotizacionDetalleTotales
            //          {
            //              ArticuloId = g.Key,
            //              TotalCantidad = g.Sum(p => p.TotalCantidad),
            //          }).ToList();

            //ViewBag.Totales = ta.ToList();

            if (campañaArticulos == null)
            {
                return HttpNotFound();
            }

            return PartialView(campañaArticulos);
        }

        [AuthorizeUser(idOperacion: 2)]
        public ActionResult ArticulosCampañasTotales(int campañaId)
        {
            TiendasArticulosView campañaArticulos = new TiendasArticulosView
            {
                CampañaArticuloTMPs = db.CampañaArticuloTMPs.Where(cat => cat.CampañaId == campañaId).ToList()
            };

            campañaArticulos.CampañaArticuloTMPs.Sum(tc => tc.Cantidad);

            if (campañaArticulos == null)
            {
                return HttpNotFound();
            }

            return PartialView(campañaArticulos);
        }

        //// GET: Campañas/Create
        //[AuthorizeUser(idOperacion: 2)]
        //public ActionResult CreateCamp(int? id)
        //{
        //    var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
        //    if (usuario == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    var response = MovementsHelper.AgregarTiendas(usuario.Compañia.CompañiaId, usuario.NombreUsuario, (int)id);

        //    if (response.Succeeded)
        //    {
        //        //TempData["mensajeLito"] = "CAMPAÑA AGREGADA";
        //    }

        //    ModelState.AddModelError(string.Empty, response.Message);

        //    //var view = new NuevaCampañaView
        //    //{
        //    //    CampañaId = db.NuevaCampañaViews.Where(cdt => cdt.CampañaId == (int)id).FirstOrDefault().CampañaId,
        //    //    Descripcion = db.NuevaCampañaViews.Where(cdt => cdt.CampañaId == (int)id).FirstOrDefault().Descripcion,
        //    //    Nombre = db.NuevaCampañaViews.Where(cdt => cdt.CampañaId == (int)id).FirstOrDefault().Nombre,
        //    //    Detalles = db.CampañaTiendaTMPs.Where(cdt => cdt.Usuario == usuario.NombreUsuario).ToList(),
        //    //};

        //    Session["CampañaId"] = view.CampañaId;

        //    return View(view);


        //}

        //=================================================================================================================================
        //=                                                                                                                               =
        //=                                                                                                                               =
        //=                                                                                                                               =
        //=================================================================================================================================

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult CodesCampArt(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index");
            }

            var response = MovementsHelper.GenerarCodigos(id);

            if (response.Succeeded)
            {
                var campaña = db.Campañas.Where(x => x.CampañaId == id).FirstOrDefault();

                var materialesCampaña = db.CampañaArticuloTMPs
                       .Where(x => x.CampañaId == id)
                       .GroupBy(x => new
                       {
                           x.ArticuloKFC.Descripcion,
                           x.CampañaId,
                           x.Cantidad,
                           x.ArticuloKFC.Proveedor.Nombre,
                       })
                       .Select(x => new MaterialesTotales()
                       {
                           Campaña = campaña.Nombre,
                           ArticuloKFC = x.Key.Descripcion,
                           Cantidad = x.Key.Cantidad,
                           Proveedor = x.Key.Nombre,
                           TotalCantidad = x.Sum(t => t.Cantidad),
                       });

                var codigosCampaña = db.CodigosCampaña
                                .Where(x => x.CampañaId == id)
                                .GroupBy(x => new { x.ArticuloKFC.Descripcion, x.Codigo, x.CampañaId })
                                .Select(x => new CodigosMateriales()
                                {
                                    ArticuloKFC = x.Key.Descripcion,
                                    Campaña = campaña.Nombre,
                                    Codigo = x.Key.Codigo,
                                });

                var materiales = codigosCampaña.Join(materialesCampaña,
                                     cali => cali.ArticuloKFC,
                                     caliInt => caliInt.ArticuloKFC,
                                     (cali, caliInt) => new { codigos = cali, materiales = caliInt })
                                .Where(x => x.codigos.ArticuloKFC == x.materiales.ArticuloKFC)
                                .GroupBy(x => new { x.codigos.ArticuloKFC, x.codigos.Campaña, x.codigos.Codigo, x.materiales.Proveedor, x.materiales.Cantidad, })
                                .Select(x => new CodigosMaterialesTotal()
                                {
                                    ArticuloKFC = x.Key.ArticuloKFC,
                                    Campaña = x.Key.Campaña,
                                    Codigo = x.Key.Codigo,
                                    Proveedor = x.Key.Proveedor,
                                    TotalCantidad = x.Sum(d => d.materiales.TotalCantidad),
                                });

                var codigosMateriales = materiales.Where(x => x.TotalCantidad != 0).ToList();


                var materialesCampañaOrdenes = db.CampañaArticuloTMPs
                       .Where(x => x.CampañaId == id)
                       .GroupBy(x => new
                       {
                           x.ArticuloKFCId,
                           x.Cantidad,
                           x.TiendaId,
                       })
                       .Select(x => new MaterialesTotalesOrdenes()
                       {
                           ArticuloKFCId = x.Key.ArticuloKFCId,
                           Cantidad = x.Key.Cantidad,
                           TiendaId = x.Key.TiendaId,
                       });

                var misMaterialesOrdenes = materialesCampañaOrdenes.ToList();

                var codigosCampañaOrdenes = db.CodigosCampaña
                                .Where(x => x.CampañaId == id)
                                .GroupBy(x => new { x.ArticuloKFC.ArticuloKFCId, x.Codigo, x.CampañaId })
                                .Select(x => new CodigosMaterialesOrdenes()
                                {
                                    ArticuloKFCId = x.Key.ArticuloKFCId,
                                    Campaña = campaña.Descripcion,
                                    Codigo = x.Key.Codigo,
                                });

                var misCodigosCampañas = codigosCampañaOrdenes.ToList();

                var materialesOrdenes = codigosCampañaOrdenes.Join(materialesCampañaOrdenes,
                                     mat => mat.ArticuloKFCId,
                                     cods => cods.ArticuloKFCId,
                                     (mat, cods) => new { codigos = mat, materiales = cods})
                                .Where(x => x.codigos.ArticuloKFCId == x.materiales.ArticuloKFCId)
                                .GroupBy(x => new { x.materiales.ArticuloKFCId, x.codigos.Campaña, x.codigos.Codigo, x.materiales.Cantidad, x.materiales.TiendaId })
                                .Select(x => new CodigosMaterialesTotalOrdenes()
                                {
                                    ArticuloKFCId = x.Key.ArticuloKFCId,
                                    Campaña = x.Key.Campaña,
                                    Codigo = x.Key.Codigo,
                                    Cantidad = x.Key.Cantidad,
                                    TiendaId = x.Key.TiendaId,
                                });

                var codigosMaterialesOrdenes = materialesOrdenes.Where(x => x.Cantidad != 0).ToList();

                var vacio = "";

                using (StreamWriter streamWriter = new StreamWriter("C:\\Articulos" + campaña.Nombre + ".txt"))
                {
                    foreach (var codigo in codigosMateriales)
                    {
                        var linea = "INSERT INTO Articulos (Codigo, Descripcion, SistemaImpresion,MedExtendida,Sustrato,Tintas,Laminado_FV,Corte,MatPegue,InfAdicional) VALUES ('" + codigo.Codigo + "', '" + codigo.ArticuloKFC + "', '" + vacio + "', '" + vacio + "', '" + vacio + "', '" + vacio + "', '" + vacio + "', '" + vacio + "', '" + vacio + "', '" + codigo.ArticuloKFC + "')";
                        streamWriter.WriteLine(linea);
                    }
                }

                var tiendas = db.Tiendas.OrderBy(x => x.TiendaId).ToList();
                var i = 1;

                using (StreamWriter streamWriter = new StreamWriter("C:\\Tiendas" + campaña.Nombre + ".txt"))
                {
                    foreach (var tienda in tiendas)
                    {
                        var linea = "INSERT INTO Tiendas (Id, Secuencia, Tienda, Region, Ciudad, IdCampana) VALUES (" + tienda.TiendaId + ", " + i + ", '" + tienda.Restaurante + "', '" + tienda.Region.Nombre + "', '" + tienda.Ciudad.Nombre + "', " + Convert.ToInt32(campaña.Nombre) + ")";
                        streamWriter.WriteLine(linea);

                        i = i + 1;
                    }
                }

                using (StreamWriter streamWriter = new StreamWriter("C:\\Ordenes" + campaña.Nombre + ".txt"))
                {
                    foreach (var codigo in codigosMaterialesOrdenes)
                    {
                        var linea = "INSERT INTO Ordenes (CAMPANA, IDTIENDA, IDARTICULO, CANTIDAD) VALUES ('" + campaña.Descripcion + "', '" + codigo.TiendaId + "', '" + codigo.Codigo + "', " + codigo.Cantidad + ")";
                        streamWriter.WriteLine(linea);
                    }
                }

                //TempData["mensajeLito"] = "CAMPAÑA AGREGADA";
            }

            return RedirectToAction("ResumenProveedor", new { id = id });
        }

        // GET: Campañas/Details/5
        [AuthorizeUser(idOperacion: 6)]
        public ActionResult CloseCampArt(int? id)
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
        [AuthorizeUser(idOperacion: 6)]
        [HttpPost, ActionName("CloseCampArt")]
        [ValidateAntiForgeryToken]
        public ActionResult CloseCampArtConfirmed(int id)
        {
            var campaña = db.Campañas.Find(id);
            campaña.Generada = "SI";
            db.Entry(campaña).State = EntityState.Modified;
            var response = DBHelper.SaveChanges(db);

            if (response.Succeeded)
            {
                TempData["mensajeLito"] = "CAMPAÑA CERRADA";

                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return PartialView(campaña);
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        public JsonResult EditarCantidad(int? id, int? q)
        {
            var result = false;
            try
            {
                CampañaArticuloTMP campañaArticuloTMP = db.CampañaArticuloTMPs.SingleOrDefault(x => x.CampañaArticuloTMPId == id);

                campañaArticuloTMP.Cantidad = (int)q;
                db.SaveChanges();
                result = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUser(idOperacion: 1)]
        public ActionResult CreateCampArt2(int? tiendaId, int? campId, TiendasArticulosView view)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var response = MovementsHelper.AgregarArticulosTiendas((int)tiendaId, (int)campId);

            if (response.Succeeded)
            {
                //TempData["mensajeLito"] = "CAMPAÑA AGREGADA";
            }



            ModelState.AddModelError(string.Empty, response.Message);

            //var view = db.CampañaArticuloTMPs.Where(cat => cat.CampañaTiendaTMPId == campId && cat.TiendaId == id).ToList();
            view.Tiendas = db.Tiendas.ToList();
            view.ArticuloKFCs = db.ArticuloKFCs.ToList();
            view.Campañas = db.Campañas.Where(c => c.CampañaId == campId).ToList();
            view.CampañaArticuloTMPs = db.CampañaArticuloTMPs.Where(cat => cat.CampañaId == campId && cat.TiendaId == tiendaId).ToList();

            ViewBag.Tienda = db.Tiendas.Where(t => t.TiendaId == tiendaId).FirstOrDefault().Restaurante;

            return PartialView(view);
        }

        [AuthorizeUser(idOperacion: 1)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCampañaArticulos(FormCollection fc)
        {

            var nombre = User.Identity.Name;
            var usuarioActual = db.Usuarios.Where(u => u.NombreUsuario == nombre).FirstOrDefault();

            string[] articuloKFCTMPId = fc.GetValues("articuloId");
            string[] cantidad = fc.GetValues("cantidad");

            //var habilitado = false;
            var cantidadNumero = 0;

            for (var i = 0; i < articuloKFCTMPId.Length; i++)
            {

                CampañaArticuloTMP campañaArticulo = db.CampañaArticuloTMPs.Find(Convert.ToInt32(articuloKFCTMPId[i]));

                //habilitado = false;
                cantidadNumero = 0;

                if (campañaArticulo.Cantidad != Convert.ToInt32(cantidad[i]))
                {
                    cantidadNumero = Convert.ToInt32(cantidad[i]);
                    campañaArticulo.Cantidad = cantidadNumero;

                    db.Entry(campañaArticulo).State = EntityState.Modified;
                    db.SaveChanges();

                }
            }

            TempData["mensajeLito"] = "CANTIDADES ACTUALIZADAS";
            return RedirectToAction("Index", "Campañas");
        }

        // POST: Campañas/Create
        [AuthorizeUser(idOperacion: 1)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCamp(Campaña campaña)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (!string.IsNullOrEmpty(campaña.Descripcion) && !string.IsNullOrEmpty(campaña.Nombre))
            {
                var Detalles = db.CampañaTiendaTMPs.ToList();

                if (Detalles.Count != 0)
                {
                    if (ModelState.IsValid)
                    {
                        //var response = MovementsHelper.NuevaCampaña(campaña, usuario.NombreUsuario, usuario.Compañia.CompañiaId);
                        //if (response.Succeeded)
                        //{
                        //    TempData["mensajeLito"] = "CAMPAÑA AGREGADA";

                        return RedirectToAction("Index");
                        //}

                        //ModelState.AddModelError(string.Empty, response.Message);
                    }
                }
                else
                {
                    TempData["mensajeLito"] = "AUN NO SE HAN AGREGADO TIENDAS";
                }
            }
            else
            {
                TempData["mensajeLito"] = "CAPTURAR LOS DATOS DE LA CAMPAÑA";
            }

            return View(campaña);

        }

        // GET: Campañas/Create
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult Create()
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();
            if (usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var campañas = new Campaña
            {
                Generada = "NO",
            };

            return PartialView(campañas);


        }

        // POST: Campañas/Create
        [AuthorizeUser(idOperacion: 1)]
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
                    MovementsHelper.AgregarArticulosNuevaCampaña(campaña.CampañaId);

                    TempData["mensajeLito"] = "CAMPAÑA CREADA";

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);

            }

            return PartialView(campaña);

        }

        // GET: Campañas/Edit/5
        [AuthorizeUser(idOperacion: 2)]
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
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Campaña campaña)
        {
            var usuario = db.Usuarios.Where(u => u.NombreUsuario == User.Identity.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Entry(campaña).State = EntityState.Modified;
                var response = DBHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    //var response2 = MovementsHelper.AgregarArticulosNuevaCampaña(campaña.CampañaId);

                    TempData["mensajeLito"] = "CAMPAÑA EDITADA";

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return View(campaña);

        }

        // GET: Campañas/Delete/5
        [AuthorizeUser(idOperacion: 3)]
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
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var detalleArticulos = db.CampañaArticuloTMPs.Where(da => da.CampañaId == id).ToList();
            var detalleTiendas = db.CampañaTiendaTMPs.Where(da => da.CampañaId == id).ToList();
            var detalleCodigos = db.CodigosCampaña.Where(da => da.CampañaId == id).ToList();

            if (detalleArticulos.Count > 0)
            {
                db.CampañaArticuloTMPs.RemoveRange(detalleArticulos);
                db.SaveChanges();
            }

            if (detalleTiendas.Count > 0)
            {
                db.CampañaTiendaTMPs.RemoveRange(detalleTiendas);
                db.SaveChanges();
            }

            if (detalleCodigos.Count > 0)
            {
                db.CodigosCampaña.RemoveRange(detalleCodigos);
                db.SaveChanges();
            }

            var campaña = db.Campañas.Find(id);
            db.Campañas.Remove(campaña);
            var response = DBHelper.SaveChanges(db);

            if (response.Succeeded)
            {
                TempData["mensajeLito"] = "CAMPAÑA ELIMINADA";

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
