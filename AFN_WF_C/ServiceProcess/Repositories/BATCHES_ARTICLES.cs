using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class BATCHES_ARTICLES
    {
        private List<SV_BATCH_ARTICLE> _source;

        public BATCHES_ARTICLES(ObjectSet<BATCH_ARTICLE> source) { 
            _source = source.ToList().ConvertAll(ba => (SV_BATCH_ARTICLE) ba); 
        }

        public void add_new(SV_BATCH_ARTICLE batch)
        {
            _source.Add(batch);
        }
    }
}
