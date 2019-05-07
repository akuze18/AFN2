using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.DataContract
{
    public class GENERIC_VALUE
    {
        public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string type { get; set; }

    #region Constructors
        public GENERIC_VALUE() { }
        public GENERIC_VALUE(ZONE z)
        {
            if (z != null)
            {
                this.id = z.id;
                this.code = z.codDept;
                this.description = z.name;
                this.type = z.GetType().ToString();
            }
        }
        public GENERIC_VALUE(KIND k)
        {
            if (k != null)
            {
                this.id = k.id;
                this.code = k.cod;
                this.description = k.descrip;
                this.type = k.GetType().ToString();
            }
        }
        public GENERIC_VALUE(VALIDATY v)
        {
            if (v != null)
            {
                this.id = v.id;
                this.code = v.name;
                this.description = v.name;
                this.type = v.GetType().ToString();
            }
        }
        public GENERIC_VALUE(CATEGORY c)
        {
            if (c != null)
            {
                this.id = c.id;
                this.code = c.code;
                this.description = c.descrip;
                this.type = c.GetType().ToString();
            }
        }
        public GENERIC_VALUE(SUBZONE sz)
        {
            if (sz != null)
            {
                this.id = sz.id;
                this.code = sz.codPlace;
                this.description = sz.descrip;
                this.type = sz.GetType().ToString();
            }
        }
        public GENERIC_VALUE(SUBKIND sk)
        {
            if (sk != null)
            {
                this.id = sk.id;
                this.code = sk.code;
                this.description = sk.descrip;
                this.type = sk.GetType().ToString();
            }
        }
        public GENERIC_VALUE(MANAGEMENT m)
        {
            if (m != null)
            {
                this.id = m.id;
                this.code = m.code;
                this.description = m.name;
                this.type = m.GetType().ToString();
            }
        }
        public GENERIC_VALUE(ORIGIN o)
        {
            if (o != null)
            {
                this.id = o.id;
                this.code = o.code;
                this.description = o.descrip;
                this.type = o.GetType().ToString();
            }
        }
        public GENERIC_VALUE(APROVAL_STATE ap_s)
        {
            if (ap_s != null)
            {
                this.id = ap_s.id;
                this.code = ap_s.code;
                this.description = ap_s.descrip;
                this.type = ap_s.GetType().ToString();
            }
        }
        public GENERIC_VALUE(TYPE_ASSET t_as)
        {
            if (t_as != null)
            {
                this.id = t_as.id;
                this.code = t_as.id.ToString();
                this.description = t_as.descrip;
                this.type = t_as.GetType().ToString();
            }
        }
    #endregion

    #region convertions
        public static implicit operator GENERIC_VALUE(ZONE z) {
            var me = new GENERIC_VALUE();
            if (z != null)
            {
                me.id = z.id;
                me.code = z.codDept;
                me.description = z.name;
                me.type = z.GetType().ToString();
            }
            return me;
        }
        public static implicit operator GENERIC_VALUE(KIND k)
        {
            var me = new GENERIC_VALUE();
            if (k != null)
            {
                me.id = k.id;
                me.code = k.cod;
                me.description = k.descrip;
                me.type = k.GetType().ToString();
            }
            return me;
        }
        public static implicit operator GENERIC_VALUE(VALIDATY v)
        {
            var me = new GENERIC_VALUE();
            if (v != null)
            {
                me.id = v.id;
                me.code = v.name;
                me.description = v.name;
                me.type = v.GetType().ToString();
            }
            return me;
        }
        public static implicit operator GENERIC_VALUE(CATEGORY c)
        {
            var me = new GENERIC_VALUE();
            if (c != null)
            {
                me.id = c.id;
                me.code = c.code;
                me.description = c.descrip;
                me.type = c.GetType().ToString();
            }
            return me;
        }
        public static implicit operator GENERIC_VALUE(SUBZONE sz)
        {
            var me = new GENERIC_VALUE();
            if (sz != null)
            {
                me.id = sz.id;
                me.code = sz.codPlace;
                me.description = sz.descrip;
                me.type = sz.GetType().ToString();
            }
            return me;
        }
        public static implicit operator GENERIC_VALUE(SUBKIND sk)
        {
            var me = new GENERIC_VALUE();
            if (sk != null)
            {
                me.id = sk.id;
                me.code = sk.code;
                me.description = sk.descrip;
                me.type = sk.GetType().ToString();
            }
            return me;
        }
        public static implicit operator GENERIC_VALUE(MANAGEMENT m)
        {
            var me = new GENERIC_VALUE();
            if (m != null)
            {
                me.id = m.id;
                me.code = m.code;
                me.description = m.name;
                me.type = m.GetType().ToString();
            }
            return me;
        }
        public static implicit operator GENERIC_VALUE(ORIGIN o)
        {
            var me = new GENERIC_VALUE();
            if (o != null)
            {
                me.id = o.id;
                me.code = o.code;
                me.description = o.descrip;
                me.type = o.GetType().ToString();
            }
            return me;
        }
        public static implicit operator GENERIC_VALUE(APROVAL_STATE ap_s)
        {
            var me = new GENERIC_VALUE();
            if (ap_s != null)
            {
                me.id = ap_s.id;
                me.code = ap_s.code;
                me.description = ap_s.descrip;
                me.type = ap_s.GetType().ToString();
            }
            return me;
        }
        public static implicit operator GENERIC_VALUE(TYPE_ASSET t_as)
        {
            var me = new GENERIC_VALUE();
            if (t_as != null)
            {
                me.id = t_as.id;
                me.code = t_as.id.ToString();
                me.description = t_as.descrip;
                me.type = t_as.GetType().ToString();
            }
            return me;
        }

    #endregion
    }
}
