using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CampaniasLito.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Compañia")]
        [Index("Producto_CompañiaId_Descripcion_Index", 1, IsUnique = true)]
        public int CompañiaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Producto")]
        [Index("Producto_CompañiaId_Descripcion_Index", 2, IsUnique = true)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(13, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Código de Barras")]
        [Index("Producto_CompañiaId_CodigoBarras_Index", 2, IsUnique = true)]
        public string CodigoBarras { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Categoria", Prompt ="[Seleccionar una Categoria...]")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Impuesto", Prompt = "[Seleccionar un Impuesto...]")]
        public int ImpuestoId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "Seleccionar un {0} entre {1} y {2}")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string Imagen { get; set; }

        [NotMapped]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase ImagenFile { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comentarios { get; set; }

        public virtual Compañia Compañia { get; set; }

        public virtual Categoria Categoria { get; set; }

    }
}