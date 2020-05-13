using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("Tiendas")]
    public class Tienda
    {
        [Key]
        public int TiendaId { get; set; }

        [Display(Name = "CLASIFICACIÓN")]
        public string Clasificacion { get; set; }

        [Display(Name = "C.C. o Franquicia")]
        public string CCoFranquicia { get; set; }

        [Display(Name = "RESTAURANTE")]
        public string Restaurante { get; set; }

        [Display(Name = "REGIÓN", Prompt = "[Seleccionar...]")]
        public int RegionId { get; set; }

        [Display(Name = "CIUDAD", Prompt = "[Seleccionar...]")]
        public int CiudadId { get; set; }

        [Display(Name = "DIRECCIÓN")]
        public string Direccion { get; set; }

        //00000000000000000000000000 GENERALES 0000000000000000000000000000000000

        [Display(Name = "TIPO", Prompt = "[Seleccionar...]")]
        public int TipoId { get; set; }

        [Display(Name = "NUEVO NIVEL DE PRECIO", Prompt = "[Seleccionar...]")]
        public int NuevoNivelDePrecioId { get; set; }

        [Display(Name = "MENÚ DIGITAL")]
        public bool MenuDigital { get; set; }

        [Display(Name = "CANTIDAD DE PANTALLAS")]
        public string CantidadDePantallas { get; set; }

        //00000000000000000000000000 POR PRODUCTO 0000000000000000000000000000000000


        [Display(Name = "3ERA RECETA")]
        public bool TerceraReceta { get; set; }

        [Display(Name = "ARROZ")]
        public bool Arroz { get; set; }

        [Display(Name = "HAMBURGUESAS")]
        public bool Hamburgesas { get; set; }

        [Display(Name = "ENSALADA")]
        public bool Ensalada { get; set; }

        [Display(Name = "PET DE 2 LITROS")]
        public bool PET2Litros { get; set; }

        [Display(Name = "POSTRES")]
        public bool Postres { get; set; }

        [Display(Name = "BISQUET MIEL")]
        public bool BisquetMiel { get; set; }

        [Display(Name = "KE-CONO")]
        public bool KeCono { get; set; }

        [Display(Name = "KREAMBALL")]
        public bool KREAMBALL { get; set; }


        //00000000000000000000000000 MATERIALES ESPECIFICOS 0000000000000000000000000000000000

        [Display(Name = "MENÚ BACKLIGHT")]
        public bool MenuBackLigth { get; set; }

        [Display(Name = "AUTOEXPRESS")]
        public bool Autoexpress { get; set; }

        [Display(Name = "COPETE AE REMODELADO")]
        public bool CopeteAERemodelado { get; set; }

        [Display(Name = "COPETE AE TRADICIONAL")]
        public bool CopeteAETradicional { get; set; }

        [Display(Name = "PANEL DE INNNOVACIÓN")]
        public bool PanelDeInnovacion { get; set; }

        [Display(Name = "DISPLAY DE BURBUJA")]
        public bool DisplayDeBurbuja { get; set; }

        [Display(Name = "DELIVERY")]
        public bool Delivery { get; set; }

        [Display(Name = "MERCADO DE PRUEBA")]
        public bool MERCADO_DE_PRUEBA { get; set; }

        [Display(Name = "AREA DE JUEGOS")]
        public bool AreaDeJuegos { get; set; }

        [Display(Name = "COPETE ESPECIAL SOPORTE LATERAL 4 VASOS")]
        public bool COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS { get; set; }

        [Display(Name = "COPETE ESPECIAL SOPORTE LATERAL PET 2L ")]
        public bool COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L { get; set; }

        [Display(Name = "Display De Piso")]
        public bool DisplayDePiso { get; set; }

        [Display(Name = "WC NACIONAL 67X100 CM")]
        public bool WCNACIONAL67X100cm { get; set; }


        //00000000000000000000000000 MEDIDAS ESPECIALES 0000000000000000000000000000000000

        [Display(Name = "WC MEDIDA ESPECIAL  60.8 x 85 cm")]
        public bool WCMedidaEspecial60_8x85cm { get; set; }

        [Display(Name = "WC MEDIDA ESPECIAL MALL ORIENTE 100x120 cm")]
        public bool WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm { get; set; }

        [Display(Name = "WC MEDIDA ESPECIAL ZUAZUA 87x120 cm")]
        public bool WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm { get; set; }

        [Display(Name = "WC MEDIDA ESPECIAL CORREO MAYOR 60x90 cm")]
        public bool WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm { get; set; }

        [Display(Name = "WC MEDIDA ESPECIAL ZARAGOZA 90x100 cm")]
        public bool WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm { get; set; }

        [Display(Name = "MEDIA ESPECIAL PNEL DE COMPLEMENTOS")]
        public bool MedidaEspecialPanelDeComplementos { get; set; }

        [Display(Name = "MEDIDA ESPECIAL PRE MENU AE SAN_ANTONIO 49x67.5 cm")]
        public bool MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE TECAMAC 48x67.5 cm")]
        public bool MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE VILLA GARCIA 45x65 cm")]
        public bool MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE XOLA 49.9x66.9 cm")]
        public bool MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE ZUAZUA 51x71 cm")]
        public bool MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE VALLE SOLEADO 51x71 cm")]
        public bool MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE MIRASIERRA 46x68 cm")]
        public bool MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE CELAYA 50x68.5 cm")]
        public bool MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE CANDILES 49.5x73.5 cm")]
        public bool MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm { get; set; }


        //00000000000000000000000000 POR EQUIPO EN EL RESTAURANTE 0000000000000000000000000000000000

        [Display(Name = "TIPO DE CAJA", Prompt = "[Seleccionar...]")]
        public int TipoDeCajaId { get; set; }

        [Display(Name = "ACOMODO DE CAJA", Prompt = "[Seleccionar...]")]
        public string AcomodoDeCajas { get; set; }

        [Display(Name = "# MESA AREAS DE COMEDOR")]
        public string NoMesaDeAreaComedor { get; set; }

        [Display(Name = "# MESA AREAS DE JUEGO")]
        public string NoMesaDeAreaDeJuegos { get; set; }

        [Display(Name = "NUMERO DE VENTANAS")]
        public string NumeroDeVentanas { get; set; }

        [Display(Name = "UBICACIÓN PANTALLAS", Prompt = "[Seleccionar...]")]
        public string UbicacionPantallas { get; set; }


        //00000000000000000000000000 GENERALES 0000000000000000000000000000000000

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        public string EquityFranquicia { get; set; }


        //00000000000000000000000000 RELACIONES 0000000000000000000000000000000000


        public virtual Region Region { get; set; }

        public virtual Ciudad Ciudad { get; set; }

        public virtual TipoCaja TipoDeCaja { get; set; }

        public virtual TipoTienda TipoTienda { get; set; }

        public virtual NivelPrecio NivelPrecio { get; set; }

    }
}