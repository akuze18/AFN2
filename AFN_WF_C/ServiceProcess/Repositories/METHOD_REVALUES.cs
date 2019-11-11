using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class METHOD_REVALUES
    {
        private List<SV_METHOD_REVALUE> _source;
        public METHOD_REVALUES(ObjectSet<METHOD_REVALUE> source) { _source = source.ToList().ConvertAll(ap => (SV_METHOD_REVALUE)ap); }
        

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(ap => ap.id == idFind).FirstOrDefault();
        }
        public List<GENERIC_VALUE> All()
        {
            return _source.ConvertAll(mr => (GENERIC_VALUE)mr);
        }
    }
}
