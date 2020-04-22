using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using P = AFN_WF_C.PCClient.Procesos;
using AFN_WF_C.PCClient.Procesos.Estructuras;

using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Vistas.Busquedas
{
    public partial class obc_borrador : AFN_WF_C.PCClient.FormBase
    {
        //OK : Edit single
        public DetalleOBC ToEdit;
        //Yes: Pass ready to process
        public List<DetalleOBC> ToProcess;

        public obc_borrador(List<Procesos.Estructuras.DetalleOBC> borradores)
        {
            InitializeComponent();

            foreach (var borr in borradores)
            {
                objectListView1.SetObjects(borradores);
            }
        }

        private void btn_toEdit_Click(object sender, EventArgs e)
        {
            if (objectListView1.SelectedObjects.Count > 0)
            {
                if (objectListView1.SelectedObject != null)
                {
                    ToEdit = (DetalleOBC) objectListView1.SelectedObject;
                    this.DialogResult = DialogResult.OK;                  
                }
                else
                    P.Mensaje.Advert("Solo un registro se puede editar a la vez");
            }
            else
                P.Mensaje.Advert("No ha seleccionado un registro para editar");
        }

        private void btn_toProcess_Click(object sender, EventArgs e)
        {
            if (objectListView1.SelectedObjects.Count > 0)
            {
                if (objectListView1.SelectedObject != null)
                {
                    ToProcess = new List<DetalleOBC>
                            {(DetalleOBC)objectListView1.SelectedObject};
                    this.DialogResult = DialogResult.Yes;
                }
                else
                {
                    P.Mensaje.Info("Solo se procesa un registro");
                }
            }
            else
                P.Mensaje.Advert("No ha seleccionado un registro para procesar");
        }

        private void btn_procAll_Click(object sender, EventArgs e)
        {
            //if (objectListView1.Objects.Any() > 0)
            //{
                var result = new List<DetalleOBC>();

                foreach (var oSel in objectListView1.Objects)
                {
                    result.Add((DetalleOBC)oSel);
                }
                ToProcess = result;
                this.DialogResult = DialogResult.Yes;
            //}
            //else
            //    P.Mensaje.Advert("No ha seleccionado registros para procesar");
        }
    }
}
