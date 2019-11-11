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

        internal class partes
        {
            public static List<PD.SV_PART> ByLote(int lote)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.Partes.ByLote(lote); ;
            }
        }
        internal class cabeceras
        {
            public static List<PD.SV_TRANSACTION_HEADER> ByParte(int parte)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.cabeceras.ByParte(parte); 
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
        internal class detalle_parametros
        {
            public static List<PD.PARAM_VALUE> ByHead_Sys(int HeadId, int SysId)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    return cServ.Repo.DetallesParametros.ByHead_Sys(HeadId, SysId); 
            }
            public static PD.RespuestaAccion create()
            {
                var result = new PD.RespuestaAccion();
                try
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                        cServ.Repo.TRANSACTION_PARAMETER_DETAIL_NEW();
                    result.set_ok();
                }
                catch( Exception ex)
                {
                    result.set(-1, ex.StackTrace);
                }

                return result;
                
            }
            public static PD.RespuestaAccion MODIFICA_IFRS(int batch_id, int valor_residual, int vida_util,int metod_val, decimal preparacion, decimal transporte, decimal montaje, decimal desmantelamiento, decimal honorarios)
            {
                var result = new PD.RespuestaAccion();
                try
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                        cServ.Repo.TRANSACTION_PARAMETER_DETAIL_NEW();
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

                //return new List<object>();
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
