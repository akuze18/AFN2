using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    class SV_TRANSACTION_PARAMETER_DETAIL
    {
        private long _id;
        private int _system_id;
        private int _trx_head_id;
        private int _paratemer_id;
        private decimal _parameter_value;
        private GENERIC_VALUE _PARAMETER;

        public long id { get { return _id; } }
        public int system_id { get { return _system_id; } }
        public int trx_head_id { get { return _trx_head_id; } }
        public int paratemer_id { get { return _paratemer_id; } }
        public decimal parameter_value { get { return _parameter_value; } }
        private GENERIC_VALUE PARAMETER { get { return _PARAMETER; } }


        #region Convertions
        public static implicit operator SV_TRANSACTION_PARAMETER_DETAIL(DataContract.TRANSACTION_PARAMETER_DETAIL od)
        {
            return new SV_TRANSACTION_PARAMETER_DETAIL()
                {
                    _id = od.id,
                    _system_id = od.system_id,
                    _trx_head_id = od.trx_head_id,
                    _paratemer_id = od.paratemer_id,
                    _parameter_value = od.parameter_value,
                    _PARAMETER = (SV_PARAMETER)od.PARAMETER
                };
        }
        public static implicit operator PARAM_VALUE(SV_TRANSACTION_PARAMETER_DETAIL sv)
        {
            return new PARAM_VALUE()
            {
                id = sv.id,
                code = sv.PARAMETER.code,
                name = sv.PARAMETER.description,
                value = sv.parameter_value
            };
        }
        #endregion
    }
}
