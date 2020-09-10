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
        private void _load_transactions_details()
        {
            _transactions_details = new TRANSACTIONS_DETAILS(_context.TRANSACTIONS_DETAILS);
        }

        public RespuestaAccion REGISTER_PURCHASE_DETAIL(GENERIC_VALUE cabecera, SV_SYSTEM sistema, bool depreciar, bool con_credito)
        {
            var res = new RespuestaAccion();
            try
            {
                
                TRANSACTION_DETAIL nuevo_det = new TRANSACTION_DETAIL();
                nuevo_det.trx_head_id = cabecera.id;
                nuevo_det.system_id = sistema.id;
                nuevo_det.validity_id = Vigencias.VIGENTE().id;
                nuevo_det.depreciate = depreciar;
                nuevo_det.allow_credit = con_credito;

                _context.TRANSACTIONS_DETAILS.AddObject(nuevo_det);
                _context.SaveChanges();

                res.result_objs.Add((SV_TRANSACTION_DETAIL)nuevo_det);
                
                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }
        public RespuestaAccion MODIF_PURCHASE_DETAIL(int headId, SV_SYSTEM sistema, bool depreciar, bool con_credito)
        {
            var res = new RespuestaAccion();
            try
            {
                int validity_id = 1;    //Durante las compras nunca se debe cambiar el estado de validacion
                
                var FindDetail = (from d in _context.TRANSACTIONS_DETAILS
                                    where d.trx_head_id == headId &&
                                    d.system_id == sistema.id
                                    select d).FirstOrDefault();
                if (FindDetail != null)
                {
                    FindDetail.validity_id = validity_id;
                    FindDetail.allow_credit = con_credito;
                    FindDetail.depreciate = depreciar;

                    _context.SaveChanges();
                    res.AddResultObj(FindDetail.id, FindDetail.GetType());
                    res.set_ok();
                }
                else
                {
                    res.set(-2, "No se encontro cabecera para modificar");
                }
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }

        public RespuestaAccion REGISTER_DOWNS_DETAIL(int trxHeadId, int systemId, SV_VALIDATY downValid)
        {
            var res = new RespuestaAccion();
            try
            {
                TRANSACTION_DETAIL DownDetail = new TRANSACTION_DETAIL();
                DownDetail.trx_head_id = trxHeadId;
                DownDetail.system_id = systemId;
                DownDetail.validity_id = downValid.id;
                DownDetail.depreciate = false;
                DownDetail.allow_credit = false;

                _context.TRANSACTIONS_DETAILS.AddObject(DownDetail);
                _context.SaveChanges();

                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }

        public RespuestaAccion REGISTER_CHANGE_DETAIL(int trxHeadId, SV_TRANSACTION_DETAIL prevDetail)
        {
            var res = new RespuestaAccion();
            try
            {
                TRANSACTION_DETAIL ChangeDetail = new TRANSACTION_DETAIL();
                ChangeDetail.trx_head_id = trxHeadId;
                ChangeDetail.system_id = prevDetail.system_id;
                ChangeDetail.validity_id = prevDetail.validity_id;
                ChangeDetail.depreciate = prevDetail.depreciate;
                ChangeDetail.allow_credit = prevDetail.allow_credit;

                _context.TRANSACTIONS_DETAILS.AddObject(ChangeDetail);
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
