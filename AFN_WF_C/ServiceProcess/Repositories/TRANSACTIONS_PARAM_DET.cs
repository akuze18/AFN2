using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class TRANSACTIONS_PARAM_DET
    {
        private List<TRANSACTION_PARAMETER_DETAIL> _source;
        public TRANSACTIONS_PARAM_DET(ObjectSet<TRANSACTION_PARAMETER_DETAIL> source) 
        { _source = source.Include("PARAMETER") .ToList(); }

        public List<PARAM_VALUE> ByHead_Sys(int HeadId, int SysId)
        {
            var myParams = _source.Where(x => x.trx_head_id == HeadId && x.system_id == SysId).ToList();
            return myParams.ConvertAll(x => (PARAM_VALUE)x);
        }
    }
}
