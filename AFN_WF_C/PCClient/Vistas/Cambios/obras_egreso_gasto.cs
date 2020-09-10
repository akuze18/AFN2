using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using P = AFN_WF_C.PCClient.Procesos;
using AFN_WF_C.PCClient.Procesos.Estructuras;

using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    public partial class obras_egreso_gasto : AFN_WF_C.PCClient.FormBase
    {
        private PD.GENERIC_VALUE Ezona;
        //private int aprovalState;
        private int? idSalida;
        private string sMil;

        public obras_egreso_gasto()
        {
            InitializeComponent();
        }

        private void obras_egreso_gasto_Load(object sender, EventArgs e)
        {
            sMil = P.Auxiliar.getSeparadorMil;
            BuildSaldosBinding();
            Tsaldos.RowHeadersWidth = 25;
            Tsaldos.Columns[0].Width = 70; //Deja de estar oculta
            Tsaldos.Columns[0].HeaderText = "Codigo";
            Tsaldos.Columns[1].Width = 300;
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

            salidaAF.Columns[0].Width = 70; //Deja de estar oculta
            salidaAF.Columns[0].HeaderText = "Codigo";
            salidaAF.Columns[1].Width = 300;
            salidaAF.Columns[1].HeaderText = "Descripción o Referencia";
            salidaAF.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft;
            salidaAF.Columns[2].Width = 75;
            salidaAF.Columns[2].HeaderText = "Fecha";
            salidaAF.Columns[3].Width = 50;
            salidaAF.Columns[3].HeaderText = "Zona";
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
            Ezona = null;
            Edesc.Enabled = false;
            EmontoMax.Enabled = false;

            Image bmp = btn_edit.Image;
            bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
            btn_edit.Image = bmp;

            Efecha.Value = Today;
            Efecha.MaxDate = Today;

            cargar_saldos();
        }

        private void cargar_saldos()
        {
            decimal ocupado;
            var salidas_borrador =GetSalidasBinding().Where(S => S.idGet() != null);

            var entradas = P.Consultas.obc.saldos_entradas(Today, "CLP", salidas_borrador);
            //colchon = base.saldos_obc(Today, "CLP")
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
            Ezona = null;
            //aprovalState = 2;
            idSalida = null;
            EmontoMax.Text = string.Empty;
            EmontoSel.Text = string.Empty;
        }

        private void Tsaldos_CellClick(Object sender, DataGridViewCellEventArgs e)// Handles Tsaldos.CellClick
        {
            if (  e.RowIndex == -1 ) {
                limpiar_saldo();
            }
            foreach ( DataGridViewRow fila in Tsaldos.SelectedRows)
            {
                DetalleOBC data = (DetalleOBC) fila.DataBoundItem;
                Ecod.Text = data.codigo.ToString();
                Edesc.Text = data.descripcion;
                EmontoMax.Text = data.saldo.ToString("#,##0");
                EmontoSel.Text = data.saldo.ToString("#,##0");
                Ezona = data.zona;
                //aprovalState = data.aprovalGet();
                idSalida = data.idGet();
            }
        }
        private void salidaAF_CellClick(Object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                salidaAF.ClearSelection();
            }
        }

        private void EmontoSel_GotFocus(Object sender, EventArgs e) //Handles EmontoSel.GotFocus
        {
            string procesar;
            procesar = EmontoSel.Text;
            procesar = procesar.Replace(sMil, "");
            EmontoSel.Text = int.Parse(procesar).ToString("#");
        }
        private void EmontoSel_LostFocus(Object sender , EventArgs e) //Handles EmontoSel.LostFocus
        {
            if ( EmontoSel.Text != String.Empty ) {
                string procesar, pmaximo;
                decimal Dprocesar, Dmaximo;
                procesar = EmontoSel.Text;
                pmaximo = EmontoMax.Text;
                procesar = procesar.Replace(sMil, "");
                pmaximo = pmaximo.Replace(sMil, "");
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
            ver3 = (EmontoMax.Text != string.Empty && int.TryParse(EmontoMax.Text.Replace(sMil, ""), out montoMax));
            if (EmontoSel.Text != String.Empty && int.TryParse(EmontoSel.Text.Replace(sMil, ""), out montoSel))
                ver4 = ( montoSel != 0 );
            else
                ver4 = false;

            if ( ver1 && ver2 && ver3 && ver4 ) {
                DetalleOBC newfila = new DetalleOBC();
                newfila.codigo = cod;
                newfila.descripcion = Edesc.Text;
                newfila.saldo = montoSel;
                newfila.zona = Ezona;
                newfila.fecha = Efecha.Value;
                //newfila.aprovalSet(aprovalState);
                newfila.idSet(idSalida);
                newfila.maximoSet(montoMax);
                AddSalidasBinding(newfila);
                
                EmontoMax.Text = (montoMax - montoSel).ToString("#,##0");
                EmontoSel.Text = "0";
                cargar_saldos();
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
                dgvr = fila;

            if (dgvr != null ) {
                //int ocupado;
                salidaAF.Rows.Remove(dgvr);
                cargar_saldos();
                salidaAF.ClearSelection();
            }
        }

        private void btn_guardar_Click(Object sender, EventArgs e) //Handles btn_guardar.Click
        {
            //validamos campos
            if (  salidaAF.Rows.Count == 0 ) {
                P.Mensaje.Advert("Debe indicar los valores de salida a gastos");
                Tsaldos.Focus();
                return;
            }
            //fin validacion
            PD.RespuestaAccion mRS;
            foreach(DetalleOBC  fila in GetSalidasBinding())
            {
                mRS = P.Consultas.obc.EGRESO_GASTO(fila.idGet(), fila.codigo, fila.fecha, fila.saldo, 2);
                if (mRS.codigo < 0) { P.Mensaje.Error(mRS.descripcion); return; }
            }
            BuildSalidasBinding();
            cargar_saldos();
            P.Mensaje.Info("Se han guardado exitosamente las salidas a gastos");
        }
   
        private void btn_in_temp_Click(Object sender, EventArgs e) //Handles btn_in_temp.Click
        {
            //validamos campos
            if (salidaAF.Rows.Count == 0)
            {
                P.Mensaje.Advert("Debe indicar los valores de salida a gastos");
                Tsaldos.Focus();
                return;
            }
            //fin validacion
            DialogResult respuesta;
            respuesta = P.Mensaje.Confirmar("Desea adjuntar los cambios como borrador?");
            if (respuesta == DialogResult.Yes)
            {
                PD.RespuestaAccion mRS;
                foreach (DetalleOBC fila in GetSalidasBinding())
                {
                    mRS = P.Consultas.obc.EGRESO_GASTO(fila.idGet(), fila.codigo, fila.fecha, fila.saldo, 1);
                    if (mRS.codigo < 0) { P.Mensaje.Error(mRS.descripcion); return; }
                }
                BuildSalidasBinding();
                cargar_saldos();
                P.Mensaje.Info("Se han guardado los registros como borrador");
            }
            
        }
        private void btn_out_temp_Click(Object sender, EventArgs e) //Handles btn_out_temp.Click
        {
            //obtenemos batch's disponibles
            List<DetalleOBC> disponibles = P.Consultas.obc.salidas_abiertas();
            if ( disponibles.Count > 0 ) {
                var ventana = new Busquedas.obc_borrador(disponibles);
                DialogResult elegir;
                elegir = ventana.ShowDialogFrom(this);
                if (  elegir == DialogResult.Yes ) {
                    var ToProcess = ventana.ToProcess;
                    //quito todas las filas existentes
                    if (salidaAF.Rows.Count > 0)
                    {
                        var accion = P.Mensaje.Confirmar("Ya hay registros para procesar, desea quitarlos?");
                        if (accion == DialogResult.Yes)
                        {
                            BuildSalidasBinding(ToProcess);
                        }
                        else
                        {
                            var ActualList = GetSalidasBinding();
                            ActualList.AddRange(ToProcess);
                            BuildSalidasBinding(ActualList);
                        }
                    }
                    else
                    {
                        BuildSalidasBinding(ToProcess);
                    }
                    cargar_saldos();

                    //EmontoMax.Text = Format(Val(Replace(EmontoMax.Text,sMil, "")) - Val(Replace(EmontoSel.Text, sMil, "")), "#,##0")
                    //EmontoSel.Text = 0
                    salidaAF.ClearSelection();
                    //act_batch = batch
                }
                if (elegir == DialogResult.OK)
                {
                    EditingAreaSet(ventana.ToEdit);
                }
            } 
            else {
                P.Mensaje.Info("No hay resultados disponibles para cargar");
            }
        }
        
        private void btn_edit_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = null;
            foreach (DataGridViewRow fila in salidaAF.SelectedRows)
                dgvr = fila;

            if (dgvr != null)
            {
                DetalleOBC info = (DetalleOBC)dgvr.DataBoundItem;
                salidaAF.Rows.Remove(dgvr);
                cargar_saldos();
                EditingAreaSet(info);
                salidaAF.ClearSelection();
            }
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

        private void EditingAreaSet(DetalleOBC information)
        {
            Ecod.Text = information.codigo.ToString();
            Edesc.Text = information.descripcion;
            EmontoMax.Text = information.maximoGet().ToString("#,##0");
            EmontoSel.Text = information.saldo.ToString("#,##0");
            Efecha.Value = information.fecha;
            Ezona = information.zona;
            idSalida = information.idGet();
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

        
    }
}
