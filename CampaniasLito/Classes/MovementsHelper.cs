using CampaniasLito.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CampaniasLito.Classes
{
    public class spGetTiendaArticulos
    {
        public int TiendaArticuloId { get; set; }

        public int TiendaId { get; set; }

        public int ArticuloKFCId { get; set; }

        public bool Seleccionado { get; set; }

        public int CantidadDefault { get; set; }

    }

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

            var materialesPrimerCampaña = db.Database.SqlQuery<spGetTiendaArticulos>("spGetTiendaArticulos").ToList();

            //List<spGetTiendaArticulos> matCamp = new List<spGetTiendaArticulos>();

            //matCamp = materialesPrimerCampaña;

            //List<CampañaArticuloTMP> campMat = new List<CampañaArticuloTMP>();

            //campMat.AddRange(matCamp);

            //List<CampañaArticuloTMP>.AddRange(IEnumerable<materialesPrimerCampaña> db);


            //db.Database.ExecuteSqlCommand(
            //"ppInsertMaterialCampañaWithTypeTable @CampañasArticulos",
            //new SqlParameter("@CampañasArticulos", materialesPrimerCampaña));


            foreach (var materialCampaña in materialesPrimerCampaña)
            {
                //var articuloKFCId = materialCampaña.ArticuloKFCId;

                //var materiales = db.Database.SqlQuery<ArticuloKFC>("spGetMateriales @ArticuloKFCId, @Eliminado, @Activo",
                //new SqlParameter("@ArticuloKFCId", articuloKFCId),
                //new SqlParameter("@Activo", activo),
                //new SqlParameter("@Eliminado", eliminado)).FirstOrDefault();

                //var materiales = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == materialCampaña.ArticuloKFCId && x.Eliminado == false).FirstOrDefault();

                //if (materiales != null)
                //{


                //==============================================================================================================================
                if (materialCampaña.Seleccionado == true)
                {
                    db.Database.ExecuteSqlCommand(
                        "spAgregarMaterialCAmpanias @ArticuloKFCId, @TiendaId, @CampañaId, @Habilitado, @Cantidad, @Codigo",
                        new SqlParameter("@ArticuloKFCId", materialCampaña.ArticuloKFCId),
                        new SqlParameter("@TiendaId", materialCampaña.TiendaId),
                        new SqlParameter("@CampañaId", campañaid),
                        new SqlParameter("@Habilitado", materialCampaña.Seleccionado),
                        new SqlParameter("@Cantidad", materialCampaña.CantidadDefault),
                        new SqlParameter("@Codigo", codigo));
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
                }

                //==================================================================================================================================

                //}
            }
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
            //using (var transaccion = db.Database.BeginTransaction())
            //{
            //try
            //{
            var tipoArticulo = db.ArticuloKFCs.Where(a => a.ArticuloKFCId == articuloKFCId).FirstOrDefault().EquityFranquicia;

            var articulos = db.ArticuloKFCs.Where(cdt => cdt.ArticuloKFCId == articuloKFCId).FirstOrDefault();

            var tiendas = db.Tiendas.Where(cdt => cdt.EquityFranquicia == tipoArticulo).ToList();

            if (tipoArticulo == "EQUITY" || tipoArticulo == "STOCK")
            {
                tiendas = db.Tiendas.Where(cdt => cdt.EquityFranquicia != "FRANQUICIAS").ToList();
            }
            else
            {
                tiendas = db.Tiendas.Where(cdt => cdt.EquityFranquicia == "FRANQUICIAS").ToList();
            }

            var todosRest = articulos.Todo;

            foreach (var tienda in tiendas)
            {

                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == articulos.ArticuloKFCId && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                if (tiendaArticulos == null)
                {

                    db.Database.ExecuteSqlCommand(
                    "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                    new SqlParameter("@ArticuloKFCId", articulos.ArticuloKFCId),
                    new SqlParameter("@TiendaId", tienda.TiendaId),
                    new SqlParameter("@Seleccionado", todosRest));

                    //var articuloDetalle = new TiendaArticulo
                    //{
                    //    ArticuloKFCId = articulos.ArticuloKFCId,
                    //    Seleccionado = true,
                    //    TiendaId = tienda.TiendaId,
                    //};

                    //db.TiendaArticulos.Add(articuloDetalle);
                }

            }

            //db.SaveChanges();
            //transaccion.Commit();

            return new Response { Succeeded = true, };

            //}
            //catch (Exception ex)
            //{
            //    transaccion.Rollback();
            //    return new Response
            //    {
            //        Message = ex.Message,
            //        Succeeded = false,
            //    };
            //}
            //}
        }

        public static Response AgregarArticuloPorTiendas(Tienda tienda)
        {
            var tiendaId = tienda.TiendaId;
            var tipoTienda = tienda.EquityFranquicia;

            var articulos = db.ArticuloKFCs.Where(cdt => cdt.EquityFranquicia == tipoTienda).ToList();

            var campaña = db.Campañas.Where(c => c.Generada == "NO").ToList();

            if (campaña.Count == 0)
            {
                return new Response { Succeeded = true, };
            }
            else
            {
                foreach (var campa in campaña)
                {
                    foreach (var articulo in articulos)
                    {
                        //var articulosCampaña = db.CampañaArticuloTMPs.Where(cdt => cdt.ArticuloKFCId == articuloKFCId && cdt.TiendaId == tienda.TiendaId && cdt.CampañaId == campa.CampañaId).FirstOrDefault();

                        var articulosCampaña = db.Database.SqlQuery<CampañaArticuloTMP>("spGetArticulosCAmpanias @ArticuloKFCId, @TiendaId, @CampañaId",
                        new SqlParameter("@ArticuloKFCId", articulo.ArticuloKFCId),
                        new SqlParameter("@CampañaId", campa.CampañaId),
                        new SqlParameter("@TiendaId", tiendaId)).FirstOrDefault();


                        if (articulosCampaña == null)
                        {
                            var materialTienda = db.Database.SqlQuery<TiendaArticulo>("spGetMaterialCAmpanias @ArticuloKFCId, @TiendaId",
                            new SqlParameter("@ArticuloKFCId", articulo.ArticuloKFCId),
                            new SqlParameter("@TiendaId", tiendaId)).FirstOrDefault();

                            var habilitado = materialTienda.Seleccionado;

                            var cantidad = 0;

                            if (habilitado == true)
                            {
                                cantidad = articulo.CantidadDefault;
                            }

                            int codigo = 0;

                            if (articulo.Activo == true && articulo.Eliminado == false)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarMaterialCAmpanias @ArticuloKFCId, @TiendaId, @CampañaId, @Habilitado, @Cantidad, @Codigo",
                                new SqlParameter("@ArticuloKFCId", articulo.ArticuloKFCId),
                                new SqlParameter("@TiendaId", tiendaId),
                                new SqlParameter("@CampañaId", campa.CampañaId),
                                new SqlParameter("@Habilitado", habilitado),
                                new SqlParameter("@Cantidad", cantidad),
                                new SqlParameter("@Codigo", codigo));
                            }
                        }
                        else
                        {
                            if (tienda.Activo == false)
                            {
                                int campId = campa.CampañaId;

                                db.Database.ExecuteSqlCommand(
                                "spEliminarMaterialCAmpanias @ArticuloKFCId, @CampañaId, @TiendaId",
                                new SqlParameter("@ArticuloKFCId", articulo.ArticuloKFCId),
                                new SqlParameter("@CampañaId", campId),
                                new SqlParameter("@TiendaId", tiendaId));
                            }
                            else
                            {
                                if (articulosCampaña.Habilitado == true)
                                {

                                    if (articulo.CantidadDefault != articulosCampaña.Cantidad)
                                    {
                                        var cantidad = articulo.CantidadDefault;

                                        db.Database.ExecuteSqlCommand(
                                       "spActualizarMaterialCAmpanias @ArticuloKFCId, @CampañaId, @TiendaId, @Cantidad",
                                        new SqlParameter("@ArticuloKFCId", articulo.ArticuloKFCId),
                                        new SqlParameter("@CampañaId", campa.CampañaId),
                                        new SqlParameter("@TiendaId", tiendaId),
                                        new SqlParameter("@Cantidad", cantidad));

                                    }
                                }
                            }
                        }
                    }
                }
            }

            return new Response { Succeeded = true, };

        }

        public static Response AgregarArticuloCampañas(ArticuloKFC articuloKFC)
        {
            var articuloKFCId = articuloKFC.ArticuloKFCId;
            var tipoArticulo = articuloKFC.EquityFranquicia;

            var tiendas = db.Tiendas.Where(cdt => cdt.EquityFranquicia == tipoArticulo && cdt.Activo == true).ToList();

            if (tipoArticulo == "EQUITY" || tipoArticulo == "STOCK")
            {
                tiendas = db.Tiendas.Where(cdt => cdt.EquityFranquicia != "FRANQUICIAS" && cdt.Activo == true).ToList();
            }
            else
            {
                tiendas = db.Tiendas.Where(cdt => cdt.EquityFranquicia == "FRANQUICIAS" && cdt.Activo == true).ToList();
            }

            var campaña = db.Campañas.Where(c => c.Generada == "NO").ToList();

            if (campaña.Count == 0)
            {
                return new Response { Succeeded = true, };
            }
            else
            {
                foreach (var campa in campaña)
                {
                    foreach (var tienda in tiendas)
                    {
                        //var articulosCampaña = db.CampañaArticuloTMPs.Where(cdt => cdt.ArticuloKFCId == articuloKFCId && cdt.TiendaId == tienda.TiendaId && cdt.CampañaId == campa.CampañaId).FirstOrDefault();

                        var articulosCampaña = db.Database.SqlQuery<CampañaArticuloTMP>("spGetArticulosCAmpanias @ArticuloKFCId, @TiendaId, @CampañaId",
                        new SqlParameter("@ArticuloKFCId", articuloKFCId),
                        new SqlParameter("@CampañaId", campa.CampañaId),
                        new SqlParameter("@TiendaId", tienda.TiendaId)).FirstOrDefault();


                        if (articulosCampaña == null)
                        {
                            var materialTienda = db.Database.SqlQuery<TiendaArticulo>("spGetMaterialCAmpanias @ArticuloKFCId, @TiendaId",
                            new SqlParameter("@ArticuloKFCId", articuloKFCId),
                            new SqlParameter("@TiendaId", tienda.TiendaId)).FirstOrDefault();

                            var habilitado = materialTienda.Seleccionado;

                            var cantidad = 0;

                            if (habilitado == true)
                            {
                                cantidad = articuloKFC.CantidadDefault;
                            }

                            int codigo = 0;

                            db.Database.ExecuteSqlCommand(
                            "spAgregarMaterialCAmpanias @ArticuloKFCId, @TiendaId, @CampañaId, @Habilitado, @Cantidad, @Codigo",
                            new SqlParameter("@ArticuloKFCId", articuloKFCId),
                            new SqlParameter("@TiendaId", tienda.TiendaId),
                            new SqlParameter("@CampañaId", campa.CampañaId),
                            new SqlParameter("@Habilitado", habilitado),
                            new SqlParameter("@Cantidad", cantidad),
                            new SqlParameter("@Codigo", codigo));
                        }
                        else
                        {
                            if (articuloKFC.Activo == false)
                            {
                                int campId = campa.CampañaId;
                                int tiendId = tienda.TiendaId;

                                db.Database.ExecuteSqlCommand(
                                "spEliminarMaterialCAmpanias @ArticuloKFCId, @CampañaId, @TiendaId",
                                new SqlParameter("@ArticuloKFCId", articuloKFCId),
                                new SqlParameter("@CampañaId", campId),
                                new SqlParameter("@TiendaId", tiendId));
                            }
                            else if (articuloKFC.Eliminado == true)
                            {
                                int campId = campa.CampañaId;
                                int tiendId = tienda.TiendaId;

                                db.Database.ExecuteSqlCommand(
                                "spEliminarMaterialCAmpanias @ArticuloKFCId, @CampañaId, @TiendaId",
                                new SqlParameter("@ArticuloKFCId", articuloKFCId),
                                new SqlParameter("@CampañaId", campId),
                                new SqlParameter("@TiendaId", tiendId));
                            }
                            else
                            {
                                if (articulosCampaña.Habilitado == true)
                                {

                                    if (articuloKFC.CantidadDefault != articulosCampaña.Cantidad)
                                    {
                                        var cantidad = articuloKFC.CantidadDefault;

                                        db.Database.ExecuteSqlCommand(
                                       "spActualizarMaterialCAmpanias @ArticuloKFCId, @CampañaId, @TiendaId, @Cantidad",
                                        new SqlParameter("@ArticuloKFCId", articuloKFCId),
                                        new SqlParameter("@CampañaId", campa.CampañaId),
                                        new SqlParameter("@TiendaId", tienda.TiendaId),
                                        new SqlParameter("@Cantidad", cantidad));

                                    }
                                }
                            }
                        }
                    }
                }
            }

            return new Response { Succeeded = true, };
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

            db.Database.ExecuteSqlCommand(
            "spEliminarCodigos @CampañaId",
            new SqlParameter("@CampañaId", id));

            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    var articulos = db.ArticuloKFCs.ToList();

                    foreach (var articulo in articulos.GroupBy(f => new { f.Familia.Codigo, f.Descripcion }))
                    {
                        var familia = articulo.FirstOrDefault().Familia.Codigo;

                        var familias = db.ArticuloKFCs.Where(f => f.Familia.Codigo == familia).ToList();

                        for (int f = 0; f < familias.Count(); f++)
                        {
                            var articuloId = familias[f].Descripcion;

                            var codigosCampañas = db.CodigosCampaña.Where(cc => cc.ArticuloKFC.Descripcion == articuloId && cc.CampañaId == id).FirstOrDefault();

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
            var tipoTienda = db.Tiendas.Where(a => a.TiendaId == tiendaId).FirstOrDefault().EquityFranquicia;

            var tiendas = db.Tiendas.Where(x => x.TiendaId == tiendaId).FirstOrDefault();

            if (tipoTienda == "STOCK")
            {
                tipoTienda = "EQUITY";
            }

            var articulos = db.ArticuloKFCs.Where(cdt => cdt.EquityFranquicia == tipoTienda).ToList();

            foreach (var articulo in articulos)
            {

                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == articulo.ArticuloKFCId && cdt.TiendaId == tiendaId).FirstOrDefault();

                if (tiendaArticulos == null)
                {

                    db.Database.ExecuteSqlCommand(
                    "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                    new SqlParameter("@ArticuloKFCId", articulo.ArticuloKFCId),
                    new SqlParameter("@TiendaId", tiendaId),
                    new SqlParameter("@Seleccionado", true));
                }
                //else
                //{
                //    if (tiendas.Activo == false)
                //    {
                //        db.Database.ExecuteSqlCommand(
                //        "spACtualizarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                //        new SqlParameter("@ArticuloKFCId", articulo.ArticuloKFCId),
                //        new SqlParameter("@TiendaId", tiendaId),
                //        new SqlParameter("@Seleccionado", false));
                //    }
                //    else
                //    {
                //        db.Database.ExecuteSqlCommand(
                //        "spACtualizarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                //        new SqlParameter("@ArticuloKFCId", articulo.ArticuloKFCId),
                //        new SqlParameter("@TiendaId", tiendaId),
                //        new SqlParameter("@Seleccionado", true));
                //    }
                //}
            }

            return new Response { Succeeded = true, };
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