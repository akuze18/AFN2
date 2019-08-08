﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class KINDS
    {
        private List<KIND> _source;
        public KINDS(ObjectSet<KIND> source) { _source = source.ToList(); }
        public KINDS(List<KIND> source) { _source = source; }

        public GENERIC_VALUE ById(int idFind)
        {
            return _source.Where(k => k.id == idFind).FirstOrDefault();
        }

        public List<GENERIC_VALUE> SearchList()
        {
            return _source.Where(k => k.show_rep == true).ToList().ConvertAll(x => (GENERIC_VALUE)x);
        }

        public GENERIC_VALUE ByCode(string codeFind)
        {
            codeFind = codeFind.TrimEnd();
            return _source.Where(k => k.cod.TrimEnd() == codeFind).FirstOrDefault();
        }

        public GENERIC_VALUE Activos
        {
            get { return _source.Where(k => k.cod == "10").FirstOrDefault(); }
        }
        public GENERIC_VALUE Intagibles
        {
            get { return _source.Where(k => k.cod == "20").FirstOrDefault(); }
        }

        public GENERIC_VALUE OBC
        {
            get { return _source.Where(k => k.cod == "18").FirstOrDefault(); }
        }
    }
}
