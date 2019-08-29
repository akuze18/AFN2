using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class TYPES_ASSETS
    {
        private List<SV_TYPE_ASSET> _source;
        public TYPES_ASSETS(ObjectSet<TYPE_ASSET> source) { _source = source.ToList().ConvertAll(ta => (SV_TYPE_ASSET) ta); }

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
