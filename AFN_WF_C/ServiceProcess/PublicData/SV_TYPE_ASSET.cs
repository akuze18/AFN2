using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_TYPE_ASSET
    {
        private int _id;
        private string _descrip;

        public int id { get { return _id; } }
        public string descrip { get { return _descrip; } }

        #region Convertions
        public static implicit operator SV_TYPE_ASSET(DataContract.TYPE_ASSET od)
        {
            return new SV_TYPE_ASSET()
            {
                _id = od.id,
                _descrip = od.descrip,
            };
        }
        public static implicit operator GENERIC_VALUE(SV_TYPE_ASSET sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.id.ToString(),
                description = sv.descrip,
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion
    }
}
