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
    public partial class ingreso_financiero : UserControl
    {
        private bool _price_calculing;
        private ACode.Vperiodo _contabilizar;
        private ingreso _padre;
        private TabPage _page;
        private int _residuo;
        private List<P.Estructuras.DetalleOBC> _salidasOBC;

        public ingreso_financiero()
        {
            InitializeComponent();
        }

        private void ingreso_financiero_Load(object sender, EventArgs e)
        {
            _price_calculing = false;
            _padre = P.Auxiliar.FindPadre(this);
            _page = P.Auxiliar.FindPage(this);
            _residuo = 0;
            _salidasOBC = null;
        }

        public void load_data()
        {
            //cargar información en los controles que no varian
            //periodo contable
            var TPcontab = P.Consultas.periodo_contable.ingreso();
            _contabilizar = P.Consultas.periodo_contable.abierto();

            cbFecha_ing.Items.Clear();
            cbFecha_ing.Items.AddRange(P.Consultas.periodo_contable.ingreso().ToArray());
            cbFecha_ing.SelectedItem = _contabilizar;

            //fecha de compra
            var tempo = new ACode.Vperiodo(DateTime.Today.Year, DateTime.Today.Month);
            Tfecha_compra.Value = _contabilizar.first;
            Tfecha_compra.CustomFormat = "dd-MM-yyyy";
            Tfecha_compra.MaxDate = DateTime.Today;
            Tfecha_compra.MinDate = (tempo - 5).first;

            //zonas
            cboZona.Items.Clear();
            cboZona.Items.AddRange(P.Consultas.zonas.All().ToArray());
            cboZona.SelectedIndex = -1;

            //consistencia / tipo
            cboConsist.Items.Clear();
            cboConsist.Items.AddRange(P.Consultas.tipos.All().ToArray());
            try
            {
                cboConsist.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                P.Mensaje.Error(ex.Message);
                P.Mensaje.Error(ex.StackTrace);
            }

            //categoria
            cboCateg.Items.Clear();
            cboCateg.Items.AddRange(P.Consultas.categorias.All().ToArray());
            cboCateg.SelectedIndex = -1;

            //proveedor
            cboProveedor.Items.Clear();
            cboProveedor.Items.AddRange(P.Consultas.proveedores.listar().ToArray());
            cboProveedor.SelectedIndex = -1;

            //Gestion
            cboGestion.Items.Clear();
            cboGestion.Items.AddRange(P.Consultas.gestiones.All());
            cboGestion.SelectedIndex = -1;
        }

        public void limpiar()
        {
            Tdescrip.Text = string.Empty;
            cboZona.SelectedIndex = -1;          //'zonas ya se cargaron en el load del form
            cboConsist.SelectedIndex = 0;
            cboClase.SelectedIndex = -1;         //clases se carga desde tipo
            cboCateg.SelectedIndex = -1;         //categoria ya se cargaron en el load del form
            cboProveedor.SelectedIndex = -1;     //proveedor ya se cargaron en el load del form
            Tfecha_compra.Value = _contabilizar.first;
            ckDepre.Checked = true;
            cbFecha_ing.SelectedItem = _contabilizar;
            _residuo = 0;
            Tcantidad.Text = string.Empty;
            Tprecio_compra.Text = string.Empty;
            TvuF.Text = string.Empty;
            Tdoc.Text = string.Empty;
            derC1.Checked = true;
            ckIFRS.Checked = true;
        }

        public void cargar(ingreso.cod_situacion cual_sit, int origen_id)
        {
            habilitar_controles(cual_sit, origen_id);
        }

        public void cargarOBC(int valor_uni, int cantidad, int diferencia, List<P.Estructuras.DetalleOBC> salidasOBC)
        {
            habilitar_controles(ingreso.cod_situacion.nuevo , 2);
            this._salidasOBC = salidasOBC;
            this._residuo = diferencia;
            this.Tcantidad.Text = cantidad.ToString("#,##0");
            P.Auxiliar.ActivarF(this.Tcantidad, false);
            this.Tprecio_compra.Text = valor_uni.ToString("#,##0");
            P.Auxiliar.ActivarF(this.Tprecio_compra, false);
            P.Auxiliar.ActivarF(this.TxtPrecioTotal, false);
        }


        public void cargar(ingreso.cod_situacion cual_sit, int origen_id, SINGLE_DETAIL informacion, bool HayIFRS)
        {
            habilitar_controles(cual_sit, origen_id);
            completar_informacion(cual_sit, informacion, HayIFRS);
        }

        private void habilitar_controles(ingreso.cod_situacion cual_sit, int origen_id)
        {
            //primer select para habilitar lo que corresponda segun el estado
            switch (cual_sit)
            {
                case ingreso.cod_situacion.nuevo:
                    _page.Enabled = true;
                    P.Auxiliar.ActivarF(Tdescrip);
                    P.Auxiliar.ActivarF(cboZona);
                    P.Auxiliar.ActivarF(cboSubzona);
                    P.Auxiliar.ActivarF(cboConsist);
                    P.Auxiliar.ActivarF(cboClase);
                    P.Auxiliar.ActivarF(cboSubclase);
                    P.Auxiliar.ActivarF(cboCateg);
                    P.Auxiliar.ActivarF(cboGestion);

                    P.Auxiliar.ActivarF(cboProveedor);
                    P.Auxiliar.ActivarF(Tfecha_compra);
                    P.Auxiliar.ActivarF(cbFecha_ing);
                    P.Auxiliar.ActivarF(Tcantidad);
                    P.Auxiliar.ActivarF(Tprecio_compra);
                    P.Auxiliar.ActivarF(TvuF);
                    P.Auxiliar.ActivarF(Tdoc);
                    P.Auxiliar.ActivarF(Fderecho);
                    P.Auxiliar.ActivarF(derC1);
                    P.Auxiliar.ActivarF(derC2);
                    P.Auxiliar.ActivarF(ckIFRS);
                    btn_guardar.Image = global::AFN_WF_C.Properties.Resources._32_next;
                    btn_guardar.Text = "Guardar";
                    btn_elim.Visible = false;
                    btn_act.Visible = false;
                    break;
                case ingreso.cod_situacion.editable:
                    _page.Enabled = true;
                    P.Auxiliar.ActivarF(Tdescrip);
                    P.Auxiliar.ActivarF(cboZona);
                    P.Auxiliar.ActivarF(cboSubzona);
                    P.Auxiliar.ActivarF(cboConsist, false);      //implica en la Clase, que no puede ser modificada
                    P.Auxiliar.ActivarF(cboClase, false);        //implica en los codigos de producto
                    P.Auxiliar.ActivarF(cboSubclase);
                    P.Auxiliar.ActivarF(cboCateg);
                    P.Auxiliar.ActivarF(cboGestion);

                    P.Auxiliar.ActivarF(cboProveedor);
                    P.Auxiliar.ActivarF(Tfecha_compra, false);   //implica en los codigos de producto
                    P.Auxiliar.ActivarF(cbFecha_ing);
                    P.Auxiliar.ActivarF(Tcantidad, false);       //implica en los codigos de producto
                    P.Auxiliar.ActivarF(Tprecio_compra, (bool)(origen_id != 2));
                    P.Auxiliar.ActivarF(TvuF);
                    P.Auxiliar.ActivarF(Tdoc);
                    P.Auxiliar.ActivarF(Fderecho);
                    P.Auxiliar.ActivarF(derC1);
                    P.Auxiliar.ActivarF(derC2);
                    P.Auxiliar.ActivarF(ckIFRS);

                    btn_guardar.Image = global::AFN_WF_C.Properties.Resources._32_edit;
                    btn_guardar.Text = "Editar";
                    btn_elim.Visible = true;
                    btn_act.Visible = true;
                    break;
                case ingreso.cod_situacion.activo:
                    _page.Enabled = false;
                    //no necesito cargar ninguna cosa, pues no se ve verán
                    break;
            }
        }
        private void completar_informacion(ingreso.cod_situacion cual_sit, SINGLE_DETAIL informacion, bool HayIFRS)
        {
            //segundo select para completar con los valores que correspondan
            switch (cual_sit)
            {
                case ingreso.cod_situacion.editable:
                    //significa que se debe modificar
                    int hay;
                    //hay = colchon.Count();
                    hay = 1;

                    if (hay == 1)
                    {
                        //solo hay 1 registro para el codigo
                        var registro = informacion; // = colchon.Rows(0)
                        _residuo = 0;
                        Tdescrip.Text = registro.descripcion;
                        try
                        {
                            cboZona.SelectedItem = registro.zona;
                            try
                                {cboSubzona.SelectedItem = registro.subzona;}
                            catch //(Exception ex)
                            {
                                if (registro.zona.code != "330")
                                    cboSubzona.SelectedIndex = -1;
                                else
                                    cboSubzona.SelectedIndex = 0;
                            }
                        }
                        catch //(Exception ex)
                            {cboZona.SelectedIndex = -1;}
                        cboConsist.SelectedItem = registro.tipo;
                        cboClase.SelectedItem = registro.clase;
                        cboSubclase.SelectedItem = registro.subclase;
                        cboCateg.SelectedItem = registro.categoria;

                        cboGestion.SelectedItem = registro.gestion;
                        try
                            {cboProveedor.SelectedItem = registro.proveedor;}
                        catch //(Exception ex)
                            {cboProveedor.SelectedIndex = -1;}
                        Tfecha_compra.Value = registro.fecha_compra;
                        try
                        {
                            cbFecha_ing.SelectedItem = registro.fecha_ingreso;
                        }
                        catch //(Exception ex)
                        {
                            cbFecha_ing.SelectedIndex = -1;
                        }
                        Tcantidad.Text = registro.cantidad.ToString();
                        Tprecio_compra.Text = registro.precio_base.ToString("#,##0");
                        TvuF.Text = registro.vida_util_inicial.ToString();
                        Tdoc.Text = registro.num_doc;
                        if (registro.derecho_credito)
                            derC1.Checked = true;
                        else
                            derC2.Checked = true;
                        ckDepre.Checked = registro.se_deprecia;
                        ckIFRS.Checked = HayIFRS;
                    }
                    else if (hay == 2)
                    {
                        //revisar que los dos registros sean iguales, excepto el valor unitario y las cantidades
                        //                    Dim QLcantidad(1), QLvida_util(1), QLparte(1) As Integer
                        //                    Dim QLfecha_inicio(1) As Date
                        //                    Dim QLzona(1), QLestado(1), QLclase(1) As String
                        //                    Dim QLprecio(1) As Double
                        //                    Dim revisar As Boolean
                        //                    For i = 0 To 1
                        //                        QLparte(i) = colchon.Rows(i).Item("parte")
                        //                        QLfecha_inicio(i) = colchon.Rows(i).Item("fecha_inicio")
                        //                        QLzona(i) = colchon.Rows(i).Item("zona")
                        //                        QLestado(i) = colchon.Rows(i).Item("estado")
                        //                        QLcantidad(i) = colchon.Rows(i).Item("cantidad")
                        //                        QLprecio(i) = colchon.Rows(i).Item("precio_base")
                        //                        QLclase(i) = colchon.Rows(i).Item("clase")
                        //                        QLvida_util(i) = colchon.Rows(i).Item("vida_util_base")
                        //                    Next
                        //                    revisar = True
                        //                    If Not (QLparte(0) = 0 And QLparte(1) = 1) And revisar Then
                        //                        MsgBox("No corresponde a las partes necesarias", vbCritical, "NH FOODS CHILE")
                        //                        revisar = False
                        //                    End If      'partes solo deben ser 0 y 1
                        //                    If QLfecha_inicio(0) <> QLfecha_inicio(1) And revisar Then
                        //                        MsgBox("Fechas de inicio no son iguales", vbCritical, "NH FOODS CHILE")
                        //                        revisar = False
                        //                    End If      'fecha iguales
                        //                    If QLzona(0) <> QLzona(1) And revisar Then
                        //                        MsgBox("Zonas no son iguales", vbCritical, "NH FOODS CHILE")
                        //                        revisar = False
                        //                    End If      'zonas iguales
                        //                    If QLestado(0) <> QLestado(1) And revisar Then
                        //                        MsgBox("Estados no son iguales", vbCritical, "NH FOODS CHILE")
                        //                        revisar = False
                        //                    End If      'estados iguales
                        //                    If Not (Math.Abs(QLprecio(0) - QLprecio(1)) = 1) And revisar Then
                        //                        MsgBox("Precios no corresponden", vbCritical, "NH FOODS CHILE")
                        //                        revisar = False
                        //                    End If      'precios deben tener 1 peso de diferencia
                        //                    If QLclase(0) <> QLclase(1) And revisar Then
                        //                        MsgBox("Clases no son iguales", vbCritical, "NH FOODS CHILE")
                        //                        revisar = False
                        //                    End If      'clases iguales
                        //                    If QLvida_util(0) <> QLvida_util(1) And revisar Then
                        //                        MsgBox("Vida Utiles no son iguales", vbCritical, "NH FOODS CHILE")
                        //                        revisar = False
                        //                    End If      'vida util igual
                        //                    If revisar Then
                        //                        Dim registro As DataRow = colchon.Rows(0)
                        //                        residuo.Text = QLcantidad(1)
                        //                        Tdescrip.Text = registro("dscrp")
                        //                        Try
                        //                            zona = registro("zona")
                        //                            cboZona.SelectedIndex = cboZona.Items.IndexOf(zona)
                        //                            Try
                        //                                cboSubzona.SelectedValue = registro("subzona")
                        //                            Catch ex As Exception
                        //                                If zona <> "30" Then
                        //                                    cboSubzona.SelectedIndex = -1
                        //                                Else
                        //                                    cboSubzona.SelectedIndex = 0
                        //                                End If
                        //                            End Try
                        //                        Catch ex As Exception
                        //                            cboZona.SelectedIndex = -1
                        //                            cboSubzona.SelectedIndex = -1
                        //                        End Try
                        //                        cboConsist.SelectedValue = registro("tipo")
                        //                        cboClase.SelectedValue = registro("clase")
                        //                        cboSubclase.SelectedValue = registro("subclase")
                        //                        cboCateg.SelectedValue = registro("categoria")
                        //                        Try
                        //                            cboProveedor.SelectedValue = registro("proveedor")
                        //                        Catch ex As Exception
                        //                            cboProveedor.SelectedIndex = -1
                        //                        End Try
                        //                        Tfecha_compra.Value = registro("fecha_compra")
                        //                        Try
                        //                            cbFecha_ing.SelectedValue = registro("fecha_ing")
                        //                        Catch ex As Exception
                        //                            cbFecha_ing.SelectedIndex = -1
                        //                        End Try
                        //                        Tcantidad.Text = QLcantidad(0) + QLcantidad(1)
                        //                        Tprecio_compra.Text = Format(registro("precio_base"), "#,##0")
                        //                        TvuF.Text = registro("vida_util_inicial")
                        //                        Tdoc.Text = registro("num_doc")
                        //                        If registro("derecho_credito") = "SI" Then
                        //                            derC1.Checked = True
                        //                        Else
                        //                            derC2.Checked = True
                        //                        End If
                        //                        ckDepre.Checked = registro("se_deprecia")
                        //                        valor_ifrs = base.DETALLE_IFRS_CLP(artic.Text).Rows.Count
                        //                        If valor_ifrs = 0 Then
                        //                            ckIFRS.Checked = False
                        //                            'CkModIFRS.Value = 0
                        //                            paso2.Enabled = False
                        //                            'Call cargar_DxG()
                        //                            'pasos.SelectedTab = paso1
                        //                        Else
                        //                            ckIFRS.Checked = True
                        //                            'CkModIFRS.Value = 1
                        //                            paso2.Enabled = True
                        //                            'Call cargar_ifrs()
                        //                            'pasos.SelectedTab = paso1
                        //                        End If
                        //                    End If
                    }
                    else
                    {
                        P.Mensaje.Error("La cantidad de registros no corresponde con el proceso");
                    }
                    break;
                case ingreso.cod_situacion.nuevo:
                    limpiar();
                    break;
                case ingreso.cod_situacion.activo:
                    //la hoja no esta activa, por lo que no es necesario hacerle nada
                    break;
            }
        }

        #region actualizacion en cascada de combos
        private void cboZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSubzona.Items.Clear();
            if(cboZona.SelectedIndex != -1)
            {
                var zona = (GENERIC_VALUE) cboZona.SelectedItem;
                var Tsubzona = P.Consultas.subzonas.ByZone(zona);
                cboSubzona.Items.AddRange(Tsubzona.ToArray());
                
                if(Tsubzona.Count() == 1)
                    cboSubzona.SelectedIndex = 0;
                else
                    cboSubzona.SelectedIndex = -1;

                cboSubzona.MaxDropDownItems = 7;
                cboSubzona.DropDownHeight = cboSubzona.ItemHeight * 7 + 2;
                
            }
            else
            {
                cboSubzona.MaxDropDownItems = 1;
                cboSubzona.DropDownHeight = (cboSubzona.ItemHeight * 1) + 2;
            }
        }
        private void cboConsist_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboClase.Items.Clear();
            if (cboConsist.SelectedIndex != -1){
                //clase segun tipo
                var item = (GENERIC_VALUE)cboConsist.SelectedItem;
                var Tclase = P.Consultas.clases.ByType(item, true);
                cboClase.Items.AddRange(Tclase.ToArray());
                cboClase.SelectedIndex = -1;
                    
            }
        }
        private void cboClase_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboSubclase.Items.Clear();
            if(cboClase.SelectedIndex != -1)
            {
                var clase = (GENERIC_VALUE)(cboClase.SelectedItem);
                var Tsubclase = P.Consultas.subclases.ByKind(clase);
                cboSubclase.Items.AddRange(Tsubclase.ToArray());
                cboSubclase.SelectedIndex = -1;
                cboSubclase.MaxDropDownItems = 7;
                cboSubclase.DropDownHeight = cboSubclase.ItemHeight * 7 + 2;
            }
            else
            {
                cboSubclase.MaxDropDownItems = 1;
                cboSubclase.DropDownHeight = cboClase.ItemHeight * 1 + 2;
            }
        }
        private void cboSubclase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboSubclase.SelectedIndex != -1)
            {
                var subclase = (GENERIC_VALUE)cboSubclase.SelectedItem;
                TvuF.Text = P.Consultas.subclases.ByGeneric(subclase).vu_sug.ToString();
            }
            else
            {
                TvuF.Text = string.Empty;
            }
        }

        private void Tcantidad_Leave(object sender, EventArgs e)
        {
            if(Tcantidad.Text != string.Empty)
            {
                int added;
                if (!int.TryParse(Tcantidad.Text, out added))
                {
                    P.Mensaje.Advert("Solo puede ingresar números en la cantidad");
                    Tcantidad.Focus();
                    Tcantidad.Text = string.Empty;
                }
                else{
                    Tcantidad.Text = added.ToString("#");
                }
            }
        }
        private void Tprecio_compra_Enter(object sender, EventArgs e)
        {
            String procesar;
            procesar = Tprecio_compra.Text;
            procesar = procesar.Replace( P.Auxiliar.getSeparadorMil , "");
            int valor;
            int.TryParse(procesar,out valor);
            Tprecio_compra.Text = valor.ToString("#");
        }
        private void Tprecio_compra_Leave(object sender, EventArgs e)
        {
            if(Tprecio_compra.Text != string.Empty){
                string procesar, sepLi;
                int valor;
                sepLi = P.Auxiliar.getSeparadorMil;
                procesar = Tprecio_compra.Text;
                procesar = procesar.Replace(sepLi, "");
                
                if(! int.TryParse(procesar,out valor) ){
                    P.Mensaje.Advert("Solo puede ingresar números en la cantidad");
                    Tprecio_compra.Focus();
                    Tprecio_compra.Text = string.Empty;
                }
                else{
                    Tprecio_compra.Text = valor.ToString("#,##0");
                }
            }   
        }
        private void PrecioOrCantidad_TextChanged(object sender, EventArgs e)
        {
            var txtMod = (TextBox)sender;
            if (!_price_calculing)
            {
                _price_calculing = true;
                if (!string.IsNullOrEmpty(Tcantidad.Text) && !string.IsNullOrEmpty(Tprecio_compra.Text) && txtMod.Name != "TxtPrecioTotal")
                {
                    string sepList;
                    sepList = P.Auxiliar.getSeparadorMil;
                    int Qty, Pr, Res;
                    int.TryParse(Tcantidad.Text.Replace(sepList, ""), out Qty);
                    //Q = Val(Strings.Replace(Tcantidad.Text, sepList, ""));
                    int.TryParse(Tprecio_compra.Text.Replace(sepList, ""), out Pr);
                    //P = Val(Strings.Replace(Tprecio_compra.Text, sepList, ""));
                    Res = _residuo;
                    //R = Val(residuo.Text);
                    TxtPrecioTotal.Text = (Qty * Pr + Res).ToString("#,##0");
                }
                else if (!string.IsNullOrEmpty(Tcantidad.Text) && !string.IsNullOrEmpty(TxtPrecioTotal.Text) && txtMod.Name == "TxtPrecioTotal")
                {
                    string sepList;
                    sepList = P.Auxiliar.getSeparadorMil;
                    int Qty, Pr, TotalCalc;
                    int UnitPrice;
                    //Q = Val(Strings.Replace(Tcantidad.Text, sepList, ""))
                    int.TryParse(Tcantidad.Text.Replace(sepList, ""), out Qty);
                    //P = Val(Strings.Replace(TxtPrecioTotal.Text, sepList, ""))
                    int.TryParse(TxtPrecioTotal.Text.Replace(sepList, ""), out Pr);
                    //R = Val(residuo.Text)
                    //Res = _residuo;
                    UnitPrice = Pr / Qty;
                    TotalCalc = UnitPrice * Qty;
                    Tprecio_compra.Text = UnitPrice.ToString("#,##0");
                    if (TotalCalc != Pr)
                    {
                        _residuo = Math.Abs(TotalCalc - Pr);
                    }
                }
                else
                {
                    TxtPrecioTotal.Text = string.Empty;
                }
                _price_calculing = false;
            }
        }
        private void Tfecha_compra_ValueChanged(object sender, EventArgs e)
        {
            if(Tfecha_compra.Value.Month == 1 && Tfecha_compra.Value.Day == 1){
                Tfecha_compra.Value = new DateTime(Tfecha_compra.Value.Year, 1, 2);
            }
        }

        #endregion


        #region Botones

        private void btn_elim_Click(Object sender,EventArgs e)// Handles btn_elim.Click
        {
            var toma = P.Mensaje.Confirmar("¿Está seguro que desea eliminar este registro?");
            if(toma == DialogResult.Yes){
                var res = P.Consultas.lotes.BORRAR_AF(_padre.codigo_artic);
                if (res.codigo == 1)
                {
                    P.Mensaje.Info("Se ha eliminado el registro correctamente");
                    _padre.iniciar_formulario();
                }
                else
                {
                    P.Mensaje.Error(res.descripcion);
                }
            }
        }
        private void btn_act_Click(Object sender,EventArgs e)// Handles btn_act.Click
        {
            var toma = P.Mensaje.Confirmar("¿Está seguro que desea activar este registro? (ya no podrá ser modificado)");
            if(toma == DialogResult.Yes){
                var res = P.Consultas.lotes.ACTIVAR_AF(_padre.codigo_artic);
                if (res.codigo == 1)
                {
                    P.Mensaje.Info("Se ha activado el registro correctamente");
                    _padre.iniciar_formulario();
                }
                else
                {
                    P.Mensaje.Error(res.descripcion);
                }
            }
        }
        private void btn_Bprov_Click(Object sender,EventArgs e)// Handles btn_Bprov.Click
        {
            var box = new Vistas.Busquedas.proveedor();
            var seleccion = box.ShowDialogFrom(_padre);
            if (seleccion == DialogResult.OK)
            {
                cboProveedor.SelectedItem = box.codigo;
            }
        }
        private void btn_guardar_Click(Object sender,EventArgs e)// Handles btn_guardar.Click
        {
            if (!validar_campos())
                return;
            ////simplicación de variables
            string documento, proveedor, usuario;
            int vutil;
            GENERIC_VALUE CtiPo = (GENERIC_VALUE)cboConsist.SelectedItem;
            string descrip = Tdescrip.Text;
            DateTime fcompra = Tfecha_compra.Value;
            decimal pcompra = decimal.Parse(Tprecio_compra.Text);
            int cantidad = int.Parse(Tcantidad.Text);
            decimal total_compra = decimal.Parse(TxtPrecioTotal.Text);
            int.TryParse(TvuF.Text,out vutil);
            GENERIC_VALUE zona = (GENERIC_VALUE)cboZona.SelectedItem;
            GENERIC_VALUE clase = (GENERIC_VALUE)cboClase.SelectedItem;
            GENERIC_VALUE categoria = (GENERIC_VALUE)cboCateg.SelectedItem;
            GENERIC_VALUE subzona = (GENERIC_VALUE)cboSubzona.SelectedItem;
            GENERIC_VALUE subclase = (GENERIC_VALUE)cboSubclase.SelectedItem;
            GENERIC_VALUE gestion = (GENERIC_VALUE)cboGestion.SelectedItem;
            usuario = P.Auxiliar.getUser();
            if (Tdoc.Text == "")
                documento = P.Consultas.documentos.defaultDocument;
            else
                documento = Tdoc.Text;

            if (cboProveedor.SelectedIndex == -1)
                proveedor = P.Consultas.documentos.defaultProveed;
            else
                proveedor = ((SV_PROVEEDOR)(cboProveedor.SelectedItem)).COD;

            bool derecho = derC1.Checked;
            int origen = _padre.fuente;

            DateTime fecha_contab = ((ACode.Vperiodo)(cbFecha_ing.SelectedItem)).last;
            bool depreciar = ckDepre.Checked;
            //fin reduccion de variables
            //Preparo valores en moneda original y yenes
            decimal valor_unitario = Math.Floor(total_compra / cantidad);
            decimal total_yen, unitario_yen;
            decimal tc = P.Consultas.tipo_cambio.YEN(fcompra);
            int residuo = (int)(total_compra - (valor_unitario * cantidad));

            RespuestaAccion mRS;
            string mensaje_final = string.Empty;
            int BatchId = 0;
            if (_padre.cual_sit == ingreso.cod_situacion.nuevo)
            {
                //ingreso en lote_articulos
                mRS = P.Consultas.lotes.INGRESO_LOTE(descrip, fcompra, proveedor, documento, total_compra, vutil, derecho, fecha_contab, origen, CtiPo);
                if (mRS.CheckError)
                {
                    //se produjo un error en el insert, se debe avisar
                    P.Mensaje.Error("Se ha producido un error al momento de guardar el lote");
                    return;
                }
                BatchId = mRS.result_objs.First().id;
                _padre.SetNewLote(BatchId);

                //checkear origen del ingreso para hacer match con obra en construccion
                if (origen == 2)//OBC
                {    
                    if (_salidasOBC != null)
                    {
                        foreach (var fila in _salidasOBC)
                        {
                            mRS = P.Consultas.obc.EGRESO_OBC(fila.codigo, BatchId, fila.saldo, zona.id);
                        }
                        _padre.ChangeOBCParent();
                    }
                }
                
                //Preparo valores para yen
                if (origen == 2)  //OBC
                    total_yen = P.Consultas.obc.TotalYen(BatchId);
                else
                    total_yen = Math.Round(((total_compra / cantidad) / tc), 3) * cantidad;
                unitario_yen = total_yen / cantidad;

                //determino partes necesarias para el batch
                mRS = P.Consultas.partes.REGISTER_PURCHASE(BatchId, fcompra, total_compra, cantidad);
                if (mRS.CheckError)
                {
                    mRS.mensaje();
                    return;
                }
                List<GENERIC_VALUE> partes = mRS.result_objs;
                //ingreso cabecera de la transaccion
                mRS = P.Consultas.trx_cabeceras.REGISTER_PURCHASE_HEAD(partes, fcompra, zona, subzona, clase, subclase, categoria, gestion, usuario);
                if (mRS.CheckError)
                {
                    mRS.mensaje();
                    return;
                }
                
                List<GENERIC_VALUE> cabeceras = mRS.result_objs;
                var AllowSystems = P.Consultas.sistema.AllWithIFRS(ckIFRS.Checked);
                foreach (SV_SYSTEM curSystem in AllowSystems)
                {
                    mRS = P.Consultas.trx_detalles.REGISTER_PURCHASE_DETAIL(cabeceras, curSystem, depreciar, derecho);
                    if (mRS.CheckError)
                    {
                        mRS.mensaje();
                        return;
                    }
                    int[] cab_ids = cabeceras.Select(c => c.id).ToArray();
                    decimal valor;
                    bool isYen = (curSystem.CURRENCY == "YEN");
                    if (!isYen)
                        valor = valor_unitario;
                    else
                        valor = unitario_yen;
                    mRS = registrar_parametros(curSystem, cab_ids, valor, derecho, vutil, clase, isYen);
                    if (mRS.CheckError)
                    {
                        mRS.mensaje();
                        return;
                    }
                }
                
                //homologar con los códigos de inventario
                mRS = P.Consultas.inventario.GENERAR_CODIGO(BatchId, clase);
                if (mRS.CheckError)
                {
                    mRS.mensaje();
                    return;
                }
                mensaje_final = "Registro de articulo ingresado correctamente al Activo Fijo";
                _padre.cual_sit = ingreso.cod_situacion.editable;
                habilitar_controles(_padre.cual_sit, BatchId);
            }
            else
            {
                //case when _padre.cual_sit != nuevo
                BatchId = _padre.codigo_artic;

                //Preparo valores para yen
                if (_padre.fuente == 2)  //OBC
                    total_yen = P.Consultas.obc.TotalYen(BatchId);
                else
                    total_yen = Math.Round(((total_compra / cantidad) / tc), 3) * cantidad;
                unitario_yen = total_yen / cantidad;

                //modificacion en lote_articulos 
                //(ya que si esta activo, esta pestaña nunca esta disponible)
                //derecho excluido de esta parte
                mRS = P.Consultas.lotes.MODIFICA_LOTE(BatchId, descrip, proveedor, documento, total_compra, vutil, fecha_contab);
                if (mRS.CheckError) { mRS.mensaje(); return; }
                
                //origen no cambia, asi que no se requiere este segmento de codigo
                //partes no cambian
                var partes = P.Consultas.partes.ByLote(BatchId);
                //modifico cabecera de la transaccion
                mRS = P.Consultas.trx_cabeceras.MODIF_PURCHASE_HEAD(partes, zona, subzona, subclase, categoria, gestion, usuario);
                if (mRS.CheckError) { mRS.mensaje(); return; }

                //trabajo con valores de parametros
                int[] cab_ids = mRS.result_objs.Select(c => c.id).ToArray();
                var AllowSystems = P.Consultas.sistema.All();
                foreach (SV_SYSTEM curSystem in AllowSystems)
                {
                    var details = P.Consultas.trx_detalles.GetByHeadsSystem(cab_ids, curSystem);
                    bool existe_det = !(details == null || details.Count == 0);
                    decimal valor;
                    bool isYen = (curSystem.CURRENCY == "YEN");
                    if (!isYen)
                        valor = valor_unitario;
                    else
                        valor = unitario_yen;
                    //Modificamos cabeceras que correspondan
                    if (existe_det)
                    {
                        mRS = P.Consultas.trx_detalles.MODIF_PURCHASE_DETAIL(cab_ids, curSystem, depreciar, derecho);
                        if (mRS.CheckError) { mRS.mensaje(); return; }
                    }

                    if (curSystem.ENVIORMENT == "IFRS" && !(ckIFRS.Checked))
                    {
                        if (existe_det)
                        {
                            //TODO: Eliminar datos de IFRS
                        }
                        //else
                        //    //si no esta marcado y no esta creado, no es necesario hacer algo
                    }
                    else
                    {
                        RespuestaAccion reg;
                        if (existe_det)
                        {
                            //Modificar datos
                            reg = modificar_parametros(curSystem, cab_ids, valor, derecho, vutil, clase, isYen);
                        }
                        else
                        {
                            //Ingresar nuevos datos
                            reg = registrar_parametros(curSystem, cab_ids, valor,derecho,vutil,clase,isYen);
                        }
                        reg.mensaje();
                    }
                }

                //no se vuelve a homologar con los códigos de inventario
                //Situación se mantiene (editable)
                //_padre.cual_sit = ingreso.cod_situacion.editable;
                mensaje_final = "Registro de articulo modificado correctamente al Activo Fijo";
            }

            _padre.cargar_otras_pestañas_fromBasic(tc);
            P.Mensaje.Info(mensaje_final);
        }

        private RespuestaAccion registrar_parametros(SV_SYSTEM curSystem, int[] cabeceras, decimal valor_unitario, bool derecho, int vutil, GENERIC_VALUE clase, bool isYen)
        {
            RespuestaAccion mRS;
            decimal valor_proceso;
            //Ingreso Precio Unitario
            SV_PARAMETER pb = P.Consultas.parametros.PrecioBase;
            mRS = P.Consultas.detalle_parametros.REGISTER_PURCHASE_PARAM(cabeceras, curSystem, pb, valor_unitario, !isYen);
            if (mRS.CheckError) return mRS;
            //Ingreso Credito
            if (derecho)
            {
                SV_PARAMETER cred = P.Consultas.parametros.Credito;
                valor_proceso = curSystem.ENVIORMENT.credit_rate * -valor_unitario;
                mRS = P.Consultas.detalle_parametros.REGISTER_PURCHASE_PARAM(cabeceras, curSystem, cred, valor_proceso);
                if (mRS.CheckError) return mRS;
            }
            //Ingreso Vida Util     //default monthly
            SV_PARAMETER vu = P.Consultas.parametros.VidaUtil;
            if (curSystem.ENVIORMENT.depreciation_rate == "monthly")
                valor_proceso = vutil;
            else
                valor_proceso = vutil / 12 * 365;
            mRS = P.Consultas.detalle_parametros.REGISTER_PURCHASE_PARAM(cabeceras, curSystem, vu, valor_proceso);
            if (mRS.CheckError) return mRS;
            //Valor Residual
            SV_PARAMETER vr = P.Consultas.parametros.ValorResidual;
            if (curSystem.ENVIORMENT == "IFRS")
            {
                decimal porc = P.Consultas.predeter_ifrs.porcentaje_valor_residual(clase);
                valor_proceso = valor_unitario * (-porc) ;
            }
            else
                valor_proceso = -1;

            mRS = P.Consultas.detalle_parametros.REGISTER_PURCHASE_PARAM(cabeceras, curSystem, vr, valor_proceso);
            return mRS;
        }
        private RespuestaAccion modificar_parametros(SV_SYSTEM curSystem, int[] cabeceras, decimal valor_unitario, bool derecho, int vutil, GENERIC_VALUE clase, bool isYen)
        {
            RespuestaAccion mRS;
            decimal ValueToWork;
            //Modifico Precio Unitario
            SV_PARAMETER pb = P.Consultas.parametros.PrecioBase;
            mRS = P.Consultas.detalle_parametros.MODIF_PURCHASE_PARAM(cabeceras, curSystem, pb, valor_unitario, !isYen);
            if (mRS.CheckError) return mRS;
            //Ingreso Credito
            if (derecho)
            {
                SV_PARAMETER cred = P.Consultas.parametros.Credito;
                ValueToWork = curSystem.ENVIORMENT.credit_rate * -valor_unitario;
                mRS = P.Consultas.detalle_parametros.MODIF_PURCHASE_PARAM(cabeceras, curSystem, cred, ValueToWork);
                if (mRS.CheckError) return mRS;
            }
            //Ingreso Vida Util     //default monthly
            SV_PARAMETER vu = P.Consultas.parametros.VidaUtil;
            if (curSystem.ENVIORMENT.depreciation_rate == "monthly")
                ValueToWork = vutil;
            else
                ValueToWork = vutil / 12 * 365;
            mRS = P.Consultas.detalle_parametros.MODIF_PURCHASE_PARAM(cabeceras, curSystem, vu, ValueToWork);
            if (mRS.CheckError) return mRS;
            //Valor Residual
            SV_PARAMETER vr = P.Consultas.parametros.ValorResidual;
            if (curSystem.ENVIORMENT == "IFRS")
            {
                decimal porc = P.Consultas.predeter_ifrs.porcentaje_valor_residual(clase);
                ValueToWork = valor_unitario * -porc;
            }
            else
                ValueToWork = -1;

            mRS = P.Consultas.detalle_parametros.MODIF_PURCHASE_PARAM(cabeceras, curSystem, vr, ValueToWork);
            return mRS;
        }

        private bool validar_campos()
        {
            //validar información ingresada
            if (Tdescrip.Text.Trim() == string.Empty)
            {
                P.Mensaje.Advert("Debe indicar la descripción del Activo Fijo");   //, MessageBoxButtons.OK
                Tdescrip.Focus();
                return false;
            }
            if (cboZona.SelectedIndex == -1)
            {
                P.Mensaje.Advert("Debe indicar la zona del Activo Fijo");
                cboZona.Focus();
                return false;
            }
            if (cboSubzona.SelectedIndex == -1)
            {
                P.Mensaje.Advert("Debe indicar una subzona para el Activo Fijo");
                cboSubzona.Focus();
                return false;
            }
            if (cboClase.SelectedIndex == -1)
            {
                P.Mensaje.Advert("Debe indicar la clase del Activo Fijo");
                cboClase.Focus();
                return false;
            }
            if (cboSubclase.SelectedIndex == -1)
            {
                P.Mensaje.Advert("Debe indicar una subclase para el Activo Fijo");
                cboSubclase.Focus();
                return false;
            }
            if (cboCateg.SelectedIndex == -1)
            {
                P.Mensaje.Advert("Debe indicar el proveedor del Activo Fijo");
                cboCateg.Focus();
                return false;
            }
            if (cboGestion.SelectedIndex == -1)
            {
                P.Mensaje.Advert("Debe indicar la gestion del Activo Fijo");
                cboGestion.Focus();
                return false;
            }
            if (Tcantidad.Text == string.Empty)
            {
                P.Mensaje.Advert("Debe indicar la cantidad de artículos");
                Tcantidad.Focus();
                return false;
            }
            if (Tprecio_compra.Text == string.Empty)
            {
                P.Mensaje.Advert("Debe indicar el precio de adquisición del Activo Fijo");
                Tprecio_compra.Focus();
                return false;
            }
            if (TvuF.Text == string.Empty)
            {
                P.Mensaje.Advert("Debe indicar la vida útil del artículo");
                TvuF.Focus();
                return false;
            }
            if (Tdoc.Text == string.Empty)
            {
                DialogResult eleccion = P.Mensaje.Confirmar("Desea continuar sin indicar el Nº de documento del Activo Fijo");
                if (eleccion != DialogResult.Yes)
                {  //no marco SI
                    Tdoc.Focus();
                    return false;
                }
            }
            if (cbFecha_ing.SelectedIndex == -1)
            {
                P.Mensaje.Advert("Debe indicar el periodo contable del Activo Fijo");
                cbFecha_ing.Focus();
                return false;
            }
            //fin validación
            return true;
        }
        #endregion

    }
}
