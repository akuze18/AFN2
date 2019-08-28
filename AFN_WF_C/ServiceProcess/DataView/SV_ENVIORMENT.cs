using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.DataView
{
    public class SV_ENVIORMENT
    {
        //
        private int _id;
        private string _code;
        private string _name;
        private string _depreciation_rate;
        private decimal _credit_rate;
        private bool _allow_cm_neg;

        public int id { get { return _id; } }
        public string code { get { return _code; } }
        public string name { get { return _name; } }
        public string depreciation_rate { get { return _depreciation_rate; } }
        public decimal credit_rate { get { return _credit_rate; } }
        public bool allow_cm_neg { get { return _allow_cm_neg; } }

        #region Convertions
        public static implicit operator SV_ENVIORMENT(C.ENVIORMENT od)
        {
            return new SV_ENVIORMENT()
            {
                _id = od.id,
                _code = od.code,
                _name = od.name,
                _depreciation_rate = od.depreciation_rate,
                _credit_rate = od.credit_rate,
                _allow_cm_neg = od.allow_cm_neg,
            };
        }
        public static implicit operator C.GENERIC_VALUE(SV_ENVIORMENT sv)
        {
            return new C.GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.code,
                description = sv.name,
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion

    }
}
