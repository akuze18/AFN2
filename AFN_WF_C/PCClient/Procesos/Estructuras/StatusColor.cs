using System;
using System.Drawing;

namespace AFN_WF_C.PCClient.Procesos.Estructuras
{
    class StatusColor
    {
        public static Color AFNok
        {
            get { return Color.FromArgb(125, 255, 125); }
        }
        public static Color AFNfail
        {
            get { return Color.FromArgb(255, 50, 50); }
        }
        public static Color AFNprocess
        {
            get { return Color.FromArgb(255, 255, 125); }
        }
            
    }
}
