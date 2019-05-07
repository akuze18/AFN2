using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class SYSTEMS_PARAMETERS
    {
        private List<SYSTEM_PARAMETER> _source;
        public SYSTEMS_PARAMETERS(ObjectSet<SYSTEM_PARAMETER> source) { _source = source.ToList(); }
        public SYSTEMS_PARAMETERS(List<SYSTEM_PARAMETER> source) { _source = source; }

        public SYSTEM_PARAMETER ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }

        public List<SYSTEM_PARAMETER> BySystem(SYSTEM s)
        {
            return BySystem(s.id);
        }

        public List<SYSTEM_PARAMETER> BySystem(int sistem_id)
        {
            return _source.Where(x => x.system_id == sistem_id).ToList();
        }
    }
}
