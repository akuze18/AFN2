using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.DataView
{
    public class SV_PART
    {
        private int _id;
        private int _article_id;
        private int _part_index;
        private int _quantity;

        public int id { get { return _id; } }
        public int article_id { get { return _article_id; } }
        public int part_index { get { return _part_index; } }
        public int quantity { get { return _quantity; } }

        #region Convertions
        public static implicit operator SV_PART(C.PART od)
        {
            return new SV_PART()
            {
                _id = od.id,
                _article_id = od.article_id,
                _part_index = od.part_index,
                _quantity = od.quantity,
            };
        }
        #endregion

        public override string ToString()
        {
            return "(" + this.part_index.ToString() + ") Cantidad : " + this.quantity.ToString();
        }
    }
}
