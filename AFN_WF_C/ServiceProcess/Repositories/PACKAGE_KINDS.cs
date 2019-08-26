using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class PACKAGE_KINDS
    {
        private List<PACKAGE_KIND> _source;
        public PACKAGE_KINDS(ObjectSet<PACKAGE_KIND> source) { _source = source.Include("PACKAGE_PAIR_KINDS").ToList(); }

        public List<GENERIC_RELATED> All()
        {
            return _source.ConvertAll(pk => (GENERIC_RELATED)pk).ToList();
        }

        public List<GENERIC_RELATED> ByType(int type_id)
        {
            return _source.Where(pk => pk.type_asset_id == type_id)
                .ToList()
                .ConvertAll(pk => (GENERIC_RELATED)pk);
        }
    }
}
