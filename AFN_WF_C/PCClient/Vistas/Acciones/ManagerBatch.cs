using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using P = AFN_WF_C.PCClient.Procesos;
using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Vistas.Acciones
{
    public partial class ManagerBatch : AFN_WF_C.PCClient.FormBase
    {
        public ManagerBatch()
        {
            InitializeComponent();
        }

        private void ManagerBatch_Load(object sender, EventArgs e)
        {
            this.MinimumSize = this.Size;
            LoadLotes();
        }

        private void LoadLotes()
        {
            var lotes = P.Consultas.lotes.GetLotesAbiertos();
            DGLotes.SetObjects(lotes);
        }
        private void btnActiveSel_Click(object sender, EventArgs e)
        {
            if (DGLotes.SelectedObject != null)
            {
                var lote = (PD.SV_BATCH_ARTICLE)DGLotes.SelectedObject;
                var res = P.Consultas.lotes.ACTIVAR_AF(lote.id);
                if (res.codigo < 0)
                {
                    P.Mensaje.Error(res.descripcion);
                    return;
                }
                P.Mensaje.Info("Lote codigo " + lote.id.ToString() + " se ha activado");
                LoadLotes();
            }
            else
                P.Mensaje.Advert("No se ha seleccionado ningun registo para activar");
        }

        private void btnActiveAll_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (var Objlote in DGLotes.Objects)
            {
                var lote = (PD.SV_BATCH_ARTICLE)Objlote;
                var res = P.Consultas.lotes.ACTIVAR_AF(lote.id);
                if (res.codigo < 0)
                {
                    P.Mensaje.Error(res.descripcion);
                    return;
                }
                count++;
            }
            if (count > 0)
            {
                LoadLotes();
                P.Mensaje.Info("Todos los lotes (" + count.ToString() + ") han sido activados");
            }
            else
                P.Mensaje.Advert("Hay lotes para activar");
        }

        private void btnDeleteOne_Click(object sender, EventArgs e)
        {
            if (DGLotes.SelectedObject != null)
            {
                var lote = (PD.SV_BATCH_ARTICLE)DGLotes.SelectedObject;
                var res = P.Consultas.lotes.BORRAR_AF(lote.id);
                if (res.codigo < 0)
                {
                    P.Mensaje.Error(res.descripcion);
                    return;
                }
                P.Mensaje.Info("Lote codigo " + lote.id.ToString() + " se ha eliminado");
                LoadLotes();
            }
            else
                P.Mensaje.Advert("No se ha seleccionado ningun registo para eliminar");
        }

   
    }
}
