using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public partial class Main
    {
        public TRANSACTION_PARAMETER_DETAIL TRANSACTION_PARAMETER_DETAIL_NEW()
        {
            var param_det = new TRANSACTION_PARAMETER_DETAIL();
            
            return param_det;
        }
    }
}
