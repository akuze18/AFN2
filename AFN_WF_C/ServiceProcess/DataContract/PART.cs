using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.DataContract
{
    public partial class PART
    {
        public override string ToString()
        {
            return "(" + this.part_index.ToString() +") Cantidad : "+this.quantity.ToString() ;
        }
    }
}
