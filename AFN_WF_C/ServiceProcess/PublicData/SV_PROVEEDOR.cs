using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_PROVEEDOR
    {
        public string FUENTE { get; set; }
        public string COD { get; set; }
        public string PARVENDID { get; set; }
        public string VENDNAME { get; set; }
        public string TEXTO { get; set; }
        public string VNDCLSID { get; set; }
        #region Convertions
        public static implicit operator SV_PROVEEDOR(DataContract.PM00200A od)
        {
            return new SV_PROVEEDOR()
            {
                FUENTE = od.FUENTE,
                COD = od.COD,
                PARVENDID = od.PARVENDID,
                VENDNAME = od.VENDNAME,
                TEXTO = od.TEXTO,
                VNDCLSID = od.VNDCLSID,
            };
        }
        #endregion

        public override string ToString()
        {
            return this.TEXTO;
        }

        #region Operadores
        public static bool operator ==(SV_PROVEEDOR a, string b)
        {
            return a.COD == b;
        }
        public static bool operator !=(SV_PROVEEDOR a, string b)
        {
            return a.COD != b;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if(obj.GetType() == typeof (string))
                return this.COD == (string)obj;

            if (this.GetType() != obj.GetType()) 
                return false;

            SV_PROVEEDOR p = (SV_PROVEEDOR)obj;
            return (this.COD == p.COD);

        }
        public override int GetHashCode()
        {
            return this.COD.GetHashCode();
        }
        #endregion
    }
}
