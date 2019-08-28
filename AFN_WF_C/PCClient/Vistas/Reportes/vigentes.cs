using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using P = AFN_WF_C.PCClient.Procesos;
using C = AFN_WF_C.ServiceProcess.DataContract;
using V = AFN_WF_C.ServiceProcess.DataView;

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
            cb_year.Items.AddRange(P.consultas.arr.years);
            cb_year.SelectedIndex = 0;
            cb_year.Tag = label1.Text;

            cb_month.Items.AddRange(P.consultas.arr.meses);
            cb_month.SelectedIndex = Today.Month-1;
            cb_month.Tag = label1.Text;

            cb_zona.Items.AddRange(P.consultas.zonas.SearchList().ToArray());
            cb_zona.SelectedIndex = 0;
            cb_zona.Tag = label4.Text;

            cb_clase.Items.AddRange(P.consultas.clases.SearchList().ToArray());
            cb_clase.SelectedIndex = 0;
            cb_clase.Tag = label3.Text;

            cb_acum.Items.AddRange(P.consultas.arr.acumulados);
            cb_acum.SelectedIndex = 0;
            cb_acum.Tag = label2.Text;

            listaReporte.Items.AddRange(P.consultas.sistema.All().ToArray());
            //listaReporte.Items.AddRange(reportes_especiales);
            listaReporte.SelectedIndex = 0;
            listaReporte.Tag = label5.Text;
        }

        private int año { get { return ((C.GENERIC_VALUE)cb_year.SelectedItem).id; } }
        private int mes { get { return ((C.GENERIC_VALUE)cb_month.SelectedItem).id; } }
        private C.GENERIC_VALUE clase { get { return (C.GENERIC_VALUE)cb_clase.SelectedItem; } }
        private C.GENERIC_VALUE zona { get { return (C.GENERIC_VALUE)cb_zona.SelectedItem; } }
        private C.GENERIC_VALUE acum { get { return (C.GENERIC_VALUE)cb_acum.SelectedItem; } }
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
                Mensaje.Info("Reporte OK : " + (lapsed).ToString());
            }

        }

        private void button_resumen_c_Click(object sender, EventArgs e)
        {
            if (!validar_formulario())
                return;
            if (check_reporte())
            {
                P.Reportes.vigentes_resumen(año, mes, clase, zona, acum, reporte);
                Mensaje.Info("Reporte OK");
            }
        }
    }
}
