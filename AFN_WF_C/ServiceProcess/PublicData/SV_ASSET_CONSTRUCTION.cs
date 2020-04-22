using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_ASSET_CONSTRUCTION
    {
        private int _id;
        private string _tipo;
        private DateTime _trx_date;
        private SV_ZONE _zone;
        private string _descrip;
        private int? _entrada_id;
        private int? _batch_id;
        private DateTime _post_date;
        private SV_APROVAL_STATE _aproval_state;
        private List<SV_ASSET_CONSTRUCTION_VALUE> _values;

        public int id { get { return _id; } }
        public string tipo { get { return _tipo; } }
        public DateTime trx_date { get { return _trx_date; } }
        public SV_ZONE zone { get { return _zone; } }
        public string descrip { get { return _descrip; } }
        public int? entrada_id { get { return _entrada_id; } }
        public int? batch_id { get { return _batch_id; } }
        public DateTime post_date { get { return _post_date; } }
        public SV_APROVAL_STATE aproval_state { get { return _aproval_state; } }
        public decimal ocupado {get;set;}

        #region Convertions
        public static implicit operator SV_ASSET_CONSTRUCTION(DataContract.ASSET_IN_PROGRESS_HEAD od)
        {
            return new SV_ASSET_CONSTRUCTION()
            {
                _id = od.id,
                _tipo = od.tipo,
                _trx_date = od.trx_date,
                _zone = od.ZONE,
                _descrip = od.descrip,
                _entrada_id = od.entrada_id,
                _batch_id = od.batch_id,
                _post_date = od.post_date,
                _aproval_state = od.APROVAL_STATES,
                _values = od.ASSETS_IN_PROGRESS_DETAIL.ToList().ConvertAll(d => (SV_ASSET_CONSTRUCTION_VALUE)d),
                ocupado = 0,
            };
        }

        public decimal TotalByCurrency(SV_CURRENCY moneda)
        {
            return this._values
                .Where(v => v.currency == moneda)
                .Select(v => v.amount)
                .DefaultIfEmpty(0)
                .Sum();
        }

        public decimal TotalByCurrency(string codeMoneda)
        {
            var valores = this._values
                .Where(v => v.currency == codeMoneda)
                .Select(v => v.amount).ToList();

            return this._values
                .Where(v => v.currency == codeMoneda)
                .Select(v => v.amount)
                .DefaultIfEmpty(0)
                .Sum();
        }

        public static implicit operator GENERIC_VALUE(SV_ASSET_CONSTRUCTION sv)
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
