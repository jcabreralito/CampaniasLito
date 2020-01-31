using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CampaniasLito.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(256, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "E-Mail")]
        [Index("Usuario_NombreUsuario_Index", IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Nombre(s)")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Calle")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Número Exterior")]
        public string NumeroExterior { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Número Interior")]
        public string NumeroInterior { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Estado", Prompt = "[Seleccionar una Ciudad...]")]
        public int CiudadId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Municipio o Delegación", Prompt = "[Seleccionar una Delegación o Municipio...]")]
        public int MunicipioDelegacionId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Colonia", Prompt = "[Seleccionar una Colonia...]")]
        public int ColoniaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Codigo Postal")]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(20, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(20, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Movil")]
        [DataType(DataType.PhoneNumber)]
        public string Movil { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Rol", Prompt = "[Seleccionar un Rol...]")]
        public int RolId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar una {0}")]
        [Display(Name = "Compañia", Prompt = "[Seleccionar una Compañias...]")]
        public int CompañiaId { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Foto")]
        public string Foto { get; set; }

        [Display(Name = "Usuario")]
        public string NombreCompleto { get { return string.Format("{0} {1}", Nombres, Apellidos); } }

        [Display(Name = "Teléfonos")]
        public string Telefonos { get { return string.Format("{0} / {1}", Telefono, Movil); } }

        [NotMapped]
        [Display(Name = "Foto")]
        public HttpPostedFileBase FotoFile { get; set; }

        public virtual Rol Rol { get; set; }

        public virtual Compañia Compañia { get; set; }
    }
}