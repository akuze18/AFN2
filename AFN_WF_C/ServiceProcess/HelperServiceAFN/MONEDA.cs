using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.HelperServiceAFN
{
    public class MONEDA
    {
        public List<GENERIC_VALUE> All()
        {
            using (AFN2Entities context = new AFN2Entities())
            {
                var c = new Repositories.CURRENCIES(context.CURRENCIES);
                return c.All();
            }
        }
    }
}
