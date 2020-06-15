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
    public partial class venta : AFN_WF_C.PCClient.FormBase
    {
        private PD.GENERIC_VALUE zona_art;
        private int rowindx;
        private int Gparte;
        private int _codig;
        private int codigoArt
        {
            get { return _codig; }
            set { _codig = value; cod_art.Text = (value == 0 ? "" : value.ToString()); }
        }

        private List<DetalleArticulo> ActualDetalleLote;

        public venta()
        {
            InitializeComponent();
        }

        private void ListBinding(List<BajasDisplay> toWork)
        {
            BindingSource source = new BindingSource();
            source.DataSource = toWork;
            lista_vender.DataSource = source;
        }
        private List<BajasDisplay> GetBinding()
        {
            var MyDataSource = (BindingSource)lista_vender.DataSource;
            List<BajasDisplay> toWork = (List<BajasDisplay>)MyDataSource.DataSource;
            return toWork;
        }
        private void AddBinding(BajasDisplay item)
        {
            var bnd = GetBinding();
            bnd.Add(item);
            ListBinding(bnd);
        }

        private void venta_Load(object sender, EventArgs e)
        {
            //agrego columnas
            List<BajasDisplay> recordset = new List<BajasDisplay>();
            ListBinding(recordset);
            
            lista_vender.ColumnHeadersHeight = lista_vender.ColumnHeadersHeight * 2;
            lista_vender.RowHeadersVisible = false;
            lista_vender.AllowUserToResizeColumns = false;
            foreach (DataGridViewColumn columna in lista_vender.Columns)
            {
                columna.SortMode = DataGridViewColumnSortMode.NotSortable;
                //columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            }
            lista_vender.Columns[0].HeaderText = "Código Artículo";
            lista_vender.Columns[0].Width = 60;
            lista_vender.Columns[1].HeaderText = "Fecha Venta";
            lista_vender.Columns[1].Width = 90;
            lista_vender.Columns[2].HeaderText = "Cantidad Vendida";
            lista_vender.Columns[2].Width = 67;
            lista_vender.Columns[3].HeaderText = "Descripcion Artículo";
            lista_vender.Columns[3].Width = 300;
            lista_vender.Columns[4].HeaderText = "Zona";
            lista_vender.Columns[4].Width = 60;
            lista_vender.Columns[5].Visible = false;
            lista_vender.Columns[6].Visible = false;
            lista_vender.Columns[7].Visible = false;
            lista_vender.Columns[8].Visible = false;
            iniciar_formulario();
            Dventa.Value = Today;
            Dventa.MaxDate = Today;
        }
        private void iniciar_formulario()
        {
            //bloqueo
            cboCant.Items.Clear();
            Dventa.Enabled = false;
            Tarticulo.Enabled = false;
            Tarticulo.Text = string.Empty;
            Tarticulo.BackColor = Color.LightGray;
            Tvalor.Enabled = false;
            rowindx = 0;
            Gparte = 0;
            codigoArt = 0;
            zona_art = null;
            Tvalor.Text = string.Empty;
            Tvalor.BackColor = Color.LightGray;
            btn_consulta.BackColor = Color.Red;
            ActualDetalleLote = new List<DetalleArticulo>();
        }

        public void cargar_formulario(PD.DETAIL_PROCESS info)
        {
            iniciar_formulario();
            for(int i = 1; i<= info.cantidad;i++)
                cboCant.Items.Add(i);

            codigoArt = info.cod_articulo;
            Gparte = info.parte;
            zona_art = info.zona;
            rowindx = info.HeadId;
            cboCant.SelectedIndex = cboCant.Items.Count - 1;
            Dventa.Enabled = true;
            //'Dventa.MaxDate = Now
            //'Dventa.Value = Now
            Tarticulo.Text = info.descrip_complete;
            Tarticulo.BackColor = System.Drawing.SystemColors.Window;
            Tvalor.Text = "0";//info.parametros.GetValorResidual.value.ToString();// recordset.Rows(0).Item("val_libro")
            Tvalor.BackColor = System.Drawing.SystemColors.Window;
            btn_consulta.BackColor = System.Drawing.SystemColors.Window;
        }

        private void btn_consulta_Click(Object sender, EventArgs ByVal) //Handles btn_consulta.Click
        {
            var box = new Vistas.Busquedas.articulo();
            box.set_criterios(Busquedas.tipo_vigencia.vigentes, Busquedas.tipo_estado.soloActivos);
            var respuesta = box.ShowDialogFrom(this);
            if(respuesta == DialogResult.OK){
                //respuesta de articulos
                cargar_formulario(box.full_data);
            }
            box = null;
        }
        private void lista_vender_CellClick(Object sender, DataGridViewCellEventArgs e) //Handles lista_vender.CellClick
        {
            if (e.RowIndex == -1)
            {
                lista_vender.ClearSelection();
            }
        }
        private void cboCant_SelectedIndexChanged(Object sender, EventArgs e)// Handles cboCant.SelectedIndexChanged
        {
            if (cboCant.SelectedIndex != -1)
            {
                int cantid;
                cantid = (int)cboCant.SelectedItem;
                ActualDetalleLote = P.Consultas.inventario.GetDetalleArticulo(codigoArt, Gparte, cantid);
            }
        }
        private void btn_detalle_cantidad_Click(Object sender, EventArgs e) //Handles btn_detalle_cantidad.Click
        {
            //Valido que la información necesaria para activar esta opcion este completa
            if (codigoArt == 0)
            {
                return;
            }
            if (cboCant.SelectedIndex == -1)
            {
                return;
            }

            if (ActualDetalleLote == null || ActualDetalleLote.Count == 0)
            {
                P.Mensaje.Error("Se produjo un error al obtener el detalle de los articulos del lote");
                return;
            }

            DialogResult resultado;
            var aux = new Busquedas.manager_det_articulo(codigoArt, Gparte, Busquedas.manager_det_articulo.form_accion.castigo, ActualDetalleLote);
            resultado = aux.ShowDialogFrom(this);
            if (resultado == DialogResult.OK)
            {
                ActualDetalleLote = aux.detalle;
            }
            aux = null;
        }

        private void btn_add_Click(Object sender, EventArgs e)// Handles btn_add.Click
        {
            if(rowindx != 0){
                bool pasa;
                //busco que no este ingresado
                pasa = true;
                foreach (BajasDisplay fila in GetBinding())
                {
                    if (fila.indice == rowindx){
                        pasa = false;
                    }
                }
                if(pasa)
                {
                    BajasDisplay newfila = new BajasDisplay();
                    newfila.codigo_articulo = codigoArt;
                    newfila.fecha_proceso = Dventa.Value;
                    newfila.cantidad_baja = (int)cboCant.SelectedItem;
                    newfila.descripcion = Tarticulo.Text;
                    newfila.zona = zona_art;
                    newfila.indice = rowindx;
                    newfila.parte = Gparte;
                    newfila.detalle = ActualDetalleLote;
                    newfila.inIFRS = true;
                    newfila.inTrib = true;
                    AddBinding(newfila);
                    lista_vender.ClearSelection();
                }
                else
                {
                    P.Mensaje.Advert("Articulo indicado ya ha sido agregado para la venta");
                }
            }
        }
        private void btn_remove_Click(Object sender, EventArgs e) //Handles btn_remove.Click
        {
            bool limpiar;
            limpiar = false;
            if (lista_vender.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow fila_sel in lista_vender.SelectedRows)
                {
                    BajasDisplay data = (BajasDisplay)fila_sel.DataBoundItem;
                    DialogResult elige;
                    elige = P.Mensaje.Confirmar("Está seguro que desea eliminar el artículo " + data.codigo_articulo.ToString() + " de la lista de ventas?");
                    if(elige == DialogResult.Yes)
                    {
                        lista_vender.Rows.RemoveAt(fila_sel.Index);// .RemoveObject(fila_sel);
                        limpiar = true;
                    }
                }
            }
            else
            {
                P.Mensaje.Info("No ha indicado ningun registro para quitar del listado de venta");
            }
            if (limpiar)
            {
                lista_vender.ClearSelection();
            }
        }
        private void btn_fin_Click(Object sender, EventArgs e ) //Handles btn_fin.Click
        {
            if(lista_vender.Rows.Count > 0)
            {
                DialogResult elige;
                elige = P.Mensaje.Confirmar("Está seguro que desea procesar los artículos de la lista de ventas?");
                if( elige == DialogResult.Yes)
                {
                    PD.RespuestaAccion respuesta = new PD.RespuestaAccion();
                    foreach (DataGridViewRow fila in lista_vender.Rows)  
                    {
                        if(fila.DefaultCellStyle.BackColor != StatusColor.AFNok)
                        {
                            //las celdas que estuvieran ok no se procesan
                            fila.DefaultCellStyle.BackColor = StatusColor.AFNprocess;
                            lista_vender.Refresh();
                            BajasDisplay data = (BajasDisplay)fila.DataBoundItem;
                            respuesta = P.Consultas.movimientos.venta_act(data.codigo_articulo,data.parte,data.fecha_proceso,data.cantidad_baja,P.Auxiliar.getUser(), data.detalle);
                            if( respuesta.codigo < 0)
                            {
                                //se produjo un error al momento de generar al castigo en la base de datos
                                fila.DefaultCellStyle.BackColor = StatusColor.AFNfail;
                                lista_vender.Refresh();
                                string vbCrLf = " ";
                                P.Mensaje.Error(respuesta.descripcion + vbCrLf + "Fila: " + fila.Index.ToString());
                                return;
                            }
                            else
                            {
                                fila.DefaultCellStyle.BackColor = StatusColor.AFNok;
                                lista_vender.Refresh();
                            }
                        }
                        Application.DoEvents();
                    }
                    respuesta = null;
                    P.Mensaje.Info("Venta se ha realizado con exito");
                    this.Close();
                }
            }
            else
            {
                P.Mensaje.Info("No ha agregado ningun articulo al listado de venta");
            }
        }
    }
}
