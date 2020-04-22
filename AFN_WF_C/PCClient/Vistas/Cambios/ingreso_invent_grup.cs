using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using P = AFN_WF_C.PCClient.Procesos;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    public partial class ingreso_invent_grup : UserControl
    {
        private ingreso _padre;
        private TabPage _page;

        public ingreso_invent_grup()
        {
            InitializeComponent();
            AtribGrupo.FullRowSelect = true;
            AtribGrupo.MultiSelect = false;
            AtribGrupo.ShowGroups = false;
        }

        private void ingreso_invent_grup_Load(object sender, EventArgs e)
        {
            _padre = P.Auxiliar.FindPadre(this);
            _page = P.Auxiliar.FindPage(this);
        }
        public void load_data()
        {
            cbGatrib.Items.AddRange(P.Consultas.inventario.GetActiveAttributes().ToArray());
        }

        public void limpiar()
        {
            cbGvalor.DataSource = null;     // Items.Clear()
            cbGvalor.Visible = false;
            TGvalor.Text = string.Empty;
            TGvalor.Visible = true;
            btn_buscaG.Visible = false;
            //AtribGrupo.DataSource = null;
        }

        public void cargar(ingreso.cod_situacion situacion)
        {
            habilitar_controles(situacion);
            completar_informacion(situacion, new List<SV_ARTICLE_DETAIL>());
        }
        public void cargar(ingreso.cod_situacion situacion, List<SV_ARTICLE_DETAIL> detail)
        {
            habilitar_controles(situacion);
            completar_informacion(situacion, detail);
        }

        private void habilitar_controles(ingreso.cod_situacion situacion)
        {
            //_page.Enabled = true;
            bool AllowEdit;
            switch (situacion)
            {
                case ingreso.cod_situacion.nuevo:
                    AllowEdit = false;
                    break;

                case ingreso.cod_situacion.editable:
                case ingreso.cod_situacion.activo:
                    AllowEdit = true;
                    break;
                default: AllowEdit = false; break;
            }
            //habilitar hoja detalle por lote
            P.Auxiliar.ActivarF(cbGatrib, AllowEdit);
            P.Auxiliar.ActivarF(TGvalor, AllowEdit);
            P.Auxiliar.ActivarF(cbGvalor, AllowEdit);
            P.Auxiliar.ActivarF(btn_addGA, AllowEdit);
            P.Auxiliar.ActivarF(btn_lessGA, AllowEdit);
            P.Auxiliar.ActivarF(btn_detallexG, AllowEdit);
        }

        private void completar_informacion(ingreso.cod_situacion situacion, List<SV_ARTICLE_DETAIL> Ldata)
        {
            switch (situacion)
            {

                case ingreso.cod_situacion.editable:
                case ingreso.cod_situacion.activo:
                    
                    DataTable TGproc = new DataTable();

                    //controles que cambian según sea el atributo que se selecciona
                    cbGvalor.Visible = false;
                    TGvalor.Visible = true;
                    btn_buscaG.Visible = false;      //para atributos de foto
                    
                    //agrego columnas a grilla resultado
                    //TGproc = base.lista_atributos_paso3;
                    AtribGrupo.SetObjects(Ldata.Where(a => a.article_id==null)
                        .ToList().ConvertAll(a => new P.Estructuras.DisplayArticDetail(a)));
                    //AtribGrupo.AutoResizeColumns();
                    //traer los valores que correspondan para el lote, si los hubiera
                    //colchon = base.lista_atributo_inicial(artic.Text)
                        foreach(object fila in Ldata){

                //            If fila.Item("codigo") = "" Then
                //                Dim newfila As DataRow = TGproc.NewRow
                //                newfila("Código Atributo") = fila.Item("cod_atrib")
                //                If base.DET_ATRIBUTO(fila("cod_atrib"))("tipo") = "FOTO" Then
                //                    newfila("valor guardado") = "XX:" + fila.Item("detalle")
                //                Else
                //                    newfila("valor guardado") = fila.Item("detalle")
                //                End If
                //                newfila("Atributo") = fila.Item("atributo")
                //                newfila("Valor del atributo") = fila.Item("dscr_detalle")
                //                newfila("Mostrar") = fila.Item("imprimir")
                //                TGproc.Rows.Add(newfila)
                //            Else
                //                Dim newfila As DataRow = TAproc.NewRow
                //                newfila("Código Atributo") = fila.Item("cod_atrib")
                //                If base.DET_ATRIBUTO(fila("cod_atrib"))("tipo") = "FOTO" Then
                //                    newfila("valor guardado") = "XX:" + fila.Item("detalle")
                //                Else
                //                    newfila("valor guardado") = fila.Item("detalle")
                //                End If
                //                newfila("Artículo") = fila.Item("codigo")
                //                newfila("Atributo") = fila.Item("atributo")
                //                newfila("Valor del atributo") = fila.Item("dscr_detalle")
                //                newfila("Mostrar") = fila.Item("imprimir")
                //                TAproc.Rows.Add(newfila)
                //            End If
                        }
                    //        With AtribGrupo
                    //            .ClearSelection()
                    //            'establecer visibilidad o ancho de columnas segun corresponda
                    //            .Columns(0).Visible = False
                    //            .Columns(1).Visible = False
                    //            .Columns(2).Width = 200
                    //            .Columns(3).Width = 355
                    //            .Columns(4).Width = 70
                    //            For Each columna As DataGridViewColumn In .Columns
                    //                columna.SortMode = DataGridViewColumnSortMode.NotSortable
                    //            Next
                    //            .Sort(.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
                    //            .AllowUserToResizeColumns = False
                    //            .AllowUserToResizeRows = False
                    //        End With
                    //        With AtribArticulo
                    //            .ClearSelection()
                    //            .Columns(0).Visible = False
                    //            .Columns(1).Visible = False
                    //            .Columns(2).Width = 110
                    //            .Columns(3).Width = 200
                    //            .Columns(4).Width = 325
                    //            .Columns(5).Width = 50
                    //            For Each columna As DataGridViewColumn In .Columns
                    //                columna.SortMode = DataGridViewColumnSortMode.NotSortable
                    //            Next
                    //            .Sort(.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
                    //            .AllowUserToResizeColumns = False
                    //            .AllowUserToResizeRows = False
                    //        End With

                    break;

                case ingreso.cod_situacion.nuevo:
                    //no se carga nada
                    break;
            }
        }

        private void btn_addGA_Click(object sender, EventArgs e)
        {
            if (cbGatrib.SelectedIndex >= 0)
            {
                //reviso que el atributo tenga valor para ingresarlo
                var SelAttrib = (SV_ATTRIBUTE)cbGatrib.SelectedItem;
                if (SelAttrib.tipo == "COMBO")
                {
                    if(cbGvalor.SelectedIndex == -1)
                    {
                        P.Mensaje.Advert("No ha seleccionado un valor para " + SelAttrib.name);
                        cbGvalor.Focus();
                        return;
                    }
                }
                else
                {
                    if( String.IsNullOrEmpty(TGvalor.Text.Trim()))
                    {
                        P.Mensaje.Advert("No ha ingresado un valor para " + SelAttrib.name);
                        TGvalor.Focus();
                        return;
                    }
                    if(SelAttrib.tipo == "FOTO")
                    {
                        //si es foto reviso que sea una direccion valida
                        if(File.Exists(TGvalor.Text) )
                        {
                            //existe archivo, esta bien
                        }
                        else
                        {
                            P.Mensaje.Advert("Archivo indicado para la foto no existe");
                            TGvalor.Focus();
                            TGvalor.SelectionStart = 0;
                            TGvalor.SelectionLength = TGvalor.Text.Length;
                            return;
                        }
                    }
                    else
                        TGvalor.Text = TGvalor.Text.ToUpper();  
                }
                //reviso que el atributo no este ingresado
                var listado_ingreso = (List<P.Estructuras.DisplayArticDetail>) AtribGrupo.Objects;
                foreach(var elem in listado_ingreso)
                {
                    if ( elem.CodigoAtributo == SelAttrib.id )
                    {
                        P.Mensaje.Advert("Atributo " +SelAttrib.name + " ya ha sido establecido");
                        return;
                    }
                }
                //ingreso atributo a la lista
                var nuevo_atributo = new P.Estructuras.DisplayArticDetail();
                nuevo_atributo.CodigoAtributo = SelAttrib.id;
                nuevo_atributo.Atributo = SelAttrib.name;
                nuevo_atributo.Mostrar = ckMostrar.Checked;
                nuevo_atributo.tipo = SelAttrib.tipo;
                if (SelAttrib.tipo == "COMBO")
                {
                    var SelComboValue = (GENERIC_VALUE) cbGvalor.SelectedItem;
                    nuevo_atributo.ValorGuardado = SelComboValue.id.ToString();
                    nuevo_atributo.ValorDelAtributo = SelComboValue.description;
                }
                else
                {
                    nuevo_atributo.ValorGuardado = TGvalor.Text;
                    string ValMostrar = TGvalor.Text;
                    if (SelAttrib.tipo == "FOTO")
                    {
                        ValMostrar = Path.GetFileName(ValMostrar);
                        //int inicio, final;
                        //inicio = 0;
                        //final = 1;
                        //while (final != 0)
                        //{
                        //    final = ValMostrar.IndexOf("\\",inicio+1);
                        //    if (final != 0)
                        //        inicio = final;
                        //}
                        //ValMostrar = ValMostrar Mid(TGvalor.Text, inicio + 1)
                    }
                    nuevo_atributo.ValorDelAtributo = ValMostrar;
                    
                }
                listado_ingreso.Add(nuevo_atributo);//.SetValue(nuevo_atributo, listado_ingreso.Length);
                AtribGrupo.SelectedItem = null;//.ClearSelection()
                AtribGrupo.SetObjects(listado_ingreso);
                cbGatrib.SelectedIndex = -1;
                TGvalor.Text = String.Empty;
                cbGvalor.SelectedIndex = -1;
                ckMostrar.Checked = false;
                cbGatrib.Focus();
            }
        }

        private void btn_lessGA_Click(object sender, EventArgs e)
        {
            var listado_borrar = (List<P.Estructuras.DisplayArticDetail>)AtribGrupo.Objects;
            var fila = (P.Estructuras.DisplayArticDetail) AtribGrupo.SelectedItem.RowObject;
            listado_borrar.Remove(fila);
            AtribGrupo.SetObjects(listado_borrar);
            AtribGrupo.SelectedItem = null;
            cbGatrib.Focus();
        }

        private void btn_detallexG_Click(object sender, EventArgs e)
        {
            //string mensaje;
            //DataTable colchon;
            string detalle, Antig, nueva_foto;//, val_foto, solo_foto;
            int lote_art, nuevo, estaba, malo, elimina, atributo;
            //DataRow tengo_atrib, resultado, mostrar;
            //bool disponible, Copiar;
            nuevo = 0;
            estaba = 0;
            malo = 0;
            lote_art = _padre.codigo_artic;
            var ListaProcesar = (List<P.Estructuras.DisplayArticDetail>)AtribGrupo.Objects;
            if (ListaProcesar.Count() > 0)
            {
                nueva_foto = "";
                detalle = "";
                Antig = "";
                foreach(var elem in ListaProcesar)
                {
                    
                    //atributo = elem.CodigoAtributo;
                    //mostrar = elem.Mostrar;
                    //tengo_atrib = base.INV_ATRIBUTOxLOTE(lote_art, atributo);
                    //if (tengo_atrib == null)
                    //{
                    //    if (elem.tipo == "FOTO")
                    //    {
                    //        //generamos el nuevo nombre para la foto
                    //        val_foto = foto_name();
                    //        //no existe la foto, por lo tanto podemos crearlo
                    //        string extension = Path.GetExtension(elem.ValorGuardado);
                    //        solo_foto = val_foto + "." + extension;
                    //        nueva_foto = P.Auxiliar.dirFotos + solo_foto;
                    //        detalle = solo_foto;
                    //        Copiar = true;
                    //    }
                    //    else
                    //    {
                    //        detalle = elem.ValorGuardado;
                    //    }
                    //}
                    //else
                    //{
                    //    if (elem.tipo == "FOTO")
                    //    {
                    //        Antig = tengo_atrib["detalle"];
                    //        if(elem.ValorGuardado.Substring(0,3) == "XX:")
                    //        {
                    //            //foto viene desde el servidor cargada, no necesito crearla de nuevo
                    //            detalle = elem.ValorGuardado.Substring(3);
                    //            Copiar = false;
                    //        }
                    //        else
                    //        {
                    //            //foto nueva ingresada
                    //            //generamos el nuevo nombre para la foto
                    //            val_foto = foto_name();
                    //            //no existe la foto, por lo tanto podemos crearlo
                    //            string extension = Path.GetExtension(elem.ValorGuardado);
                    //            solo_foto = val_foto + "." + extension;
                    //            nueva_foto = P.Auxiliar.dirFotos + solo_foto;
                    //            detalle = solo_foto;
                    //            Copiar = true;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        detalle = elem.ValorGuardado;
                    //    }
                    //}
                
                    //resultado = base.INGRESO_ATRIB_LOTE(lote_art, atributo, detalle, mostrar);
                    //switch(resultado("estado"))
                    //{
                    //    case 1 :    //nuevo
                    //        nuevo = nuevo + 1;
                    //        if (elem.tipo == "FOTO")
                    //        {
                    //            //copio la foto al server una vez que ya deje el registro en la BD
                    //            File.Copy(elem.ValorGuardado, nueva_foto);
                    //            elem.ValorGuardado = "XX:" + detalle;
                    //        }
                    //        break;
                    //    Case 0      'existente
                    //        estaba = estaba + 1
                    //        If base.DET_ATRIBUTO(atributo)("tipo") = "FOTO" Then
                    //            If Copiar Then
                    //                'copio la foto al server una vez que ya deje el registro en la BD
                    //                IO.File.Copy(AtribGrupo.Rows(i).Cells(1).Value, nueva_foto)
                    //                AtribGrupo.Rows(i).Cells(1).Value = "XX:" + detalle
                    //                If Antig <> detalle Then
                    //                    'borro antigua
                    //                    Kill(base.dirFotos + Antig)
                    //                End If
                    //            End If
                    //        End If
                    //    Case -1     'error
                    //        malo = malo + 1
                    //}
                    Application.DoEvents();
                }
            }
            //'una vez que recorri toda la grilla (o estaba vacia), debo eliminar de la base de datos aquellos que no esten listados
            //elimina = 0;
            //colchon = base.INV_ATRIBUTOxLOTE(lote_art);
            //foreach(DataRow fila in colchon.Rows)
            //{
            //    atributo = fila["cod_atrib"];
            //    detalle = fila["detalle"];
            //    disponible = false;
            //    For i = 0 To AtribGrupo.Rows.Count - 1
            //        If AtribGrupo.Rows(i).Cells(0).Value = atributo Then
            //            disponible = True
            //        End If
            //        Application.DoEvents()
            //    Next
            //    If Not disponible Then
            //        'ya no esta disponible en la grilla, entonces lo sacamos de la BD tb
            //        base.BORRAR_ATRIBUTOxLOTE(artic.Text, atributo)
            //        elimina = elimina + 1
            //    End If
            //    Application.DoEvents()
            //}

            //If nuevo = 0 And estaba = 0 And elimina = 0 And malo = 0 Then
            //    MessageBox.Show("No hay registros para ingresar al detalle por grupo", "NH FOODS CHILE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            //    Exit Sub
            //End If

            //mensaje = "Proceso completado :" & vbCrLf & vbCrLf
            //If nuevo <> 0 Then
            //    mensaje = mensaje + "   Nuevos  " + CStr(nuevo) & vbCrLf
            //End If
            //If estaba <> 0 Then
            //    mensaje = mensaje + "   Actualizados  " + CStr(estaba) & vbCrLf
            //End If
            //If elimina <> 0 Then
            //    mensaje = mensaje + "   Eliminados  " + CStr(elimina) & vbCrLf
            //End If
            //If malo <> 0 Then
            //    mensaje = mensaje + "   Fallidos  " + CStr(malo) & vbCrLf
            //End If
            //MsgBox(mensaje, vbInformation, "NH FOODS CHILE")
            //carga_superior()

        }
    }
}
