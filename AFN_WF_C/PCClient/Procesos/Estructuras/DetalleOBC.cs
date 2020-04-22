using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Procesos.Estructuras
{
    public class DetalleOBC
    {
        public int codigo { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
        public PD.GENERIC_VALUE zona { get; set; }
        public decimal saldo { get; set; }
        //TBSaldo.Columns.Add("Codigo")
        //TBSaldo.Columns.Add("Descripción o Referencia")
        //TBSaldo.Columns.Add("Fecha", Type.GetType("System.DateTime"))
        //TBSaldo.Columns.Add("Zona", Type.GetType("System.String"))
        //TBSaldo.Columns.Add("Saldo", Type.GetType("System.Int32"))
        //private int aprovalId;
        private int? id;
        private decimal maximo;

        public DetalleOBC()
        {
            //aprovalId = 2;
            id = null;
            maximo = 0;
        }
        //public void aprovalSet(int valor) { aprovalId = valor; }
        //public int aprovalGet() { return aprovalId; }

        public void idSet(int? valor) { id = valor; }
        public int? idGet() { return id; }

        public void maximoSet(decimal valor) { maximo = valor; }
        public decimal maximoGet() { return maximo; }
    }
}
