using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_BATCH_ARTICLE
    {
        private int _id;
        private int _aproval_state_id;
        private string _descrip;
        private DateTime _purchase_date;
        private decimal _initial_price;
        private int _initial_life_time;
        private DateTime _account_date;
        private int _origin_id;
        private int _type_asset_id;
        private List<SV_DOCUMENT> _documents;

        public int id { get { return _id; } }
        public int aproval_state_id { get { return _aproval_state_id; } }
        public string descrip { get { return _descrip; } }
        public DateTime purchase_date { get { return _purchase_date; } }
        public decimal initial_price { get { return _initial_price; } }
        public int initial_life_time { get { return _initial_life_time; } }
        public DateTime account_date { get { return _account_date; } }
        public int origin_id { get { return _origin_id; } }
        public int type_asset_id { get { return _type_asset_id; } }
        public List<SV_DOCUMENT> documents { get { return _documents; } }

        #region Convertions
        public static implicit operator SV_BATCH_ARTICLE(DataContract.BATCH_ARTICLE od)
        {
            //od.DOCS_BATCH.Load();
            return new SV_BATCH_ARTICLE()
            {
                _id = od.id,
                _aproval_state_id = od.aproval_state_id,
                _descrip = od.descrip,
                _purchase_date = od.purchase_date,
                _initial_price = od.initial_price,
                _initial_life_time = od.initial_life_time,
                _account_date = od.account_date,
                _type_asset_id = od.type_asset_id,
                _origin_id = od.origin_id,
                _documents = od.DOCS_BATCH.Select(db => db.DOCUMENT)
                        .ToList()
                        .ConvertAll(d => (SV_DOCUMENT)d),
            };
        }
        public static implicit operator GENERIC_VALUE(SV_BATCH_ARTICLE sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.id.ToString(),
                description = sv.descrip,
                type = sv.GetType().Name.Substring(3),
            };
        }

        #endregion
    }
}
