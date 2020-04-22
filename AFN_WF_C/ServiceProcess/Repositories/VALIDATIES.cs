using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class VALIDATIES
    {
        private List<SV_VALIDATY> _source;
        public VALIDATIES(ObjectSet<VALIDATY> source) { _source = source.ToList().ConvertAll(v => (SV_VALIDATY)v); }

        public SV_VALIDATY ById(int idFind)
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
        public List<GENERIC_VALUE> Sells()
        {

            return _source
                .Where(v => v.id == 2)
                .ToList()
                .ConvertAll(v => (GENERIC_VALUE)v);
        }
        public List<GENERIC_VALUE> Disposals()
        {

            return _source
                .Where(v => v.id == 3)
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

        public SV_VALIDATY VIGENTE()
        {
            return _source.Where(v => v.name == "VIGENTE").First();
        }
        public SV_VALIDATY VENTA()
        {
            return _source.Where(v => v.name == "VENTA").First();
        }
        public SV_VALIDATY CASTIGO()
        {
            return _source.Where(v => v.name == "CASTIGO").First();
        }
    }
}
