using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.DataView;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class TRANSACTIONS_PARAM_DET
    {
        private List<SV_TRANSACTION_PARAMETER_DETAIL> _source;
        public TRANSACTIONS_PARAM_DET(ObjectSet<TRANSACTION_PARAMETER_DETAIL> source)
        { 
            _source = source
                .Include("PARAMETER")
                .ToList()
                .ConvertAll(c => (SV_TRANSACTION_PARAMETER_DETAIL)c); 
        }
        public TRANSACTIONS_PARAM_DET(ObjectSet<TRANSACTION_PARAMETER_DETAIL> source, int SystemId, int[] Heads)
        { 
            _source = source
                .Include("PARAMETER")
                .Where(tpd => tpd.system_id == SystemId && Heads.Contains(tpd.trx_head_id))
                .ToList()
                .ConvertAll(c => (SV_TRANSACTION_PARAMETER_DETAIL)c); 
        }

        public List<PARAM_VALUE> ByHead_Sys(int HeadId, int SysId)
        {
            var myParams = _source.Where(x => x.trx_head_id == HeadId && x.system_id == SysId).ToList();
            return myParams.ConvertAll(x => (PARAM_VALUE)x);
        }
    }
}
