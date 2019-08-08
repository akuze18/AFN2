using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACode;

using SC = AFN_WF_C.ServiceProcess.DataContract;
using Exc = Microsoft.Office.Interop.Excel;
using AFN_WF_C.PCClient.Procesos.Estructuras;

namespace AFN_WF_C.PCClient.Procesos
{
    internal class Reportes
    {
        private static object[,] RawDataColumn(IEnumerable<SC.IElemento> detail, TituloCabera[] titulos, int current_column)
        {
            return RawDataColumn(detail.ToList(), titulos, current_column);
        }
        private static object[,] RawDataColumn(List<SC.IElemento> detail, TituloCabera[] titulos, int current_column)
        {
            object[,] result;

            result = new object[detail.Count, 1];
            //var Imatrix = new int[detail.Count, 1];
            //var Tmatrix = new string[detail.Count, 1];
            //var Nmatrix = new double[detail.Count, 1];
            //var Dmatrix = new DateTime[detail.Count, 1];

            for (var j = 0; j < detail.Count; j++)
            {
                var row_proc = detail[j];
                var titulo_proc = titulos[current_column];
                switch (titulo_proc.ColTipo)
                {
                    case "Int32":
                    case "Int16":
                        int val_a = (int)(row_proc.Item(titulo_proc.Index));
                        result[j, 0] = val_a;
                        
                        break;
                    case "DateTime":
                        DateTime val_b = (DateTime)(row_proc.Item(titulo_proc.Index));
                        result[j, 0] = val_b;
                        break;
                    case "String":
                        string val_c = (string)(row_proc.Item(titulo_proc.Index));
                        result[j, 0] = val_c;
                        break;
                    case "GENERIC_VALUE":
                        var val_d = (SC.GENERIC_VALUE)(row_proc.Item(titulos[current_column].Index));
                        if (titulo_proc.FormatTipo == "CODE")
                            result[j, 0] = val_d.code;
                        else if (titulo_proc.FormatTipo == "DESCRIP")
                            result[j, 0] = val_d.description;
                        else if (titulo_proc.FormatTipo == "ID")
                            result[j, 0] = val_d.id.ToString();
                        else
                            result[j, 0] = val_d.code;
                        break;
                    case "Decimal":
                    case "Double":
                    case "Int64":
                        double val_e = (double)(row_proc.Item(titulo_proc.Index));
                        result[j, 0] = val_e;
                        break;
                }
            }

            return result;
        }

        #region Vigentes Detalle
        public static void vigentes_detalle(int año, int mes, SC.GENERIC_VALUE clase, SC.GENERIC_VALUE zona, SC.GENERIC_VALUE acumulado, SC.SYSTEM sistema)
        {
            var pServ = new ServiceProcess.ServiceAFN();
            var periodo = new Vperiodo(año, mes, acumulado.id);
            var detalle = pServ.reporte_vigentes(periodo, clase, zona, sistema);

            vigentes_detalle_toExcel(detalle, periodo, clase, zona, sistema);

        }
        
        private static void vigentes_detalle_toExcel(List<SC.DETAIL_MOVEMENT> det, ACode.Vperiodo periodo, SC.GENERIC_VALUE clase, SC.GENERIC_VALUE zona, SC.SYSTEM sistema)
        {
            Exc.Application excel;
            Exc.Workbook worKbooK;
            Exc.Worksheet worKsheeT;
            Exc.Range celLrangE;
            int ini_titulo = 1;
            excel = new Exc.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;

            worKbooK = excel.Workbooks.Add();
            worKsheeT = (Exc.Worksheet)worKbooK.ActiveSheet;
            worKsheeT.Name = "Detalle Activo Fijo";

            worKsheeT.Cells[ini_titulo, 4] = sistema.ToString();
            worKsheeT.Cells[ini_titulo, 1] = "Fecha Cierre:";
            worKsheeT.Cells[ini_titulo, 2] = periodo.last.ToShortDateString();

            ini_titulo = ini_titulo + 1;
            worKsheeT.Cells[ini_titulo, 1] = "Zona:";
            worKsheeT.Cells[ini_titulo, 2] = zona.description;
            ini_titulo = ini_titulo + 1;

            worKsheeT.Cells[ini_titulo, 1] = "Clase:";
            worKsheeT.Cells[ini_titulo, 2] = clase.description;
            ini_titulo = ini_titulo + 2;

            TituloCabera[] titulos;
            if (sistema.ENVIORMENT.code == "IFRS")
                titulos = GetTitulosVigentes_IFRS;
            else
                titulos = GetTitulosVigentes;
            int max_columnas = titulos.Count();
            //formato titulos
            celLrangE = worKsheeT.Rows[ini_titulo.ToString()+":"+ini_titulo.ToString()];
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignCenter;        //-4108 equivale a centrar
            celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignCenter;
            celLrangE.WrapText = true;                    //equivale a alinear contenido a la celda
            celLrangE.Orientation = 0;
            celLrangE.AddIndent = false;
            celLrangE.IndentLevel = 0;
            celLrangE.ShrinkToFit = false;
            celLrangE.RowHeight = 30;                     //alto de fila se estable en 30
            celLrangE.Font.Bold = true;
            
            var ini_det = ini_titulo + 1;
            for (int curr_col = 0; curr_col < max_columnas; curr_col++)
            {
                worKsheeT.Cells[ini_titulo, (curr_col+1)].Value = titulos[curr_col].ColTitulo;

                var Imatrix = new int[det.Count, 1];
                var Tmatrix = new string[det.Count, 1];
                var Nmatrix = new double[det.Count, 1];
                var Dmatrix = new DateTime[det.Count, 1];

                for (var j = 0; j < det.Count; j++)
                {
                    var row_proc = det[j];
                    var titulo_proc = titulos[curr_col];
                    switch (titulo_proc.ColTipo)
                    {
                        case "Int32":
                        case "Int16":
                            int val_a = (int)(row_proc.Item(titulo_proc.Index));
                            Imatrix[j, 0] = val_a;
                            break;
                        case "DateTime":
                            DateTime val_b = (DateTime)(row_proc.Item(titulo_proc.Index));
                            Dmatrix[j, 0] = val_b;
                            break;
                        case "String":
                            string val_c = (string)(row_proc.Item(titulo_proc.Index));
                            Tmatrix[j, 0] = val_c;
                            break;
                        case "GENERIC_VALUE":
                            var val_d = (SC.GENERIC_VALUE)(row_proc.Item(titulos[curr_col].Index));
                            if (titulo_proc.FormatTipo == "CODE")
                                Tmatrix[j, 0] = val_d.code;
                            else if (titulo_proc.FormatTipo == "DESCRIP")
                                Tmatrix[j, 0] = val_d.description;
                            else if (titulo_proc.FormatTipo == "ID")
                                Tmatrix[j, 0] = val_d.id.ToString();
                            else
                                Tmatrix[j, 0] = val_d.code;
                            break;
                        case "Decimal":
                        case "Double":
                        case "Int64":
                            double val_e = (double)(row_proc.Item(titulo_proc.Index));
                            Nmatrix[j, 0] = val_e;
                            break;
                    }
                    
                }
                celLrangE = worKsheeT.Range[
                    worKsheeT.Cells[ini_det, (curr_col+1)],
                    worKsheeT.Cells[(ini_det + det.Count - 1), (curr_col+1)]];
                switch (titulos[curr_col].ColTipo) 
                {
                    case "Int32":
                    case "Int16":
                        celLrangE.NumberFormat = "###0;[red]-###0";
                        celLrangE.Value = Imatrix;
                        break;
                    case "DateTime":
                        celLrangE.NumberFormat = "dd-MM-yyyy";
                        celLrangE.Value = Dmatrix;
                        break;
                    case "String":
                    case "GENERIC_VALUE":
                        celLrangE.NumberFormat = "@";
                        celLrangE.Value = Tmatrix;
                        break;
                    case "Decimal":
                    case "Double":
                    case "Int64":
                        celLrangE.NumberFormat = "#,##0;[red]-#,##0";
                        celLrangE.Value = Nmatrix;
                        break;
                }
            }
            worKsheeT.Columns["B:B"].NumberFormat = "#;";   //corrijo formato para columna de codigo articulo
            excel.Visible = true;
            excel.DisplayAlerts = true;
            celLrangE = null;
            worKsheeT = null;
            worKbooK = null;
        }
        
