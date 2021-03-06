﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_ZONE
    {
        private int _id;
        private string _name;
        private bool _active;
        private string _codDept;
        private string _codOld;

        public int id { get { return _id; } }
        public string name { get { return _name; } }
        public bool active { get { return _active; } }
        public string codDept { get { return _codDept; } }
        public string codOld { get { return _codOld; } }

        #region Convertions
        public static implicit operator SV_ZONE(DataContract.ZONE od)
        {
            return new SV_ZONE()
            {
                _id = od.id,
                _name = od.name,
                _active = od.active,
                _codDept = od.codDept,
                _codOld = od.codOld
            };
        }
        public static implicit operator GENERIC_VALUE(SV_ZONE sv)
        {
            if (sv == null) return new GENERIC_VALUE();
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.codDept,
                description = sv.name,
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion

        public override string ToString()
        {
            return this.name;
        }
    }
}
