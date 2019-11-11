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
            //int indice = -1;
            //for (int i = 0; i < TPcontab.Count; i++)
            //{
            //    cbFecha_ing.Items.Add(TPcontab.ElementAt(i));
            //    if (TPcontab.ElementAt(i).last == contabilizar.last)
            //        indice = i;
            //}
            //cbFecha_ing.SelectedIndex = indice;

            //fecha de compra
            var tempo = new ACode.Vperiodo(DateTime.Today.Year, DateTime.Today.Month);
            Tfecha_compra.Value = P.Consultas.periodo_contable.abierto().first;
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
            //'periodo contable
            cbFecha_ing.SelectedItem = _contabilizar;
            //int i = 0;
            //foreach(ACode.Vperiodo dato in cbFecha_ing.Items){
            //    if(dato.last == _contabilizar.last)
            //        cbFecha_ing.SelectedIndex = i;
            //    i = i + 1;
            //}
            _residuo = 0;
            Tcantidad.Text = string.Empty;
            Tprecio_compra.Text = string.Empty;
            TvuF.Text = string.Empty;
            Tdoc.Text = string.Empty;
            derC1.Checked = true;
            ckIFRS.Checked = false;
        }

        public void cargar(ingreso.cod_situacion cual_sit, string fuente)
        {
            habilitar_controles(cual_sit, fuente);
        }

        public void cargar(ingreso.cod_situacion cual_sit, string fuente, SINGLE_DETAIL informacion, bool HayIFRS)
        {
            habilitar_controles(cual_sit, fuente);
            completar_informacion(cual_sit, informacion, HayIFRS);
        }

        private void habilitar_controles(ingreso.cod_situacion cual_sit, string fuente)
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
                    P.Auxiliar.ActivarF(Tprecio_compra, (bool)(fuente != "OBC"));
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
                            //cboZona.SelectedIndex = cboZona.Items.IndexOf(registro.zona);
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
                        //Seleccionar Gestion
                        cboGestion.SelectedItem = registro.gestion;
                        //for (int i = 0; i < cboGestion.Items.Count - 1; i++)
                        //{
                        //    var item = (GENERIC_VALUE)cboGestion.Items[i];
                        //    if (item.id == registro.gestion.id)
                        //        cboGestion.SelectedIndex = i;
                        //}
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
                        //MsgBox("La cantidad de registros no corresponde con el proceso", vbCritical, "NH FOODS CHILE")
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

        private void btn_elim_Click(System.Object sender,System.EventArgs e)// Handles btn_elim.Click
        {
            var toma = P.Mensaje.Confirmar("¿Está seguro que desea eliminar este registro?");
            if(toma == DialogResult.Yes){
                var res = P.Consultas.cabeceras.BORRAR_AF(_padre.codigo_artic);
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
        private void btn_act_Click(System.Object sender,System.EventArgs e)// Handles btn_act.Click
        {
            var toma = P.Mensaje.Confirmar("¿Está seguro que desea activar este registro? (ya no podrá ser modificado)");
            if(toma == DialogResult.Yes){
                var res = P.Consultas.cabeceras.ACTIVAR_AF(_padre.codigo_artic);
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
        private void btn_Bprov_Click(System.Object sender,System.EventArgs e)// Handles btn_Bprov.Click
        {
            var box = new Vistas.Busquedas.proveedor();
            var seleccion = box.DialogFrom(_padre);
            if (seleccion == DialogResult.OK)
            {
                cboProveedor.SelectedItem = box.codigo;
            }
            //bus_prov.Show();
            //bus_prov.actualizar_origen("Mod", this, Me.cboProveedor);
        }
        private void btn_guardar_Click(System.Object sender,System.EventArgs e)// Handles btn_guardar.Click
        {
            ////validar información ingresada
            //if( Tdescrip.Text.Trim() == string.Empty){
            //    MessageBox.Show("Debe indicar la descripción del Activo Fijo", "NH FOODS CHILE", MessageBoxIcon.Exclamation);   //, MessageBoxButtons.OK
            //    Tdescrip.Focus();
            //    return;
            //}
            //if (cboZona.SelectedIndex == -1){
            //    MessageBox.Show("Debe indicar la zona del Activo Fijo", "NH FOODS CHILE", MessageBoxIcon.Exclamation);
            //    cboZona.Focus();
            //    return;
            //}
            //if(cboSubzona.SelectedIndex == -1){
            //    MessageBox.Show("Debe indicar una subzona para el Activo Fijo",  "NH FOODS CHILE", MessageBoxIcon.Exclamation);
            //    cboSubzona.Focus();
            //    return;
            //}
            //if( cboClase.SelectedIndex == -1){
            //    MessageBox.Show("Debe indicar la clase del Activo Fijo", "NH FOODS CHILE", MessageBoxIcon.Exclamation);
            //    cboClase.Focus();
            //    return;
            //}
            //if(cboSubclase.SelectedIndex == -1){
            //    MessageBox.Show("Debe indicar una subclase para el Activo Fijo", "NH FOODS CHILE", MessageBoxIcon.Exclamation);
            //    cboSubclase.Focus();
            //    return;
            //}
            //if(cboCateg.SelectedIndex == -1) {
            //    MessageBox.Show("Debe indicar el proveedor del Activo Fijo", "NH FOODS CHILE", MessageBoxIcon.Exclamation);
            //    cboCateg.Focus();
            //    return;
            //}
            //if( cboGestion.SelectedIndex == -1) {
            //    MessageBox.Show("Debe indicar la gestion del Activo Fijo", "NH FOODS CHILE", MessageBoxIcon.Exclamation);
            //    cboGestion.Focus();
            //    return;
            //}
            //if( Tcantidad.Text == string.Empty){
            //    MessageBox.Show("Debe indicar la cantidad de artículos", "NH FOODS CHILE", MessageBoxIcon.Exclamation);
            //    Tcantidad.Focus();
            //    return;
            //}
            //if( Tprecio_compra.Text == string.Empty){
            //    MessageBox.Show("Debe indicar el precio de adquisición del Activo Fijo", "NH FOODS CHILE", MessageBoxIcon.Exclamation);
            //    Tprecio_compra.Focus();
            //    return;
            //}
            //if(TvuF.Text == string.Empty){
            //    MessageBox.Show("Debe indicar la vida útil del artículo", "NH FOODS CHILE", MessageBoxIcon.Exclamation);
            //    TvuF.Focus();
            //    return;
            //}
            //if( Tdoc.Text == string.Empty){
            //    DialogResult eleccion;
            //    eleccion = MessageBox.Show("Desea continuar sin indicar el Nº de documento del Activo Fijo", "NH FOODS CHILE", MessageBoxButtons.YesNo);
            //    if( eleccion != DialogResult.Yes){  //no marco SI
            //        Tdoc.Focus();
            //        return;
            //    }
            //}
            //if (cbFecha_ing.SelectedIndex == -1) {
            //    MessageBox.Show("Debe indicar el periodo contable del Activo Fijo", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    cbFecha_ing.Focus();
            //    return;
            //}
            ////fin validación
            ////simplicación de variables
            //string documento, derecho, origen, descrip, proveedor, pcompra, vutil, cantidad,  
            //usuario, total_compra, CtiPo;
            //bool depreciar;
            ////Dim sGestion As gestion.fila
            //GENERIC_VALUE zona,clase,categoria,  subzona, subclase,sGestion;
            //DateTime fcompra, fecha_contab;
            //CtiPo = cboConsist.Text;
            //descrip = Tdescrip.Text;
            //fcompra = Tfecha_compra.Value;
            //pcompra = string.Format(Tprecio_compra.Text, "General Number");
            //cantidad = Tcantidad.Text;
            //total_compra = string.Format(TxtPrecioTotal.Text, "General Number");
            //vutil = TvuF.Text;
            //zona = (GENERIC_VALUE)cboZona.SelectedItem;
            //clase = (GENERIC_VALUE)cboClase.SelectedItem;
            //categoria = (GENERIC_VALUE)cboCateg.SelectedItem;
            //subzona = (GENERIC_VALUE)cboSubzona.SelectedItem;
            //subclase = (GENERIC_VALUE)cboSubclase.SelectedItem;
            //sGestion = (GENERIC_VALUE)cboGestion.SelectedItem;
            //usuario = ""//form_welcome.GetUsuario;
            //if (Tdoc.Text == ""){
            //    documento = "SIN_DOCUMENTO";
            //}else{
            //    documento = Tdoc.Text;
            //}
            //if(cboProveedor.SelectedIndex == -1) {
            //    proveedor = "SIN_PROVEED";
            //}else{
            //    proveedor = ((GENERIC_VALUE)(cboProveedor.SelectedItem)).code;
            //}
            //if(derC1.Checked)
            //    derecho = "SI";
            //else
            //    derecho = "NO";
            //origen = ""//fuente.Text;
            //fecha_contab = ((ACode.Vperiodo)(cbFecha_ing.SelectedItem)).last;
            //depreciar = ckDepre.Checked;
            ////fin reduccion de variables
            //DataRow mRS;
            //string mensaje_final;
            //if(cual_sit ==  cod_situacion.nuevo){
            //    //ingreso en lote_articulos
            //    mRS = base.INGRESO_LOTE(descrip, fcompra, proveedor, documento, total_compra, vutil, derecho, fecha_contab, origen, CtiPo)
            //    if(mRS["cod_sit"] == -1) {
            //        //se produjo un error en el insert, se debe avisar
            //        MessageBox.Show("Se ha producido un error al momento de guardar el lote", "NH FOODS CHILE",MessageBoxIcon.Error);
            //        return;
            //    }
            //    string codigo = (string) mRS["codigo"];
            //    artic.Text = codigo;
            //    mRS = null;
            //    //checkear origen del ingreso para hacer match con obra en construccion
            //    if(origen == "OBC"){
            //        string monto, codEnt;
            //        foreach(DataGridViewRow fila in form_ter_obra.salidaAF.Rows){
            //            monto = string.Format(fila.Cells[2].Value, "General Number");
            //            codEnt = string.Format(fila.Cells[0].Value, "General Number");
            //            mRS = base.EGRESO_OBC(codEnt, artic.Text, monto, zona.codigo);
            //        }
            //        form_ter_obra.continuar = True
            //        form_ter_obra.Close()
            //    }
            //    //ingreso primer registro FINANCIERO
            //    mRS = base.INGRESO_FINANCIERO(codigo, zona.codigo, cantidad, clase, categoria, subzona, subclase, depreciar, usuario, sGestion.ID)
            //    If mRS("cod_status") < 0 Then
            //        'se produjo un error en el insert, se debe avisar
            //        MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
            //        return;
            //    }
            //    'ingreso primer registro TRIBUTARIO
            //    mRS = base.INGRESO_TRIBUTARIO(codigo, zona.codigo, cantidad, clase, categoria, subzona, subclase, depreciar, usuario, sGestion.ID)
            //    If mRS("cod_status") < 0 Then
            //        'se produjo un error en el insert, se debe avisar
            //        MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
            //        return;
            //    }
            //    mRS = Nothing
            //    'analizo si tiene marcada la casilla IFRS
            //    If ckIFRS.Checked Then
            //        'entonces ingresamos el módulo ifrs con valores por defecto (despues ya podrá modificarlos si desea)
            //        Dim val_res, VUA, metod_val As String
            //        val_res = Math.Round(CLng(pcompra) * CDbl(base.IFRS_PREDET(clase)("pValRes")), 0)
            //        VUA = Math.Round(CInt(vutil) / 12 * 365)
            //        metod_val = base.IFRS_PREDET(clase)("metodVal")
            //        mRS = base.INGRESO_IFRS(codigo, val_res, VUA, metod_val, 0, 0, 0, 0, 0)
            //        If mRS("cod_status") < 0 Then
            //            'se produjo un error en el procedimiento, se debe avisar
            //            MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
            //            return;
            //        Else
            //            btn_IFRS.Image = My.Resources._32_edit
            //            btn_IFRS.Text = "Modificar"
            //        }
            //        mRS = Nothing
            //    Else
            //        'como es nuevo, no hay que eliminar nada, puesto que no existe
            //    }
            //    'homologar con los códigos de inventario
            //    mRS = base.GENERAR_CODIGO_INV(codigo)
            //    If mRS("cod_status") < 0 Then
            //        MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
            //    }
            //    mensaje_final = "Registro de articulo ingresado correctamente al Activo Fijo"
            //    cual_sit = cod_situacion.editable
            //Else
            //    'modificacion en lote_articulos (ya que si esta activo, esta pestaña nunca esta disponible)
            //    mRS = base.MODIFICA_LOTE(artic.Text, descrip, proveedor, documento, total_compra, vutil, derecho, fecha_contab)
            //    If mRS("cod_sit") == -1) {
            //        'se produjo un error en el insert, se debe avisar
            //        MsgBox("Se ha producido un error al momento de modificar el lote", vbCritical, "NH FOODS CHILE")
            //        return;
            //    }
            //    mRS = Nothing
            //    'origen no cambia, asi que no se requiere este segmento de codigo

            //    'modifico primer historico FINANCIERO
            //    mRS = base.MODIFICA_FINANCIERO(artic.Text, zona.codigo, categoria, subzona, subclase, depreciar, usuario, sGestion.ID)
            //    If mRS("cod_status") < 0 Then
            //        'se produjo un error en el procedimiento, se debe avisar
            //        MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
            //        return;
            //    }
            //    mRS = Nothing
            //    'modifico primer historico TRIBUTARIO
            //    mRS = base.MODIFICA_TRIBUTARIO(artic.Text, zona.codigo, categoria, subzona, subclase, depreciar, usuario, sGestion.ID)
            //    If mRS("cod_status") < 0 Then
            //        'se produjo un error en el procedimiento, se debe avisar
            //        MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
            //        return;
            //    }
            //    mRS = Nothing
            //    'analizo si tiene marcada la casilla IFRS
            //    Dim cont_ifrs As Integer
            //    cont_ifrs = base.DETALLE_IFRS_CLP(artic.Text).Rows.Count
            //    If ckIFRS.Checked Then
            //        If cont_ifrs = 0 Then
            //            'no existe en ifrs y se ha solicitado ingresarlo
            //            Dim val_res, VUA, metod_val As String
            //            val_res = Math.Round(CLng(pcompra) * base.IFRS_PREDET(clase)("pValRes"), 0)
            //            VUA = Math.Round(CInt(vutil) / 12 * 365)
            //            metod_val = base.IFRS_PREDET(clase)("metodVal")
            //            mRS = base.INGRESO_IFRS(artic.Text, val_res, VUA, metod_val, 0, 0, 0, 0, 0)
            //            If mRS("cod_status") < 0 Then
            //                'se produjo un error en el procedimiento, se debe avisar
            //                MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
            //                return;
            //            Else
            //                btn_IFRS.Image = My.Resources._32_edit
            //                btn_IFRS.Text = "Modificar"
            //            }
            //            mRS = Nothing
            //        Else
            //            'si esta marcado y creado, no es necesario hacer algo
            //        }
            //    Else
            //        If cont_ifrs > 0 Then
            //            //si esta desmarcado y existe, se debe eliminar el registro ifrs del artículo
            //            mRS = base.ELIMINA_IFRS(artic.Text)
            //            If mRS("cod_status") < 0 Then
            //                'se produjo un error en el procedimiento, se debe avisar
            //                MsgBox(mRS("status"), vbCritical, "NH FOODS CHILE")
            //                return;
            //            }
            //            mRS = Nothing
            //        Else
            //            //si no esta marcado y no esta creado, no es necesario hacer algo
            //        }
            //    }
            //    mensaje_final = "Registro de articulo modificado correctamente al Activo Fijo"
            //}
            //carga_superior()
            //cargar_ifrs()
            //cargar_DxG()
            //If ckIFRS.Checked Then
            //    seleccionar_pestaña(paso2)
            //Else
            //    seleccionar_pestaña(paso3)
            //}
            //    MessageBox.Show(mensaje_final, "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
        #endregion

    }
}
