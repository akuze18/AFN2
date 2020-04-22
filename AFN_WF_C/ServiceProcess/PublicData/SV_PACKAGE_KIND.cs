using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_PACKAGE_KIND
    {
        private int _id;
        private string _descrip;
        private bool _display;
        private int _type_asset_id;
        private int[] _pair_kinds_id;

        public int id { get { return _id; } }
        public string descrip { get { return _descrip; } }
        public bool display { get { return _display; } }
        public int type_asset_id { get { return _type_asset_id; } }

        public int[] PACKAGE_PAIR_KINDS { get { return _pair_kinds_id; }}

        #region Convertions
        public static implicit operator SV_PACKAGE_KIND(DataContract.PACKAGE_KIND od)
        {
            return new SV_PACKAGE_KIND()
            {
                _id = od.id,
                _descrip = od.descrip,
                _display = od.display,
                _type_asset_id = od.type_asset_id,
                _pair_kinds_id = od.PACKAGE_PAIR_KINDS.Select(x => x.kind_id).ToArray(),
            };
        }

        public static implicit operator GENERIC_RELATED(SV_PACKAGE_KIND pk)
        {
            var converted = new GENERIC_RELATED();
            if (pk != null)
            {

                converted.id = pk.id;
                converted.code = pk.type_asset_id.ToString();
                converted.description = pk.descrip;
                converted.type = pk.GetType().Name;
                //this.related_id = pk.PACKAGE_PAIR_KINDS.FirstOrDefault().kind_id;
                var find_rel = pk.PACKAGE_PAIR_KINDS.FirstOrDefault();
                //int indice = (find_rel != null ? find_rel : 0);
                converted.related = new GENERIC_VALUE()
                {
                    id = find_rel,
                    type = "KIND"
                };
                
            }
            return converted;
        }

        //public static implicit operator GENERIC_VALUE(SV_PACKAGE_KIND sv)
        //{
        //    return new GENERIC_VALUE()
        //    {
        //        id = sv.id,
        //        code = sv.descrip,
        //        description = sv.descrip,
        //        type = sv.GetType().Name.Substring(3),
        //    };
        //}
        #endregion
    }
}
