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
            DataIFRS.DataSource = null;
        }

        //habilitar_controles
        public void cargar(ingreso.cod_situacion situacion)
        {
            cargar(situacion,false, null);
        }
        public void cargar(ingreso.cod_situacion situacion, bool HayIFRS, SINGLE_DETAIL detalle)
        {
            //_page.Enabled = true;
            bool AllowModif;

            switch(situacion)
            {
                case ingreso.cod_situacion.nuevo:
                    _page.Enabled = true;
                    AllowModif = false;
                    break;
                case ingreso.cod_situacion.activo:
                    _page.Enabled = HayIFRS;
                    AllowModif = false;
                    break;
                case ingreso.cod_situacion.editable:
                    //reviso si existe IFRS cargado
                    _page.Enabled = HayIFRS;
                    AllowModif = HayIFRS;
                    break;
                default: AllowModif = false;
                    break;
            }

            //habilitar hoja IFRS
            P.Auxiliar.ActivarF(TvuI, AllowModif);
            P.Auxiliar.ActivarF(cboMetod, AllowModif);
            P.Auxiliar.ActivarF(Tval_resI, AllowModif);
            P.Auxiliar.ActivarF(DataIFRS, AllowModif);
            P.Auxiliar.ActivarF(btn_IFRS, AllowModif);

            if (AllowModif)
            {
                List<T_CUADRO_IFRS> datos_ifrs = P.Consultas.detalle_parametros.CUADRO_INGRESO_IFRS(_padre.codigo_artic);
                DataIFRS.DataSource = datos_ifrs;

                foreach (DataGridViewColumn columna in DataIFRS.Columns)
                {
                    columna.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                //DataIFRS.Sort(DataIFRS.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                DataIFRS.Columns[0].Visible = false;
                DataIFRS.Columns[1].Width = 160;
                DataIFRS.Columns[2].DefaultCellStyle.Format = "C0";
                DataIFRS.Columns[2].Width = 130;
                DataIFRS.Columns[3].DefaultCellStyle.Format = "C2";
                DataIFRS.Columns[3].Width = 130;

                //valor residual
                Tval_resI.Text = detalle.valor_residual.ToString("#,#0");
                //vida util
                TvuI.Text = detalle.vida_util_base.ToString();
                //metodo valotizacion
                cboMetod.SelectedItem = detalle.metod_val;
            }
            

            

            btn_IFRS.Image = global::AFN_WF_C.Properties.Resources._32_next;
            btn_IFRS.Text = "Guardar";
        }

        #region Botones Paso 2
        
        private void btn_IFRS_Click(Object sender, EventArgs e) //Handles btn_IFRS.Click
        {
            if (!validar_campos())
                return;
            //reduccion de campos

            int metod_val,VUA, val_res;
            decimal[] prepa, trans, monta, desma, honor;
            prepa = new decimal[] { 0, 0 };
            desma = new decimal[] { 0, 0 };
            trans = new decimal[] { 0, 0 };
            monta = new decimal[] { 0, 0 };
            honor = new decimal[] { 0, 0 };

            VUA = int.Parse(TvuI.Text);
            val_res = int.Parse(Tval_resI.Text);
            metod_val =((GENERIC_VALUE) cboMetod.SelectedItem).id;
            foreach(DataGridViewRow fila in DataIFRS.Rows)
            {
                int valor = (int)fila.Cells[0].Value;
                decimal monto_clp, monto_yen;
                decimal.TryParse(fila.Cells[2].Value.ToString(), out monto_clp);
                decimal.TryParse(fila.Cells[3].Value.ToString(), out monto_yen);
                switch((int)(fila.Cells[0].Value))
                {
                    case 1:
                        prepa[0] = monto_clp;
                        prepa[1] = monto_yen;
                        break;
                    case 2:
                        desma[0] = monto_clp;
                        desma[1] = monto_yen;
                        break;
                    case 3:
                        trans[0] = monto_clp;
                        trans[1] = monto_yen;
                        break;
                    case 4:
                        monta[0] = monto_clp;
                        monta[1] = monto_yen;
                        break;
                    case 5:
                        honor[0] = monto_clp;
                        honor[1] = monto_yen;
                        break;
                }
            }
            RespuestaAccion respuesta;
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
            _padre.cargar_otras_pestañas_fromIFRS();
        }
        private void DataIFRS_CellDoubleClick(Object sender, DataGridViewCellEventArgs e)// Handles DataIFRS.CellDoubleClick
        {
            int fila;
            fila = e.RowIndex;
            if(fila > -1){
                string nombre_fila, opcion, valor_actual;
                nombre_fila = DataIFRS.Rows[fila].Cells[1].Value.ToString();
                valor_actual = string.Format(DataIFRS.Rows[fila].Cells[2].Value.ToString(), "General Number");
                opcion = P.Mensaje.InputBox("Ingrese nuevo valor para " + nombre_fila, valor_actual);
                if(! String.IsNullOrEmpty(opcion)){
                    decimal decOpcion;
                    if (decimal.TryParse(opcion, out decOpcion))
                    {
                        DataIFRS.Rows[fila].Cells[2].Value = decOpcion;
                        DataIFRS.Rows[fila].Cells[3].Value = Math.Round(decOpcion / _padre.TCambio, 0);
                    }else{
                        P.Mensaje.Advert("Solo puede ingresar números");
                    }
                }
            }
        }
        #endregion

        private bool validar_campos()
        {
            if (TvuI.Text == "")
            {
                P.Mensaje.Advert("Debe indicar la vida útil en días");
                TvuI.Focus();
                return false;
            }
            if (Tval_resI.Text == "")
            {
                P.Mensaje.Advert("Debe indicar el valor residual");
                Tval_resI.Focus();
                return false;
            }
            if (cboMetod.SelectedIndex == -1)
            {
                P.Mensaje.Advert("Debe indicar el tipo de valorización");
                cboMetod.Focus();
                return false;
            }
            return true;
        }
    }
}
