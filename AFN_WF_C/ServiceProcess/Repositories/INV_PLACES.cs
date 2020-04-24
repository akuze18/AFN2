using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class INV_PLACES
    {
        private List<SV_PLACE> _source;

        public INV_PLACES(ObjectSet<PLACE> source)
        {
            _source = source.ToList().ConvertAll(a => (SV_PLACE)a);
            foreach (var plc in _source)
            {
                plc.parentPlace = _source
                                    .Where(p => p.id == plc.superior)
                                    .FirstOrDefault();
            }
        }

        public SV_PLACE ById(int PlaceId)
        {
            return _source.Where(p => p.id == PlaceId).FirstOrDefault();
        }
    }
}
