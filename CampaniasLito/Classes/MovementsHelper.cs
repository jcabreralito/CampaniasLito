using CampaniasLito.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace CampaniasLito.Classes
{
    public class MovementsHelper : IDisposable
    {
        private static CampaniasLitoContext db = new CampaniasLitoContext();

        public void Dispose()
        {
            db.Dispose();
        }
        public static Response AgregarTiendas(int compañia, string userName, int id)
        {

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    var campañas = db.Campañas.Where(cdt => cdt.CampañaId == id).FirstOrDefault();

                    if (campañas != null)
                    {
                        var nuevaCampaña = db.NuevaCampañaViews.Where(cdt => cdt.CampañaId == campañas.CampañaId).FirstOrDefault();

                        if (nuevaCampaña == null)
                        {
                            var nuevaCampañaAdd = new NuevaCampañaView
                            {
                                CampañaId = campañas.CampañaId,
                                Descripcion = campañas.Descripcion,
                                Nombre = campañas.Nombre,
                            };

                            db.NuevaCampañaViews.Add(nuevaCampañaAdd);
                        }
                    }

                    var tiendas = db.Tiendas.Where(cdt => cdt.CompañiaId == compañia).ToList();

                    foreach (var tienda in tiendas)
                    {
                        var tiendasTMPs = db.CampañaTiendaTMPs.Where(cdt => cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                        if (tiendasTMPs == null)
                        {
                            var tiendaDetalle = new CampañaTiendaTMP
                            {
                                Compañia = compañia,
                                Usuario = userName,
                                TiendaId = tienda.TiendaId,
                                Seleccionada = true,
                            };

                            db.CampañaTiendaTMPs.Add(tiendaDetalle);
                        }

                    }

                    db.SaveChanges();
                    transaccion.Commit();

                    return new Response { Succeeded = true, };
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    return new Response
                    {
                        Message = ex.Message,
                        Succeeded = false,
                    };
                }
            }
        }

        public static Response AgregarArticulosTiendas(int compañiaId, string nombreUsuario, int id, int campId)
        {
            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {

                    var articulos = db.ArticuloKFCs.Where(cdt => cdt.CompañiaId == compañiaId).ToList();


                    foreach (var articulo in articulos)
                    {
                        var articulosTMPs = db.CampañaArticuloTMPs.Where(cdt => cdt.ArticuloKFCId == articulo.ArticuloKFCId && cdt.TiendaId == id && cdt.CampañaTiendaTMPId == campId).FirstOrDefault();

                        if (articulosTMPs == null)
                        {
                            var articuloDetalle = new CampañaArticuloTMP
                            {
                                Compañia = compañiaId,
                                Usuario = nombreUsuario,
                                TiendaId = id,
                                ArticuloKFCId = articulo.ArticuloKFCId,
                                Cantidad = 1,
                                Habilitado = true,
                                CampañaTiendaTMPId = campId,
                            };

                            db.CampañaArticuloTMPs.Add(articuloDetalle);
                        }

                    }


                    db.SaveChanges();
                    transaccion.Commit();

                    return new Response { Succeeded = true, };
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    return new Response
                    {
                        Message = ex.Message,
                        Succeeded = false,
                    };
                }
            }
        }

        public static Response NuevaCampaña(NuevaCampañaView view, string userName, int compañia)
        {

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    var campaña = new Campaña
                    {
                        CampañaId = compañia,
                        Descripcion = view.Descripcion,
                        Generada = "NO",
                        Nombre = view.Nombre,
                    };

                    db.Campañas.Add(campaña);
                    db.SaveChanges();

                    var detalles = db.CampañaTiendaTMPs.Where(cdt => cdt.Usuario == userName).ToList();

                    foreach (var detalle in detalles)
                    {
                        var tiendaDetalle = new CampañaTienda
                        {
                            CompañiaId = detalle.Compañia,
                            CreatedDate = DateTime.Now,
                            CampañaId = campaña.CampañaId,
                            TiendaId = detalle.TiendaId,
                        };

                        db.CampañaTiendas.Add(tiendaDetalle);
                        db.CampañaTiendaTMPs.Remove(detalle);

                    }

                    db.SaveChanges();
                    transaccion.Commit();

                    return new Response { Succeeded = true, };
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    return new Response
                    {
                        Message = ex.Message,
                        Succeeded = false,
                    };
                }
            }
        }

        public static Response EditarCampaña(Campaña view)
        {
            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    db.Entry(view).State = EntityState.Modified;

                    db.SaveChanges();

                    var detalles = db.CampañaTiendas.Where(cdt => cdt.CampañaId == view.CampañaId).ToList();

                    foreach (var detalle in detalles)
                    {
                        db.Entry(detalle).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    transaccion.Commit();

                    return new Response { Succeeded = true, };
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    return new Response
                    {
                        Message = ex.Message,
                        Succeeded = false,
                    };
                }
            }
        }
    }
}