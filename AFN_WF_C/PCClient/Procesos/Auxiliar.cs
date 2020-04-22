using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Drawing;
using System.Security.Principal;
using System.IO;

namespace AFN_WF_C.PCClient.Procesos
{
    public class Auxiliar
    {
        public static void ActivarF(Control elemento,Boolean stat = true)
        {
            elemento.Enabled = stat;
            //if (!(elemento.GetType() == typeof(DateTimePicker)))
            //{
            //    if( elemento.Enabled){
            //        elemento.BackColor = Color.White;
            //    }else{
            //        elemento.BackColor = Color.Silver;
            //    }
            //}
        }

        public static string getSeparadorMil
        {
            get
            {
                //Dim oldDecimalSeparator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
                string oldGroupSeparator = Application.CurrentCulture.NumberFormat.NumberGroupSeparator;
                //Dim oldListSeparator As String = Application.CurrentCulture.TextInfo.ListSeparator

                //Dim o1, o2, o3, o4, o5, o6 As String
                //o1 = Application.CurrentCulture.NumberFormat.NumberGroupSeparator()
                //o2 = System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
                //o3 = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator

                //o4 = Application.CurrentCulture.TextInfo.ListSeparator
                //o5 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator
                //o6 = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator

                return oldGroupSeparator;
            }
        }

        public static void bloquearW(Form reporte)
        {
            reporte.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            reporte.Enabled = false;
        }

        public static void desbloquearW(Form reporte)
        {
            reporte.Cursor = System.Windows.Forms.Cursors.Default;
            reporte.Enabled = true;
        }

        public static Vistas.Cambios.ingreso FindPadre(Control hijo)
        {
            if (hijo.Parent == null)
                return null;
            else
            {
                if (hijo.Parent.GetType() == typeof(Vistas.Cambios.ingreso))
                {
                    return (Vistas.Cambios.ingreso)hijo.Parent;
                }
                else
                {
                    return FindPadre(hijo.Parent);
                }
            }
        }
        public static TabPage FindPage(Control hijo)
        {
            if (hijo.Parent == null)
                return null;
            else
            {
                if (hijo.Parent.GetType() == typeof(TabPage))
                {
                    return (TabPage)hijo.Parent;
                }
                else
                {
                    return FindPage(hijo.Parent);
                }
            }
        }


        public static string getUser()
        {
            using (var WIdentity = WindowsIdentity.GetCurrent())
            {
                //SecurityIdentifier user = WIdentity.User;
                string name = WIdentity.Name.Split('\\')[1];
                return name;
            }
        }

        public static string base_dato { get { return "ATENEA\\GP2013"; } }
        public static string servidor { get { return "AFN2"; } }

        
        #region "Directorio de la App"

        public static string rutaApp { get { return System.AppDomain.CurrentDomain.BaseDirectory; } }
        
        public static string rutaSrv {get { return "\\\\Zeus\\Programas\\Activo Fijo 2\\"; }}

        public static string[] dirAll {get { return new string[] {dirArchivos, dirError, dirFotos};}}
            
        public static string dirArchivos { get { return rutaApp + "files\\"; } }
        public static string dirError { get { return rutaApp + "log_error\\"; } }
            
        public static string dirFotos { get { return rutaSrv + "fotos_AF\\"; } }

        public static string fileLogo { get { return dirArchivos + "logo_nippon.jpg"; } }

        public static string fileFontBarcode { get { return dirArchivos + "FRE3OF9X.TTF"; } }

        public static string fileFontLabel { get { return dirArchivos + "BrowalliaUPC.TTF"; } }
        #endregion

        

        public static bool crear_log_error(Exception resultado, string origen)
        {
            string archivo_log;
            try
            {
                archivo_log = dirError + "ERROR " + DateTime.Now.ToString("dd-MM-YYYY HH.mm.ss") + ".log";
                StreamWriter ArchivoSalida = new StreamWriter(archivo_log);
                ArchivoSalida.WriteLine("Site: " + (resultado.TargetSite.Name));
                ArchivoSalida.WriteLine("Number: " + (resultado.ToString()));
                ArchivoSalida.WriteLine("Descripcion: " + (resultado.Message));
                ArchivoSalida.WriteLine("Trace: " + (resultado.StackTrace));
                ArchivoSalida.WriteLine("Fuente: " + (resultado.Source));
                ArchivoSalida.WriteLine("Usuario: " + getUser());
                ArchivoSalida.WriteLine("Modulo: " + (origen));
                ArchivoSalida.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
