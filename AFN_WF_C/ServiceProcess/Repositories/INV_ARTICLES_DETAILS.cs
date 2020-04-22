using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class INV_ARTICLES_DETAILS
    {
        private List<SV_ARTICLE_DETAIL> _source;
        public INV_ARTICLES_DETAILS(ObjectSet<ARTICLES_VALUES> source) 
        { 
            _source = source
                .Include("ARTICLE")
                .Include("ATTRIBUTE")
                .ToList()
                .ConvertAll(s => (SV_ARTICLE_DETAIL)s); 
        }

        public List<SV_ARTICLE_DETAIL> ByBatch(int batch_id)
        {
            return _source.Where(a => a.lote_id == batch_id).ToList();
        }
        
    }
}
