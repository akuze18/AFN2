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
        public string ColTipo { get; set; }
        public string FormatTipo { get; set; }

        public TituloCabera(int Ind, string cTitulo, string cTipo) {
            Index = Ind;
            ColTitulo = cTitulo;
            ColTipo = cTipo;
            FormatTipo = cTipo;
        }
        public TituloCabera(int Ind, string cTitulo, Type cTipo)
        {
            Index = Ind;
            ColTitulo = cTitulo;
            ColTipo = cTipo.Name;
            FormatTipo = cTipo.Name;
        }
        public TituloCabera(int Ind, string cTitulo, string cTipo, string fTipo)
        {
            Index = Ind;
            ColTitulo = cTitulo;
            ColTipo = cTipo;
            FormatTipo = fTipo;
        }
        public TituloCabera(int Ind, string cTitulo, Type cTipo, Type fTipo)
        {
            Index = Ind;
            ColTitulo = cTitulo;
            ColTipo = cTipo.Name;
            FormatTipo = fTipo.Name;
        }
        public TituloCabera(int Ind, string cTitulo, Type cTipo, string fTipo)
        {
            Index = Ind;
            ColTitulo = cTitulo;
            ColTipo = cTipo.Name;
            FormatTipo = fTipo;
        }
    }
}
