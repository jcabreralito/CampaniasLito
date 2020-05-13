using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("TiendaConfiguracion")]
    public class TiendaConfiguracion
    {
        [Key]
        public int TiendaConfiguracionId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(250, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Caracteristica")]
        [Index("TiendaConfiguracion_Nombre_Index", IsUnique = true)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Tipo", Prompt = "[Seleccionar...]")]
        public int TipoConfiguracionId { get; set; }

        public virtual TipoConfiguracion TipoConfiguracion { get; set; }

    }
}