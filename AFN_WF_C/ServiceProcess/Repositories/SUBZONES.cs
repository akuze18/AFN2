using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class SUBZONES
    {
        private List<SV_SUBZONE> _source;
        public SUBZONES(ObjectSet<SUBZONE> source) { _source = source.ToList().ConvertAll(sz =>(SV_SUBZONE)sz); }        

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }

        public List<GENERIC_VALUE> ByZone(GENERIC_VALUE zone)
        {
            return _source.Where(sz => sz.zone_id == zone.id)
                .Where(sz => sz.active)
                .ToList().ConvertAll(sz => (GENERIC_VALUE)sz);
        }
    }
}
