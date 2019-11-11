using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using P = AFN_WF_C.PCClient.Procesos;
using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Vistas.Migracion
{
    public partial class Ajuste_Parametros : AFN_WF_C.PCClient.FormBase
    {
        public Ajuste_Parametros()
        {
            InitializeComponent();
        }

        private void TbCodigoLote_Leave(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(TextBox)) {
                var tb = (TextBox)sender;
                if (!string.IsNullOrEmpty(tb.Text)) {
                    int codigo;
                    if (int.TryParse(tb.Text, out codigo))
                    {
                        //MessageBox.Show("Tu numero " +codigo.ToString());
                        CbParte.Items.Clear();
                        CbParte.Items.AddRange(P.Consultas.partes.ByLote(codigo).ToArray());
                        
                    }
                    else {
                        MessageBox.Show("El valor ingresado no es valido");
                        tb.Text = string.Empty;
                    }
                }
                
            }
        }

        private void CbParte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox))
            {
                var cb = (ComboBox)sender;
                if (cb.SelectedIndex < 0)
                {
                    CbTHead.Items.Clear();
                    LParametros.RemoveObjects(LParametros.SelectedObjects);
                }
                else
                {
                    CbTHead.Items.Clear();
                    var selected = (PD.SV_PART)cb.SelectedItem;
                    CbTHead.Items.AddRange(P.Consultas.cabeceras.ByParte(selected.id).ToArray());

                }
            }
        }

        private void CbTHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox))
            {
                //var cb = (ComboBox)sender;
                if (CbTHead.SelectedIndex < 0 || CbSistema.SelectedIndex < 0)
                {
                    LParametros.RemoveObjects(LParametros.SelectedObjects);
                }
                else
                {
                    LParametros.RemoveObjects(LParametros.SelectedObjects);
                    var SelectedHead = (PD.SV_TRANSACTION_HEADER)(CbTHead.SelectedItem);
                    var SelectedSystem = (PD.SV_SYSTEM)(CbSistema.SelectedItem);
                    var param = P.Consultas.detalle_parametros.ByHead_Sys(SelectedHead.id, SelectedSystem.id).ToArray();
                    LParametros.SetObjects(param);

                }
            }
        }

        private void Ajuste_Parametros_Load(object sender, EventArgs e)
        {
            CbSistema.Items.AddRange(P.Consultas.sistema.All().ToArray());
        }




    }
}
