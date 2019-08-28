using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.DataView;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class PARTS
    {
        private List<SV_PART> _source;

        public PARTS(ObjectSet<PART> source) { _source = source.ToList().ConvertAll(p => (SV_PART)p); }

        public List<SV_PART> ByLote(int Lote)
        {
            return _source.Where(p => p.article_id == Lote).ToList();
        }
    }
}
