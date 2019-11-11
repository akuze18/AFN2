using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Drawing;

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

    }
}
