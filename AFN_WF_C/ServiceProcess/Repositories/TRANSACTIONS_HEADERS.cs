using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.DataView;

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

        public SV_TRANSACTION_HEADER byPartFecha(int PartArtId, DateTime fecha_corte)
        {
            return _source.Where(th => th.article_part_id == PartArtId && 
                (th.trx_ini <= fecha_corte && th.trx_end >fecha_corte)).FirstOrDefault();
        }
    }
}
