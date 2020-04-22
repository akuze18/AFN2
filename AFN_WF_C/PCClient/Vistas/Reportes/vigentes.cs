using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using P = AFN_WF_C.PCClient.Procesos;
using V = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Vistas.Reportes
{
    public partial class vigentes : AFN_WF_C.PCClient.FormBase
    {
        public vigentes()
        {
            InitializeComponent();
        }

        private void vigentes_Load(object sender, EventArgs e)
        {
            cb_year.Items.AddRange(P.Consultas.arr.years);
            cb_year.SelectedIndex = 0;
            cb_year.Tag = label1.Text;

            cb_month.Items.AddRange(P.Consultas.arr.meses);
            cb_month.SelectedIndex = Today.Month-1;
            cb_month.Tag = label1.Text;

            cb_zona.Items.AddRange(P.Consultas.zonas.SearchList().ToArray());
            cb_zona.SelectedIndex = 0;
            cb_zona.Tag = label4.Text;

            cb_clase.Items.AddRange(P.Consultas.clases.SearchList().ToArray());
            cb_clase.SelectedIndex = 0;
            cb_clase.Tag = label3.Text;

            cb_acum.Items.AddRange(P.Consultas.arr.acumulados);
            cb_acum.SelectedIndex = 0;
            cb_acum.Tag = label2.Text;

            listaReporte.Items.AddRange(P.Consultas.sistema.All().ToArray());
            //listaReporte.Items.AddRange(reportes_especiales);
            listaReporte.SelectedIndex = 0;
            listaReporte.Tag = label5.Text;
        }

        private int año { get { return ((V.GENERIC_VALUE)cb_year.SelectedItem).id; } }
        private int mes { get { return ((V.GENERIC_VALUE)cb_month.SelectedItem).id; } }
        private V.GENERIC_VALUE clase { get { return (V.GENERIC_VALUE)cb_clase.SelectedItem; } }
        private V.GENERIC_VALUE zona { get { return (V.GENERIC_VALUE)cb_zona.SelectedItem; } }
        private V.GENERIC_VALUE acum { get { return (V.GENERIC_VALUE)cb_acum.SelectedItem; } }
        private V.SV_SYSTEM reporte
        {
            get
            {
                if (check_reporte())
                    return (V.SV_SYSTEM)listaReporte.SelectedItem;
                else
                    return null;
            }
        }

        private bool check_reporte()
        {
            return listaReporte.SelectedItem.GetType() == typeof(V.SV_SYSTEM);
        }

        private void button_detalle_Click(object sender, EventArgs e)
        {
            if (!validar_formulario())
                return;
            if (check_reporte())
            {
                var ini = DateTime.Now;
                P.Reportes.vigentes_detalle(año, mes, clase, zona, acum, reporte);
                var fin = DateTime.Now;
                var lapsed = fin - ini;
                //P.Mensaje.Info("Reporte OK : " + (lapsed).ToString());
                P.Mensaje.Info("Reporte Detalle Vigente Generado");
            }
        }

        private void button_resumen_c_Click(object sender, EventArgs e)
        {
            if (!validar_formulario())
                return;
            if (check_reporte())
            {
                P.Reportes.vigentes_resumen(año, mes, clase, zona, acum, reporte, "C");
                P.Mensaje.Info("Reporte Resumen por Clase Generado");
            }
        }

        private void button_resumen_z_Click(object sender, EventArgs e)
        {
            if (!validar_formulario())
                return;
            if (check_reporte())
            {
                P.Reportes.vigentes_resumen(año, mes, clase, zona, acum, reporte, "Z");
                P.Mensaje.Info("Reporte Resumen por Clase Generado");
            }
        }

        private void button_det_inv_Click(object sender, EventArgs e)
        {
            if (!validar_formulario())
                return;
            if (check_reporte())
            {
                var ini = DateTime.Now;
                P.Reportes.vigentes_detalle(año, mes, clase, zona, acum, reporte);
                var fin = DateTime.Now;
                var lapsed = fin - ini;
                //P.Mensaje.Info("Reporte OK : " + (lapsed).ToString());
                P.Mensaje.Info("Reporte Detalle Vigente Generado");
            }
        }

      
    }
}
