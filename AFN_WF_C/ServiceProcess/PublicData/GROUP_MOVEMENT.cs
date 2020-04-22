using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class GROUP_MOVEMENT: IElemento
    {
        #region Fill Data
        private void Filling(GENERIC_VALUE group1, GENERIC_VALUE group2, GENERIC_VALUE group3, IEnumerable<DETAIL_MOVEMENT> GroupDetail)
        {
            this.clase = (group1.type == "KIND" ? group1 : ((group2.type == "KIND")?group2:group3));
            this.zona = (group1.type == "ZONE" ? group1 : ((group2.type == "ZONE") ? group2 : group3));
            this.lugar = (group1.type == "SUBZONE" ? group1 : ((group2.type == "SUBZONE") ? group2 : group3));
            this.valor_inicial_activo = GroupDetail.Sum(a => a.valor_activo_inicial);
            this.cm_activo = GroupDetail.Sum(a => a.valor_activo_cm);
            this.credito = GroupDetail.Sum(a => a.credito_monto);
            this.preparacion = GroupDetail.Sum(a => a.preparacion);
            this.desmantelamiento = GroupDetail.Sum(a => a.desmantelamiento);
            this.transporte = GroupDetail.Sum(a => a.transporte);
            this.montaje = GroupDetail.Sum(a => a.montaje);
            this.honorario = GroupDetail.Sum(a => a.honorario);
            this.revalorizacion = GroupDetail.Sum(a => a.revalorizacion);
            this.valor_final_activo = GroupDetail.Sum(a => a.valor_activo_final);
            this.depreciacion_acumulada_inicial = GroupDetail.Sum(a => a.depreciacion_acum_inicial);
            this.cm_depreciacion = GroupDetail.Sum(a => a.depreciacion_acum_cm);
            this.valor_residual = GroupDetail.Sum(a => a.valor_residual);
            this.depreciacion_ejercicio = GroupDetail.Sum(a => a.depreciacion_ejercicio);
            this.depreciacion_acumulada_final = GroupDetail.Sum(a => a.depreciacion_acum_final);
            this.valor_libro = GroupDetail.Sum(a => a.valor_libro);
            this.orden1 = group1.code;
            this.orden2 = group2.code;
            this.orden3 = group3.code;
        }
        public void GroupKindDetailed(GENERIC_VALUE Kind, IEnumerable<DETAIL_MOVEMENT> GroupDetail, Repositories.SUBZONES subzoneAux)
        {
            var Zone = (from a in GroupDetail select a.zona).First();
            SV_SUBZONE SubZone = (from a in GroupDetail where (a.subzona.principal) select a.subzona).FirstOrDefault();
            if (SubZone == null)
            {
                SV_SUBZONE specific = (from a in GroupDetail select a.subzona).First();
                SubZone = subzoneAux.PrincipalByCode(specific.codPlace);
            }
            this.Filling(Kind, Zone, SubZone, GroupDetail);
        }
        public void GroupKindTotalized(GENERIC_VALUE Kind, IEnumerable<DETAIL_MOVEMENT> GroupDetail)
        {
            var Zone =  new GENERIC_VALUE(99999, "TOTAL", "ZONE");
            var SubZone = new GENERIC_VALUE(99999, "", "SUBZONE");
            this.Filling(Kind, Zone, SubZone, GroupDetail);
        }
        public void GroupKindGrandTotalized(IEnumerable<DETAIL_MOVEMENT> GroupDetail)
        {
            var Zone = new GENERIC_VALUE(99999, "TOTAL", "ZONE");
            var SubZone = new GENERIC_VALUE(99999, "", "SUBZONE");
            var Kind = new GENERIC_VALUE(99999, "TOTAL", "KIND");
            this.Filling(Kind, Zone, SubZone, GroupDetail);
        }
        public void GroupZoneDetailed(GENERIC_VALUE Zone, IEnumerable<DETAIL_MOVEMENT> GroupDetail, Repositories.SUBZONES subzoneAux)
        {
            var Kind = (from a in GroupDetail select a.clase).First();
            SV_SUBZONE SubZone = (from a in GroupDetail where (a.subzona.principal) select a.subzona).FirstOrDefault();
            if (SubZone == null)
            {
                SV_SUBZONE specific = (from a in GroupDetail select a.subzona).First();
                SubZone = subzoneAux.PrincipalByCode(specific.codPlace);
            }
            this.Filling(Zone, SubZone, Kind, GroupDetail);
        }
        public void GroupZoneTotalized(GENERIC_VALUE Zone, IEnumerable<DETAIL_MOVEMENT> GroupDetail)
        {
            var Kind = new GENERIC_VALUE(99999, "TOTAL", "KIND");
            GENERIC_VALUE SubZone = new GENERIC_VALUE(99999, "TOTAL_n", "SUBZONE");
            this.Filling(Zone, SubZone, Kind, GroupDetail);
        }
        public void GroupZoneGrandTotalized(IEnumerable<DETAIL_MOVEMENT> GroupDetail)
        {
            var Zone = new GENERIC_VALUE(99999, "TOTAL", "ZONE");
            var SubZone = new GENERIC_VALUE(99999, "", "SUBZONE");
            var Kind = new GENERIC_VALUE(99999, "TOTAL", "KIND");
            this.Filling(Zone, SubZone, Kind, GroupDetail);
        }

        #endregion

        public GENERIC_VALUE clase;
        public GENERIC_VALUE zona;
        public GENERIC_VALUE lugar;
        public decimal saldo_inicial_activo;
        public decimal adiciones_regular;
        public decimal adiciones_obc;
        public decimal valor_inicial_activo; //saldo_inicial_activo+adiciones_regular+adiciones_obc
        public decimal cm_activo;
        public decimal credito;
        //IFRS
        public decimal preparacion;
        public decimal desmantelamiento;
        public decimal transporte;
        public decimal montaje;
        public decimal honorario;
        public decimal revalorizacion;

        public decimal incremento_obc;
        public decimal decremento_obc;
        public decimal castigo_activo;
        public decimal venta_activo;
        public decimal valor_final_activo;
        public decimal depreciacion_acumulada_inicial;
        public decimal cm_depreciacion;
        public decimal valor_residual;
        public decimal depreciacion_ejercicio;
        public decimal castigo_depreciacion;
        public decimal venta_depreciacion;
        public decimal depreciacion_acumulada_final;
        public decimal valor_libro;
        public string orden1;
        public string orden2;
        public string orden3;

        public object Item(int index)
        {
            int signo = (index <0 ? -1 : 1);
            switch (Math.Abs(index))
            {
                case 1: return clase;
                case 2: return zona;
                case 3: return lugar;
                case 4: return valor_inicial_activo * signo;
                case 5: return cm_activo * signo;
                case 6: return credito * signo;
                case 7: return valor_final_activo * signo;
                case 8: return depreciacion_acumulada_inicial * signo;
                case 9: return cm_depreciacion * signo;
                case 10: return valor_residual * signo;
                case 11: return depreciacion_ejercicio * signo;
                case 12: return depreciacion_acumulada_final * signo;
                case 13: return valor_libro * signo;
                case 14: return orden1;
                case 15: return orden2;
                case 16: return orden3;
                case 17: return saldo_inicial_activo * signo;
                case 18: return adiciones_regular * signo;
                case 19: return adiciones_obc * signo;
                case 20: return castigo_activo * signo;
                case 21: return venta_activo * signo;
                case 22: return castigo_depreciacion * signo;
                case 23: return venta_depreciacion * signo;
                case 24: return incremento_obc * signo;
                case 25: return decremento_obc * signo;
                case 26: return preparacion * signo;
                case 27: return desmantelamiento * signo;
                case 28: return transporte * signo;
                case 29: return montaje * signo;
                case 30: return honorario * signo;
                case 31: return revalorizacion * signo;
                default: return null;
            }
        }

    }
}
