using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.HelperServiceAFN
{
    public class PACKAGE_CLASE
    {
        public List<GENERIC_RELATED> All()
        {
            using (AFN2Entities context = new AFN2Entities())
            {
                var pk = new Repositories.PACKAGE_KINDS(context.PACKAGE_KINDS);
                return pk.All();
            }
        }

        public List<GENERIC_RELATED> ByType(int type_id)
        {
            using (AFN2Entities context = new AFN2Entities())
            {
                var pk = new Repositories.PACKAGE_KINDS(context.PACKAGE_KINDS);
                return pk.ByType(type_id);
            }
        }
    }
}
