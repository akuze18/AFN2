using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class ORIGINS
    {
        private List<ORIGIN> _source;
        public ORIGINS(ObjectSet<ORIGIN> source) { _source = source.ToList(); }
        public ORIGINS(List<ORIGIN> source) { _source = source; }

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
    }
}
