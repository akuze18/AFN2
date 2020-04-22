using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_CURRENCY
    {
        private int _id;
        private string _code;
        private string _name;

        public int id { get { return _id; } }
        public string code { get { return _code; } }
        public string name { get { return _name; } }

        #region Convertions
        public static implicit operator SV_CURRENCY(DataContract.CURRENCY od)
        {
            return new SV_CURRENCY()
            {
                _id = od.id,
                _code = od.code,
                _name = od.name,
            };
        }
        public static implicit operator GENERIC_VALUE(SV_CURRENCY sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.code,
                description = sv.name,
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion

        public static bool operator ==(SV_CURRENCY a, string b)
        {
            return a.code == b;
        }
        public static bool operator !=(SV_CURRENCY a, string b)
        {
            return a.code != b;
        }

        public static bool operator ==(SV_CURRENCY a, SV_CURRENCY b)
        {
            return (a.id == b.id);
        }
        public static bool operator !=(SV_CURRENCY a, SV_CURRENCY b)
        {
            return (a.id != b.id);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() == typeof(string))
                return (this == (string)obj);
            
            if (this.GetType() != obj.GetType()) 
                return false;
            return (this == (SV_CURRENCY)obj);
        }
        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public override string ToString()
        {
            return code;
        }
    }
}
