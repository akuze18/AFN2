using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Exc = Microsoft.Office.Interop.Excel;
using PD = AFN_WF_C.ServiceProcess.PublicData;

using AFN_WF_C.PCClient.Procesos.Estructuras;
using ACode;

namespace AFN_WF_C.PCClient.Procesos
{
    internal class ExcelWrite
    {
        private Exc._Worksheet _oSheet;

        public ExcelWrite(Exc._Worksheet oSheet)
        {
            _oSheet = oSheet;
        }

        public static Exc.XlBordersIndex[] ClasicBorders
        {
            get
            {
                return new Exc.XlBordersIndex[]{
                    Exc.XlBordersIndex.xlEdgeLeft,
                    Exc.XlBordersIndex.xlEdgeTop,
                    Exc.XlBordersIndex.xlEdgeBottom,
                    Exc.XlBordersIndex.xlEdgeRight,
                    Exc.XlBordersIndex.xlInsideVertical,
                    Exc.XlBordersIndex.xlInsideHorizontal
                };
            }
        }
        public static void set_sheet_amount(Exc.Workbook eBook, int amount)
        {
            var currCount = eBook.Sheets.Count;
            if (currCount > amount)
            {
                for (int i = currCount; i > (amount); i--)
                    eBook.Sheets[i].Delete();

            }
            if (currCount < amount)
            {
                for (int i = 0; i < (amount - currCount); i++)
                    eBook.Sheets.Add();
            }
        }

        public static Exc.Workbook newBookOff()
        {
            return newBookOff(0);
        }
        public static Exc.Workbook newBookOff(int sheetAmount)
        {
            Exc.Application excel;
            excel = new Exc.Application();
            //excel.Visible = false;
            //excel.DisplayAlerts = false;
            var book = excel.Workbooks.Add();
            if(sheetAmount >0)
                set_sheet_amount(book, sheetAmount);
            return book;
        }
        public static void BookOn(Exc.Workbook wBook)
        {
            wBook.Application.Visible = true;
            wBook.Application.DisplayAlerts = true;
        }
        
        public void set_clear_grid()
        {
            var grid = _oSheet.Cells;
            grid.ColumnWidth = 3;
            foreach (var border in ClasicBorders)
            {
                var sideBorder = grid.Borders[border];
                sideBorder.LineStyle = 1;
                sideBorder.ColorIndex = 2;
                sideBorder.TintAndShade = 0;
                sideBorder.Weight = 2;
                sideBorder = null;
            }
            grid = null;
        }

        public int write_data_column(IEnumerable<PD.IElemento> detail, TituloCabera[] titulos, int TitleIndexRow, BorderPaintingFormat tituloFormato, BorderPaintingFormat contentFormato, BorderPaintingFormat TotalsFormato)
        {
            bool ckT = false;
            int processed_rows = detail.Count();
            int max_columnas = titulos.Count();
            int DetailIndexRow = TitleIndexRow + 1;

            for (int curr_col = 0; curr_col < max_columnas; curr_col++)
            {
                var tituloValue = titulos[curr_col];
                write_tittle_area(tituloValue, TitleIndexRow, (curr_col + 1), tituloFormato);
                write_content_area(detail, tituloValue, DetailIndexRow, detail.Count(), (curr_col + 1), contentFormato);
                ckT = write_totals_area(detail, tituloValue, DetailIndexRow + processed_rows, (curr_col + 1), TotalsFormato);
            }
            if (ckT) processed_rows += 1;
            return processed_rows;
        }
        public int write_data_column_colored(IEnumerable<PD.IElemento> detail, TituloCabera[] titulos, int TitleIndexRow, BorderPaintingFormat tituloFormato, BorderPaintingFormat contentFormato, BorderPaintingFormat TotalsFormato)
        {
            bool ckT = false;
            int processed_rows = detail.Count();
            int max_columnas = titulos.Count();
            int DetailIndexRow = TitleIndexRow + 1;

            for (int curr_col = 0; curr_col < max_columnas; curr_col++)
            {
                var tituloValue = titulos[curr_col];
                write_tittle_area(tituloValue, TitleIndexRow, (curr_col + 1), tituloFormato);
                if (detail.GetType() == typeof(List<PD.GROUP_MOVEMENT>))
                {
                    var grouped = (List<PD.GROUP_MOVEMENT>)detail;
                    int SingleIndexRow = DetailIndexRow + 1;
                    foreach (var gp in grouped)
                    {
                        var single_list = new List<PD.GROUP_MOVEMENT> { gp };
                        var singleFormato = contentFormato.Copy();
                        int extra_space = 0;
                        if (gp.orden1 == "99999")
                        {
                            singleFormato.InteriorColor = 16764057;
                            SingleIndexRow++;
                            extra_space = 1;
                        }
                        else
                            if (gp.orden2 == "99999")
                            {
                                singleFormato.InteriorColor = 5296274;
                                SingleIndexRow++;
                                extra_space = 1;
                            }
                        write_content_area(single_list, tituloValue, SingleIndexRow, single_list.Count(), (curr_col + 1), singleFormato);
                        SingleIndexRow += extra_space + 1;
                    }
                }
            }
            if (ckT) processed_rows += 1;
            return processed_rows;
        }

