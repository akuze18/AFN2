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
    public partial class fixed_assets : AFN_WF_C.PCClient.FormBase
    {
        public fixed_assets()
        {
            InitializeComponent();
        }

        private void fixed_assets_Load(object sender, EventArgs e)
        {
            cb_year.Items.AddRange(P.Consultas.arr.years);
            cb_year.SelectedIndex = 0;
            cb_year.Tag = label1.Text;

            cb_month.Items.AddRange(P.Consultas.arr.meses);
            cb_month.SelectedIndex = Today.Month - 1;
            cb_month.Tag = label1.Text;

            cb_tipo.Items.AddRange(P.Consultas.tipos.All().ToArray());
            cb_tipo.SelectedIndex = 0;
            cb_tipo.Tag = label2.Text;

            cb_acum.Items.AddRange(P.Consultas.arr.acumulados);
            cb_acum.SelectedIndex = 0;
            cb_acum.Tag = label3.Text;

            cb_sistema.Items.AddRange(P.Consultas.sistema.All().ToArray());
            cb_sistema.SelectedIndex = 0;
            cb_sistema.Tag = label4.Text;
        }

        private int año { get { return ((V.GENERIC_VALUE)(cb_year.SelectedItem)).id; } }
        private int mes { get { return ((V.GENERIC_VALUE)(cb_month.SelectedItem)).id; } }
        private V.GENERIC_VALUE tipo { get { return ((V.GENERIC_VALUE)(cb_tipo.SelectedItem)); } }
        private V.GENERIC_VALUE acum { get { return ((V.GENERIC_VALUE)(cb_acum.SelectedItem)); } }
        private V.SV_SYSTEM reporte { get { return ((V.SV_SYSTEM)(cb_sistema.SelectedItem)); } }

        private void btn_generar_Click(object sender, EventArgs e)
        {
            if (!validar_formulario())
                return;

            P.Reportes.fixed_assets(año, mes, tipo, acum.id, reporte);
            MessageBox.Show("Reporte OK");
        }
    }
}
