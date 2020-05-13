using System.ComponentModel.DataAnnotations;

namespace CampaniasLito.Models
{
    public class TiendaMatEspecifico
    {
        [Key]
        public int TiendaMatEspecificoId { get; set; }

        public int TiendaId { get; set; }

        [Display(Name = "MENÚ BACKLIGHT")]
        public bool MenuBackLigth { get; set; }

        [Display(Name = "AUTOEXPRESS")]
        public bool Autoexpress { get; set; }

        [Display(Name = "COPETE AE REMODELADO")]
        public bool CopeteAERemodelado { get; set; }

        [Display(Name = "COPETE AE TRADICIONAL")]
        public bool CopeteAETradicional { get; set; }

        [Display(Name = "PANEL DE INNNOVACIÓN")]
        public bool PanelDeInnovacion { get; set; }

        [Display(Name = "DISPLAY DE BURBUJA")]
        public bool DisplayDeBurbuja { get; set; }

        [Display(Name = "DELIVERY")]
        public bool Delivery { get; set; }

        [Display(Name = "MERCADO DE PRUEBA")]
        public bool MERCADO_DE_PRUEBA { get; set; }

        [Display(Name = "AREA DE JUEGOS")]
        public bool AreaDeJuegos { get; set; }

    }
}