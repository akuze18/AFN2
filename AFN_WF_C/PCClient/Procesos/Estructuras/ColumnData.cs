using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.PCClient.Procesos.Estructuras
{
    internal class ColumnData
    {
        public object[,] Valores { get; set; }
        public Type ColType { get; set; }
        public string TName { get { if (ColType == null) return string.Empty; else return ColType.Name; } }

    }
}
