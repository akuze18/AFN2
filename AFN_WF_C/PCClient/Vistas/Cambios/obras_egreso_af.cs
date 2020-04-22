using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using P = AFN_WF_C.PCClient.Procesos;
using AFN_WF_C.PCClient.Procesos.Estructuras;

using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    public partial class obras_egreso_af : AFN_WF_C.PCClient.FormBase
    {
        

        public obras_egreso_af()
        {
            InitializeComponent();
        }

        private void obras_egreso_af_Load(object sender, EventArgs e)
        {
            //agrego columnas al datagridview de las entradas con saldo

            BuildSaldosBinding();
            Tsaldos.RowHeadersWidth = 25;
            Tsaldos.Columns[0].Width = 50;  //Deja de estar oculta
            Tsaldos.Columns[0].HeaderText = "Codigo";
            Tsaldos.Columns[1].Width = 450;
            Tsaldos.Columns[1].HeaderText = "Descripción o Referencia";
            Tsaldos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            Tsaldos.Columns[2].Width = 75;
            Tsaldos.Columns[2].HeaderText = "Fecha";
            Tsaldos.Columns[3].Width = 50;
            Tsaldos.Columns[3].HeaderText = "Zona";
            Tsaldos.Columns[4].Width = 90;
            Tsaldos.Columns[4].HeaderText = "Saldo";
            Tsaldos.Columns[4].DefaultCellStyle.Format = "N0";
            Tsaldos.MultiSelect = false;
            Tsaldos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Tsaldos.AllowUserToAddRows = false;
            Tsaldos.AllowUserToDeleteRows = false;
            Tsaldos.AllowUserToResizeColumns = false;
            Tsaldos.AllowUserToOrderColumns = false;
            Tsaldos.EditMode = DataGridViewEditMode.EditProgrammatically;

            BuildSalidasBinding();
            salidaAF.RowHeadersWidth = 25;

            salidaAF.Columns[0].Width = 50;  //Deja de estar oculta
            salidaAF.Columns[0].HeaderText = "Codigo";
            salidaAF.Columns[1].Width = 450;
            salidaAF.Columns[1].HeaderText = "Descripción o Referencia";
            salidaAF.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
            salidaAF.Columns[2].Visible = false;    //Fecha no requerida
            salidaAF.Columns[3].Visible = false;    //Zona no requerida
            salidaAF.Columns[4].Width = 90;
            salidaAF.Columns[4].HeaderText = "Monto Utilizado";
            salidaAF.Columns[4].DefaultCellStyle.Format = "N0";

            salidaAF.MultiSelect = false;
            salidaAF.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            salidaAF.AllowUserToAddRows = false;
            salidaAF.AllowUserToDeleteRows = false;
            salidaAF.AllowUserToResizeColumns = false;
            salidaAF.AllowUserToOrderColumns = false;
            salidaAF.EditMode = DataGridViewEditMode.EditProgrammatically;
            

            Ecod.Enabled = false;
            Edesc.Enabled = false;
            EmontoMax.Enabled = false;

            LvalorAF.Enabled = false;

            cargar_saldos();
        }

        #region Datos grilla

        private void BuildSaldosBinding()
        {
            var vacio = new List<DetalleOBC>();
            BuildSaldosBinding(vacio);
        }
        private void BuildSaldosBinding(List<DetalleOBC> toWork)
        {
            BindingSource source = new BindingSource();
            source.DataSource = toWork;
            Tsaldos.DataSource = source;
            Tsaldos.Refresh();
        }
        private List<DetalleOBC> GetSaldosBinding()
        {
            var MyDataSource = (BindingSource)Tsaldos.DataSource;
            List<DetalleOBC> toWork = (List<DetalleOBC>)MyDataSource.DataSource;
            return toWork;
        }
        private void AddSaldosBinding(DetalleOBC item)
        {
            var bnd = GetSaldosBinding();
            bnd.Add(item);
            BuildSaldosBinding(bnd);
        }

        private void BuildSalidasBinding()
        {
            var vacio = new List<DetalleOBC>();
            BuildSalidasBinding(vacio);
        }
        private void BuildSalidasBinding(List<DetalleOBC> toWork)
        {
            BindingSource source = new BindingSource();
            source.DataSource = toWork;
            salidaAF.DataSource = source;
            salidaAF.Refresh();
        }
        private List<DetalleOBC> GetSalidasBinding()
        {
            var MyDataSource = (BindingSource)salidaAF.DataSource;
            List<DetalleOBC> toWork = (List<DetalleOBC>)MyDataSource.DataSource;
            return toWork;
        }
        private void AddSalidasBinding(DetalleOBC item)
        {
            var bnd = GetSalidasBinding();
            bnd.Add(item);
            BuildSalidasBinding(bnd);
        }

        #endregion

        private void cargar_saldos()
        {
            
            decimal ocupado;
            var entradas = P.Consultas.obc.saldos_entradas(Today, "CLP");

            var datos = new List<DetalleOBC>();

            foreach(DetalleOBC fila in entradas)
            {
                //agrego filas
                DetalleOBC newdato = new DetalleOBC();
                newdato.codigo = fila.codigo;
                newdato.descripcion = fila.descripcion;
                newdato.fecha = fila.fecha;
                newdato.zona = fila.zona;
                //busco el codigo dentro la salida para restar los saldos
                ocupado = 0;
                foreach (DetalleOBC fila_salida in GetSalidasBinding())
                {
                    if (fila_salida.codigo == fila.codigo)
                        ocupado = ocupado + fila_salida.saldo;
                }
                newdato.saldo = fila.saldo - ocupado;
                datos.Add(newdato);
            }

            BuildSaldosBinding(datos);
            //entradas = null;
            limpiar_saldo();
        }

        private void limpiar_saldo()
        {
            Tsaldos.ClearSelection();
            Ecod.Text = string.Empty;
            Edesc.Text = string.Empty;
            EmontoMax.Text = string.Empty;
            EmontoSel.Text = string.Empty;
        }

        private void Tsaldos_CellClick(Object sender, DataGridViewCellEventArgs e) //Handles Tsaldos.CellClick
        {
            if ( e.RowIndex == -1 ) {
                limpiar_saldo();
            }
            foreach (DataGridViewRow fila in Tsaldos.SelectedRows)
            {
                DetalleOBC data = (DetalleOBC) fila.DataBoundItem;
                decimal numero = data.saldo;

                Ecod.Text = data.codigo.ToString();
                Edesc.Text = data.descripcion;
                EmontoMax.Text = numero.ToString("#,##0");
                EmontoSel.Text = numero.ToString("#,##0");
                //MaskedTextBox1.Text = Format(Val(numero), MaskedTextBox1.Mask) //(numero).ToString("D" + largo)
                //MaskedTextBox1.Text = (numero).ToString("D" + largo)
            }
        }
        private void EmontoSel_GotFocus(Object sender, EventArgs e) //Handles EmontoSel.GotFocus
        {
            string procesar;
            procesar = EmontoSel.Text;
            procesar = procesar.Replace(",", "");
            EmontoSel.Text = int.Parse(procesar).ToString("#");
        }
        private void EmontoSel_LostFocus(Object sender, EventArgs e) //Handles EmontoSel.LostFocus
        {
            if ( EmontoSel.Text != String.Empty ) {
                string procesar, pmaximo;
                decimal Dprocesar, Dmaximo;
                procesar = EmontoSel.Text;
                pmaximo = EmontoMax.Text;
                procesar = procesar.Replace(",", "");
                pmaximo = pmaximo.Replace( ",", "");
                Dmaximo = decimal.Parse(pmaximo);
                if (! decimal.TryParse(procesar,out Dprocesar) ) {
                    P.Mensaje.Advert("Solo puede ingresar números en la cantidad");
                    EmontoSel.Focus();
                    EmontoSel.Text = string.Empty;
                }
                else{
                    if ( Dmaximo < Dprocesar ) {
                        P.Mensaje.Advert("No puede exceder el monto máximo disponible para la entrada");
                        EmontoSel.Text = EmontoMax.Text;
                    }
                    else{
                        EmontoSel.Text = Dprocesar.ToString("#,##0");
                    }
                }
            }
        }

        private void btn_adjuntar_Click(Object sender, EventArgs e) //Handles btn_adjuntar.Click
        {
            //reviso que haya valores para adjuntar
            bool ver1, ver2, ver3, ver4;
            int cod, montoMax, montoSel;
            cod = 0;  montoMax = 0; montoSel = 0;
            ver1 = (Ecod.Text != string.Empty && int.TryParse(Ecod.Text, out cod));
            ver2 = (Edesc.Text != string.Empty);
            ver3 = (EmontoMax.Text != string.Empty && int.TryParse(EmontoMax.Text.Replace(",", ""), out montoMax));
            if (EmontoSel.Text != String.Empty && int.TryParse(EmontoSel.Text.Replace(",", ""), out montoSel))
                ver4 = ( montoSel != 0 );
            else
                ver4 = false;

            if ( ver1 && ver2 && ver3 && ver4 ) {
                //ingreso valores en segundo flexgrid
                //h = Tsaldos.Row
                //i = salidaAF.Rows
                DetalleOBC newfila = new DetalleOBC();
                decimal ocupado;
                newfila.codigo = cod;
                newfila.descripcion = Edesc.Text;
                newfila.saldo = montoSel;
                AddSalidasBinding(newfila);
                
                EmontoMax.Text = (montoMax - montoSel).ToString("#,##0");
                EmontoSel.Text = "0";
                cargar_saldos();
                ocupado = 0;
                foreach (DetalleOBC fila in GetSalidasBinding())
                {
                    ocupado = ocupado + fila.saldo;
                }
                LvalorAF.Text = ocupado.ToString("#,##0");
                salidaAF.ClearSelection();
            }
            else{
                //hacer nada
            }
        }
        private void btn_quitar_Click(Object sender, EventArgs e) //Handles btn_quitar.Click
        {
            DataGridViewRow dgvr = null;
            foreach(DataGridViewRow fila in salidaAF.SelectedRows)
            {
                dgvr = fila;
            }
            if (dgvr != null ) {
                decimal ocupado;
                salidaAF.Rows.Remove(dgvr);
                cargar_saldos();
                //busco el codigo dentro la salida para restar los saldos
                ocupado = 0;
                foreach(DataGridViewRow fila in salidaAF.Rows)
                {
                    var objSalida = (DetalleOBC)fila.DataBoundItem;
                    ocupado = ocupado + objSalida.saldo;//int)(fila.Cells[2].Value);
                }
                LvalorAF.Text = ocupado.ToString("#,##0");
                salidaAF.ClearSelection();
            }
        }

        private void btn_guardar_Click(Object sender, EventArgs e ) //Handles btn_guardar.Click
        {
            int cantidad;
            //validamos campos
            if ( Tcantidad.Text == string.Empty ) {
                P.Mensaje.Advert("Ingrese un monto para cantidad de articulos");
                Tcantidad.Focus();
                return;
            }
            else{
                if ( ! int.TryParse(Tcantidad.Text, out cantidad) ) {
                    P.Mensaje.Advert("Valor ingresado no corresponde a un número");
                    Tcantidad.Focus();
                    return;
                }
            }
            if ( LvalorAF.Text == "0" || LvalorAF.Text == string.Empty ) {
                P.Mensaje.Advert("Seleccione entradas para formar el valor del Activo Fijo");
                Tsaldos.Focus();
                return;
            }
            //fin validacion
            int valorAF, valor_uni, diferencia ;
            valorAF = int.Parse(LvalorAF.Text.Replace(",",""));
            valor_uni = (int)(valorAF / cantidad);
            diferencia = valorAF - (valor_uni * cantidad);
            //form_ingreso.Show();
            //Me.Hide()
            var nextStep = new ingreso();
            nextStep.ShowFrom(this);
            nextStep.LoadOBC(valor_uni, cantidad, diferencia, GetSalidasBinding());
            //this.Close();
            
        }

        private void btFindEntrada_Click(Object sender, EventArgs e) //Handles btFindEntrada.Click
        {
            string find_entrada;
            find_entrada = P.Mensaje.InputBox("Ingrese el código que desea buscar");
            int cod_entrada;
            if( ! int.TryParse(find_entrada, out cod_entrada))
            {
                P.Mensaje.Error("El valor ingresado es valido");
                return;
            }
            bool encontrar = false;
            foreach(DataGridViewRow registro in Tsaldos.Rows)
            {
                if ( (int)(registro.Cells[0].Value) == cod_entrada ) {
                    encontrar = true;
                    Tsaldos.FirstDisplayedScrollingRowIndex = registro.Index;
                    registro.Selected = true;
                    Tsaldos_CellClick(sender, new DataGridViewCellEventArgs(0, registro.Index));
                }
            }
            if (! encontrar ) {
                P.Mensaje.Info("No se encontró ningun registro con el codigo indicado");
            }
        }

        public bool ReleaseOBCParent()
        {
            return this.ChangeOrigen(null);
        }
    }
}
