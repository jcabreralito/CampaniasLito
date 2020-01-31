using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("Tiendas")]
    public class Tienda
    {
        [Key]
        public int TiendaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Compañia")]
        [Index("Tienda_CompañiaId_CCoFranquicia_Index", 1, IsUnique = true)]
        public int CompañiaId { get; set; }

        [Display(Name = "CLASIFICACIÓN")]
        public string Clasificacion { get; set; }

        [Display(Name = "C.C. o Franquicia")]
        [Index("Tienda_CompañiaId_CCoFranquicia_Index", 2, IsUnique = true)]
        public int CCoFranquicia { get; set; }

        [Display(Name = "RESTAURANTE")]
        public string Restaurante { get; set; }

        [Display(Name = "REGIÓN")]
        public int RegionId { get; set; }

        [Display(Name = "CIUDAD")]
        public int CiudadId { get; set; }

        [Display(Name = "DIRECCIÓN")]
        public string Direccion { get; set; }

        [Display(Name = "NUEVO NIVEL DE PRECIO")]
        public int NuevoNivelDePrecioId { get; set; }

        [Display(Name = "TIPO")]
        public int TipoId { get; set; }

        [Display(Name = "ENSALADA")]
        public string Ensalada { get; set; }

        [Display(Name = "AUTOEXPRESS")]
        public string Autoexpress { get; set; }

        [Display(Name = "MENÚ BACKLIGHT")]
        public string MenuBackLigth { get; set; }

        [Display(Name = "MP KE MARTES BIG KRUNCH")]
        public string MPKeMartesBigKrunch { get; set; }

        [Display(Name = "3ERA RECETA")]
        public string TerceraReceta { get; set; }

        [Display(Name = "POSTRES")]
        public string Postres { get; set; }

        [Display(Name = "COPETE AE REMODELADO")]
        public string CopeteAERemodelado { get; set; }

        [Display(Name = "COPETE AE TRADICIONAL")]
        public string CopeteAETradicional { get; set; }

        [Display(Name = "DELIVERY")]
        public string Delivery { get; set; }

        [Display(Name = "WC MEDIDA ESPECIAL  60.8 x 85 cm")]
        public string WCMedidaEspecial60_8x85cm { get; set; }

        [Display(Name = "PANEL DE INNNOVACIÓN")]
        public string PanelDeInnovacion { get; set; }

        [Display(Name = "BISQUET MIEL")]
        public string BisquetMiel { get; set; }

        [Display(Name = "ELOTE")]
        public string Elote { get; set; }

        [Display(Name = "HAMBURGUESAS")]
        public string Hamburgesas { get; set; }

        [Display(Name = "ARROZ")]
        public string Arroz { get; set; }

        [Display(Name = "PET DE 2 LITROS")]
        public string PET2Litros { get; set; }

        [Display(Name = "MENÚ DIGITAL")]
        public string MenuDigital { get; set; }

        [Display(Name = "MEDIDAS ESPECIALES MENU")]
        public string Medidas { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AUTOEXPRESS")]
        public string MedidaEspecialAutoexpress { get; set; }

        [Display(Name = "MEDIA ESPECIAL PNEL DE COMPLEMENTOS")]
        public string MedidaEspecialPanelDeComplementos { get; set; }

        [Display(Name = "NUMERO DE VENTANAS")]
        public string NumeroDeVentanas { get; set; }

        [Display(Name = "DISPLAY DE BURBUJA")]
        public string DisplayDeBurbuja { get; set; }

        [Display(Name = "AREA DE JUEGOS")]
        public string AreaDeJuegos { get; set; }

        [Display(Name = "# MESA AREAS DE JUEGO")]
        public string NoMesaDeAreaDeJuegos { get; set; }

        [Display(Name = "# MESA AREAS DE COMEDOR")]
        public string NoMesaDeAreaComedor { get; set; }

        [Display(Name = "CANTIDAD DE PANTALLAS")]
        public string CantidadDePantallas { get; set; }

        [Display(Name = "ACOMODO DE CAJA")]
        public string AcomodoDeCajas { get; set; }

        [Display(Name = "TIPO DE CAJA")]
        public string TipoDeCajaId { get; set; }

        public virtual ICollection<CampañaArticulo> CampañaArticulos { get; set; }

        public virtual ICollection<CampañaTienda> CampañaTiendas { get; set; }

    }
}