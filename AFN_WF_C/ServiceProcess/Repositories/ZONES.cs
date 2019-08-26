using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.DataView;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class ZONES
    {
        private List<SV_ZONE> _source;
        public ZONES(ObjectSet<ZONE> source) { _source = source.ToList().ConvertAll(z => (SV_ZONE)z); }
        //public ZONES(List<ZONE> source) { _source = source; }

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
        public List<GENERIC_VALUE> All()
        {
            var zona =
                   (from o in _source
                   where o.active == true
                   select o).ToList();
            return zona.ConvertAll(z => (GENERIC_VALUE)z);
        }

        public GENERIC_VALUE ByCode(string codeFind)
        {
            return _source.Where(z => z.codDept == codeFind || z.codOld == codeFind).FirstOrDefault();
        }
    }
}
