using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Exc = Microsoft.Office.Interop.Excel;

namespace AFN_WF_C.PCClient.Procesos.Estructuras
{
    internal class BorderPaintingFormat
    {
        public BorderParameters EdgeLeft;
        public BorderParameters EdgeTop;
        public BorderParameters EdgeBottom;
        public BorderParameters EdgeRight;
        public BorderParameters InsideVertical;
        public BorderParameters InsideHorizontal;

        public int InteriorColor;

        public BorderPaintingFormat()
        {
            EdgeLeft = new BorderParameters(Exc.XlBordersIndex.xlEdgeLeft);
            EdgeTop = new BorderParameters(Exc.XlBordersIndex.xlEdgeTop);
            EdgeBottom = new BorderParameters(Exc.XlBordersIndex.xlEdgeBottom);
            EdgeRight = new BorderParameters(Exc.XlBordersIndex.xlEdgeRight);
            InsideVertical = new BorderParameters(Exc.XlBordersIndex.xlInsideVertical);
            InsideHorizontal = new BorderParameters(Exc.XlBordersIndex.xlInsideHorizontal);
            InteriorColor = 16777215;
        }
        public List<BorderParameters> Each()
        {
            var resultado = new List<BorderParameters>();
            resultado.AddRange(new[] { EdgeLeft, EdgeTop, EdgeBottom, EdgeRight, InsideVertical, InsideHorizontal });
            return resultado;
        }

        public BorderPaintingFormat Copy()
        {
            BorderPaintingFormat copied = new BorderPaintingFormat();
            copied.EdgeLeft = this.EdgeLeft;
            copied.EdgeRight = this.EdgeRight;
            copied.EdgeTop = this.EdgeTop;
            copied.EdgeBottom = this.EdgeBottom;
            copied.InsideVertical = this.InsideVertical;
            copied.InsideHorizontal = this.InsideHorizontal;
            copied.InteriorColor = this.InteriorColor;
            return copied;
        }

        public bool ApplyFormats(Exc.Range Celda)
        {
            try
            {
                Celda.Interior.Color = InteriorColor;
                foreach (var borde in this.Each())
                {
                    if (borde.MustFormat)
                    {
                        Celda.Borders[borde.Side].ColorIndex = borde.IndexColor;
                        Celda.Borders[borde.Side].TintAndShade = borde.TintAndShade;
                        Celda.Borders[borde.Side].Weight = borde.Weight;
                        Celda.Borders[borde.Side].LineStyle = borde.LineStyle;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    internal class BorderParameters
    {
        private bool _MustFormat;
        public Exc.XlBordersIndex Side;
        public int IndexColor;
        //You can enter a number from -1 (darkest) to 1 (lightest) for the TintAndShade property. Zero (0) is neutral.
        public int TintAndShade;
        public int Weight;
        public Exc.XlLineStyle LineStyle;


        public bool MustFormat { get { return _MustFormat; } }

        public BorderParameters(Exc.XlBordersIndex side)
        {
            Side = side;
            _MustFormat = false;
            IndexColor = (int) Exc.XlColorIndex.xlColorIndexNone;
            TintAndShade = 0;
            Weight = 0;
            LineStyle = Exc.XlLineStyle.xlLineStyleNone;
        }
        public void SetValues(int index_color, int weight, int tint_and_shade = 0)
        {
            _MustFormat = true;
            IndexColor = index_color;
            TintAndShade = tint_and_shade;
            Weight = weight;
            LineStyle = Exc.XlLineStyle.xlContinuous;
        }
        public void SetValues(int index_color, int weight,Exc.XlLineStyle line_style, int tint_and_shade = 0)
        {
            _MustFormat = true;
            IndexColor = index_color;
            TintAndShade = tint_and_shade;
            Weight = weight;
            LineStyle = line_style;
        }
    }
}
