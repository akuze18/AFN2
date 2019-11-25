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
                        var estados = cServ.Repo.EstadoAprobacion.NoDeleted;
                        return estados.Select(e => e.code).ToArray();
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
                        var vigencias = cServ.Repo.Vigencias.Downs();
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
                        var vigencias = cServ.Repo.Vigencias.Downs();
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
        }

        internal class lotes
        {
            public static PD.RespuestaAccion INGRESO_LOTE(string descripcion, DateTime fecha_compra, string cod_proveedor, string documento, decimal total_compra, int vida_util, bool derecho_credito, DateTime fecha_contab, int origen_id, PD.GENERIC_VALUE CtiPo)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.INGRESO_LOTE(descripcion, fecha_compra, cod_proveedor, documento, total_compra, vida_util, derecho_credito, fecha_contab, origen_id, CtiPo);   
            }
            public static PD.RespuestaAccion MODIFICA_LOTE(int batch_id, string descripcion, string cod_proveedor, string documento, decimal total_compra, int vida_util, bool derecho_credito, DateTime fecha_contab)
            {
                //fecha_compra no se puede modificar, pues compone la codigo de inventario
                //origen_id y CtiPo no se puede modificar
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.MODIFICA_LOTE(batch_id,descripcion, cod_proveedor, documento, total_compra, vida_util, derecho_credito, fecha_contab);
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
            public static PD.RespuestaAccion REGISTER_PURCHASE_DETAIL(List<PD.GENERIC_VALUE> cabeceras, PD.SV_SYSTEM sistema, bool depreciar, bool con_credito)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.REGISTER_PURCHASE_DETAIL(cabeceras, sistema, depreciar, con_credito);
            }

            public static List<PD.SV_TRANSACTION_DETAIL> GetByPartsSystem(int[] head_ids, PD.SV_SYSTEM system)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.detalles.GetByPartsSystem(head_ids, system);
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
            public static PD.RespuestaAccion MODIFICA_IFRS(int batch_id, int valor_residual, int vida_util,int metod_val, decimal preparacion, decimal transporte, decimal montaje, decimal desmantelamiento, decimal honorarios)
            {
                //TODO: Modificar información exclusiva de IFRS
                var result = new PD.RespuestaAccion();
                try
                {
                    //using (var cServ = new ServiceProcess.ServiceAFN2())
                    //    cServ.Repo.PURCHASE_TRANSACTION_PARAMS();
                    result.set_ok();
                }
                catch (Exception ex)
                {
                    result.set(-1, ex.StackTrace);
                }

                return result;
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
        public static bool check_ingreso_ifrs(int codigo_lote)
        {
            using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Proceso.ingreso_ifrs_check(codigo_lote);
        }

        public static List<PD.DETAIL_DEPRECIATE> depreciar(int año, int mes)
        {
            using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Proceso.DepreciacionProcesoMensual(año, mes);
        }

        public static List<PD.DETAIL_PROCESS> buscar_Articulo(DateTime desde, DateTime hasta, int codigo, string descrip, string zona, int[] vigencias, string[] origenes, Vistas.Busquedas.moment_data cuando)
        {
            using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Proceso.buscar_Articulo(desde, hasta, codigo, descrip, zona, vigencias, origenes, cuando);
        }
        public static List<PD.SV_PROVEEDOR> buscar_proveedor(string codigo, string nombre)
        {
            using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Proceso.buscar_proveedor(codigo, nombre);
        }

        #endregion
    }
}
