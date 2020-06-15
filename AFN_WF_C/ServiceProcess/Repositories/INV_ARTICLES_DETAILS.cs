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
            return _source.Where(a => a.lote_id == batch_id && a.article_id == null).ToList();
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

        public List<SV_ARTICLE_DETAIL> GetValuesByAttribute(List<SV_ATTRIBUTE> Attributes)
        {
            return GetValuesByAttribute(Attributes.Select(a => a.id).ToArray());
        }
        public List<SV_ARTICLE_DETAIL> GetValuesByAttribute(int[] AttributesId)
        {
            return _source.Where(ad => AttributesId.Contains(ad.cod_atrib)).ToList();
        }

        public string ExtraDescrip(int BatchId)
        {
            string result = string.Empty;
            var values = _source.Where(a => a.lote_id == BatchId && 
                                a.article_id == null &&
                                a.imprimir &&
                                a.fech_ini <= DateTime.Today && a.fech_fin> DateTime.Today)
                            .OrderBy(a => a.cod_atrib)
                            .ToList();
            foreach (var value in values)
                result = result + ", " +(value.atributo.imprimir? value.atributo.name + " " :string.Empty) + value.detalle;
            return result;
        }

        
    }
}
