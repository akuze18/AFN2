using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class APROVALS_STATES
    {
        private List<APROVAL_STATE> _source;
        public APROVALS_STATES(ObjectSet<APROVAL_STATE> source) { _source = source.ToList(); }
        public APROVALS_STATES(List<APROVAL_STATE> source) { _source = source; }

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
    }
}
