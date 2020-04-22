using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_ARTICLE
    {
        public int id { get; private set; }
        public string code { get; private set; }
        public int part_id { get; private set; }
        public string codigo_old { get; private set; }
        public int ubicacion_id { get; private set; }
        public DateTime desde { get; private set; }
        public DateTime hasta { get; private set; }
    

    #region Convertions
        public static implicit operator SV_ARTICLE(DataContract.ARTICLE od)
        {
            return new SV_ARTICLE()
            {
                id = od.id,
                code = od.code,
                part_id = od.part_id,
                codigo_old = od.codigo_old,
                ubicacion_id = od.ubicacion_id,
                desde = od.desde,
                hasta = od.hasta
            };
        }
        public static implicit operator GENERIC_VALUE(SV_ARTICLE sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.code,
                description = sv.code,
                type = sv.GetType().Name.Substring(3),
            };
        }
    #endregion
    }
}
