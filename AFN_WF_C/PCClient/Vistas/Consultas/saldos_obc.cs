using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Proc = AFN_WF_C.PCClient.Procesos;
using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Vistas.Consultas
{
    public partial class saldos_obc : AFN_WF_C.PCClient.FormBase
    {
        public saldos_obc()
        {
            InitializeComponent();
        }

        private void saldos_obc_Load(object sender, EventArgs e)
        {
            cb_resultado.Items.AddRange(Proc.consultas.arr.opciones_menu_obc);
            cb_resultado.SelectedIndex = 0;
            cb_resultado.Tag = label1.Text;

            cb_year.Items.AddRange(Proc.consultas.arr.years);
            cb_year.SelectedIndex = 0;
            cb_year.Tag = label2.Text;

            cb_month.Items.AddRange(Proc.consultas.arr.meses);
            cb_month.SelectedIndex = Today.Month - 1;
            cb_month.Tag = label2.Text;

            cb_moneda.Items.AddRange(Proc.consultas.monedas.All());
            cb_moneda.SelectedIndex = 0;
            cb_moneda.Tag = label3.Text;

            cb_acum.Items.AddRange(Proc.consultas.arr.acumulados_wMes);
            cb_acum.SelectedIndex = 0;
            cb_acum.Tag = label4.Text;
        }

        //Parametros
        private PD.GENERIC_VALUE resultado { get { return (PD.GENERIC_VALUE)(cb_resultado.SelectedItem); } }
        private PD.GENERIC_VALUE año { get { return (PD.GENERIC_VALUE)(cb_year.SelectedItem); } }
        private PD.GENERIC_VALUE mes { get { return (PD.GENERIC_VALUE)(cb_month.SelectedItem); } }
        private PD.GENERIC_VALUE moneda { get { return (PD.GENERIC_VALUE)(cb_moneda.SelectedItem); } }
        private PD.GENERIC_VALUE acumulado { get { return (PD.GENERIC_VALUE)(cb_acum.SelectedItem); } }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validar_formulario())
                return;
            Proc.Reportes.obc_detalle(resultado,año.id,mes.id,moneda,acumulado);
            Mensaje.Info("Reporte concluido");
        }
    }
}
