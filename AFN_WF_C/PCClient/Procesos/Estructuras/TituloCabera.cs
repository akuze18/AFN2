using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.PCClient.Procesos.Estructuras
{
    internal class TituloCabera
    {
        public int Index { get; set; }
        public string ColTitulo { get; set; }
        public string FormatTipo { get; set; }

        public TituloCabera(int Ind,string cTitulo, string fTipo) {
            Index = Ind;
            ColTitulo = cTitulo;
            FormatTipo = fTipo;
        }
       
        public TituloCabera(int Ind, string cTitulo)
        {
            Index = Ind;
            ColTitulo = cTitulo;
            FormatTipo = string.Empty;
        }

    }
}
