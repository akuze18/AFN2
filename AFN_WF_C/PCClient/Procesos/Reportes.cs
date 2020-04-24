using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ACode;
using AFN_WF_C.PCClient.Procesos.Estructuras;

using Exc = Microsoft.Office.Interop.Excel;
using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Procesos
{
    internal class Reportes
    {
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
                string tName = tipo.Name;
                switch (tName)
                {
                    case "GENERIC_VALUE":
                    case "GENERIC_RELATED":
                        var val_d = (PD.GENERIC_VALUE) selected;
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

        #region Vigentes Detalle
        public static void vigentes_detalle(int año, int mes, PD.GENERIC_VALUE clase, PD.GENERIC_VALUE zona, PD.GENERIC_VALUE acumulado, PD.SV_SYSTEM sistema)
        {
            List<PD.DETAIL_MOVEMENT> detalle;
            var periodo = new Vperiodo(año, mes, acumulado.id);
            using (var cServ = new ServiceProcess.ServiceAFN2())
                detalle = cServ.Proceso.reporte_vigentes(periodo, clase, zona, sistema);

            vigentes_detalle_toExcel(detalle, periodo, clase, zona, sistema);

        }

        private static void vigentes_detalle_toExcel(List<PD.DETAIL_MOVEMENT> det, ACode.Vperiodo periodo, PD.GENERIC_VALUE clase, PD.GENERIC_VALUE zona, PD.SV_SYSTEM sistema)
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
            ExcelWrite.set_sheet_amount(worKbooK, 1);
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
                    worKsheeT.Cells[(ini_det + det.Count -1), (curr_col + 1)]];

                celLrangE.NumberFormat = proceso.Formating;
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
                    new TituloCabera(36,"Parte"),
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
                    new TituloCabera(34,"Codigo Subzona", "CODE" ) ,
                    new TituloCabera(34,"Descripción Subzona", "DESCRIP" ) ,
                    new TituloCabera(38,"Codigo Gestion", "CODE") ,
                    new TituloCabera(38,"Descripción Gestion", "DESCRIP" ) ,
                    new TituloCabera(39,"Fecha Contabilizacion") ,
                    new TituloCabera(40,"Origen", "CODE" ) ,
                    new TituloCabera(41,"Vida Util Inicial") ,
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
                    new TituloCabera(36,"Parte"),
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
                    new TituloCabera(34,"Codigo Subzona", "CODE" ) ,
                    new TituloCabera(34,"Descripción Subzona", "DESCRIP" ) ,
                    new TituloCabera(38,"Codigo Gestion", "CODE") ,
                    new TituloCabera(38,"Descripción Gestion", "DESCRIP" ) ,
                    new TituloCabera(39,"Fecha Contabilizacion") ,
                    new TituloCabera(40,"Origen", "CODE" ) ,
                    new TituloCabera(41,"Vida Util Inicial") ,
                };
            }
        }

        #endregion

        #region Vigenes Detalle con Inventario (DI)
        public static void vigentes_detalle_inventario(int año, int mes, PD.GENERIC_VALUE clase, PD.GENERIC_VALUE zona, PD.GENERIC_VALUE acumulado, PD.SV_SYSTEM sistema)
        {
            List<PD.DETAIL_MOVEMENT> detalle;
            var periodo = new Vperiodo(año, mes, acumulado.id);
            using (var cServ = new ServiceProcess.ServiceAFN2())
                detalle = cServ.Proceso.reporte_vigentes_con_inv(periodo, clase, zona, sistema);

            vigentes_detalle_inventario_toExcel(detalle, periodo, clase, zona, sistema);

        }

        private static void vigentes_detalle_inventario_toExcel(List<PD.DETAIL_MOVEMENT> det, ACode.Vperiodo periodo, PD.GENERIC_VALUE clase, PD.GENERIC_VALUE zona, PD.SV_SYSTEM sistema)
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
            ExcelWrite.set_sheet_amount(worKbooK, 1);
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
            //if (sistema.ENVIORMENT.code == "IFRS")
            //    titulos = GetTitulosVigentesDI;
            //else
                titulos = GetTitulosVigentesDI;
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

                celLrangE.NumberFormat = proceso.Formating;
                celLrangE.Value = proceso.Valores;
            }
            worKsheeT.Columns["B:B"].NumberFormat = "#;";   //corrijo formato para columna de codigo articulo
            excel.Visible = true;
            excel.DisplayAlerts = true;
            celLrangE = null;
            worKsheeT = null;
            worKbooK = null;
        }

        private static TituloCabera[] GetTitulosVigentesDI
        {
            get
            {
                return new TituloCabera[] { 
                    new TituloCabera(2,"Fecha de Compra") ,
                    new TituloCabera(1,"Codigo del Bien") ,
                    new TituloCabera(36,"Parte"),
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
                    new TituloCabera(34,"Codigo Subzona", "CODE" ) ,
                    new TituloCabera(34,"Descripción Subzona", "DESCRIP" ) ,
                    new TituloCabera(38,"Codigo Gestion", "CODE") ,
                    new TituloCabera(38,"Descripción Gestion", "DESCRIP" ) ,
                    new TituloCabera(39,"Fecha Contabilizacion") ,
                    new TituloCabera(40,"Origen", "CODE" ) ,
                    new TituloCabera(41,"Vida Util Inicial") ,
                    new TituloCabera(43,"Codigo Ubicación", "CODE") ,
                    new TituloCabera(43,"Ubicación","DESCRIP") ,
                    new TituloCabera(44,"Entregado") ,
                    new TituloCabera(47,"Código Ultimo Estado", "CODE") ,
                    new TituloCabera(47,"Último Estado","DESCRIP") ,
                };
            }
        }
        #endregion

        #region Vigentes Resumen
        public static void vigentes_resumen(int año, int mes, PD.GENERIC_VALUE clase, PD.GENERIC_VALUE zona, PD.GENERIC_VALUE acumulado, PD.SV_SYSTEM sistema, string tipo)
        {
            List<PD.GROUP_MOVEMENT> resumen;
            var periodo = new Vperiodo(año, mes, acumulado.id);
            using (var pServ = new ServiceProcess.ServiceAFN2())
            {
                resumen = pServ.Proceso.reporte_vigente_resumen(periodo, clase, zona, sistema, tipo);
            }
            vigentes_resumen_toExcel(resumen, periodo, clase, zona, sistema, tipo);

        }

        private static void vigentes_resumen_toExcel(List<PD.GROUP_MOVEMENT> det, ACode.Vperiodo periodo, PD.GENERIC_VALUE clase, PD.GENERIC_VALUE zona, PD.SV_SYSTEM sistema, string tipo)
        {
            int ini_titulo = 1, filas_procesadas;

            var worKbooK = ExcelWrite.newBookOff(1);
            var worKsheeT = (Exc.Worksheet)worKbooK.Sheets[1];

            ExcelWrite SheetWrite = new ExcelWrite(worKsheeT);
            //Formatos de bordes
            var tituloFormato = new BorderPaintingFormat();
            tituloFormato.EdgeBottom.SetValues(0, 3);

            var contentFormato = new BorderPaintingFormat();
            //contentFormato.InteriorColor = 16764057;
            contentFormato.EdgeTop.SetValues(0, 2);
            contentFormato.EdgeBottom.SetValues(0, 3);
            var TotalsFormato = new BorderPaintingFormat();
            TotalsFormato.EdgeBottom.SetValues(0, 2, Exc.XlLineStyle.xlDouble);
            TotalsFormato.EdgeTop.SetValues(0, 1);

            TituloCabera[] titulos;
            if (sistema.ENVIORMENT.code == "IFRS")
                titulos = GetTitulosVigentesResumen_IFRS(tipo);
            else
                titulos = GetTitulosVigentesResumen(tipo);

            //SECCION 1
            filas_procesadas = SheetWrite.write_data_column_colored(det, titulos, ini_titulo, tituloFormato, contentFormato, TotalsFormato);
            ini_titulo += filas_procesadas + 2;
            string sName = "", displayTitulo = "";
            if (tipo == "C")
            {
                sName = "Resumen Activo Fijo";
                displayTitulo = "RESUMEN CLASE ";
            }
            else if (tipo == "Z")
            {
                sName = "Resumen Activo Fijo";
                displayTitulo = "RESUMEN ZONA ";
            }
            SheetWrite.set_sheet_format(titulos.Count(), sistema, periodo, displayTitulo, sName);

            ExcelWrite.BookOn(worKbooK);

            worKsheeT = null;
            worKbooK = null;
        }
        
        private static TituloCabera[] GetTitulosVigentesResumen(string tipo)
        {
            var clase = new TituloCabera(1, "Clase de Activo", "DESCRIP");
            var depto = new TituloCabera(2, "Nombre Departamento", "DESCRIP");
            var lugar = new TituloCabera(3, "Nombre Lugar", "DESCRIP");

            var orden1 = (tipo == "C" ? clase : depto);
            var orden2 = (tipo == "C" ? depto : lugar);
            var orden3 = (tipo == "C" ? lugar : clase);
            
            return new TituloCabera[] { 
                orden1 ,
                orden2 ,
                orden3 ,
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
            };
        }    
        private static TituloCabera[] GetTitulosVigentesResumen_IFRS(string tipo)
        {
            string Tord1, Tord2, Tord3;
            int Cord1, Cord2, Cord3;
            if (tipo == "C")
            {
                Tord1 = "Clase";
                Tord2 = "Zona";
                Tord3 = "Lugar";
                Cord1 = 1;
                Cord2 = 2;
                Cord3 = 3;
            }
            else
            {
                Tord1 = "Zona";
                Tord2 = "Lugar";
                Tord3 = "Clase";
                Cord1 = 2;
                Cord2 = 3;
                Cord3 = 1;
            }
            return new TituloCabera[] { 
                new TituloCabera(Cord1,Tord1, "DESCRIP" ) ,
                new TituloCabera(Cord2,Tord2, "DESCRIP"  ) ,
                new TituloCabera(Cord3,Tord3, "DESCRIP"  ) ,
                new TituloCabera(4,"Valor Inicial")  ,
                new TituloCabera(25,"Preparacion") ,
                new TituloCabera(26,"Desmantelamiento") ,
                new TituloCabera(27,"Transporte") ,
                new TituloCabera(28,"Montaje") ,
                new TituloCabera(29,"Honorarios") ,
                new TituloCabera(6,"Credito adiciones") ,
                new TituloCabera(7,"Valor de Activo Fijo") ,
                new TituloCabera(8,"Dep. Acum Anterior") ,
                new TituloCabera(9,"C. Monetaria Dep. Acum.") ,
                new TituloCabera(10,"Valor Residual") ,
                new TituloCabera(11,"Depreciación del Ejercicio") ,
                new TituloCabera(12,"Depreciación Acumulada") ,
                new TituloCabera(13,"Valor Libro del Activo") ,
            };
            
        }

        #endregion

        #region Bajas
        public static void bajas_detalle(Vperiodo desde, Vperiodo hasta, int situacion, PD.SV_SYSTEM sistema)
        {
            List<PD.DETAIL_MOVEMENT> detalle;
            using (var cServ = new ServiceProcess.ServiceAFN2())
                detalle = cServ.Proceso.reporte_bajas(desde, hasta, situacion, sistema);

            bajas_detalle_toExcel(detalle,desde, hasta, situacion, sistema);
        }

        private static void bajas_detalle_toExcel(List<PD.DETAIL_MOVEMENT> det, Vperiodo desde, Vperiodo hasta, int situacion, PD.SV_SYSTEM sistema)
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

                celLrangE.NumberFormat = proceso.Formating;
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
                    new TituloCabera(36,"Parte"),
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
                    new TituloCabera(34,"Descripción Subzona","DESCRIP"),
                    new TituloCabera(42,"Precio Venta")
                };
            }
        }
        #endregion

        #region Cuadro Movimiento
        public static void cuadro_movimiento(int año, int mes, PD.GENERIC_VALUE tipo, PD.GENERIC_VALUE acumulado, PD.SV_SYSTEM sistema)
        {
            List<PD.GROUP_MOVEMENT> cuadro_mov;
            var periodo = new Vperiodo(año, mes, acumulado.id);
            using (var cServ = new ServiceProcess.ServiceAFN2())
                cuadro_mov = cServ.Proceso.reporte_cuadro_movimiento(periodo, tipo, sistema);

            cuadro_movimiento_toExcel(cuadro_mov, periodo, tipo, acumulado, sistema);
            
        }
        private static void cuadro_movimiento_toExcel(List<PD.GROUP_MOVEMENT> det, Vperiodo periodo, PD.GENERIC_VALUE tipo, PD.GENERIC_VALUE acumulado, PD.SV_SYSTEM sistema)
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

                celLrangE.NumberFormat = proceso.Formating;
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
        public static void fixed_assets(int año, int mes, PD.GENERIC_VALUE tipo, int acumulado, PD.SV_SYSTEM sistema)
        {
            List<PD.GROUP_MOVEMENT> fix_rep;
            var periodo = new Vperiodo(año, mes, acumulado);
            using (var cServ = new ServiceProcess.ServiceAFN2())
                fix_rep = cServ.Proceso.reporte_fixed_assets(periodo, tipo, sistema);

            fixed_assets_toExcel(fix_rep, periodo, tipo, acumulado, sistema);

        }
        private static void fixed_assets_toExcel(List<PD.GROUP_MOVEMENT> det, Vperiodo periodo, PD.GENERIC_VALUE tipo, int acumulado, PD.SV_SYSTEM sistema)
        {            
            int ini_titulo = 1, filas_procesadas;
            
            var worKbooK = ExcelWrite.newBookOff(1);
            var worKsheeT = (Exc.Worksheet)worKbooK.Sheets[1];

            ExcelWrite SheetWrite = new ExcelWrite(worKsheeT);
            //Formatos de bordes
            var tituloFormato = new BorderPaintingFormat();
            tituloFormato.EdgeBottom.SetValues(0, 3);

            var contentFormato = new BorderPaintingFormat();

            var TotalsFormato = new BorderPaintingFormat();
            TotalsFormato.EdgeBottom.SetValues(0, 2, Exc.XlLineStyle.xlDouble);
            TotalsFormato.EdgeTop.SetValues(0, 1);
            //SECCION 1

            filas_procesadas = SheetWrite.write_data_column(det, GetTitulosFixedAssetsActive, ini_titulo, tituloFormato,contentFormato, TotalsFormato);
            ini_titulo += filas_procesadas+2;
            //SECCION 2
            filas_procesadas = SheetWrite.write_data_column(det, GetTitulosFixedAssetsDeprec, ini_titulo, tituloFormato,contentFormato, TotalsFormato);
            
            string sName = "FixedAssets_" + sistema.ToCode() + periodo.last.ToString("yyyy-MM");
            string displayTitulo = "REPORT FIXED ASSETS";
            SheetWrite.set_sheet_format(GetTitulosFixedAssetsActive.Count(), sistema, periodo, displayTitulo, sName);

            ExcelWrite.BookOn(worKbooK);

            worKsheeT = null;
            worKbooK = null;
        }
        private static TituloCabera[] GetTitulosFixedAssetsActive
        {
            get
            {
                return new TituloCabera[]{
                    new TituloCabera(1,"PACKING","DESCRIP"),
                    new TituloCabera(17,"BEGINNING"),
                    new TituloCabera(18,"ACQUISITION FROM OTHER"),
                    new TituloCabera(19,"FROM CONSTRUCTION IN"),
                    new TituloCabera(24,"INCREASE CONSTRUCTION"),
                    new TituloCabera(25,"DECREASE OF CONSTRUCTION"),
                    new TituloCabera(20,"DISPOSAL","CODE"),
                    new TituloCabera(21,"SALES","CODE"),
                    new TituloCabera(6,"DECREASE OTHER 4%"),
                    new TituloCabera(7,"AT END")
                };
            }
        }
        private static TituloCabera[] GetTitulosFixedAssetsDeprec
        {
            get
            {
                return new TituloCabera[]{
                    new TituloCabera(1,"PACKING","DESCRIP"),
                    new TituloCabera(-8,"BEGINNING"),
                    new TituloCabera(-11,"INCREASE (DEPRECIATION)"),
                    new TituloCabera(-22,"DISPOSAL"),
                    new TituloCabera(-23,"DECREASE (SALES) OTHER"),
                    new TituloCabera(-12,"AT END"),
                    new TituloCabera(0,""),
                    new TituloCabera(0,""),
                    new TituloCabera(0,""),
                    new TituloCabera(13,"BALANCE","CODE")
                    //new TituloCabera(1,"Cuenta","DESCRIP"),
                    //new TituloCabera(17,"Saldo Inicial"),
                    //new TituloCabera(18,"Adiciones"),
                    //new TituloCabera(19,"Desde Obras en Construccion"),
                    //new TituloCabera(5,"Hacia Activo Fijo"),
                    //new TituloCabera(6,"Credito"),
                    //new TituloCabera(20,"Castigos","CODE"),
                    //new TituloCabera(21,"Ventas","CODE"),
                    //new TituloCabera(7,"Saldo Final"),
                    //new TituloCabera(1,"Cuenta","DESCRIP"),
                    //new TituloCabera(8,"Saldo Inicial"),
                    //new TituloCabera(11,"Dep. Ejercicio"),
                    //new TituloCabera(9,"C Mon"),
                    //new TituloCabera(22,"Castigos"),
                    //new TituloCabera(23,"Ventas"),
                    //new TituloCabera(12,"Saldo Final"),
                    //new TituloCabera(13,"Valor Neto")
                };
            }
        }
        #endregion

        #region Obras en Construccion
        public static void obc_detalle(PD.GENERIC_VALUE reporte, int año, int mes, PD.GENERIC_VALUE moneda, PD.GENERIC_VALUE acumulado)
        {
            var period = new ACode.Vperiodo(año, mes, acumulado.id);
            List<PD.DETAIL_OBC> detalle;
            using (var cServ = new ServiceProcess.ServiceAFN2())
            {
                switch (reporte.id)
                {
                    case 1: //saldo
                        detalle = cServ.Proceso.saldo_obras(period.last, moneda);
                        obc_detalle_toExcel(detalle, period.last, moneda);
                        break;
                    case 2: //entradas
                        detalle = cServ.Proceso.entradas_obras(period.first,period.last, moneda);
                        obc_entradas_toExcel(detalle, period.last, moneda);
                        break;
                    case 3: //salidas
                        detalle = cServ.Proceso.salidas_obras(period.first,period.last, moneda);
                        obc_salidas_toExcel(detalle, period.last, moneda);
                        break;
                    default:    
                        detalle = cServ.Proceso.saldo_obras(period.last, moneda);
                        obc_detalle_toExcel(detalle, period.last, moneda);
                        break;
                }
            }
            
        }

        private static void obc_toExcel(List<PD.DETAIL_OBC> data, DateTime fecha, PD.GENERIC_VALUE moneda, string TitleName, TituloCabera[] TitlesHead)
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
            ExcelWrite.set_sheet_amount(worKbooK, 1);
            worKsheeT = (Exc.Worksheet)worKbooK.ActiveSheet;
            worKsheeT.Name = TitleName;

            int max_columnas = TitlesHead.Count();

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
                TituloCabera procesando = TitlesHead[curr_col];
                worKsheeT.Cells[ini_titulo, (curr_col + 1)].Value = procesando.ColTitulo;

                var proceso = RawDataColumn(data, procesando);

                celLrangE = worKsheeT.Range[
                    worKsheeT.Cells[ini_det, (curr_col + 1)],
                    worKsheeT.Cells[(ini_det + data.Count - 1), (curr_col + 1)]];
                celLrangE.NumberFormat = proceso.Formating;
                celLrangE.Value = proceso.Valores;

            }
            excel.Visible = true;
            excel.DisplayAlerts = true;
            celLrangE = null;
            worKsheeT = null;
            worKbooK = null;
        }

        private static void obc_detalle_toExcel(List<PD.DETAIL_OBC> data, DateTime fecha, PD.GENERIC_VALUE moneda)
        {
            obc_toExcel(data, fecha, moneda, "Saldo Obras", obc_detalle_saldos_titulos);
        }
        private static void obc_entradas_toExcel(List<PD.DETAIL_OBC> data, DateTime fecha, PD.GENERIC_VALUE moneda)
        {
            obc_toExcel(data, fecha, moneda, "Entradas Obras", obc_detalle_entradas_titulos);
        }
        private static void obc_salidas_toExcel(List<PD.DETAIL_OBC> data, DateTime fecha, PD.GENERIC_VALUE moneda)
        {
            obc_toExcel(data, fecha, moneda, "Salidas Obras", obc_detalle_salidas_titulos);
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
        private static TituloCabera[] obc_detalle_entradas_titulos
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
                    new TituloCabera(9,"Nº Documento"),
                    new TituloCabera(10,"ID Proveedor"),
                    new TituloCabera(11,"Nombre Proveedor"),
                    new TituloCabera(6,"Monto Entrada"),
                };
            }
        }
        private static TituloCabera[] obc_detalle_salidas_titulos
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
                    new TituloCabera(6,"Monto Salida"),
                    new TituloCabera(16,"Código de Movimiento Entrada"),
                    new TituloCabera(12,"Cod. Lote Activo"),
                    new TituloCabera(13,"Descripcion de Activo"),
                    new TituloCabera(14,"Zona Activo"),
                    new TituloCabera(15,"Clase Activo"),
                    new TituloCabera(10,"ID Proveedor"),
                    new TituloCabera(9,"Nº Documento"),
                    new TituloCabera(17,"Monto Total Activo"),
                };
            }
        }

        #endregion

        #region Fichas
        public static PD.RespuestaAccion get_ficha_ingreso(int id_articulo)
        {
            var resultado = new PD.RespuestaAccion();
            try
            {
                List<PD.SINGLE_DETAIL> informacion;
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
        private static void ficha_ingreso_toExcel(List<PD.SINGLE_DETAIL> info)
        {
            Exc.Application oExcel;
            Exc.Workbook worKbooK;
            Exc.Worksheet oSheet;
            Exc.Range celLrangE;
            int colN1, filT1, colN2, colN3, colT1, colT2, colT3;
            int filT0, colG0,colG1;
            oExcel = new Exc.Application();
            oExcel.Visible = false;
            oExcel.DisplayAlerts = false;

            var firstInfo = info.First();
            worKbooK = oExcel.Workbooks.Add();
            ExcelWrite.set_sheet_amount(worKbooK, 1);
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
            //tercera seccion, ingreso de descripciones, si las hubiera.
            //'cuarta seccion lineas para firma
            int largoFirma, filFirma, ColFirma;
            largoFirma = 4;      //'contendra 5 celdas
            filFirma = filT1 + 7;
            ColFirma = 5;
            celLrangE = oSheet.Range[oSheet.Cells[filFirma, ColFirma], oSheet.Cells[filFirma, (ColFirma + largoFirma)]];
            //With oSheet.Range(lcol(ColFirma) + CStr(filFirma) + ":" + lcol(ColFirma + largoFirma) + CStr(filFirma))
            celLrangE.MergeCells = true;
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignCenter;    //xlCenter:  -4108
            celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignCenter;      //xlCenter:   -4108
            celLrangE.Borders[Exc.XlBordersIndex.xlEdgeTop].ColorIndex = 1; //Exc.XlBordersIndex.xlEdgeTop = 8
            celLrangE.Value = "Contador General";
            celLrangE = null;
            ColFirma = ColFirma + largoFirma + 3;
            celLrangE = oSheet.Range[oSheet.Cells[filFirma, ColFirma], oSheet.Cells[filFirma + 1, (ColFirma + largoFirma)]];
            //With oSheet.Range(lcol(ColFirma) + CStr(filFirma) + ":" + lcol(ColFirma + largoFirma) + CStr(filFirma + 1))
            celLrangE.MergeCells = true;
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignCenter;    //xlCenter:  -4108
            celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignCenter;      //xlCenter:   -4108
            celLrangE.Borders[Exc.XlBordersIndex.xlEdgeTop].ColorIndex = 1; //Exc.XlBordersIndex.xlEdgeTop = 8
            celLrangE.Value = "Gerencia" + Environment.NewLine + "(Opcional)";
            celLrangE = null;
            
            //configurar pagina
            string periodo, nombre_total;
            string cabezera, nom_pag;
            bool config_hoja = true;
            nom_pag = "ficha_ingreso";
            //'indx_pag = 1
            cabezera = "&\"-,Negrita\"&20Ficha de Ingreso \tActivo Fijo";
            periodo = "&25 " + firstInfo.fecha_ingreso.ToString("yyyy-MM");
            oSheet.Name = nom_pag;
            nombre_total = Auxiliar.fileLogo;
            Exc.PageSetup ps = oSheet.PageSetup;
            ps.LeftHeaderPicture.Filename = nombre_total;
            
            ps.Orientation = Exc.XlPageOrientation.xlLandscape; //2
            try
            {
                if (config_hoja)
                    ps.PaperSize = Exc.XlPaperSize.xlPaperFolio;
            }
            catch
            {
                try
                {
                    if (config_hoja)
                        ps.PaperSize = Exc.XlPaperSize.xlPaperLetter;
                }
                catch(Exception ex2)
                {
                    config_hoja = false;
                    Mensaje.Info("No se establecio el tamaño del papel la hoja Excel.\tDebe configurarlo manualmente");
                    if (! Auxiliar.crear_log_error(ex2, "Reporte ingreso"))
                        Mensaje.Error("No se guardo log de error");
                }
            }
            ps.LeftHeader = "&G";
            ps.CenterHeader = cabezera;
            ps.RightHeader = periodo;
            ps.LeftFooter = "&P / &N";
            //ps.CenterFooter = pies;
            ps.RightFooter = "Fecha de Impresion : &D\tHora de Impresion: &T";
            ps.TopMargin = oExcel.InchesToPoints(1.2);
            ps.RightMargin = oExcel.InchesToPoints(1.01);
            ps.LeftMargin = oExcel.InchesToPoints(1.01);
            ps.CenterHorizontally = true;
            

            //Termino de hoja
            oExcel.Visible = true;
            oExcel.DisplayAlerts = true;
            celLrangE = null;
            oSheet = null;
            worKbooK = null;
        }

        public static PD.RespuestaAccion get_ficha_baja(PD.DETAIL_PROCESS info)
        {
            var resultado = new PD.RespuestaAccion();
            try
            {
                List<PD.SINGLE_DETAIL> informacion;
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    informacion = cServ.Proceso.ficha_baja(info);

                ficha_baja_toExcel(informacion);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                resultado.set_ok();
            }
            catch (Exception ex)
            {
                resultado.set(-1, ex.Message);
            }
            return resultado;
        }
        private static void ficha_baja_toExcel(List<PD.SINGLE_DETAIL> info)
        {
            Exc.Application excel;
            Exc.Workbook worKbooK;
            Exc.Worksheet oSheet;
            Exc.Range celLrangE;
            int colN1, filT1, colN2, colN3, colT1, colT2, colT3;
            int filT0, colG0, colG1;
            excel = new Exc.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;

            var firstInfo = info.First();
            worKbooK = excel.Workbooks.Add();
            ExcelWrite.set_sheet_amount(worKbooK, 1);
            oSheet = worKbooK.Sheets[1];
            oSheet.Name = "ficha_baja_cod" + firstInfo.codigo_articulo.ToString();
            ExcelWrite sheetWrite = new ExcelWrite(oSheet);
            //formamos la grilla blanca y de ancho fijo para trabajar sobre ella
            sheetWrite.set_clear_grid();
            
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
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN1], oSheet.Cells[filT1, colT1 - 1]];
            //With oSheet.Range(lcol(colN1) + CStr(filT1) + ":" + lcol(colT1 - 1) + CStr(filT1))
            celLrangE.MergeCells = true;
            celLrangE.Font.Bold = true;
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
            celLrangE.Value = "Código Grupo :";
            celLrangE = null;
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colT1], oSheet.Cells[filT1, colT1 + 2]];
            celLrangE.MergeCells = true;
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
            celLrangE.Value = firstInfo.codigo_articulo;
            celLrangE = null;
            //celLrangE = oSheet.Cells[filT1, colN2];
            //    celLrangE.Value = "Descripción :";
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN2], oSheet.Cells[filT1, colT2]];
            celLrangE.MergeCells = true;
            celLrangE.Font.Bold = true;
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
            celLrangE.Value = "Descripción :";
            celLrangE = null;
            celLrangE = oSheet.Cells[filT1, colT2 + 1];
            celLrangE.Value = firstInfo.descripcion;
            celLrangE = null;

            filT1 = filT1 + 2;

            //celLrangE = oSheet.Cells[filT1, colN1];
            //    celLrangE.Value = "Cantidad :";
            celLrangE = null;
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN1], oSheet.Cells[filT1, (colT1 - 1)]];
            celLrangE.MergeCells = true;
            celLrangE.Font.Bold = true;
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
            celLrangE.Value = "Cantidad :";
            celLrangE = null;
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
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN1], oSheet.Cells[filT1, (colT1 - 2)]];
            celLrangE.MergeCells = true;
            celLrangE.Font.Bold = true;
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
            celLrangE.Value = "Orígen :";
            celLrangE = null;
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
            celLrangE.Value = "Categoria :";
            celLrangE = null;
            oSheet.Cells[filT1, colT2].Value = firstInfo.categoria.description;
            //oSheet.Cells(filT1, colN3) = "Nº Doc. :"
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN3], oSheet.Cells[filT1, (colT3 - 1)]];
            celLrangE.MergeCells = true;
            celLrangE.Font.Bold = true;
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
            celLrangE.Value = "Nº Doc. :";
            celLrangE = null;
            oSheet.Cells[filT1, colT3].Value = firstInfo.num_doc;

            filT1 = filT1 + 2;

            oSheet.Cells[filT1, colN1].Value = "Derecho Credito :";
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN1], oSheet.Cells[filT1, (colT1)]];
            celLrangE.MergeCells = true;
            celLrangE.Font.Bold = true;
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
            celLrangE.Value = "Derecho Credito :";// +(firstInfo.derecho_credito ? "SI" : "NO");
            celLrangE = null;
            oSheet.Cells[filT1, colT1 + 1] = (firstInfo.derecho_credito ? "SI" : "NO");
            oSheet.Cells[filT1, colN2].Value = "Proveedor :";
            celLrangE = oSheet.Range[oSheet.Cells[filT1, colN2], oSheet.Cells[filT1, (colT2 - 1)]];
            celLrangE.MergeCells = true;
            celLrangE.Font.Bold = true;
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignLeft;
            celLrangE = null;
            oSheet.Cells[filT1, colT2].Value = "'" + firstInfo.proveedor;
            var linea_extra = 0;
            if (firstInfo.proveedor != firstInfo.descrip_proveedor)
            {
                oSheet.Cells[filT1 + 1, colT2].Value = firstInfo.descrip_proveedor;
                linea_extra = 1;
            }
            if (hay_ifrs > 0)
            {
                oSheet.Cells[filT1, colN3].Value = "Metodo Revaluación :";
                celLrangE = oSheet.Range[oSheet.Cells[filT1, colN3], oSheet.Cells[filT1, (colT3 + 2)]];
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
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = "Precio Unitario";
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = "Vida Útil";
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = "Valor Residual";
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = "Dep. Acum.";
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            if (hay_ifrs > 0)
            {
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = "Preparación";
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = "Transporte";
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = "Montaje";
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = "Desmantelamiento";
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = "Honorario";
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = "Revalorización";
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            }
            //aplico formato para la columna
            celLrangE = oSheet.Range[oSheet.Cells[(filT0 + 1), colG0], oSheet.Cells[filT1, colG1]];
            foreach(var b in ExcelWrite.ClasicBorders)
                celLrangE.Borders[b].ColorIndex = 1;
            celLrangE.Font.Bold = true;
            celLrangE = null;

            //columna 2
            //filT0 = filT0   'fila inicio de la sección no cambia
            filT1 = filT0;   //vuelvo al inicio el contador de filas

            colG0 = colG1 + 1;   //1 columna a la derecha de donde termino la anterior
            colG1 = colG0 + 2;   //seteo nuevo rango, en este caso seran 4

            var FinClp = info.Where(i => i.fuente.ENVIORMENT == "FIN" && i.fuente.CURRENCY == "CLP").FirstOrDefault();
            oSheet.Cells[filT1, colG0].Value = "FINANCIERO";
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = FinClp.precio_base;
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = FinClp.vida_util_base;
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = FinClp.valor_residual;
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = FinClp.depreciacion_acum;
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            if (hay_ifrs > 0)
            {
                for (int a = 0; a < 6; a++)
                {
                    filT1 = filT1 + 1;
                    oSheet.Cells[filT1, colG0].Value = "-";
                    oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                }
            }
            //aplico formato para la columna
            celLrangE = oSheet.Range[oSheet.Cells[(filT0), colG0], oSheet.Cells[filT1, colG1]];
            foreach (var b in ExcelWrite.ClasicBorders)
                celLrangE.Borders[b].ColorIndex = 1;
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
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = TribCLP.precio_base;
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = TribCLP.vida_util_base;
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = TribCLP.valor_residual;
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            filT1 = filT1 + 1;
            oSheet.Cells[filT1, colG0].Value = TribCLP.depreciacion_acum;
            oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
            if (hay_ifrs > 0)
            {
                for (int a = 0; a < 6; a++)
                {
                    filT1 = filT1 + 1;
                    oSheet.Cells[filT1, colG0].Value = "-";
                    oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                }
            }
            //aplico formato para la columna
            celLrangE = oSheet.Range[oSheet.Cells[(filT0), colG0], oSheet.Cells[filT1, colG1]];
            foreach (var b in ExcelWrite.ClasicBorders)
                celLrangE.Borders[b].ColorIndex = 1;
            //celLrangE.Font.Bold = true;
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignRight; // -4152;    //xlRight
            celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignTop;     // -4160;      //xlTop
            celLrangE.NumberFormat = "#,##0";
            celLrangE = null;

            if (hay_ifrs > 0)
            {
                //columna 4
                //filT0 = filT0;   //fila inicio de la sección no cambia
                filT1 = filT0;   //vuelvo al inicio el contador de filas

                colG0 = colG1 + 1;   //1 columna a la derecha de donde termino la anterior
                colG1 = colG0 + 2;   //seteo nuevo rango, en este caso seran 4

                var IfrsCLP = info.Where(i => i.fuente.ENVIORMENT == "IFRS" && i.fuente.CURRENCY == "CLP").FirstOrDefault();
                oSheet.Cells[filT1, colG0].Value = "IFRS CLP";
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.precio_base;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.vida_util_base;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.valor_residual;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.depreciacion_acum;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.preparacion;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.transporte;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.montaje;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.desmantel;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.honorario;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsCLP.revalorizacion;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                //aplico formato para la columna
                celLrangE = oSheet.Range[oSheet.Cells[(filT0), colG0], oSheet.Cells[filT1, colG1]];
                foreach (var b in ExcelWrite.ClasicBorders)
                    celLrangE.Borders[b].ColorIndex = 1;
                //celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignRight; // -4152;    //xlRight
                celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignTop;     // -4160;      //xlTop
                celLrangE.NumberFormat = "#,##0";
                celLrangE = null;
            }
            //aca empieza ifrs yen
            var IfrsYen = info.Where(i => i.fuente.ENVIORMENT == "IFRS" && i.fuente.CURRENCY == "YEN").FirstOrDefault();
            if (IfrsYen != null)
            {
                //columna 5
                //filT0 = filT0;   //fila inicio de la sección no cambia
                filT1 = filT0;   //'vuelvo al inicio el contador de filas

                colG0 = colG1 + 1;   //1 columna a la derecha de donde termino la anterior
                colG1 = colG0 + 2;   //seteo nuevo rango, en este caso seran 4

                oSheet.Cells[filT1, colG0].Value = "IFRS YEN";
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.precio_base;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.vida_util_base;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.valor_residual;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.depreciacion_acum;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.preparacion;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.transporte;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.montaje;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.desmantel;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.honorario;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                filT1 = filT1 + 1;
                oSheet.Cells[filT1, colG0].Value = IfrsYen.revalorizacion;
                oSheet.Range[oSheet.Cells[filT1, colG0], oSheet.Cells[filT1, colG1]].MergeCells = true;
                //aplico formato para la columna
                celLrangE = oSheet.Range[oSheet.Cells[(filT0), colG0], oSheet.Cells[filT1, colG1]];
                foreach (var b in ExcelWrite.ClasicBorders)
                    celLrangE.Borders[b].ColorIndex = 1;
                //celLrangE.Font.Bold = true;
                celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignRight; // -4152;    //xlRight
                celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignTop;     // -4160;      //xlTop
                celLrangE.NumberFormat = "#,##0";
                celLrangE = null;
            }
            //tercera seccion, ingreso de descripciones, si las hubiera.
            //'cuarta seccion lineas para firma
            int largoFirma, filFirma, ColFirma;
            largoFirma = 4;      //'contendra 5 celdas
            filFirma = filT1 + 7;
            ColFirma = 5;
            celLrangE = oSheet.Range[oSheet.Cells[filFirma,ColFirma],oSheet.Cells[filFirma,(ColFirma + largoFirma)]];
            //With oSheet.Range(lcol(ColFirma) + CStr(filFirma) + ":" + lcol(ColFirma + largoFirma) + CStr(filFirma))
            celLrangE.MergeCells = true;
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignCenter;    //xlCenter:  -4108
            celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignCenter;      //xlCenter:   -4108
            celLrangE.Borders[Exc.XlBordersIndex.xlEdgeTop].ColorIndex = 1; //Exc.XlBordersIndex.xlEdgeTop = 8
            celLrangE.Value = "Contador General";
            celLrangE = null;
            ColFirma = ColFirma + largoFirma + 3;
            celLrangE = oSheet.Range[oSheet.Cells[filFirma,ColFirma],oSheet.Cells[filFirma + 1,(ColFirma + largoFirma)]];
            //With oSheet.Range(lcol(ColFirma) + CStr(filFirma) + ":" + lcol(ColFirma + largoFirma) + CStr(filFirma + 1))
            celLrangE.MergeCells = true;
            celLrangE.HorizontalAlignment = Exc.XlHAlign.xlHAlignCenter;    //xlCenter:  -4108
            celLrangE.VerticalAlignment = Exc.XlVAlign.xlVAlignCenter;      //xlCenter:   -4108
            celLrangE.Borders[Exc.XlBordersIndex.xlEdgeTop].ColorIndex = 1; //Exc.XlBordersIndex.xlEdgeTop = 8
            celLrangE.Value = "Gerencia" + Environment.NewLine + "(Opcional)";
            celLrangE = null;

            //quinta sección añadir fotos si las hubiera
            //int indx_pag;
            bool config_hoja = true;
            //string imagen, pies;
            string nom_pag, cabezera, periodo, nombre_total;
            //for(int i = 0; i<= rpt2.Rows.Count - 1; i++)
            //{
            //    int Atrib = rpt2.Rows(i).Item("cod_atrib");
            //    if (Atrib == 16 || Atrib == 17 || Atrib == 18) 
            //    {
            //        int totalSheets = worKbooK.Sheets.Count;
            //        worKbooK.Sheets.Add(After: worKbooK.Sheets[totalSheets]).Name = "00";
            //        indx_pag = totalSheets+1;
            //        //imagen = base.dirFotos + rpt2.Rows(i).Item("dscr_detalle")
            //        //nom_pag = rpt2.Rows(i).Item("atributo")
            //        if (rpt2.Rows(i).Item("codigo") != "")
            //            nom_pag = nom_pag + "-" + rpt2.Rows(i).Item("codigo");

            //        cabezera = nom_pag;
            //        pies = "&P / &N";
            //        Exc.Worksheet fSheet = worKbooK.Sheets[indx_pag];
            //        fSheet.Shapes.AddPicture(imagen, false, true, 0, 0, 709, 482);
            //        fSheet.Name = nom_pag;
            //        Exc.PageSetup ps =  fSheet.PageSetup;
            //        ps.Orientation = 2;
            //        try
            //        {
            //            if(config_hoja)
            //                ps.PaperSize = Exc.XlPaperSize.xlPaperFolio;
            //        }
            //        catch(Exception ex)
            //        {
            //            try
            //            {
            //                if(config_hoja)
            //                    ps.PaperSize = Exc.XlPaperSize.xlPaperLetter;

            //            }
            //            catch (Exception ex2)
            //            {
            //                config_hoja = false;
            //                P.Mensaje.Info("No se establecio el tamaño del papel la hoja Excel. \t Debe configurarlo manualmente");
            //                if( !crear_log_error(ex2) )
            //                    P.Mensaje.Error("No se guardo log de error");

            //            }
            //        }
            //        ps.CenterHeader = cabezera;
            //        ps.CenterFooter = pies;
            //        ps.CenterHorizontally = true;
            //        ps.CenterVertically = true;
            //    }
            //}

            //configurar pagina
            nom_pag = "ficha_baja";
            string causa;
            int Fcausa = firstInfo.estado;// rpt1.Rows(0).Item("cod_est")
            switch (Fcausa)
            {
                case 3:
                    causa = "Castigo";
                    break;
                case 2:
                    causa = "Venta";
                    break;
                default:
                    causa = "";
                    break;
            }

            cabezera = "&\"-,Negrita\"&20Ficha de " + causa + "\t Activo Fijo";
            periodo = "&25 " + firstInfo.fecha_inicio.ToString("yyyy-MM");
            oSheet.Name = nom_pag;
            nombre_total = Auxiliar.fileLogo;
            oSheet.PageSetup.LeftHeaderPicture.Filename = nombre_total;
            Exc.PageSetup ps = oSheet.PageSetup;
            ps.Orientation = Exc.XlPageOrientation.xlLandscape; //2
            try
            {
                if (config_hoja)
                    ps.PaperSize = Exc.XlPaperSize.xlPaperFolio;
            }
            catch
            {
                try
                {
                    if (config_hoja)
                        ps.PaperSize = Exc.XlPaperSize.xlPaperLetter;
                }
                catch (Exception ex2)
                {
                    config_hoja = false;
                    Mensaje.Info("No se establecio el tamaño del papel la hoja Excel.\t Debe configurarlo manualmente");
                    if (!Auxiliar.crear_log_error(ex2, "Reporte Baja"))
                        Mensaje.Error("No se guardo log de error");
                }
            }
            ps.LeftHeader = "&G";
            ps.CenterHeader = cabezera;
            ps.RightHeader = periodo;
            ps.LeftFooter = "&P / &N";
            ps.RightFooter = "Fecha de Impresion : &D \t Hora de Impresion: &T";
            ps.TopMargin = excel.InchesToPoints(1.2);
            ps.RightMargin = excel.InchesToPoints(1.01);
            ps.LeftMargin = excel.InchesToPoints(1.01);
            ps.CenterHorizontally = true;
            


            //Termino de hoja
            excel.Visible = true;
            excel.DisplayAlerts = true;
            celLrangE = null;
            oSheet = null;
            worKbooK = null;
        }

        public static PD.RespuestaAccion get_ficha_cambio(PD.DETAIL_MOVEMENT info)
        {
            var resultado = new PD.RespuestaAccion();
            try
            {
                List<PD.DETAIL_MOVEMENT> informacion;
                using (var cServ = new ServiceProcess.ServiceAFN2())
                    informacion = cServ.Proceso.ficha_cambio(info);
                //sql_datos = "AFN_reporte_inicio2 " + cod_articulo.ToString
                var info_articulos = new List<PD.SV_ARTICLE_DETAIL>(); //maestro.ejecuta(sql_datos)
                ficha_cambio_toExcel(informacion, info_articulos);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                resultado.set_ok();
            }
            catch (Exception ex)
            {
                resultado.set(-1, ex.Message);
            }
            return resultado;
        }
        private static void ficha_cambio_toExcel(List<PD.DETAIL_MOVEMENT> info, List<PD.SV_ARTICLE_DETAIL> info_articulos)
        {
            
            PD.DETAIL_MOVEMENT fin_clp, tributario, ifrs_clp, ifrs_yen;
            List<PD.SV_ARTICLE_DETAIL> inf_art_grupo, inf_art_solo, inf_art_foto;
            string dir_archivos, PArchivo, Narchivo;
            bool DocAbierto;
            fin_clp = info.Where(i => i.sistema.CURRENCY == "CLP" && i.sistema.ENVIORMENT == "FIN").FirstOrDefault();
            tributario = info.Where(i => i.sistema.CURRENCY == "CLP" && i.sistema.ENVIORMENT == "TRIB").FirstOrDefault();
            ifrs_clp = info.Where(i => i.sistema.CURRENCY == "CLP" && i.sistema.ENVIORMENT == "IFRS").FirstOrDefault();
            ifrs_yen = info.Where(i => i.sistema.CURRENCY == "YEN" && i.sistema.ENVIORMENT == "IFRS").FirstOrDefault();
            
            dir_archivos = Auxiliar.dirArchivos;
            PArchivo = "";
            DocAbierto = true;      //asumo que archivo está abierto
            do
            {
                Narchivo = "ficha_cambio" + PArchivo + ".xlsx";
                try
                {
                    File.WriteAllBytes(dir_archivos + Narchivo, Properties.Resources.ficha_cambio);
                    DocAbierto = false;
                }
                catch
                {
                    //archivo está abierto al intentar sobreescribir, cambio la parte
                    if (PArchivo == "")
                    {
                        PArchivo = "(1)";
                    }
                    else
                    {
                        int numero =  int.Parse( PArchivo.Substring(1, PArchivo.Length - 2));
                        numero = numero + 1;
                        PArchivo = "(" +numero.ToString() + ")";
                    }
                }
            }while (DocAbierto);

            Exc.Application oExcel = new Exc.Application();
            Exc.Workbook oBook = oExcel.Workbooks.Open(dir_archivos + Narchivo);
            Exc.Worksheet HojaMain;
            DateTime TmpFecha;
            int inicio2 = 14;
            HojaMain = oBook.Sheets[1];
            int[] PhotoAtrib = new int[] {16,17,18};
            inf_art_grupo = info_articulos.Where(a => a.article_id == null && !PhotoAtrib.Contains(a.cod_atrib)).ToList();
            inf_art_solo = info_articulos.Where(a => a.article_id != null && !PhotoAtrib.Contains(a.cod_atrib)).ToList();
            inf_art_foto = info_articulos.Where(a => PhotoAtrib.Contains(a.cod_atrib)).ToList();
            
            HojaMain.Cells[1, 5].Value = fin_clp.cod_articulo;
            HojaMain.Cells[1, 13].Value = fin_clp.desc_breve;
            HojaMain.Cells[3, 5].Value = fin_clp.cantidad;
            HojaMain.Cells[3, 14].Value = fin_clp.clase.descrip;
            HojaMain.Cells[3, 28].Value = fin_clp.subclase.description;
            HojaMain.Cells[5, 4].Value = fin_clp.origen.description;
            HojaMain.Cells[5, 14].Value = fin_clp.zona.name; //("txt_zona_act")
            HojaMain.Cells[5, 28].Value = fin_clp.subzona.descrip;  //("txt_subz_act")
            TmpFecha = fin_clp.fecha_inicio;
            HojaMain.Cells[7, 5].Value = TmpFecha.ToString("dd-MM-yyyy");
            HojaMain.Cells[7, 14].Value = fin_clp.zona_anterior.name;  //("txt_zona_ant")
            HojaMain.Cells[7, 28].Value = fin_clp.subzona_anterior.descrip;   //("txt_subz_ant")
            TmpFecha = fin_clp.fecha_compra;
            HojaMain.Cells[9, 5].Value = TmpFecha.ToString("dd-MM-yyyy");
            HojaMain.Cells[9, 14].Value = fin_clp.categoria.description;
            HojaMain.Cells[9, 28].Value = fin_clp.num_documento;
            HojaMain.Cells[11, 6].Value = (fin_clp.derecho_credito?"SI":"NO");
            HojaMain.Cells[11, 14].Value = fin_clp.proveedor_id;
            if (fin_clp.proveedor_id != fin_clp.proveedor_name)
            {
                HojaMain.Cells[12, 14].Value = fin_clp.proveedor_name;
            }
            else
            {
                Exc.Range fila_extra;
                fila_extra = HojaMain.Rows[12];
                fila_extra.Delete();
                fila_extra = null;
                inicio2 = inicio2 - 1;
            }
            if (ifrs_clp != null)
            {
                //valor para metodo de valorizacion
                HojaMain.Cells[11, 29].Value = ifrs_clp.metodo_reval.descrip;
            }
            else
            {
                //limpio el titulo de metodo valorizacion
                HojaMain.Cells[11, 23].Value = String.Empty;
                HojaMain.Cells[11, 29].Value = String.Empty;
            }
            //financiero
            HojaMain.Cells[inicio2 + 2, 7].Value = fin_clp.valor_activo_final;  //("precio_base")
            HojaMain.Cells[inicio2 + 3, 7].Value = fin_clp.vida_util_ocupada;   //("vida_util")
            HojaMain.Cells[inicio2 + 4, 7].Value = fin_clp.valor_residual;
            HojaMain.Cells[inicio2 + 5, 7].Value = fin_clp.depreciacion_acum_final; //("depreciacion_acum")
            //tributario
            HojaMain.Cells[inicio2 + 2, 10].Value = tributario.valor_activo_final;
            HojaMain.Cells[inicio2 + 3, 10].Value = tributario.vida_util_ocupada; 
            HojaMain.Cells[inicio2 + 4, 10].Value = tributario.valor_residual;
            HojaMain.Cells[inicio2 + 5, 10].Value = tributario.depreciacion_acum_final;
            //analizo si existe ifrs o no
            if (ifrs_clp != null && ifrs_yen != null)
            {
                //ingreso ifrs clp
                HojaMain.Cells[inicio2 + 2, 13].Value = ifrs_clp.valor_activo_final;
                HojaMain.Cells[inicio2 + 3, 13].Value = ifrs_clp.vida_util_ocupada;
                HojaMain.Cells[inicio2 + 4, 13].Value = ifrs_clp.valor_residual;
                HojaMain.Cells[inicio2 + 5, 13].Value = ifrs_clp.depreciacion_acum_final;
                HojaMain.Cells[inicio2 + 6, 13].Value = ifrs_clp.preparacion;
                HojaMain.Cells[inicio2 + 7, 13].Value = ifrs_clp.transporte;
                HojaMain.Cells[inicio2 + 8, 13].Value = ifrs_clp.montaje;
                HojaMain.Cells[inicio2 + 9, 13].Value = ifrs_clp.desmantelamiento;
                HojaMain.Cells[inicio2 + 10, 13].Value = ifrs_clp.honorario;
                HojaMain.Cells[inicio2 + 11, 13].Value = ifrs_clp.revalorizacion;
                //ingreso ifrs yen
                HojaMain.Cells[inicio2 + 2, 16].Value = ifrs_yen.valor_activo_final;
                HojaMain.Cells[inicio2 + 3, 16].Value = ifrs_yen.vida_util_ocupada;
                HojaMain.Cells[inicio2 + 4, 16].Value = ifrs_yen.valor_residual;
                HojaMain.Cells[inicio2 + 5, 16].Value = ifrs_yen.depreciacion_acum_final;
                HojaMain.Cells[inicio2 + 6, 16].Value = ifrs_yen.preparacion;
                HojaMain.Cells[inicio2 + 7, 16].Value = ifrs_yen.transporte;
                HojaMain.Cells[inicio2 + 8, 16].Value = ifrs_yen.montaje;
                HojaMain.Cells[inicio2 + 9, 16].Value = ifrs_yen.desmantelamiento;
                HojaMain.Cells[inicio2 + 10, 16].Value = ifrs_yen.honorario;
                HojaMain.Cells[inicio2 + 11, 16].Value = ifrs_yen.revalorizacion;
            }
            else
            {
                //limpio celdas que eran para ifrs
                Exc.Range zona1, zona2; 
                zona1 = HojaMain.Range["B" + (inicio2 + 6).ToString() + ":R" + (inicio2 + 11).ToString() + ""];
                zona2 = HojaMain.Range["M" + (inicio2 + 1).ToString() + ":R" + (inicio2 + 11).ToString() + ""];
                    
                zona1.Value = "";
                zona1.Borders[Exc.XlBordersIndex.xlEdgeRight].ColorIndex = 2;
                zona1.Borders[Exc.XlBordersIndex.xlEdgeLeft].ColorIndex = 2;
                zona1.Borders[Exc.XlBordersIndex.xlEdgeBottom].ColorIndex = 2;
                zona1.Borders[Exc.XlBordersIndex.xlInsideHorizontal].ColorIndex = 2;
                zona1.Borders[Exc.XlBordersIndex.xlInsideVertical].ColorIndex = 2;
                    
                zona2.Value = "";
                zona2.Borders[Exc.XlBordersIndex.xlEdgeTop].ColorIndex = 2;
                zona2.Borders[Exc.XlBordersIndex.xlEdgeRight].ColorIndex = 2;
                zona2.Borders[Exc.XlBordersIndex.xlEdgeBottom].ColorIndex = 2;
                zona2.Borders[Exc.XlBordersIndex.xlInsideHorizontal].ColorIndex = 2;
                zona2.Borders[Exc.XlBordersIndex.xlInsideVertical].ColorIndex = 2;
                    
                zona1 = null;
                zona2 = null;
            }
            //revisar detalle de articulos
            int pos1;
            pos1 = inicio2;
            //oExcel.Visible = True
            if (inf_art_grupo.Count > 0)
            {
                //agrego descripcion de grupo
                Exc.Range Rtitulo = HojaMain.Cells[pos1, 20];
                    
                Rtitulo.Value = "DESCRIPCIÓN GENERICA";
                Rtitulo.Font.Bold = true;
                Rtitulo.Font.Underline = true;
                    
                Rtitulo = null;
                pos1 = pos1 + 2;
                foreach(var info_fila in inf_art_grupo)
                {
                    HojaMain.Cells[pos1, 20].Value = info_fila.atributo.name;
                    HojaMain.Cells[pos1, 24].Value = info_fila.detalle;
                    HojaMain.Range["X" + pos1.ToString() + ":AK" + pos1.ToString()].Merge();
                    pos1 = pos1 + 1;
                }
                pos1 = pos1 + 1;
            }
            if (inf_art_solo.Count > 0)
            {
                //agrego descripcion de articulos individuales
                Exc.Range Rtitulo = HojaMain.Cells[pos1, 20];
                    
                Rtitulo.Value = "DESCRIPCIÓN ESPECIFICA";
                Rtitulo.Font.Bold = true;
                Rtitulo.Font.Underline = true;
                    
                Rtitulo = null;
                pos1 = pos1 + 2;
                foreach(var info_fila in inf_art_solo)
                {
                    HojaMain.Cells[pos1, 20].Value = info_fila.article_id;
                    HojaMain.Cells[pos1, 24].Value = info_fila.atributo.name;
                    HojaMain.Cells[pos1, 28].Value = info_fila.detalle;
                    HojaMain.Range["X" + pos1.ToString() + ":AA" + pos1.ToString()].Merge();
                    HojaMain.Range["AB" + pos1.ToString() + ":AK" + pos1.ToString()].Merge();
                    pos1 = pos1 + 1;
                }
            }
            
            //fin de oSheet(1)

            //reviso fotos
            /*
            Dim HojaFoto As Excel.Worksheet
            HojaFoto = oBook.Sheets(2)
            If inf_art_foto.Count > 0 Then
                'determino si ocupare solo la hoja que existe o si debo copiar mas
                If inf_art_foto.Count > 1 Then
                    'copio la hoja las veces que necesite
                    For i = 2 To inf_art_foto.Count
                        HojaFoto.Copy(After:=HojaFoto)
                    Next
                End If
                'declaro variables necesarias
                Dim indice_hoja As Integer
                Dim imagen, nom_pagina As String
                indice_hoja = 2
                For Each fila_foto In inf_art_foto
                    Dim HTfoto As Excel.Worksheet = oBook.Sheets(indice_hoja)
                    If fila_foto("codigo") = "" Then
                        nom_pagina = fila_foto("atributo")
                    Else
                        nom_pagina = fila_foto("atributo") + "-"
                        nom_pagina = nom_pagina + Strings.Right(fila_foto("codigo"), 31 - nom_pagina.Length)
                    End If
                    imagen = base.dirFotos + fila_foto("detalle")
                    With HTfoto
                        .Name = nom_pagina
                        .Shapes.AddPicture(imagen, False, True, 0, 0, 709, 482)
                    End With

                    indice_hoja = indice_hoja + 1
                Next
            Else
                'borro hoja de fotos ya que no se usara
                HojaFoto.Delete()
            End If
             * */
            HojaMain.Select();

            HojaMain = null;
            //HojaFoto = null;

            oExcel.Visible = true;
        }
        #endregion

        #region Contabilizacion
        public static List<PD.DETAIL_ACCOUNT> CONTABILIZAR_GP2013(Vperiodo periodo)
        {
            using (var cServ = new ServiceProcess.ServiceAFN2())
                return cServ.Proceso.CONTABILIZAR_GP2013(periodo);
        }
        #endregion

    }
}