        private static TituloCabera[] GetTitulosVigentes
        {
            get
            {
                return new TituloCabera[] { 
                    new TituloCabera(2,"Fecha de Compra", typeof(DateTime) ) ,
                    new TituloCabera(1,"Codigo del Bien",typeof(int) ) ,
                    new TituloCabera(3,"Descripción breve de los bienes",typeof(string))  ,
                    new TituloCabera(4,"Cantidad", typeof(int) ) ,
                    new TituloCabera(5,"Zona", typeof(SC.GENERIC_VALUE),"CODE" ) ,
                    new TituloCabera(6,"Clase", typeof(SC.GENERIC_VALUE),"CODE" ) ,
                    new TituloCabera(7,"Valor Inicial",  typeof(decimal) ) ,
                    new TituloCabera(20,"%C.M.", typeof(decimal) ) ,
                    new TituloCabera(21,"C. Monetaria Activo",  typeof(decimal) ) ,
                    new TituloCabera(22,"Valor activo actualizado",  typeof(decimal) ) ,
                    new TituloCabera(8,"Credito Adiciones",  typeof(decimal) ) ,
                    new TituloCabera(9,"Valor de Activo Fijo",  typeof(decimal) ) ,
                    new TituloCabera(10,"Dep. Acum. Anterior",  typeof(decimal) ) ,
                    new TituloCabera(23,"C. Monetaria Dep. Acum.",  typeof(decimal) ) ,
                    new TituloCabera(24,"Dep. Acum. Actualizada",  typeof(decimal) ) ,
                    new TituloCabera(11,"Deterioro de Activo",  typeof(decimal) ) ,
                    new TituloCabera(12,"Valor Residual",  typeof(decimal) ) ,
                    new TituloCabera(13,"Valor sujeto a Depreciación",  typeof(decimal) ) ,
                    new TituloCabera(14,"V. Util Asignada",  typeof(int) ) ,
                    new TituloCabera(15,"V. Util Ocupada",  typeof(int) ) ,
                    new TituloCabera(16,"V. Util Residual", typeof(int) ) ,
                    new TituloCabera(17,"Depreciación del Ejercicio", typeof(decimal) ) ,
                    new TituloCabera(18,"Depreciación Acumulada", typeof(decimal) ) ,
                    new TituloCabera(19,"Valor Libro del Activo", typeof(decimal) ) ,
                    //new TituloCabera(25,"Codigo Subzona", typeof(decimal) ) ,
                    //new TituloCabera(26,"Descripción Subzona", typeof(string) ) ,
                    //new TituloCabera(27,"Codigo Gestion", typeof(int) ) ,
                    //new TituloCabera(28,"Descripción Gestion", typeof(string) ) ,
                    //new TituloCabera(29,"Fecha Contabilizacion", typeof(DateTime) ) ,
                    //new TituloCabera(30,"Origen", typeof(string) ) ,
                    //new TituloCabera(31,"Vida Util Inicial", typeof(int) ) ,
                };
            }
        }
        
