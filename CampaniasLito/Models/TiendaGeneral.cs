using System.ComponentModel.DataAnnotations;

namespace CampaniasLito.Models
{
    public class TiendaGeneral
    {
        [Key]
        public int TiendaGeneralId { get; set; }

        public int TiendaId { get; set; }

        [Display(Name = "TIPO", Prompt = "[Tipo...]")]
        public int TipoId { get; set; }

        [Display(Name = "NUEVO NIVEL DE PRECIO", Prompt = "[Nivel Precio...]")]
        public int NuevoNivelDePrecioId { get; set; }

        [Display(Name = "MENÚ DIGITAL")]
        public bool MenuDigital { get; set; }

        [Display(Name = "CANTIDAD DE PANTALLAS")]
        public string CantidadDePantallas { get; set; }


    }
}