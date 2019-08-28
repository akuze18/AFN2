using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.DataView;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class SUBZONES
    {
        private List<SV_SUBZONE> _source;
        public SUBZONES(ObjectSet<SUBZONE> source) { _source = source.ToList().ConvertAll(sz =>(SV_SUBZONE)sz); }        

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
    }
}
