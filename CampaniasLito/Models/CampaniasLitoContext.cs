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

        public DbSet<NivelPrecio> NivelPrecios { get; set; }

        public DbSet<TipoCaja> TipoCajas { get; set; }

        public DbSet<AcomodoCaja> AcomodoCajas { get; set; }

        public DbSet<TipoTienda> TipoTiendas { get; set; }

        public DbSet<ArticuloKFC> ArticuloKFCs { get; set; }

        public DbSet<CampañaTiendaTMP> CampañaTiendaTMPs { get; set; }

        public DbSet<NuevaCampañaView> NuevaCampañaViews { get; set; }

        public DbSet<CampañaArticuloTMP> CampañaArticuloTMPs { get; set; }
    }
}