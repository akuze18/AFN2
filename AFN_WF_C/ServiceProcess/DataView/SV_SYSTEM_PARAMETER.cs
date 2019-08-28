using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.DataView
{
    public class SV_SYSTEM_PARAMETER
    {
        private int _id;
        private int _system_id;
        private int _parameter_id;
        private bool _isRequired;

        public int id { get { return _id; } }
        public int system_id { get { return _system_id; } }
        public int parameter_id { get { return _parameter_id; } }
        public bool isRequired { get { return _isRequired; } }

        #region Convertions
        public static implicit operator SV_SYSTEM_PARAMETER(C.SYSTEM_PARAMETER od)
        {
            return new SV_SYSTEM_PARAMETER()
            {
                _id = od.id,
                _system_id = od.system_id,
                _parameter_id = od.parameter_id,
                _isRequired  = od.isRequired,
            };
        }
        //public static implicit operator C.PARAM_VALUE(SV_SYSTEM_PARAMETER sv)
        //{
        //    return new C.PARAM_VALUE()
        //    {
        //        id = sv.id,
        //        code = sv.PARAMETER.code,
        //        name = sv.PARAMETER.description,
        //        value = sv.parameter_value
        //    };
        //}
        #endregion
    }
}
