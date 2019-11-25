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

        public List<SV_TRANSACTION_DETAIL> GetByPartsSystem(int[] head_ids, SV_SYSTEM system)
        {
            return _source
                .Where(td => 
                    head_ids.Contains(td.trx_head_id) && 
                    td.system_id == system.id)
                .ToList();
        }
    }
}
