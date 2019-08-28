using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACode;

using SC = AFN_WF_C.ServiceProcess.DataContract;
using SV = AFN_WF_C.ServiceProcess.DataView;
using Exc = Microsoft.Office.Interop.Excel;
using AFN_WF_C.PCClient.Procesos.Estructuras;

namespace AFN_WF_C.PCClient.Procesos
{
    internal class Reportes
    {
        private static ColumnData RawDataColumn(IEnumerable<SC.IElemento> detail, TituloCabera titulo)
        {
            return RawDataColumn(detail.ToList(), titulo);
        }
        private static ColumnData RawDataColumn(List<SC.IElemento> detail, TituloCabera titulo)
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
                        var val_d = (SC.GENERIC_VALUE)selected;
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
                    default :
                        result[j, 0] = selected;
                        break;
                }
            }

            return new ColumnData { Valores = result, ColType = tipo };
        }

        private static string FormatType(string tipo)
        {
            switch (tipo)
            {
                case "Int32":
                case "Int16":
                    return "###0;[red]-###0";
                case "DateTime":
                    return "dd-MM-yyyy";
                case "String":
                case "GENERIC_VALUE":
                    return "@";
                case "Decimal":
                case "Double":
                case "Int64":
                    return "#,##0;[red]-#,##0";
                default:
                    return string.Empty;
            }
        }
        private static string FormatType(Type tipo){
            return FormatType(tipo.Name);
        }

        #region Vigentes Detalle
        public static void vigentes_detalle(int año, int mes, SC.GENERIC_VALUE clase, SC.GENERIC_VALUE zona, SC.GENERIC_VALUE acumulado, SV.SV_SYSTEM sistema)
        {
            var pServ = new ServiceProcess.ServiceAFN();
            var periodo = new Vperiodo(año, mes, acumulado.id);
            var detalle = pServ.reporte_vigentes(periodo, clase, zona, sistema);

            vigentes_detalle_toExcel(detalle, periodo, clase, zona, sistema);

        }

        private static void vigentes_detalle_toExcel(List<SC.DETAIL_MOVEMENT> det, ACode.Vperiodo periodo, SC.GENERIC_VALUE clase, SC.GENERIC_VALUE zona, SV.SV_SYSTEM sistema)
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
                var titulo = titulos[curr_col];
                var proceso = RawDataColumn(det, titulo);

                worKsheeT.Cells[ini_titulo, (curr_col + 1)].Value = titulo.ColTitulo;

                celLrangE = worKsheeT.Range[
                    worKsheeT.Cells[ini_det, (curr_col + 1)],
                    worKsheeT.Cells[(ini_det + det.Count - 1), (curr_col + 1)]];

                celLrangE.NumberFormat = FormatType(proceso.TName);
                celLrangE.Value = proceso.Valores;
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
                    new TituloCabera(2,"Fecha de Compra") ,
                    new TituloCabera(1,"Codigo del Bien") ,
                    new TituloCabera(3,"Descripción breve de los bienes")  ,
                    new TituloCabera(4,"Cantidad") ,
                    new TituloCabera(5,"Zona","CODE" ) ,
                    new TituloCabera(6,"Clase","CODE" ) ,
                    new TituloCabera(7,"Valor Inicial") ,
                    new TituloCabera(20,"%C.M.") ,
                    new TituloCabera(21,"C. Monetaria Activo") ,
                    new TituloCabera(22,"Valor activo actualizado") ,
                    new TituloCabera(8,"Credito Adiciones") ,
                    new TituloCabera(9,"Valor de Activo Fijo") ,
                    new TituloCabera(10,"Dep. Acum. Anterior") ,
                    new TituloCabera(23,"C. Monetaria Dep. Acum.") ,
                    new TituloCabera(24,"Dep. Acum. Actualizada") ,
                    new TituloCabera(11,"Deterioro de Activo") ,
                    new TituloCabera(12,"Valor Residual") ,
                    new TituloCabera(13,"Valor sujeto a Depreciación") ,
                    new TituloCabera(14,"V. Util Asignada") ,
                    new TituloCabera(15,"V. Util Ocupada") ,
                    new TituloCabera(16,"V. Util Residual") ,
                    new TituloCabera(17,"Depreciación del Ejercicio") ,
                    new TituloCabera(18,"Depreciación Acumulada") ,
                    new TituloCabera(19,"Valor Libro del Activo") ,
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
                    new TituloCabera(2,"Fecha de Compra") ,
                    new TituloCabera(1,"Codigo del Bien") ,
                    new TituloCabera(3,"Descripción breve de los bienes")  ,
                    new TituloCabera(4,"Cantidad") ,
                    new TituloCabera(5,"Zona","CODE" ) ,
                    new TituloCabera(6,"Clase","CODE" ) ,
                    new TituloCabera(7,"Valor Inicial") ,
                    new TituloCabera(25,"Preparacion") ,
                    new TituloCabera(26,"Desmantelamiento") ,
                    new TituloCabera(27,"Transporte") ,
                    new TituloCabera(28,"Montaje") ,
                    new TituloCabera(29,"Honorarios") ,
                    new TituloCabera(8,"Credito Adiciones") ,
                    new TituloCabera(9,"Valor de Activo Fijo") ,
                    new TituloCabera(10,"Dep. Acum. Anterior") ,
                    new TituloCabera(11,"Deterioro de Activo") ,
                    new TituloCabera(12,"Valor Residual") ,
                    new TituloCabera(13,"Valor sujeto a Depreciación") ,
                    new TituloCabera(14,"V. Util Asignada") ,
                    new TituloCabera(15,"V. Util Ocupada") ,
                    new TituloCabera(16,"V. Util Residual") ,
                    new TituloCabera(17,"Depreciación del Ejercicio") ,
                    new TituloCabera(18,"Depreciación Acumulada") ,
                    new TituloCabera(19,"Valor Libro del Activo") ,
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
        public static void vigentes_resumen(int año, int mes, SC.GENERIC_VALUE clase, SC.GENERIC_VALUE zona, SC.GENERIC_VALUE acumulado, SV.SV_SYSTEM sistema)
        {
            var pServ = new ServiceProcess.ServiceAFN();
            var periodo = new Vperiodo(año, mes, acumulado.id);
            var resumen = pServ.reporte_vigente_resumen(periodo, clase, zona, sistema, "C");

            vigentes_resumen_toExcel(resumen, periodo, clase, zona, sistema);

        }

        private static void vigentes_resumen_toExcel(List<SC.GROUP_MOVEMENT> det, ACode.Vperiodo periodo, SC.GENERIC_VALUE clase, SC.GENERIC_VALUE zona, SV.SV_SYSTEM sistema)
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
                var titulo = titulos[curr_col];
                var proceso = RawDataColumn(det, titulo);

                worKsheeT.Cells[ini_titulo, (curr_col + 1)].Value = titulo.ColTitulo;

                celLrangE = worKsheeT.Range[
                    worKsheeT.Cells[ini_det, (curr_col + 1)],
                    worKsheeT.Cells[(ini_det + det.Count - 1), (curr_col + 1)]];

                celLrangE.NumberFormat = FormatType(proceso.TName);
                celLrangE.Value = proceso.Valores;
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
                    new TituloCabera(1,"Clase", "DESCRIP" ) ,
                    new TituloCabera(2,"Zona", "DESCRIP"  ) ,
                    new TituloCabera(3,"Lugar", "DESCRIP"  ) ,
                    new TituloCabera(4,"Valor Inicial")  ,
                    new TituloCabera(5,"C Monetaria Activo")  ,
                    new TituloCabera(6,"Credito adiciones") ,
                    new TituloCabera(7,"Valor de Activo Fijo") ,
                    new TituloCabera(8,"Dep. Acum Anterior") ,
                    new TituloCabera(9,"C. Monetaria Dep. Acum.") ,
                    new TituloCabera(10,"Valor Residual") ,
                    new TituloCabera(11,"Depreciación del Ejercicio") ,
                    new TituloCabera(12,"Depreciación Acumulada") ,
                    new TituloCabera(13,"Valor Libro del Activo") ,
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
                    new TituloCabera(2,"Fecha de Compra") ,
                    new TituloCabera(1,"Codigo del Bien") ,
                    new TituloCabera(3,"Descripción breve de los bienes")  ,
                    new TituloCabera(4,"Cantidad") ,
                    new TituloCabera(5,"Zona","CODE" ) ,
                    new TituloCabera(6,"Clase","CODE" ) ,
                    new TituloCabera(7,"Valor Inicial") ,
                    new TituloCabera(25,"Preparacion") ,
                    new TituloCabera(26,"Desmantelamiento") ,
                    new TituloCabera(27,"Transporte") ,
                    new TituloCabera(28,"Montaje") ,
                    new TituloCabera(29,"Honorarios") ,
                    new TituloCabera(8,"Credito Adiciones") ,
                    new TituloCabera(9,"Valor de Activo Fijo") ,
                    new TituloCabera(10,"Dep. Acum. Anterior") ,
                    new TituloCabera(11,"Deterioro de Activo") ,
                    new TituloCabera(12,"Valor Residual") ,
                    new TituloCabera(13,"Valor sujeto a Depreciación") ,
                    new TituloCabera(14,"V. Util Asignada") ,
                    new TituloCabera(15,"V. Util Ocupada") ,
                    new TituloCabera(16,"V. Util Residual") ,
                    new TituloCabera(17,"Depreciación del Ejercicio") ,
                    new TituloCabera(18,"Depreciación Acumulada") ,
                    new TituloCabera(19,"Valor Libro del Activo") ,
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
        public static void bajas_detalle(Vperiodo desde, Vperiodo hasta, int situacion, SV.SV_SYSTEM sistema)
        {
            var pServ = new ServiceProcess.ServiceAFN();
            var detalle = pServ.reporte_bajas(desde, hasta, situacion, sistema);

            bajas_detalle_toExcel(detalle,desde, hasta, situacion, sistema);
        }

        private static void bajas_detalle_toExcel(List<SC.DETAIL_MOVEMENT> det, Vperiodo desde, Vperiodo hasta, int situacion, SV.SV_SYSTEM sistema)
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

                var titulo = titulos[curr_col];
                var proceso = RawDataColumn(det, titulo);

                worKsheeT.Cells[ini_titulo, (curr_col + 1)].Value = titulo.ColTitulo;

                celLrangE = worKsheeT.Range[
                    worKsheeT.Cells[ini_det, (curr_col + 1)],
                    worKsheeT.Cells[(ini_det + det.Count - 1), (curr_col + 1)]];

                celLrangE.NumberFormat = FormatType(proceso.TName);
                celLrangE.Value = proceso.Valores;
            }
            //worKsheeT.Columns["B:B"].NumberFormat = "#;";   //corrijo formato para columna de codigo articulo
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
                    new TituloCabera(1,"Codigo del Bien"),
                    new TituloCabera(2,"Fecha de Compra"),
                    new TituloCabera(31,"Fecha de Baja"),
                    new TituloCabera(33,"Situación","DESCRIP"),
                    new TituloCabera(3,"Descripción breve de los bienes"),
                    new TituloCabera(4,"Cantidad"),
                    new TituloCabera(5,"Zona","CODE"),
                    new TituloCabera(6,"Clase","CODE"),
                    new TituloCabera(7,"Valor Anterior"),
                    new TituloCabera(8,"Credito"),
                    new TituloCabera(9,"Valor de Activo Fijo"),
                    new TituloCabera(17,"Depreciación Ejercicio"),
                    new TituloCabera(18,"Depreciación Acumulada"),
                    new TituloCabera(19,"Valor Libro del Activo"),
                    new TituloCabera(34,"Codigo Subzona","CODE"),
                    new TituloCabera(34,"Descripción Subzona","DESCRIP")
                };
            }
        }
        #endregion

        #region Cuadro Movimiento
        public static void cuadro_movimiento(int año, int mes, SC.GENERIC_VALUE tipo, SC.GENERIC_VALUE acumulado, SV.SV_SYSTEM sistema)
        {
            var pServ = new ServiceProcess.ServiceAFN();
            var periodo = new Vperiodo(año, mes, acumulado.id);
            var cuadro_mov = pServ.reporte_cuadro_movimiento(periodo, tipo, sistema);

            cuadro_movimiento_toExcel(cuadro_mov, periodo, tipo, acumulado, sistema);
            
        }
        private static void cuadro_movimiento_toExcel(List<SC.GROUP_MOVEMENT> det, Vperiodo periodo, SC.GENERIC_VALUE tipo, SC.GENERIC_VALUE acumulado, SV.SV_SYSTEM sistema)
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
                var titulo = titulos[curr_col];
                var proceso = RawDataColumn(det, titulo);

                worKsheeT.Cells[ini_titulo, (curr_col + 1)].Value = titulo.ColTitulo;

                celLrangE = worKsheeT.Range[
                    worKsheeT.Cells[ini_det, (curr_col + 1)],
                    worKsheeT.Cells[(ini_det + det.Count - 1), (curr_col + 1)]];

                celLrangE.NumberFormat = FormatType(proceso.TName);
                celLrangE.Value = proceso.Valores;
            }

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
                    new TituloCabera(1,"Cuenta","DESCRIP"),
                    new TituloCabera(17,"Saldo Inicial"),
                    new TituloCabera(18,"Adiciones"),
                    new TituloCabera(19,"Desde Obras en Construccion"),
                    new TituloCabera(5,"Hacia Activo Fijo"),
                    new TituloCabera(6,"Credito"),
                    new TituloCabera(20,"Castigos","CODE"),
                    new TituloCabera(21,"Ventas","CODE"),
                    new TituloCabera(7,"Saldo Final"),
                    new TituloCabera(1,"Cuenta","DESCRIP"),
                    new TituloCabera(8,"Saldo Inicial"),
                    new TituloCabera(11,"Dep. Ejercicio"),
                    new TituloCabera(9,"C Mon"),
                    new TituloCabera(22,"Castigos"),
                    new TituloCabera(23,"Ventas"),
                    new TituloCabera(12,"Saldo Final"),
                    new TituloCabera(13,"Valor Neto")
                };
            }
        }
        #endregion

        #region Fixed Assets
        public static void fixed_assets(int año, int mes, SC.GENERIC_VALUE tipo, int acumulado, SV.SV_SYSTEM sistema)
        {
            var pServ = new ServiceProcess.ServiceAFN();
            var periodo = new Vperiodo(año, mes, acumulado);
            var fix_rep = pServ.reporte_fixed_assets(periodo, tipo, sistema);

            fixed_assets_toExcel(fix_rep, periodo, tipo, acumulado, sistema);

        }
        private static void fixed_assets_toExcel(List<SC.GROUP_MOVEMENT> det, Vperiodo periodo, SC.GENERIC_VALUE tipo, int acumulado, SV.SV_SYSTEM sistema)
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
            worKsheeT.Name = "FA_" + sistema.ToString();

            worKsheeT.Cells[ini_titulo, 4] = sistema.ToString();
            worKsheeT.Cells[ini_titulo, 1] = "Desde:";
            worKsheeT.Cells[ini_titulo, 2] = periodo.first.ToShortDateString();

            ini_titulo = ini_titulo + 1;
            worKsheeT.Cells[ini_titulo, 1] = "Hasta:";
            worKsheeT.Cells[ini_titulo, 2] = periodo.last.ToShortDateString();
            ini_titulo = ini_titulo + 2;

            TituloCabera[] titulos;
            titulos = GetTitulosFixedAssets;
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
                var titulo = titulos[curr_col];
                var proceso = RawDataColumn(det, titulo);

                worKsheeT.Cells[ini_titulo, (curr_col + 1)].Value = titulo.ColTitulo;

                celLrangE = worKsheeT.Range[
                    worKsheeT.Cells[ini_det, (curr_col + 1)],
                    worKsheeT.Cells[(ini_det + det.Count - 1), (curr_col + 1)]];

                celLrangE.NumberFormat = FormatType(proceso.TName);
                celLrangE.Value = proceso.Valores;
            }

            excel.Visible = true;
            excel.DisplayAlerts = true;
            celLrangE = null;
            worKsheeT = null;
            worKbooK = null;
        }
        private static TituloCabera[] GetTitulosFixedAssets
        {
            get
            {
                return new TituloCabera[]{
                    new TituloCabera(1,"Cuenta","DESCRIP"),
                    new TituloCabera(17,"Saldo Inicial"),
                    new TituloCabera(18,"Adiciones"),
                    new TituloCabera(19,"Desde Obras en Construccion"),
                    new TituloCabera(5,"Hacia Activo Fijo"),
                    new TituloCabera(6,"Credito"),
                    new TituloCabera(20,"Castigos","CODE"),
                    new TituloCabera(21,"Ventas","CODE"),
                    new TituloCabera(7,"Saldo Final"),
                    new TituloCabera(1,"Cuenta","DESCRIP"),
                    new TituloCabera(8,"Saldo Inicial"),
                    new TituloCabera(11,"Dep. Ejercicio"),
                    new TituloCabera(9,"C Mon"),
                    new TituloCabera(22,"Castigos"),
                    new TituloCabera(23,"Ventas"),
                    new TituloCabera(12,"Saldo Final"),
                    new TituloCabera(13,"Valor Neto")
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
                TituloCabera procesando = titulos[curr_col];
                worKsheeT.Cells[ini_titulo, (curr_col + 1)].Value = procesando.ColTitulo;

                var proceso = RawDataColumn(data, procesando);

                celLrangE = worKsheeT.Range[
                    worKsheeT.Cells[ini_det, (curr_col + 1)],
                    worKsheeT.Cells[(ini_det + data.Count - 1), (curr_col + 1)]];
                celLrangE.NumberFormat = FormatType(proceso.TName);
                celLrangE.Value = proceso.Valores;
                
            }
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
                    new TituloCabera(1,"Codigo Movimiento"),
                    new TituloCabera(2,"Descripción"),
                    new TituloCabera(3,"Fecha Documento"),
                    new TituloCabera(4,"Fecha Contable"),
                    new TituloCabera(5,"Zona","CODE"),
                    new TituloCabera(6,"Monto Documento"),
                    new TituloCabera(7,"Monto Utilizado"),
                    new TituloCabera(8,"Saldo Final"),
                };
            }
        }
        #endregion
    }
}
