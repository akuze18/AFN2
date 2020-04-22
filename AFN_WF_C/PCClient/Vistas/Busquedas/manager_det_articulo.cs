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

namespace AFN_WF_C.PCClient.Vistas.Busquedas
{
    public partial class manager_det_articulo : AFN_WF_C.PCClient.FormBase
    {
        public enum form_accion
        {
            cambio,
            castigo,
            venta,
        }

        private form_accion _accion;
        private int _id;
        private int _parte;
        //private List<DetalleArticulo> _detalle;
        private int _cantidad;

        public manager_det_articulo(int id , int parte, form_accion accion, List<DetalleArticulo> mydetalle)
        {
            InitializeComponent();
            _accion = accion;
            _id = id;
            _parte = parte;
            //_detalle = mydetalle;
            ListBinding(mydetalle);
            _cantidad = cantidad_detalle();

            DialogResult = DialogResult.Cancel;
        }

        #region Formulario
        private void manager_det_articulo_Load(object sender, EventArgs e)
        {
            switch(_accion)
            {
                case form_accion.cambio:
                    this.Text = "Detalle de artículos para cambiar de zona/subzona";
                    break;
                case form_accion.castigo:
                    this.Text = "Detalle de artículos para castigar";
                    break;
                case form_accion.venta:
                    this.Text = "Detalle de artículos para vender";
                    break;
            }
            TB_cod_lote.Text = _id.ToString();
            TB_cod_lote.Enabled = false;
            LBdescrip.Text = "";//base.articulo_descrip(_id)
            //DG_articulos.DataSource = _detalle;
            DG_articulos.RowHeadersVisible = false;
            DG_articulos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DG_articulos.AllowUserToResizeColumns = false;
            DG_articulos.Columns[0].HeaderText = "Producto";
            DG_articulos.Columns[0].Width = 200;
            DG_articulos.Columns[1].HeaderText = "codigoLote";
            DG_articulos.Columns[1].Visible = false;
            DG_articulos.Columns[2].HeaderText = "parteLote";
            DG_articulos.Columns[2].Visible = false;
            DG_articulos.Columns[3].HeaderText = "Procesar";
            DG_articulos.Columns[4].HeaderText = "rowId";
            DG_articulos.Columns[4].Visible = false;
            DG_articulos.Columns[5].HeaderText = "PartId";
            DG_articulos.Columns[5].Visible = false;
            foreach(DataGridViewColumn columna in DG_articulos.Columns)
            {
                columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            btn_less.Text = "Sel Inf";
            btn_top.Text = "Sel Sup";
            btn_clear.Text = "Borrar";
            mark_total.Text = "/ " + _cantidad.ToString();
            mark_actual.Text = cantidad_detalle().ToString();
        }
        private int cantidad_detalle()
        {
            return GetBinding().FindAll(d => d.procesar).Count;
        }
        #endregion

        #region Controles
        private void DG_articulos_CellClick(Object sender, DataGridViewCellEventArgs e) //Handles DG_articulos.CellClick
        {
            DataGridView LAtrib = (DataGridView) sender;
            int fil, col, col_procesar;
            col_procesar = 3;
            fil = e.RowIndex;
            col = e.ColumnIndex;
            if (fil != -1 && col == col_procesar)
            {
                DetalleArticulo data = (DetalleArticulo)LAtrib.Rows[fil].DataBoundItem;
                Boolean valor = data.procesar;
                if(valor)
                {
                    mark_actual.Text = (cantidad_detalle() - 1).ToString();
                }
                else
                {
                    mark_actual.Text = (cantidad_detalle() + 1).ToString();
                }
                data.procesar = !valor;

                var currList = GetBinding();
                currList[fil] = data;
                ListBinding(currList);


                RBclear.Focus();
            }
        }

        private void btn_less_Click(Object sender, EventArgs e) //Handles btn_less.Click
        {
            int cont = 0;
            var renew = new List<DetalleArticulo>();
            foreach (DetalleArticulo fila in GetBinding())
            {
                if (cont < (GetBinding().Count - _cantidad))
                {
                    fila.procesar = false;
                }else{
                    fila.procesar = true;
                }
                renew.Add(fila);
                cont = cont + 1;
            }
            ListBinding(renew);
            mark_actual.Text = _cantidad.ToString();
        }

        private void btn_top_Click(Object sender, EventArgs e) //Handles btn_top.Click
        {
            int cont = 0;
            var renew = new List<DetalleArticulo>();
            foreach (DetalleArticulo fila in GetBinding())
            {
                if ( cont < _cantidad){
                    fila.procesar = true;
                }else{
                    fila.procesar = false;
                }
                renew.Add(fila);
                cont = cont + 1;
            }
            ListBinding(renew);
            mark_actual.Text = _cantidad.ToString();
        }

        private void btn_clear_Click(Object sender, EventArgs e)// Handles btn_clear.Click
        {
            var renew = new List<DetalleArticulo>();
            foreach (DetalleArticulo fila in GetBinding())
            {
                fila.procesar = false;
                renew.Add(fila);
            }
            ListBinding(renew);
            mark_actual.Text = "0";
        }

        private void btn_guardar_Click(Object sender, EventArgs e) // Handles btn_guardar.Click
        {
            //Valido que los ticket existentes corresponden a la cantidad requerida
            int cant_actual = cantidad_detalle();
            if (cant_actual != _cantidad)
            {
                string mensaje;
                mensaje = "No ha indicado la cantidad de registros necesarios para ";
                switch(_accion)
                {
                    case form_accion.cambio:
                        mensaje = mensaje + "cambiar de zona/subzona";
                        break;
                    case form_accion.castigo:
                        mensaje = mensaje + "castigar";
                        break;
                    case form_accion.venta:
                        mensaje = mensaje + "vender";
                        break;
                    default:
                        mensaje = mensaje + "procesar";
                        break;
                }
                P.Mensaje.Advert(mensaje);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        #region Retornos para solicitantes
        public List<DetalleArticulo> detalle
        {
            get
            {
                if (DialogResult == DialogResult.OK)
                    return GetBinding();
                else
                    return new  List<DetalleArticulo>();
            }
        }

        #endregion


        #region DG_articulos DataSource
        private void ListBinding(List<DetalleArticulo> toWork)
        {
            BindingSource source = new BindingSource();
            source.DataSource = toWork;
            DG_articulos.DataSource = source;
        }
        private List<DetalleArticulo> GetBinding()
        {
            var MyDataSource = (BindingSource)DG_articulos.DataSource;
            List<DetalleArticulo> toWork = (List<DetalleArticulo>)MyDataSource.DataSource;
            return toWork;
        }
        private void AddBinding(DetalleArticulo item)
        {
            var bnd = GetBinding();
            bnd.Add(item);
            ListBinding(bnd);
        }
        #endregion
    }
}
