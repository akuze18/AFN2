﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_PARAMETER
    {
        private int _id;
        private string _code;
        private string _name;

        public int id { get { return _id; } }
        public string code { get { return _code; } }
        public string name { get { return _name; } }

        #region Convertions
        public static implicit operator SV_PARAMETER(DataContract.PARAMETER od)
        {
            return new SV_PARAMETER()
            {
                _id = od.id,
                _code = od.code,
                _name = od.name,
            };
        }
        public static implicit operator GENERIC_VALUE(SV_PARAMETER sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.code,
                description = sv.name,
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion
    }
}
