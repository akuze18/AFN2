using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    class SV_VALIDATY
    {
        private int _id;
        private string _name;

        public int id { get { return _id; } }
        public string name { get { return _name; } }

        #region Convertions
        public static implicit operator SV_VALIDATY(DataContract.VALIDATY od)
        {
            return new SV_VALIDATY()
            {
                _id = od.id,
                _name = od.name,
            };
        }
        public static implicit operator GENERIC_VALUE(SV_VALIDATY sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.name,
                description = sv.name,
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion
    }
}