        private bool write_tittle_area(TituloCabera info, int RowPosition, int ColumnPosition, BorderPaintingFormat formatos)
        {
            try
            {
                Exc.Range celLrangE;
                celLrangE = _oSheet.Cells[RowPosition, ColumnPosition];
                //Format
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignCenter;        //-4108 equivale a centrar
                celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignCenter;
                celLrangE.WrapText = true;                    //equivale a alinear contenido a la celda
                celLrangE.Orientation = 0;
                celLrangE.AddIndent = false;
                celLrangE.IndentLevel = 0;
                celLrangE.ShrinkToFit = false;
                celLrangE.RowHeight = 30;                     //alto de fila se estable en 30
                celLrangE.Font.Bold = true;
                //Border Format
                formatos.ApplyFormats(celLrangE);

                //Value
                celLrangE.Value = info.ColTitulo;
                celLrangE = null;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool write_content_area(IEnumerable<PD.IElemento> detail, TituloCabera info, int RowInitPosition, int TotalElements, int ColumnPosition, BorderPaintingFormat formatos)
        {
            try
            {
                Exc.Range celLrangE;
                celLrangE = _oSheet.Range[
                        _oSheet.Cells[RowInitPosition, ColumnPosition],
                        _oSheet.Cells[(RowInitPosition + TotalElements - 1), ColumnPosition]];
                //Border Values
                formatos.ApplyFormats(celLrangE);
                if (info.Index != 0)
                {
                    ColumnData data = RawDataColumn(detail, info);
                    celLrangE.NumberFormat = data.Formating;
                    celLrangE.Value = data.Valores;
                }
                return true;
            }
            catch
            { return false; }

        }
        private bool write_totals_area(IEnumerable<PD.IElemento> detail, TituloCabera info, int RowPosition, int ColumnPosition, BorderPaintingFormat formatos)
        {
            try
            {
                Exc.Range celLrangE;
                celLrangE = _oSheet.Cells[RowPosition, ColumnPosition];
                //Border Values
                formatos.ApplyFormats(celLrangE);
                if (info.Index != 0)
                {
                    ColumnData data = RawDataColumn(detail, info);
                    celLrangE.NumberFormat = data.Formating;
                    if (data.ColType == typeof(decimal))
                    {
                        decimal suma = 0;
                        for (int i = 0; i < data.Valores.Length; i++)
                        {
                            suma += (decimal)(data.Valores[i, 0]);
                        }
                        celLrangE.Value = suma;
                    }
                }
                return true;
            }
            catch
            { return false; }
        }

        public void set_sheet_format(int TotalColumns, PD.SV_SYSTEM sistema, Vperiodo WorkPeriodo, string Titulo, string nom_pag)
        {
            _oSheet.Range[_oSheet.Cells[1, 1], _oSheet.Cells[1, TotalColumns]].EntireColumn.AutoFit();
            string Mostrar, periodo, Titulo1, Titulo2;
            DateTime fecha_cm;
            Titulo1 = sistema.ENVIORMENT.name;
            Titulo2 = "(" + sistema.CURRENCY.code + ")";

            fecha_cm = WorkPeriodo.last;
            Mostrar = Titulo + WorkPeriodo.lastDB.Substring(0, 4) + " " + Titulo2 + "\r";
            periodo = fecha_cm.ToString("MMMM yyyy");
            //pies = ""

            _oSheet.Name = nom_pag;
            //oSheet.Application.ScreenUpdating = false;
            //oSheet.Application.Calculation = Exc.XlCalculation.xlCalculationManual;

            var ps = _oSheet.PageSetup;
            ps.LeftHeaderPicture.Filename = Auxiliar.fileLogo;

            //ps.PrintTitleRows = "$1:$2"
            ps.Orientation = Exc.XlPageOrientation.xlLandscape;
            //ps.BlackAndWhite = False
            ps.Zoom = 80;
            //ps.FitToPagesWide = 1;
            //ps.FitToPagesTall = 1;
            try
            {
                ps.PaperSize = Exc.XlPaperSize.xlPaperFolio;
            }
            catch
            {
                try
                {
                    ps.PaperSize = Exc.XlPaperSize.xlPaperLetter;
                }
                catch
                {
                    //solo pasar el error, buscar un manejo mas correcto para este caso
                }
            }

            ps.LeftHeader = "&G";
            ps.CenterHeader = Mostrar;
            ps.RightHeader = periodo;
            ps.LeftFooter = Auxiliar.getUser() + "\r" + "&P / &N";
            //ps.CenterFooter = pies;
            ps.RightFooter = "Fecha de Impresion : &D" + "\r" + "Hora de Impresion: &T";
            ps.TopMargin = _oSheet.Application.InchesToPoints(1.01);
            ps.CenterHorizontally = true;

        }


        private static ColumnData RawDataColumn(IEnumerable<PD.IElemento> detail, TituloCabera titulo)
        {
            return RawDataColumn(detail.ToList(), titulo);
        }
        private static ColumnData RawDataColumn(List<PD.IElemento> detail, TituloCabera titulo)
        {
            object[,] result = new object[detail.Count, 1];
            Type tipo = null;
            for (var j = 0; j < detail.Count; j++)
            {
                var row_proc = detail[j];
                var titulo_proc = titulo;
                object selected = row_proc.Item(titulo_proc.Index);
                tipo = selected.GetType();
                switch (tipo.Name)
                {
                    case "GENERIC_VALUE":
                    case "GENERIC_RELATED":
                        var val_d = (PD.GENERIC_VALUE)selected;
                        string formato = titulo_proc.FormatTipo;
                        if (formato == "CODE")
                            result[j, 0] = val_d.code;
                        else if (formato == "DESCRIP")
                            result[j, 0] = val_d.description;
                        else if (formato == "ID")
                            result[j, 0] = val_d.id.ToString();
                        else
                            result[j, 0] = val_d.code;
                        break;
                    default:
                        result[j, 0] = selected;
                        break;
                }
            }

            return new ColumnData { Valores = result, ColType = tipo };
        }


    }
}
