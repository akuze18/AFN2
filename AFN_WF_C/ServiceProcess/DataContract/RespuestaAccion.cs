using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.DataContract
{
    class RespuestaAccion
    {

        int _codigo = 0;
        string _descripcion = string.Empty;

        public int codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

    }
}
