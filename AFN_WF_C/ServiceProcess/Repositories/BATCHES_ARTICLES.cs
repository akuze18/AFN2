using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class BATCHES_ARTICLES
    {
        private List<BATCH_ARTICLE> _source;

        public BATCHES_ARTICLES(ObjectSet<BATCH_ARTICLE> source) { _source = source.ToList(); }

        public void add_new(BATCH_ARTICLE batch){
            _source.Add(batch);
        }
    }
}
