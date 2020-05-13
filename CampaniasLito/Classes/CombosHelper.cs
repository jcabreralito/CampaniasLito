using CampaniasLito.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampaniasLito.Classes
{
    public class CombosHelper : IDisposable
    {
        private static readonly CampaniasLitoContext db = new CampaniasLitoContext();

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
                Nombre = "[Seleccionar...]",
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
                Nombre = "[Seleccionar...]",
            });
            return roles.OrderBy(r => r.Nombre).ToList();
        }

        public static List<Region> GetRegiones(int equiFran)
        {
            var tipo = string.Empty;

            if (equiFran == 1)
            {
                tipo = "EQUITY";
            }
            else if (equiFran == 2)
            {
                tipo = "FRANQUICIAS";
            }
            else if (equiFran == 3)
            {
                tipo = "STOCK";
            }

            var regiones = db.Regions.Where(c => c.EquityFranquicia == tipo).ToList();
            regiones.Add(new Region
            {
                RegionId = 0,
                Nombre = "[Seleccionar...]",
            });
            return regiones.OrderBy(r => r.RegionId).ToList();
        }

        public static List<Region> GetRegiones(bool sw)
        {
            var regiones = db.Regions.ToList();
            return regiones.OrderBy(r => r.Nombre).ToList();
        }

        public static List<Region> GetRegiones(int equiFran, bool sw)
        {
            var tipo = string.Empty;

            if (equiFran == 1)
            {
                tipo = "EQUITY";
            }
            else if (equiFran == 2)
            {
                tipo = "FRANQUICIAS";
            }
            else if (equiFran == 3)
            {
                tipo = "STOCK";
            }

            var regiones = db.Regions.Where(c => c.EquityFranquicia == tipo).ToList();
            return regiones.OrderBy(r => r.Nombre).ToList();
        }

        public static List<TipoConfiguracion> GetTipoConfiguracion(bool v)
        {
            var configuraciones = db.TipoConfiguracions.ToList();
            return configuraciones.OrderBy(r => r.Nombre).ToList();
        }

        public static List<Ciudad> GetCiudades(int equiFran)
        {
            var tipo = string.Empty;

            if (equiFran == 1)
            {
                tipo = "EQUITY";
            }
            else if (equiFran == 2)
            {
                tipo = "FRANQUICIAS";
            }
            else if (equiFran == 3)
            {
                tipo = "STOCK";
            }

            var ciudades = db.Ciudads.Where(c => c.EquityFranquicia == tipo).ToList();
            ciudades.Add(new Ciudad
            {
                CiudadId = 0,
                Nombre = "[Seleccionar...]",

            });

            return ciudades.OrderBy(r => r.Nombre).ToList();
        }

        public static List<Ciudad> GetCiudades(bool sw)
        {
            var ciudades = db.Ciudads.ToList();
            return ciudades.OrderBy(r => r.Nombre).ToList();
        }

        public static List<Ciudad> GetCiudades(int equiFran, bool sw)
        {
            var tipo = string.Empty;

            if (equiFran == 1)
            {
                tipo = "EQUITY";
            }
            else if (equiFran == 2)
            {
                tipo = "FRANQUICIAS";
            }
            else if (equiFran == 3)
            {
                tipo = "STOCK";
            }

            var ciudades = db.Ciudads.Where(c => c.EquityFranquicia == tipo).ToList();
            return ciudades.OrderBy(r => r.Nombre).ToList();
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

        public static List<Familia> GetFamilias(bool v)
        {
            var familias = db.Familias.ToList();
            return familias.OrderBy(c => c.Codigo).ToList();
        }

        public static List<Familia> GetFamilias()
        {
            var familias = db.Familias.ToList();
            familias.Add(new Familia
            {
                FamiliaId = 0,
                Descripcion = "[Seleccionar...]",
            });
            return familias.OrderBy(c => c.Codigo).ToList();
        }

        public static List<TipoCaja> GetTiposCaja(bool v)
        {
            var tiposCaja = db.TipoCajas.ToList();
            tiposCaja.Add(new TipoCaja
            {
                TipoCajaId = 0,
                Descripcion = "[Seleccionar...]",
            });
            return tiposCaja.OrderBy(c => c.Descripcion).ToList();
        }

        public static List<TipoCaja> GetTiposCaja()
        {
            var tiposCaja = db.TipoCajas.ToList();
            tiposCaja.Add(new TipoCaja
            {
                TipoCajaId = 0,
                Descripcion = "[Seleccionar...]",
            });
            return tiposCaja.OrderBy(c => c.Descripcion).ToList();
        }

        public static List<AcomodoCaja> GetAcomodoCajas(bool v)
        {
            var acomodos = db.AcomodoCajas.ToList();
            acomodos.Add(new AcomodoCaja
            {
                AcomodoCajaId = 0,
                Descripcion = "[Seleccionar...]",
            });
            return acomodos.OrderBy(c => c.Descripcion).ToList();
        }

        public static List<AcomodoCaja> GetAcomodoCajas()
        {
            var acomodos = db.AcomodoCajas.ToList();
            acomodos.Add(new AcomodoCaja
            {
                AcomodoCajaId = 0,
                Descripcion = "[Seleccionar...]",
            });
            return acomodos.OrderBy(c => c.Descripcion).ToList();
        }

        public static List<TipoTienda> GetTiposTienda()
        {
            var tipos = db.TipoTiendas.ToList();
            tipos.Add(new TipoTienda
            {
                TipoTiendaId = 0,
                Tipo = "[Seleccionar...]",
            });
            return tipos.OrderBy(c => c.Tipo).ToList();
        }

        public static List<NivelPrecio> GetNivelesPrecio()
        {
            var niveles = db.NivelPrecios.ToList();
            niveles.Add(new NivelPrecio
            {
                NivelPrecioId = 0,
                Descripcion = "[Seleccionar...]",
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