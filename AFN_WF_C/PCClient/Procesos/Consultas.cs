using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SC = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.PCClient.Procesos
{
    internal class consultas
    {
        public static List<SC.DETAIL_DEPRECIATE> depreciar(int año, int mes)
        {
            var pServ = new ServiceProcess.ServiceAFN();
            return pServ.DepreciacionProcesoMensual(año,mes);
        }

        public static List<SC.DETAIL_PROCESS> buscar_Articulo(DateTime desde, DateTime hasta, int codigo, string descrip, string zona, int[] vigencias, string[] origenes)
        {
            var cServ = new ServiceProcess.ServiceAFN();
            return cServ.buscar_Articulo(desde, hasta, codigo, descrip, zona, vigencias, origenes);
        }

        internal class sistema {
            public static SC.SYSTEM ByCodes(string ambiente, string moneda) {
                var cServ = new ServiceProcess.ServiceAFN();
                return cServ.Sistemas.ByCodes(ambiente,moneda);
            }
            public static List<SC.SYSTEM> All()
            {
                var cServ = new ServiceProcess.ServiceAFN();
                return cServ.Sistemas.All();
            }
        }
        internal class zonas { 
            public static List<SC.GENERIC_VALUE> All() {
                var cServ = new ServiceProcess.ServiceAFN();
                return cServ.Zonas.All();
            }
            public static List<SC.GENERIC_VALUE> SearchList()
            {
                var process = All();
                process.Insert(0, new SC.GENERIC_VALUE(0, "TODAS", "ZONE"));
                return process;
            }
        }
        internal class estados_aprobacion {
            public static string[] All
            {
                get
                {
                    var cServ = new ServiceProcess.ServiceAFN();
                    var estados = cServ.EstadoAprovacion.All;
                    return estados.Select(e => e.code).ToArray();
                }
            }
            public static string[] OnlyActive
            {
                get
                {
                    var cServ = new ServiceProcess.ServiceAFN();
                    var estados = cServ.EstadoAprovacion.OnlyActive;
                    return estados.Select(e => e.code).ToArray();
                }
            }
            public static string[] OnlyDigited
            {
                get
                {
                    var cServ = new ServiceProcess.ServiceAFN();
                    var estados = cServ.EstadoAprovacion.OnlyDigited;
                    return estados.Select(e => e.code).ToArray();
                }
            }
            public static string[] NoDeleted
            {
                get
                {
                    var cServ = new ServiceProcess.ServiceAFN();
                    var estados = cServ.EstadoAprovacion.NoDeleted;
                    return estados.Select(e => e.code).ToArray();
                }
            }
            public static string[] Default {
                get
                {
                    var cServ = new ServiceProcess.ServiceAFN();
                    var estados = cServ.EstadoAprovacion.Default;
                    return estados.Select(e=> e.code).ToArray();
                }
            }
        }
        internal class clases {
            public static List<SC.GENERIC_VALUE> SearchList()
            {
                var cServ = new ServiceProcess.ServiceAFN();
                return cServ.Clases.SearchList();
            }
        }
        internal class vigencias
        {
            public static List<SC.GENERIC_VALUE> SearchDownsList()
            {
                var cServ = new ServiceProcess.ServiceAFN();
                return cServ.Vigencias.SearchDownsList();
            }

        }
        internal class tipos
        {
            public static List<SC.GENERIC_VALUE> All()
            {
                var cServ = new ServiceProcess.ServiceAFN();
                return cServ.Tipos.All();
            }
        }
        internal class monedas
        {
            public static SC.GENERIC_VALUE[] All()
            {
                var cServ = new ServiceProcess.ServiceAFN();
                return cServ.Monedas.All().ToArray();
            }
        }

        internal class partes
        {
            public static List<SC.PART> ByLote(int lote)
            {
                var cServ = new ServiceProcess.ServiceAFN();
                return cServ.Partes.ByLote(lote); ;
            }
        }
        internal class cabeceras
        {
            public static List<SC.TRANSACTION_HEADER> ByParte(int parte)
            {
                var cServ = new ServiceProcess.ServiceAFN();
                return cServ.Cabeceras.ByParte(parte); 
            }
        }
        internal class detalle_parametros
        {
            public static List<SC.PARAM_VALUE> ByHead_Sys(int HeadId, int SysId)
            {
                var cServ = new ServiceProcess.ServiceAFN();
                return cServ.DetallesParametros.ByHead_Sys(HeadId, SysId); 
            }
        }
        
        #region Opciones

        internal class arr
        {
            public static SC.GENERIC_VALUE[] meses { get { return consultas.meses.ToArray(); } }
            public static SC.GENERIC_VALUE[] years { get { return consultas.years.ToArray(); } }
            public static SC.GENERIC_VALUE[] acumulados { get { return consultas.acumulados.ToArray(); } }
            public static SC.GENERIC_VALUE[] acumulados_wMes { get { return consultas.acumulados_wMes.ToArray(); } }

            public static SC.GENERIC_VALUE[] opciones_menu_obc { get { return consultas.opciones_menu_obc.ToArray(); } }
        }

        public static List<SC.GENERIC_VALUE> meses {
            get {
                var x = new List<SC.GENERIC_VALUE> { };
                string tipo = "Mes";
                for (int i = 1; i <= 12; i++) {
                    var mes = new DateTime(1,i,1).ToString("MMMM");
                    mes = mes.Substring(0,1).ToUpper() + mes.Substring(1);
                    x.Add(new SC.GENERIC_VALUE(i, mes, tipo));
                }
                return x;
            }
        }
        public static List<SC.GENERIC_VALUE> years
        {
            get
            {
                var x = new List<SC.GENERIC_VALUE> { };
                string tipo = "Año";
                for (int i = DateTime.Now.Year; i >= 2012; i--)
                {
                    x.Add(new SC.GENERIC_VALUE(i, i.ToString(), tipo));
                }
                return x;
            }
        }
        public static List<SC.GENERIC_VALUE> acumulados
        {
            get
            {
                var x = new List<SC.GENERIC_VALUE>();
                var tipo = "Acumulado";
                x.Add(new SC.GENERIC_VALUE(1, "Regular", tipo));
                x.Add(new SC.GENERIC_VALUE(2, "Japones", tipo));
                return x;
            }
        }
        public static List<SC.GENERIC_VALUE> acumulados_wMes
        {
            get
            {
                var x = new List<SC.GENERIC_VALUE>();
                var tipo = "Acumulado";
                x.Add(new SC.GENERIC_VALUE(0, "Solo Mes", tipo));
                x.Add(new SC.GENERIC_VALUE(1, "Regular", tipo));
                x.Add(new SC.GENERIC_VALUE(2, "Japones", tipo));
                return x;
            }
        }
        public static List<SC.GENERIC_VALUE> opciones_menu_obc
        {
            get
            {
                var result = new List<SC.GENERIC_VALUE> { };
                string tipo = "menu_obc";
                result.Add(new SC.GENERIC_VALUE(1, "Saldo", tipo));
                result.Add(new SC.GENERIC_VALUE(2, "Entradas", tipo));
                result.Add(new SC.GENERIC_VALUE(3, "Salidas", tipo));
                return result;
            }
        }
        #endregion
    }
}
