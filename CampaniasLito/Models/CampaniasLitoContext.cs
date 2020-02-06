using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CampaniasLito.Models
{
    public class CampaniasLitoContext : DbContext
    {
        public CampaniasLitoContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DbSet<Compañia> Compañias { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Rol> Roles { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Bodega> Bodegas { get; set; }

        public DbSet<Articulo> Articuloes { get; set; }

        public DbSet<Tienda> Tiendas { get; set; }

        public DbSet<Ciudad> Ciudads { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<CampañaArticulo> CampañaArticulos { get; set; }

        public DbSet<CampañaTienda> CampañaTiendas { get; set; }

        public DbSet<Campaña> Campañas { get; set; }

        public System.Data.Entity.DbSet<CampaniasLito.Models.NivelPrecio> NivelPrecios { get; set; }

        public System.Data.Entity.DbSet<CampaniasLito.Models.TipoCaja> TipoCajas { get; set; }

        public System.Data.Entity.DbSet<CampaniasLito.Models.AcomodoCaja> AcomodoCajas { get; set; }

        public System.Data.Entity.DbSet<CampaniasLito.Models.TipoTienda> TipoTiendas { get; set; }

        public System.Data.Entity.DbSet<CampaniasLito.Models.ArticuloKFC> ArticuloKFCs { get; set; }
    }
}