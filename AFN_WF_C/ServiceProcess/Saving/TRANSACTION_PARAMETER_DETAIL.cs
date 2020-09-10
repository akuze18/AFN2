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
        private void _load_transactions_param_details()
        {
            _transactions_parameters = new TRANSACTIONS_PARAM_DET(_context.TRANSACTIONS_PARAMETERS_DETAILS);
        }

        public RespuestaAccion REGISTER_PURCHASE_PARAM(int head_id, SV_SYSTEM sistema, SV_PARAMETER parametro, decimal valor)
        {
            var res = new RespuestaAccion();
            try
            {
                TRANSACTION_PARAMETER_DETAIL nuevo_param = new TRANSACTION_PARAMETER_DETAIL();
                nuevo_param.trx_head_id = head_id ;
                nuevo_param.system_id = sistema.id;
                nuevo_param.paratemer_id = parametro.id;
                nuevo_param.parameter_value = valor;

                _context.TRANSACTIONS_PARAMETERS_DETAILS.AddObject(nuevo_param);
                _context.SaveChanges();
                res.result_objs.Add((SV_TRANSACTION_PARAMETER_DETAIL)nuevo_param);
                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }
        public RespuestaAccion MODIF_PURCHASE_PARAM(int head_id, SV_SYSTEM sistema, SV_PARAMETER parametro, decimal valor, bool withResiduo)
        {
            var res = new RespuestaAccion();
            try
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
                    curr_param.parameter_value = valor ;
                    _context.TRANSACTIONS_PARAMETERS_DETAILS.AddObject(curr_param);
                }
                else
                {
                    curr_param.parameter_value = valor; 
                }
                _context.SaveChanges();

                res.result_objs.Add((SV_TRANSACTION_PARAMETER_DETAIL)curr_param);
                
                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }
        public RespuestaAccion DELETE_PURCHASE_PARAM(int head_id, SV_SYSTEM sistema, SV_PARAMETER parametro)
        {
            var res = new RespuestaAccion();
            try
            {
                
                TRANSACTION_PARAMETER_DETAIL curr_param = (from p in _context.TRANSACTIONS_PARAMETERS_DETAILS
                                                            where p.trx_head_id == head_id &&
                                                            p.paratemer_id == parametro.id &&
                                                            p.system_id == sistema.id
                                                            select p).FirstOrDefault();
                if (curr_param != null)
                {
                    _context.TRANSACTIONS_PARAMETERS_DETAILS.DeleteObject(curr_param);
                    _context.SaveChanges();
                    res.result_objs.Add((SV_TRANSACTION_PARAMETER_DETAIL)curr_param);
                    res.set_ok();
                }
                else
                    //no existe, por lo que no hay que borrarlo           
                    res.set(2,"No existe parametro para borrar");
                
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }

        public RespuestaAccion REGISTER_PARAM_DETAIL(int trxHeadId, int systemId, int paramId, decimal currValue)
        {
            var res = new RespuestaAccion();
            try
            {
                TRANSACTION_PARAMETER_DETAIL sellParam = new TRANSACTION_PARAMETER_DETAIL();
                sellParam.trx_head_id = trxHeadId;
                sellParam.system_id = systemId;
                sellParam.paratemer_id = paramId;
                sellParam.parameter_value = currValue;

                _context.TRANSACTIONS_PARAMETERS_DETAILS.AddObject(sellParam);
                _context.SaveChanges();

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