        private static TituloCabera[] GetTitulosVigentes_IFRS
        {
            get
            {
                return new TituloCabera[] { 
                    new TituloCabera(2,"Fecha de Compra", typeof(DateTime) ) ,
                    new TituloCabera(1,"Codigo del Bien",typeof(int) ) ,
                    new TituloCabera(3,"Descripción breve de los bienes",typeof(string))  ,
                    new TituloCabera(4,"Cantidad", typeof(int) ) ,
                    new TituloCabera(5,"Zona", typeof(SC.GENERIC_VALUE),"CODE" ) ,
                    new TituloCabera(6,"Clase", typeof(SC.GENERIC_VALUE),"CODE" ) ,
                    new TituloCabera(7,"Valor Inicial",  typeof(decimal) ) ,
                    new TituloCabera(25,"Preparacion", typeof(decimal) ) ,
                    new TituloCabera(26,"Desmantelamiento",  typeof(decimal) ) ,
                    new TituloCabera(27,"Transporte",  typeof(decimal) ) ,
                    new TituloCabera(28,"Montaje",  typeof(decimal) ) ,
                    new TituloCabera(29,"Honorarios",  typeof(decimal) ) ,
                    new TituloCabera(8,"Credito Adiciones",  typeof(decimal) ) ,
                    new TituloCabera(9,"Valor de Activo Fijo",  typeof(decimal) ) ,
                    new TituloCabera(10,"Dep. Acum. Anterior",  typeof(decimal) ) ,
                    new TituloCabera(11,"Deterioro de Activo",  typeof(decimal) ) ,
                    new TituloCabera(12,"Valor Residual",  typeof(decimal) ) ,
                    new TituloCabera(13,"Valor sujeto a Depreciación",  typeof(decimal) ) ,
                    new TituloCabera(14,"V. Util Asignada",  typeof(int) ) ,
                    new TituloCabera(15,"V. Util Ocupada",  typeof(int) ) ,
                    new TituloCabera(16,"V. Util Residual", typeof(int) ) ,
                    new TituloCabera(17,"Depreciación del Ejercicio", typeof(decimal) ) ,
                    new TituloCabera(18,"Depreciación Acumulada", typeof(decimal) ) ,
                    new TituloCabera(19,"Valor Libro del Activo", typeof(decimal) ) ,
                    //new TituloCabera(25,"Codigo Subzona", typeof(decimal) ) ,
                    //new TituloCabera(26,"Descripción Subzona", typeof(string) ) ,
                    //new TituloCabera(27,"Codigo Gestion", typeof(int) ) ,
                    //new TituloCabera(28,"Descripción Gestion", typeof(string) ) ,
                    //new TituloCabera(29,"Fecha Contabilizacion", typeof(DateTime) ) ,
                    //new TituloCabera(30,"Origen", typeof(string) ) ,
                    //new TituloCabera(31,"Vida Util Inicial", typeof(int) ) ,
                };
            }
        }

        #endregion

        #region Vigentes Resumen
        public static void vigentes_resumen(int año, int mes, SC.GENERIC_VALUE clase, SC.GENERIC_VALUE zona, SC.GENERIC_VALUE acumulado, SC.SYSTEM sistema)
        {
            var pServ = new ServiceProcess.ServiceAFN();
            var periodo = new Vperiodo(año, mes, acumulado.id);
            var resumen = pServ.reporte_vigente_resumen(periodo, clase, zona, sistema, "C");

            vigentes_resumen_toExcel(resumen, periodo, clase, zona, sistema);

        }
        
