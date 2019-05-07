using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class DOCUMENTS
    {
        private List<DOCUMENT> _source;
        public DOCUMENTS(ObjectSet<DOCUMENT> source) { _source = source.ToList(); }
        public DOCUMENTS(List<DOCUMENT> source) { _source = source; }

        public DOCUMENT ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
        public List<DOCUMENT> ByBatch(BATCH_ARTICLE b)
        {
            return ByBatch(b.id);
        }
        public List<DOCUMENT> ByBatch(int batch_id)
        {
            return _source.Where(x => x.batch_article_id == batch_id).ToList();
        }
    }
}
