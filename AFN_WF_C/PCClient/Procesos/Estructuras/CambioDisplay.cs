using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Procesos.Estructuras
{
    public class CambioDisplay
    {
        public int codigo_articulo { get; set; }
        public DateTime fecha_proceso { get; set; }
        public int cantidad_proceso { get; set; }
        public PD.GENERIC_VALUE zona { get; set; }
        public PD.GENERIC_VALUE subzona { get; set; }
        public int indice { get; set; }
        public string descripcion { get; set; }
        public int parte { get; set; }
        public List<DetalleArticulo> detalle { get; set; }
    }
}
