using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class INV_ARTICLES
    {
        private List<SV_ARTICLE> _source;
        public INV_ARTICLES(ObjectSet<ARTICLE> source)
        {
            _source = source.ToList().ConvertAll(a => (SV_ARTICLE)a);
        }

        public List<SV_ARTICLE> ByParts(int[] parts_ids)
        {
            return _source.Where(a => parts_ids.Contains(a.part_id))
                .ToList();
        }
        public int GetCorrelativoCodigo(string raiz)
        {
            return _source.Where(a => a.code.StartsWith(raiz)).Count();
        }

        public List<SV_ARTICLE> GetDetalleArticulo(int partId)
        {
            return _source.Where(a => a.part_id == partId).ToList();
        }


    }
}
