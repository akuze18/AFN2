using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class SUBKINDS
    {
        private List<SV_SUBKIND> _source;
        public SUBKINDS(ObjectSet<SUBKIND> source) { _source = source.ToList().ConvertAll(sk=>(SV_SUBKIND)sk); }
        //public SUBKINDS(List<SUBKIND> source) { _source = source; }

        public SV_SUBKIND ById(int idFind)
        {
            return _source.Where(sk => sk.id == idFind).FirstOrDefault();
        }

        public GENERIC_VALUE ByCode(string codeFind)
        {
            if (string.IsNullOrEmpty(codeFind))
                return _source.Where(sk => sk.id == 1).FirstOrDefault();
            return _source.Where(sk => sk.code == codeFind).FirstOrDefault();
        }

        public List<GENERIC_VALUE> ByKind(GENERIC_VALUE clase)
        {
            return _source.Where(sk => sk.kind_id == clase.id && sk.display)
                .ToList().ConvertAll(sk => (GENERIC_VALUE)sk);
        }

        public SV_SUBKIND ByGeneric(GENERIC_VALUE subkind)
        {
            return _source.Where(sk => sk.id == subkind.id).First();
        }
    }
}
