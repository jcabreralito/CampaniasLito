using System.ComponentModel.DataAnnotations;

namespace CampaniasLito.Models
{
    public class FamiliaTienda
    {
        [Key]
        public int FamiliaTiendaId { get; set; }

        public int FamiliaId { get; set; }

        public int TiendaId { get; set; }

        public virtual Familia Familia { get; set; }

        public virtual Tienda Tienda { get; set; }

    }
}