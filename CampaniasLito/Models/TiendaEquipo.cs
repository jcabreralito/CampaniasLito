using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CampaniasLito.Models
{
    public class TiendaEquipo
    {
        [Key]
        public int TiendaEquipoId { get; set; }

        public int TiendaId { get; set; }

        [Display(Name = "TIPO DE CAJA", Prompt = "[Tipo Caja...]")]
        public int TipoDeCajaId { get; set; }

        [Display(Name = "ACOMODO DE CAJA", Prompt = "[Acomodo...]")]
        public string AcomodoDeCajas { get; set; }

        [Display(Name = "# MESA AREAS DE COMEDOR")]
        public string NoMesaDeAreaComedor { get; set; }

        [Display(Name = "# MESA AREAS DE JUEGO")]
        public string NoMesaDeAreaDeJuegos { get; set; }

        [Display(Name = "NUMERO DE VENTANAS")]
        public string NumeroDeVentanas { get; set; }


    }
}