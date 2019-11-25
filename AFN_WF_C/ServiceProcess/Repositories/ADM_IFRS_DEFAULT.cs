using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class ADM_IFRS_DEFAULT
    {
        private List<IFRS_DEFAULT> _source;
        public ADM_IFRS_DEFAULT(ObjectSet<IFRS_DEFAULT> source)
        {
            _source = source.ToList();
        }

        public decimal residual_value_rate(GENERIC_VALUE Kind)
        {
            return _source.Where(idef => idef.kind_id == Kind.id)
                        .Select(idef => idef.pValRes)
                        .DefaultIfEmpty(0)
                        .First();
        }

    }
}
