using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Procesos
{
    internal class Migracion
    {
        public static void CargaDepreciacion(int año, int mes)
        {
            using(var pServ = new ServiceProcess.ServiceAFN2())
                pServ.Migracion.CargaDepreciacion(año, mes);
        }

        public static PD.RespuestaAccion CargaTransacciones(int grupo)
        {
            using(var pServ = new ServiceProcess.ServiceAFN2())
                return pServ.Migracion.CargaDatosDesdeAFN(grupo);
        }

        public static void agregar_credito()
        {
            using(var pServ = new ServiceProcess.ServiceAFN2())
                pServ.Migracion.AgregarCredito();
        }

        public static void corregir_bajas()
        {
            using(var pServ = new ServiceProcess.ServiceAFN2())
                pServ.Migracion.CorregirBajas();
        }

        public static void CargarDatosOBC()
        {
            using(var pServ = new ServiceProcess.ServiceAFN2())
                pServ.Migracion.CargarDatosOBC();
        }

        public static void SincronizarAFN1()
        {
            using (var pServ = new ServiceProcess.ServiceAFN2())
                pServ.Migracion.SincronizarAFN();
        }

        public static void TestSave()
        {
        //    using (var pServ = new ServiceProcess.ServiceAFN2())
        //        pServ.Migracion.TestSave();
        }
    }
}
