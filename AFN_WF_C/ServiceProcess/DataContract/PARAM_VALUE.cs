using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.DataContract
{
    public class PARAM_VALUE
    {
        public long id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public double value { get; set; }

        public static implicit operator PARAM_VALUE(TRANSACTION_PARAMETER_DETAIL tpd)
        {
            var me = new PARAM_VALUE();
            if (tpd != null)
            {
                me.id = tpd.id;
                me.code = tpd.PARAMETER.code;
                me.name = tpd.PARAMETER.name;
                me.value = (double)tpd.parameter_value;
            }
            return me;
        }
    }
}
