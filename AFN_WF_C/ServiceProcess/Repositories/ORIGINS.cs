using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.DataView;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class ORIGINS
    {
        private List<SV_ORIGIN> _source;
        public ORIGINS(ObjectSet<ORIGIN> source) 
        { 
            _source = source
                .ToList()
                .ConvertAll(o =>(SV_ORIGIN)o ); 
        }

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
    }
}
