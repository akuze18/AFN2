using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.DataView
{
    class SV_CORRECTION_MONETARY_VALUE
    {
        private int _id;
        private string _period;
        private int _applyTo;
        private decimal _amount;

        public int id { get { return _id; } }
        public string period { get { return _period; } }
        public int applyTo { get { return _applyTo; } }
        public decimal amount { get { return _amount; } }

        public static SV_CORRECTION_MONETARY_VALUE empty
        {
            get {
                return new SV_CORRECTION_MONETARY_VALUE() { _amount = 0 };
            }
        }

        #region Convertions
        public static implicit operator SV_CORRECTION_MONETARY_VALUE(C.CORRECTION_MONETARY_VALUE od)
        {
            return new SV_CORRECTION_MONETARY_VALUE()
            {
                _id = od.id,
                _period = od.period,
                _applyTo = od.applyTo,
                _amount = od.amount,
            };
        }
        public static implicit operator C.PARAM_VALUE(SV_CORRECTION_MONETARY_VALUE sv)
        {
            return new C.PARAM_VALUE()
            {
                id = sv.id,
                code = sv.period,
                name = sv.period,
                value = sv.amount,
            };
        }
        #endregion
    }
}