        private static void vigentes_resumen_toExcel(List<SC.GROUP_MOVEMENT> det, ACode.Vperiodo periodo, SC.GENERIC_VALUE clase, SC.GENERIC_VALUE zona, SC.SYSTEM sistema)
        {
            Exc.Application excel;
            Exc.Workbook worKbooK;
            Exc.Worksheet worKsheeT;
            Exc.Range celLrangE;
            int ini_titulo = 1;
            excel = new Exc.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;

            worKbooK = excel.Workbooks.Add();
            worKsheeT = (Exc.Worksheet)worKbooK.ActiveSheet;
            worKsheeT.Name = "Resumen Activo Fijo";

            worKsheeT.Cells[ini_titulo, 4] = sistema.ToString();
            worKsheeT.Cells[ini_titulo, 1] = "Fecha Cierre:";
            worKsheeT.Cells[ini_titulo, 2] = periodo.last.ToShortDateString();

            ini_titulo = ini_titulo + 1;
            worKsheeT.Cells[ini_titulo, 1] = "Zona:";
            worKsheeT.Cells[ini_titulo, 2] = zona.description;
            ini_titulo = ini_titulo + 1;

            worKsheeT.Cells[ini_titulo, 1] = "Clase:";
            worKsheeT.Cells[ini_titulo, 2] = clase.description;
            ini_titulo = ini_titulo + 2;

            TituloCabera[] titulos;
            if (sistema.ENVIORMENT.code == "IFRS")
                titulos = GetTitulosVigentesResumen_IFRS;
            else
                titulos = GetTitulosVigentesResumen;
            int max_columnas = titulos.Count();
            //formato titulos
            celLrangE = worKsheeT.Rows[ini_titulo.ToString() + ":" + ini_titulo.ToString()];
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignCenter;        //-4108 equivale a centrar
            celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignCenter;
            celLrangE.WrapText = true;                    //equivale a alinear contenido a la celda
            celLrangE.Orientation = 0;
            celLrangE.AddIndent = false;
            celLrangE.IndentLevel = 0;
            celLrangE.ShrinkToFit = false;
            celLrangE.RowHeight = 30;                     //alto de fila se estable en 30
            celLrangE.Font.Bold = true;

            var ini_det = ini_titulo + 1;
            for (int curr_col = 0; curr_col < max_columnas; curr_col++)
            {
                worKsheeT.Cells[ini_titulo, (curr_col + 1)].Value = titulos[curr_col].ColTitulo;

                var Imatrix = new int[det.Count, 1];
                var Tmatrix = new string[det.Count, 1];
                var Nmatrix = new double[det.Count, 1];
                var Dmatrix = new DateTime[det.Count, 1];

                for (var j = 0; j < det.Count; j++)
                {
                    var row_proc = det[j];
                    var titulo_proc = titulos[curr_col];
                    switch (titulo_proc.ColTipo)
                    {
                        case "Int32":
                        case "Int16":
                            int val_a = (int)(row_proc.Item(titulo_proc.Index));
                            Imatrix[j, 0] = val_a;
                            break;
                        case "DateTime":
                            DateTime val_b = (DateTime)(row_proc.Item(titulo_proc.Index));
                            Dmatrix[j, 0] = val_b;
                            break;
                        case "String":
                            string val_c = (string)(row_proc.Item(titulo_proc.Index));
                            Tmatrix[j, 0] = val_c;
                            break;
                        case "GENERIC_VALUE":
                            var val_d = (SC.GENERIC_VALUE)(row_proc.Item(titulos[curr_col].Index));
                            if (titulo_proc.FormatTipo == "CODE")
                                Tmatrix[j, 0] = val_d.code;
                            else if (titulo_proc.FormatTipo == "DESCRIP")
                                Tmatrix[j, 0] = val_d.description;
                            else if (titulo_proc.FormatTipo == "ID")
                                Tmatrix[j, 0] = val_d.id.ToString();
                            else
                                Tmatrix[j, 0] = val_d.code;
                            break;
                        case "Decimal":
                        case "Double":
                        case "Int64":
                            double val_e = (double)(row_proc.Item(titulo_proc.Index));
                            Nmatrix[j, 0] = val_e;
                            break;
                    }

                }
                celLrangE = worKsheeT.Range[
                    worKsheeT.Cells[ini_det, (curr_col + 1)],
                    worKsheeT.Cells[(ini_det + det.Count - 1), (curr_col + 1)]];
                switch (titulos[curr_col].ColTipo)
                {
                    case "Int32":
                    case "Int16":
                        celLrangE.NumberFormat = "###0;[red]-###0";
                        celLrangE.Value = Imatrix;
                        break;
                    case "DateTime":
                        celLrangE.NumberFormat = "dd-MM-yyyy";
                        celLrangE.Value = Dmatrix;
                        break;
                    case "String":
                    case "GENERIC_VALUE":
                        celLrangE.NumberFormat = "@";
                        celLrangE.Value = Tmatrix;
                        break;
                    case "Decimal":
                    case "Double":
                    case "Int64":
                        celLrangE.NumberFormat = "#,##0;[red]-#,##0";
                        celLrangE.Value = Nmatrix;
                        break;
                }
            }
            //worKsheeT.Columns["B:B"].NumberFormat = "#;";   //corrijo formato para columna de codigo articulo
            excel.Visible = true;
            excel.DisplayAlerts = true;
            celLrangE = null;
            worKsheeT = null;
            worKbooK = null;
        }
        
        private static TituloCabera[] GetTitulosVigentesResumen
        {
            get
            {
                return new TituloCabera[] { 
                    new TituloCabera(1,"Clase", typeof(SC.GENERIC_VALUE), "DESCRIP" ) ,
                    new TituloCabera(2,"Zona",typeof(SC.GENERIC_VALUE), "DESCRIP"  ) ,
                    new TituloCabera(3,"Lugar",typeof(SC.GENERIC_VALUE), "DESCRIP"  ) ,
                    new TituloCabera(4,"Valor Inicial",typeof(decimal))  ,
                    new TituloCabera(5,"C Monetaria Activo",typeof(decimal))  ,
                    new TituloCabera(6,"Credito adiciones", typeof(decimal) ) ,
                    new TituloCabera(7,"Valor de Activo Fijo", typeof(decimal)) ,
                    new TituloCabera(8,"Dep. Acum Anterior", typeof(decimal) ) ,
                    new TituloCabera(9,"C. Monetaria Dep. Acum.",  typeof(decimal) ) ,
                    new TituloCabera(10,"Valor Residual",  typeof(decimal) ) ,
                    new TituloCabera(11,"Depreciación del Ejercicio", typeof(decimal) ) ,
                    new TituloCabera(12,"Depreciación Acumulada",  typeof(decimal) ) ,
                    new TituloCabera(13,"Valor Libro del Activo", typeof(decimal) ) ,
                    //new TituloCabera(25,"Codigo Subzona", typeof(decimal) ) ,
                    //new TituloCabera(26,"Descripción Subzona", typeof(string) ) ,
                    //new TituloCabera(27,"Codigo Gestion", typeof(int) ) ,
                    //new TituloCabera(28,"Descripción Gestion", typeof(string) ) ,
                    //new TituloCabera(29,"Fecha Contabilizacion", typeof(DateTime) ) ,
                    //new TituloCabera(30,"Origen", typeof(string) ) ,
                    //new TituloCabera(31,"Vida Util Inicial", typeof(int) ) ,
                };
            }
        }    
        private static TituloCabera[] GetTitulosVigentesResumen_IFRS
        {
            get
            {
                return new TituloCabera[] { 
                    new TituloCabera(2,"Fecha de Compra", typeof(DateTime) ) ,
                    new TituloCabera(1,"Codigo del Bien",typeof(int) ) ,
                    new TituloCabera(3,"Descripción breve de los bienes",typeof(string))  ,
                    new TituloCabera(4,"Cantidad", typeof(int) ) ,
                    new TituloCabera(5,"Zona", typeof(SC.GENERIC_VALUE),"CODE" ) ,
                    new TituloCabera(6,"Clase", typeof(SC.GENERIC_VALUE),"CODE" ) ,
                    new TituloCabera(7,"Valor Inicial",  typeof(decimal) ) ,
                    new TituloCabera(25,"Preparacion", typeof(decimal) ) ,
                    new TituloCabera(26,"Desmantelamiento",  typeof(decimal) ) ,
                    new TituloCabera(27,"Transporte",  typeof(decimal) ) ,
                    new TituloCabera(28,"Montaje",  typeof(decimal) ) ,
                    new TituloCabera(29,"Honorarios",  typeof(decimal) ) ,
                    new TituloCabera(8,"Credito Adiciones",  typeof(decimal) ) ,
                    new TituloCabera(9,"Valor de Activo Fijo",  typeof(decimal) ) ,
                    new TituloCabera(10,"Dep. Acum. Anterior",  typeof(decimal) ) ,
                    new TituloCabera(11,"Deterioro de Activo",  typeof(decimal) ) ,
                    new TituloCabera(12,"Valor Residual",  typeof(decimal) ) ,
                    new TituloCabera(13,"Valor sujeto a Depreciación",  typeof(decimal) ) ,
                    new TituloCabera(14,"V. Util Asignada",  typeof(int) ) ,
                    new TituloCabera(15,"V. Util Ocupada",  typeof(int) ) ,
                    new TituloCabera(16,"V. Util Residual", typeof(int) ) ,
                    new TituloCabera(17,"Depreciación del Ejercicio", typeof(decimal) ) ,
                    new TituloCabera(18,"Depreciación Acumulada", typeof(decimal) ) ,
                    new TituloCabera(19,"Valor Libro del Activo", typeof(decimal) ) ,
                    //new TituloCabera(25,"Codigo Subzona", typeof(decimal) ) ,
                    //new TituloCabera(26,"Descripción Subzona", typeof(string) ) ,
                    //new TituloCabera(27,"Codigo Gestion", typeof(int) ) ,
                    //new TituloCabera(28,"Descripción Gestion", typeof(string) ) ,
                    //new TituloCabera(29,"Fecha Contabilizacion", typeof(DateTime) ) ,
                    //new TituloCabera(30,"Origen", typeof(string) ) ,
                    //new TituloCabera(31,"Vida Util Inicial", typeof(int) ) ,
                };
            }
        }

