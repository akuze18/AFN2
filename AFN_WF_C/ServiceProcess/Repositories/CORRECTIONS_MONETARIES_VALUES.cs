using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.DataView;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class CORRECTIONS_MONETARIES_VALUES
    {
        private List<SV_CORRECTION_MONETARY_VALUE> _source;
        public CORRECTIONS_MONETARIES_VALUES(ObjectSet<CORRECTION_MONETARY_VALUE> source) 
        { 
            _source = source.ToList().ConvertAll(cm => (SV_CORRECTION_MONETARY_VALUE)cm); 
        }
        public CORRECTIONS_MONETARIES_VALUES(ObjectSet<CORRECTION_MONETARY_VALUE> source, 
            string periodo)
        {
            _source = source
                .Where(cm => cm.period == periodo)
                .ToList()
                .ConvertAll(cm => (SV_CORRECTION_MONETARY_VALUE)cm);
        }

        public SV_CORRECTION_MONETARY_VALUE ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }

        //public List<SV_CORRECTION_MONETARY_VALUE> ByPeriod(string periodo)
        //{
        //    return _source.Where(x => x.period == periodo).ToList();
        //}

        public SV_CORRECTION_MONETARY_VALUE byAplica(DateTime fecha_compra, ACode.Vperiodo Periodo)
        {

            if (Periodo.last.Year == fecha_compra.Year)
            {
                var r = _source.Where(x => x.applyTo == fecha_compra.Month).FirstOrDefault();
                if (r != null)
                    return r;
                else
                    return SV_CORRECTION_MONETARY_VALUE.empty;
            }
            else
            {
                var r = _source.Where(x => x.applyTo == 0).FirstOrDefault();
                if (r != null)
                    return r;
                else
                    return SV_CORRECTION_MONETARY_VALUE.empty;
            }
        }
    }
}
