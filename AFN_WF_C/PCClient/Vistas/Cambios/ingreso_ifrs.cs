using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using P = AFN_WF_C.PCClient.Procesos;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    public partial class ingreso_ifrs : UserControl
    {
        private ingreso _padre;
        private TabPage _page;

        public ingreso_ifrs()
        {
            InitializeComponent();
        }
        private void ingreso_ifrs_Load(object sender, EventArgs e)
        {
            _padre = P.Auxiliar.FindPadre(this);
            _page = P.Auxiliar.FindPage(this);
        }

        public void load_data()
        {
            //metodo valorizacion
            cboMetod.Items.Clear();
            cboMetod.Items.AddRange(P.Consultas.metodo_val.All());
            cboMetod.SelectedIndex = 0;
        }

        public void limpiar()
        {
            TvuI.Text = string.Empty;
            Tval_resI.Text = string.Empty;
            cboMetod.SelectedIndex = -1;
            xt.Text = string.Empty;
            DataIFRS.DataSource = null;
        }

        //habilitar_controles
        public void cargar(ingreso.cod_situacion situacion)
        {
            cargar(situacion,true);
        }
        public void cargar(ingreso.cod_situacion situacion, bool HayIFRS)
        {
            switch(situacion)
            {
                case ingreso.cod_situacion.nuevo:
                case ingreso.cod_situacion.activo:
                    _page.Enabled = false;
                    break;
                case ingreso.cod_situacion.editable:
                    //reviso si existe IFRS cargado
                    if(HayIFRS){
                        _page.Enabled = true;
                        //habilitar hoja IFRS
                        P.Auxiliar.ActivarF(TvuI);
                        P.Auxiliar.ActivarF(cboMetod);
                        P.Auxiliar.ActivarF(Tval_resI);

                        DataTable datos_ifrs, detalle_ifrs;
                        //datos_ifrs = base.CUADRO_INGRESO_IFRS(_padre.codigo_artic);
                        //DataIFRS.DataSource = datos_ifrs;
                        
                        foreach(DataGridViewColumn columna in DataIFRS.Columns){
                            columna.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        DataIFRS.Sort(DataIFRS.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                        DataIFRS.Columns[0].Visible = false;
                        DataIFRS.Columns[1].Width = 160;
                        DataIFRS.Columns[2].DefaultCellStyle.Format = "C0";
                        DataIFRS.Columns[2].Width = 130;
                        DataIFRS.Columns[3].DefaultCellStyle.Format = "C2";
                        DataIFRS.Columns[3].Width = 130;

                        //tipo cambio
                        //xt.Text = base.TipoCambio(Tfecha_compra.Value);
                        xt.Text = "1";

                        //detalle_ifrs = base.DETALLE_IFRS_CLP(artic.Text, 0);
                        //valor residual
                        //Tval_resI.Text = string.Format(detalle_ifrs.Rows[0]["valor_residual"].ToString(), "#,#0");
                        ////vida util
                        //TvuI.Text = (detalle_ifrs.Rows[0])["vida_util_base"].ToString();
                        ////'metodo valotizacion
                        //cboMetod.SelectedIndex = (int)(detalle_ifrs.Rows[0]["metod_val"]) - 1;

                        btn_IFRS.Image = global::AFN_WF_C.Properties.Resources._32_next;
                        btn_IFRS.Text = "Guardar";
                    }
                    else
                    {
                        _page.Enabled = false;
                    }
                    break;
            }
        }

        #region Botones Paso 2
        private void Tval_resI_GotFocus(Object sender, EventArgs e) //Handles Tval_resI.GotFocus
        {
            
            Tval_resI.Text = string.Format(Tval_resI.Text,"General Number");
            Tval_resI.SelectionStart = 0;
            Tval_resI.SelectionLength = (Tval_resI.Text.Length);
            
        }
        private void Tval_resI_LostFocus(Object sender, EventArgs e) //Handles Tval_resI.LostFocus
        {
            Tval_resI.Text = string.Format(Tval_resI.Text,"Standard");
            Tval_resI.Text = Tval_resI.Text.Substring(0, (Tval_resI.Text.Length) - 3);
        }
        private void btn_IFRS_Click(Object sender, EventArgs e) //Handles btn_IFRS.Click
        {
            //validar campos vacios
            if(TvuI.Text == ""){
                P.Mensaje.Advert("Debe indicar la vida útil en días");
                TvuI.Focus();
                return;
            }
            if(Tval_resI.Text == ""){
                P.Mensaje.Advert("Debe indicar el valor residual");
                Tval_resI.Focus();
                return;
            }
            if(cboMetod.SelectedIndex == -1){
                P.Mensaje.Advert("Debe indicar el tipo de valorización");
                cboMetod.Focus();
                return;
            }
            //reduccion de campos

            int metod_val,VUA, val_res;
            decimal prepa, trans, monta, desma, honor;
            prepa = 0;
            desma = 0;
            trans = 0;
            monta = 0;
            honor = 0;

            VUA = int.Parse(TvuI.Text);
            val_res = int.Parse(Tval_resI.Text);
            metod_val = (cboMetod.SelectedIndex + 1);
            foreach(DataGridViewRow fila in DataIFRS.Rows)
            {
                int valor = (int)fila.Cells[0].Value;
                decimal monto;
                decimal.TryParse(fila.Cells[2].Value.ToString(), out monto);
                //string.Format(fila.Cells[2].Value.ToString(), "General Number");
                switch((int)(fila.Cells[0].Value))
                {
                    case 1:
                        prepa = monto;
                        break;
                    case 2:
                        desma = monto;
                        break;
                    case 3:
                        trans = monto;
                        break;
                    case 4:
                        monta = monto;
                        break;
                    case 5:
                        honor = monto;
                        break;
                }
            }
            RespuestaAccion respuesta;
            //modifico el datos
            respuesta = P.Consultas.detalle_parametros.MODIFICA_IFRS(_padre.codigo_artic, val_res, VUA, metod_val, prepa, trans, monta, desma, honor);// base.MODIFICA_IFRS(artic.Text, val_res, VUA, metod_val, prepa, trans, monta, desma, honor);
            if( respuesta.codigo < 0)
            {
                //se produjo un error en el insert, se debe avisar
                P.Mensaje.Error(respuesta.descripcion);
            }
            else{
                P.Mensaje.Info("Registro se modificó correctamente al módulo Activo Fijo IFRS");
            }
            respuesta = null;
            //seleccionar_pestaña(paso3);
        }
        private void DataIFRS_CellDoubleClick(Object sender, DataGridViewCellEventArgs e)// Handles DataIFRS.CellDoubleClick
        {
            int fila;
            fila = e.RowIndex;
            if(fila > -1){
                string nombre_fila, opcion, valor_actual;
                nombre_fila = DataIFRS.Rows[fila].Cells[1].Value.ToString();
                valor_actual = string.Format(DataIFRS.Rows[fila].Cells[2].Value.ToString(), "General Number");
                //opcion = InputBox("Ingrese nuevo valor para " + nombre_fila, "NH FOODS CHILE", valor_actual);
                opcion = string.Empty;
                if(! String.IsNullOrEmpty(opcion)){
                    int intOpcion;
                    decimal tpCamb;
                    decimal.TryParse(xt.Text,out tpCamb);
                    if(int.TryParse(opcion,out intOpcion)){
                        DataIFRS.Rows[fila].Cells[2].Value = intOpcion;
                        DataIFRS.Rows[fila].Cells[3].Value = Math.Round(intOpcion / tpCamb, 0);
                    }else{
                        P.Mensaje.Advert("Solo puede ingresar números");
                    }
                }
            }
        }
        #endregion

    }
}
