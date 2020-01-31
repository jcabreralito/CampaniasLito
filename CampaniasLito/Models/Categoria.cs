using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CampaniasLito.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Categoria")]
        [Index("Categoria_CompañiaId_Descripcion_Index", 2, IsUnique = true)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Compañia")]
        [Index("Categoria_CompañiaId_Descripcion_Index", 1, IsUnique = true)]
        public int CompañiaId { get; set; }

        public virtual Compañia Compañia { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}