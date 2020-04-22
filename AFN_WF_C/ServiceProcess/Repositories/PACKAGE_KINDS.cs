using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class PACKAGE_KINDS
    {
        private List<SV_PACKAGE_KIND> _source;
        public PACKAGE_KINDS(ObjectSet<PACKAGE_KIND> source) 
        { 
            _source = source
                .Include("PACKAGE_PAIR_KINDS")
                .ToList()
                .ConvertAll(pk => (SV_PACKAGE_KIND)pk); }

        public List<GENERIC_RELATED> All()
        {
            return _source.ConvertAll(pk => (GENERIC_RELATED)pk).ToList();
        }

        public List<SV_PACKAGE_KIND> ByType(int type_id)
        {
            return _source.Where(pk => pk.type_asset_id == type_id)
                .ToList();
                //.ConvertAll(pk => (GENERIC_RELATED)pk);
        }
    }
}
