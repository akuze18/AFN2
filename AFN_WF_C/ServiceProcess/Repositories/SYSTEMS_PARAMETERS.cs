using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class SYSTEMS_PARAMETERS
    {
        private List<SV_SYSTEM_PARAMETER> _source;
        public SYSTEMS_PARAMETERS(ObjectSet<SYSTEM_PARAMETER> source) { _source = source.ToList().ConvertAll(sp=>(SV_SYSTEM_PARAMETER)sp); }

        public SV_SYSTEM_PARAMETER ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }

        public List<SV_SYSTEM_PARAMETER> BySystem(SV_SYSTEM s)
        {
            return BySystem(s.id);
        }

        public List<SV_SYSTEM_PARAMETER> BySystem(int sistem_id)
        {
            return _source.Where(x => x.system_id == sistem_id).ToList();
        }
    }
}
