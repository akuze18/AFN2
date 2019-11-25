using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public partial class Main
    {
        public RespuestaAccion REGISTER_PURCHASE_PARAM(int[] cabeceras, SV_SYSTEM sistema, SV_PARAMETER parametro, decimal valor, bool withResiduo)
        {
            var res = new RespuestaAccion();
            try
            {
                int dif = 0;    //primer ciclo no hay diferencia
                foreach (var head_id in cabeceras)
                {
                    TRANSACTION_PARAMETER_DETAIL nuevo_param = new TRANSACTION_PARAMETER_DETAIL();
                    nuevo_param.trx_head_id = head_id ;
                    nuevo_param.system_id = sistema.id;
                    nuevo_param.paratemer_id = parametro.id;
                    nuevo_param.parameter_value = valor + dif;

                    _context.TRANSACTIONS_PARAMETERS_DETAILS.AddObject(nuevo_param);
                    _context.SaveChanges();

                    res.result_objs.Add((SV_TRANSACTION_PARAMETER_DETAIL)nuevo_param);
                    dif = (withResiduo ? 1 : 0);    //siguiente ciclo aplica la diferencia si corresponde
                }
                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }
        public RespuestaAccion MODIF_PURCHASE_PARAM(int[] cabeceras, SV_SYSTEM sistema, SV_PARAMETER parametro, decimal valor, bool withResiduo)
        {
            var res = new RespuestaAccion();
            try
            {
                int dif = 0;    //primer ciclo no hay diferencia
                foreach (var head_id in cabeceras)
                {
                    TRANSACTION_PARAMETER_DETAIL curr_param = (from p in _context.TRANSACTIONS_PARAMETERS_DETAILS
                                                               where p.trx_head_id == head_id &&
                                                               p.paratemer_id == parametro.id &&
                                                               p.system_id == sistema.id
                                                               select p).FirstOrDefault();
                    if (curr_param == null)
                    {
                        curr_param = new TRANSACTION_PARAMETER_DETAIL();
                        curr_param.trx_head_id = head_id;
                        curr_param.system_id = sistema.id;
                        curr_param.paratemer_id = parametro.id;
                        curr_param.parameter_value = valor + dif;
                    }
                    else
                    {
                        curr_param.parameter_value = valor + dif; 
                    }

                    _context.TRANSACTIONS_PARAMETERS_DETAILS.AddObject(curr_param);
                    _context.SaveChanges();

                    res.result_objs.Add((SV_TRANSACTION_PARAMETER_DETAIL)curr_param);
                    dif = (withResiduo ? 1 : 0);    //siguiente ciclo aplica la diferencia si corresponde
                }
                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }
    }
}
