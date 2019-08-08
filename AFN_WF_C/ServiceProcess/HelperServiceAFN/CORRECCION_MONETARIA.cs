using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.HelperServiceAFN
{
    public class CORRECCION_MONETARIA
    {
        public LIST_CORR_MON ByPeriodo(string periodo)
        {
            using (AFN2Entities context = new AFN2Entities())
            {
                var z = new Repositories.CORRECTIONS_MONETARIES_VALUES(context.CORRECTIONS_MONETARIES_VALUES);
                return z.ByPeriod(periodo);
            }   
        }
    }
}
