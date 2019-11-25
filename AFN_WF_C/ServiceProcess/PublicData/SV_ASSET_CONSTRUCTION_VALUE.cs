using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_ASSET_CONSTRUCTION_VALUE
    {
        private int _id;
        private int _head_id;
        SV_CURRENCY _currency;
        private decimal _amount;

        public int id { get { return _id; } }
        public int head_id { get { return _head_id; } }
        public SV_CURRENCY currency { get { return _currency; } }
        public decimal amount { get { return _amount; } }

        #region Convertions
        public static implicit operator SV_ASSET_CONSTRUCTION_VALUE(DataContract.ASSET_IN_PROGRESS_DETAIL od)
        {
            return new SV_ASSET_CONSTRUCTION_VALUE()
            {
                _id = od.id,
                _head_id = od.head_id,
                _currency = od.CURRENCY,
                _amount = od.amount,
            };
        }
        public static implicit operator GENERIC_VALUE(SV_ASSET_CONSTRUCTION_VALUE sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.id.ToString(),
                description = sv.currency.code + ": " + sv.amount.ToString(),
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion
    }
}
