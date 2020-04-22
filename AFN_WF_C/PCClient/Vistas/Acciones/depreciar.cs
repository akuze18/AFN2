using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using P = AFN_WF_C.PCClient.Procesos;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Vistas.Acciones
{
    public partial class depreciar : AFN_WF_C.PCClient.FormBase
    {
        public depreciar()
        {
            InitializeComponent();
        }

        private void depreciar_Load(object sender, EventArgs e)
        {
            var ListEnv = P.Consultas.ambientes.GetAll();
            int x = 30, y = 20;
            foreach (var envio in ListEnv)
            {
                var opcion = new CheckBox();
                opcion.Name = "opcEnv";
                opcion.Text = envio.name;
                opcion.Tag = envio;
                EnvChoose.Controls.Add(opcion);
                opcion.Location = new Point(x, y);
                x += (opcion.Width+(y*2));
            }
            var ListZone = P.Consultas.zonas.All();
            
            objListZones.View = View.List;
            BrightIdeasSoftware.OLVColumn col1 = new BrightIdeasSoftware.OLVColumn() { AspectName = "description", Text = "Zonas Disponibles" };
            objListZones.Columns.Add(col1);
            objListZones.SetObjects(ListZone);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(20000);
            MessageBox.Show("Proceso finalizado");
        }

    }
}
