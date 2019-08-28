using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.DataView;

namespace AFN_WF_C.ServiceProcess
{
    class Process
    {
        internal static SV_SYSTEM SistemaDefecto()
        {
            using (AFN2Entities context = new AFN2Entities())
            using (Repositories.Main Repo = new Repositories.Main(context))
            {
                return Repo.sistemas.Default;
            }

        }
    }
}
