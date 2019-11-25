using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class GP_PM
    {
        private List<PM00200A> _source;
        public GP_PM(ObjectSet<PM00200A> source)
        {
            _source = source.ToList();
        }

        public List<SV_PROVEEDOR> listar()
        {
            return _source.Where(p => p.FUENTE == "NEW")
                .ToList().ConvertAll(p => (SV_PROVEEDOR)p);
        }

        public List<SV_PROVEEDOR> buscar(string idRut, string nombre)
        {
            return _source
                .Where(p => p.FUENTE == "NEW")
                .Where(p => p.COD.Contains(idRut) || idRut == string.Empty)
                .Where(p => p.VENDNAME.ToUpper().Contains(nombre.ToUpper()) || nombre == string.Empty)
                .ToList().ConvertAll(p => (SV_PROVEEDOR)p);
        }

        public string getNameByCode(string code)
        {
            return _source.Where(p => p.COD == code)
                .Select(p => p.VENDNAME)
                .DefaultIfEmpty("SIN_PROVEED")
                .First();
        }
    }
}
