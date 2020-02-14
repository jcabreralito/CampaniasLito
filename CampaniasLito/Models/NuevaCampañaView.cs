using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampaniasLito.Models
{
    public class NuevaCampañaView
    {
        [Key]
        public int NuevaCampañaViewId { get; set; }
        public int CampañaId { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public List<CampañaTiendaTMP> Detalles { get; set; }

    }
}