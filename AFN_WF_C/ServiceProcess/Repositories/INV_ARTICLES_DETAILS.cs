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

        public SV_ARTICLE_DETAIL ForArticle(int BatchId, int? ArticleId, int AttrId)
        {
            IEnumerable<SV_ARTICLE_DETAIL> details;
            if(ArticleId != null)
            {
                details = _source.Where(sa => sa.article_id == ArticleId && sa.cod_atrib == AttrId);
                if (details.Count() == 0)
                    details = _source.Where(sa => sa.lote_id == BatchId && sa.cod_atrib == AttrId);
            }
            else
                details = _source.Where(sa => sa.lote_id == BatchId && sa.cod_atrib == AttrId);
            return details.OrderByDescending(sa => sa.fech_ini).DefaultIfEmpty(SV_ARTICLE_DETAIL.Empty).FirstOrDefault();
        }
    }
}
