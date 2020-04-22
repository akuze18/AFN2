using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using P = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class DETAIL_ACCOUNT
    {
        public ACode.Vperiodo periodo;
        public GENERIC_VALUE grupo;
        public GENERIC_VALUE tipo_cont;
        public string NUM_CUENTA;
        public string DSC_CUENTA;
        public decimal valor_antes;
        public decimal valor_actual;
        public string dim_lugar;
        public string dim_depto;

        //DETALLE
        public int codigo;
        public int parte;
        public string situacion;
        public GENERIC_VALUE Fclase;
        public GENERIC_VALUE Fzona;
        public GENERIC_VALUE Fsubzona;

        //CAMPOS PARA CALCULOS
        public DateTime fingreso;
        public int signo;

        //public decimal Icm_act;
        //public decimal Icm_DA;
        //public decimal Icred_adi;
        //public decimal Icred_bas;
        //public decimal IDA_ant;
        //public decimal Idep_bas;
        //public decimal Idep_eje;
        //public decimal Fcm_act;
        //public decimal Fcm_DA;
        //public decimal Fcred_adi;
        //public decimal Fcred_bas;
        //public decimal FDA_ant;
        //public decimal Fdep_bas;
        //public decimal Fdep_eje;
        //public decimal fval_af;
        //public decimal fda_af;
        //public decimal fval_libro;
        //public decimal ival_af;
        //public decimal ida_af;
        //FIN CAMPOS PARA CALCULOS
        //CAMPOS PARA DETALLE
        //public GENERIC_VALUE Iclase;
        //public GENERIC_VALUE Izona;
        //public GENERIC_VALUE Isubzona;
        //public GENERIC_VALUE IcodSubzona;

        //public GENERIC_VALUE IEstado;
        public GENERIC_VALUE FEstado;

        //public GENERIC_VALUE origen;
        //public GENERIC_VALUE tipo;
        //public DateTime fcompra;
        //public DateTime finiA;
        //public DateTime ffinA;
        //public DateTime finiB;
        //public DateTime ffinB;
        //public GENERIC_VALUE subclase;




        //    A.txt_subclase,A.categoria,A.dsc_breve
        //    A.FcodSubzona,A.ItxtSubzona,A.FtxtSubzona,A.IcodE,A.FcodE,A.Iestado,
        //    A.Festado,A.origen,A.tipo,A.fcompra,A.finiA,A.ffinA,A.finiB,A.ffinB,A.cod_subclase,
        //    A.txt_subclase,A.categoria,A.dsc_breve
        //B.TIPO_CONT,B.NUM_CUENTA,B.DSC_CUENTA,
        //    cast(0 as numeric(18,0))[valor_antes],
        //    cast(0 as numeric(18,0))[valor_actual],
        //    CAST('' AS VARCHAR(5))[dim_depto],CAST('' AS VARCHAR(5))[dim_lugar],
        //    A.codigo,A.parte,A.situacion,
        //    --A.Fclase,A.Fzona,A.Fsubzona,
        //    --CAMPOS PARA CALCULOS
        //    A.fingreso,C.signo,
        //    A.Icm_act,A.Icm_DA,A.Icred_adi,A.Icred_bas,A.IDA_ant,A.Idep_bas,A.Idep_eje,
        //    A.Fcm_act,A.Fcm_DA,A.Fcred_adi,A.Fcred_bas,A.FDA_ant,A.Fdep_bas,A.Fdep_eje,
        //    A.fval_af,A.fda_af,A.fval_libro,A.ival_af,A.ida_af,
        //    --FIN CAMPOS PARA CALCULOS
        //    --PARA DETALLE
        //    A.Iclase,A.Fclase,A.Izona,A.Fzona,A.Isubzona,A.Fsubzona,A.IcodSubzona,
        //    A.FcodSubzona,A.ItxtSubzona,A.FtxtSubzona,A.IcodE,A.FcodE,A.Iestado,
        //    A.Festado,A.origen,A.tipo,A.fcompra,A.finiA,A.ffinA,A.finiB,A.ffinB,A.cod_subclase,
        //    A.txt_subclase,A.categoria,A.dsc_breve
    }
}
