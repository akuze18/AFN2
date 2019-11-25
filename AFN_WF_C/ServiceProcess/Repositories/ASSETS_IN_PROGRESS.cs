using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class ASSETS_IN_PROGRESS
    {
        private List<SV_ASSET_CONSTRUCTION> _source;
        public ASSETS_IN_PROGRESS(ObjectSet<ASSET_IN_PROGRESS_HEAD> source)
        {
            _source = source
                .Include("ASSET_IN_PROGRESS_DETAIL")
                .Include("ASSET_IN_PROGRESS_DETAIL.CURRENCY")
                .Include("ZONE")
                .Include("APROVAL_STATES")
                .ToList()
                .ConvertAll(s => (SV_ASSET_CONSTRUCTION)s); 
        }

        public decimal TotalYen(int batch_id)
        {
            var values = _source.Where(obc => obc.batch_id == batch_id)
                .Select(obc => 
                            obc.values
                                .Where(d => d.currency.code == "YEN")
                                .Select(d => d.amount)
                                .First()
                    ).ToList();
            //var total = values.Sum(
            //        val => val.Where(d => d.currency.code == "YEN")
            //            .Select(d => d.amount)
            //            .DefaultIfEmpty(0)
            //            .Sum()
                    //);
            var total = values.Sum();
            return total;
        }
    }
}
