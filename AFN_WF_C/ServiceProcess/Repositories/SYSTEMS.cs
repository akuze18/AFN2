using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class SYSTEMS
    {
        private List<SYSTEM> _source;
        public SYSTEMS(ObjectSet<SYSTEM> source) { _source = source.ToList(); }
        public SYSTEMS(List<SYSTEM> source) { _source = source; }

        public SYSTEM ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
    }
}
