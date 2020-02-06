using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("TiposTienda")]
    public class TipoTienda
    {
        [Key]
        public int TipoTiendaId { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

    }
}