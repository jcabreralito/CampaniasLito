using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CampaniasLito.Models
{
    public class CampañaArticuloTMP
    {
        [Key]
        public int CampañaArticuloTMPId { get; set; }

        public string Usuario { get; set; }

        public int Compañia { get; set; }

        [Display(Name = "Articulo Id")]
        public int ArticuloKFCId { get; set; }

        [Display(Name = "Tienda Id")]
        public int TiendaId { get; set; }

        public int CampañaTiendaTMPId { get; set; }

        public bool Habilitado { get; set; }

        public double Cantidad { get; set; }

        [Display(Name = "Código")]
        public int Codigo { get; set; }

        public virtual CampañaTiendaTMP CampañaTiendaTMP { get; set; }

        public virtual ArticuloKFC ArticuloKFC { get; set; }

    }
}