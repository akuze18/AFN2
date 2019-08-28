using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SC = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.PCClient.Procesos
{
    internal class Migracion
    {
        public static void CargaDepreciacion(int año, int mes)
        {
            var pServ = new ServiceProcess.ServiceAFN2();
            pServ.Migracion.CargaDepreciacion(año, mes);
        }

        public static SC.RespuestaAccion CargaTransacciones(int grupo)
        {
            var pServ = new ServiceProcess.ServiceAFN2();
            return pServ.Migracion.CargaDatosDesdeAFN(grupo);
        }

        public static void agregar_credito()
        {
            var pServ = new ServiceProcess.ServiceAFN2();
            pServ.Migracion.AgregarCredito();
        }

        public static void corregir_bajas()
        {
            var pServ = new ServiceProcess.ServiceAFN2();
            pServ.Migracion.CorregirBajas();
        }

        public static void CargarDatosOBC()
        {
            var pServ = new ServiceProcess.ServiceAFN2();
            pServ.Migracion.CargarDatosOBC();
        }
    }
}
