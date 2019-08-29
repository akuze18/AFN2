using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ServAFN = AFN_WF_C.ServiceProcess.PublicData;
using AFN_WF_C.PCClient.Procesos;

namespace AFN_WF_C.PCClient.Vistas.Busquedas
{
    //public enum tipo_estado { 
    //    soloActivos,
    //    soloDigitados,
    //    noBorrados,
    //    todos
    //}
    public partial class articulo : PCClient.FormBase
    {
        int[] _vigencias;
        string[] _estado;
        int _parte, _cod;
        public articulo()
        {
            InitializeComponent();
            _vigencias = new int[] {1};
            _estado = consultas.estados_aprobacion.NoDeleted;
            _cod = 0;
            _parte = -1;
        }

        //public void set_criterios(int[] vigencias = null, string[] estado = null)
        //{
        //    if (vigencias == null) { vigencias = new int[] {1}; }
        //    _vigencias = vigencias;
        //    //_estado = estado;
        //}

        public int codigo {
            get { return _cod; }
        }
        public int parte{
            get { return _parte; }
        }

        private void articulo_Load(object sender, EventArgs e)
        {
            
            //Fdesde
            Fdesde.ShowCheckBox = true;
            Fdesde.Value = LastDayPM;
            Fdesde.CustomFormat = "dd-MM-yyyy";
            Fdesde.Checked = false;
            Fdesde.MaxDate = LastDayTM;

            //Fhasta
            Fhasta.ShowCheckBox = true;
            Fhasta.Value = LastDayPM;
            Fhasta.CustomFormat = "dd-MM-yyyy";
            Fhasta.Checked = false;
            Fhasta.MaxDate = LastDayTM;
            
            //cboZona
            var tabla_zona = consultas.zonas.All().ToArray();
            cboZona.Items.AddRange(tabla_zona);
            cboZona.SelectedIndex = -1;
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            foreach (var o in MosResult.Items) { MosResult.RemoveObject(o); }
            Lresultado.Text = "Resultados : ";

            string bZona,Bdescrip;
            int Bcodigo;
            DateTime fecha_min,fecha_max;
            if( cboZona.SelectedIndex == -1){
                bZona = "00";
            }
            else{
                var sel = (ServAFN.GENERIC_VALUE)(cboZona.SelectedItem);
                bZona = sel.code;
            }
            if (!int.TryParse(Tcodigo.Text,out Bcodigo)){
                Bcodigo = 0;
            }
            Bdescrip = Tdescrip.Text;
            fecha_min = DateTime.MinValue;
            fecha_max = DateTime.MaxValue;
            if(Fdesde.Checked){fecha_min = Fdesde.Value;}
            if(Fhasta.Checked){fecha_max = Fhasta.Value;}

            var resultado = consultas.buscar_Articulo(fecha_min, fecha_max, Bcodigo, Bdescrip, bZona, _vigencias, _estado);
            MosResult.SetObjects(resultado);
            //Lresultado.Text = "Resultados : " + resultado.Length.ToString();
            Lresultado.Text = "Resultados : " + resultado.Count.ToString();

        }

        private void btn_marcar_Click(object sender, EventArgs e)
        {
            if (MosResult.SelectedItem != null)
            {
                var seleccionado = (ServAFN.DETAIL_PROCESS)MosResult.SelectedItem.RowObject;
                _cod = seleccionado.cod_articulo;
                _parte = seleccionado.parte;
                this.DialogResult = DialogResult.OK;
            }
            else {
                MessageBox.Show("No ha seleccionado ningún articulo", "NIPPON CHILE");
            }            
        }

        private void cboZona_KeyUp(object sender, KeyEventArgs e)
        {
            var clearKey = new List<Keys>(){ Keys.Space,Keys.Escape};
            if (clearKey.Contains(e.KeyCode)) {
                if(sender is ComboBox){
                    var combo = (ComboBox)sender;
                    combo.SelectedIndex = -1;
                }
            }
        }

       
    }
}
