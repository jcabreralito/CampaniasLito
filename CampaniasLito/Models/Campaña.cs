using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CampaniasLito.Models
{
    [Table("Campañas")]
    public class Campaña
    {
        [Key]
        public int CampañaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "ID")]
        [Index("Campaña_CompañiaId_Nombre_Index", 2, IsUnique = true)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Compañia")]
        [Index("Campaña_CompañiaId_Nombre_Index", 1, IsUnique = true)]
        public int CompañiaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Generada")]
        public string Generada { get; set; }

        public virtual Compañia Compañia { get; set; }

        public virtual ICollection<CampañaArticulo> CampañaArticulos { get; set; }

        public virtual ICollection<CodigoCampaña> CodigoCampañas { get; set; }

        public List<CampañaArticuloTMP> CampañaDetalles { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public double TotalCantidad { get { return CampañaDetalles == null ? 0 : CampañaDetalles.Sum(d => d.Cantidad); } }



    }
}