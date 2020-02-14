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
        public int CompañiaId { get; set; }

        [Display(Name = "CLASIFICACIÓN")]
        public string Clasificacion { get; set; }

        [Display(Name = "C.C. o Franquicia")]
        public string CCoFranquicia { get; set; }

        [Display(Name = "RESTAURANTE")]
        public string Restaurante { get; set; }

        [Display(Name = "REGIÓN", Prompt = "[Región...]")]
        public int RegionId { get; set; }

        [Display(Name = "CIUDAD", Prompt = "[Ciudad...]")]
        public int CiudadId { get; set; }

        [Display(Name = "DIRECCIÓN")]
        public string Direccion { get; set; }

        [Display(Name = "NUEVO NIVEL DE PRECIO", Prompt = "[Nivel Precio...]")]
        public int NuevoNivelDePrecioId { get; set; }

        [Display(Name = "TIPO", Prompt = "[Tipo...]")]
        public int TipoId { get; set; }

        [Display(Name = "ENSALADA")]
        public bool Ensalada { get; set; }

        [Display(Name = "AUTOEXPRESS")]
        public bool Autoexpress { get; set; }

        [Display(Name = "MENÚ BACKLIGHT")]
        public bool MenuBackLigth { get; set; }

        [Display(Name = "MP KE MARTES BIG KRUNCH")]
        public bool MPKeMartesBigKrunch { get; set; }

        [Display(Name = "3ERA RECETA")]
        public bool TerceraReceta { get; set; }

        [Display(Name = "POSTRES")]
        public bool Postres { get; set; }

        [Display(Name = "COPETE AE REMODELADO")]
        public bool CopeteAERemodelado { get; set; }

        [Display(Name = "COPETE AE TRADICIONAL")]
        public bool CopeteAETradicional { get; set; }

        [Display(Name = "DELIVERY")]
        public bool Delivery { get; set; }

        [Display(Name = "WC MEDIDA ESPECIAL  60.8 x 85 cm")]
        public bool WCMedidaEspecial60_8x85cm { get; set; }

        [Display(Name = "PANEL DE INNNOVACIÓN")]
        public bool PanelDeInnovacion { get; set; }

        [Display(Name = "BISQUET MIEL")]
        public bool BisquetMiel { get; set; }

        [Display(Name = "ELOTE")]
        public bool Elote { get; set; }

        [Display(Name = "HAMBURGUESAS")]
        public bool Hamburgesas { get; set; }

        [Display(Name = "ARROZ")]
        public bool Arroz { get; set; }

        [Display(Name = "PET DE 2 LITROS")]
        public bool PET2Litros { get; set; }

        [Display(Name = "MENÚ DIGITAL")]
        public bool MenuDigital { get; set; }

        [Display(Name = "MEDIDAS ESPECIALES MENU")]
        public bool MedidasEspecialesMenu { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AUTOEXPRESS")]
        public bool MedidaEspecialAutoexpress { get; set; }

        [Display(Name = "MEDIA ESPECIAL PNEL DE COMPLEMENTOS")]
        public bool MedidaEspecialPanelDeComplementos { get; set; }

        [Display(Name = "NUMERO DE VENTANAS")]
        public string NumeroDeVentanas { get; set; }

        [Display(Name = "DISPLAY DE BURBUJA")]
        public bool DisplayDeBurbuja { get; set; }

        [Display(Name = "AREA DE JUEGOS")]
        public bool AreaDeJuegos { get; set; }

        [Display(Name = "# MESA AREAS DE JUEGO")]
        public string NoMesaDeAreaDeJuegos { get; set; }

        [Display(Name = "# MESA AREAS DE COMEDOR")]
        public string NoMesaDeAreaComedor { get; set; }

        [Display(Name = "CANTIDAD DE PANTALLAS")]
        public string CantidadDePantallas { get; set; }

        [Display(Name = "ACOMODO DE CAJA", Prompt = "[Acomodo...]")]
        public string AcomodoDeCajas { get; set; }

        [Display(Name = "TIPO DE CAJA", Prompt = "[Tipo Caja...]")]
        public int TipoDeCajaId { get; set; }

        public virtual Region Region { get; set; }

        public virtual Ciudad Ciudad { get; set; }

        public virtual ICollection<CampañaArticulo> CampañaArticulos { get; set; }

        public virtual ICollection<CampañaTienda> CampañaTiendas { get; set; }

        public virtual ICollection<CampañaTiendaTMP> CampañaTiendaTMPs { get; set; }

    }
}