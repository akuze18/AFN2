﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class TRANSACTIONS_HEADERS
    {
        private List<TRANSACTION_HEADER> _source;
        public TRANSACTIONS_HEADERS(ObjectSet<TRANSACTION_HEADER> source) { _source = source.ToList(); }

        public List<TRANSACTION_HEADER> ByParte(int ParteID)
        {
            return _source.Where(x => x.article_part_id == ParteID).ToList();
        }

        public TRANSACTION_HEADER byPartFecha(int PartArtId, DateTime fecha_corte)
        {
            return _source.Where(th => th.article_part_id == PartArtId && 
                (th.trx_ini <= fecha_corte && th.trx_end >fecha_corte)).FirstOrDefault();
        }
    }
}
