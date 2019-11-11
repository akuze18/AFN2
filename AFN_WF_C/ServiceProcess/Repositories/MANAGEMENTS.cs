using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class MANAGEMENTS
    {
        private List<SV_MANAGEMENT> _source;
        public MANAGEMENTS(ObjectSet<MANAGEMENT> source) { _source = source.ToList().ConvertAll(m => (SV_MANAGEMENT)m); }

        public GENERIC_VALUE ById(int? idFind)
        {
            if (idFind != null)
            {
                return _source.Where(z => z.id == idFind).FirstOrDefault();
            }
            else { return new GENERIC_VALUE() { type = "MANAGEMENT"}; }
        }

        public List<GENERIC_VALUE> All()
        {
            return _source.ConvertAll(m => (GENERIC_VALUE)m);
        }
    }
}
