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
            return _source.Where(
                    mc =>   mc.CURNCYID == "YEN" &&
                            mc.EXGTBLID.Contains("OBS") &&
                            mc.EXCHDATE == fecha
                    )
                    .Select(mc => mc.XCHGRATE).FirstOrDefault();
        }

    }
}
