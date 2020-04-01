using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("Familias")]
    public class Familia
    {
        public int FamiliaId { get; set; }

        public string Descripcion { get; set; }

        [MaxLength(3, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Codigo")]
        public string Codigo { get; set; }

        public virtual ICollection<ArticuloKFC> ArticulosKFC { get; set; }

    }
}