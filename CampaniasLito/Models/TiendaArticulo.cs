using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CampaniasLito.Models
{
    [Table("TiendaArticulos")]
    public class TiendaArticulo
    {
        [Key]
        public int TiendaArticuloId { get; set; }

        public int TiendaId { get; set; }

        public int ArticuloKFCId { get; set; }

        public bool Seleccionado { get; set; }

        public virtual Tienda Tienda { get; set; }

        public virtual ArticuloKFC ArticuloKFC { get; set; }

    }
}