using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.HelperServiceAFN
{
    public class VIGENCIA
    {
        public List<GENERIC_VALUE> All()
        {
            using (AFN2Entities context = new AFN2Entities())
            using (Repositories.Main repo = new Repositories.Main(context))
            {
                var AllValid = repo.validaciones.All();
                return AllValid;
            }
        }
        public List<GENERIC_VALUE> Downs()
        {
            using (AFN2Entities context = new AFN2Entities())
            using (Repositories.Main repo = new Repositories.Main(context))
            {
                var DownsValid = repo.validaciones.Downs();
                return DownsValid;
            }
        }

        public List<GENERIC_VALUE> Ups()
        {
            using (AFN2Entities context = new AFN2Entities())
            using (Repositories.Main repo = new Repositories.Main(context))
            {
                var UpsValid = repo.validaciones.Ups();
                return UpsValid;
            }
        }

        public List<GENERIC_VALUE> SearchDownsList()
        {
            var resulta = Downs();
            resulta.Insert(0, new GENERIC_VALUE() { id = 0, type = "OPTION", description = "TODOS" });
            return resulta;
        }
    }
}
