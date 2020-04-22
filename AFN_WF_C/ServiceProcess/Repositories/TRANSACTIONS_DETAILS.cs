using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class TRANSACTIONS_DETAILS
    {
        private List<SV_TRANSACTION_DETAIL> _source;
        public TRANSACTIONS_DETAILS(ObjectSet<TRANSACTION_DETAIL> source) 
        {
            _source = source.ToList().ConvertAll(th => (SV_TRANSACTION_DETAIL)th); 
        }

        public List<SV_TRANSACTION_DETAIL> GetByHeadsSystem(int[] head_ids, SV_SYSTEM system)
        {
            return _source
                .Where(td => 
                    head_ids.Contains(td.trx_head_id) && 
                    td.system_id == system.id)
                .ToList();
        }
        public SV_TRANSACTION_DETAIL GetByPartSystem(int head_id, SV_SYSTEM system)
        {
            return _source
                .Where(td => td.trx_head_id == head_id &&
                    td.system_id == system.id)
                .FirstOrDefault();
        }

        public List<SV_TRANSACTION_DETAIL> GetByHead(int head_id)
        {
            return _source
                .Where(td => td.trx_head_id == head_id)
                .ToList();
        }
    }
}
