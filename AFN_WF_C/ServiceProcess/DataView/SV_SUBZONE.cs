﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.DataView
{
    public class SV_SUBZONE
    {
        private int _id;
        private string _descrip;
        private bool _active;
        private bool _principal;
        private int? _zone_id;
        private string _codPlace;
        private string _codOld;

        public int id { get { return _id; } }
        public string descrip { get { return _descrip; } }
        public bool active { get { return _active; } }
        public bool principal { get { return _principal; } }
        public int? zone_id { get { return _zone_id; } }
        public string codPlace { get { return _codPlace; } }
        public string codOld { get { return _codOld; } }

        #region Convertions
        public static implicit operator SV_SUBZONE(C.SUBZONE od)
        {
            return new SV_SUBZONE()
            {
                _id = od.id,
                _descrip = od.descrip,
                _active = od.active,
                _principal = od.principal,
                _zone_id = od.zone_id,
                _codPlace = od.codPlace,
                _codOld = od.codOld,
            };
        }
        public static implicit operator C.GENERIC_VALUE(SV_SUBZONE sv)
        {
            return new C.GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.codPlace,
                description = sv.descrip,
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion
    }
}
