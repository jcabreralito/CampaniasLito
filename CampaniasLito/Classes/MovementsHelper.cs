using CampaniasLito.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CampaniasLito.Classes
{
    public class MovementsHelper : IDisposable
    {
        private static CampaniasLitoContext db = new CampaniasLitoContext();

        public void Dispose()
        {
            db.Dispose();
        }

        public static Response AgregarArticulosNuevaCampaña(int campañaid)
        {
            var cantidadDefault = 0;

            var codigo = 0;

            var materiales = db.ArticuloKFCs.ToList();

            var campañaIdActual = campañaid;

            //var articulosCampañas = db.CampañaArticuloTMPs.Where(c => c.CampañaId == campañaIdActual).ToList();

            //if (articulosCampañas.Count == 0)
            //{
            //var materialesPrimerCampaña = db.TiendaArticulos.Where(x => x.TiendaArticuloId >= 19441).ToList();

            var materialesPrimerCampaña = db.TiendaArticulos.ToList();

            foreach (var materialCampaña in materialesPrimerCampaña)
            {
                if (materialCampaña.Seleccionado == true)
                {
                    //  modelBuilder
                    //.Entity<Blog>()
                    //.MapToStoredProcedures(s =>
                    //  s.Update(u => u.HasName("modify_blog")
                    //                 .Parameter(b => b.BlogId, "blog_id")
                    //                 .Parameter(b => b.Name, "blog_name")
                    //                 .Parameter(b => b.Url, "blog_url"))
                    //   .Delete(d => d.HasName("delete_blog")
                    //                 .Parameter(b => b.BlogId, "blog_id"))
                    //   .Insert(i => i.HasName("insert_blog")
                    //                 .Parameter(b => b.Name, "blog_name")
                    //                 .Parameter(b => b.Url, "blog_url")));

                    //var material = db.Database.SqlQuery<CampañaArticuloTMP>(
                    //    "spAgregarMaterialCAmpanias @ArticuloKFCId, @TiendaId, @CampañaId, @Habilitado, @Cantidad, @Codigo",
                    //    new SqlParameter("@ArticuloKFCId", materialCampaña.ArticuloKFCId),
                    //    new SqlParameter("@TiendaId", materialCampaña.TiendaId),
                    //    new SqlParameter("@CampañaId", campañaid),
                    //    new SqlParameter("@Habilitado", materialCampaña.Seleccionado),
                    //    new SqlParameter("@Cantidad", materialCampaña.ArticuloKFC.CantidadDefault),
                    //    new SqlParameter("@Codigo", 0));

                    db.Database.ExecuteSqlCommand(
                        "spAgregarMaterialCAmpanias @ArticuloKFCId, @TiendaId, @CampañaId, @Habilitado, @Cantidad, @Codigo",
                        new SqlParameter("@ArticuloKFCId", materialCampaña.ArticuloKFCId),
                        new SqlParameter("@TiendaId", materialCampaña.TiendaId),
                        new SqlParameter("@CampañaId", campañaid),
                        new SqlParameter("@Habilitado", materialCampaña.Seleccionado),
                        new SqlParameter("@Cantidad", materialCampaña.ArticuloKFC.CantidadDefault),
                        new SqlParameter("@Codigo", codigo));

                    //var campañaDetalle = new CampañaArticuloTMP
                    //{
                    //    ArticuloKFCId = materialCampaña.ArticuloKFCId,
                    //    Cantidad = materialCampaña.ArticuloKFC.CantidadDefault,
                    //    Habilitado = materialCampaña.Seleccionado,
                    //    CampañaId = campañaid,
                    //    TiendaId = materialCampaña.TiendaId,
                    //    Codigo = 0,
                    //};

                    //db.CampañaArticuloTMPs.Add(campañaDetalle);
                }
                else
                {
                    db.Database.ExecuteSqlCommand(
                        "spAgregarMaterialCAmpanias @ArticuloKFCId, @TiendaId, @CampañaId, @Habilitado, @Cantidad, @Codigo",
                        new SqlParameter("@ArticuloKFCId", materialCampaña.ArticuloKFCId),
                        new SqlParameter("@TiendaId", materialCampaña.TiendaId),
                        new SqlParameter("@CampañaId", campañaid),
                        new SqlParameter("@Habilitado", materialCampaña.Seleccionado),
                        new SqlParameter("@Cantidad", cantidadDefault),
                        new SqlParameter("@Codigo", codigo));

                    //var lects = db.Database.SqlQuery<CampañaArticuloTMP>("spAgregarMaterialCAmpanias @fecha,@idParteMaquina,@inicio,@inicioMasDuracion,@dia", fecha, partemaquina.Id, inicio.TotalSeconds, inicioMasDuracion.TotalSeconds, dia);

                    //var campañaDetalle = new CampañaArticuloTMP
                    //{
                    //    ArticuloKFCId = materialCampaña.ArticuloKFCId,
                    //    Cantidad = materialCampaña.ArticuloKFC.CantidadDefault,
                    //    Habilitado = materialCampaña.Seleccionado,
                    //    CampañaId = campañaid,
                    //    TiendaId = materialCampaña.TiendaId,
                    //    Codigo = 0,
                    //};

                    //db.CampañaArticuloTMPs.Add(campañaDetalle);
                }


            }
            //}
            //else
            //{

            //}

            //var tiendas = db.Tiendas.ToList();

            //foreach (var tienda in tiendas)
            //{
            //    var tiendasTMPs = db.TiendaArticulos.Where(cdt => cdt.TiendaId == tienda.TiendaId && cdt.Seleccionado == true).FirstOrDefault();
            //    //var tiendasTMPs = db.CampañaTiendaTMPs.Where(cdt => cdt.TiendaId == tienda.TiendaId && cdt.CampañaId == campañaid).FirstOrDefault();

            //    if (tiendasTMPs != null)
            //    {
            //        var tiendasSeleccionadas = db.CampañaTiendaTMPs.Where(cdt => cdt.TiendaId == tiendasTMPs.TiendaId && cdt.CampañaId == campañaid).FirstOrDefault();

            //        if (tiendasSeleccionadas == null)
            //        {
            //            var tiendaDetalle = new CampañaTiendaTMP
            //            {
            //                CampañaId = campañaid,
            //                TiendaId = tienda.TiendaId,
            //                Seleccionada = false,
            //            };

            //            db.CampañaTiendaTMPs.Add(tiendaDetalle);
            //        }
            //    }

            //    //var articulos = db.ArticuloKFCs.Where(cdt => cdt.CompañiaId == compañia).ToList();

            //    //var articulosTMP = db.CampañaArticuloTMPs.Where(cdt => cdt.TiendaId == tienda.TiendaId && cdt.CampañaTiendaTMPId == campañas.CampañaId).ToList();

            //    //if (articulos.Count != articulosTMP.Count)
            //    //{
            //    //    foreach (var articulo in articulos)
            //    //    {
            //    //        var articulosTMPs = db.CampañaArticuloTMPs.Where(cdt => cdt.ArticuloKFCId == articulo.ArticuloKFCId && cdt.TiendaId == tienda.TiendaId && cdt.CampañaTiendaTMPId == campañas.CampañaId).FirstOrDefault();

            //    //        if (articulosTMPs == null)
            //    //        {
            //    //            var articuloDetalle = new CampañaArticuloTMP
            //    //            {
            //    //                Compañia = compañia,
            //    //                Usuario = userName,
            //    //                TiendaId = tienda.TiendaId,
            //    //                ArticuloKFCId = articulo.ArticuloKFCId,
            //    //                Cantidad = articulo.CantidadDefault,
            //    //                Habilitado = true,
            //    //                CampañaTiendaTMPId = campañas.CampañaId,
            //    //            };

            //    //            db.CampañaArticuloTMPs.Add(articuloDetalle);
            //    //        }

            //    //    }
            //    //}

            //}


            //using (var transaccion = db.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        //db.SaveChanges();
            //        transaccion.Commit();

            //        return new Response { Succeeded = true, };
            //    }
            //    catch (Exception ex)
            //    {
            //        transaccion.Rollback();
            //        return new Response
            //        {
            //            Message = ex.Message,
            //            Succeeded = false,
            //        };
            //    }
            //}

            return new Response { Succeeded = true, };
        }

        public static Response AgregarArticulosTiendas(int tiendaId, int campId)
        {
            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    List<TiendaArticulo> tiendaArticulos = db.TiendaArticulos.Where(t => t.TiendaId == tiendaId).ToList();

                    foreach (var item in tiendaArticulos)
                    {
                        var articuloId = item.ArticuloKFCId;

                        CampañaArticuloTMP campañaArticuloTMP = db.CampañaArticuloTMPs.Where(ca => ca.ArticuloKFCId == articuloId && ca.TiendaId == tiendaId && ca.CampañaId == campId).SingleOrDefault();

                        var habilitado = false;
                        var seleccionado = item.Seleccionado;
                        var cantidad = item.ArticuloKFC.CantidadDefault;

                        if (campañaArticuloTMP == null)
                        {
                            var articuloDetalle = new CampañaArticuloTMP
                            {
                                TiendaId = tiendaId,
                                ArticuloKFCId = articuloId,
                                Cantidad = cantidad,
                                Habilitado = seleccionado,
                                CampañaId = campId,
                            };

                            db.CampañaArticuloTMPs.Add(articuloDetalle);
                            db.SaveChanges();

                        }
                        else
                        {
                            habilitado = campañaArticuloTMP.Habilitado;

                            if (habilitado != seleccionado)
                            {
                                if (seleccionado == false)
                                {
                                    campañaArticuloTMP.Habilitado = false;
                                    campañaArticuloTMP.Cantidad = 0;
                                }
                                else
                                {
                                    campañaArticuloTMP.Habilitado = seleccionado;
                                    campañaArticuloTMP.Cantidad = cantidad;
                                }

                                db.Entry(campañaArticuloTMP).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }

                    }







                    //var articulos = db.ArticuloKFCs.Where(cdt => cdt.CompañiaId == compañiaId).ToList();

                    //foreach (var articulo in articulos)
                    //{
                    //    int cantidad = 0;
                    //    bool habilitado = false;

                    //    for (int i = 0; i < articulos.Count(); i++)
                    //    {
                    //        var habilitados = articulos[i].CampañaArticuloTMPs.FirstOrDefault().Habilitado;
                    //        var seleccionados = articulos[i].TiendaArticulos.FirstOrDefault().Seleccionado;

                    //        if (habilitados != seleccionados)
                    //        {
                    //            var articuloId = articulos[i].CampañaArticuloTMPs.FirstOrDefault().CampañaArticuloTMPId;

                    //            var articulosTMPsId = db.CampañaArticuloTMPs.Where(cdt => cdt.CampañaArticuloTMPId == articuloId).FirstOrDefault();

                    //            if (articulos[i].TiendaArticulos.FirstOrDefault().Seleccionado == true)
                    //            {
                    //                cantidad = articulo.CantidadDefault;
                    //                habilitado = true;
                    //            }
                    //            else
                    //            {
                    //                cantidad = 0;
                    //                habilitado = false;
                    //            }

                    //            articulosTMPsId.Habilitado = habilitado;
                    //            articulosTMPsId.Cantidad = cantidad;

                    //            db.Entry(articulosTMPsId).State = EntityState.Modified;
                    //            db.SaveChanges();
                    //        }
                    //    }



                    //List<CampañaArticuloTMP> articulosTMPs = db.CampañaArticuloTMPs.Where(cdt => cdt.ArticuloKFCId == articulo.ArticuloKFCId && cdt.TiendaId == tiendaId && cdt.CampañaTiendaTMPId == campId).ToList();
                    //List<TiendaArticulo> articulosTiendas = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == articulo.ArticuloKFCId && cdt.TiendaId == tiendaId).ToList();


                    //if (articulosTMPs != null)
                    //{
                    //    var habilitados = articulosTMPs.FirstOrDefault().Habilitado;
                    //    var seleccionados = articulosTiendas.FirstOrDefault().Seleccionado;


                    //    if (habilitados != seleccionados)
                    //    {

                    //        var articulosTMPsId = db.CampañaArticuloTMPs.Where(cdt => cdt.CampañaArticuloTMPId == articulosTMPs.FirstOrDefault().CampañaArticuloTMPId).FirstOrDefault();

                    //        if (articulosTiendas.FirstOrDefault().Seleccionado == true)
                    //        {
                    //            cantidad = articulo.CantidadDefault;
                    //            habilitado = true;
                    //        }
                    //        else
                    //        {
                    //            cantidad = 0;
                    //            habilitado = false;
                    //        }

                    //        articulosTMPsId.Habilitado = habilitado;
                    //        articulosTMPsId.Cantidad = cantidad;

                    //        db.Entry(articulosTMPsId).State = EntityState.Modified;
                    //        db.SaveChanges();
                    //    }
                    //}
                    //else if (articulosTMPs == null)
                    //{

                    //    if (articulosTiendas.FirstOrDefault().Seleccionado == true)
                    //    {
                    //        cantidad = articulo.CantidadDefault;
                    //        habilitado = true;
                    //    }
                    //    else
                    //    {
                    //        cantidad = 0;
                    //        habilitado = false;
                    //    }

                    //    var articuloDetalle = new CampañaArticuloTMP
                    //    {
                    //        Compañia = compañiaId,
                    //        Usuario = nombreUsuario,
                    //        TiendaId = tiendaId,
                    //        ArticuloKFCId = articulo.ArticuloKFCId,
                    //        Cantidad = cantidad,
                    //        Habilitado = habilitado,
                    //        CampañaTiendaTMPId = campId,
                    //    };

                    //    db.CampañaArticuloTMPs.Add(articuloDetalle);
                    //    db.SaveChanges();
                    //}

                    //}

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

        public static Response AgregarArticuloTiendas(int articuloKFCId)
        {
            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    var tipoArticulo = db.ArticuloKFCs.Where(a => a.ArticuloKFCId == articuloKFCId).FirstOrDefault().EquityFranquicia;

                    var articulos = db.ArticuloKFCs.Where(cdt => cdt.ArticuloKFCId == articuloKFCId).FirstOrDefault();

                    var tiendas = db.Tiendas.Where(cdt => cdt.EquityFranquicia == tipoArticulo).ToList();

                    foreach (var tienda in tiendas)
                    {
                        var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == articulos.ArticuloKFCId && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                        if (tiendaArticulos == null)
                        {
                            var articuloDetalle = new TiendaArticulo
                            {
                                ArticuloKFCId = articulos.ArticuloKFCId,
                                Seleccionado = true,
                                TiendaId = tienda.TiendaId,
                            };

                            db.TiendaArticulos.Add(articuloDetalle);
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

        public static Response AgregarArticuloCampañas(int articuloKFCId)
        {
            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    var tipoArticulo = db.ArticuloKFCs.Where(a => a.ArticuloKFCId == articuloKFCId).FirstOrDefault().EquityFranquicia;

                    var articulos = db.ArticuloKFCs.Where(cdt => cdt.ArticuloKFCId == articuloKFCId).FirstOrDefault();

                    var tiendas = db.Tiendas.Where(cdt => cdt.EquityFranquicia == tipoArticulo).ToList();

                    var campaña = db.Campañas.Where(c => c.Generada == "NO").ToList();

                    if (campaña.Count == 0)
                    {
                        var campañaNueva = new Campaña
                        {
                            Descripcion = "Editar Descripción",
                            Generada = "NO",
                            Nombre = "Editar Nombre",
                        };

                        db.Campañas.Add(campañaNueva);
                        db.SaveChanges();

                    }

                    campaña = db.Campañas.Where(c => c.Generada == "NO").ToList();

                    foreach (var tienda in tiendas)
                    {
                        var articulosCampaña = db.CampañaArticuloTMPs.Where(cdt => cdt.ArticuloKFCId == articulos.ArticuloKFCId && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                        if (articulosCampaña == null)
                        {
                            foreach (var campa in campaña)
                            {
                                var articuloDetalle = new CampañaArticuloTMP
                                {
                                    ArticuloKFCId = articulos.ArticuloKFCId,
                                    Habilitado = true,
                                    TiendaId = tienda.TiendaId,
                                    CampañaId = campa.CampañaId,
                                    Cantidad = articulos.CantidadDefault,
                                    Codigo = 0
                                };

                                db.CampañaArticuloTMPs.Add(articuloDetalle);

                            }
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

        public static Response ActualizarTiendaArticulos(int? id)
        {
            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {

                    var articuloTienda = db.TiendaArticulos.Where(ta => ta.TiendaArticuloId == id).FirstOrDefault();

                    db.Entry(articuloTienda).State = EntityState.Modified;

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

        //public static Response NuevaCampaña(NuevaCampañaView view, string userName, int compañia)
        //{

        //    using (var transaccion = db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var campaña = new Campaña
        //            {
        //                CampañaId = compañia,
        //                Descripcion = view.Descripcion,
        //                Generada = "NO",
        //                Nombre = view.Nombre,
        //            };

        //            db.Campañas.Add(campaña);
        //            db.SaveChanges();

        //            var detalles = db.CampañaTiendaTMPs.Where(cdt => cdt.Usuario == userName).ToList();

        //            foreach (var detalle in detalles)
        //            {
        //                var tiendaDetalle = new CampañaTiendaTMP
        //                {
        //                    CompañiaId = detalle.CompañiaId,
        //                    CampañaId = campaña.CampañaId,
        //                    TiendaId = detalle.TiendaId,
        //                };

        //                db.CampañaTiendas.Add(tiendaDetalle);
        //                db.CampañaTiendaTMPs.Remove(detalle);

        //            }

        //            db.SaveChanges();
        //            transaccion.Commit();

        //            return new Response { Succeeded = true, };
        //        }
        //        catch (Exception ex)
        //        {
        //            transaccion.Rollback();
        //            return new Response
        //            {
        //                Message = ex.Message,
        //                Succeeded = false,
        //            };
        //        }
        //    }
        //}

        public static Response GenerarCodigos(int? id)
        {
            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    var articulos = db.ArticuloKFCs.ToList();

                    foreach (var articulo in articulos.GroupBy(f => f.Familia.Codigo))
                    {
                        var familia = articulo.FirstOrDefault().Familia.Codigo;

                        var familias = db.ArticuloKFCs.Where(f => f.Familia.Codigo == familia).ToList();

                        for (int f = 0; f < familias.Count(); f++)
                        {
                            var articuloId = familias[f].ArticuloKFCId;

                            var codigosCampañas = db.CodigosCampaña.Where(cc => cc.ArticuloKFCId == articuloId && cc.CampañaId == id).FirstOrDefault();

                            var idCampaña = db.Campañas.Where(c => c.CampañaId == id).FirstOrDefault().Nombre;

                            var consecutivo = string.Empty;

                            if (f >= 0 && f <= 9)
                            {
                                consecutivo = "00";
                            }
                            else if (f >= 10 && f <= 99)
                            {
                                consecutivo = "0";
                            }
                            else if (f >= 100)
                            {
                                consecutivo = "";
                            }

                            if (codigosCampañas == null)
                            {
                                var codigo = idCampaña + familias[f].Familia.Codigo + consecutivo + f;

                                var codigoArticulo = new CodigoCampaña
                                {
                                    ArticuloKFCId = familias[f].ArticuloKFCId,
                                    CampañaId = (int)id,
                                    Codigo = Convert.ToInt32(codigo),
                                };

                                db.CodigosCampaña.Add(codigoArticulo);

                                db.SaveChanges();
                            }

                        }


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

        public static Response AgregarTiendaArticulos(int tiendaId)
        {
            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    var tipoTienda = db.Tiendas.Where(t => t.TiendaId == tiendaId).FirstOrDefault().EquityFranquicia;

                    var articulos = db.ArticuloKFCs.Where(cdt => cdt.EquityFranquicia == tipoTienda).ToList();

                    //Borrar al terminar

                    // DE AQUI

                    //var tiendas = db.Tiendas.Where(cdt => cdt.CompañiaId == compañiaId && cdt.EquityFranquicia == tipoTienda).ToList();

                    //foreach (var tienda in tiendas)
                    //{
                    // HASTA AQUI

                    foreach (var articulo in articulos)
                    {
                        // DE AQUI
                        //var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == articulo.ArticuloKFCId && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();
                        // HASTA AQUI


                        var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == articulo.ArticuloKFCId && cdt.TiendaId == tiendaId).FirstOrDefault();

                        if (tiendaArticulos == null)
                        {
                            var articuloDetalle = new TiendaArticulo
                            {
                                ArticuloKFCId = articulo.ArticuloKFCId,
                                Seleccionado = false,
                                // DE AQUI
                                //TiendaId = tienda.TiendaId,
                                // HASTA AQUI
                                TiendaId = tiendaId,
                            };

                            db.TiendaArticulos.Add(articuloDetalle);
                        }
                    }
                    // DE AQUI
                    //}
                    // HASTA AQUI

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

        public static Response AgregarTiendaArticulosTodo()
        {
            var tipoTienda = "STOCK";
            var articulos = db.ArticuloKFCs.Where(x => x.EquityFranquicia == "EQUITY").ToList();
            var tiendas = db.Tiendas.Where(x => x.EquityFranquicia == tipoTienda).ToList();

            foreach (var tienda in tiendas)
            {
                foreach (var articulo in articulos)
                {
                    var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == articulo.ArticuloKFCId && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                    if (tiendaArticulos == null)
                    {

                        db.Database.ExecuteSqlCommand(
                            "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                            new SqlParameter("@ArticuloKFCId", articulo.ArticuloKFCId),
                            new SqlParameter("@TiendaId", tienda.TiendaId),
                            new SqlParameter("@Seleccionado", false));
                    }
                }
            }

            return new Response { Succeeded = true, };

        }

        public static Response ReglasTiendaArticulos(int tiendaId)
        {
            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    var tipoTienda = db.Tiendas.Where(t => t.TiendaId == tiendaId).FirstOrDefault().EquityFranquicia;

                    //var familiaTienda = db.Tiendas.Where(t => t.TiendaId == tiendaId).FirstOrDefault().Arroz;

                    var articulos = db.ArticuloKFCs.Where(cdt => cdt.EquityFranquicia == tipoTienda).ToList();

                    //Borrar al terminar

                    var tiendas = db.Tiendas.Where(cdt => cdt.EquityFranquicia == tipoTienda).ToList();


                    // DE AQUI
                    //foreach (var tienda in tiendas)
                    //{
                    // HASTA AQUI

                    foreach (var articulo in articulos)
                    {
                        // DE AQUI
                        //var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == articulo.ArticuloKFCId && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();
                        // HASTA AQUI


                        var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == articulo.ArticuloKFCId && cdt.TiendaId == tiendaId).FirstOrDefault();

                        if (tiendaArticulos == null)
                        {
                            var articuloDetalle = new TiendaArticulo
                            {
                                ArticuloKFCId = articulo.ArticuloKFCId,
                                Seleccionado = true,
                                // DE AQUI
                                //TiendaId = tienda.TiendaId,
                                // HASTA AQUI
                                TiendaId = tiendaId,
                            };

                            db.TiendaArticulos.Add(articuloDetalle);
                        }
                    }
                    // DE AQUI
                    //}
                    // HASTA AQUI

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

                    var detalles = db.CampañaTiendaTMPs.Where(cdt => cdt.CampañaId == view.CampañaId).ToList();

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