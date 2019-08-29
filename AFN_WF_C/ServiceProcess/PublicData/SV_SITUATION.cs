using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using C = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_SITUATION
    {
        private int _id;
        private int _estado1;
        private int _estado2;
        private string _condicion;

        public int id { get { return _id; } }
        public int estado1 { get { return _estado1; } }
        public int estado2 { get { return _estado2; } }
        public string condicion { get { return _condicion; } }

        #region Convertions
        public static implicit operator SV_SITUATION(C.SITUATION od)
        {
            return new SV_SITUATION()
            {
                _id = od.id,
                _estado1 = od.estado1,
                _estado2 = od.estado2,
                _condicion = od.condicion,
            };
        }
        public static implicit operator GENERIC_VALUE(SV_SITUATION sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.id.ToString(),
                description = sv.condicion,
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion
    }
}
