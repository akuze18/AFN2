using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class STATES
    {
        private List<GENERIC_VALUE> _source;
        public STATES(ObjectSet<STATE> source) 
        { _source = source.ToList().ConvertAll(s => (GENERIC_VALUE)s); }


        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(s => s.id == idFind).FirstOrDefault();
        }

        public List<GENERIC_VALUE> All()
        {
            return _source.Where(s => s.id>=0)
                .ToList()
                .ConvertAll(s => (GENERIC_VALUE)s);
        }
    }
}
