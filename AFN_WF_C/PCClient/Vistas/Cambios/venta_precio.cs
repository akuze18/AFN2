using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

using BrightIdeasSoftware;
using P = AFN_WF_C.PCClient.Procesos;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    public partial class venta_precio : AFN_WF_C.PCClient.FormBase
    {
        private decimal _TotalAmount;
        public venta_precio()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
        }

        private void venta_precio_Load(object sender, EventArgs e)
        {
            Tdocumento.Text = "";
            TprecioExt.Text = "";
            Lestado_doc.Text = "";
            TprecioExt.Enabled = false;
            _TotalAmount = 0;
            DTfecha.Value = Today;
            var colVentaPrecio = new List<OLVColumn>();
            colVentaPrecio.Add(new OLVColumn() { AspectName = "CodArticulo", Text = "Cod Artículo" });
            colVentaPrecio.Add(new OLVColumn() { AspectName = "DescripArt", Text = "Descripción Artículo" });
            colVentaPrecio.Add(new OLVColumn() { AspectName = "Cantidad", Text = "Cantidad" });
            colVentaPrecio.Add(new OLVColumn() { AspectName = "PrecioUnitario", Text = "Precio Unitario", AspectToStringFormat = "{0:#,##0}" });
            colVentaPrecio.Add(new OLVColumn() { AspectName = "PrecioTotal", Text = "Precio Total", AspectToStringFormat = "{0:#,##0}" });
            
            detalle_venta.Columns.AddRange(colVentaPrecio.ToArray());
            //detalle_venta.Columns[0].Width = 0;
            detalle_venta.Columns[0].Width = (int)(120 * 0.75);
            //detalle_venta.Columns[2].Width = 0;
            detalle_venta.Columns[1].Width = (int)(450 * 0.75);
            detalle_venta.Columns[2].Width = (int)(100 * 0.75);
            //detalle_venta.Columns[5].Width = 0;
            //detalle_venta.Columns[6].Width = 0;
            detalle_venta.Columns[3].Width = (int)(150 * 0.75);
            detalle_venta.Columns[4].Width = (int)(180 * 0.75);
            //detalle_venta.Columns[9].Width = 0;
            //detalle_venta.Columns[10].Width = 0;
            //detalle_venta.Columns[11].Width = 0;
            //detalle_venta.Columns[12].Width = 0;
            //detalle_venta.RowHeadersVisible = False
            detalle_venta.FullRowSelect = true;// .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            detalle_venta.MultiSelect = false;
            detalle_venta.ShowGroups = false;
            detalle_venta.SetObjects(new List<P.Estructuras.DisplayVentaPrecio>());
        }

        private void Tdocumento_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(Tdocumento.Text))
            {
                //valor del documento fue modificado
                //reviso si documento existe en la base de datos
                int esta;
                esta = P.Consultas.ventas.CheckDocName(Tdocumento.Text);
                if (esta > 0)
                {
                    Lestado_doc.Text = "Documento ya ha sido utilizado";
                    Lestado_doc.ForeColor = Color.Red;
                }
                else
                {   
                    Lestado_doc.Text = "OK";
                    Lestado_doc.ForeColor = Color.Green;
                }   
            }
            else
            {
                if (Tdocumento.Text == string.Empty)
                {                    
                    Lestado_doc.Text = string.Empty;
                    Lestado_doc.ForeColor = this.ForeColor;
                }
            }
        }

        private void btn_add_new_Click(object sender, EventArgs e)
        {
            var box = new Busquedas.articulo();
            box.set_criterios(Busquedas.tipo_vigencia.ventas, Busquedas.tipo_estado.soloActivos);
            var respuesta = box.ShowDialogFrom(this);
            if (respuesta == DialogResult.OK)
            {
                //Valido que articulo no se encuentre agregado previamente en la factura
                bool existe = false;
                foreach (var oFila in detalle_venta.Objects)
                {
                    var fila = (P.Estructuras.DisplayVentaPrecio)oFila;
                    if (fila.CodArticulo == box.codigo && fila.Parte == box.parte)
                        existe = true;

                }
                if (existe)
                    P.Mensaje.Advert("Venta ya ha sido ingresada a la factura actual");
                else
                    AgregarDetalleFactura(box.full_data);
            }

        }

        private void AgregarDetalleFactura(DETAIL_PROCESS datos)
        {
            decimal costo_ext, cost_uni;
            costo_ext = 0;//colchon.Rows(0).Item("val_libro")
            cost_uni = costo_ext / datos.cantidad;
            //reviso si la fila pertene a una otra factura en el sistema
            int esta = P.Consultas.ventas.CheckArticlePartUsed(datos.cod_articulo, datos.parte);
            if (esta > 0)
            {
                //fila esta asociada al menos a una factura
                P.Mensaje.Advert("Venta ya ha sido ingresada a otra factura en el sistema");
            }
            else
            {
                //obtenemos valores de precios
                var caja = new Busquedas.PriceQuantitySetter(datos.cantidad);
                var res = caja.ShowDialogFrom(this);
                //fila puede ser procesada para esta factura
                var newfila = new P.Estructuras.DisplayVentaPrecio();
                newfila.rowIndex = datos.PartId;
                newfila.CodArticulo = datos.cod_articulo;
                newfila.Parte = datos.parte;
                newfila.DescripArt = datos.dscrp;
                newfila.Cantidad = datos.cantidad;
                newfila.CostoUnitario = cost_uni;
                newfila.CostoExtend = costo_ext;
                newfila.PrecioUnitario = caja.UnitPrice;
                newfila.PrecioTotal = caja.TotalPrice;
                newfila.Zona = datos.zona;
                newfila.Subzona = datos.subzona;
                newfila.Clase = datos.clase;
                newfila.UoE = caja.UoE;

                detalle_venta.SetObjects(AddFila(newfila));
                SetTotalFactura();
            }
        }

        private List<P.Estructuras.DisplayVentaPrecio> AddFila(P.Estructuras.DisplayVentaPrecio FilaNew)
        {
            var origen = (List<P.Estructuras.DisplayVentaPrecio>)detalle_venta.Objects;
            origen.Add(FilaNew);
            return origen;
        }

        private void detalle_venta_DoubleClick(object sender, EventArgs e)
        {
            var CurrentDet = (P.Estructuras.DisplayVentaPrecio)detalle_venta.SelectedObject;
            var origen = CurrentDetailSource;
            int indice = origen.IndexOf(CurrentDet);

            var box = new Busquedas.PriceQuantitySetter(CurrentDet.Cantidad);
            var result = box.ShowDialogFrom(this);
            if(result == DialogResult.OK)
                if (CurrentDet.PrecioTotal != box.TotalPrice || CurrentDet.PrecioUnitario != box.UnitPrice)
                {
                    CurrentDet.PrecioTotal = box.TotalPrice;
                    CurrentDet.PrecioUnitario = box.UnitPrice;
                    origen[indice] = CurrentDet;
                    detalle_venta.RefreshObjects(origen);
                    SetTotalFactura();
                }
        }

        private void SetTotalFactura()
        {
            _TotalAmount = CurrentDetailSource.Select(o => o.PrecioTotal).Sum();
            TprecioExt.Text = _TotalAmount.ToString("#,##0.0##");
        }

        private void detalle_venta_KeyUp(object sender, KeyEventArgs e)
        {
            var CurrentDet = (P.Estructuras.DisplayVentaPrecio)detalle_venta.SelectedObject;
            var origen = CurrentDetailSource;
            int fila = origen.IndexOf(CurrentDet);
            if (fila >= 0 && e.KeyCode == Keys.Delete)
            {
                var respuesta = P.Mensaje.Confirmar("¿Está seguro que desea eliminar la fila seleccionada?");
                if (respuesta == DialogResult.Yes)
                {
                    origen.RemoveAt(fila);
                    detalle_venta.SetObjects(origen);
                    SetTotalFactura();
                }
            }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            //validar que el formulario contenga toda la información para guardar
            if (Tdocumento.Text == "")
            {
                P.Mensaje.Advert("Debe indicar el número del documento");
                Tdocumento.Focus();
                return;
            }
            //volvemos a chequear que el documento no exista en la base de datos
            int existe_doc = P.Consultas.ventas.CheckDocName(Tdocumento.Text);
            if (existe_doc > 0 )
            {
                P.Mensaje.Advert("Número de documento ya ha sido utilizado");
                Tdocumento.Focus();
                return;
            }
            if (CurrentDetailSource.Count() == 0)
            {
                P.Mensaje.Advert("No ha ingresado información en el detalle de la factura");
                detalle_venta.Focus();
                return;
            }
            foreach(var cDet in CurrentDetailSource)
            {
                if (cDet.PrecioTotal == 0)
                {
                    int index = CurrentDetailSource.IndexOf(cDet);
                    P.Mensaje.Advert("Debe ingresar el precio para la fila " + (index + 1) + " del detalle");
                    detalle_venta.Focus();
                    return;
                }
            }
            //paso la validación, procedemos a guardar
            var res = P.Consultas.ventas.CREATE_SALES_DOC(Tdocumento.Text, _TotalAmount, DTfecha.Value, CurrentDetailSource);
            if (res.CheckError)
                res.mensaje();
            else
            {
                P.Mensaje.Info("Documento de venta ingresado correctamente al sistema");
                this.Close();
            }
        }

        private List<P.Estructuras.DisplayVentaPrecio> CurrentDetailSource
        {
            get {return (List<P.Estructuras.DisplayVentaPrecio>)detalle_venta.Objects;}
        }
    }
}
