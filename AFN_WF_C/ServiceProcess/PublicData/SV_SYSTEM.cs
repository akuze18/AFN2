using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_SYSTEM
    {
        private int _id;
        //private int _enviorment_id;
        //private int _currency_id;
        private SV_ENVIORMENT _enviorment;
        private SV_CURRENCY _currency;

        public int id { get { return _id; } }
        public SV_ENVIORMENT ENVIORMENT { get { return _enviorment; } }
        public SV_CURRENCY CURRENCY { get { return _currency; } }

        #region Convertions
        public static implicit operator SV_SYSTEM(C.SYSTEM od)
        {
            return new SV_SYSTEM()
            {
                _id = od.id,
                //_enviorment_id = od.enviorment_id,
                //_currency_id = od.currency_id,
                _enviorment = od.ENVIORMENT,
                _currency = od.CURRENCY,
            };
        }
        public static implicit operator GENERIC_VALUE(SV_SYSTEM sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.ENVIORMENT.code+"-"+sv.CURRENCY.code,
                description = sv.ToString(),
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion

        public override string ToString()
        {
            return this.ENVIORMENT.name + " " + this.CURRENCY.code;
        }
        public static bool operator ==(SV_SYSTEM a, SV_SYSTEM b)
        {
            return a.id == b.id;
        }
        public static bool operator !=(SV_SYSTEM a, SV_SYSTEM b)
        {
            return a.id != b.id;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType()) return false;

            SV_SYSTEM p = (SV_SYSTEM)obj;
            return (this.id == p.id);
        }
        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

    }
}
