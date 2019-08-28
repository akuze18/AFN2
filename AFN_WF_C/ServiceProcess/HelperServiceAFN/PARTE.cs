using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.DataView;

namespace AFN_WF_C.ServiceProcess.HelperServiceAFN
{
    public class PARTE
    {
        public List<SV_PART> ByLote(int l) {
            using (AFN2Entities context = new AFN2Entities())
            {
                var p = new Repositories.PARTS(context.PARTS);
                return p.ByLote(l);
            }
        }
    }
}
