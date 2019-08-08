using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.DataContract
{
    public class GROUP_MOVEMENT
    {
        public GENERIC_VALUE clase;
        public GENERIC_VALUE zona;
        public GENERIC_VALUE lugar;
        public double saldo_inicial_activo;
        public double adiciones_regular;
        public double adiciones_obc;
        public double valor_inicial_activo; //saldo_inicial_activo+adiciones_regular+adiciones_obc
        public double cm_activo;
        public double credito;
        public double castigo_activo;
        public double venta_activo;
        public double valor_final_activo;
        public double depreciacion_acumulada_inicial;
        public double cm_depreciacion;
        public double valor_residual;
        public double depreciacion_ejercicio;
        public double castigo_depreciacion;
        public double venta_depreciacion;
        public double depreciacion_acumulada_final;
        public double valor_libro;
        public string orden1;
        public string orden2;
        public string orden3;

        public object Item(int index)
        {
            switch (index)
            {
                case 1: return clase;
                case 2: return zona;
                case 3: return lugar;
                case 4: return valor_inicial_activo;
                case 5: return cm_activo;
                case 6: return credito;
                case 7: return valor_final_activo;
                case 8: return depreciacion_acumulada_inicial;
                case 9: return cm_depreciacion;
                case 10: return valor_residual;
                case 11: return depreciacion_ejercicio;
                case 12: return depreciacion_acumulada_final;
                case 13: return valor_libro;
                case 14: return orden1;
                case 15: return orden2;
                case 16: return orden3;
                case 17: return saldo_inicial_activo;
                case 18: return adiciones_regular;
                case 19: return adiciones_obc;
                case 20: return castigo_activo;
                case 21: return venta_activo;
                case 22: return castigo_depreciacion;
                case 23: return venta_depreciacion;
                default: return null;
            }
        }

    }
}
