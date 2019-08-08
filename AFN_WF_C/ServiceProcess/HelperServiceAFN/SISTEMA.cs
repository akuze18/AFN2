using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.HelperServiceAFN
{
    public class SISTEMA
    {
        public List<SYSTEM> All() {
            using (AFN2Entities context = new AFN2Entities())
            using (Repositories.Main repo = new Repositories.Main(context))
            {
                var AllSystem = repo.sistemas.All();
                return AllSystem;
            }
        }
        public SYSTEM ByCodes(string codeEnv, string codeCurr)
        {
            using (AFN2Entities context = new AFN2Entities())
            using (Repositories.Main repo = new Repositories.Main(context))
            {
                var S1 = repo.sistemas.ByCodes(codeEnv, codeCurr);
                return S1;
            }
        }

        //public SYSTEM Default() {
        //    using (AFN2Entities context = new AFN2Entities())
        //    using (Repositories.Main repo = new Repositories.Main(context))
        //    {
        //        var SDef = repo.sistemas.Default;
        //        return SDef;
        //    }
        //}
    }
}
