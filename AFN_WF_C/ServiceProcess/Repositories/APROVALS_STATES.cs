using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class APROVALS_STATES
    {
        private List<SV_APROVAL_STATE> _source;
        public APROVALS_STATES(ObjectSet<APROVAL_STATE> source) { _source = source.ToList().ConvertAll(ap => (SV_APROVAL_STATE)ap); }
        //public APROVALS_STATES(List<APROVAL_STATE> source) { _source = source; }

        public SV_APROVAL_STATE ById(int idFind)
        {
            return _source.Where(ap => ap.id == idFind).FirstOrDefault();
        }

        public GENERIC_VALUE ByCode(string codeFind)
        {
            return _source.Where(ap => ap.code == codeFind).FirstOrDefault();
        }

        public List<GENERIC_VALUE> OnlyActive
        {
            get
            {
                return _source.Where(ap => ap.code == "CLOSE").ToList()
                    .ConvertAll(ap => (GENERIC_VALUE)ap);
            }
        }

        public List<GENERIC_VALUE> OnlyDigited {
            get {
                return _source.Where(ap => ap.code == "OPEN").ToList()
                    .ConvertAll(ap => (GENERIC_VALUE)ap);
            }
        }

        public List<GENERIC_VALUE> NoDeleted
        {
            get
            {
                return _source.Where(ap => ap.code == "OPEN" || ap.code=="CLOSE").ToList()
                    .ConvertAll(ap => (GENERIC_VALUE)ap);
            }
        }

        public List<GENERIC_VALUE> All
        {
            get
            {
                return _source.ConvertAll(ap => (GENERIC_VALUE)ap);
            }
        }

        public List<GENERIC_VALUE> Default {
            get { return this.OnlyActive; }
        }

        public GENERIC_VALUE CLOSE
        {
            get
            {
                return _source.Where(ap => ap.code == "CLOSE").First();
            }
        }
        public GENERIC_VALUE DELETE
        {
            get
            {
                return _source.Where(ap => ap.code == "DELETE").First();
            }
        }
        public GENERIC_VALUE OPEN
        {
            get
            {
                return _source.Where(ap => ap.code == "OPEN").First();
            }
        }

        public string[] ArrNoDeleted
        {
            get { return NoDeleted.Select(e => e.code).ToArray(); }
        }
        public string[] ArrOnlyActive
        {
            get { return OnlyActive.Select(e => e.code).ToArray(); }
        }
    }
}
