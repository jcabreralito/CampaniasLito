using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("Reglas")]
    public class Regla
    {
        [Key]
        public int ReglaId { get; set; }

        public int TiendaId { get; set; }

        public int ArticuloKFCId { get; set; }

        public bool Seleccionado { get; set; }

        public virtual Tienda Tienda { get; set; }

        public virtual ArticuloKFC ArticuloKFC { get; set; }
    }
}