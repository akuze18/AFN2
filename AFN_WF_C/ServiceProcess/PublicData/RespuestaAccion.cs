using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using P = AFN_WF_C.PCClient.Procesos;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class RespuestaAccion
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
        public List<GENERIC_VALUE> result_objs { get; set; }

        public RespuestaAccion()
        {
            result_objs = new List<GENERIC_VALUE>();
        }

        public void AddResultObj(int id, Type typeObj)
        {
            string tipoVal;
            if(typeObj.Name.Contains("SV_"))
                tipoVal = typeObj.Name.Substring(2,typeObj.Name.Length);
            else
                tipoVal = typeObj.Name;
            result_objs.Add(new GENERIC_VALUE(id, string.Empty, tipoVal));
        }

        public void set(int new_codigo, string new_description)
        {
            _codigo = new_codigo;
            _descripcion = new_description;
        }

        public void set_ok()
        {
            _codigo = 1;
            _descripcion = "OK";
        }

        public bool CheckError { get { return codigo < 0; } }
        public void mensaje()
        {
            if (CheckError)
                P.Mensaje.Error(descripcion);
            //else
            //    P.Mensaje.Error(descripcion);
        }

    }
}
