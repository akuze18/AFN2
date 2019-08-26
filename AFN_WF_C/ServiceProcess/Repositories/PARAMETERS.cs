using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class PARAMETERS
    {
        private List<PARAMETER> _source;
        public PARAMETERS(ObjectSet<PARAMETER> source) { _source = source.ToList(); }
        //public PARAMETERS(List<PARAMETER> source) { _source = source; }

        public PARAMETER ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
        public PARAMETER byCode(string codeFind)
        {
            return (from p in _source where p.code == codeFind select p).First();
        }

        public PARAMETER PrecioBase
        {
            get { return byCode("PB"); }
        }
        public PARAMETER DepreciacionAcum
        {
            get { return byCode("DA"); }
        }
        public PARAMETER Deterioro
        {
            get { return byCode("DT"); }
        }
        public PARAMETER ValorResidual
        {
            get { return byCode("VR"); }
        }
        public PARAMETER VidaUtil
        {
            get { return byCode("VUB"); }
        }
        public PARAMETER Credito
        {
            get { return byCode("CRED"); }
        }

        public PARAMETER Preparacion
        {
            get { return byCode("PREP"); }
        }
        public PARAMETER Transporte
        {
            get { return byCode("TRAN"); }
        }
        public PARAMETER Montaje
        {
            get { return byCode("MON"); }
        }
        public PARAMETER Desmantelamiento
        {
            get { return byCode("DESM"); }
        }
        public PARAMETER Honorario
        {
            get { return byCode("HON"); }
        }
        public PARAMETER Revalorizacion
        {
            get { return byCode("REVAL"); }
        }
    }
}
