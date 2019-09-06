using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_TRANSACTION_HEADER
    {
        private int _id;
        private int _article_part_id;
        private DateTime _trx_ini;
        private DateTime _trx_end;
        private string _ref_source;
        private int _zone_id;
        private int _subzone_id;
        private int _kind_id;
        private int _subkind_id;
        private int _category_id;
        private string _user_own;
        private int? _manage_id;

        public int id { get { return _id; } }
        public int article_part_id { get { return _article_part_id; } }
        public DateTime trx_ini { get { return _trx_ini; } }
        public DateTime trx_end { get { return _trx_end; } }
        public string ref_source { get { return _ref_source; } }
        public int zone_id { get { return _zone_id; } }
        public int subzone_id { get { return _subzone_id; } }
        public int kind_id { get { return _kind_id; } }
        public int subkind_id { get { return _subkind_id; } }
        public int category_id { get { return _category_id; } }
        public string user_own { get { return _user_own; } }
        public int? manage_id { get { return _manage_id; } }

        #region Convertions
        public static implicit operator SV_TRANSACTION_HEADER(DataContract.TRANSACTION_HEADER od)
        {
            return new SV_TRANSACTION_HEADER()
            {
                _id = od.id,
                _article_part_id = od.article_part_id,
                _trx_ini = od.trx_ini,
                _trx_end = od.trx_end,
                _ref_source = od.ref_source,
                _zone_id = od.zone_id,
                _subzone_id = od.subzone_id,
                _kind_id = od.kind_id,
                _subkind_id = od.subkind_id,
                _category_id = od.category_id,
                _user_own = od.user_own,
                _manage_id = od.manage_id,

            };
        }
        #endregion

        public override string ToString()
        {
            return this.trx_ini.ToShortDateString() + " (" + this.ref_source + ")";
        }
    }
}
