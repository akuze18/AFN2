using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class TYPES_ASSETS
    {
        private List<TYPE_ASSET> _source;
        public TYPES_ASSETS(ObjectSet<TYPE_ASSET> source) { _source = source.ToList(); }
        public TYPES_ASSETS(List<TYPE_ASSET> source) { _source = source; }

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }

        public List<GENERIC_VALUE> All()
        {
            return _source.ConvertAll(x => (GENERIC_VALUE)x);
        }
    }
}
