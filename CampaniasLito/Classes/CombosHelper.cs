using CampaniasLito.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampaniasLito.Classes
{
    public class CombosHelper : IDisposable
    {
        private static CampaniasLitoContext db = new CampaniasLitoContext();

        public static List<Compañia> GetCompañias(bool sw)
        {
            var compañias = db.Compañias.ToList();
            return compañias.OrderBy(c => c.Nombre).ToList();
        }

        public static List<Compañia> GetCompañias()
        {
            var compañias = db.Compañias.ToList();
            compañias.Add(new Compañia
            {
                CompañiaId = 0,
                Nombre = "[Seleccionar una Compañia...]",
            });
            return compañias.OrderBy(c => c.Nombre).ToList();
        }

        public static List<Rol> GetRoles(bool sw)
        {
            var roles = db.Roles.ToList();
            return roles.OrderBy(r => r.Nombre).ToList();
        }

        public static List<Rol> GetRoles()
        {
            var roles = db.Roles.ToList();
            roles.Add(new Rol
            {
                RolId = 0,
                Nombre = "[Seleccionar un Rol...]",
            });
            return roles.OrderBy(r => r.Nombre).ToList();
        }

        public static List<Region> GetRegiones(int compañiaId)
        {
            var regiones = db.Regions.Where(c => c.CompañiaId == compañiaId).ToList();
            regiones.Add(new Region
            {
                RegionId = 0,
                Nombre = "[Región...]",
            });
            return regiones.OrderBy(r => r.RegionId).ToList();
        }

        public static List<Region> GetRegiones(int compañiaId, bool sw)
        {
            var regiones = db.Regions.Where(c => c.CompañiaId == compañiaId).ToList();
            return regiones.OrderBy(r => r.Nombre).ToList();
        }

        public static List<Ciudad> GetCiudades(int compañiaId)
        {
            var ciudades = db.Ciudads.Where(c => c.CompañiaId == compañiaId).ToList();
            ciudades.Add(new Ciudad
            {
                CiudadId = 0,
                Nombre = "[Ciudad]",
                
            });

            return ciudades.OrderBy(r => r.Nombre).ToList();
        }

        public static List<Ciudad> GetCiudades(int compañiaId, bool sw)
        {
            var ciudades = db.Ciudads.Where(c => c.CompañiaId == compañiaId).ToList();
            return ciudades.OrderBy(r => r.Nombre).ToList();
        }

        public static List<Categoria> GetCategorias(int compañiaId, bool sw)
        {
            var categorias = db.Categorias.Where(c => c.CompañiaId == compañiaId).ToList();
            return categorias.OrderBy(c => c.Descripcion).ToList();
        }

        public static List<Categoria> GetCategorias(int compañiaId)
        {
            var categorias = db.Categorias.Where(c => c.CompañiaId == compañiaId).ToList();
            categorias.Add(new Categoria
            {
                CategoriaId = 0,
                Descripcion = "[Seleccionar una Categoria...]",
            });
            return categorias.OrderBy(c => c.Descripcion).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public static List<TipoTienda> GetTiposTienda(bool v)
        {
            var tipos = db.TipoTiendas.ToList();
            return tipos.OrderBy(c => c.Tipo).ToList();
        }

        public static List<TipoCaja> GetTiposCaja(bool v)
        {
            var tiposCaja = db.TipoCajas.ToList();
            return tiposCaja.OrderBy(c => c.Descripcion).ToList();
        }

        public static List<TipoCaja> GetTiposCaja()
        {
            var tiposCaja = db.TipoCajas.ToList();
            tiposCaja.Add(new TipoCaja
            {
                TipoCajaId = 0,
                Descripcion = "[Tipo de Caja]",
            });
            return tiposCaja.OrderBy(c => c.Descripcion).ToList();
        }

        public static List<AcomodoCaja> GetAcomodoCajas(bool v)
        {
            var acomodos = db.AcomodoCajas.ToList();
            return acomodos.OrderBy(c => c.Descripcion).ToList();
        }

        public static List<AcomodoCaja> GetAcomodoCajas()
        {
            var acomodos = db.AcomodoCajas.ToList();
            acomodos.Add(new AcomodoCaja
            {
                AcomodoCajaId = 0,
                Descripcion = "[Acomodo Caja]",
            });
            return acomodos.OrderBy(c => c.Descripcion).ToList();
        }

        public static List<TipoTienda> GetTiposTienda()
        {
            var tipos = db.TipoTiendas.ToList();
            tipos.Add(new TipoTienda
            {
                TipoTiendaId = 0,
                Tipo = "[Tipo]",
            });
            return tipos.OrderBy(c => c.Tipo).ToList();
        }

        public static List<NivelPrecio> GetNivelesPrecio()
        {
            var niveles = db.NivelPrecios.ToList();
            niveles.Add(new NivelPrecio
            {
                NivelPrecioId = 0,
                Descripcion = "[Nivel de Precios]",
            });
            return niveles.OrderBy(c => c.Descripcion).ToList();
        }

        public static List<NivelPrecio> GetNivelesPrecio(bool sw)
        {
            var niveles = db.NivelPrecios.ToList();
            return niveles.OrderBy(c => c.Descripcion).ToList();
        }

        public static List<Proveedor> GetProveedores(bool sw)
        {
            var proveedores = db.Proveedors.ToList();
            return proveedores.OrderBy(c => c.Nombre).ToList();
        }

    }
}