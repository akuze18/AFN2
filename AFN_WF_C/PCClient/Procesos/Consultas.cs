using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

using ServAFN = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.PCClient.Procesos
{
    internal class consultas
    {
        public static void depreciar()
        {
            var cServ = new ServiceProcess.ServiceAFN();
            ServAFN.SYSTEM S1 = cServ.System_Get("FIN", "CLP");
            DateTime fecha = new DateTime(2019, 1, 31);
            var x = cServ.base_movimiento(S1.id, fecha);
            var y = (ServAFN.DETAIL_PROCESS)x;
            //MessageBox.Show(S1.ToString());
            //MessageBox.Show(x.ToString());
            MessageBox.Show(y.zona.description);
        }
    }
}
