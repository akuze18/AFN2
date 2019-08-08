using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class SYSTEMS
    {
        private List<SYSTEM> _source;
        public SYSTEMS(ObjectSet<SYSTEM> source) { _source = source.Include("ENVIORMENT").Include("CURRENCY").ToList(); }
        //public SYSTEMS(List<SYSTEM> source) { _source = source; }

        public List<SYSTEM> All() {
            return _source;
        }
        public SYSTEM ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
        public SYSTEM ByCodes(string codeEnv, string codeCurr)
        {
            var S1 = (from A in _source
                      where A.ENVIORMENT.code == codeEnv && A.CURRENCY.code == codeCurr
                      select A).FirstOrDefault();
            return S1;
        }
        
        public SYSTEM Default
        {
            get
            {
                return this.FinCLP;
            }
        }
        
        public SYSTEM FinCLP
        {
            get
            {
                return this.ByCodes("FIN","CLP");
            }
        }
        public SYSTEM FinYEN
        {
            get
            {
                return this.ByCodes("FIN", "YEN");
            }
        }
        public SYSTEM TribCLP
        {
            get
            {
                return this.ByCodes("TRIB", "CLP");
            }
        }
        public SYSTEM IfrsCLP
        {
            get
            {
                return this.ByCodes("IFRS", "CLP");
            }
        }
        public SYSTEM IfrsYEN
        {
            get
            {
                return this.ByCodes("IFRS", "YEN");
            }
        }
    }
}
