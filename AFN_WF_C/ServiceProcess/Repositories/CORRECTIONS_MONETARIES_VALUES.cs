using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class CORRECTIONS_MONETARIES_VALUES
    {
        private List<CORRECTION_MONETARY_VALUE> _source;
        public CORRECTIONS_MONETARIES_VALUES(ObjectSet<CORRECTION_MONETARY_VALUE> source) { _source = source.ToList(); }
        public CORRECTIONS_MONETARIES_VALUES(List<CORRECTION_MONETARY_VALUE> source) { _source = source; }

        public CORRECTION_MONETARY_VALUE ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }

        public LIST_CORR_MON ByPeriod(string periodo)
        {
            return _source.Where(x => x.period == periodo).ToList();
        }
    }
}
