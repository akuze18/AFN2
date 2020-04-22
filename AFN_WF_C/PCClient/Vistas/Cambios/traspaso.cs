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
    public partial class traspaso : AFN_WF_C.PCClient.FormBase
    {
        private int rowindx;
        private PD.GENERIC_VALUE zona_art;
        private PD.GENERIC_VALUE subzona_art;
        private int Gparte;
        private int _codig;
        private int codigoArt
        {
            get { return _codig; }
            set { _codig = value; cod_art.Text = (value == 0?"":value.ToString()); }
        }

        private List<DetalleArticulo> ActualDetalleLote;

        private void ListBinding(List<CambioDisplay> toWork)
        {
            BindingSource source = new BindingSource();
            source.DataSource = toWork;
            lista_cambiar.DataSource = source;
        }
        private List<CambioDisplay> GetBinding()
        {
            var MyDataSource = (BindingSource)lista_cambiar.DataSource;
            List<CambioDisplay> toWork = (List<CambioDisplay>)MyDataSource.DataSource;
            return toWork;
        }
        private void AddBinding(CambioDisplay item)
        {
            var bnd = GetBinding();
            bnd.Add(item);
            ListBinding(bnd);
        }

        public traspaso()
        {
            InitializeComponent();
        }

        #region Del Formulario
        
        private void form_cambio_Load(Object sender, EventArgs e) //Handles MyBase.Load
        {
            //cargo columnas
            List<CambioDisplay> recordset = new List<CambioDisplay>();
            ListBinding(recordset);
            //lista_cambiar.DataSource = recordset;
            lista_cambiar.ColumnHeadersHeight = lista_cambiar.ColumnHeadersHeight * 2;
            lista_cambiar.RowHeadersVisible = false;
            //lista_cambiar.AllowUserToResizeColumns = False
            foreach(DataGridViewColumn columna in lista_cambiar.Columns)
            {
                columna.SortMode = DataGridViewColumnSortMode.NotSortable;
                //columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            }
            lista_cambiar.Columns[0].HeaderText = "Código Artículo";
            lista_cambiar.Columns[0].Width = 60;
            lista_cambiar.Columns[1].HeaderText = "Fecha Traspaso";
            lista_cambiar.Columns[1].Width = 90;
            lista_cambiar.Columns[2].HeaderText = "Cantidad Traspaso";
            lista_cambiar.Columns[2].Width = 67;
            lista_cambiar.Columns[3].HeaderText = "Zona Destino";
            lista_cambiar.Columns[3].Width = 50;
            lista_cambiar.Columns[4].HeaderText = "SubZona Destino";
            lista_cambiar.Columns[4].Width = 60;
            lista_cambiar.Columns[5].HeaderText = "rowindx";
            lista_cambiar.Columns[5].Visible = false;
            lista_cambiar.Columns[6].HeaderText = "Descripcion Artículo";
            lista_cambiar.Columns[6].Width = 300;
            lista_cambiar.Columns[7].HeaderText = "parte";
            lista_cambiar.Columns[7].Visible = false;
            //lista_cambiar.Columns[8].HeaderText = "";

            Dcambio.Value = Today;
            Dcambio.MaxDate = Today;

            iniciar_formulario();
        }

        private void form_cambio_Resize(Object sender,EventArgs e) //Handles Me.Resize
        {
            lista_cambiar.Size = new Size(this.Size.Width - 16 - 40, this.Size.Height - 38 - 273);
            decimal halfWidth = lista_cambiar.Size.Width / 2;
            int pointX = (int)(Math.Round(halfWidth, 0) - 8);
            btn_fin.Location = new Point(pointX, lista_cambiar.Location.Y + lista_cambiar.Size.Height + 6);
        }

        private void iniciar_formulario()
        {
            //bloqueo
            cboCant.Items.Clear();
            Dcambio.Enabled = false;
            Tarticulo.Enabled = false;
            Tarticulo.Text = String.Empty;
            Tarticulo.BackColor = Color.LightGray;
            Tvalor.Enabled = false;
            Tvalor.Text = String.Empty;
            Tvalor.BackColor = Color.LightGray;
            P.Auxiliar.ActivarF(TzonaAct, false);
            TzonaAct.Text = String.Empty;
            P.Auxiliar.ActivarF(TsubZact, false);
            TsubZact.Text = String.Empty;
            btn_consulta.BackColor = Color.Red;
            rowindx = 0;
            zona_art = null;
            subzona_art = null;
            codigoArt = 0;
            Gparte = 0;
            ActualDetalleLote = new List<DetalleArticulo>();
        }
    #endregion

    #region De los Controles
        private void cargar_formulario(PD.DETAIL_PROCESS info)
        {
            this.Enabled = false;
            iniciar_formulario();
            List<PD.GENERIC_VALUE> TBzonas;
            int cantid, cont_subz;
            PD.GENERIC_VALUE zona;
        
            zona = info.zona;
            TBzonas = P.Consultas.zonas.All();
            //Determino si es necesario quitar la zona del combo
            PD.GENERIC_VALUE remove_fila = null;
            foreach(PD.GENERIC_VALUE fila in TBzonas)
            {
               if (fila.code == zona.code)
               {
                    TzonaAct.Text = fila.description;
                    cont_subz = P.Consultas.subzonas.ByZone(zona).Count;
                    if (cont_subz == 0 )
                    {
                        //zona no tiene subzonas, por lo que no puede ser cambiada dentro de la misma zona
                        remove_fila = fila;
                    }
                    if (cont_subz == 1) 
                    {
                        if (info.subzona.id != 0 )
                        {
                            //solo tiene una subzona y no puede cambiarse a si misma
                            remove_fila = fila;
                        }
                        else
                        {
                            //si bien tiene solo 1 subzona, actualmente no tiene indicada una (valor 0) por lo que
                            //se puede hacer el cambio, desde "sin subzonas" a la subzona disponible
                        }
                    }
               }
            }
            if (! (remove_fila == null))
                TBzonas.Remove(remove_fila);

            cboZona.Items.Clear();
            cboZona.Items.AddRange(TBzonas.ToArray());
            cboZona.SelectedIndex = -1;

            codigoArt = info.cod_articulo;
            Gparte = info.parte;

            cantid = info.cantidad;
            for (int i = 1; i<=cantid; i++)
                cboCant.Items.Add(i);
        
            cboCant.SelectedIndex = (cboCant.Items.Count - 1);

            zona_art = info.zona;
            //TzonaAct.Text = recordset.Rows(0).Item("")

            subzona_art = info.subzona;
            TsubZact.Text = subzona_art.description;

            Dcambio.Enabled = true;
            Dcambio.MinDate = (new ACode.Vperiodo(Today.Year,Today.Month) -1).first;
            rowindx = info.HeadId;

            Tarticulo.Text = info.dscrp;
            Tvalor.Text = "0";  //Format(recordset.Rows(0).Item("val_libro"), "General Number")
            btn_consulta.BackColor = SystemColors.Window;
            this.Enabled = true;
        }

        private void cboZona_SelectedIndexChanged(Object sender, EventArgs e) //Handles cboZona.SelectedIndexChanged
        {
            if (cboZona.SelectedIndex != -1)
            {
                var sel_zona =(PD.GENERIC_VALUE) cboZona.SelectedItem;
                var colchon = P.Consultas.subzonas.ByZone(sel_zona);
                cboSubzona.Items.AddRange(colchon.ToArray());
                cboSubzona.SelectedIndex = -1;
            }
        }
        private void cboCant_SelectedIndexChanged(Object sender, EventArgs e) //Handles cboCant.SelectedIndexChanged
        {
            if (cboCant.SelectedIndex != -1)
            {
                int cantid;
                cantid = (int)cboCant.SelectedItem;
                ActualDetalleLote = P.Consultas.inventario.GetDetalleArticulo(codigoArt, Gparte, cantid);
            }
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
        private void btn_add_Click(Object sender, EventArgs e) //Handles btn_add.Click
        {
            if (rowindx != 0)
            {
                bool pasa;
                //valido combos
                if (cboZona.SelectedIndex == -1)
                {
                    P.Mensaje.Advert("Debe indicar la zona de destino del Activo Fijo");
                    cboZona.Focus();
                    return;
                }
                if (cboSubzona.SelectedIndex == -1 && cboSubzona.Items.Count > 0 )
                {
                    P.Mensaje.Advert("Debe indicar la subzona de destino del Activo Fijo");
                    cboSubzona.Focus();
                    return;
                }
                //busco que no este ingresado
                pasa = true;
                foreach(DataGridViewRow fila in lista_cambiar.Rows)
                {
                    CambioDisplay data = (CambioDisplay) fila.DataBoundItem;
                    if (data.indice == rowindx)
                    {
                        pasa = false;
                    }
                }
                if (pasa)
                {
                    CambioDisplay newfila = new CambioDisplay();
                    PD.GENERIC_VALUE Vsubzona;
                    if (cboSubzona.SelectedIndex == -1)
                        Vsubzona = PD.SV_SUBZONE.Empty();
                    else
                        Vsubzona = (PD.GENERIC_VALUE)cboSubzona.SelectedItem;

                     
                    newfila.codigo_articulo = codigoArt;
                    newfila.fecha_proceso = Dcambio.Value;
                    newfila.cantidad_proceso = (int)cboCant.SelectedItem;
                    newfila.zona = (PD.GENERIC_VALUE) (cboZona.SelectedItem);
                    newfila.subzona = Vsubzona;
                    newfila.indice = rowindx;
                    newfila.descripcion = Tarticulo.Text;
                    newfila.parte = Gparte;
                    newfila.detalle = ActualDetalleLote;
                    
                    AddBinding(newfila);

                    //detalle_add(ActualDetalleLote, cod_art.Text, Gparte.Text, rowindx.Text)

                    lista_cambiar.ClearSelection();
                }
                else
                {
                    P.Mensaje.Advert("Articulo indicado ya ha sido agregado para ser traspasado");
                }
            }
        }
        private void btn_remove_Click(Object sender, EventArgs e) //Handles btn_remove.Click
        {
            bool limpiar;
            limpiar = false;
            if (lista_cambiar.SelectedRows.Count > 0)
            {
                foreach(DataGridViewRow fila_sel in lista_cambiar.SelectedRows)
                {
                    DialogResult elige;
                    CambioDisplay select = (CambioDisplay)fila_sel.DataBoundItem;
                    elige = P.Mensaje.Confirmar("Está seguro que desea eliminar el lote artículo " + select.codigo_articulo.ToString() + " de la lista de traspasos?");
                    if (elige == DialogResult.Yes)
                    {
                        lista_cambiar.Rows.Remove(fila_sel);
                        limpiar = true;
                    }
                }
            }
            else
            {
                P.Mensaje.Advert("No ha indicado ningun registro para quitar del listado de traspaso");
            }
            if (limpiar)
                lista_cambiar.ClearSelection();
        }
        private void btn_fin_Click(Object sender, EventArgs e) //Handles btn_fin.Click
        {
            if (lista_cambiar.Rows.Count > 0)
            {
                DialogResult elige;
                elige = P.Mensaje.Confirmar("Está seguro que desea procesar los artículos de la lista de traspasos?");
                if (elige == DialogResult.Yes)
                {
                    PD.RespuestaAccion recordset;
                    foreach (DataGridViewRow fila in lista_cambiar.Rows)
                    {
                        if (fila.DefaultCellStyle.BackColor != StatusColor.AFNok)
                        {

                            //las celdas que estuvieran ok no se procesan
                            fila.DefaultCellStyle.BackColor = StatusColor.AFNprocess;
                            lista_cambiar.Refresh();
                            CambioDisplay data = (CambioDisplay)fila.DataBoundItem;
                            string usuario = P.Auxiliar.getUser();
                            recordset = P.Consultas.movimientos.CAMBIO_ZONA(data.codigo_articulo, data.parte, data.fecha_proceso, data.zona, data.subzona, data.cantidad_proceso, usuario, data.detalle);
                            if (recordset.codigo < 0)
                            {
                                //se produjo un error al momento de generar al castigo en la base de datos
                                fila.DefaultCellStyle.BackColor = StatusColor.AFNfail;
                                lista_cambiar.Refresh();
                                string vbCrLf = "";
                                P.Mensaje.Error(recordset.descripcion + vbCrLf + "Fila: " + fila.Index.ToString());
                                return;
                            }
                            else
                            {
                                fila.DefaultCellStyle.BackColor = StatusColor.AFNok;
                                lista_cambiar.Refresh();
                            }
                        }
                        Application.DoEvents();
                    }
                    recordset = null;
                    P.Mensaje.Info("Traspaso se ha realizado exitosamente");
                    this.Close();
                }
            }
            else
            {
                P.Mensaje.Advert("No ha agregado ningun articulo al listado de traspaso");
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
        #endregion
    }
}
