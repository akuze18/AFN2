using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PD = AFN_WF_C.ServiceProcess.PublicData;
using P = AFN_WF_C.PCClient.Procesos;
using BrightIdeasSoftware;

namespace AFN_WF_C.PCClient.Vistas.Sistema
{
    public partial class Depreciar : AFN_WF_C.PCClient.FormBase
    {
        public Depreciar()
        {
            InitializeComponent();
            Generator.GenerateColumns(this.objectListView1, typeof(PD.DETAIL_DEPRECIATE), true);
            foreach (BrightIdeasSoftware.OLVColumn a in this.objectListView1.Columns) {
                a.Width = 100;
            }
            this.objectListView1.ShowFilterMenuOnRightClick = true;

            this.inYear.Minimum = 2012;
            this.inYear.Maximum = Today.Year;
            this.inYear.Value = Today.Year;

            this.inMonth.Items.AddRange(P.Consultas.arr.meses);
            this.inMonth.SelectedIndex = Today.Month - 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selMonth = (PD.GENERIC_VALUE)(inMonth.SelectedItem);
            var res = P.Consultas.depreciar((int)inYear.Value, selMonth.id);
            objectListView1.SetObjects(res);
            MessageBox.Show("Proceso Terminado");
        }
    }
}
