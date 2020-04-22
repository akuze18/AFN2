using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_ATTRIBUTE
    {
        public int id { get; private set; }
        public int codOld { get; private set; }
        public string name { get; private set; }
        public bool active { get; private set; }
        public bool imprimir { get; private set; }
        public string tipo { get; private set; }

        #region Convertions
        public static implicit operator SV_ATTRIBUTE(DataContract.ATTRIBUTE od)
        {
            return new SV_ATTRIBUTE()
            {
                id = od.id,
                codOld = od.codOld,
                name = od.name,
                active = od.active,
                imprimir = od.imprimir,
                tipo = od.tipo
            };
        }
        public static implicit operator GENERIC_VALUE(SV_ATTRIBUTE sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.id.ToString(),
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
