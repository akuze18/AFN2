using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.DataView;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class SITUATIONS
    {
        private List<SV_SITUATION> _source;
        public SITUATIONS(ObjectSet<SITUATION> source) { _source = source.ToList().ConvertAll(s => (SV_SITUATION)s); }
        //public SITUATIONS(List<SITUATION> source) { _source = source; }

        public GENERIC_VALUE ByValidations(int idEstadoInicial, int idEstadoFinal)
        {
            return _source.Where(s => s.estado1 == idEstadoInicial && s.estado2 == idEstadoFinal).FirstOrDefault();
        }
    }
}
