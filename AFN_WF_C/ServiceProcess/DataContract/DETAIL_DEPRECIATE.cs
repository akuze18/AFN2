using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.DataContract
{
    public class DETAIL_DEPRECIATE
    {
        private DETAIL_PROCESS _detail;
        private double _porcentaje_cm;
        private double _valor_inicial_unitario;
        private double _depreciacion_inicial_unitaria;
        private double _deterioro_unitario;
        private double _valor_residual_unitario;
        private int _vida_util;
        private double _credito_unitario;
        //private double _credit_rate;
        private string _depreciation_rate;
        private ACode.Vperiodo _periodo_calc;

        public DETAIL_DEPRECIATE(DETAIL_PROCESS p, double porcentaje_cm, ACode.Vperiodo periodo_calc)
        {
            _detail = p;
            _valor_inicial_unitario = p.parametros.GetPrecioBase.value;
            _depreciacion_inicial_unitaria = p.parametros.GetDepreciacionAcum.value;
            _deterioro_unitario = p.parametros.GetDeterioro.value;
            _valor_residual_unitario = p.parametros.GetValorResidual.value;
            _credito_unitario = p.parametros.GetCredito.value;
            _vida_util = (int)p.parametros.GetVidaUtil.value;
            _periodo_calc = periodo_calc;
            if (!(sistema.ENVIORMENT.allow_cm_neg) && porcentaje_cm < 0)
                { _porcentaje_cm = 0; }
            else 
                { _porcentaje_cm = porcentaje_cm; }
            //_credit_rate = (double)sistema.ENVIORMENT.credit_rate;
            _depreciation_rate = sistema.ENVIORMENT.depreciation_rate;
        }

        public SYSTEM sistema { get { return _detail.sistema; } }
        public int cod_articulo { get { return _detail.cod_articulo; } }
        public int parte { get { return _detail.parte; } }
        public DateTime fecha_compra { get { return _detail.fecha_compra; } }
        public string desc_breve { get { return _detail.dscrp; } }
        public GENERIC_VALUE vigencia { get { return _detail.vigencia; } }
        public int cantidad { get { return _detail.cantidad; } }
        public GENERIC_VALUE zona { get { return _detail.zona; } }
        public GENERIC_VALUE clase { get { return _detail.clase; } }

        #region Calculando

        public double porcentaje_cm
        {
            get { return _porcentaje_cm; }
        }

        public double valor_anterior_base
        {
            get{ return _valor_inicial_unitario * cantidad; }
        }
        public double cm_activo
        { 
            get { 
                return Math.Round(_valor_inicial_unitario * porcentaje_cm / 100, 0) * cantidad; 
            } 
        }
        public double valor_anterior_cm
        {
            get { return valor_anterior_base + cm_activo; }
        }

        public double cred_adi_base
        { 
            get 
            {
                return _credito_unitario * cantidad;   
            } 
        }
        public double cm_cred
        {
            get
            {
                return Math.Round(_credito_unitario * porcentaje_cm / 100, 0) * cantidad; 
            }
        }
        public double credi_adi_cm
        {
            get { return cred_adi_base + cm_cred; }
        }

        public double val_AF_base
        { 
            get { return valor_anterior_base + cred_adi_base; } 
        }
        public double val_AF_cm
        {
            get { return valor_anterior_cm + credi_adi_cm; } 
        }

        public double DA_anterior_base
        {
            get { return _depreciacion_inicial_unitaria * cantidad; }
        }
        public double cm_depreciacion
        {
            get { return Math.Round(_depreciacion_inicial_unitaria * porcentaje_cm / 100, 0) * cantidad; }
        }
        public double DA_anterior_cm
        {
            get { return DA_anterior_base + cm_depreciacion; }
        }

        public double deter
        {
            get { return _deterioro_unitario * cantidad; }
        }
        public double val_res
        {
            get { return _valor_residual_unitario * cantidad; }
        }

        public double val_suj_dep_base
        {
            get { return val_AF_base + DA_anterior_base + deter + val_res; }
        }
        public double val_suj_dep
        {
            get{ return val_AF_cm + DA_anterior_cm + deter + val_res;}
        }

        public int vu_asig
        {
            get { return _vida_util; }
        }
        public int vu_ocup
        {
            get {
                if (vu_asig > 0)
                {
                    if (_depreciation_rate == "monthly")
                    {
                        if (!(fecha_compra.Month == _periodo_calc.last.Month && fecha_compra.Year == _periodo_calc.last.Year))
                        {
                            if (se_deprecia)
                                return 1;
                        }
                    }
                    else if (_depreciation_rate == "daily")
                    {
                        if (!(fecha_compra.Month == _periodo_calc.last.Month && fecha_compra.Year == _periodo_calc.last.Year))
                        {
                            if (se_deprecia)
                            {
                                if (fecha_compra > _periodo_calc.first)
                                {
                                    return (int)(_periodo_calc.last - fecha_compra).TotalDays;
                                }
                                else
                                {
                                    return (int)(_periodo_calc.last - _periodo_calc.first).TotalDays + 1;
                                }
                            }
                        }
                    }
                }
                return 0; 
            }
        }
        public int vu_resi {
            get { return vu_asig - vu_ocup; }
        }

        public double dep_eje
        {
            get
            {
                if (val_suj_dep <= 0 || vu_asig <= 0)
                {
                    return 0;
                }
                return Math.Round(val_suj_dep / cantidad / vu_asig * vu_ocup, 0) * -cantidad;
            }
        }
        public double dep_eje_base
        {
            get
            {
                if (val_suj_dep_base <= 0 || vu_asig <= 0)
                {
                    return 0;
                }
                return Math.Round(val_suj_dep_base / cantidad / vu_asig * vu_ocup, 0) * -cantidad;
            }
        }

        public double DA_AF
        {
            get { return DA_anterior_cm + dep_eje; }
        }
        public double DA_AF_base
        {
            get { return DA_anterior_base + dep_eje_base; }
        }

        public double val_libro
        {
            get { return val_AF_base + DA_AF; }
        }

        #endregion

        public GENERIC_VALUE subzona { get { return _detail.subzona; } }
        public GENERIC_VALUE subclase { get { return _detail.subclase; } }
        public GENERIC_VALUE gestion { get { return _detail.gestion; } }
        public GENERIC_VALUE categoria { get { return _detail.categoria; } }

        public DateTime fecha_ingreso { get { return _detail.fecha_ing; } }
        public DateTime fecha_inicio { get { return _detail.fecha_inicio; } }
        public DateTime fecha_fin { get { return _detail.fecha_fin; } }

        public GENERIC_VALUE origen { get { return _detail.origen; } }
        public GENERIC_VALUE tipo { get { return _detail.tipo; } }

        public bool se_deprecia { get { return _detail.se_deprecia; } }
        public int vida_util_inicial { get { return _detail.vida_util_inicial; } }

        public GENERIC_VALUE aprobacion { get { return _detail.aprobacion; } }

        public bool derecho_credito { get { return _detail.derecho_credito; } }
        
        public int PartId { get { return _detail.PartId; } }
        public int HeadId { get { return _detail.HeadId; } }
        public string RefSource { get { return _detail.RefSource; } }

        //public string usuario { get; set; }             //TRANSACTIONS_HEADERS
        //public decimal precio_inicial { get; set; }     //BATCHS_ARTICLES

    }
}
