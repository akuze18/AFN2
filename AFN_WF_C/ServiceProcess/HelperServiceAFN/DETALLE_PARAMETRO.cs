using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.HelperServiceAFN
{
    public class DETALLE_PARAMETRO
    {
        public List<PARAM_VALUE> ByHead_Sys(int HeadId, int SysId)
        {
            using (AFN2Entities context = new AFN2Entities())
            {
                var pd = new Repositories.TRANSACTIONS_PARAM_DET(context.TRANSACTIONS_PARAMETERS_DETAILS);
                return pd.ByHead_Sys(HeadId, SysId);
            }
        }
    }
}
