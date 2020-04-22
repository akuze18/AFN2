using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class GP_MultiCurrency
    {
        private List<MC00101> _source;
        public GP_MultiCurrency(ObjectSet<MC00101> source)
        {
            _source = source.ToList();
        }

        public decimal YEN(DateTime fecha)
        {
            var result = _source
                    .Where(mc =>
                        mc.CURNCYID.Trim() == "YEN" && 
                        mc.EXGTBLID.Contains("OBS")&&
                        mc.EXCHDATE == fecha
                    )
                    .Select(mc => mc.XCHGRATE);
            //if(result.Count() > 0)
            //    AFN_WF_C.PCClient.Procesos.Mensaje.Info(result.First().CURNCYID);
            //else
            //    AFN_WF_C.PCClient.Procesos.Mensaje.Info("Sin resultados");
            return result.FirstOrDefault();
            //return 0;
        }

    }
}
