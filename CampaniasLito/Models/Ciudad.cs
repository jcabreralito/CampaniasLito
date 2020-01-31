﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CampaniasLito.Models
{
    [Table("Ciudades")]
    public class Ciudad
    {
        [Key]
        public int CiudadId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Ciudad")]
        [Index("Ciudad_CompañiaId_Nombre_Index", 2, IsUnique = true)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Región")]
        public int RegionId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Compañia")]
        [Index("Ciudad_CompañiaId_Nombre_Index", 1, IsUnique = true)]
        public int CompañiaId { get; set; }

        public virtual Compañia Compañia { get; set; }

        public virtual Region Region { get; set; }
        
        public virtual ICollection<Tienda> Tiendas { get; set; }
    }
}