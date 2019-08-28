using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SC = AFN_WF_C.ServiceProcess.DataContract;
using SV = AFN_WF_C.ServiceProcess.DataView;

namespace AFN_WF_C.PCClient.Procesos
{
    internal class consultas
    {
        public static List<SC.DETAIL_DEPRECIATE> depreciar(int año, int mes)
        {
            using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Proceso.DepreciacionProcesoMensual(año, mes);
        }

        public static List<SC.DETAIL_PROCESS> buscar_Articulo(DateTime desde, DateTime hasta, int codigo, string descrip, string zona, int[] vigencias, string[] origenes)
        {
            using(var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Proceso.buscar_Articulo(desde, hasta, codigo, descrip, zona, vigencias, origenes);
        }

        internal class sistema {
            public static SV.SV_SYSTEM ByCodes(string ambiente, string moneda) {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Repo.sistemas.ByCodes(ambiente,moneda);
            }
            public static List<SV.SV_SYSTEM> All()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Repo.sistemas.All();
            }
        }
        internal class zonas { 
            public static List<SC.GENERIC_VALUE> All() {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Repo.zonas.All();
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
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                    {
                        var estados = cServ.Repo.aprobaciones.All;
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
                        var estados = cServ.Repo.aprobaciones.OnlyActive;
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
                        var estados = cServ.Repo.aprobaciones.OnlyDigited;
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
                        var estados = cServ.Repo.aprobaciones.NoDeleted;
                        return estados.Select(e => e.code).ToArray();
                    }
                }
            }
            public static string[] Default {
                get
                {
                    using (var cServ = new ServiceProcess.ServiceAFN2())
                    {
                        var estados = cServ.Repo.aprobaciones.Default;
                        return estados.Select(e => e.code).ToArray();
                    }
                }
            }
        }
        internal class clases {
            public static List<SC.GENERIC_VALUE> SearchList()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Repo.Clases.SearchList();
            }
        }
        internal class vigencias
        {
            public static List<SC.GENERIC_VALUE> SearchDownsList()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Repo.Vigencias.SearchDownsList();
            }

        }
        internal class tipos
        {
            public static List<SC.GENERIC_VALUE> All()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Repo.tipos.All();
            }
        }
        internal class monedas
        {
            public static SC.GENERIC_VALUE[] All()
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Repo.Monedas.All().ToArray();
            }
        }

        internal class partes
        {
            public static List<SV.SV_PART> ByLote(int lote)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Repo.Partes.ByLote(lote); ;
            }
        }
        internal class cabeceras
        {
            public static List<SV.SV_TRANSACTION_HEADER> ByParte(int parte)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Repo.cabeceras.ByParte(parte); 
            }
        }
        internal class detalle_parametros
        {
            public static List<SC.PARAM_VALUE> ByHead_Sys(int HeadId, int SysId)
            {
                using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Repo.detalle_parametros.ByHead_Sys(HeadId, SysId); 
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
