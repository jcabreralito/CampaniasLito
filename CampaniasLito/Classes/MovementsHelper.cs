using CampaniasLito.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace CampaniasLito.Classes
{
    public class spArticuloKFC
    {
        public int ArticuloKFCId { get; set; }
        public string Descripcion { get; set; }
        public string Proveedor { get; set; }
        public string Familia { get; set; }
        public int CantidadDefault { get; set; }
        public string EquityFranquicia { get; set; }
        public string Observaciones { get; set; }
        public string Imagen { get; set; }
        public bool Eliminado { get; set; }
        public bool Activo { get; set; }
        public bool Todo { get; set; }
        public string LigaImagen { get; set; }
        public string CodigoFamilia { get; set; }
    }

    public class spReglas
    {
        public int ReglaId { get; set; }
        public string Material { get; set; }
        public int ArticuloKFCId { get; set; }
        public int TipoId { get; set; }
        public string EquityFranquicia { get; set; }
        public bool TerceraReceta { get; set; }
        public bool Arroz { get; set; }
        public bool Hamburgesas { get; set; }
        public bool Ensalada { get; set; }
        public bool PET2Litros { get; set; }
        public bool Postres { get; set; }
        public bool BisquetMiel { get; set; }

        //00000000000000000000000000 MATERIALES ESPECIFICOS 0000000000000000000000000000000000
        public bool MenuBackLigth { get; set; }
        public bool Autoexpress { get; set; }
        public bool CopeteAERemodelado { get; set; }
        public bool CopeteAETradicional { get; set; }
        public bool PanelDeInnovacion { get; set; }
        public bool DisplayDeBurbuja { get; set; }
        public bool Delivery { get; set; }
        public bool AreaDeJuegos { get; set; }
        public bool COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS { get; set; }
        public bool COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L { get; set; }
        public bool DisplayDePiso { get; set; }
        public bool WCNACIONAL67X100cm { get; set; }

        //00000000000000000000000000 MEDIDAS ESPECIALES 0000000000000000000000000000000000
        public bool WCMedidaEspecial60_8x85cm { get; set; }
        public bool WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm { get; set; }
        public bool WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm { get; set; }
        public bool WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm { get; set; }
        public bool WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm { get; set; }
        public bool MedidaEspecialPanelDeComplementos { get; set; }
        public bool MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm { get; set; }
        public bool MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm { get; set; }
        public bool MEDIDA_BACKLIGHT_55_5X75_5CM { get; set; }
        public bool MEDIDA_BACKLIGHT_59_5X79CM { get; set; }
        public bool MEDIDAS_ESPECIALES_MENU { get; set; }
        public bool TipoFC { get; set; }
        public bool TipoFS { get; set; }
        public bool TipoIL { get; set; }
        public bool TipoSB { get; set; }
    }

    public class spGetTiendaArticulos
    {
        public long TiendaArticuloId { get; set; }

        public int TiendaId { get; set; }

        public int ArticuloKFCId { get; set; }

        public bool Seleccionado { get; set; }

        public int CantidadDefault { get; set; }

        public string Restaurante { get; set; }

        public string Material { get; set; }

    }

    public class MovementsHelper : IDisposable
    {
        private static readonly CampaniasLitoContext db = new CampaniasLitoContext();

        public void Dispose()
        {
            db.Dispose();
        }

        public static Response AgregarArticulosNuevaCampaña(int campañaid)
        {
            var cantidadDefault = 0;
            var codigo = 0;

            var materialesPrimerCampaña = db.Database.SqlQuery<spGetTiendaArticulos>("spGetTiendaArticulosCampaña").ToList();

            foreach (var materialCampaña in materialesPrimerCampaña)
            {
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
            var tipoArticulo = db.ArticuloKFCs.Where(a => a.ArticuloKFCId == articuloKFCId).FirstOrDefault().EquityFranquicia;

            var tiendas = db.Tiendas.Where(cdt => cdt.EquityFranquicia == tipoArticulo).ToList();

            if (tipoArticulo == "EQUITY" || tipoArticulo == "STOCK")
            {
                tiendas = db.Tiendas.Where(cdt => cdt.EquityFranquicia != "FRANQUICIAS").ToList();
            }
            else
            {
                tiendas = db.Tiendas.Where(cdt => cdt.EquityFranquicia == "FRANQUICIAS").ToList();
            }

            var todosRest = false;

            if (tipoArticulo == "EQUITY")
            {
                AgregarArticulosTiendaCampañaExiste(articuloKFCId);
            }
            else
            {
                AgregarArticulosTiendaCampañaExisteF(articuloKFCId);
            }

            foreach (var tienda in tiendas)
            {

                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == articuloKFCId && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                if (tiendaArticulos == null)
                {

                    db.Database.ExecuteSqlCommand(
                    "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                    new SqlParameter("@ArticuloKFCId", articuloKFCId),
                    new SqlParameter("@TiendaId", tienda.TiendaId),
                    new SqlParameter("@Seleccionado", todosRest));

                }

            }

            return new Response { Succeeded = true, };

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

                        var articulosCampaña = db.Database.SqlQuery<CampañaArticuloTMP>("spGetArticulosCAmpanias @ArticuloKFCId, @TiendaId, @CampañaId",
                        new SqlParameter("@ArticuloKFCId", articulo.ArticuloKFCId),
                        new SqlParameter("@CampañaId", campa.CampañaId),
                        new SqlParameter("@TiendaId", tiendaId)).FirstOrDefault();


                        if (articulosCampaña == null)
                        {
                            var materialTienda = db.Database.SqlQuery<TiendaArticulo>("spGetMaterialCAmpanias @ArticuloKFCId, @TiendaId",
                            new SqlParameter("@ArticuloKFCId", articulo.ArticuloKFCId),
                            new SqlParameter("@TiendaId", tiendaId)).FirstOrDefault();

                            var habilitado = false;

                            if (materialTienda != null)
                            {
                                habilitado = materialTienda.Seleccionado;
                            }

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

        private static void AgregarTiendaArticulosCampañaExiste(Tienda tienda)
        {
            var tiendaId = tienda.TiendaId;

            var tiendaTodas = db.Database.SqlQuery<Tienda>("spGetRestaurantesTodos").ToList();

            var tiendas = tiendaTodas.Where(x => x.TiendaId == tiendaId).FirstOrDefault();

            var selec = false;

            if (tiendas.Autoexpress == true && tiendas.Arroz == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 4 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 4).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 4),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 1 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 1).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 1),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 4));

                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }


            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 2 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 2).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 2),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 4));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 3 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 3).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 3),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 4));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 5 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 5).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 5),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 4));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 6 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 6).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 6),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 4));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 7 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 7).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 7),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 4));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 8 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 8).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 8),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 4));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 9 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 9).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 9),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 4));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 13 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 13).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 13),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 10 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 10).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 10),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 13));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 11 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 11).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 11),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 13));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 12 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 12).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 12),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 13));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 14 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 14).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 14),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 13));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 15 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 15).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 15),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 13));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 16 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 16).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 16),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 13));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 17 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 17).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 17),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 13));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 18 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 18).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 18),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 13));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 22 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 22).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 22),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 19 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 19).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 19),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 22));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 20 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 20).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 20),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 22));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 21 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 21).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 21),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 22));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 23 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 23).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 23),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 22));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 24 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 24).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 24),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 22));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 25 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 25).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 25),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 22));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 26 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 26).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 26),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 22));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 27 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 27).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 27),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 22));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 28 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 28).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 28),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 33 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 33).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 33),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 29 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 29).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 29),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 33));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 30 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 30).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 30),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 33));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 31 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 31).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 31),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 33));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 32 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 32).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 32),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 33));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 34 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 34).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 34),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 33));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 35 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 35).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 35),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 33));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 36 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 36).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 36),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 33));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 41 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 41).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 41),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == false && tiendas.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 37 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 37).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 37),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 41));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == false && tiendas.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 38 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 38).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 38),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 41));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == false && tiendas.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 39 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 39).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 39),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 41));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == false && tiendas.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 40 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 40).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 40),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 41));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == false && tiendas.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 42 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 42).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 42),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 41));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == false && tiendas.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 43 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 43).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 43),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 41));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == false && tiendas.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 44 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 44).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 44),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 41));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == false && tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 45 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 45).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 45),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 41));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 50 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 50).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 50),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == false && tiendas.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 46 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 46).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 46),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 50));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 47 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 47).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 47),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 50));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 48 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 48).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 48),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 50));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 49 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 49).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 49),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 50));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 51 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 51).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 51),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 50));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 52 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 52).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 52),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 50));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 53 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 53).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 53),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 50));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 54 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 54).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 54),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 50));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 55 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 55).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 55),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 59 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 59).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 59),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 56 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 56).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 56),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 59));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 57 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 57).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 57),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 59));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 58 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 58).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 58),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 59));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 60 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 60).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 60),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 59));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 61 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 61).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 61),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 59));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 62 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 62).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 62),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 59));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 63 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 63).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 63),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 59));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 64 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 64).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 64),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 59));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 68 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 68).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 68),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 65 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 65).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 65),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 68));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 66 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 66).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 66),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 68));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 67 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 67).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 67),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 68));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 69 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 69).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 69),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 68));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 70 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 70).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 70),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 68));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 71 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 71).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 71),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 68));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 72 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 72).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 72),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 68));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 73 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 73).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 73),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 68));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 74 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 74).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 74),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_59_5X79CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 75 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 75).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 75),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 76 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 76).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 76),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_59_5X79CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 77 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 77).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 77),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 78 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 78).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 78),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_59_5X79CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 79 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 79).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 79),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 80 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 80).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 80),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_59_5X79CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 81 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 81).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 81),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 82 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 82).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 82),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_59_5X79CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 83 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 83).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 83),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 84 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 84).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 84),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_59_5X79CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 85 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 85).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 85),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 86 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 86).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 86),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_59_5X79CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 87 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 87).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 87),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 88 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 88).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 88),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_59_5X79CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 89 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 89).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 89),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 90 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 90).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 90),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_59_5X79CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 91 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 91).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 91),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 92 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 92).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 92),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_59_5X79CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 93 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 93).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 93),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 94 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 94).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 94),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.MenuBackLigth == true && tiendas.MEDIDA_BACKLIGHT_59_5X79CM == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 95 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 95).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 95),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 96 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 96).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 96),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 97 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 97).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 97),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 98 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 98).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 98),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.CopeteAERemodelado == true && tiendas.PET2Litros == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 99 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 99).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 99),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.CopeteAERemodelado == true && tiendas.PET2Litros == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 100 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 100).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 100),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.CopeteAETradicional == true && tiendas.PET2Litros == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 101 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 101).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 101),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.CopeteAETradicional == true && tiendas.PET2Litros == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 102 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 102).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 102),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Postres == false && tiendas.PET2Litros == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 103 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 103).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 103),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Postres == true && tiendas.PET2Litros == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 104 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 104).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 104),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Postres == true && tiendas.PET2Litros == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 105 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 105).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 105),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 1 || tiendas.TipoId == 3 || tiendas.TipoId == 4)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 106 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 106).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 106),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.DisplayDeBurbuja == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 107 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 107).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 107),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 1 || tiendas.TipoId == 4)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 108 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 108).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 108),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 109 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 109).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 109),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Arroz == true && tiendas.Hamburgesas == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 110 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 110).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 110),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Arroz == false && tiendas.Hamburgesas == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 111 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 111).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 111),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Arroz == false && tiendas.Hamburgesas == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 112 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 112).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 112),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.AreaDeJuegos == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 113 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 113).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 113),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 114 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 114).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 114),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 115 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 115).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 115),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3 || tiendas.TipoId == 1 || tiendas.TipoId == 4)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 116 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 116).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 116),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Delivery == true && tiendas.TerceraReceta == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 117 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 117).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 117),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Delivery == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 118 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 118).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 118),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Delivery == true && tiendas.TerceraReceta == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 119 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 119).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 119),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3 || tiendas.TipoId == 1 || tiendas.TipoId == 4)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 120 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 120).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 120),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TerceraReceta == true && tiendas.Hamburgesas == true && tiendas.Ensalada == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 121 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 121).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 121),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TerceraReceta == false && tiendas.Hamburgesas == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 122 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 122).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 122),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TerceraReceta == false && tiendas.Hamburgesas == false && tiendas.TipoId == 4)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 123 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 123).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 123),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TerceraReceta == true && tiendas.Arroz == true && tiendas.PanelDeComplementosDigital == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 124 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 124).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 124),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TerceraReceta == false && tiendas.Arroz == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 128 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 128).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 128),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TerceraReceta == false && tiendas.Arroz == false && tiendas.MedidaEspecialPanelDeComplementos == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 125 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 125).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 125),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 128));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TerceraReceta == true && tiendas.Arroz == true && tiendas.MedidaEspecialPanelDeComplementos == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 126 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 126).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 126),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 124));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TerceraReceta == false && tiendas.Arroz == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 127 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 127).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 127),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.PanelDeInnovacion == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 129 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 129).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 129),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.Delivery == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 130 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 130).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 130),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 131 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 131).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 131),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Delivery == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 132 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 132).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 132),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 133 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 133).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 133),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 136 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 136).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 136),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 134 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 134).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 134),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 136));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 135 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 135).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 135),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 136));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 137 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 137).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 137),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 136));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 138 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 138).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 138),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 136));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == false && tiendas.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 139 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 139).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 139),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 136));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 140 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 140).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 140),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 136));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 141 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 141).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 141),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 136));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 142 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 142).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 142),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 136));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true && tiendas.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 143 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 143).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 143),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 136));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == false && tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 144 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 144).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 144),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 149 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 149).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 149),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 145 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 145).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 145),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 149));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 146 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 146).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 146),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 149));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 147 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 147).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 147),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 149));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 148 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 148).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 148),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 149));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 150 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 150).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 150),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 149));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 151 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 151).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 151),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 149));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 152 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 152).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 152),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 149));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 153 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 153).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 153),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 149));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.Autoexpress == true && tiendas.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 154 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 154).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 154),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 149));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 1)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 155 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 155).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 155),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 155 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 155).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 155),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 3)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 155 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 155).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 155),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 4)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 155 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 155).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 155),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == true && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 161 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 161).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 161),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 3 && tiendas.TerceraReceta == true && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 161 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 161).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 161),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == true && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 156 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 156).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 156),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 161));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.TerceraReceta == true && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 156 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 156).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 156),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 161));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == true && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 157 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 157).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 157),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 161));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.TerceraReceta == true && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 157 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 157).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 157),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 161));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == true && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 158 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 158).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 158),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 161));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.TerceraReceta == true && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 158 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 158).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 158),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 161));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == true && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 159 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 159).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 159),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 161));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.TerceraReceta == true && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 159 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 159).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 159),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 161));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == true && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 160 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 160).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 160),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 161));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.TerceraReceta == true && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 160 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 160).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 160),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 161));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == false && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 167 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 167).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 167),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 3 && tiendas.TerceraReceta == false && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 167 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 167).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 167),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == false && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 162 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 162).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 162),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 167));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.TerceraReceta == false && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 162 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 162).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 162),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 167));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == false && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 163 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 163).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 163),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 167));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.TerceraReceta == false && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 163 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 163).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 163),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 167));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 164 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 164).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 164),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 167));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.TerceraReceta == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 164 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 164).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 164),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 167));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 165 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 165).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 165),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 167));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.TerceraReceta == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 165 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 165).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 165),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 167));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 166 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 166).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 166),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 167));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.TerceraReceta == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 166 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 166).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 166),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 167));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 173 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 173).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 173),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == false)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 173 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 173).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 173),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 168 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 168).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 168),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 173));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 168 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 168).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 168),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 173));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 169 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 169).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 169),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 173));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 169 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 169).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 169),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 173));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 170 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 170).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 170),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 173));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 170 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 170).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 170),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 173));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 171 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 171).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 171),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 173));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 171 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 171).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 171),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 173));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 172 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 172).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 172),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 173));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 172 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 172).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 172),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 173));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            //if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            if (tiendas.TipoId == 2)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 179 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 179).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 179),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            //if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            if (tiendas.TipoId == 3)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 179 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 179).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 179),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 174 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 174).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 174),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 179));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 174 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 174).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 174),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 179));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 175 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 175).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 175),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 179));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 175 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 175).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 175),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 179));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 176 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 176).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 176),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 179));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 176 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 176).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 176),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 179));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 177 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 177).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 177),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 179));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 177 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 177).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 177),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 179));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 178 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 178).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 178),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 179));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 178 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 178).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 178),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 179));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            //if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            if (tiendas.TipoId == 2)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 185 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 185).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 185),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            //if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            if (tiendas.TipoId == 3)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 185 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 185).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 185),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 180 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 180).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 180),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 185));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 180 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 180).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 180),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 185));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 181 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 181).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 181),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 185));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 181 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 181).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 181),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 185));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 182 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 182).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 182),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 185));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 182 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 182).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 182),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 185));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 183 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 183).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 183),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 185));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 183 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 183).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 183),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 185));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 184 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 184).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 184),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 185));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 184 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 184).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 184),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 185));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 191 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 191).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 191),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 191 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 191).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 191),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 186 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 186).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 186),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 191));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 186 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 186).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 186),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 191));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 187 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 187).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 187),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 191));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 187 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 187).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 187),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 191));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 188 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 188).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 188),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 191));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 188 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 188).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 188),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 191));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 189 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 189).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 189),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 191));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 189 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 189).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 189),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 191));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 190 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 190).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 190),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 191));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 190 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 190).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 190),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 191));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 2 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 193 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 193).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 193),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 3 && tiendas.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tiendas.WCMedidaEspecial60_8x85cm == false && tiendas.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tiendas.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 193 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 193).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 193),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            if (tiendas.TipoId == 2 && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 192 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 192).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 192),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 193));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            if (tiendas.TipoId == 3 && tiendas.WCMedidaEspecial60_8x85cm == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 192 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 192).FirstOrDefault().Activo;

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", 192),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", true));

                        db.Database.ExecuteSqlCommand(
                        "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@ArticuloKFCId", 193));
                    }
                    else
                    {
                        selec = true;

                        tiendaArticulos.Seleccionado = selec;
                        db.Entry(tiendaArticulos).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            if (tiendas.TerceraReceta == true && tiendas.Arroz == true && tiendas.PanelDeComplementosDigital == true)
            {
                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 286 && cdt.TiendaId == tiendaId).FirstOrDefault();

                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == 286).FirstOrDefault().Activo;

                //if (articuloActivo == true)
                //{
                if (tiendaArticulos == null)
                {
                    //db.Database.ExecuteSqlCommand(
                    //"spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                    //new SqlParameter("@ArticuloKFCId", 286),
                    //new SqlParameter("@TiendaId", tiendaId),
                    //new SqlParameter("@Seleccionado", true));

                    db.Database.ExecuteSqlCommand(
                    "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                    new SqlParameter("@TiendaId", tiendaId),
                    new SqlParameter("@ArticuloKFCId", 124));

                    db.Database.ExecuteSqlCommand(
                    "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                    new SqlParameter("@TiendaId", tiendaId),
                    new SqlParameter("@ArticuloKFCId", 125));

                    db.Database.ExecuteSqlCommand(
                    "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                    new SqlParameter("@TiendaId", tiendaId),
                    new SqlParameter("@ArticuloKFCId", 126));

                    db.Database.ExecuteSqlCommand(
                    "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                    new SqlParameter("@TiendaId", tiendaId),
                    new SqlParameter("@ArticuloKFCId", 127));

                    db.Database.ExecuteSqlCommand(
                    "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                    new SqlParameter("@TiendaId", tiendaId),
                    new SqlParameter("@ArticuloKFCId", 128));
                }
                else
                {
                    selec = true;

                    tiendaArticulos.Seleccionado = selec;
                    db.Entry(tiendaArticulos).State = EntityState.Modified;
                    db.SaveChanges();
                }
                //}
            }

        }

        private static void AgregarTiendaArticulosCampañaExisteF(Tienda tienda)
        {
            var categoria = tienda.EquityFranquicia;
            var tiendaId = tienda.TiendaId;

            var tiendaTodas = db.Database.SqlQuery<Tienda>("spGetRestaurantesTodos").ToList();

            var tiendas = tiendaTodas.Where(x => x.TiendaId == tiendaId).FirstOrDefault();

            var totalArticulos = db.ArticuloKFCs.Where(x => x.Eliminado == false && x.EquityFranquicia == categoria).ToList();

            var primero = totalArticulos.FirstOrDefault().ArticuloKFCId;

            if (tiendas.Autoexpress == true && tiendas.Arroz == true) //194
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true) //195
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true) //196
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == false) //197
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == true) //198
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true && tiendas.TerceraReceta == false) //199
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true) //200
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == false) //201
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true) //202
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true) //203
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3) //204
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS == true) //205
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.CopeteAETradicional == true && tiendas.PET2Litros == false) //206
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Postres == false && tiendas.PET2Litros == false) //207
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Postres == true && tiendas.PET2Litros == false) //208
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 1 || tiendas.TipoId == 3 || tiendas.TipoId == 4) //209
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.DisplayDeBurbuja == true) //210
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 1 || tiendas.TipoId == 4) //211
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3) //212
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Arroz == true && tiendas.Hamburgesas == true) //213
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Arroz == false && tiendas.Hamburgesas == true) //214
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Arroz == false && tiendas.Hamburgesas == false) //215
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.AreaDeJuegos == true) //216
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3) //217
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3) //218
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3 || tiendas.TipoId == 1 || tiendas.TipoId == 4) //219
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Delivery == true && tiendas.TerceraReceta == true && tiendas.Telefono == true) //220
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Delivery == true && tiendas.Telefono == true) //221
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Delivery == true && tiendas.TerceraReceta == false && tiendas.Telefono == true) //222
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3 || tiendas.TipoId == 1 || tiendas.TipoId == 4) //223
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TerceraReceta == true && tiendas.Hamburgesas == true && tiendas.PanelALaCartaCaribe == true) //224
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TerceraReceta == true && tiendas.Hamburgesas == true && tiendas.Ensalada == true) //225
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TerceraReceta == false && tiendas.Hamburgesas == true) //226
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TerceraReceta == false && tiendas.Hamburgesas == false && tiendas.TipoId == 4) //227
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TerceraReceta == true && tiendas.Arroz == true) //228
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TerceraReceta == true && tiendas.Arroz == true && tiendas.PanelComplementosHolding == true) //229
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TerceraReceta == false && tiendas.Arroz == true) //230
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TerceraReceta == false && tiendas.Arroz == false) //231
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.PanelDeInnovacion == true) //232
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true) //233
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3) //234
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true) //235
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true) //236
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3 || tiendas.TipoId == 1 || tiendas.TipoId == 4) //237
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == true) //238
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else if (tiendas.TipoId == 3 && tiendas.TerceraReceta == true) //238
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 && tiendas.TerceraReceta == false) //239
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else if (tiendas.TipoId == 3 && tiendas.TerceraReceta == false) //239
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3) //240
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3) //241
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3) //242
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3) //243
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TipoId == 2 || tiendas.TipoId == 3) //244
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 268 && tiendas.TelefonoPersonalizado == true) //245
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 334 && tiendas.TelefonoPersonalizado == true) //246
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 390 && tiendas.TelefonoPersonalizado == true || tiendas.TiendaId == 391 && tiendas.TelefonoPersonalizado == true) //247
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 286 && tiendas.TelefonoPersonalizado == true) //248
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 287 && tiendas.TelefonoPersonalizado == true) //249
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 288 && tiendas.TelefonoPersonalizado == true) //250
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 290 && tiendas.TelefonoPersonalizado == true) //251
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 291 && tiendas.TelefonoPersonalizado == true) //252
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 297 && tiendas.TelefonoPersonalizado == true || tiendas.TiendaId == 384 && tiendas.TelefonoPersonalizado == true) //253
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 299 && tiendas.TelefonoPersonalizado == true) //254
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 300 && tiendas.TelefonoPersonalizado == true) //255
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 304 && tiendas.TelefonoPersonalizado == true) //256
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 307 && tiendas.TelefonoPersonalizado == true) //257
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 308 && tiendas.TelefonoPersonalizado == true) //258
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 309 && tiendas.TelefonoPersonalizado == true) //259
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 310 && tiendas.TelefonoPersonalizado == true) //260
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 314 && tiendas.TelefonoPersonalizado == true) //261
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 319 && tiendas.TelefonoPersonalizado == true) //262
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 327 && tiendas.TelefonoPersonalizado == true) //263
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 328 && tiendas.TelefonoPersonalizado == true) //264
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 329 && tiendas.TelefonoPersonalizado == true) //265
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 335 && tiendas.TelefonoPersonalizado == true) //266
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 336 && tiendas.TelefonoPersonalizado == true) //267
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 337 && tiendas.TelefonoPersonalizado == true) //268
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 339 && tiendas.TelefonoPersonalizado == true) //269
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 343 && tiendas.TelefonoPersonalizado == true) //270
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 347 && tiendas.TelefonoPersonalizado == true) //271
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 349 && tiendas.TelefonoPersonalizado == true) //272
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 351 && tiendas.TelefonoPersonalizado == true) //273
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 356 && tiendas.TelefonoPersonalizado == true) //274
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 367 && tiendas.TelefonoPersonalizado == true) //275
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 368 && tiendas.TelefonoPersonalizado == true) //276
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 373 && tiendas.TelefonoPersonalizado == true || tiendas.TiendaId == 379 && tiendas.TelefonoPersonalizado == true || tiendas.TiendaId == 382 && tiendas.TelefonoPersonalizado == true) //277
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 374 && tiendas.TelefonoPersonalizado == true) //278
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 375 && tiendas.TelefonoPersonalizado == true || tiendas.TiendaId == 381 && tiendas.TelefonoPersonalizado == true) //279
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 394 && tiendas.TelefonoPersonalizado == true) //280
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 0) //281
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 0) //282
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 0) //283
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 0) //284
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TiendaId == 0) //285
            {
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TerceraReceta == true && tiendas.Arroz == true && tiendas.PanelDeComplementosDigital == true) //287
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TerceraReceta == false && tiendas.Arroz == false && tiendas.PanelComplementosHoldingMR == true) //288
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TerceraReceta == false && tiendas.Autoexpress == true) //289
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TerceraReceta == true && tiendas.Arroz == true) //290
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.TerceraReceta == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm) //291
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == false) //292
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tiendas.Autoexpress == true && tiendas.Arroz == true && tiendas.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm) //293
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tienda.TerceraReceta == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true) //294
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true) //295
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true) //296
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true) //297
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true) //298
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true) //299
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true) //300
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tienda.Autoexpress == true && tienda.TerceraReceta == false && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true) //301
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true) //302
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
            if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true) //303
            {
                primero++;
                AgregarTiendas(tiendaId, primero);
                primero++;
            }
            else
            {
                primero++;
            }
        }

        private static void AgregarTiendas(int tiendaId, int primero)
        {
            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == primero && cdt.TiendaId == tiendaId).FirstOrDefault();

            var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == primero).FirstOrDefault().Activo;
            var selec = false;

            if (articuloActivo == true)
            {
                if (tiendaArticulos == null)
                {
                    db.Database.ExecuteSqlCommand(
                    "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                    new SqlParameter("@ArticuloKFCId", primero),
                    new SqlParameter("@TiendaId", tiendaId),
                    new SqlParameter("@Seleccionado", true));
                }
                else
                {
                    selec = true;

                    tiendaArticulos.Seleccionado = selec;
                    db.Entry(tiendaArticulos).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

        }

        private static void AgregarArticulosTiendaCampañaExiste(int articuloKFCId)
        {
            var categoria = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == articuloKFCId).FirstOrDefault().EquityFranquicia;

            var restaurantes = db.Database.SqlQuery<Tienda>("spGetRestaurantes").ToList();

            var tiendas = restaurantes.Where(x => x.EquityFranquicia == categoria && x.Activo == true).ToList();

            var selec = false;

            var articulos = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == articuloKFCId).ToList();

            foreach (var articulo in articulos)
            {
                if (articulo.ArticuloKFCId == 1)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 1 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 1),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 4));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }

                    }
                }

                if (articulo.ArticuloKFCId == 2)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 2 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 2),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 4));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 3)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 3 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 3),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 4));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 4)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 4 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 4),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 5)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 5 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 5),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 4));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 6)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 6 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 6),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 4));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 7)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 7 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 7),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 4));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 8)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 8 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 8),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 4));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 9)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 9 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 9),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 4));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 10)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 10 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 10),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 13));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 11)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 11 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 11),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 13));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 12)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 12 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 12),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 13));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 13)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 13 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 13),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 14)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 14 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 14),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 13));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 15)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 15 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 15),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 13));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 16)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 16 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 16),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 13));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 17)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 17 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 17),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 13));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 18)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 18 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 18),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 13));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 19)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 19 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 19),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 22));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 20)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 20 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 20),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 22));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 21)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 21 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 21),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 22));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 22)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 22 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 22),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 23)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 23 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 23),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 22));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 24)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 24 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 24),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 22));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 25)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 25 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 25),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 22));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 26)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 26 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 26),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 22));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 27)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 27 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 27),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 22));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 28)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 28 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 28),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 29)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 29 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 29),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 33));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 30)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 30 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 30),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 33));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 31)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 31 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 31),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 33));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 32)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 32 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 32),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 33));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 33)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 33 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 33),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 34)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 34 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 34),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 33));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 35)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 35 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 35),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 33));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 36)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 36 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 36),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 33));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 37)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == false && tienda.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 37 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 37),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 41));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 38)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == false && tienda.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 38 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 38),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 41));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 39)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == false && tienda.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 39 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 39),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 41));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 40)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == false && tienda.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 40 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 40),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 41));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 41)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 41 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 41),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 42)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == false && tienda.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 42 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 42),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 41));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 43)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == false && tienda.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 43 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 43),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 41));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 44)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == false && tienda.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 44 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 44),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 41));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 45)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == false && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 45 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 45),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 41));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 46)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == false && tienda.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 46 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 46),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 50));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 47)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 47 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 47),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 50));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 48)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 48 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 48),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 50));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 49)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 49 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 49),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 50));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 50)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 50 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 50),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 51)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 51 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 51),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 50));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 52)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 52 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 52),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 50));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 53)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 53 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 53),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 50));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 54)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 54 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 54),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 50));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 55)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 55 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 55),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 56)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 56 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 56),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 59));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 57)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 57 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 57),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 59));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 58)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 58 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 58),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 59));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 59)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 59 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 59),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 60)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 60 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 60),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 59));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 61)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 61 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 61),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 59));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 62)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 62 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 62),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 59));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 63)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 63 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 63),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 59));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 64)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 64 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 64),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 59));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 65)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 65 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 65),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 68));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 66)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 66 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 66),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 68));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 67)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 67 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 67),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 68));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 68)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 68 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 68),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 69)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 69 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 69),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 68));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 70)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 70 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 70),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 68));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 71)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 71 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 71),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 68));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 72)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 72 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 72),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 68));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 73)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 73 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 73),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 68));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 74)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 74 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 74),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 75)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_59_5X79CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 75 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 75),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 76)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 76 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 76),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 77)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_59_5X79CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 77 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 77),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 78)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 78 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 78),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 79)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_59_5X79CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 79 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 79),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 80)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 80 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 80),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 81)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_59_5X79CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 81 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 81),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 82)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 82 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 82),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 83)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_59_5X79CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 83 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 83),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 84)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 84 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 84),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 85)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_59_5X79CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 85 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 85),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 86)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 86 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 86),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 87)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_59_5X79CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 87 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 87),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 88)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 88 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 88),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 89)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_59_5X79CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 89 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 89),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 90)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 90 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 90),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 91)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_59_5X79CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 91 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 91),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 92)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 92 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 92),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 93)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_59_5X79CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 93 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 93),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 94)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_55_5X75_5CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 94 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 94),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 95)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.MenuBackLigth == true && tienda.MEDIDA_BACKLIGHT_59_5X79CM == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 95 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 95),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 96)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 96 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 96),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 97)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 97 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 97),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 98)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.COPETE_ESPECIAL_SOPORTE_LATERAL_PET_2L == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 98 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 98),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 99)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.CopeteAERemodelado == true && tienda.PET2Litros == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 99 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 99),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 100)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.CopeteAERemodelado == true && tienda.PET2Litros == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 100 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 100),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 101)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.CopeteAETradicional == true && tienda.PET2Litros == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 101 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 101),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 102)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.CopeteAETradicional == true && tienda.PET2Litros == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 102 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 102),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 103)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Postres == false && tienda.PET2Litros == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 103 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 103),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 104)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Postres == true && tienda.PET2Litros == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 104 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 104),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 105)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Postres == true && tienda.PET2Litros == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 105 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 105),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }

                    {
                    }
                }

                if (articulo.ArticuloKFCId == 106)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 1 || tienda.TipoId == 3 || tienda.TipoId == 4)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 106 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 106),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 107)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.DisplayDeBurbuja == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 107 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 107),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 108)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 1 || tienda.TipoId == 4)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 108 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 108),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 109)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 109 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 109),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 110)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Arroz == true && tienda.Hamburgesas == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 110 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 110),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 111)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Arroz == false && tienda.Hamburgesas == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 111 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 111),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 112)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Arroz == false && tienda.Hamburgesas == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 112 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 112),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 113)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.AreaDeJuegos == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 113 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 113),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 114)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 114 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 114),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 115)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 115 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 115),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 116)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3 || tienda.TipoId == 1 || tienda.TipoId == 4)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 116 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 116),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 117)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Delivery == true && tienda.TerceraReceta == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 117 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 117),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 118)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Delivery == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 118 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 118),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 119)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Delivery == true && tienda.TerceraReceta == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 119 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 119),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 120)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3 || tienda.TipoId == 1 || tienda.TipoId == 4)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 120 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 120),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 121)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == true && tienda.Hamburgesas == true && tienda.Ensalada == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 121 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 121),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 122)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == false && tienda.Hamburgesas == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 122 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 122),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 123)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == false && tienda.Hamburgesas == false && tienda.TipoId == 4)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 123 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 123),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 124)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == true && tienda.Arroz == true && tienda.PanelDeComplementosDigital == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 124 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 124),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 125)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == false && tienda.Arroz == false && tienda.MedidaEspecialPanelDeComplementos == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 125 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 125),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 128));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 126)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == true && tienda.Arroz == true && tienda.MedidaEspecialPanelDeComplementos == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 126 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 126),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 124));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 127)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == false && tienda.Arroz == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 127 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 127),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 128)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == false && tienda.Arroz == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 128 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 128),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 129)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.PanelDeInnovacion == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 129 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 129),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 130)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Delivery == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 130 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 130),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 131)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 131 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 131),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 132)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Delivery == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 132 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 132),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 133)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 133 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 133),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 134)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 134 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 134),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 136));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 135)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 135 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 135),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 136));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 136)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 136 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 136),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 137)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 137 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 137),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 136));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 138)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 138 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 138),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 136));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 139)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == false && tienda.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 139 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 139),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 136));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 140)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 140 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 140),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 136));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 141)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 141 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 141),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 136));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 142)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 142 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 142),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 136));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 143)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 143 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 143),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 136));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 144)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == false && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 144 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 144),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 145)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 145 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 145),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 149));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 146)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 146 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 146),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 149));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 147)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 147 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 147),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 149));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 148)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 148 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 148),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 149));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 149)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 149 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 149),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 150)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 150 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 150),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 149));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 151)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 151 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 151),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 149));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 152)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 152 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 152),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 149));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 153)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 153 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 153),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 149));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 154)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 154 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 154),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 149));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 155)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3 || tienda.TipoId == 1 || tienda.TipoId == 4)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 155 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 155),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 156)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == true && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 156 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 156),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 161));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == true && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 156 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 156),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 161));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 157)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == true && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 157 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 157),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 161));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == true && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 157 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 157),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 161));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 158)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == true && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 158 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 158),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 161));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == true && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 158 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 158),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 161));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 159)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == true && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 159 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 159),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 161));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == true && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 159 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 159),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 161));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 160)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == true && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 160 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 160),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 161));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == true && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 160 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 160),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 161));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 161)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == true && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 161 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 161),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == true && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 161 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 161),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 162)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == false && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 162 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 162),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 167));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == false && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 162 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 162),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 167));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 163)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == false && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 163 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 163),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 167));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == false && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 163 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 163),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 167));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 164)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 164 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 164),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 167));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 164 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 164),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 167));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 165)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 165 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 165),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 167));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 165 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 165),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 167));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 166)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 166 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 166),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 167));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 166 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 166),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 167));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 167)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == false && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 167 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 167),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == false && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 167 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 167),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 168)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 168 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 168),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 173));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 168 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 168),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 173));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 169)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 169 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 169),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 173));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 169 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 169),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 173));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 170)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 170 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 170),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 173));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 170 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 170),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 173));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 171)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 171 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 171),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 173));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 171 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 171),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 173));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 172)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 172 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 172),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 173));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 172 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 172),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 173));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 173)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 173 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 173),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == false)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 173 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 173),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 174)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 174 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 174),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 179));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 174 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 174),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 179));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 175)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 175 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 175),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 179));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 175 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 175),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 179));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 176)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 176 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 176),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 179));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 176 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 176),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 179));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 177)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 177 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 177),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 179));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 177 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 177),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 179));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 178)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 178 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 178),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 179));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 178 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 178),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 179));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 179)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 179 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 179),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 179 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 179),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 180)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 180 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 180),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 185));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 180 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 180),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 185));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 181)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 181 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 181),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 185));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 181 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 181),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 185));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 182)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 182 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 182),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 185));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 182 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 182),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 185));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 183)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 183 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 183),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 185));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 183 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 183),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 185));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 184)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 184 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 184),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 185));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 184 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 184),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 185));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 185)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 185 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 185),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 185 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 185),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 186)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 186 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 186),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 191));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 186 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 186),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 191));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 187)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 187 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 187),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 191));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 187 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 187),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 191));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 188)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 188 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 188),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 191));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 188 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 188),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 191));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 189)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 189 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 189),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 191));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 189 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 189),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 191));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 190)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 190 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 190),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 191));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 190 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 190),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 191));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 191)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 191 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 191),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 191 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 191),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 192)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 192 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 192),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 193));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        if (tienda.TipoId == 3 && tienda.WCMedidaEspecial60_8x85cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 192 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 192),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 193));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                    }
                }

                if (articulo.ArticuloKFCId == 193)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 193 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 193),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                        if (tienda.TipoId == 3 && tienda.WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm == false && tienda.WCMedidaEspecial60_8x85cm == false && tienda.WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm == false && tienda.WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 193 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 193),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                }

                if (articulo.ArticuloKFCId == 286)
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == true && tienda.Arroz == true && tienda.PanelDeComplementosDigital == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == 286 && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", 286),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 124));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 125));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 126));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 127));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 128));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }

        }

        private static void AgregarArticulosTiendaCampañaExisteF(int articuloKFCId)
        {
            var categoria = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == articuloKFCId).FirstOrDefault().EquityFranquicia;

            var restaurantes = db.Database.SqlQuery<Tienda>("spGetRestaurantes").ToList();

            var tiendas = restaurantes.Where(x => x.EquityFranquicia == categoria && x.Activo == true).ToList();

            var selec = false;

            var articulos = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == articuloKFCId).ToList();
            var totalArticulos = db.ArticuloKFCs.Where(x => x.Eliminado == false && x.EquityFranquicia == categoria).ToList();

            var primero = totalArticulos.FirstOrDefault().ArticuloKFCId;

            foreach (var articulo in articulos)
            {
                if (articulo.ArticuloKFCId == primero) //194
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //195
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //196
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //197
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == false)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //198
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //199
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == false)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //200
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //201
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == false)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //202
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //203
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //204
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //205
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.COPETE_ESPECIAL_SOPORTE_LATERAL_4_VASOS == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //206
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.CopeteAETradicional == true && tienda.PET2Litros == false)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //207
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Postres == false && tienda.PET2Litros == false)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //208
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Postres == true && tienda.PET2Litros == false)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //209
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 1 || tienda.TipoId == 3 || tienda.TipoId == 4)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //210
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.DisplayDeBurbuja == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //211
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 1 || tienda.TipoId == 4)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //212
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //213
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Arroz == true && tienda.Hamburgesas == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //214
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Arroz == false && tienda.Hamburgesas == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //215
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Arroz == false && tienda.Hamburgesas == false)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //216
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.AreaDeJuegos == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //217
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //218
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //219
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3 || tienda.TipoId == 1 || tienda.TipoId == 4)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //220
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Delivery == true && tienda.TerceraReceta == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //221
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Delivery == true && tienda.Telefono == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //222
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Delivery == true && tienda.TerceraReceta == false)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //223
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3 || tienda.TipoId == 1 || tienda.TipoId == 4)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //224
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == true && tienda.Hamburgesas == true && tienda.PanelALaCartaCaribe == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //225
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == true && tienda.Hamburgesas == true && tienda.Ensalada == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //226
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == false && tienda.Hamburgesas == true && tienda.Ensalada == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //227
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == false && tienda.Hamburgesas == false && tienda.TipoId == 4)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //228
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == true && tienda.Arroz == true && tienda.PanelDeComplementosDigital == false)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //229
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == true && tienda.Arroz == true && tienda.PanelComplementosHolding == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //230
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == false && tienda.Arroz == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //231
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == false && tienda.Arroz == false)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //232
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.PanelDeInnovacion == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //233
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //234
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //235
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //236
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //237
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3 || tienda.TipoId == 1 || tienda.TipoId == 4)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //238
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //239
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 && tienda.TerceraReceta == false)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                        if (tienda.TipoId == 3 && tienda.TerceraReceta == false)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //240
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //241
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //242
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //243
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //244
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TipoId == 2 || tienda.TipoId == 3)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //245
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 268 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //246
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 334 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //247
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 390 && tienda.TelefonoPersonalizado == true || tienda.TiendaId == 391 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //248
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 286 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //249
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 287 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //250
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 288 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //251
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 290 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //252
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 291 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //253
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 297 && tienda.TelefonoPersonalizado == true || tienda.TiendaId == 384 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //254
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 299 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //255
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 300 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //256
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 304 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //257
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 307 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //258
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 308 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //259
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 309 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //260
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 310 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //261
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 314 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //262
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 319 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //263
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 327 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //264
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 328 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //265
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 329 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //266
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 335 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //267
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 336 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //268
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 337 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //269
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 339 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //270
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 343 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //271
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 347 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //272
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 349 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //273
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 351 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //274
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 356 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //275
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 367 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //276
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 368 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //277
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 373 && tienda.TelefonoPersonalizado == true || tienda.TiendaId == 379 && tienda.TelefonoPersonalizado == true || tienda.TiendaId == 382 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //278
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 374 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }

                if (articulo.ArticuloKFCId == primero) //279
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 375 && tienda.TelefonoPersonalizado == true || tienda.TiendaId == 381 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == primero) //280
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 394 && tienda.TelefonoPersonalizado == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == primero) //281
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 0)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == primero) //282
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 0)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == primero) //283
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 0)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == primero) //284
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 0)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == primero) //285
                {
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TiendaId == 0)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 287) //287
                {
                    primero = 287;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == true && tienda.Arroz == true && tienda.PanelDeComplementosDigital == true)
                        {
                            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == primero && cdt.TiendaId == tienda.TiendaId).FirstOrDefault();

                            if (tiendaArticulos == null)
                            {
                                db.Database.ExecuteSqlCommand(
                                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                                new SqlParameter("@ArticuloKFCId", primero),
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@Seleccionado", true));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 228));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 229));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 230));

                                db.Database.ExecuteSqlCommand(
                                "spEliminarArticuloPorTienda @TiendaId, @ArticuloKFCId",
                                new SqlParameter("@TiendaId", tienda.TiendaId),
                                new SqlParameter("@ArticuloKFCId", 231));
                            }
                            else
                            {
                                selec = true;

                                tiendaArticulos.Seleccionado = selec;
                                db.Entry(tiendaArticulos).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 288) //288
                {
                    primero = 288;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == false && tienda.Arroz == false && tienda.PanelComplementosHoldingMR == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 289) //289
                {
                    primero = 289;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == false && tienda.Autoexpress == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 290) //290
                {
                    primero = 290;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == true && tienda.Arroz == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 291) //291
                {
                    primero = 291;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 292) //292
                {
                    primero = 292;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == false)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 293) //293
                {
                    primero = 293;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 294) //294
                {
                    primero = 294;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.TerceraReceta == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 295) //295
                {
                    primero = 295;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 296) //296
                {
                    primero = 296;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 297) //297
                {
                    primero = 297;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 298) //298
                {
                    primero = 298;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 299) //299
                {
                    primero = 299;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.Arroz == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 300) //300
                {
                    primero = 300;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 301) //301
                {
                    primero = 301;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == false && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 302) //302
                {
                    primero = 302;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.TerceraReceta == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
                if (articulo.ArticuloKFCId == 303) //303
                {
                    primero = 303;

                    foreach (var tienda in tiendas)
                    {
                        if (tienda.Autoexpress == true && tienda.MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm == true)
                        {
                            AsignarMaterialF(primero, tienda.TiendaId);
                        }
                    }
                    break;
                }
                else
                {
                    primero++;
                }
            }
        }

        private static void AsignarMaterialF(int primero, int tiendaId)
        {
            var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == primero && cdt.TiendaId == tiendaId).FirstOrDefault();

            if (tiendaArticulos == null)
            {
                db.Database.ExecuteSqlCommand(
                "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                new SqlParameter("@ArticuloKFCId", primero),
                new SqlParameter("@TiendaId", tiendaId),
                new SqlParameter("@Seleccionado", true));
            }
            else
            {
                var selec = true;

                tiendaArticulos.Seleccionado = selec;
                db.Entry(tiendaArticulos).State = EntityState.Modified;
                db.SaveChanges();
            }
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


                            var habilitado = false;

                            if (materialTienda == null)
                            {
                                habilitado = false;
                            }
                            else
                            {
                                habilitado = materialTienda.Seleccionado;
                            }

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

            try
            {
                var articulos = db.Database.SqlQuery<spArticuloKFC>("spGetMaterialesCodigos").ToList();

                var familiasArt = articulos.GroupBy(f => new { f.CodigoFamilia }).ToList();

                //foreach (var articulo in articulos.GroupBy(f => new { f.CodigoFamilia, f.Descripcion }))
                foreach (var familia in familiasArt)
                {
                    var materialesFamilias = articulos.Where(x => x.CodigoFamilia == familia.Key.CodigoFamilia).ToList();
                    //var familias = db.ArticuloKFCs.Where(f => f.Familia.Codigo == familia).ToList();

                    for (int f = 0; f < materialesFamilias.Count(); f++)
                    {
                        var articuloId = materialesFamilias[f].Descripcion;

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
                            var codigo = idCampaña + materialesFamilias[f].CodigoFamilia + consecutivo + f;

                            db.Database.ExecuteSqlCommand(
                            "spAgregarCodigos @ArticuloKFCId, @CampañaId, @Codigo",
                            new SqlParameter("@ArticuloKFCId", materialesFamilias[f].ArticuloKFCId),
                            new SqlParameter("@CampañaId", (int)id),
                            new SqlParameter("@Codigo", Convert.ToInt32(codigo)));

                        }

                    }


                }

                return new Response { Succeeded = true, };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Message = ex.Message,
                    Succeeded = false,
                };
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

            //var articulos = db.ArticuloKFCs.Where(cdt => cdt.EquityFranquicia == tipoTienda).ToList();

            var articulos = db.Database.SqlQuery<ArticuloKFC>("spGetMaterialesTodos @Categoria",
                new SqlParameter("@Categoria", tipoTienda)).ToList();

            AgregarTiendaArticulosCampañaExiste(tiendas);

            foreach (var articulo in articulos)
            {
                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == articulo.ArticuloKFCId).FirstOrDefault().Activo;

                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == articulo.ArticuloKFCId && cdt.TiendaId == tiendaId).FirstOrDefault();

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", articulo.ArticuloKFCId),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", false));
                    }
                }
            }

            return new Response { Succeeded = true, };
        }

        public static Response AgregarTiendaArticulosF(int tiendaId)
        {
            var tipoTienda = db.Tiendas.Where(a => a.TiendaId == tiendaId).FirstOrDefault().EquityFranquicia;

            var tiendas = db.Tiendas.Where(x => x.TiendaId == tiendaId).FirstOrDefault();

            //var articulos = db.ArticuloKFCs.Where(cdt => cdt.EquityFranquicia == tipoTienda).ToList();

            var articulos = db.Database.SqlQuery<ArticuloKFC>("spGetMaterialesTodos @Categoria",
                new SqlParameter("@Categoria", tipoTienda)).ToList();

            AgregarTiendaArticulosCampañaExisteF(tiendas);

            foreach (var articulo in articulos)
            {
                var articuloActivo = db.ArticuloKFCs.Where(x => x.ArticuloKFCId == articulo.ArticuloKFCId).FirstOrDefault().Activo;

                var tiendaArticulos = db.TiendaArticulos.Where(cdt => cdt.ArticuloKFCId == articulo.ArticuloKFCId && cdt.TiendaId == tiendaId).FirstOrDefault();

                if (articuloActivo == true)
                {
                    if (tiendaArticulos == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendasMaterialC @ArticuloKFCId, @TiendaId, @Seleccionado",
                        new SqlParameter("@ArticuloKFCId", articulo.ArticuloKFCId),
                        new SqlParameter("@TiendaId", tiendaId),
                        new SqlParameter("@Seleccionado", false));
                    }
                }
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

        public static Response AgregarReglasCaracteristicas(int reglaId, string cat)
        {
            var caracteristicas = db.ReglasCatalogo.Where(x => x.Categoria == cat).ToList();

            foreach (var caracteristica in caracteristicas)
            {
                var reglas = db.Reglas.Where(x => x.ArticuloKFC.EquityFranquicia == cat).ToList();

                if (reglas != null)
                {
                    foreach (var regla in reglas)
                    {
                        var existentes = db.ReglasCaracteristicas.Where(x => x.ReglaId == regla.ReglaId && x.ReglaCatalogoId == caracteristica.ReglaCatalogoId).FirstOrDefault();

                        if (existentes == null)
                        {
                            db.Database.ExecuteSqlCommand(
                            "spAgregarCaracteristicasReglas @ReglaId, @ReglaCatalogoId, @Seleccionado",
                            new SqlParameter("@ReglaId", regla.ReglaId),
                            new SqlParameter("@ReglaCatalogoId", caracteristica.ReglaCatalogoId),
                            new SqlParameter("@Seleccionado", false));
                        }
                    }
                }
            }
            return new Response { Succeeded = true, };
        }

        public static Response AgregarTiendasCaracteristicas(int reglaIdTienda, string cat, int fc, int fs, int il, int sb)
        {
            var tiendas = db.Tiendas.Where(x => x.EquityFranquicia == cat).ToList();

            if (reglaIdTienda != 0)
            {
                foreach (var tienda in tiendas)
                {
                    var valor = "NO";

                    if (tienda.TipoId == fc || tienda.TipoId == fs || tienda.TipoId == il || tienda.TipoId == sb)
                    {
                        valor = "SI";
                    }

                    var existentes = db.TiendaCaracteristicas.Where(x => x.ReglaCatalogoId == reglaIdTienda && x.TiendaId == tienda.TiendaId).FirstOrDefault();

                    if (existentes == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendaCaracteristicas @TiendaId, @ReglaCatalogoId, @Valor",
                        new SqlParameter("@TiendaId", tienda.TiendaId),
                        new SqlParameter("@ReglaCatalogoId", reglaIdTienda),
                        new SqlParameter("@Valor", valor));
                    }
                }
            }
            else
            {
                foreach (var tienda in tiendas)
                {
                    var valor = "NO";

                    if (tienda.TipoId == fc || tienda.TipoId == fs || tienda.TipoId == il || tienda.TipoId == sb)
                    {
                        valor = "SI";
                    }

                    var existentes = db.TiendaCaracteristicas.Where(x => x.ReglaCatalogoId == reglaIdTienda && x.TiendaId == tienda.TiendaId).FirstOrDefault();

                    if (existentes == null)
                    {
                        db.Database.ExecuteSqlCommand(
                        "spAgregarTiendaCaracteristicas @TiendaId, @ReglaCatalogoId, @Valor",
                        new SqlParameter("@TiendaId", tienda.TiendaId),
                        new SqlParameter("@ReglaCatalogoId", reglaIdTienda),
                        new SqlParameter("@Valor", valor));
                    }
                }
            }
            return new Response { Succeeded = true, };
        }

        public static Response MovimientosBitacora(int usuarioId, string modulo, string movimiento)
        {
            db.Database.ExecuteSqlCommand(
            "spAgregarBitacora @Fecha, @UsuarioId, @Modulo, @Movimiento",
            new SqlParameter("@Fecha", DateTime.Now),
            new SqlParameter("@UsuarioId", usuarioId),
            new SqlParameter("@Modulo", modulo),
            new SqlParameter("@Movimiento", movimiento));

            return new Response { Succeeded = true, };
        }
    }
}