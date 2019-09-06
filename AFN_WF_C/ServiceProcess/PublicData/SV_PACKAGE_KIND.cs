﻿using System;
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

        public int id { get { return _id; } }
        public string descrip { get { return _descrip; } }
        public bool display { get { return _display; } }
        public int type_asset_id { get { return _type_asset_id; } }

        #region Convertions
        public static implicit operator SV_PACKAGE_KIND(DataContract.PACKAGE_KIND od)
        {
            return new SV_PACKAGE_KIND()
            {
                _id = od.id,
                _descrip = od.descrip,
                _display = od.display,
                _type_asset_id = od.type_asset_id,
            };
        }
        public static implicit operator GENERIC_VALUE(SV_PACKAGE_KIND sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.descrip,
                description = sv.descrip,
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion
    }
}
