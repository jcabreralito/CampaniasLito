using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace CampaniasLito.Models
{
    [Table("Compañias")]
    public class Compañia
    {
        [Key]
        public int CompañiaId { get; set; }

        [Required(ErrorMessage = "El Campo {0} es obligatorio")]
        [MaxLength(50, ErrorMessage = "El Campo {0} debe tener máximo {1} carácteres de largo")]
        [Display(Name = "Compañia")]
        [Index("Compañia_Nombre_Index", IsUnique = true)]
        public string Nombre { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Logo")]
        public string Logo { get; set; }

        [NotMapped]
        [Display(Name = "Logo")]
        public HttpPostedFileBase LogoFile { get; set; }

        public virtual ICollection<Categoria> Categorias { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; }

        public virtual ICollection<Bodega> Bodegas { get; set; }

        public virtual ICollection<Region> Regiones { get; set; }

        public virtual ICollection<Ciudad> Ciudades { get; set; }

        public virtual ICollection<Campaña> Campañas { get; set; }

        public virtual ICollection<CampañaTienda> CampañaTiendas { get; set; }

        public virtual ICollection<CampañaArticulo> CampañaArticulos { get; set; }

        public virtual ICollection<ArticuloKFC> ArticulosKFC { get; set; }

    }
}