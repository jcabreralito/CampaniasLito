using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("TiendaArticulos")]
    public class TiendaArticulo
    {
        [Key]
        public long TiendaArticuloId { get; set; }

        public int TiendaId { get; set; }

        public int ArticuloKFCId { get; set; }

        public bool Seleccionado { get; set; }

        //public string EquityFranquicia { get; set; }

        public virtual Tienda Tienda { get; set; }

        public virtual ArticuloKFC ArticuloKFC { get; set; }

        public virtual CampañaArticuloTMP CampañaArticuloTMP { get; set; }

    }
}