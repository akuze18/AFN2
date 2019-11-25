using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_PART
    {
        private int _id;
        private int _article_id;
        private int _part_index;
        private int _quantity;
        private DateTime _first_date;

        public int id { get { return _id; } }
        public int article_id { get { return _article_id; } }
        public int part_index { get { return _part_index; } }
        public int quantity { get { return _quantity; } }
        public DateTime first_date { get { return _first_date; } }

        #region Convertions
        public static implicit operator SV_PART(DataContract.PART od)
        {
            return new SV_PART()
            {
                _id = od.id,
                _article_id = od.article_id,
                _part_index = od.part_index,
                _quantity = od.quantity,
                _first_date = od.first_date,
            };
        }

        public static implicit operator GENERIC_VALUE(SV_PART sv)
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
            return "(" + this.part_index.ToString() + ") Cantidad : " + this.quantity.ToString();
        }
    }
}
