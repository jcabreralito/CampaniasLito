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
                Nombre = "[Seleccionar una Región...]",
            });
            return regiones.OrderBy(r => r.Nombre).ToList();
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
                Nombre = "[Seleccionar una Ciudad...]",
                
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
    }
}