        #endregion

        #region Bajas
        public static void bajas_detalle(Vperiodo desde, Vperiodo hasta, int situacion, SC.SYSTEM sistema)
        {
            var pServ = new ServiceProcess.ServiceAFN();
            var detalle = pServ.reporte_bajas(desde, hasta, situacion, sistema);

            bajas_detalle_toExcel(detalle,desde, hasta, situacion, sistema);
        }

        private static void bajas_detalle_toExcel(List<SC.DETAIL_MOVEMENT> det, Vperiodo desde, Vperiodo hasta, int situacion, SC.SYSTEM sistema)
        {
            Exc.Application excel;
            Exc.Workbook worKbooK;
            Exc.Worksheet worKsheeT;
            Exc.Range celLrangE;
            int ini_titulo = 1;
            excel = new Exc.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;

            worKbooK = excel.Workbooks.Add();
            worKsheeT = (Exc.Worksheet)worKbooK.ActiveSheet;
            worKsheeT.Name = "Detalle Bajas";

            worKsheeT.Cells[ini_titulo, 4] = sistema.ToString();
            worKsheeT.Cells[ini_titulo, 1] = "Desde:";
            worKsheeT.Cells[ini_titulo, 2] = desde.first.ToShortDateString();

            ini_titulo = ini_titulo + 1;
            worKsheeT.Cells[ini_titulo, 1] = "Hasta:";
            worKsheeT.Cells[ini_titulo, 2] = hasta.last.ToShortDateString();
            ini_titulo = ini_titulo + 2;

            TituloCabera[] titulos;
            titulos = GetTitulosBajas;
            int max_columnas = titulos.Count();
            //formato titulos
            celLrangE = worKsheeT.Rows[ini_titulo.ToString() + ":" + ini_titulo.ToString()];
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignCenter;        //-4108 equivale a centrar
            celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignCenter;
            celLrangE.WrapText = true;                    //equivale a alinear contenido a la celda
            celLrangE.Orientation = 0;
            celLrangE.AddIndent = false;
            celLrangE.IndentLevel = 0;
            celLrangE.ShrinkToFit = false;
            celLrangE.RowHeight = 30;                     //alto de fila se estable en 30
            celLrangE.Font.Bold = true;

            var ini_det = ini_titulo + 1;
            for (int curr_col = 0; curr_col < max_columnas; curr_col++)
            {
                worKsheeT.Cells[ini_titulo, (curr_col + 1)].Value = titulos[curr_col].ColTitulo;

                var Imatrix = new int[det.Count, 1];
                var Tmatrix = new string[det.Count, 1];
                var Nmatrix = new double[det.Count, 1];
                var Dmatrix = new DateTime[det.Count, 1];

                for (var j = 0; j < det.Count; j++)
                {
                    var row_proc = det[j];
                    var titulo_proc = titulos[curr_col];
                    switch (titulo_proc.ColTipo)
                    {
                        case "Int32":
                        case "Int16":
                            int val_a = (int)(row_proc.Item(titulo_proc.Index));
                            Imatrix[j, 0] = val_a;
                            break;
                        case "DateTime":
                            DateTime val_b = (DateTime)(row_proc.Item(titulo_proc.Index));
                            Dmatrix[j, 0] = val_b;
                            break;
                        case "String":
                            string val_c = (string)(row_proc.Item(titulo_proc.Index));
                            Tmatrix[j, 0] = val_c;
                            break;
                        case "GENERIC_VALUE":
                            var val_d = (SC.GENERIC_VALUE)(row_proc.Item(titulos[curr_col].Index));
                            if(titulo_proc.FormatTipo == "CODE")
                                Tmatrix[j, 0] = val_d.code;
                            else if (titulo_proc.FormatTipo == "DESCRIP")
                                Tmatrix[j, 0] = val_d.description;
                            else if (titulo_proc.FormatTipo == "ID")
                                Tmatrix[j, 0] = val_d.id.ToString();
                            else
                                Tmatrix[j, 0] = val_d.code;
                            
                            break;
                        case "Decimal":
                        case "Double":
                        case "Int64":
                            double val_e = (double)(row_proc.Item(titulo_proc.Index));
                            Nmatrix[j, 0] = val_e;
                            break;
                    }

                }
                celLrangE = worKsheeT.Range[
                    worKsheeT.Cells[ini_det, (curr_col + 1)],
                    worKsheeT.Cells[(ini_det + det.Count - 1), (curr_col + 1)]];
                switch (titulos[curr_col].ColTipo)
                {
                    case "Int32":
                    case "Int16":
                        celLrangE.NumberFormat = "###0;[red]-###0";
                        celLrangE.Value = Imatrix;
                        break;
                    case "DateTime":
                        celLrangE.NumberFormat = "dd-MM-yyyy";
                        celLrangE.Value = Dmatrix;
                        break;
                    case "String":
                    case "GENERIC_VALUE":
                        celLrangE.NumberFormat = "@";
                        celLrangE.Value = Tmatrix;
                        break;
                    case "Decimal":
                    case "Double":
                    case "Int64":
                        celLrangE.NumberFormat = "#,##0;[red]-#,##0";
                        celLrangE.Value = Nmatrix;
                        break;
                }
            }
            worKsheeT.Columns["B:B"].NumberFormat = "#;";   //corrijo formato para columna de codigo articulo
            excel.Visible = true;
            excel.DisplayAlerts = true;
            celLrangE = null;
            worKsheeT = null;
            worKbooK = null;
        }

