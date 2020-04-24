using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_PLACE
    {
        public int id { get; private set; }
        public string descrip { get; private set; }
        public int nivel { get; private set; }
        public int superior { get; private set; }
        public bool activo { get; private set; }

        public SV_PLACE parentPlace { get; set; }

        #region Convertions
        public static implicit operator SV_PLACE(DataContract.PLACE od)
        {
            return new SV_PLACE()
            {
                id = od.id,
                descrip = od.descrip,
                nivel = od.nivel,
                superior = od.superior,
                activo = od.activo,
            };
        }

        public static implicit operator GENERIC_VALUE(SV_PLACE sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.id.ToString(),
                description = sv.ToString(),
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion

        public override string ToString()
        {
            if (parentPlace != null && nivel == 2)
                return parentPlace.descrip + "-" + this.descrip;
            else
                return this.descrip;
        }
    }
}
