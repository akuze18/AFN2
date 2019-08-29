using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class PARAM_VALUE
    {
        public long id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public decimal value { get; set; }

        public static implicit operator PARAM_VALUE(DataContract.TRANSACTION_PARAMETER_DETAIL tpd)
        {
            var me = new PARAM_VALUE();
            if (tpd != null)
            {
                me.id = tpd.id;
                me.code = tpd.PARAMETER.code;
                me.name = tpd.PARAMETER.name;
                me.value = tpd.parameter_value;
            }
            return me;
        }
    }
}
