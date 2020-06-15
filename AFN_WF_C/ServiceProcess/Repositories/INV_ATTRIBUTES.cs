using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using DC = AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class INV_ATTRIBUTES
    {
        private List<SV_ATTRIBUTE> _source;

        public INV_ATTRIBUTES(ObjectSet<DC.ATTRIBUTE> source)
        {
            _source = source.ToList().ConvertAll(a => (SV_ATTRIBUTE)a);
        }

        public List<SV_ATTRIBUTE> AllActive()
        {
            return _source.Where(a => a.active).ToList();
        }
        public SV_ATTRIBUTE Entregado()
        {
            return _source.Where(at => at.name == "ENTREGADO").FirstOrDefault();
        }

        public List<SV_ATTRIBUTE> GetFotoType()
        {
            return _source.Where(a => a.tipo == "FOTO").ToList();
        }
    }
}
