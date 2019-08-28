using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.DataView;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class SYSTEMS
    {
        private List<SV_SYSTEM> _source;
        public SYSTEMS(ObjectSet<SYSTEM> source) 
        { 
            _source = source
                .Include("ENVIORMENT")
                .Include("CURRENCY")
                .ToList()
                .ConvertAll(s => (SV_SYSTEM)s); 
        }

        public List<SV_SYSTEM> All()
        {
            return _source;
        }
        public SV_SYSTEM ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
        public SV_SYSTEM ByCodes(string codeEnv, string codeCurr)
        {
            var S1 = _source.Where(A => 
                        A.ENVIORMENT.code == codeEnv 
                        && A.CURRENCY.code == codeCurr)
                    .FirstOrDefault();
            return S1;
        }

        public SV_SYSTEM Default
        {
            get
            {
                return this.FinCLP;
            }
        }

        public SV_SYSTEM FinCLP
        {
            get
            {
                return this.ByCodes("FIN","CLP");
            }
        }
        public SV_SYSTEM FinYEN
        {
            get
            {
                return this.ByCodes("FIN", "YEN");
            }
        }
        public SV_SYSTEM TribCLP
        {
            get
            {
                return this.ByCodes("TRIB", "CLP");
            }
        }
        public SV_SYSTEM IfrsCLP
        {
            get
            {
                return this.ByCodes("IFRS", "CLP");
            }
        }
        public SV_SYSTEM IfrsYEN
        {
            get
            {
                return this.ByCodes("IFRS", "YEN");
            }
        }
    }
}
