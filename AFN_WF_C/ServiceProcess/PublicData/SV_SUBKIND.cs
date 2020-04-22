using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_SUBKIND
    {
        private int _id;
        private string _code;
        private string _descrip;
        private int _vu_sug;
        private bool _display;
        private int? _kind_id;

        public int id { get { return _id; } }
        public string code { get { return _code; } }
        public string descrip { get { return _descrip; } }
        public int vu_sug { get { return _vu_sug; } }
        public bool display { get { return _display; } }
        public int? kind_id { get { return _kind_id; } }

        #region Convertions
        public static implicit operator SV_SUBKIND(DataContract.SUBKIND od)
        {
            return new SV_SUBKIND()
            {
                _id = od.id,
                _code = od.code,
                _descrip = od.descrip,
                _vu_sug = od.vu_sug,
                _display = od.display,
                _kind_id = od.kind_id,

            };
        }
        public static implicit operator GENERIC_VALUE(SV_SUBKIND sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.code,
                description = sv.descrip,
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion
        public override string ToString()
        {
            return this._descrip;
        }
    }
}
