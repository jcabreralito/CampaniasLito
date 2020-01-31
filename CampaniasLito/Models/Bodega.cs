using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    public class Bodega
    {
        [Key]
        public int BodegaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Compañia")]
        [Index("Bodega_CompañiaId_Nombre_Index", 2, IsUnique = true)]
        public int CompañiaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Compañia")]
        [Index("Bodega_CompañiaId_Nombre_Index", 1, IsUnique = true)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Calle")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Número Exterior")]
        public string NumeroExterior { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "Número Interior")]
        public string NumeroInterior { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar un Estado")]
        [Display(Name = "Estado", Prompt ="[Seleccionar un Estado...]")]
        public int CiudadId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Delegación o Municipio", Prompt ="[Seleccionar una Delegación o Municipio...]")]
        public int MunicipioDelegacionId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Colonia", Prompt ="[Seleccionar una Colonia...]")]
        public int ColoniaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Display(Name = "C. P.")]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(20, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [MaxLength(20, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono2 { get; set; }

        [Display(Name = "Teléfonos")]
        public string Telefonos { get { return string.Format("{0} / {1}", Telefono, Telefono2); } }

        public virtual Compañia Compañia { get; set; }

    }
}