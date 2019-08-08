using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class CATEGORIES
    {
        private List<CATEGORY> _source;
        public CATEGORIES(ObjectSet<CATEGORY> source) { _source = source.ToList(); }
        public CATEGORIES(List<CATEGORY> source) { _source = source; }

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(c => c.id == idFind).FirstOrDefault();
        }

        public GENERIC_VALUE ByCode(string codeFind)
        {
            return _source.Where(c => c.code == codeFind).FirstOrDefault();
        }
    }
}
