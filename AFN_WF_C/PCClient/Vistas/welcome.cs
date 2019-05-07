using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AFN_WF_C.PCClient.Vistas
{
    public partial class welcome : PCClient.FormBase
    {
        public welcome()
        {
            InitializeComponent();
        }
        #region Menu Consultas
        private void smConsulta01_Click(object sender, EventArgs e)
        {
            //var finder = new busqueda.articulo();
            ////finder.set_criterios(estado: busqueda.tipo_estado.soloActivos);
            //DialogResult result = finder.DialogFrom(this);
            //if (result == DialogResult.OK)
            //{
            //    //MessageBox.Show("eligio el articulo:" + finder.codigo.ToString() + " - parte:" + finder.parte.ToString());
            //    procesos.consultas.get_ficha_ingreso(finder.codigo, finder.parte);
            //}
            //finder = null;
        }
        #endregion

        private void smSistemaTest_Click(object sender, EventArgs e)
        {
            //Microsoft.VisualBasic.Interaction.InputBox("conexion actual","",Procesos.consultas.coneccion());
            //MessageBox.Show(Procesos.consultas.revisar().ToString());

            //var x = Procesos.consultas.Migracion();
            //MessageBox.Show(x.descripcion);
            DateTime a, b;
            a = DateTime.Now;
            Procesos.consultas.depreciar();
            b = DateTime.Now;
            MessageBox.Show((b - a).ToString());


        }

    }
}
