﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class ORIGINS
    {
        private List<SV_ORIGIN> _source;
        public ORIGINS(ObjectSet<ORIGIN> source) 
        { 
            _source = source
                .ToList()
                .ConvertAll(o =>(SV_ORIGIN)o ); 
        }

        public SV_ORIGIN ById(int idFind)
        {
            return _source.Where(o => o.id == idFind).FirstOrDefault();
        }
        public GENERIC_VALUE ByCode(string CodeFind)
        {
            return _source.Where(o => o.code == CodeFind).FirstOrDefault();
        }
    }
}
