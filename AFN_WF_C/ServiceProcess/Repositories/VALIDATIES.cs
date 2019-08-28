using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class VALIDATIES
    {
        private List<VALIDATY> _source;
        public VALIDATIES(ObjectSet<VALIDATY> source) { _source = source.ToList(); }
        public VALIDATIES(List<VALIDATY> source) { _source = source; }

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(v => v.id == idFind).FirstOrDefault();
        }

        public List<GENERIC_VALUE> All() {
            return _source.ConvertAll(v => (GENERIC_VALUE)v);
        }

        public List<GENERIC_VALUE> Downs()
        {

            return _source
                .Where(v => v.id == 2 || v.id == 3)
                .ToList()
                .ConvertAll(v => (GENERIC_VALUE)v);
        }

        public List<GENERIC_VALUE> Ups()
        {

            return _source
                .Where(v => v.id == 1)
                .ToList()
                .ConvertAll(v => (GENERIC_VALUE)v);
        }

        public List<GENERIC_VALUE> SearchDownsList()
        {
            var resulta = Downs();
            resulta.Insert(0, new GENERIC_VALUE() { id = 0, type = "OPTION", description = "TODOS" });
            return resulta;
        }
    }
}
