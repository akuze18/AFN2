using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class PARAMETERS
    {
        private List<PARAMETER> _source;
        public PARAMETERS(ObjectSet<PARAMETER> source) { _source = source.ToList(); }
        public PARAMETERS(List<PARAMETER> source) { _source = source; }

        public PARAMETER ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
        public PARAMETER byCode(string codeFind)
        {
            return (from p in _source where p.code == codeFind select p).First();
        }
    }
}
