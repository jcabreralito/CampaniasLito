﻿using System.Data.Entity;
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

        public DbSet<Rol> Roles { get; set; }

        public DbSet<Tienda> Tiendas { get; set; }

        public DbSet<Ciudad> Ciudads { get; set; }

        public DbSet<Region> Regions { get; set; }

        //public DbSet<CampañaArticulo> CampañaArticulos { get; set; }

        public DbSet<Campaña> Campañas { get; set; }

        public DbSet<NivelPrecio> NivelPrecios { get; set; }

        public DbSet<TipoCaja> TipoCajas { get; set; }

        public DbSet<AcomodoCaja> AcomodoCajas { get; set; }

        public DbSet<TipoTienda> TipoTiendas { get; set; }

        public DbSet<ArticuloKFC> ArticuloKFCs { get; set; }

        public DbSet<CampañaTiendaTMP> CampañaTiendaTMPs { get; set; }

        public DbSet<CampañaArticuloTMP> CampañaArticuloTMPs { get; set; }

        public DbSet<Proveedor> Proveedors { get; set; }

        public DbSet<Modulo> Modulos { get; set; }

        public DbSet<Operacion> Operaciones { get; set; }

        public DbSet<RolOperacion> RolOperaciones { get; set; }

        public DbSet<TiendaArticulo> TiendaArticulos { get; set; }

        public DbSet<CodigoCampaña> CodigosCampaña { get; set; }

        public DbSet<Familia> Familias { get; set; }

        public DbSet<TiendaEquipo> TiendaEquipos { get; set; }

        public DbSet<TiendaGeneral> TiendaGenerales { get; set; }

        public DbSet<TiendaMatEspecifico> TiendaMatEspecificos { get; set; }

        public DbSet<TiendaMedidaEspecial> TiendaMedidaEspeciales { get; set; }

        public DbSet<TiendaProducto> TiendaProductos { get; set; }

        public DbSet<FamiliaTienda> FamiliaTiendas { get; set; }

        public DbSet<TiendaConfiguracion> TiendaConfiguracions { get; set; }

        public DbSet<TipoConfiguracion> TipoConfiguracions { get; set; }

        public DbSet<AsignarConfiguracionTienda> AsignarConfiguracionTiendas { get; set; }
        public DbSet<Regla> Reglas { get; set; }

    }
}