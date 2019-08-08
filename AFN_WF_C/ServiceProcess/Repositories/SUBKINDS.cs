using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class SUBKINDS
    {
        private List<SUBKIND> _source;
        public SUBKINDS(ObjectSet<SUBKIND> source) { _source = source.ToList(); }
        public SUBKINDS(List<SUBKIND> source) { _source = source; }

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(sk => sk.id == idFind).FirstOrDefault();
        }

        public GENERIC_VALUE ByCode(string codeFind)
        {
            if (string.IsNullOrEmpty(codeFind))
                return _source.Where(sk => sk.id == 1).FirstOrDefault();
            return _source.Where(sk => sk.code == codeFind).FirstOrDefault();
        }
    }
}
