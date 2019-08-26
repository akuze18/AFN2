using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.DataView
{
    class SV_KIND
    {
        private int _id;
        private string _cod;
        private string _descrip;
        private bool _active;
        private int _type_asset_id;
        private bool _show_in;
        private bool _show_rep;

        public int id { get { return _id; } }
        public string cod { get { return _cod; } }
        public string descrip { get { return _descrip; } }
        public bool active { get { return _active; } }
        public int type_asset_id { get { return _type_asset_id; } }
        public bool show_in { get { return _show_in; } }
        public bool show_rep { get { return _show_rep; } }


        #region Convertions
        public static implicit operator SV_KIND(C.KIND od)
        {
            return new SV_KIND()
            {
                _id = od.id,
                _cod = od.cod,
                _descrip = od.descrip,
                _active = od.active,
                _type_asset_id = od.type_asset_id,
                _show_in = od.show_in,
                _show_rep= od.show_rep
            };
        }
        public static implicit operator C.GENERIC_VALUE(SV_KIND sv)
        {
            return new C.GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.cod,
                description = sv.descrip,
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion

    }
}
