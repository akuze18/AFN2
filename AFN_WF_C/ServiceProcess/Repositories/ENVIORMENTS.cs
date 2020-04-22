using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class ENVIORMENTS
    {
        private List<SV_ENVIORMENT> _source;
        public ENVIORMENTS(ObjectSet<ENVIORMENT> source)
        { _source = source.ToList().ConvertAll(k => (SV_ENVIORMENT)k); }

        public List<SV_ENVIORMENT> GetAll()
        {
            return _source;
        }
    }
}
