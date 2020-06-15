using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Procesos
{
    internal class Consultas
    {
        
        internal class sistema {
            public static PD.SV_SYSTEM ByCodes(string ambiente, string moneda) {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.sistemas.ByCodes(ambiente,moneda);
            }
            public static List<PD.SV_SYSTEM> All()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.sistemas.All();
            }
            public static List<PD.SV_SYSTEM> AllWithIFRS(bool hasIFRS)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.sistemas.AllWithIFRS(hasIFRS);
            }
            public static PD.SV_SYSTEM Default()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.sistemas.Default;
            }
        }
        internal class zonas {
            public static List<PD.GENERIC_VALUE> All()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.zonas.All();
            }
            public static List<PD.GENERIC_VALUE> SearchList()
            {
                var process = All();
                process.Insert(0, new PD.GENERIC_VALUE(0, "TODAS", "ZONE"));
                return process;
            }
        }

        internal class subzonas
        {
            public static List<PD.GENERIC_VALUE> ByZone(PD.GENERIC_VALUE zona)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.subzonas.ByZone(zona);
            }
        }
        internal class estados_aprobacion {
            public static string[] All
            {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                    {
                        var estados = cServ.Repo.EstadoAprobacion.All;
                            return estados.Select(e => e.code).ToArray();
                    }
                }
            }
            public static string[] OnlyActive
            {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                    {
                        var estados = cServ.Repo.EstadoAprobacion.OnlyActive;
                        return estados.Select(e => e.code).ToArray();
                    }
                }
            }
            public static string[] OnlyDigited
            {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                    {
                        var estados = cServ.Repo.EstadoAprobacion.OnlyDigited;
                        return estados.Select(e => e.code).ToArray();
                    }
                }
            }
            public static string[] NoDeleted
            {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                    {
                        return cServ.Repo.EstadoAprobacion.ArrNoDeleted;
                    }
                }
            }
            public static string[] Default {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                    {
                        var estados = cServ.Repo.EstadoAprobacion.Default;
                        return estados.Select(e => e.code).ToArray();
                    }
                }
            }
        }
        internal class clases {
            public static List<PD.GENERIC_VALUE> SearchList()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.Clases.SearchList();
            }
            public static List<PD.GENERIC_VALUE> ByType(PD.GENERIC_VALUE tipo, bool para_ingreso)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.Clases.ByType(tipo,para_ingreso);
            }
        }
        internal class subclases
        {
            public static List<PD.GENERIC_VALUE> ByKind(PD.GENERIC_VALUE clase)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.subclases.ByKind(clase);
            }
            public static PD.SV_SUBKIND ByGeneric(PD.GENERIC_VALUE subkind)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.subclases.ByGeneric(subkind);
            }
        }
        internal class vigencias
        {
            public static List<PD.GENERIC_VALUE> SearchDownsList()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.Vigencias.SearchDownsList();
            }

            public static int[] Actives
            {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                    {
                        var vigencias = cServ.Repo.Vigencias.Ups();
                        return vigencias.Select(v => v.id).ToArray();
                    }
                }
            }
            public static int[] Downs
            {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                    {
                        var vigencias = cServ.Repo.Vigencias.Downs();
                        return vigencias.Select(v => v.id).ToArray();
                    }
                }
            }
            public static int[] Sells
            {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                    {
                        var vigencias = cServ.Repo.Vigencias.Sells();
                        return vigencias.Select(v => v.id).ToArray();
                    }
                }
            }
            public static int[] Disposals
            {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                    {
                        var vigencias = cServ.Repo.Vigencias.Disposals();
                        return vigencias.Select(v => v.id).ToArray();
                    }
                }
            }
            public static int[] All
            {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                    {
                        var vigencias = cServ.Repo.Vigencias.All();
                        return vigencias.Select(v => v.id).ToArray();
                    }
                }
            }
        }
        internal class tipos
        {
            public static List<PD.GENERIC_VALUE> All()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.Tipos.All();
            }
        }
        internal class monedas
        {
            public static PD.GENERIC_VALUE[] All()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.Monedas.All().ToArray();
            }
        }
        internal class ambientes
        {
            public static List<PD.SV_ENVIORMENT> GetAll()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.ambientes.GetAll();
            }
        }
        internal class gestiones
        {
            public static PD.GENERIC_VALUE[] All()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.gestiones.All().ToArray();
            }
        }
        internal class categorias
        {
            public static PD.GENERIC_VALUE[] All()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.categorias.All().ToArray();
            }
        }
        internal class metodo_val
        {
            public static PD.GENERIC_VALUE[] All()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.MetodosRev.All().ToArray();
            }
        }
        internal class parametros
        {
            public static PD.SV_PARAMETER PrecioBase
            {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                        return cServ.Repo.parametros.PrecioBase;
                }
            }
            public static PD.SV_PARAMETER Credito
            {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                        return cServ.Repo.parametros.Credito;
                }
            }
            public static PD.SV_PARAMETER VidaUtil
            {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                        return cServ.Repo.parametros.VidaUtil;
                }
            }
            public static PD.SV_PARAMETER ValorResidual
            {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                        return cServ.Repo.parametros.ValorResidual;
                }
            }
        }
        internal class predeter_ifrs
        {
            public static decimal porcentaje_valor_residual(PD.GENERIC_VALUE clase)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.predetIFRS.residual_value_rate(clase);
            }
        }

        internal class inventario
        {
            public static PD.RespuestaAccion GENERAR_CODIGO(int batch_id, PD.GENERIC_VALUE kind)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.GENERAR_CODIGO(batch_id, kind);
            }

            public static List<Estructuras.DetalleArticulo> GetDetalleArticulo(int codLote, int partIndex, int quantity)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                {
                    PD.SV_PART part = cServ.Repo.Partes.ByLotePart(codLote, partIndex);
                    var info = cServ.Repo.inv_articulos.GetDetalleArticulo(part.id);
                    var resultado = new List<Estructuras.DetalleArticulo>();
                    int contad = 0;
                    foreach (var inf in info)
                    {
                        Estructuras.DetalleArticulo dato = new Estructuras.DetalleArticulo();
                        dato.producto = inf.code;
                        dato.codigoLote = codLote;
                        dato.parteLote = partIndex;
                        dato.procesar = (contad < quantity);
                        dato.rowId = inf.id;
                        dato.PartId = inf.part_id;
                        contad++;

                        resultado.Add(dato);
                    }
                    return resultado;
                }
            }
            public static List<PD.SV_ARTICLE> GetCodigosArtByLote(int batch_id)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                {
                    var partes = cServ.Repo.Partes.ByLote(batch_id);
                    var partsId = partes.Select(p => p.id).ToArray();
                    return cServ.Repo.inv_articulos.ByParts(partsId);
                }
            }
            public static List<PD.SV_ATTRIBUTE> GetActiveAttributes()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.inv_atributos.AllActive();
            }
            public static List<PD.SV_ARTICLE_DETAIL> GetDetailArtByLote(int batch_id)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                {
                    return cServ.Repo.inv_articulos_details.ByBatch(batch_id);
                }
            }

            public static PD.SV_ARTICLE_DETAIL GetDetailAttrByLote(int batch_id, int attribute_id)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                {
                    return cServ.Repo.inv_articulos_details.ForArticle(batch_id, null, attribute_id);
                }
            }

            public static int busca_nombre_foto(string nuevo_nombre_foto)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                {
                    var FotoAttributes = cServ.Repo.inv_atributos.GetFotoType();
                    List<PD.SV_ARTICLE_DETAIL> FotoValues = cServ.Repo.inv_articulos_details.GetValuesByAttribute(FotoAttributes);
                    return FotoValues.Where(f => f.detalle == nuevo_nombre_foto).Count();
                }
            }
            public static PD.RespuestaAccion INGRESO_ATRIB_LOTE(int lote_art, int atributo_id, string detalle, bool mostrar)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                {
                    return cServ.Repo.INGRESO_ATRIB_LOTE(lote_art, atributo_id, detalle, mostrar);
                }
            }
            public static PD.RespuestaAccion BORRAR_ATRIBUTOxLOTE(int lote_art, int atributo_id)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                {
                    return cServ.Repo.BORRAR_ATRIBUTOxLOTE(lote_art, atributo_id);
                }
            }
        }
        internal class movimientos
        {
            public static PD.RespuestaAccion venta_act(int codigo_articulo, int parte_articulo, DateTime newfecha, int newcantidad, string usuario, List<Estructuras.DetalleArticulo> detalle)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.venta_act(codigo_articulo, parte_articulo, newfecha, newcantidad, usuario, detalle);
            }
            public static PD.RespuestaAccion castigo_act(int codigo_articulo, int parte_articulo, DateTime newfecha, int newcantidad, string usuario, List<Estructuras.DetalleArticulo> detalle)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.castigo_act(codigo_articulo, parte_articulo, newfecha, newcantidad, usuario, detalle);
            
            }
            public static PD.RespuestaAccion CAMBIO_ZONA(int codigo_articulo, int parte_articulo, DateTime newfecha, PD.GENERIC_VALUE newzona, PD.GENERIC_VALUE newsubzona, int newcantidad, string usuario, List<Estructuras.DetalleArticulo> detalle)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.CAMBIO_ZONA(codigo_articulo, parte_articulo, newfecha,newzona,newsubzona,newcantidad, usuario, detalle);
            
            }
        }
        internal class ventas
        {
            public static int CheckDocName(string DesiredNameDoc)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.ventas.CheckDocName(DesiredNameDoc);
            
            }
            public static int CheckArticlePartUsed(int BatchId, int PartIndex)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                {
                     var PartObj = cServ.Repo.Partes.ByLotePart(BatchId,PartIndex);
                     return cServ.Repo.ventas.CheckArticlePartUsed(PartObj.id);
                }
            }
            public static PD.RespuestaAccion CREATE_SALES_DOC(string DocNumber, decimal ExtendedCost, DateTime DocDate, List<Estructuras.DisplayVentaPrecio> DetailsList)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                {
                    return cServ.Repo.CREATE_SALES_DOC(DocNumber, ExtendedCost, DocDate, DetailsList);
                }
            }
        }

        internal class lotes
        {
            public static List<PD.SV_BATCH_ARTICLE> GetLotesAbiertos()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.lotes.GetLotesAbiertos();
            }
            public static PD.RespuestaAccion INGRESO_LOTE(string descripcion, DateTime fecha_compra, string cod_proveedor, string documento, decimal total_compra, int vida_util, bool derecho_credito, DateTime fecha_contab, int origen_id, PD.GENERIC_VALUE CtiPo)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.INGRESO_LOTE(descripcion, fecha_compra, cod_proveedor, documento, total_compra, vida_util, derecho_credito, fecha_contab, origen_id, CtiPo);   
            }
            public static PD.RespuestaAccion MODIFICA_LOTE(int batch_id, string descripcion, string cod_proveedor, string documento, decimal total_compra, int vida_util, DateTime fecha_contab)
            {
                //fecha_compra no se puede modificar, pues compone la codigo de inventario
                //origen_id y CtiPo no se puede modificar
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.MODIFICA_LOTE(batch_id,descripcion, cod_proveedor, documento, total_compra, vida_util, fecha_contab);
            }
            public static PD.RespuestaAccion BORRAR_AF(int codigo_lote)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.BORRAR_AF(codigo_lote);
            }

            public static PD.RespuestaAccion ACTIVAR_AF(int codigo_lote)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.ACTIVAR_AF(codigo_lote);
            }
        }
        internal class partes
        {
            public static List<PD.SV_PART> ByLote(int lote)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.Partes.ByLote(lote);
            }
            public static PD.RespuestaAccion REGISTER_PURCHASE(int batch_id, DateTime fecha_compra, decimal valor_total, int cantidad_total)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.REGISTER_PURCHASE(batch_id, fecha_compra,valor_total, cantidad_total);
            }
        }
        internal class trx_cabeceras
        {
            public static List<PD.SV_TRANSACTION_HEADER> ByParte(int parte)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.cabeceras.ByParte(parte); 
            }
            public static PD.RespuestaAccion REGISTER_PURCHASE_HEAD(List<PD.GENERIC_VALUE> parts, DateTime fecha_compra, PD.GENERIC_VALUE zona, PD.GENERIC_VALUE subzona, PD.GENERIC_VALUE clase, PD.GENERIC_VALUE subclase, PD.GENERIC_VALUE categoria, PD.GENERIC_VALUE gestion, string usuario)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.REGISTER_PURCHASE_HEAD(parts, fecha_compra,zona,subzona,clase,subclase,categoria,gestion,usuario);
            }
            public static PD.RespuestaAccion MODIF_PURCHASE_HEAD(List<PD.SV_PART> partes, PD.GENERIC_VALUE zona, PD.GENERIC_VALUE subzona, PD.GENERIC_VALUE subclase, PD.GENERIC_VALUE categoria, PD.GENERIC_VALUE gestion, string usuario)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.MODIF_PURCHASE_HEAD(partes, zona, subzona, subclase, categoria, gestion, usuario);
            }
        }
        internal class trx_detalles
        {
            public static PD.RespuestaAccion REGISTER_PURCHASE_DETAIL(List<PD.GENERIC_VALUE> cabeceras, PD.SV_SYSTEM OSystem, bool depreciar, bool con_credito)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.REGISTER_PURCHASE_DETAIL(cabeceras, OSystem, depreciar, con_credito);
            }
            public static PD.RespuestaAccion MODIF_PURCHASE_DETAIL(int[] TrxHeadIds, PD.SV_SYSTEM OSystem, bool depreciar, bool con_credito)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.MODIF_PURCHASE_DETAIL(TrxHeadIds, OSystem, depreciar, con_credito);
            }

            public static List<PD.SV_TRANSACTION_DETAIL> GetByHeadsSystem(int[] head_ids, PD.SV_SYSTEM system)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.detalles.GetByHeadsSystem(head_ids, system);
            }
        }
        internal class detalle_parametros
        {
            public static List<PD.PARAM_VALUE> ByHead_Sys(int HeadId, int SysId)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.DetallesParametros.ByHead_Sys(HeadId, SysId); 
            }
            public static PD.RespuestaAccion REGISTER_PURCHASE_PARAM(int[] cabeceras, PD.SV_SYSTEM sistema, PD.SV_PARAMETER parametro, decimal valor, bool withResiduo = false)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.REGISTER_PURCHASE_PARAM(cabeceras,sistema,parametro,valor,withResiduo);
            }
            public static PD.RespuestaAccion MODIF_PURCHASE_PARAM(int[] cabeceras, PD.SV_SYSTEM sistema, PD.SV_PARAMETER parametro, decimal valor, bool withResiduo = false)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.MODIF_PURCHASE_PARAM(cabeceras, sistema, parametro, valor, withResiduo);
            }
            public static PD.RespuestaAccion MODIFICA_IFRS(int batch_id, decimal valor_residual, int vida_util,int metod_val, decimal[] preparacion, decimal[] transporte, decimal[] montaje, decimal[] desmantelamiento, decimal[] honorarios)
            {
                var result = new PD.RespuestaAccion();
                try
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                    {
                        var partes_id = cServ.Repo.Partes.ByLote(batch_id)
                                        .Select(l => l.id)
                                        .ToArray();
                        var IFRS_systems = cServ.Repo.sistemas.IFRS();
                        var heads_ids = cServ.Repo.cabeceras.ByPartes(partes_id)
                                        .Select(h => h.id)
                                        .ToArray();
                        //metod_val
                        result = cServ.Repo.MODIF_PURCHASE_HEAD_MethodVal(heads_ids, metod_val);


                        foreach (var currSystem in IFRS_systems)
                        {
                            int index = ((currSystem.CURRENCY == "CLP")?0:1);

                            PD.SV_PARAMETER CurrParam;
                            //valor_residual
                            CurrParam = cServ.Repo.parametros.ValorResidual;
                            if (valor_residual == 0)
                                result = cServ.Repo.DELETE_PURCHASE_PARAM(heads_ids, currSystem, CurrParam);
                            else
                                result = cServ.Repo.MODIF_PURCHASE_PARAM(heads_ids, currSystem, CurrParam, valor_residual,false);
                            if (result.codigo < 0) return result;
                            //vida_util
                            CurrParam = cServ.Repo.parametros.VidaUtil;
                            if (vida_util == 0)
                                result = cServ.Repo.DELETE_PURCHASE_PARAM(heads_ids, currSystem, CurrParam);
                            else
                                result = cServ.Repo.MODIF_PURCHASE_PARAM(heads_ids, currSystem, CurrParam, vida_util, false);
                            if (result.codigo < 0) return result;
                            //decimal valor_procesar;
                            //preparacion
                            CurrParam = cServ.Repo.parametros.Preparacion;

                            if (preparacion[index] == 0)
                                result = cServ.Repo.DELETE_PURCHASE_PARAM(heads_ids, currSystem, CurrParam);
                            else
                                result = cServ.Repo.MODIF_PURCHASE_PARAM(heads_ids, currSystem, CurrParam, preparacion[index], false);
                            if (result.codigo < 0) return result;
                            //transporte
                            CurrParam = cServ.Repo.parametros.Transporte;
                            if (transporte[index] == 0)
                                result = cServ.Repo.DELETE_PURCHASE_PARAM(heads_ids, currSystem, CurrParam);
                            else
                                result = cServ.Repo.MODIF_PURCHASE_PARAM(heads_ids, currSystem, CurrParam, transporte[index], false);
                            if (result.codigo < 0) return result;
                            //montaje
                            CurrParam = cServ.Repo.parametros.Montaje;
                            if (montaje[index] == 0)
                                result = cServ.Repo.DELETE_PURCHASE_PARAM(heads_ids, currSystem, CurrParam);
                            else
                                result = cServ.Repo.MODIF_PURCHASE_PARAM(heads_ids, currSystem, CurrParam, montaje[index], false);
                            if (result.codigo < 0) return result;
                            //desmantelamiento
                            CurrParam = cServ.Repo.parametros.Desmantelamiento;
                            if (desmantelamiento[index] == 0)
                                result = cServ.Repo.DELETE_PURCHASE_PARAM(heads_ids, currSystem, CurrParam);
                            else
                                result = cServ.Repo.MODIF_PURCHASE_PARAM(heads_ids, currSystem, CurrParam, desmantelamiento[index], false);
                            if (result.codigo < 0) return result;
                            //honorarios
                            CurrParam = cServ.Repo.parametros.Honorario;
                            if (honorarios[index] == 0)
                                result = cServ.Repo.DELETE_PURCHASE_PARAM(heads_ids, currSystem, CurrParam);
                            else
                                result = cServ.Repo.MODIF_PURCHASE_PARAM(heads_ids, currSystem, CurrParam, honorarios[index], false);
                            if (result.codigo < 0) return result;
                        }
                    }
                    result.set_ok();
                }
                catch (Exception ex)
                {
                    result.set(-1, ex.StackTrace);
                }

                return result;
            }

            public static List<PD.T_CUADRO_IFRS> CUADRO_INGRESO_IFRS(int batch_id)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.CUADRO_INGRESO_IFRS(batch_id);
            }
        }

        internal class periodo_contable
        {
            public static List<ACode.Vperiodo> ingreso()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.PeriodoContable.ingreso();
            }
            public static ACode.Vperiodo abierto()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.PeriodoContable.abierto();
            }
        }
        internal class proveedores
        {
            public static List<PD.SV_PROVEEDOR> listar()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.Proveedor.listar();
            }
        }

        internal class documentos
        {
            public static int buscar_entrada_obc(string document,string provider, int zoneId)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                {
                    var findDoc = cServ.Repo.documentos.ByNumProv(document, provider);
                    return (findDoc != null? 1:0);
                    //if (findDoc != null)
                    //{
                        
                    //}
                    //else
                    //    return 0;
                }
            }
            public static string defaultDocument
            { get { return ServiceProcess.Repositories.DOCUMENTS.defaultDocument; } }
            public static string defaultProveed
            { get { return ServiceProcess.Repositories.DOCUMENTS.defaultProveed; } }
        }

        

        #region Opciones

        internal class arr
        {
            public static PD.GENERIC_VALUE[] meses { get { return Consultas.meses.ToArray(); } }
            public static PD.GENERIC_VALUE[] years { get { return Consultas.years.ToArray(); } }
            public static PD.GENERIC_VALUE[] acumulados { get { return Consultas.acumulados.ToArray(); } }
            public static PD.GENERIC_VALUE[] acumulados_wMes { get { return Consultas.acumulados_wMes.ToArray(); } }

            public static PD.GENERIC_VALUE[] opciones_menu_obc { get { return Consultas.opciones_menu_obc.ToArray(); } }
        }

        public static List<PD.GENERIC_VALUE> meses
        {
            get {
                var x = new List<PD.GENERIC_VALUE> { };
                string tipo = "Mes";
                for (int i = 1; i <= 12; i++) {
                    var mes = new DateTime(1,i,1).ToString("MMMM");
                    mes = mes.Substring(0,1).ToUpper() + mes.Substring(1);
                    x.Add(new PD.GENERIC_VALUE(i, mes, tipo));
                }
                return x;
            }
        }
        public static List<PD.GENERIC_VALUE> years
        {
            get
            {
                var x = new List<PD.GENERIC_VALUE> { };
                string tipo = "Año";
                for (int i = DateTime.Now.Year; i >= 2012; i--)
                {
                    x.Add(new PD.GENERIC_VALUE(i, i.ToString(), tipo));
                }
                return x;
            }
        }
        public static List<PD.GENERIC_VALUE> acumulados
        {
            get
            {
                var x = new List<PD.GENERIC_VALUE>();
                var tipo = "Acumulado";
                x.Add(new PD.GENERIC_VALUE(1, "Regular", tipo));
                x.Add(new PD.GENERIC_VALUE(2, "Japones", tipo));
                return x;
            }
        }
        public static List<PD.GENERIC_VALUE> acumulados_wMes
        {
            get
            {
                var x = new List<PD.GENERIC_VALUE>();
                var tipo = "Acumulado";
                x.Add(new PD.GENERIC_VALUE(0, "Solo Mes", tipo));
                x.Add(new PD.GENERIC_VALUE(1, "Regular", tipo));
                x.Add(new PD.GENERIC_VALUE(2, "Japones", tipo));
                return x;
            }
        }
        public static List<PD.GENERIC_VALUE> opciones_menu_obc
        {
            get
            {
                var result = new List<PD.GENERIC_VALUE> { };
                string tipo = "menu_obc";
                result.Add(new PD.GENERIC_VALUE(1, "Saldo", tipo));
                result.Add(new PD.GENERIC_VALUE(2, "Entradas", tipo));
                result.Add(new PD.GENERIC_VALUE(3, "Salidas", tipo));
                return result;
            }
        }
        #endregion


        internal class obc
        {
            public static decimal TotalYen(int codigo_lote)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.ObrasConstruccion.TotalYen(codigo_lote);
            }
            public static PD.RespuestaAccion INGRESO_OBC(DateTime fechaIngreso, int zonaId, string proveedor, string documento, string glosaIngreso, decimal montoOriginal, DateTime fechaContable )
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.INGRESO_OBC(fechaIngreso, zonaId, proveedor, documento, glosaIngreso, montoOriginal, fechaContable);
            }
            public static PD.RespuestaAccion EGRESO_OBC(int codigoEntrada, int codigoBatchSalida, decimal montoOriginal, int zonaId)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.EGRESO_OBC(codigoEntrada, codigoBatchSalida,montoOriginal,zonaId);
            }
            public static PD.RespuestaAccion EGRESO_GASTO(int? id, int codigoEntrada, DateTime fechaSalida, decimal montoOriginal, int aprovalId)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.EGRESO_GASTO(id, codigoEntrada, fechaSalida, montoOriginal, aprovalId);
            }

            public static List<Estructuras.DetalleOBC> saldos_entradas(DateTime moment, string CurrencyCode)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                {
                    var result = new List<Estructuras.DetalleOBC>();
                    try
                    {
                        var data = cServ.Repo.ObrasConstruccion.SaldoEntradas(moment, CurrencyCode);
                        var temporales = cServ.Repo.ObrasConstruccion.SalidasAbiertas();
                        foreach (PD.SV_ASSET_CONSTRUCTION dt in data)
                        {
                            
                            decimal CantidadTomada = temporales
                                        .Where(tp => tp.entrada_id == dt.id)
                                        .Select(tp => tp.TotalByCurrency(CurrencyCode))
                                        .DefaultIfEmpty(0)
                                        .Sum();
                            var linea = new Estructuras.DetalleOBC();
                            linea.codigo = dt.id;
                            linea.descripcion = dt.descrip;
                            linea.fecha = dt.trx_date;
                            linea.zona = dt.zone;
                            linea.saldo = dt.ocupado - CantidadTomada;
                            result.Add(linea);
                        }
                    }
                    catch (Exception e)
                    {
                        Mensaje.Error(e.StackTrace);
                    }
                    return result;
                }
            }

            public static List<Estructuras.DetalleOBC> saldos_entradas(DateTime moment, string CurrencyCode, IEnumerable<Estructuras.DetalleOBC> borradores)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                {
                    var result = new List<Estructuras.DetalleOBC>();
                    try
                    {
                        var data = cServ.Repo.ObrasConstruccion.SaldoEntradas(moment, CurrencyCode);
                        var temporales = cServ.Repo.ObrasConstruccion.SalidasAbiertas();
                        foreach (PD.SV_ASSET_CONSTRUCTION dt in data)
                        {
                            int[] ProcessId = borradores
                                .Where(b => b.codigo == dt.id)
                                .Select(b => (int)b.idGet())
                                .ToArray();
                            decimal CantidadTomada = temporales
                                        .Where(tp => tp.entrada_id == dt.id
                                        && ! ProcessId.Contains(tp.id))
                                        .Select(tp => tp.TotalByCurrency(CurrencyCode))
                                        .DefaultIfEmpty(0)
                                        .Sum();
                            var linea = new Estructuras.DetalleOBC();
                            linea.codigo = dt.id;
                            linea.descripcion = dt.descrip;
                            linea.fecha = dt.trx_date;
                            linea.zona = dt.zone;
                            linea.saldo = dt.ocupado - CantidadTomada;
                            result.Add(linea);
                        }
                    }
                    catch (Exception e)
                    {
                        Mensaje.Error(e.StackTrace);
                    }
                    return result;
                }
            }

            public static List<Estructuras.DetalleOBC> salidas_abiertas()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                {
                    var result = new List<Estructuras.DetalleOBC>();
                    var data = cServ.Repo.ObrasConstruccion.SalidasAbiertas();
                    foreach (PD.SV_ASSET_CONSTRUCTION dt in data)
                    {
                        var entrada = cServ.Repo.ObrasConstruccion.IngresoById((int)dt.entrada_id);
                        var linea = new Estructuras.DetalleOBC();
                        linea.codigo = entrada.id;
                        linea.descripcion = entrada.descrip;
                        linea.fecha = dt.trx_date;
                        linea.zona = dt.zone;
                        linea.saldo = dt.TotalByCurrency("CLP");

                        linea.idSet(dt.id);
                        linea.maximoSet(entrada.ocupado);

                        result.Add(linea);
                    }

                    return result;
                }
            }
        }

        internal class tipo_cambio
        {
            public static decimal YEN(DateTime fecha)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.TipoCambio.YEN(fecha);
            }
        }

        #region Data Procesada

        public static PD.SINGLE_DETAIL data_ingreso_financiero(int codigo_lote)
        {
            using(var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Proceso.ingreso_financiero(codigo_lote);
        
        }
        public static PD.SINGLE_DETAIL data_ingreso_IFRS(int codigo_lote)
        {
            using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Proceso.ingreso_ifrs(codigo_lote);

        }
        //public static bool check_ingreso_ifrs(int codigo_lote)
        //{
        //    using (var cServ = new ServiceProcess.ServiceAFN2())
        //        return cServ.Proceso.ingreso_ifrs_check(codigo_lote);
        //}

        public static List<PD.DETAIL_DEPRECIATE> depreciar(int año, int mes, string usuario)
        {
            using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Proceso.DepreciacionProcesoMensual(año, mes, usuario);
        }

        public static List<PD.DETAIL_PROCESS> buscar_Articulo(DateTime desde, DateTime hasta, int codigo, string descrip, string zona, int[] vigencias, string[] origenes, Vistas.Busquedas.moment_data cuando, bool CheckPost)
        {
            using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Proceso.buscar_Articulo(desde, hasta, codigo, descrip, zona, vigencias, origenes, cuando, CheckPost);
        }
        public static List<PD.SV_PROVEEDOR> buscar_proveedor(string codigo, string nombre)
        {
            using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Proceso.buscar_proveedor(codigo, nombre);
        }

        public static List<PD.DETAIL_MOVEMENT> listar_cambios(PD.SV_SYSTEM Vsistema,ACode.Vperiodo periodo,int BatchId, int PartIndex, DateTime desde, DateTime hasta, PD.SV_KIND[] Vclases, PD.SV_ZONE[] Vzonas)
        {
            using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Proceso.changed_movimiento(Vsistema, periodo,BatchId, PartIndex, desde, hasta, Vclases, Vzonas);
        }

        #endregion
    }
}
