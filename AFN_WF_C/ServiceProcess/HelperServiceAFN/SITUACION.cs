using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.HelperServiceAFN
{
    public class SITUACION
    {
        public GENERIC_VALUE ByValidations(int estado1, int estado2)
        {
            using (AFN2Entities context = new AFN2Entities())
            {
                var s = new Repositories.SITUATIONS(context.SITUATIONS);
                return s.ByValidations(estado1, estado2);
            }
        }
    }
}
