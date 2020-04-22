using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class TRANSACTIONS_HEADERS
    {
        private List<SV_TRANSACTION_HEADER> _source;
        public TRANSACTIONS_HEADERS(ObjectSet<TRANSACTION_HEADER> source) 
        { 
            _source = source.ToList().ConvertAll(th => (SV_TRANSACTION_HEADER)th); 
        }

        public List<SV_TRANSACTION_HEADER> ByParte(int ParteID)
        {
            return _source.Where(x => x.article_part_id == ParteID).ToList();
        }
        public List<SV_TRANSACTION_HEADER> ByPartes(int[] PartesIDs)
        {
            return _source.Where(x => PartesIDs.Contains(x.article_part_id)).ToList();
        }

        public SV_TRANSACTION_HEADER byPartFechaValid(int PartArtId, DateTime fecha_corte)
        {
            return _source.Where(th => th.article_part_id == PartArtId && 
                (th.trx_ini <= fecha_corte && th.trx_end >fecha_corte)).FirstOrDefault();
        }
        public SV_TRANSACTION_HEADER byPartFechaFix(int PartArtId, DateTime fecha_corte)
        {
            return _source.Where(th => th.article_part_id == PartArtId &&
                th.trx_ini == fecha_corte).FirstOrDefault();
        }
        public List<SV_TRANSACTION_HEADER> byPartsFechaFix(List<SV_PART> Parts, DateTime fecha_corte)
        {
            int[] PartsId = Parts.Select(p => p.id).ToArray();
            return _source.Where(th => PartsId.Contains(th.article_part_id) &&
                th.trx_ini == fecha_corte).ToList();
        }

        public SV_TRANSACTION_HEADER ByParteFirst(int ParteID)
        {
            DateTime firstTime = this.ByParte(ParteID).Select(x => x.trx_ini).Min();
            return _source
                    .Where(x => x.article_part_id == ParteID &&
                        x.trx_ini == firstTime)
                    .First();
        }
    }
}
