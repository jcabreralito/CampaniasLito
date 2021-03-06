﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("Ciudades")]
    public class Ciudad
    {
        [Key]
        public int CiudadId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Ciudad")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Región", Prompt = "[Seleccionar una Región...]")]
        public int RegionId { get; set; }

        public string EquityFranquicia { get; set; }

        public virtual Region Region { get; set; }

    }
}