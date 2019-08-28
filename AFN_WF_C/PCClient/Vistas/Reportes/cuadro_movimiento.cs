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
    public partial class cuadro_movimiento : AFN_WF_C.PCClient.FormBase
    {
        public cuadro_movimiento()
        {
            InitializeComponent();
        }

        private void cuadro_movimiento_Load(object sender, EventArgs e)
        {
            cb_year.Items.AddRange(P.consultas.arr.years);
            cb_year.SelectedIndex = 0;
            cb_year.Tag = label1.Text;

            cb_month.Items.AddRange(P.consultas.arr.meses);
            cb_month.SelectedIndex = Today.Month - 1;
            cb_month.Tag = label1.Text;

            cb_tipo.Items.AddRange(P.consultas.tipos.All().ToArray());
            cb_tipo.SelectedIndex = 0;
            cb_tipo.Tag = label2.Text;

            cb_acum.Items.AddRange(P.consultas.arr.acumulados);
            cb_acum.SelectedIndex = 0;
            cb_acum.Tag = label3.Text;

            cb_sistema.Items.AddRange(P.consultas.sistema.All().ToArray());
            cb_sistema.SelectedIndex = 0;
            cb_sistema.Tag = label4.Text;
        }

        private int año { get { return ((C.GENERIC_VALUE)(cb_year.SelectedItem)).id; } }
        private int mes { get { return ((C.GENERIC_VALUE)(cb_month.SelectedItem)).id; } }
        private C.GENERIC_VALUE tipo { get { return ((C.GENERIC_VALUE)(cb_tipo.SelectedItem)); } }
        private C.GENERIC_VALUE acum { get { return ((C.GENERIC_VALUE)(cb_acum.SelectedItem)); } }
        private V.SV_SYSTEM reporte { get { return ((V.SV_SYSTEM)(cb_sistema.SelectedItem)); } }

        private void button_visualizar_Click(object sender, EventArgs e)
        {
            if (!validar_formulario())
                return;

            P.Reportes.cuadro_movimiento(año, mes, tipo, acum, reporte);
            MessageBox.Show("Reporte OK");
        }
    }
}
