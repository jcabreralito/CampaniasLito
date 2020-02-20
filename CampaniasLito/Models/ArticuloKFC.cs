using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("ArticulosKFC")]
    public class ArticuloKFC
    {
        [Key]
        public int ArticuloKFCId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(255, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Artículo")]
        [Index("ArticuloKFC_CompañiaId_Descripcion_Index", 2, IsUnique = true)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Compañia")]
        [Index("ArticuloKFC_CompañiaId_Descripcion_Index", 1, IsUnique = true)]
        public int CompañiaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(3, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Familia")]
        public string Familia { get; set; }

        public virtual Compañia Compañia { get; set; }
        public virtual ICollection<CampañaArticuloTMP> CampañaArticuloTMPs { get; set; }

    }
}