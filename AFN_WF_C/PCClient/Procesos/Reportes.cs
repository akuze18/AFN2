using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACode;
using AFN_WF_C.PCClient.Procesos.Estructuras;
using Exc = Microsoft.Office.Interop.Excel;
using SV = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Procesos
{
    internal class Reportes
    {
        private static ColumnData RawDataColumn(IEnumerable<SV.IElemento> detail, TituloCabera titulo)
        {
            return RawDataColumn(detail.ToList(), titulo);
        }
        private static ColumnData RawDataColumn(List<SV.IElemento> detail, TituloCabera titulo)
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
                        var val_d = (SV.GENERIC_VALUE)selected;
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
        public static void vigentes_detalle(int año, int mes, SV.GENERIC_VALUE clase, SV.GENERIC_VALUE zona, SV.GENERIC_VALUE acumulado, SV.SV_SYSTEM sistema)
        {
            List<SV.DETAIL_MOVEMENT> detalle;
            var periodo = new Vperiodo(año, mes, acumulado.id);
            using (var cServ = new ServiceProcess.ServiceAFN2())
                detalle = cServ.Proceso.reporte_vigentes(periodo, clase, zona, sistema);

            vigentes_detalle_toExcel(detalle, periodo, clase, zona, sistema);

        }

        private static void vigentes_detalle_toExcel(List<SV.DETAIL_MOVEMENT> det, ACode.Vperiodo periodo, SV.GENERIC_VALUE clase, SV.GENERIC_VALUE zona, SV.SV_SYSTEM sistema)
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
        public static void vigentes_resumen(int año, int mes, SV.GENERIC_VALUE clase, SV.GENERIC_VALUE zona, SV.GENERIC_VALUE acumulado, SV.SV_SYSTEM sistema)
        {
            List<SV.GROUP_MOVEMENT> resumen;
            var periodo = new Vperiodo(año, mes, acumulado.id);
            using (var pServ = new ServiceProcess.ServiceAFN2())
            {
                resumen = pServ.Proceso.reporte_vigente_resumen(periodo, clase, zona, sistema, "C");
            }
            vigentes_resumen_toExcel(resumen, periodo, clase, zona, sistema);

        }

        private static void vigentes_resumen_toExcel(List<SV.GROUP_MOVEMENT> det, ACode.Vperiodo periodo, SV.GENERIC_VALUE clase, SV.GENERIC_VALUE zona, SV.SV_SYSTEM sistema)
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
            List<SV.DETAIL_MOVEMENT> detalle;
            using (var cServ = new ServiceProcess.ServiceAFN2())
                detalle = cServ.Proceso.reporte_bajas(desde, hasta, situacion, sistema);

            bajas_detalle_toExcel(detalle,desde, hasta, situacion, sistema);
        }

        private static void bajas_detalle_toExcel(List<SV.DETAIL_MOVEMENT> det, Vperiodo desde, Vperiodo hasta, int situacion, SV.SV_SYSTEM sistema)
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
        public static void cuadro_movimiento(int año, int mes, SV.GENERIC_VALUE tipo, SV.GENERIC_VALUE acumulado, SV.SV_SYSTEM sistema)
        {
            List<SV.GROUP_MOVEMENT> cuadro_mov;
            var periodo = new Vperiodo(año, mes, acumulado.id);
            using (var cServ = new ServiceProcess.ServiceAFN2())
                cuadro_mov = cServ.Proceso.reporte_cuadro_movimiento(periodo, tipo, sistema);

            cuadro_movimiento_toExcel(cuadro_mov, periodo, tipo, acumulado, sistema);
            
        }
        private static void cuadro_movimiento_toExcel(List<SV.GROUP_MOVEMENT> det, Vperiodo periodo, SV.GENERIC_VALUE tipo, SV.GENERIC_VALUE acumulado, SV.SV_SYSTEM sistema)
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
        public static void fixed_assets(int año, int mes, SV.GENERIC_VALUE tipo, int acumulado, SV.SV_SYSTEM sistema)
        {
            List<SV.GROUP_MOVEMENT> fix_rep;
            var periodo = new Vperiodo(año, mes, acumulado);
            using (var cServ = new ServiceProcess.ServiceAFN2())
                fix_rep = cServ.Proceso.reporte_fixed_assets(periodo, tipo, sistema);

            fixed_assets_toExcel(fix_rep, periodo, tipo, acumulado, sistema);

        }
        private static void fixed_assets_toExcel(List<SV.GROUP_MOVEMENT> det, Vperiodo periodo, SV.GENERIC_VALUE tipo, int acumulado, SV.SV_SYSTEM sistema)
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
        public static void obc_detalle(SV.GENERIC_VALUE reporte, int año, int mes, SV.GENERIC_VALUE moneda, SV.GENERIC_VALUE acumulado)
        {
            DateTime fecha_proceso = new ACode.Vperiodo(año, mes).last;
            List<SV.DETAIL_OBC> detalle;

            using (var cServ = new ServiceProcess.ServiceAFN2())
            {
                switch (reporte.id)
                {
                    case 1:
                        detalle = cServ.Proceso.saldo_obras(fecha_proceso, moneda);
                        break;
                    case 2:
                        detalle = cServ.Proceso.saldo_obras(fecha_proceso, moneda);
                        break;
                    default:
                        detalle = cServ.Proceso.saldo_obras(fecha_proceso, moneda);
                        break;
                }
            }
            obc_detalle_toExcel(detalle, fecha_proceso,moneda);
        }
        private static void obc_detalle_toExcel(List<SV.DETAIL_OBC> data, DateTime fecha, SV.GENERIC_VALUE moneda)
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

        #region Fichas
        public static SV.RespuestaAccion get_ficha_ingreso(int id_articulo)
        {
            var resultado = new SV.RespuestaAccion();
            try
            {
                List<SV.SINGLE_DETAIL> informacion;
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    informacion = cServ.Proceso.ficha_ingreso1(id_articulo);

                ficha_ingreso_toExcel(informacion);
                resultado.set_ok();
            }
            catch(Exception ex)
            {
                resultado.set(-1, ex.Message);
            }
            return resultado;
        }

        private static void ficha_ingreso_toExcel(List<SV.SINGLE_DETAIL> info)
        {
            Exc.Application excel;
            Exc.Workbook worKbooK;
            Exc.Worksheet oSheet;
            Exc.Range celLrangE;
            int colN1, filT1, colN2, colN3, colT1, colT2, colT3;
            int filT0, colG0,colG1;
            excel = new Exc.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;

            var firstInfo = info.First();
            worKbooK = excel.Workbooks.Add();
            set_sheet_amount(worKbooK, 1);
            oSheet = worKbooK.Sheets[1];
            oSheet.Name = "ficha_ingreso_cod" + firstInfo.codigo_articulo.ToString();
            //formamos la grilla blanca y de ancho fijo para trabajar sobre ella
            var grid = oSheet.Cells;
            grid.ColumnWidth = 3;
            var borders = new Exc.XlBordersIndex[] {
                Exc.XlBordersIndex.xlEdgeLeft,
                Exc.XlBordersIndex.xlEdgeTop,
                Exc.XlBordersIndex.xlEdgeBottom,
                Exc.XlBordersIndex.xlEdgeRight,
                Exc.XlBordersIndex.xlInsideVertical,
                Exc.XlBordersIndex.xlInsideHorizontal};
            foreach(var border in borders)
            {
                var sideBorder = grid.Borders[border];
                sideBorder.LineStyle = 1;
                sideBorder.ColorIndex = 2;
                sideBorder.TintAndShade = 0;
                sideBorder.Weight = 2;
                sideBorder=null;
            }
            grid = null;
            //Determino modulos involucrados
            var hay_ifrs = info.Where(i => i.fuente.ENVIORMENT == "IFRS").Count();
            //ingreso primera data
            colN1 = 1;
            filT1 = 1;
            colN2 = colN1 + 8;
            colN3 = colN2 + 16;
            colT1 = colN1 + 4;
            colT2 = colN2 + 3;
            colT3 = colN3 + 3;
            //celLrangE = oSheet.Cells[filT1, colN1];
            //celLrangE.Value = "Código Grupo :";
            celLrangE = oSheet.Range[oSheet.Cells[filT1,colN1], oSheet.Cells[filT1,colT1 - 1]];
            //With oSheet.Range(lcol(colN1) + CStr(filT1) + ":" + lcol(colT1 - 1) + CStr(filT1))
                celLrangE.MergeCells = true;
                celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value = "Código Grupo :";
            celLrangE = null;
            //celLrangE = oSheet.Range[oSheet.Cells[filT1,colN1],oSheet.Cells[filT1,colT1 - 1]];
            //    celLrangE.MergeCells = true;
            //    celLrangE.Font.Bold = true;
            //    celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
            //celLrangE = null;
            //celLrangE =  oSheet.Cells[filT1, colT1];
            //    celLrangE.Value = firstInfo.codigo_articulo;
            //celLrangE = null;
            celLrangE = oSheet.Range[oSheet.Cells[filT1,colT1], oSheet.Cells[filT1,colT1 + 2]];
                celLrangE.MergeCells = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value = firstInfo.codigo_articulo;
            celLrangE = null;
            //celLrangE = oSheet.Cells[filT1, colN2];
            //    celLrangE.Value = "Descripción :";
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN2],oSheet.Cells[filT1,colT2]];
                celLrangE.MergeCells = true;
                celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value = "Descripción :";
            celLrangE=null;
            celLrangE = oSheet.Cells[filT1, colT2 + 1];
                celLrangE.Value = firstInfo.descripcion;
            celLrangE = null;

            filT1 = filT1 + 2;

            //celLrangE = oSheet.Cells[filT1, colN1];
            //    celLrangE.Value = "Cantidad :";
            celLrangE = null;
            celLrangE = oSheet.Range[oSheet.Cells[filT1,colN1],oSheet.Cells[filT1,(colT1 - 1)]];
                celLrangE.MergeCells = true;
                celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value = "Cantidad :";
            celLrangE = null;
            //celLrangE = oSheet.Cells[filT1, colT1];
            //celLrangE.Value = firstInfo.cantidad;
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colT1], oSheet.Cells[filT1, colT1 + 2]];
                celLrangE.MergeCells = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value = firstInfo.cantidad;
            celLrangE = null;
            //oSheet.Cells[filT1, colN2].Value = "Zona :";
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN2], oSheet.Cells[filT1, colT2 - 1]];
                celLrangE.MergeCells = true;
                celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value = "Zona :";
            celLrangE = null;
            oSheet.Cells[filT1, colT2].Value = firstInfo.zona.description;
            //oSheet.Cells[filT1, colN3].Value = "Clase :";
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN3], oSheet.Cells[filT1, (colT3 - 1)]];
                celLrangE.MergeCells = true;
                celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value = "Clase :";
            celLrangE = null;
            oSheet.Cells[filT1, colT3].Value = firstInfo.clase.description;
            filT1 = filT1 + 2;
            //oSheet.Cells[filT1, colN1].Value = "Orígen :";
            celLrangE = oSheet.Range[oSheet.Cells[filT1,colN1],oSheet.Cells[filT1,(colT1 - 2)]];
                celLrangE.MergeCells = true;
                celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value = "Orígen :";
            celLrangE = null;
            //oSheet.Cells[filT1, colT1 - 1].Value = firstInfo.origen.description;
            celLrangE = oSheet.Range[oSheet.Cells[filT1, (colT1 - 1)], oSheet.Cells[filT1, (colT1 + 2)]];
                celLrangE.MergeCells = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value = firstInfo.origen.description;
            celLrangE = null;
            //oSheet.Cells[filT1, colN2].Value = "Sub Zona :";
            celLrangE = oSheet.Range[oSheet.Cells[filT1, (colN2)], oSheet.Cells[filT1, (colT2 - 1)]];
                celLrangE.MergeCells = true;
                celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value = "Sub Zona :";
            celLrangE = null;
            oSheet.Cells[filT1, colT2].Value = firstInfo.subzona.description;
            //oSheet.Cells[filT1, colN3].Value = "Sub Clase :";
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN3], oSheet.Cells[filT1, (colT3 - 1)]];
                celLrangE.MergeCells = true;
                celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value = "Sub Clase :";
            celLrangE = null;
            oSheet.Cells[filT1, colT3].Value = firstInfo.subclase.description;
            filT1 = filT1 + 2;
            //oSheet.Cells[filT1, colN1].Value = "Fecha Compra :";
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN1], oSheet.Cells[filT1, (colT1 - 1)]];
                celLrangE.MergeCells = true;
                celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value = "Fecha Compra :";
            celLrangE = null;
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colT1], oSheet.Cells[filT1, (colT1 + 2)]];
                celLrangE.MergeCells = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.NumberFormat = "@";
                celLrangE.Value = firstInfo.fecha_compra.ToShortDateString();
            celLrangE = null;
            //oSheet.Cells(filT1, colN2) = "Categoria :"
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN2], oSheet.Cells[filT1, (colT2 - 1)]];
                celLrangE.MergeCells = true;
                celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value =  "Categoria :";
            celLrangE = null;
            oSheet.Cells[filT1, colT2].Value = firstInfo.categoria.description;
            //oSheet.Cells(filT1, colN3) = "Nº Doc. :"
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN3], oSheet.Cells[filT1, (colT3 - 1)] ];
                celLrangE.MergeCells = true;
                celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value =  "Nº Doc. :";
             celLrangE = null;
             oSheet.Cells[filT1, colT3].Value = firstInfo.num_doc;
            //oSheet.Cells[filT1, colN1].Value = "Derecho Credito :";
            celLrangE = oSheet.Range[oSheet.Cells[filT1,colN1],oSheet.Cells[filT1,(colT1)]];
                celLrangE.MergeCells = true;
                celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE.Value = "Derecho Credito :";
            celLrangE = null;
            oSheet.Cells[filT1, colT1 + 1] = (firstInfo.derecho_credito?"SI":"NO");
            oSheet.Cells[filT1, colN2].Value = "Proveedor :";
            celLrangE= oSheet.Range[oSheet.Cells[filT1,colN2],oSheet.Cells[filT1,(colT2 - 1)]];
                celLrangE.MergeCells = true;
                celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
            celLrangE = null;
            oSheet.Cells[filT1, colT2].Value = "'" + firstInfo.proveedor;
            var linea_extra = 0;
            if(firstInfo.proveedor != firstInfo.descrip_proveedor){
                oSheet.Cells[filT1 + 1, colT2].Value = firstInfo.descrip_proveedor;
                linea_extra = 1;
            }
            if(hay_ifrs>0){
                oSheet.Cells[filT1, colN3].Value = "Metodo Revaluación :";
                celLrangE =  oSheet.Range[oSheet.Cells[filT1,colN3],oSheet.Cells[filT1,(colT3 + 2)]];
                    celLrangE.MergeCells = true;
                    celLrangE.Font.Bold = true;
                    celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
                celLrangE = null;
                oSheet.Cells[filT1, colT3 + 3].Value = firstInfo.metod_val.description;
            }
            
            //segunda parte del reporte, cuadro resumen de valores
            //columna 1
            filT0 = filT1 + 3 + linea_extra;
            filT1 = filT0;
            //objExcel.Visible = true
            colG0 = 2;
            colG1 = colG0 + 4;
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = "Precio Unitario";
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = "Vida Útil";
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = "Valor Residual";
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = "Dep. Acum.";
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            if(hay_ifrs>0){
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = "Preparación";
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = "Transporte";
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = "Montaje";
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = "Desmantelamiento";
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = "Honorario";
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = "Revalorización";
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            }
            //aplico formato para la columna
            celLrangE = oSheet.Range[oSheet.Cells[(filT0 + 1),colG0],oSheet.Cells[filT1,colG1]];
                celLrangE.Borders[Exc.XlBordersIndex.xlEdgeLeft].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlEdgeTop].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlEdgeBottom].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlEdgeRight].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlInsideVertical].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlInsideHorizontal].ColorIndex = 1;
                celLrangE.Font.Bold = true;
            celLrangE = null;

            //columna 2
            //filT0 = filT0   'fila inicio de la sección no cambia
            filT1 = filT0;   //vuelvo al inicio el contador de filas

            colG0 = colG1 + 1;   //1 columna a la derecha de donde termino la anterior
            colG1 = colG0 + 2;   //seteo nuevo rango, en este caso seran 4

            var FinClp = info.Where(i => i.fuente.ENVIORMENT =="FIN" && i.fuente.CURRENCY=="CLP").FirstOrDefault();
            oSheet.Cells[filT1, colG0].Value = "FINANCIERO";
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = FinClp.precio_base;
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = FinClp.vida_util_base;
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = FinClp.valor_residual;
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = FinClp.depreciacion_acum;
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            if(hay_ifrs>0){
                for(int a =0;a<6;a++){
                    filT1 = filT1 + 1;
                    oSheet.Cells[filT1, colG0].Value = "-";
                    oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                }
            }
            //aplico formato para la columna
            celLrangE = oSheet.Range[oSheet.Cells[(filT0),colG0],oSheet.Cells[filT1,colG1]];
                celLrangE.Borders[Exc.XlBordersIndex.xlEdgeLeft].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlEdgeTop].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlEdgeBottom].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlEdgeRight].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlInsideVertical].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlInsideHorizontal].ColorIndex = 1;
                //celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignRight; // -4152;    //xlRight
                celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignTop;     // -4160;      //xlTop
                celLrangE.NumberFormat = "#,##0";
            celLrangE = null;
 
            //columna 3
            //'filT0 = filT0;   //fila inicio de la sección no cambia
            filT1 = filT0;   //vuelvo al inicio el contador de filas

            colG0 = colG1 + 1;   //1 columna a la derecha de donde termino la anterior
            colG1 = colG0 + 2;   //seteo nuevo rango, en este caso seran 4

            var TribCLP = info.Where(i => i.fuente.CURRENCY == "CLP" && i.fuente.ENVIORMENT == "TRIB").FirstOrDefault();
            oSheet.Cells[filT1, colG0].Value = "TRIBUTARIO";
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = TribCLP.precio_base;
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = TribCLP.vida_util_base;
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = TribCLP.valor_residual;
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = TribCLP.depreciacion_acum;
            oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
            if(hay_ifrs>0){
				for(int a =0;a<6;a++){
                    filT1 = filT1 + 1;
                    oSheet.Cells[filT1, colG0].Value = "-";
                    oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                }
            }
            //aplico formato para la columna
            celLrangE = oSheet.Range[oSheet.Cells[(filT0),colG0],oSheet.Cells[filT1,colG1]];
                celLrangE.Borders[Exc.XlBordersIndex.xlEdgeLeft].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlEdgeTop].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlEdgeBottom].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlEdgeRight].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlInsideVertical].ColorIndex = 1;
                celLrangE.Borders[Exc.XlBordersIndex.xlInsideHorizontal].ColorIndex = 1;
                //celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignRight; // -4152;    //xlRight
                celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignTop;     // -4160;      //xlTop
                celLrangE.NumberFormat = "#,##0";
            celLrangE = null;
            
            if(hay_ifrs >0){
                //columna 4
                //filT0 = filT0;   //fila inicio de la sección no cambia
                filT1 = filT0;   //vuelvo al inicio el contador de filas

                colG0 = colG1 + 1;   //1 columna a la derecha de donde termino la anterior
                colG1 = colG0 + 2;   //seteo nuevo rango, en este caso seran 4

                var IfrsCLP = info.Where(i => i.fuente.ENVIORMENT == "IFRS" && i.fuente.CURRENCY == "CLP").FirstOrDefault();
                oSheet.Cells[filT1, colG0].Value = "IFRS CLP";
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.precio_base;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.vida_util_base;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.valor_residual;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.depreciacion_acum;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.preparacion;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.transporte;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.montaje;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.desmantel;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.honorario;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.revalorizacion;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                //aplico formato para la columna
                celLrangE = oSheet.Range[oSheet.Cells[(filT0),colG0],oSheet.Cells[filT1,colG1]];
                    celLrangE.Borders[Exc.XlBordersIndex.xlEdgeLeft].ColorIndex = 1;
                    celLrangE.Borders[Exc.XlBordersIndex.xlEdgeTop].ColorIndex = 1;
                    celLrangE.Borders[Exc.XlBordersIndex.xlEdgeBottom].ColorIndex = 1;
                    celLrangE.Borders[Exc.XlBordersIndex.xlEdgeRight].ColorIndex = 1;
                    celLrangE.Borders[Exc.XlBordersIndex.xlInsideVertical].ColorIndex = 1;
                    celLrangE.Borders[Exc.XlBordersIndex.xlInsideHorizontal].ColorIndex = 1;
                    //celLrangE.Font.Bold = true;
                    celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignRight; // -4152;    //xlRight
                    celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignTop;     // -4160;      //xlTop
                    celLrangE.NumberFormat = "#,##0";
                celLrangE = null;
            }
            //aca empieza ifrs yen
            var IfrsYen = info.Where(i => i.fuente.ENVIORMENT == "IFRS" && i.fuente.CURRENCY == "YEN").FirstOrDefault();
            if(IfrsYen != null){
                //columna 5
                //filT0 = filT0;   //fila inicio de la sección no cambia
                filT1 = filT0;   //'vuelvo al inicio el contador de filas

                colG0 = colG1 + 1;   //1 columna a la derecha de donde termino la anterior
                colG1 = colG0 + 2;   //seteo nuevo rango, en este caso seran 4

                oSheet.Cells[filT1, colG0].Value = "IFRS YEN";
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.precio_base;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.vida_util_base;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value =IfrsYen.valor_residual;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value =IfrsYen.depreciacion_acum;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.preparacion;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.transporte;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.montaje;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.desmantel;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.honorario;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.revalorizacion;
                oSheet.Range[oSheet.Cells[filT1,colG0],oSheet.Cells[filT1,colG1]].MergeCells = true;
                //aplico formato para la columna
                celLrangE = oSheet.Range[oSheet.Cells[(filT0),colG0],oSheet.Cells[filT1,colG1]];
                    celLrangE.Borders[Exc.XlBordersIndex.xlEdgeLeft].ColorIndex = 1;
                    celLrangE.Borders[Exc.XlBordersIndex.xlEdgeTop].ColorIndex = 1;
                    celLrangE.Borders[Exc.XlBordersIndex.xlEdgeBottom].ColorIndex = 1;
                    celLrangE.Borders[Exc.XlBordersIndex.xlEdgeRight].ColorIndex = 1;
                    celLrangE.Borders[Exc.XlBordersIndex.xlInsideVertical].ColorIndex = 1;
                    celLrangE.Borders[Exc.XlBordersIndex.xlInsideHorizontal].ColorIndex = 1;
                    //celLrangE.Font.Bold = true;
                    celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignRight; // -4152;    //xlRight
                    celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignTop;     // -4160;      //xlTop
                    celLrangE.NumberFormat = "#,##0";
                celLrangE = null;
            }
            //Termino de hoja
            excel.Visible = true;
            excel.DisplayAlerts = true;
            celLrangE = null;
            oSheet = null;
            worKbooK = null;
        }
        #endregion

        private static void set_sheet_amount(Exc.Workbook eBook, int amount) 
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
    }
}
