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
    public partial class castigo : AFN_WF_C.PCClient.FormBase
    {
        public castigo()
        {
            InitializeComponent();
        }

        private int rowindx;
        private PD.GENERIC_VALUE zona_art;
        private int Gparte;
        private int _codig;
        private int codigoArt
        {
            get { return _codig; }
            set { _codig = value; cod_art.Text = (value == 0?"":value.ToString()); }
        }

        private List<DetalleArticulo> ActualDetalleLote;

        private void ListBinding(List<BajasDisplay> toWork)
        {
            BindingSource source = new BindingSource();
            source.DataSource = toWork;
            lista_castigar.DataSource = source;
        }
        private List<BajasDisplay> GetBinding()
        {
            var MyDataSource = (BindingSource)lista_castigar.DataSource;
            List<BajasDisplay> toWork = (List<BajasDisplay>)MyDataSource.DataSource;
            return toWork;
        }
        private void AddBinding(BajasDisplay item)
        {
            var bnd = GetBinding();
            bnd.Add(item);
            ListBinding(bnd);
        }

        private void castigo_Load(object sender, EventArgs e)
        {
            //cargo columnas
            List<BajasDisplay> recordset = new List<BajasDisplay>();
            ListBinding(recordset);
            lista_castigar.ColumnHeadersHeight = lista_castigar.ColumnHeadersHeight * 2;
            lista_castigar.RowHeadersVisible = false;
            lista_castigar.AllowUserToResizeColumns = false;
            foreach(DataGridViewColumn columna in lista_castigar.Columns)
            {
                columna.SortMode = DataGridViewColumnSortMode.NotSortable;
                //columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            }
            //configuro anchos
            lista_castigar.Columns[0].HeaderText = "Código Artículo";
            lista_castigar.Columns[0].Width = 60;
            lista_castigar.Columns[1].HeaderText = "Fecha Castigo";
            lista_castigar.Columns[1].Width = 90;
            lista_castigar.Columns[2].HeaderText = "Cantidad Castigada";
            lista_castigar.Columns[2].Width = 67;
            lista_castigar.Columns[3].HeaderText = "Descripcion Artículo";
            lista_castigar.Columns[3].Width = 300;
            lista_castigar.Columns[4].HeaderText = "rowindx";
            lista_castigar.Columns[4].Visible = false;
            lista_castigar.Columns[5].Visible = false;
            lista_castigar.Columns[6].Visible = false;
            lista_castigar.Columns[7].Visible = false;
            lista_castigar.Columns[8].Visible = false;
            iniciar_formulario();
            Dcastigo.Value = Today;
            Dcastigo.MaxDate = Today;
        }

        private void iniciar_formulario()
        {
            //bloqueo
            cboCant.Items.Clear();
            Dcastigo.Enabled = false;
            Tarticulo.Enabled = false;
            Tarticulo.Text = string.Empty;
            Tarticulo.BackColor = Color.LightGray;
            Tvalor.Enabled = false;
            Tvalor.Text = string.Empty;
            Tvalor.BackColor = Color.LightGray;
            CheckF.Enabled = false;
            CheckI.Enabled = false;
            btn_consulta.BackColor = Color.Red;
            rowindx = 0;
            zona_art = null;
            codigoArt = 0;
            Gparte = 0;
            ActualDetalleLote = new List<DetalleArticulo>();
        }

        private void cargar_formulario(PD.DETAIL_PROCESS info)
        {
            iniciar_formulario();
            for (int i = 1; i <= info.cantidad; i++)
                cboCant.Items.Add(i);
            codigoArt = info.cod_articulo;
            Gparte = info.parte;
            zona_art = info.zona;
            rowindx = info.HeadId;
            cboCant.SelectedIndex = cboCant.Items.Count - 1;
            Dcastigo.Enabled = true;
            Tarticulo.Text = info.descrip_complete;
            Tarticulo.BackColor = System.Drawing.SystemColors.Window;
            Tvalor.Text = "0";// info.parametros.GetPrecioBase.value.ToString();
            Tvalor.BackColor = System.Drawing.SystemColors.Window;
            btn_consulta.BackColor = System.Drawing.SystemColors.Window;
        }

        private void btn_consulta_Click(Object sender, EventArgs e)// Handles btn_consulta.Click
        {
            var box = new Busquedas.articulo();
            box.set_criterios(Busquedas.tipo_vigencia.vigentes,Busquedas.tipo_estado.soloActivos);
            var respuesta = box.ShowDialogFrom(this);
            if(respuesta == DialogResult.OK)
            {
                cargar_formulario(box.full_data);
            }
            box = null;
        }
        private void lista_castigar_CellContentClick(Object sender, DataGridViewCellEventArgs e) //Handles lista_castigar.CellContentClick
        {
            if(e.RowIndex == -1)
            {
                lista_castigar.ClearSelection();
            }
        }

        private void btn_add_Click(Object sender, EventArgs e)// Handles btn_add.Click
        {
            if(rowindx != 0)
            {
                bool pasa;
                //busco que no este ingresado
                pasa = true;
                foreach(BajasDisplay fila in GetBinding())
                {
                    if (fila.indice == rowindx)
                    {
                        pasa = false;
                    }
                }
                if(pasa){
                    var newfila = new BajasDisplay();
                    newfila.codigo_articulo = codigoArt;
                    newfila.fecha_proceso = Dcastigo.Value;
                    newfila.cantidad_baja = (int)(cboCant.SelectedItem);
                    newfila.descripcion = Tarticulo.Text;
                    newfila.indice = rowindx;
                    newfila.zona = zona_art;
                    newfila.inTrib = CheckT.Checked;
                    newfila.inIFRS = true;
                    newfila.detalle = ActualDetalleLote;
                    newfila.parte = Gparte;
                    AddBinding(newfila);
                    lista_castigar.ClearSelection();
                }
                else
                    P.Mensaje.Advert("Articulo indicado ya ha sido agregado para el castigo");
                
            }
        }
        private void btn_remove_Click(Object sender, EventArgs e)// Handles btn_remove.Click
        {
            bool limpiar;
            limpiar = false;
            if(lista_castigar.SelectedRows.Count > 0){
                foreach(DataGridViewRow fila_sel in lista_castigar.SelectedRows)
                {
                    DialogResult elige;
                    BajasDisplay select = (BajasDisplay)fila_sel.DataBoundItem;
                    elige = P.Mensaje.Confirmar("Está seguro que desea eliminar el artículo " + select.codigo_articulo.ToString() + " de la lista de castigos?");
                    if(elige == DialogResult.Yes){
                        lista_castigar.Rows.Remove(fila_sel);
                        limpiar = true;
                    }
                }
            }
            else
                P.Mensaje.Info("No ha indicado ningun registro para quitar del listado de castigo");
            
            if(limpiar){
                lista_castigar.ClearSelection();
            }
        }
        private void btn_fin_Click(Object sender, EventArgs e ) //Handles btn_fin.Click
        {
            if (lista_castigar.Rows.Count > 0){
                DialogResult elige;
                elige = P.Mensaje.Confirmar("Está seguro que desea procesar los artículos de la lista de castigos?");
                if(elige == DialogResult.Yes){
                    var respuesta = new PD.RespuestaAccion();
                    foreach(DataGridViewRow fila in lista_castigar.Rows)
                    {
                        if(fila.DefaultCellStyle.BackColor != StatusColor.AFNok)
                        {
                            //las celdas que estuvieran ok no se procesan
                            fila.DefaultCellStyle.BackColor = StatusColor.AFNprocess;
                            lista_castigar.Refresh();
                            BajasDisplay data = (BajasDisplay)fila.DataBoundItem;
                            //recordset = base.CASTIGO(codigo_articulo, parte_articulo, newfecha, newcantidad, procT, form_welcome.GetUsuario, TotalDetalleLote, codigo_grupo)
                            respuesta = P.Consultas.movimientos.castigo_act(data.codigo_articulo,data.parte,data.fecha_proceso,data.cantidad_baja,P.Auxiliar.getUser(), data.detalle);
                            if( respuesta.codigo < 0)
                            {
                                //se produjo un error al momento de generar al castigo en la base de datos
                                fila.DefaultCellStyle.BackColor = StatusColor.AFNfail;
                                lista_castigar.Refresh();
                                string vbCrLf = " ";
                                P.Mensaje.Error(respuesta.descripcion + vbCrLf + "Fila: " + fila.Index.ToString());
                                return;
                            }
                            else
                            {
                                fila.DefaultCellStyle.BackColor = StatusColor.AFNok;
                                lista_castigar.Refresh();
                            }
                        }
                        Application.DoEvents();
                    }
                    respuesta = null;
                    P.Mensaje.Info("Castigo se ha realizado con exito");
                    this.Close();
                }
            }
            else
                P.Mensaje.Info("No ha agregado ningun articulo al listado de castigo");
            
        }

        private void cboCant_SelectedIndexChanged(Object sender, EventArgs e)// Handles cboCant.SelectedIndexChanged
        {
            if (cboCant.SelectedIndex != -1){
                int cantid;
                cantid = (int)cboCant.SelectedItem;
                ActualDetalleLote = P.Consultas.inventario.GetDetalleArticulo(codigoArt, Gparte, cantid);
            }
        }

        private void btn_detalle_cantidad_Click(Object sender, EventArgs e) //Handles btn_detalle_cantidad.Click
        {
            //Valido que la información necesaria para activar esta opcion este completa
            if (codigoArt ==0 )
            {
                return;
            }
            if (cboCant.SelectedIndex == -1){
                return;
            }

            if( ActualDetalleLote == null || ActualDetalleLote.Count == 0)
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

    }
}
