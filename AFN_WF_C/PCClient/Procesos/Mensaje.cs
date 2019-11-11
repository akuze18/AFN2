using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;

namespace AFN_WF_C.PCClient.Procesos
{
    public class Mensaje
    {
        static string titulo = "ACTIVO FIJO NH FOODS";
        public static void Info(string mensaje)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Error(string mensaje)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Advert(string mensaje)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult Confirmar(string mensaje)
        {
            return MessageBox.Show(mensaje, "CONFIRMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }
    }
}
