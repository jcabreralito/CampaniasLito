using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampaniasLito.Models
{
    public class CampañaTiendaTMP
    {
        [Key]
        public int CampañaTiendaTMPId { get; set; }

        public string Usuario { get; set; }

        public int CompañiaId { get; set; }

        [Display(Name = "Tienda Id")]
        public int TiendaId { get; set; }

        [Display(Name = "Campaña")]
        public int CampañaId { get; set; }

        public bool Seleccionada { get; set; }

        public virtual Tienda Tienda { get; set; }

        public virtual ICollection<CampañaArticuloTMP> CampañaArticuloTMPs { get; set; }

    }
}