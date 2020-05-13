using System.ComponentModel.DataAnnotations;

namespace CampaniasLito.Models
{
    public class TiendaProducto
    {
        [Key]
        public int TiendaProductoId { get; set; }

        public int TiendaId { get; set; }

        [Display(Name = "3ERA RECETA")]
        public bool TerceraReceta { get; set; }

        [Display(Name = "ARROZ")]
        public bool Arroz { get; set; }

        [Display(Name = "HAMBURGUESAS")]
        public bool Hamburgesas { get; set; }

        [Display(Name = "ENSALADA")]
        public bool Ensalada { get; set; }

        [Display(Name = "PET DE 2 LITROS")]
        public bool PET2Litros { get; set; }

        [Display(Name = "POSTRES")]
        public bool Postres { get; set; }

        [Display(Name = "BISQUET MIEL")]
        public bool BisquetMiel { get; set; }


    }
}