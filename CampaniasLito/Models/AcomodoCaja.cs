using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("AcomodoCajas")]
    public class AcomodoCaja
    {
        [Key]
        public int AcomodoCajaId { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

    }
}