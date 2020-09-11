using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("Reglas")]
    public class Regla
    {
        [Key]
        public int ReglaId { get; set; }

        [Display(Name = "Material", Prompt = "[Seleccionar...]")]
        public int ArticuloKFCId { get; set; }

        public string NombreRegla { get; set; }

        public virtual ArticuloKFC ArticuloKFC { get; set; }

    }
}