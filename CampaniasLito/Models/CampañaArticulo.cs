using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("CampañaArticulos")]
    public class CampañaArticulo
    {
        [Key]
        public int CampañaArticuloId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Campaña")]
        public int CampañaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Articulo")]
        public int ArticuloId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Tienda")]
        public int TiendaId { get; set; }

        public virtual Campaña Campaña { get; set; }

        public virtual Articulo Articulo { get; set; }

        public virtual Tienda Tienda { get; set; }

        public virtual Compañia Compañia { get; set; }
    }
}