using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class PARTS
    {
        private List<PART> _source;

        public PARTS(ObjectSet<PART> source) { _source = source.ToList(); }

        public List<PART> ByLote(int Lote)
        {
            return _source.Where(p => p.article_id == Lote).ToList();
        }
    }
}
