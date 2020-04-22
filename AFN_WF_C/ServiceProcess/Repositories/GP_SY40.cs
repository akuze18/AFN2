using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;
using ACode;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class GP_SY40
    {
        private List<SY40100> _period_check;
        private List<SY40101> _year_check;
        public GP_SY40(ObjectSet<SY40100> source1, ObjectSet<SY40101> source2)
        {
            _period_check = source1.ToList();
            _year_check = source2.ToList();
        }

        public ACode.Vperiodo abierto()
        {
            var hoy = DateTime.Today;
            var year_no_hist = _year_check
                    .Where(y => y.HISTORYR == 0)
                    .Select(y => y.YEAR1).ToArray();
            var result = (from p in _period_check
                          where year_no_hist.Contains(p.YEAR1) && 
                            p.CLOSED == 0 &&
                            p.SERIES != 0 &&
                            p.PERIODID != 0
                          orderby p.YEAR1 descending
                          orderby p.PERIODID descending
                          group p by new {p.YEAR1, p.PERIODID} 
                          
                          into grouped
                          select grouped.Key);
            //if (result.Count() > 0)
            foreach (var p in result)
                return new Vperiodo(p.YEAR1, p.PERIODID);
            //else
            return new Vperiodo(hoy.Year, hoy.Month);
        }

        public List<Vperiodo> ingreso()
        {
            List<Vperiodo> result = new List<Vperiodo>(); 
            var Now = DateTime.Today;
            var Ping = new ACode.Vperiodo(Now.Year, Now.Month) + 1;

            for (int i = 0; i < 8; i++)
            {
                result.Add(Ping);
                Ping = Ping - 1;
            }

            return result;
        }
    }
}
