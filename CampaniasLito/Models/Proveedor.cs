using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("Proveedores")]
    public class Proveedor
    {
        [Key]
        public int ProveedorId { get; set; }

        [Display(Name = "Proveedor")]
        public string Nombre { get; set; }

        public virtual ICollection<ArticuloKFC> ArticuloKFCs { get; set; }

    }
}