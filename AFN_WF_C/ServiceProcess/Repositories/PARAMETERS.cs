using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class PARAMETERS
    {
        private List<SV_PARAMETER> _source;
        public PARAMETERS(ObjectSet<PARAMETER> source) { _source = source.ToList().ConvertAll(p => (SV_PARAMETER)p); }

        public SV_PARAMETER ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
        public SV_PARAMETER byCode(string codeFind)
        {
            return (from p in _source where p.code == codeFind select p).First();
        }

        public SV_PARAMETER PrecioBase
        {
            get { return byCode("PB"); }
        }
        public SV_PARAMETER DepreciacionAcum
        {
            get { return byCode("DA"); }
        }
        public SV_PARAMETER Deterioro
        {
            get { return byCode("DT"); }
        }
        public SV_PARAMETER ValorResidual
        {
            get { return byCode("VR"); }
        }
        public SV_PARAMETER VidaUtil
        {
            get { return byCode("VUB"); }
        }
        public SV_PARAMETER Credito
        {
            get { return byCode("CRED"); }
        }

        public SV_PARAMETER Preparacion
        {
            get { return byCode("PREP"); }
        }
        public SV_PARAMETER Transporte
        {
            get { return byCode("TRAN"); }
        }
        public SV_PARAMETER Montaje
        {
            get { return byCode("MON"); }
        }
        public SV_PARAMETER Desmantelamiento
        {
            get { return byCode("DESM"); }
        }
        public SV_PARAMETER Honorario
        {
            get { return byCode("HON"); }
        }
        public SV_PARAMETER Revalorizacion
        {
            get { return byCode("REVAL"); }
        }

        public List<SV_PARAMETER> ForIFRS()
        {
            string[] IFRSCodes = {"PB", "DESM", "HON", "MON", "PREP","TRAN"};
            return _source.Where(p => IFRSCodes.Contains(p.code))
                .ToList();
        }
    }
}
