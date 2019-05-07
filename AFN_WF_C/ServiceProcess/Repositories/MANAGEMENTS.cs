using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class MANAGEMENTS
    {
        private List<MANAGEMENT> _source;
        public MANAGEMENTS(ObjectSet<MANAGEMENT> source) { _source = source.ToList(); }
        public MANAGEMENTS(List<MANAGEMENT> source) { _source = source; }

        public GENERIC_VALUE ById(int? idFind)
        {
            if (idFind != null)
            {
                return _source.Where(z => z.id == idFind).FirstOrDefault();
            }
            else { return new GENERIC_VALUE(); }
        }
    }
}
