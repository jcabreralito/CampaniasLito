﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("ReglaMateriales")]
    public class ReglaMaterial
    {
        public int ReglaMaterialId { get; set; }

        public int ReglaId { get; set; }

        [Display(Name = "Material", Prompt = "[Seleccionar...]")]
        public int ArticuloKFCId { get; set; }

        public virtual ArticuloKFC ArticuloKFC { get; set; }

        public virtual Regla Regla { get; set; }

    }
}