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
        public RespuestaAccion REGISTER_PURCHASE_DETAIL(List<GENERIC_VALUE> cabeceras, SV_SYSTEM sistema, bool depreciar, bool con_credito)
        {
            var res = new RespuestaAccion();
            try
            {
                foreach (var cabecera in cabeceras)
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
