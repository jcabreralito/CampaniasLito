using System.Collections.Generic;

namespace CampaniasLito.Models
{
    public class TiendasArticulosView
    {
        public List<Tienda> Tiendas { get; set; }

        public List<ArticuloKFC> ArticuloKFCs { get; set; }

        public List<Campaña> Campañas { get; set; }

        public List<CampañaArticuloTMP> CampañaArticuloTMPs { get; set; }

    }

}