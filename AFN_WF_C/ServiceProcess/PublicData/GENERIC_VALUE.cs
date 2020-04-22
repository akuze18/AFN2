using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class GENERIC_VALUE : IComparable<GENERIC_VALUE>
    {
        public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string type { 
            get {
                return _type;
            } 
            set {
                _type = value;
                if (_OnlyCode.Contains(_type))
                {
                    _display = TYPE_DISPLAY.OnlyCode;
                }
                else if (_CodeDesc.Contains(_type)) {
                    _display = TYPE_DISPLAY.Code_Description;
                }
                else{
                    _display = TYPE_DISPLAY.OnlyDescription;
                }
            } 
        }

        private TYPE_DISPLAY _display;
        private string _type;
        private string[] _OnlyCode = new string[] { "APROVAL_STATE", "CURRENCY" };
        private string[] _CodeDesc = new string[] {  };
        //default: ZONE, KIND

        public enum TYPE_DISPLAY { 
            OnlyDescription,
            OnlyCode,
            Code_Description
        }

    #region Constructors
        public GENERIC_VALUE() {
            this.id = 0;
            this.code = "0";
            this.description = string.Empty;
            this.type = string.Empty;
        }
        public GENERIC_VALUE(int id, string description, string type) {
            this.id = id;
            this.code = id.ToString();
            this.description = description;
            this.type = type;
        }
        public static GENERIC_VALUE Empty
        {
            get { return new GENERIC_VALUE(); }
        }
        public static GENERIC_VALUE EmptyText
        {
            get { return new GENERIC_VALUE() { id = 0, code = "", description = string.Empty, type = string.Empty }; }
        }
    #endregion

    #region Convertions
        public static implicit operator GENERIC_VALUE(DataContract.ARTICLE a)
        {
            var me = new GENERIC_VALUE();
            if (a != null)
            {
                me.id = a.id;
                me.code = a.code;
                me.description = a.code;
                me.type = a.GetType().Name;
            }
            return me;
        }
        public static implicit operator GENERIC_VALUE(DataContract.TYPE_ACCOUNT a)
        {
            var me = new GENERIC_VALUE();
            if (a != null)
            {
                me.id = a.id;
                me.code = a.id.ToString();
                me.description = a.name;
                me.type = a.GetType().Name;
            }
            return me;
        }
        public static implicit operator GENERIC_VALUE(DataContract.GROUP_ACCOUNT g)
        {
            var me = new GENERIC_VALUE();
            if (g != null)
            {
                me.id = g.id;
                me.code = g.id.ToString();
                me.description = g.name;
                me.type = g.GetType().Name;
            }
            return me;
        }
        //public static implicit operator GENERIC_VALUE(ZONE z) {
        //    var me = new GENERIC_VALUE();
        //    if (z != null)
        //    {
        //        me.id = z.id;
        //        me.code = z.codDept;
        //        me.description = z.name;
        //        me.type = z.GetType().Name;
        //    }
        //    return me;
        //}
        //public static implicit operator GENERIC_VALUE(KIND k)
        //{
        //    var me = new GENERIC_VALUE();
        //    if (k != null)
        //    {
        //        me.id = k.id;
        //        me.code = k.cod;
        //        me.description = k.descrip;
        //        me.type = k.GetType().Name;
        //    }
        //    return me;
        //}
        //public static implicit operator GENERIC_VALUE(VALIDATY v)
        //{
        //    var me = new GENERIC_VALUE();
        //    if (v != null)
        //    {
        //        me.id = v.id;
        //        me.code = v.name;
        //        me.description = v.name;
        //        me.type = v.GetType().Name;
        //    }
        //    return me;
        //}
        //public static implicit operator GENERIC_VALUE(CATEGORY c)
        //{
        //    var me = new GENERIC_VALUE();
        //    if (c != null)
        //    {
        //        me.id = c.id;
        //        me.code = c.code;
        //        me.description = c.descrip;
        //        me.type = c.GetType().Name;
        //    }
        //    return me;
        //}
        //public static implicit operator GENERIC_VALUE(SUBZONE sz)
        //{
        //    var me = new GENERIC_VALUE();
        //    if (sz != null)
        //    {
        //        me.id = sz.id;
        //        me.code = sz.codPlace;
        //        me.description = sz.descrip;
        //        me.type = sz.GetType().Name;
        //    }
        //    return me;
        //}
        //public static implicit operator GENERIC_VALUE(SUBKIND sk)
        //{
        //    var me = new GENERIC_VALUE();
        //    if (sk != null)
        //    {
        //        me.id = sk.id;
        //        me.code = sk.code;
        //        me.description = sk.descrip;
        //        me.type = sk.GetType().Name;
        //    }
        //    return me;
        //}
        //public static implicit operator GENERIC_VALUE(MANAGEMENT m)
        //{
        //    var me = new GENERIC_VALUE();
        //    if (m != null)
        //    {
        //        me.id = m.id;
        //        me.code = m.code;
        //        me.description = m.name;
        //        me.type = m.GetType().Name;
        //    }
        //    return me;
        //}
        //public static implicit operator GENERIC_VALUE(ORIGIN o)
        //{
        //    var me = new GENERIC_VALUE();
        //    if (o != null)
        //    {
        //        me.id = o.id;
        //        me.code = o.code;
        //        me.description = o.descrip;
        //        me.type = o.GetType().Name;
        //    }
        //    return me;
        //}
        //public static implicit operator GENERIC_VALUE(APROVAL_STATE ap_s)
        //{
        //    var me = new GENERIC_VALUE();
        //    if (ap_s != null)
        //    {
        //        me.id = ap_s.id;
        //        me.code = ap_s.code;
        //        me.description = ap_s.descrip;
        //        me.type = ap_s.GetType().Name;
        //    }
        //    return me;
        //}
        //public static implicit operator GENERIC_VALUE(TYPE_ASSET t_as)
        //{
        //    var me = new GENERIC_VALUE();
        //    if (t_as != null)
        //    {
        //        me.id = t_as.id;
        //        me.code = t_as.id.ToString();
        //        me.description = t_as.descrip;
        //        me.type = t_as.GetType().Name;
        //    }
        //    return me;
        //}
        //public static implicit operator GENERIC_VALUE(SITUATION s)
        //{
        //    var me = new GENERIC_VALUE();
        //    if (s != null)
        //    {
        //        me.id = s.id;
        //        me.code = s.id.ToString();
        //        me.description = s.condicion;
        //        me.type = s.GetType().Name;
        //    }
        //    return me;
        //}
        //public static implicit operator GENERIC_VALUE(CURRENCY c)
        //{
        //    var me = new GENERIC_VALUE();
        //    if (c != null)
        //    {
        //        me.id = c.id;
        //        me.code = c.code;
        //        me.description = c.name;
        //        me.type = c.GetType().Name;
        //    }
        //    return me;
        //}
        //public static implicit operator GENERIC_VALUE(PARAMETER p)
        //{
        //    var me = new GENERIC_VALUE();
        //    if (p != null)
        //    {
        //        me.id = p.id;
        //        me.code = p.code;
        //        me.description = p.name;
        //        me.type = p.GetType().Name;
        //    }
        //    return me;
        //}
    #endregion

    #region Boolean Operators
        public static bool operator ==(GENERIC_VALUE a, GENERIC_VALUE b)
        {
            if ((object)a != null && (object)b != null)
                return a.id == b.id && a.type == b.type;
            else
                if ((object)a != null || (object)b != null)
                    return false;
                else
                    return true;
        }
        public static bool operator !=(GENERIC_VALUE a, GENERIC_VALUE b)
        {
            if ((object)a != null && (object)b != null)
                return !(a.id == b.id && a.type == b.type);
            else
                if ((object)a != null || (object)b != null)
                    return true;
                else
                    return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType()) return false;

            GENERIC_VALUE p = (GENERIC_VALUE)obj;
            return (this.id == p.id && this.type == p.type);
        }
        public override int GetHashCode()
        {
            return this.id.GetHashCode()*13+this.type.GetHashCode();
        }
    #endregion

        public override string ToString()
        {
            switch (_display) { 
                case TYPE_DISPLAY.OnlyDescription:
                    return this.description;
                case TYPE_DISPLAY.OnlyCode:
                    return this.code;
                case TYPE_DISPLAY.Code_Description:
                    return this.code + "-" + this.description;
                default:
                    return this.description;
            }
            //return this.description;
        }


        public int CompareTo(GENERIC_VALUE other)
        {
            if (this.Equals(other))
            {
                return 0;
            }
            else
            {
                return string.Compare(other.code,this.code);
            }
        }
   
    }
}
