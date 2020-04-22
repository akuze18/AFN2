using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Tracking
{
    internal class ExportTo
    {
        private static string FullFileName(string FileName)
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            return currentPath + FileName + ".txt";
        }

        public static void FileText(List<PD.DETAIL_PROCESS> ListData, string FileName)
        {
            string FullFile = FullFileName( FileName );
            using (StreamWriter file = new StreamWriter(FullFile,false, Encoding.UTF8))
            {
                #region Titulo
                string titulos;
                titulos = "LoteArticulo\t" +
                    "Parte\t" +
                    "Cantidad\t" +
                    "FechaIngreso\t" +
                    "FechaCompra\t" +
                    "FechaInicio\t" +
                    "FechaFin\t" +
                    "Vigencia\t" +
                    "Zona\t" +
                    "Clase\t" +
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
                        Data.fecha_ing.ToShortDateString() + "\t" +
                        Data.fecha_compra.ToShortDateString() + "\t" +
                        Data.fecha_inicio.ToShortDateString() + "\t" +
                        Data.fecha_fin.ToShortDateString() + "\t" +
                        Data.vigencia.name + "\t" +
                        Data.zona.codDept + "\t" +
                        Data.clase.cod + "\t" +
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
        public static void FileText(List<PD.DETAIL_MOVEMENT> ListData, string FileName)
        {
            string FullFile = FullFileName( FileName );
            using (StreamWriter file = new StreamWriter(FullFile, false, Encoding.UTF8))
            {
                #region Titulo
                string titulos;
                titulos = "LoteArticulo\t" +
                    "Parte\t" +
                    "Cantidad\t" +
                    "FechaCompra\t" +
                    "FechaIngreso\t" +
                    "FechaInicio\t" +
                    "FechaFin\t" +
                    "Vigencia\t" +
                    "Zona\t" +
                    "Clase\t" +
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
                        Data.fecha_compra.ToShortDateString() + "\t" +
                        Data.fecha_ingreso.ToShortDateString() + "\t" +
                        Data.fecha_inicio.ToShortDateString()+ "\t" +
                        Data.fecha_fin.ToShortDateString() + "\t" +
                        Data.vigencia + "\t" +
                        Data.zona + "\t" +
                        Data.clase+ "\t" +
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

        public static void FileText(List<PD.DETAIL_ACCOUNT> ListData, string FileName)
        {
            string FullFile = FullFileName(FileName);
            using (StreamWriter file = new StreamWriter(FullFile, false, Encoding.UTF8))
            {
                #region Titulo
                string titulos;
                titulos = "periodo\t" +
                    "grupo_id\t" +
                    "grupo_desc\t" +
                    "tipo_cont_id\t" +
                    "tipo_cont_desc\t" +
                    "NUM_CUENTA\t" +
                    "DSC_CUENTA\t" +
                    "valor_antes\t" +
                    "valor_actual\t" +
                    "dim_lugar\t" +
                    "dim_depto\t" +
                    "codigo\t" +
                    "parte\t" +
                    "situacion\t" +
                    "Fclase\t" +
                    "Fzona\t" +
                    "Fsubzona\t" +
                    "fingreso\t" +
                    "signo\t" +
                    "FEstado_code\t" +
                    "FEstado_desc\t";
                file.WriteLine(titulos);
                #endregion
                #region Valores
                foreach (var Data in ListData)
                {
                    string values;
                    values = Data.periodo.lastDB + "\t" +
                        Data.grupo.code + "\t" +
                        Data.grupo.description + "\t" +
                        Data.tipo_cont.code + "\t" +
                        Data.tipo_cont.description + "\t" +
                        Data.NUM_CUENTA + "\t" +
                        Data.DSC_CUENTA + "\t" +
                        Data.valor_antes + "\t" +
                        Data.valor_actual + "\t" +
                        Data.dim_lugar + "\t" +
                        Data.dim_depto + "\t" +
                        Data.codigo.ToString() + "\t" +
                        Data.parte.ToString() + "\t" +
                        Data.situacion + "\t" +
                        Data.Fclase.code + "\t" +
                        Data.Fzona.code + "\t" +
                        Data.Fsubzona.code + "\t" +
                        Data.fingreso.ToShortDateString() + "\t" +
                        Data.FEstado.code+ "\t" +
                        Data.FEstado.description + "\t" 
                        ;
                    
                    file.WriteLine(values);
                }
                #endregion
            }
        }
    }
}
