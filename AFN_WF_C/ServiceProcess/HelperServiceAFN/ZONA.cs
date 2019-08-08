using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.HelperServiceAFN
{
    public class ZONA
    {
        public List<GENERIC_VALUE> All()
        {
            using (AFN2Entities context = new AFN2Entities())
            {
                var z = new Repositories.ZONES(context.ZONES);
                return z.All();
            }
        }
    }
}
