using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.HelperServiceAFN
{
    public class ESTADO_APROVACION
    {
        public List<GENERIC_VALUE> All {
            get {
                using (AFN2Entities context = new AFN2Entities())
                {
                    var z = new Repositories.APROVALS_STATES(context.APROVAL_STATES);
                    return z.All;
                }
            }
        }
        public List<GENERIC_VALUE> OnlyActive
        {
            get
            {
                using (AFN2Entities context = new AFN2Entities())
                {
                    var z = new Repositories.APROVALS_STATES(context.APROVAL_STATES);
                    return z.OnlyActive;
                }
            }
        }
        public List<GENERIC_VALUE> OnlyDigited
        {
            get
            {
                using (AFN2Entities context = new AFN2Entities())
                {
                    var z = new Repositories.APROVALS_STATES(context.APROVAL_STATES);
                    return z.OnlyDigited;
                }
            }
        }
        public List<GENERIC_VALUE> NoDeleted
        {
            get
            {
                using (AFN2Entities context = new AFN2Entities())
                {
                    var z = new Repositories.APROVALS_STATES(context.APROVAL_STATES);
                    return z.NoDeleted;
                }
            }
        }

        public List<GENERIC_VALUE> Default
        {
            get
            {
                using (AFN2Entities context = new AFN2Entities())
                {
                    var z = new Repositories.APROVALS_STATES(context.APROVAL_STATES);
                    return z.Default;
                }
            }
        }
    }
}
