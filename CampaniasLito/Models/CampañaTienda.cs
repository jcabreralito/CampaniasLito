using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("CampañaTiendas")]
    public class CampañaTienda
    {
        [Key]
        public int CampañaTiendaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Campaña")]
        public int CampañaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Tienda")]
        public int TiendaId { get; set; }

        public int CompañiaId { get; set; }

        [Display(Name = "Fecha")]
        public DateTime CreatedDate { get; set; }

        public virtual Campaña Campaña { get; set; }

        public virtual Tienda Tienda { get; set; }

        public virtual Compañia Compañia { get; set; }
    }
}