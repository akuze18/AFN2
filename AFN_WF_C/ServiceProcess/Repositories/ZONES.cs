using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class ZONES
    {
        private List<ZONE> _source;
        public ZONES(ObjectSet<ZONE> source) { _source = source.ToList(); }
        public ZONES(List<ZONE> source) { _source = source; }

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
        public List<GENERIC_VALUE> All()
        {
            var result = new List<GENERIC_VALUE>();
            var zona =
                   from o in _source
                   where o.active == true
                   select o;
            foreach (var z in zona)
            {
                result.Add(z);
            }
            return result;
        }
    }
}
