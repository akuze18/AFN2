using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_TRANSACTION_DETAIL
    {
        private int _id;
        private int _trx_head_id;
        private int _system_id;
        private int _validity_id;
        private bool _depreciate;
        private bool _allow_credit;

        public int id { get { return _id; } }
        public int trx_head_id { get { return _trx_head_id; } }
        public int system_id { get { return _system_id; } }
        public int validity_id { get { return _validity_id; } }
        public bool depreciate { get { return _depreciate; } }
        public bool allow_credit { get { return _allow_credit; } }


        #region Convertions
        public static implicit operator SV_TRANSACTION_DETAIL(DataContract.TRANSACTION_DETAIL od)
        {
            return new SV_TRANSACTION_DETAIL()
            {
                _id = od.id,
                _trx_head_id = od.trx_head_id,
                _system_id = od.system_id,
                _validity_id = od.validity_id,
                _depreciate = od.depreciate,
                _allow_credit = od.allow_credit,
            };
        }

        public static implicit operator GENERIC_VALUE(SV_TRANSACTION_DETAIL sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.id.ToString(),
                description = sv.ToString(),
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion

        public override string ToString()
        {
            return "DETAIL " + this.id.ToString();
        }
    }
}
