using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.HelperServiceAFN
{
    public class CLASE
    {
        public List<GENERIC_VALUE> SearchList()
        {
            using (AFN2Entities context = new AFN2Entities())
            {
                var k = new Repositories.KINDS(context.KINDS);
                return k.SearchList();
            }
        }

        public GENERIC_VALUE Activos
        {
            get
            {
                using (AFN2Entities context = new AFN2Entities())
                {
                    var k = new Repositories.KINDS(context.KINDS);
                    return k.Activos;
                }
            }
        }
        public GENERIC_VALUE Intagibles
        {
            get
            {
                using (AFN2Entities context = new AFN2Entities())
                {
                    var k = new Repositories.KINDS(context.KINDS);
                    return k.Intagibles;
                }
            }
        }

        public GENERIC_VALUE OBC
        {
            get
            {
                using (AFN2Entities context = new AFN2Entities())
                {
                    var k = new Repositories.KINDS(context.KINDS);
                    return k.OBC;
                }
            }
        }
    }
}
