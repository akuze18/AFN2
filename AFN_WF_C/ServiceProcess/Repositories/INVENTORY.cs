using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class INVENTORY
    {
        private List<ARTICLE> _source;
        public INVENTORY(ObjectSet<ARTICLE> source)
        {
            _source = source.ToList();
        }

        public List<ARTICLE> ByParts(int[] parts_ids)
        {
            return _source.Where(a => parts_ids.Contains(a.part_id))
                .ToList();
        }
        public int GetCorrelativoCodigo(string raiz)
        {
            return _source.Where(a => a.code.StartsWith(raiz)).Count();
        }
    }
}
