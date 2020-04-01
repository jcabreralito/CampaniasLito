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
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar un {0}")]
        [Display(Name = "Proveedor", Prompt = "[Seleccionar un Proveedor...]")]
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Familia", Prompt = "[Seleccionar una Familia...]")]
        public int FamiliaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Cantidad Default")]
        public int CantidadDefault { get; set; }

        public virtual Compañia Compañia { get; set; }

        public virtual ICollection<CampañaArticuloTMP> CampañaArticuloTMPs { get; set; }

        public virtual Proveedor Proveedor { get; set; }

        public virtual Familia Familia { get; set; }

        public virtual ICollection<TiendaArticulo> TiendaArticulos { get; set; }

        public virtual ICollection<CodigoCampaña> CodigoCampañas { get; set; }

    }
}