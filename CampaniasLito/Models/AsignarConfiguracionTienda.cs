using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("AsignarConfiguracionTiendas")]
    public class AsignarConfiguracionTienda
    {
        [Key]
        public int AsignarConfiguracionTiendaId { get; set; }

        public int TiendaConfiguracionId { get; set; }

        public int TiendaId { get; set; }

        public bool Seleccionado { get; set; }

        public virtual TiendaConfiguracion TiendaConfiguracion { get; set; }

        public virtual Tienda Tienda { get; set; }

    }
}