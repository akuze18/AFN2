using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class TRANSACTIONS_PARAM_DET
    {
        private List<SV_TRANSACTION_PARAMETER_DETAIL> _source;
        public TRANSACTIONS_PARAM_DET(ObjectSet<TRANSACTION_PARAMETER_DETAIL> source)
        { 
            _source = source
                .Include("PARAMETER")
                .ToList()
                .ConvertAll(c => (SV_TRANSACTION_PARAMETER_DETAIL)c); 
        }
        public TRANSACTIONS_PARAM_DET(ObjectSet<TRANSACTION_PARAMETER_DETAIL> source, int SystemId, int[] Heads)
        { 
            _source = source
                .Include("PARAMETER")
                .Where(tpd => tpd.system_id == SystemId && Heads.Contains(tpd.trx_head_id))
                .ToList()
                .ConvertAll(c => (SV_TRANSACTION_PARAMETER_DETAIL)c); 
        }

        public List<PARAM_VALUE> ByHead_Sys(int HeadId, int SysId)
        {
            var myParams = _source.Where(x => x.trx_head_id == HeadId && x.system_id == SysId).ToList();
            return myParams.ConvertAll(x => (PARAM_VALUE)x);
        }
        public List<SV_TRANSACTION_PARAMETER_DETAIL> ByHead(int HeadId)
        {
            return _source.Where(x => x.trx_head_id == HeadId).ToList();
        }
        public List<SV_TRANSACTION_PARAMETER_DETAIL> ByHeadSys(int HeadId, SV_SYSTEM system)
        {
            return _source.Where(x => x.trx_head_id == HeadId && x.system_id == system.id).ToList();
        }

        private int param_ifrs_order(string param_code)
        {
            switch (param_code)
            {
                case "PREP": return 1;
                case "DESM": return 2;
                case "TRAN": return 3;
                case "MON": return 4;
                case "HON": return 5;
                default: return 0;
            }
        }
        public List<T_CUADRO_IFRS> CUADRO_INGRESO_IFRS(int trx_head_id, SV_PARAMETER[] ifrs_param, SV_SYSTEM[] ifrs_systems)
        {
            List<T_CUADRO_IFRS> result = new List<T_CUADRO_IFRS>();
            foreach (SV_PARAMETER param in ifrs_param)
            {
                T_CUADRO_IFRS nuevo = new T_CUADRO_IFRS();
                nuevo.orden = param_ifrs_order(param.code);
                nuevo.Parametro = param.name;
                foreach (SV_SYSTEM system in ifrs_systems)
                {
                    var item = _source.Where(p => p.paratemer_id == param.id &&
                        p.system_id == system.id &&
                        p.trx_head_id == trx_head_id).FirstOrDefault();

                    decimal valor = ((item == null) ? 0 : item.parameter_value);
 
                    if (system.CURRENCY == "CLP")
                        nuevo.CLP = valor;
                    if (system.CURRENCY == "YEN")
                        nuevo.YEN = valor;
                }
                result.Add(nuevo);
            }
            return result.OrderBy(r => r.orden).ToList();
        }
    }
}
