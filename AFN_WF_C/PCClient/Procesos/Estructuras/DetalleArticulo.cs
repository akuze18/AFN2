using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.PCClient.Procesos.Estructuras
{
    public class DetalleArticulo
    {
        public string producto { get; set; }
        public int codigoLote { get; set; }
        public int parteLote { get; set; }
        public bool procesar { get; set; }
        public int rowId { get; set; }
        public int PartId { get; set; }
    }
}
