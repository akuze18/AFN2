using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class ACCOUNTING
    {
        private List<TYPE_ACCOUNT> _source1;
        private List<GROUP_ACCOUNT> _source2;
        private List<DETAIL_ACCOUNT_LINES> _source3;
        private List<CONTAB_GRUPO> _source4;

        public ACCOUNTING(ObjectSet<TYPE_ACCOUNT> source1, ObjectSet<GROUP_ACCOUNT> source2,
            ObjectSet<DETAIL_ACCOUNT_LINES> source3,
            ObjectSet<CONTAB_GRUPO> source4) 
        {
            _source1 = source1.ToList();    //.ConvertAll(ap => (SV_APROVAL_STATE)ap)
            _source2 = source2.ToList();
            _source3 = source3.ToList();
            _source4 = source4.ToList();
        }


        public List<GROUP_ACCOUNT> grupos()
        {
            return _source2;
        }

        public List<DETAIL_ACCOUNT_LINES> lineas()
        {
            return _source3;
        }

        public List<CONTAB_GRUPO> grupos_contables()
        {
            return _source4;
        }
    }
}
