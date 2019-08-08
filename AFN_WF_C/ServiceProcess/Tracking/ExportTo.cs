using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using DC = AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Tracking
{
    internal class ExportTo
    {
        private static string FullFileName(string FileName)
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            return currentPath + FileName + ".txt";
        }

        public static void FileText(List<DC.DETAIL_PROCESS> ListData, string FileName)
        {
            string FullFile = FullFileName( FileName );
            using (StreamWriter file = new StreamWriter(FullFile,false, Encoding.UTF8))
            {
                #region Titulo
                string titulos;
                titulos = "LoteArticulo\t" +
                    "Parte\t" +
                    "Cantidad\t" +
                    "FechaInicio\t" +
                    "FechaFin\t" +
                    "Vigencia\t" +
                    "Zona\t" +
                    "Clase\t" +
                    "FechaCompra\t" +
                    "PartId\t" +
                    "HeadId\t" +
                    "RefSource\t";
                for (int i = 1; i <= 13; i++)
                {
                    titulos = titulos + "ParamName" + i.ToString() + "\t" +
                    "ParamValue"+i.ToString()+"\t"; 
                }
                file.WriteLine(titulos);
                #endregion
                #region Valores
                foreach (var Data in ListData)
                {
                    string values;
                    values = Data.cod_articulo.ToString() + "\t" +
                        Data.parte.ToString() + "\t" +
                        Data.cantidad.ToString() + "\t" +
                        Data.fecha_inicio.ToShortDateString() + "\t" +
                        Data.fecha_fin.ToShortDateString() + "\t" +
                        Data.vigencia + "\t" +
                        Data.zona + "\t" +
                        Data.clase + "\t" +
                        Data.fecha_compra.ToShortDateString() + "\t" +
                        Data.PartId.ToString() + "\t" +
                        Data.HeadId.ToString() + "\t" +
                        Data.RefSource + "\t"
                        ;
                    foreach (var Param in Data.parametros)
                    {
                        values = values + Param.name + "\t" +
                                Param.value.ToString() + "\t";
                    }
                    file.WriteLine(values);
                }
                #endregion
            }
        }
        public static void FileText(List<DC.DETAIL_MOVEMENT> ListData, string FileName)
        {
            string FullFile = FullFileName( FileName );
            using (StreamWriter file = new StreamWriter(FullFile, false, Encoding.UTF8))
            {
                #region Titulo
                string titulos;
                titulos = "LoteArticulo\t" +
                    "Parte\t" +
                    "Cantidad\t" +
                    "FechaInicio\t" +
                    "FechaFin\t" +
                    "Vigencia\t" +
                    "Zona\t" +
                    "Clase\t" +
                    "FechaCompra\t" +
                    "Origen\t" +
                    "Situacion\t" +
                    "valor_activo_inicial\t" +
                    "credito_monto\t" +
                    "valor_activo_final\t" +
                    "depreciacion_acum_inicial\t" +
                    "deterioro\t" +
                    "valor_residual\t" +
                    "valor_sujeto_dep\t" +
                    "vida_util_asignada\t" +
                    "vida_util_ocupada\t" +
                    "vida_util_residual\t" +
                    "depreciacion_ejercicio\t" +
                    "depreciacion_acum_final\t" +
                    "valor_libro\t" +
                    "porcentaje_cm\t" +
                    "valor_activo_cm\t" +
                    "valor_activo_update\t" +
                    "depreciacion_acum_cm\t" +
                    "depreciacion_acum_update\t" +
                    "preparacion\t" +
                    "desmantelamiento\t" +
                    "transporte\t" +
                    "montaje\t"+
                    "honorario\t" +
                    "revalorizacion\t";
                file.WriteLine(titulos);
                #endregion
                #region Valores
                foreach (var Data in ListData)
                {
                    string values;
                    values = Data.cod_articulo.ToString() + "\t" +
                        Data.parte.ToString() + "\t" +
                        Data.cantidad.ToString() + "\t" +
                        Data.fecha_inicio.ToShortDateString()+ "\t" +
                        Data.fecha_fin.ToShortDateString() + "\t" +
                        Data.vigencia + "\t" +
                        Data.zona + "\t" +
                        Data.clase+ "\t" +
                        Data.fecha_compra.ToShortDateString() + "\t" +
                        Data.origen + "\t" +
                        Data.situacion + "\t" +
                        Data.valor_activo_inicial + "\t" +
                        Data.credito_monto+ "\t" +
                        Data.valor_activo_final+ "\t" +
                        Data.depreciacion_acum_inicial+ "\t" +
                        Data.deterioro + "\t" +
                        Data.valor_residual + "\t" +
                        Data.valor_sujeto_dep + "\t" +
                        Data.vida_util_asignada+ "\t" +
                        Data.vida_util_ocupada + "\t" +
                        Data.vida_util_residual + "\t" +
                        Data.depreciacion_ejercicio + "\t" +
                        Data.depreciacion_acum_final + "\t" +
                        Data.valor_libro + "\t" +
                        Data.porcentaje_cm + "\t" +
                        Data.valor_activo_cm + "\t" +
                        Data.valor_activo_update + "\t" +
                        Data.depreciacion_acum_cm + "\t" +
                        Data.depreciacion_acum_update + "\t" +
                        Data.preparacion + "\t" +
                        Data.desmantelamiento + "\t" +
                        Data.transporte + "\t" +
                        Data.montaje + "\t" +
                        Data.honorario + "\t" +
                        Data.revalorizacion + "\t"
                        ;
                    file.WriteLine(values);
                }
                #endregion
            }
        }
    }
}