        private static TituloCabera[] GetTitulosBajas
        {
            get
            {
                return new TituloCabera[]{
                    new TituloCabera(1,"Codigo del Bien",typeof(int)),
                    new TituloCabera(2,"Fecha de Compra",typeof(DateTime)),
                    new TituloCabera(31,"Fecha de Baja",typeof(DateTime)),
                    new TituloCabera(33,"Situación",typeof(SC.GENERIC_VALUE),"DESCRIP"),
                    new TituloCabera(3,"Descripción breve de los bienes",typeof(string)),
                    new TituloCabera(4,"Cantidad",typeof(int)),
                    new TituloCabera(5,"Zona",typeof(SC.GENERIC_VALUE),"CODE"),
                    new TituloCabera(6,"Clase",typeof(SC.GENERIC_VALUE),"CODE"),
                    new TituloCabera(7,"Valor Anterior",typeof(decimal)),
                    new TituloCabera(8,"Credito",typeof(decimal)),
                    new TituloCabera(9,"Valor de Activo Fijo",typeof(decimal)),
                    new TituloCabera(17,"Depreciación Ejercicio",typeof(decimal)),
                    new TituloCabera(18,"Depreciación Acumulada",typeof(decimal)),
                    new TituloCabera(19,"Valor Libro del Activo",typeof(decimal)),
                    new TituloCabera(34,"Codigo Subzona",typeof(SC.GENERIC_VALUE),"CODE"),
                    new TituloCabera(34,"Descripción Subzona",typeof(SC.GENERIC_VALUE),"DESCRIP")
                };
            }
        }
        #endregion

        #region Cuadro Movimiento
        public static void cuadro_movimiento(int año, int mes,  SC.GENERIC_VALUE tipo,  SC.GENERIC_VALUE acumulado, SC.SYSTEM sistema)
        {
            var pServ = new ServiceProcess.ServiceAFN();
            var periodo = new Vperiodo(año, mes, acumulado.id);
            var cuadro_mov = pServ.reporte_cuadro_movimiento(periodo, tipo, sistema);

            cuadro_movimiento_toExcel(cuadro_mov, periodo, tipo, acumulado, sistema);
        }

