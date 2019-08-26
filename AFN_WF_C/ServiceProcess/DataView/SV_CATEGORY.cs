using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.DataView
{
    class SV_CATEGORY
    {
        private int _id;
        private string _code;
        private string _descrip;

        public int id { get { return _id; } }
        public string code { get { return _code; } }
        public string descrip { get { return _descrip; } }

        #region Convertions
        public static implicit operator SV_CATEGORY(C.CATEGORY od)
        {
            return new SV_CATEGORY()
            {
                _id = od.id,
                _code = od.code,
                _descrip = od.descrip,
            };
        }
        public static implicit operator C.GENERIC_VALUE(SV_CATEGORY sv)
        {
            return new C.GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.code,
                description = sv.descrip,
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion
    }
}
