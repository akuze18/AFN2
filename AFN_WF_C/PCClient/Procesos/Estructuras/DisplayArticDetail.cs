using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Procesos.Estructuras
{
    public class DisplayArticDetail
    {
        public int CodigoAtributo { get; set; }
        public string Articulo { get; set; }
        public string ValorGuardado { get; set; }
        public string Atributo { get; set; }
        public string ValorDelAtributo { get; set; }
        public bool Mostrar { get; set; }
        public string tipo { get; set; }

        public DisplayArticDetail()
        {
            CodigoAtributo = 0;
            Articulo = string.Empty;
            ValorGuardado = string.Empty;
            Atributo = string.Empty;
            ValorDelAtributo = string.Empty;
            Mostrar = false;
            tipo = "";
        }
        public DisplayArticDetail(PD.SV_ARTICLE_DETAIL source)
        {
            CodigoAtributo = source.cod_atrib;
            Articulo = source.article_code;
            //las fotos cargadas desde Servidor son marcadas con XX:
            ValorGuardado = (source.atributo.tipo == "FOTO" ? "XX:" : "") + source.detalle;
            Atributo = source.atributo.name;
            ValorDelAtributo = source.dscr_detalle;
            Mostrar = source.imprimir;
            tipo = source.atributo.tipo;
        }
    }
}
