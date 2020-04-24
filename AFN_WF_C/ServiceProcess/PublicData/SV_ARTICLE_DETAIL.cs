using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_ARTICLE_DETAIL
    {
        public int id { get; private set; }
        public int lote_id { get; private set; }
        public int? article_id { get; private set; }
        public string article_code { get; private set; }
        public int cod_atrib { get; private set; }
        public SV_ATTRIBUTE atributo { get; private set; }
        public string detalle { get; private set; }
        public string dscr_detalle { get; private set; }
        public DateTime fech_ini { get; private set; }
        public DateTime fech_fin { get; private set; }
        public bool imprimir { get; private set; }

        private static int[] specialAtt = new int[] { 14, 15 };

        #region Convertions
        public static implicit operator SV_ARTICLE_DETAIL(DataContract.ARTICLES_VALUES od)
        {
            string arCode;
            int oldCode;
            if (od.article_id == null)
            {
                arCode = string.Empty;
                oldCode = 0;
            }
            else
            {
                arCode = od.ARTICLE.code;
                oldCode = od.ATTRIBUTE.codOld;
            }

            return new SV_ARTICLE_DETAIL()
            {
                id = od.id,
                lote_id = od.batch_id,
                article_id = od.article_id,
                article_code = arCode,
                cod_atrib = od.attrib_id,
                atributo = od.ATTRIBUTE,
                detalle = od.detail,
                fech_ini = od.date_init,
                fech_fin = od.date_end,
                imprimir = od.imprimir,
                dscr_detalle = (specialAtt.Contains(oldCode) ? "" : od.detail)
            };
        }
        public static implicit operator GENERIC_VALUE(SV_ARTICLE_DETAIL sv)
        {
            return new GENERIC_VALUE()
            {
                id = sv.id,
                code = sv.cod_atrib.ToString(),
                description = sv.detalle,
                type = sv.GetType().Name.Substring(3),
            };
        }
        #endregion

        public static SV_ARTICLE_DETAIL Empty
        {
            get
            {
                return new SV_ARTICLE_DETAIL()
                {
                    id = 0,
                    lote_id = 0,
                    article_id = null,
                    article_code = string.Empty,
                    cod_atrib = 0,
                    atributo = null,
                    detalle = string.Empty,
                    fech_ini = DateTime.MinValue,
                    fech_fin = DateTime.MaxValue,
                    imprimir = false,
                    dscr_detalle = string.Empty
                };
            }
        }
    }
}
