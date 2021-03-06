﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("ReglasCatalogo")]
    public class ReglaCatalogo
    {
        [Key]
        public int ReglaCatalogoId { get; set; }

        [MaxLength(250, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Index("IX_Nombre_Valor_Categoria", 1, IsUnique = true)]
        public string Nombre { get; set; }

        [MaxLength(250, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Index("IX_Nombre_Valor_Categoria", 2, IsUnique = true)]
        public string Valor { get; set; }

        public bool SiNo { get; set; }

        public bool Activo { get; set; }

        [MaxLength(250, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Index("IX_Nombre_Valor_Categoria", 3, IsUnique = true)]
        public string Categoria { get; set; }

        public bool Equity { get; set; }

        public bool Franquicias { get; set; }

        public bool Stock { get; set; }

        public int TipoConfiguracionId { get; set; }

        public virtual TipoConfiguracion TipoConfiguracion { get; set; }
    }
}