        private static void cuadro_movimiento_toExcel(List<SC.GROUP_MOVEMENT> det, Vperiodo periodo, SC.GENERIC_VALUE tipo, SC.GENERIC_VALUE acumulado, SC.SYSTEM sistema)
        {
            Exc.Application excel;
            Exc.Workbook worKbooK;
            Exc.Worksheet worKsheeT;
            Exc.Range celLrangE;
            int ini_titulo = 1;
            excel = new Exc.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;

            worKbooK = excel.Workbooks.Add();
            worKsheeT = (Exc.Worksheet)worKbooK.ActiveSheet;
            worKsheeT.Name = "CMov_" + sistema.ToString();

            worKsheeT.Cells[ini_titulo, 4] = sistema.ToString();
            worKsheeT.Cells[ini_titulo, 1] = "Desde:";
            worKsheeT.Cells[ini_titulo, 2] = periodo.first.ToShortDateString();

            ini_titulo = ini_titulo + 1;
            worKsheeT.Cells[ini_titulo, 1] = "Hasta:";
            worKsheeT.Cells[ini_titulo, 2] = periodo.last.ToShortDateString();
            ini_titulo = ini_titulo + 2;

            TituloCabera[] titulos;
            titulos = GetTitulosCuadroMovimiento;
            int max_columnas = titulos.Count();
            //formato titulos
            celLrangE = worKsheeT.Rows[ini_titulo.ToString() + ":" + ini_titulo.ToString()];
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignCenter;        //-4108 equivale a centrar
            celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignCenter;
            celLrangE.WrapText = true;                    //equivale a alinear contenido a la celda
            celLrangE.Orientation = 0;
            celLrangE.AddIndent = false;
            celLrangE.IndentLevel = 0;
            celLrangE.ShrinkToFit = false;
            celLrangE.RowHeight = 30;                     //alto de fila se estable en 30
            celLrangE.Font.Bold = true;

            var ini_det = ini_titulo + 1;
            for (int curr_col = 0; curr_col < max_columnas; curr_col++)
            {
                worKsheeT.Cells[ini_titulo, (curr_col + 1)].Value = titulos[curr_col].ColTitulo;

                var Imatrix = new int[det.Count, 1];
                var Tmatrix = new string[det.Count, 1];
                var Nmatrix = new double[det.Count, 1];
                var Dmatrix = new DateTime[det.Count, 1];

                for (var j = 0; j < det.Count; j++)
                {
                    var row_proc = det[j];
                    var titulo_proc = titulos[curr_col];
                    switch (titulo_proc.ColTipo)
                    {
                        case "Int32":
                        case "Int16":
                            int val_a = (int)(row_proc.Item(titulo_proc.Index));
                            Imatrix[j, 0] = val_a;
                            break;
                        case "DateTime":
                            DateTime val_b = (DateTime)(row_proc.Item(titulo_proc.Index));
                            Dmatrix[j, 0] = val_b;
                            break;
                        case "String":
                            string val_c = (string)(row_proc.Item(titulo_proc.Index));
                            Tmatrix[j, 0] = val_c;
                            break;
                        case "GENERIC_VALUE":
                            var val_d = (SC.GENERIC_VALUE)(row_proc.Item(titulos[curr_col].Index));
                            if (titulo_proc.FormatTipo == "CODE")
                                Tmatrix[j, 0] = val_d.code;
                            else if (titulo_proc.FormatTipo == "DESCRIP")
                                Tmatrix[j, 0] = val_d.description;
                            else if (titulo_proc.FormatTipo == "ID")
                                Tmatrix[j, 0] = val_d.id.ToString();
                            else
                                Tmatrix[j, 0] = val_d.code;

                            break;
                        case "Decimal":
                        case "Double":
                        case "Int64":
                            double val_e = (double)(row_proc.Item(titulo_proc.Index));
                            Nmatrix[j, 0] = val_e;
                            break;
                    }

                }
                celLrangE = worKsheeT.Range[
                    worKsheeT.Cells[ini_det, (curr_col + 1)],
                    worKsheeT.Cells[(ini_det + det.Count - 1), (curr_col + 1)]];
                switch (titulos[curr_col].ColTipo)
                {
                    case "Int32":
                    case "Int16":
                        celLrangE.NumberFormat = "###0;[red]-###0";
                        celLrangE.Value = Imatrix;
                        break;
                    case "DateTime":
                        celLrangE.NumberFormat = "dd-MM-yyyy";
                        celLrangE.Value = Dmatrix;
                        break;
                    case "String":
                    case "GENERIC_VALUE":
                        celLrangE.NumberFormat = "@";
                        celLrangE.Value = Tmatrix;
                        break;
                    case "Decimal":
                    case "Double":
                    case "Int64":
                        celLrangE.NumberFormat = "#,##0;[red]-#,##0";
                        celLrangE.Value = Nmatrix;
                        break;
                }
            }
            worKsheeT.Columns["B:B"].NumberFormat = "#;";   //corrijo formato para columna de codigo articulo
            excel.Visible = true;
            excel.DisplayAlerts = true;
            celLrangE = null;
            worKsheeT = null;
            worKbooK = null;
        }
        
        private static TituloCabera[] GetTitulosCuadroMovimiento
        {
            get
            {
                return new TituloCabera[]{
                    new TituloCabera(1,"Cuenta",typeof(SC.GENERIC_VALUE),"DESCRIP"),
                    new TituloCabera(17,"Saldo Inicial",typeof(double)),
                    new TituloCabera(18,"Adiciones",typeof(double)),
                    new TituloCabera(19,"Desde Obras en Construccion",typeof(double)),
                    new TituloCabera(5,"Hacia Activo Fijo",typeof(double)),
                    new TituloCabera(6,"Credito",typeof(double)),
                    new TituloCabera(20,"Castigos",typeof(double),"CODE"),
                    new TituloCabera(21,"Ventas",typeof(double),"CODE"),
                    new TituloCabera(7,"Saldo Final",typeof(double)),
                    new TituloCabera(1,"Cuenta",typeof(SC.GENERIC_VALUE),"DESCRIP"),
                    new TituloCabera(8,"Saldo Inicial",typeof(double)),
                    new TituloCabera(11,"Dep. Ejercicio",typeof(double)),
                    new TituloCabera(9,"C Mon",typeof(double)),
                    new TituloCabera(22,"Castigos",typeof(double)),
                    new TituloCabera(23,"Ventas",typeof(double)),
                    new TituloCabera(12,"Saldo Final",typeof(double)),
                    new TituloCabera(13,"Valor Neto",typeof(double))
                };
            }
        }
        #endregion

