using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_APROVAL_STATE
    {
        private int _id;
        private string _code;
        private string _descrip;

        public int id { get { return _id; } }
        public string code { get { return _code; } }
        public string descrip { get { return _descrip; } }

        #region Convertions
        public static implicit operator SV_APROVAL_STATE(DataContract.APROVAL_STATE od)
        {
            return new SV_APROVAL_STATE()
            {
                _id = od.id,
                _code = od.code,
                _descrip = od.descrip,
            };
        }
        public static implicit operator GENERIC_VALUE(SV_APROVAL_STATE sv)
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
            return this._code;
        }
    }
}
