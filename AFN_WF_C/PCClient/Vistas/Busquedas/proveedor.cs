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
    public partial class proveedor : AFN_WF_C.PCClient.FormBase
    {
        public proveedor()
        {
            InitializeComponent();
        }

        private string _cod_result;
        private PD.SV_PROVEEDOR _full_data;

        public string codigo { get { return _cod_result; } }
        public PD.SV_PROVEEDOR proveedor { get { return _full_data; } }

        private void bus_prov_Load(object sender, EventArgs e )// Handles MyBase.Load
        {
            MosResult2.FullRowSelect = true;
            //MosResult2.HasCollapsibleGroups = false;
            MosResult2.ShowGroups = false;
            
            //MosResult2.AllowUserToResizeColumns = false;
            //MosResult2.AllowUserToResizeRows = false;
            MosResult2.Columns[0].Width = 100;
            MosResult2.Columns[1].Width = 310;
            MosResult2.Columns[2].Width = 150;
            MosResult2.Columns[3].Width = 0;

            //Proveedores ya no están indexados por zonas
            Label3.Visible = false; //label de zona
            cboZona.Enabled = false;
            cboZona.Visible = false;
            //zonas
            //sql_periodo = "SELECT COD_GL [cod],NOMBRE [es] FROM AFN_ZONA WHERE ACTIVA=1 ORDER BY COD_GL"
            //TB_zona = maestro.ejecuta(sql_periodo)
            //cboZona.DisplayMember = "es"
            //cboZona.ValueMember = "cod"
            //cboZona.DataSource = TB_zona
        }

        private void btn_buscar_Click(Object sender, EventArgs e)// Handles btn_buscar.Click
        {
            //todos los valores son opcionales, asi que no es necesario validar blancos
            P.Auxiliar.bloquearW(this);
            //if(cboZona.SelectedIndex == -1 || cboZona.Text == "")
            //    bZona = "";
            //else
            //    bZona = cboZona.SelectedValue.ToString();

            var resultado = buscar_Proveedor(Tcodigo.Text, Tdescrip.Text);
            int Tresult = resultado.Count;
            Lresultado.Text = "Resultados : " + Tresult.ToString();

            MosResult2.SetObjects(resultado);
            Application.DoEvents();
            P.Auxiliar.desbloquearW(this);
        }

        private void btn_marcar_Click(Object sender, EventArgs e) //Handles btn_marcar.Click, MosResult.DoubleClick
        {
            if (MosResult2.SelectedItem != null)
            {
                var seleccionado = (PD.SV_PROVEEDOR)MosResult2.SelectedItem.RowObject;
                _cod_result = seleccionado.COD;
                _full_data = seleccionado;
                this.DialogResult = DialogResult.OK;
            }
        }

        private List<PD.SV_PROVEEDOR> buscar_Proveedor(string codigo, string nombre)
        {
            string rut = onlynumRUT(codigo);
            var resultado = P.Consultas.buscar_proveedor(rut, nombre);
            return resultado;
        }
        
        private string onlynumRUT(string texto){
            int posi;
            string salida, revis, patron, Pat;
            bool pasa;
            salida = "";
            patron = "1234567890K";
            for(posi = 0; posi < texto.Length; posi ++){
                revis = texto.Substring(posi, 1);
                pasa = false;
                for( int i = 0; i< patron.Length;i++){
                    Pat = patron.Substring(i, 1);
                    if(revis == Pat){
                        //caracter esta ok
                        pasa = true;
                        i = patron.Length;
                    }
                    Application.DoEvents();
                }
                if(pasa)
                    salida = salida + revis;
            }
            return salida;
        }
    }
}
