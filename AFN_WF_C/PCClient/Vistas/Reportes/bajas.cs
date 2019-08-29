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
    public partial class bajas : AFN_WF_C.PCClient.FormBase
    {
        public bajas()
        {
            InitializeComponent();
        }

        private void bajas_Load(object sender, EventArgs e)
        {
            cb_desde_y.Items.AddRange(P.consultas.arr.years);
            cb_desde_y.SelectedIndex = 0;
            cb_desde_y.Tag = label1.Text;

            cb_desde_m.Items.AddRange(P.consultas.arr.meses);
            cb_desde_m.SelectedIndex = Today.Month - 1;
            cb_desde_m.Tag = label1.Text;

            cb_hasta_y.Items.AddRange(P.consultas.arr.years);
            cb_hasta_y.SelectedIndex = 0;
            cb_hasta_y.Tag = label2.Text;

            cb_hasta_m.Items.AddRange(P.consultas.arr.meses);
            cb_hasta_m.SelectedIndex = Today.Month - 1;
            cb_hasta_m.Tag = label2.Text;

            cb_situacion.Items.AddRange(P.consultas.vigencias.SearchDownsList().ToArray());
            cb_situacion.SelectedIndex = 0;
            cb_situacion.Tag = label3.Text;

            cb_acum.Items.AddRange(P.consultas.arr.acumulados);
            cb_acum.SelectedIndex = 0;
            cb_acum.Tag = label5.Text;

            lb_reporte.Items.AddRange(P.consultas.sistema.All().ToArray());
            //listaReporte.Items.AddRange(reportes_especiales);
            lb_reporte.SelectedIndex = 0;
            lb_reporte.Tag = label4.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Valido formulario
            foreach (Control cnt in this.Controls)
            {
                if (cnt.GetType() == typeof(ComboBox))
                {
                    var combo = (ComboBox)cnt;
                    if (combo.SelectedIndex < 0)
                    {
                        MessageBox.Show("Debe seleccionar una opción para " + combo.Tag.ToString());
                        combo.Focus();
                        return;
                    }
                }
                if (cnt.GetType() == typeof(ListBox))
                {
                    var listbox = (ListBox)cnt;
                    if (listbox.SelectedIndex < 0)
                    {
                        MessageBox.Show("Debe seleccionar una opción para " + listbox.Tag.ToString());
                        listbox.Focus();
                        return;
                    }
                }
            }

            //Obtengo valores del formulario
            int año_desde = ((V.GENERIC_VALUE)cb_desde_y.SelectedItem).id;
            int mes_desde = ((V.GENERIC_VALUE)cb_desde_m.SelectedItem).id;
            int año_hasta = ((V.GENERIC_VALUE)cb_hasta_y.SelectedItem).id;
            int mes_hasta = ((V.GENERIC_VALUE)cb_hasta_m.SelectedItem).id;
            int acumulado = ((V.GENERIC_VALUE)cb_acum.SelectedItem).id;
            ACode.Vperiodo desde, hasta;
            desde = new ACode.Vperiodo(año_desde, mes_desde);
            hasta = new ACode.Vperiodo(año_hasta, mes_hasta, acumulado);
            int situacion = ((V.GENERIC_VALUE)cb_situacion.SelectedItem).id;
            if (lb_reporte.SelectedItem.GetType() == typeof(V.SV_SYSTEM))
            {
                var sistema = (V.SV_SYSTEM)lb_reporte.SelectedItem;
                P.Reportes.bajas_detalle(desde, hasta, situacion, sistema);
                MessageBox.Show("Reporte OK");
            }
            
        }
    }
}
