using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.DataContract
{
    public class DETAIL_MOVEMENT : IElemento
    {

        public SYSTEM sistema { get; set; }
        public int cod_articulo { get; set; }
        public int parte { get; set; }
        public DateTime fecha_compra { get; set; }
        public string desc_breve { get; set; }
        //public GENERIC_VALUE vigencia { get; set; }
        public int cantidad { get; set; }
        public GENERIC_VALUE zona { get; set; }
        public GENERIC_VALUE clase { get; set; }

        public GENERIC_VALUE subzona { get; set; }
        public GENERIC_VALUE subclase { get; set; }
        //public GENERIC_VALUE gestion { get; set; }
        //public GENERIC_VALUE categoria { get; set; }

        //public DateTime fecha_ingreso { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }

        public GENERIC_VALUE origen { get; set; }
        //public GENERIC_VALUE tipo { get; set; }
        public GENERIC_VALUE vigencia { get; set; }

        //public bool se_deprecia { get; set; }
        //public int vida_util_inicial { get; set; }

        //public GENERIC_VALUE aprobacion { get; set; }

        public decimal valor_activo_inicial { get; set; }
        public decimal credito_monto { get; set; }
        public decimal valor_activo_final { get; set; }
        public decimal depreciacion_acum_inicial { get; set; }
        public decimal deterioro { get; set; }
        public decimal valor_residual { get; set; }
        public decimal valor_sujeto_dep { get; set; }
        public int vida_util_asignada { get; set; }
        public int vida_util_ocupada { get; set; }
        public int vida_util_residual { get; set; }
        public decimal depreciacion_ejercicio { get; set; }
        public decimal depreciacion_acum_final { get; set; }
        public decimal valor_libro { get; set; }
        //public string usuario { get; set; }             
        //public decimal precio_inicial { get; set; }     
        public decimal porcentaje_cm { get; set; }
        public decimal valor_activo_cm { get; set; }
        public decimal valor_activo_update { get; set; }
        public decimal depreciacion_acum_cm { get; set; }
        public decimal depreciacion_acum_update { get; set; }
        public decimal preparacion { get; set; }
        public decimal desmantelamiento { get; set; }
        public decimal transporte { get; set; }
        public decimal montaje { get; set; }
        public decimal honorario { get; set; }
        public decimal revalorizacion { get; set; }

        public string situacion { get; set; }

        public object Item(int index)
        {
            switch (index)
            {
                case 0: return sistema;
                case 1: return cod_articulo;
                case 2: return fecha_compra;
                case 3: return desc_breve;
                case 4: return cantidad;
                case 5: return zona;
                case 6: return clase;
                case 7: return valor_activo_inicial;
                case 8: return credito_monto;
                case 9: return valor_activo_final;
                case 10: return depreciacion_acum_inicial;
                case 11: return deterioro;
                case 12: return valor_residual;
                case 13: return valor_sujeto_dep;
                case 14: return vida_util_asignada;
                case 15: return vida_util_ocupada;
                case 16: return vida_util_residual;
                case 17: return depreciacion_ejercicio;
                case 18: return depreciacion_acum_final;
                case 19: return valor_libro;
                case 20: return porcentaje_cm;
                case 21: return valor_activo_cm;
                case 22: return valor_activo_update;
                case 23: return depreciacion_acum_cm;
                case 24: return depreciacion_acum_update;
                case 25: return preparacion;
                case 26: return desmantelamiento;
                case 27: return transporte;
                case 28: return montaje;
                case 29: return honorario;
                case 30: return revalorizacion;
                case 31: return fecha_inicio;
                case 32: return fecha_fin;
                case 33: return vigencia;
                case 34: return subzona;
                case 35: return subclase;
                case 36: return parte;
                case 37: return situacion;
                default:
                    return null;
            }
        }
    }
}
