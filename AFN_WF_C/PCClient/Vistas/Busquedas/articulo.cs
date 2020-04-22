using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PD = AFN_WF_C.ServiceProcess.PublicData;
using P = AFN_WF_C.PCClient.Procesos;

namespace AFN_WF_C.PCClient.Vistas.Busquedas
{
    public enum tipo_estado
    {
        soloActivos,
        soloDigitados,
        noBorrados,
        todos
    }
    public enum tipo_vigencia
    {
        vigentes,
        bajas,
        ventas,
        castigos,
        todos
    }

    public enum moment_data
    {
        today,
        first
    }
    public partial class articulo : PCClient.FormBase
    {
        int[] _vigencias_toFind;
        string[] _estados_toFind;
        moment_data _cuando_toFind;
        bool _check_post_toFind;
        int _parte_result, _cod_result;
        bool _activado_result;
        private PD.DETAIL_PROCESS _full_data;
        
        public articulo()
        {
            InitializeComponent();
            _vigencias_toFind = P.Consultas.vigencias.Actives;
            _estados_toFind = P.Consultas.estados_aprobacion.NoDeleted;
            _cuando_toFind = moment_data.today;
            _check_post_toFind = false;
            _cod_result = 0;
            _parte_result = -1;
            _activado_result = false;
            _full_data = null;
        }

        private void set_vigencias(tipo_vigencia vigencias)
        {
            switch (vigencias)
            {
                case tipo_vigencia.todos:
                    _vigencias_toFind = P.Consultas.vigencias.All;
                    _check_post_toFind = false;
                    break;
                case tipo_vigencia.bajas:
                    _vigencias_toFind = P.Consultas.vigencias.Downs;
                    _check_post_toFind = true;
                    break;
                case tipo_vigencia.ventas:
                    _vigencias_toFind = P.Consultas.vigencias.Sells;
                    _check_post_toFind = true;
                    break;
                case tipo_vigencia.castigos:
                    _vigencias_toFind = P.Consultas.vigencias.Disposals;
                    _check_post_toFind = true;
                    break;
                default:    //case tipo_vigencia.vigentes:
                    _vigencias_toFind = P.Consultas.vigencias.Actives;
                    _check_post_toFind = true;
                    break;
            }
        }
        private void set_estados(tipo_estado estado)
        {
            switch (estado)
            {
                case tipo_estado.soloActivos:
                    _estados_toFind = P.Consultas.estados_aprobacion.OnlyActive;
                    break;
                case tipo_estado.soloDigitados:
                    _estados_toFind = P.Consultas.estados_aprobacion.OnlyDigited;
                    break;
                case tipo_estado.noBorrados:
                    _estados_toFind = P.Consultas.estados_aprobacion.NoDeleted;
                    break;
                default:    //case tipo_estado.todos:
                    _estados_toFind = P.Consultas.estados_aprobacion.NoDeleted;
                    break;
            }
        }
        private void set_momento(moment_data cuando)
        {
            _cuando_toFind = cuando;
        }

        public void set_criterios(tipo_vigencia vigencias, tipo_estado estado)
        {
            set_vigencias(vigencias);
            set_estados(estado);
        }

        public int codigo { get { return _cod_result; }}
        public int parte { get { return _parte_result; }}
        public bool activado { get { return _activado_result; }}
        public PD.DETAIL_PROCESS full_data { get { return _full_data; }}

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
            var tabla_zona = P.Consultas.zonas.All().ToArray();
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
                var sel = (PD.GENERIC_VALUE)(cboZona.SelectedItem);
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

            var resultado = P.Consultas.buscar_Articulo(fecha_min, fecha_max, Bcodigo, Bdescrip, bZona, _vigencias_toFind, _estados_toFind, _cuando_toFind, _check_post_toFind);
            MosResult.SetObjects(resultado);
            //Lresultado.Text = "Resultados : " + resultado.Length.ToString();
            Lresultado.Text = "Resultados : " + resultado.Count.ToString();

        }

        private void btn_marcar_Click(object sender, EventArgs e)
        {
            if (MosResult.SelectedItem != null)
            {
                var seleccionado = (PD.DETAIL_PROCESS)MosResult.SelectedItem.RowObject;
                _cod_result = seleccionado.cod_articulo;
                _parte_result = seleccionado.parte;
                _activado_result = (seleccionado.aprobacion.code == "CLOSE");
                _full_data = seleccionado;
                this.DialogResult = DialogResult.OK;
            }
            else {
                P.Mensaje.Info("No ha seleccionado ningún articulo");
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
