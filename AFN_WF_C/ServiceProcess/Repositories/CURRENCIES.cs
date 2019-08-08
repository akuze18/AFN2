﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class CURRENCIES
    {
        private List<CURRENCY> _source;

        public CURRENCIES(ObjectSet<CURRENCY> source) { _source = source.ToList(); }

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(c => c.id == idFind).FirstOrDefault();
        }

        public List<GENERIC_VALUE> All()
        {
            return _source.ConvertAll(c => (GENERIC_VALUE)c);
        }


    }
}
