using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CampaniasLito.Models
{
    [Table("ArticuloKFCMedidas")]
    public class ArticuloKFCMedida
    {
        public int ArticuloKFCMedidaId { get; set; }

        public int ArticuloKFCId { get; set; }

        public string Medida { get; set; }

        public virtual ArticuloKFC ArticuloKFC { get; set; }

    }
}