        #region Obras en Construccion
        public static void obc_detalle(SC.GENERIC_VALUE reporte, int año, int mes, SC.GENERIC_VALUE moneda, SC.GENERIC_VALUE acumulado)
        {
            var pServ = new ServiceProcess.ServiceAFN();
            DateTime fecha_proceso = new ACode.Vperiodo(año, mes).last;
            List<SC.DETAIL_OBC> detalle;
            switch (reporte.id)
            {
                case 1:
                    detalle = pServ.saldo_obras(fecha_proceso, moneda);
                    break;
                case 2:
                    detalle = pServ.saldo_obras(fecha_proceso, moneda);
                    break;
                default:
                    detalle = pServ.saldo_obras(fecha_proceso, moneda);
                    break;
            }
            obc_detalle_toExcel(detalle, fecha_proceso,moneda);
        }
        private static void obc_detalle_toExcel(List<SC.DETAIL_OBC> data, DateTime fecha, SC.GENERIC_VALUE moneda)
        {
            Exc.Application excel;
            Exc.Workbook worKbooK;
            Exc.Worksheet worKsheeT;
            Exc.Range celLrangE;
            int ini_titulo = 1;
            excel = new Exc.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;

            worKbooK = excel.Workbooks.Add();
            worKsheeT = (Exc.Worksheet)worKbooK.ActiveSheet;
            worKsheeT.Name = "Saldo OBC";

            //worKsheeT.Cells[ini_titulo, 4] = sistema.ToString();
            //worKsheeT.Cells[ini_titulo, 1] = "Fecha Cierre:";
            //worKsheeT.Cells[ini_titulo, 2] = periodo.last.ToShortDateString();

            //ini_titulo = ini_titulo + 1;
            //worKsheeT.Cells[ini_titulo, 1] = "Zona:";
            //worKsheeT.Cells[ini_titulo, 2] = zona.description;
            //ini_titulo = ini_titulo + 1;

            //worKsheeT.Cells[ini_titulo, 1] = "Clase:";
            //worKsheeT.Cells[ini_titulo, 2] = clase.description;
            //ini_titulo = ini_titulo + 2;

            TituloCabera[] titulos;
            titulos = obc_detalle_saldos_titulos;
            int max_columnas = titulos.Count();
            //formato titulos
            celLrangE = worKsheeT.Rows[ini_titulo.ToString() + ":" + ini_titulo.ToString()];
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignCenter;        //-4108 equivale a centrar
            celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignCenter;
            celLrangE.WrapText = true;                    //equivale a alinear contenido a la celda
            celLrangE.Orientation = 0;
            celLrangE.AddIndent = false;
            celLrangE.IndentLevel = 0;
            celLrangE.ShrinkToFit = false;
            celLrangE.RowHeight = 30;                     //alto de fila se estable en 30
            celLrangE.Font.Bold = true;

            var ini_det = ini_titulo + 1;
            for (int curr_col = 0; curr_col < max_columnas; curr_col++)
            {
                worKsheeT.Cells[ini_titulo, (curr_col + 1)].Value = titulos[curr_col].ColTitulo;

                var x = RawDataColumn(data, titulos, curr_col);

                //for (var j = 0; j < det.Count; j++)
                //{
                //    var row_proc = det[j];
                //    var titulo_proc = titulos[curr_col];
                //    switch (titulo_proc.ColTipo)
                //    {
                //        case "Int32":
                //        case "Int16":
                //            int val_a = (int)(row_proc.Item(titulo_proc.Index));
                //            Imatrix[j, 0] = val_a;
                //            break;
                //        case "DateTime":
                //            DateTime val_b = (DateTime)(row_proc.Item(titulo_proc.Index));
                //            Dmatrix[j, 0] = val_b;
                //            break;
                //        case "String":
                //            string val_c = (string)(row_proc.Item(titulo_proc.Index));
                //            Tmatrix[j, 0] = val_c;
                //            break;
                //        case "GENERIC_VALUE":
                //            var val_d = (SC.GENERIC_VALUE)(row_proc.Item(titulos[curr_col].Index));
                //            if (titulo_proc.FormatTipo == "CODE")
                //                Tmatrix[j, 0] = val_d.code;
                //            else if (titulo_proc.FormatTipo == "DESCRIP")
                //                Tmatrix[j, 0] = val_d.description;
                //            else if (titulo_proc.FormatTipo == "ID")
                //                Tmatrix[j, 0] = val_d.id.ToString();
                //            else
                //                Tmatrix[j, 0] = val_d.code;
                //            break;
                //        case "Decimal":
                //        case "Double":
                //        case "Int64":
                //            double val_e = (double)(row_proc.Item(titulo_proc.Index));
                //            Nmatrix[j, 0] = val_e;
                //            break;
                //    }

                //}
                celLrangE = worKsheeT.Range[
                    worKsheeT.Cells[ini_det, (curr_col + 1)],
                    worKsheeT.Cells[(ini_det + data.Count - 1), (curr_col + 1)]];
                switch (titulos[curr_col].ColTipo)
                {
                    case "Int32":
                    case "Int16":
                        celLrangE.NumberFormat = "###0;[red]-###0";
                        celLrangE.Value = x;
                        break;
                    case "DateTime":
                        celLrangE.NumberFormat = "dd-MM-yyyy";
                        celLrangE.Value = x;
                        break;
                    case "String":
                    case "GENERIC_VALUE":
                        celLrangE.NumberFormat = "@";
                        celLrangE.Value = x;
                        break;
                    case "Decimal":
                    case "Double":
                    case "Int64":
                        celLrangE.NumberFormat = "#,##0;[red]-#,##0";
                        celLrangE.Value = x;
                        break;
                }
            }
            //worKsheeT.Columns["B:B"].NumberFormat = "#;";   //corrijo formato para columna de codigo articulo
            excel.Visible = true;
            excel.DisplayAlerts = true;
            celLrangE = null;
            worKsheeT = null;
            worKbooK = null;
        }
        private static TituloCabera[] obc_detalle_saldos_titulos
        {
            get
            {
                return new TituloCabera[] 
                { 
                    new TituloCabera(1,"Codigo Movimiento",typeof(int)),
                    new TituloCabera(2,"Descripción",typeof(string)),
                    new TituloCabera(3,"Fecha Documento",typeof(DateTime)),
                    new TituloCabera(4,"Fecha Contable",typeof(DateTime)),
                    new TituloCabera(5,"Zona",typeof(SC.GENERIC_VALUE),"CODE"),
                    new TituloCabera(6,"Monto Documento",typeof(double)),
                    new TituloCabera(7,"Monto Utilizado",typeof(double)),
                    new TituloCabera(8,"Saldo Final", typeof(double) ),
                    
                };
            }
        }
        #endregion
    }
}
