using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using V = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class DETAIL_DEPRECIATE
    {
        private DETAIL_PROCESS _detail;
        private decimal _porcentaje_cm;
        private decimal _valor_inicial_unitario;
        private decimal _depreciacion_inicial_unitaria;
        private decimal _deterioro_unitario;
        private decimal _valor_residual_unitario;
        private int _vida_util;
        private decimal _credito_unitario;

        private decimal _preparacion_unit;
        private decimal _transporte_unit;
        private decimal _montaje_unit;
        private decimal _desmantelamiento_unit;
        private decimal _honorario_unit;
        private decimal _revalorizacion_unit;

        //private double _credit_rate;
        private string _depreciation_rate;
        private DateTime _fecha_calc;

        public DETAIL_DEPRECIATE(DETAIL_PROCESS p, decimal porcentaje_cm, DateTime fecha_calc)
        {
            _detail = p;
            _valor_inicial_unitario = p.parametros.GetPrecioBase.value;
            _depreciacion_inicial_unitaria = p.parametros.GetDepreciacionAcum.value;
            _deterioro_unitario = p.parametros.GetDeterioro.value;
            _valor_residual_unitario = p.parametros.GetValorResidual.value;
            _credito_unitario = p.parametros.GetCredito.value;
            _vida_util = (int)p.parametros.GetVidaUtil.value;
            _preparacion_unit = p.parametros.GetPreparacion.value;
            _transporte_unit = p.parametros.GetTransporte.value;
            _montaje_unit = p.parametros.GetMontaje.value;
            _desmantelamiento_unit = p.parametros.GetDesmantelamiento.value;
            _honorario_unit = p.parametros.GetHonorario.value;
            _revalorizacion_unit = p.parametros.GetRevalorizacion.value;
            
            _fecha_calc = fecha_calc;
            if (!(sistema.ENVIORMENT.allow_cm_neg) && porcentaje_cm < 0)
            { _porcentaje_cm = 0; }
            else
            { _porcentaje_cm = porcentaje_cm; }
            //_credit_rate = (double)sistema.ENVIORMENT.credit_rate;
            _depreciation_rate = sistema.ENVIORMENT.depreciation_rate;
        }

        public V.SV_SYSTEM sistema { get { return _detail.sistema; } }
        public int cod_articulo { get { return _detail.cod_articulo; } }
        public int parte { get { return _detail.parte; } }
        public DateTime fecha_compra { get { return _detail.fecha_compra; } }
        public string desc_breve { get { return _detail.dscrp; } }
        public SV_VALIDATY vigencia { get { return _detail.vigencia; } }
        public int cantidad { get { return _detail.cantidad; } }
        public SV_ZONE zona { get { return _detail.zona; } }
        public SV_KIND clase { get { return _detail.clase; } }

        #region Calculando

        public decimal porcentaje_cm
        {
            get { return _porcentaje_cm / 100; }
        }

        public decimal valor_anterior_base
        {
            get{ return _valor_inicial_unitario * cantidad; }
        }
        public decimal cm_activo
        { 
            get { 
                return Math.Round(_valor_inicial_unitario * porcentaje_cm, 0) * cantidad; 
            } 
        }
        public decimal valor_anterior_cm
        {
            get { return valor_anterior_base + cm_activo; }
        }

        public decimal cred_adi_base
        { 
            get { return _credito_unitario * cantidad; } 
        }
        public decimal cm_cred
        {
            get
            {
                return Math.Round(_credito_unitario * porcentaje_cm, 0) * cantidad; 
            }
        }
        public decimal credi_adi_cm
        {
            get { return cred_adi_base + cm_cred; }
        }

        public decimal val_AF_base
        { 
            get { return valor_anterior_base + cred_adi_base + preparacion +desmantelamiento + transporte + montaje + honorario; } 
        }
        public decimal val_AF_cm
        {
            get { return valor_anterior_cm + credi_adi_cm + preparacion + desmantelamiento + transporte + montaje + honorario; } 
        }

        public decimal DA_anterior_base
        {
            get { return _depreciacion_inicial_unitaria * cantidad; }
        }
        public decimal cm_depreciacion
        {
            get { return Math.Round(_depreciacion_inicial_unitaria * porcentaje_cm, 0) * cantidad; }
        }
        public decimal DA_anterior_cm
        {
            get { return DA_anterior_base + cm_depreciacion; }
        }

        public decimal deter
        {
            get { return _deterioro_unitario * cantidad; }
        }
        public decimal val_res
        {
            get { return _valor_residual_unitario * cantidad; }
        }

        public decimal val_suj_dep_base
        {
            get { return val_AF_base + DA_anterior_base + deter + val_res; }
        }
        public decimal val_suj_dep
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
                int calculado = 0;
                if (vu_asig > 0)
                {
                    bool SameMonth = fecha_ingreso.Month == _fecha_calc.Month;
                    bool SameYear = fecha_ingreso.Year == _fecha_calc.Year;
                    bool SamePeriod = SameMonth && SameYear;

                    if (_depreciation_rate == "monthly")
                    {
                        if (!(SamePeriod) && (se_deprecia)) { calculado = 1; }
                    }
                    else if (_depreciation_rate == "daily")
                    {
                        if (!(SamePeriod) && (se_deprecia))
                            calculado = (int)(_fecha_calc - _detail.fecha_proceso).TotalDays;
                        else if ((SamePeriod) && se_deprecia)
                            calculado = (int)(_fecha_calc - fecha_compra).TotalDays;
                    }
                    if (vu_asig < calculado)
                        calculado = vu_asig;
                }
                return calculado; 
            }
        }
        public int vu_resi {
            get { return vu_asig - vu_ocup; }
        }

        public decimal dep_eje
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
        public decimal dep_eje_base
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

        public decimal DA_AF
        {
            get { return DA_anterior_cm + dep_eje; }
        }
        public decimal DA_AF_base
        {
            get { return DA_anterior_base + dep_eje_base; }
        }

        public decimal val_libro
        {
            get { return val_AF_base + DA_AF + revalorizacion; }
        }

        //IFRS ONLY
        public decimal preparacion { get { return _preparacion_unit * cantidad; } }
        public decimal transporte { get { return _transporte_unit * cantidad; } }
        public decimal montaje { get { return _montaje_unit * cantidad; } }
        public decimal desmantelamiento { get { return _desmantelamiento_unit * cantidad; } }
        public decimal honorario { get { return _honorario_unit * cantidad; } }
        public decimal revalorizacion { get { return _revalorizacion_unit * cantidad; } }
        #endregion

        public SV_SUBZONE subzona { get { return _detail.subzona; } }
        public SV_SUBKIND subclase { get { return _detail.subclase; } }
        public SV_MANAGEMENT gestion { get { return _detail.gestion; } }
        public SV_CATEGORY categoria { get { return _detail.categoria; } }

        public DateTime fecha_ingreso { get { return _detail.fecha_ing; } }
        public DateTime fecha_inicio { get { return _detail.fecha_inicio; } }
        public DateTime fecha_fin { get { return _detail.fecha_fin; } }

        public GENERIC_VALUE origen { get { return _detail.origen; } }
        public GENERIC_VALUE tipo { get { return _detail.tipo; } }

        public bool se_deprecia { get { return _detail.se_deprecia; } }
        public int vida_util_inicial { get { return _detail.vida_util_inicial; } }

        public SV_APROVAL_STATE aprobacion { get { return _detail.aprobacion; } }

        public bool derecho_credito { get { return _detail.derecho_credito; } }
        
        public int PartId { get { return _detail.PartId; } }
        public int HeadId { get { return _detail.HeadId; } }
        public string RefSource { get { return _detail.RefSource; } }

        //public string usuario { get; set; }             //TRANSACTIONS_HEADERS
        //public decimal precio_inicial { get; set; }     //BATCHS_ARTICLES

    }
}
