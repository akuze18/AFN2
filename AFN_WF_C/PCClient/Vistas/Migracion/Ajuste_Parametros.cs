using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AFN_WF_C.PCClient.Procesos;
using V = AFN_WF_C.ServiceProcess.PublicData;

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
                        CbParte.Items.AddRange(consultas.partes.ByLote(codigo).ToArray());
                        
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
                    var selected = (V.SV_PART)cb.SelectedItem;
                    CbTHead.Items.AddRange(consultas.cabeceras.ByParte(selected.id).ToArray());

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
                    var SelectedHead = (V.SV_TRANSACTION_HEADER)(CbTHead.SelectedItem);
                    var SelectedSystem = (V.SV_SYSTEM)(CbSistema.SelectedItem);
                    var param = consultas.detalle_parametros.ByHead_Sys(SelectedHead.id, SelectedSystem.id).ToArray();
                    LParametros.SetObjects(param);

                }
            }
        }

        private void Ajuste_Parametros_Load(object sender, EventArgs e)
        {
            CbSistema.Items.AddRange(consultas.sistema.All().ToArray());
        }




    }
}
