using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.DataContract
{
    public partial class SYSTEM
    {
        public override string ToString()
        {
            return this.ENVIORMENT.name + " " + this.CURRENCY.code;
        }
        public static bool operator ==(SYSTEM a, SYSTEM b)
        {
            return a.id == b.id;
        }
        public static bool operator !=(SYSTEM a, SYSTEM b)
        {
            return a.id != b.id;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType()) return false;

            SYSTEM p = (SYSTEM)obj;
            return (this.id == p.id);
        }
        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }
    }
}
