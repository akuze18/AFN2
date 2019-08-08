﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class APROVALS_STATES
    {
        private List<APROVAL_STATE> _source;
        public APROVALS_STATES(ObjectSet<APROVAL_STATE> source) { _source = source.ToList(); }
        public APROVALS_STATES(List<APROVAL_STATE> source) { _source = source; }

        public GENERIC_VALUE ById(int idFind)
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
    }
}
