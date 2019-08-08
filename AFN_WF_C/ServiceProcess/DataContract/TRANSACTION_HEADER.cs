using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.DataContract
{
    public partial class TRANSACTION_HEADER
    {
        public override string ToString()
        {
            return this.trx_ini.ToShortDateString() + " (" + this.ref_source + ")";
        }
    }
}
