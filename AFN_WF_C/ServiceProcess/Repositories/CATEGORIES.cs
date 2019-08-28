using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.DataView;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class CATEGORIES
    {
        private List<SV_CATEGORY> _source;
        public CATEGORIES(ObjectSet<CATEGORY> source) { _source = source.ToList().ConvertAll(c => (SV_CATEGORY)c); }
        //public CATEGORIES(List<CATEGORY> source) { _source = source; }

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
