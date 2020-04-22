using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class CATEGORIES
    {
        private List<SV_CATEGORY> _source;
        public CATEGORIES(ObjectSet<CATEGORY> source) { _source = source.ToList().ConvertAll(c => (SV_CATEGORY)c); }
        //public CATEGORIES(List<CATEGORY> source) { _source = source; }

        public SV_CATEGORY ById(int idFind)
        {
            return _source.Where(c => c.id == idFind).FirstOrDefault();
        }

        public GENERIC_VALUE ByCode(string codeFind)
        {
            return _source.Where(c => c.code == codeFind).FirstOrDefault();
        }

        public List<GENERIC_VALUE> All()
        {
            return _source.ConvertAll(c => (GENERIC_VALUE)c);
        }
    }
}
