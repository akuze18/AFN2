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

        private string FormatType(string tipo)
        {
            switch (tipo)
            {
                case "Int32":
                case "Int16":
                    return "###0;[red]-###0";
                case "DateTime":
                    return "dd-MM-yyyy";
                case "String":
                case "GENERIC_VALUE":
                    return "@";
                case "Decimal":
                case "Double":
                case "Int64":
                    return "#,##0;[red]-#,##0";
                default:
                    return "@";
            }
        }
        
        public string Formating{ get { return FormatType(TName); } }
        
    }
}
