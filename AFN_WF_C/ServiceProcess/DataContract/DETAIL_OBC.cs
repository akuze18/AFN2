using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.DataContract
{
    public class DETAIL_OBC : IElemento
    {
        public GENERIC_VALUE moneda;
        public int codMov;
        public string description;
        public DateTime txFecha;
        public DateTime glFecha;
        public GENERIC_VALUE zona;
        public double monto;
        public double ocupado;
        public double saldo{ get { return monto-ocupado; } }



        public object Item(int index)
        {
            switch (index)
            {
                case 0: return moneda;
                case 1: return codMov;
                case 2: return description;
                case 3: return txFecha;
                case 4: return glFecha;
                case 5: return zona;
                case 6: return monto;
                case 7: return ocupado;
                case 8: return saldo;
                default:
                    return null;
            }
        }
    }
}
