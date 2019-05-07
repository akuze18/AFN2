using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess
{
    class Process
    {
        internal static SYSTEM SistemaDefecto()
        {
            using (AFN2Entities context = new AFN2Entities())
            {
                SYSTEM defecto = (from a in context.SYSTEMS.Include("CURRENCY")
                                  .Include("ENVIORMENT")
                                  where a.CURRENCY.code == "CLP" &&
                                  a.ENVIORMENT.code == "FIN"
                                  select a).First();
                return defecto;
            }

        }
    }
}
