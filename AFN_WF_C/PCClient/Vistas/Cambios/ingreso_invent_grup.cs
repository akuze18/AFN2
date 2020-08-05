using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Env = System.Environment;

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
                    AtribGrupo.SetObjects(Ldata.Where(a => a.article_id==null)
                        .ToList().ConvertAll(a => new P.Estructuras.DisplayArticDetail(a)));
                    //AtribGrupo.AutoResizeColumns();
                    AtribGrupo.SelectedItem = null;// ClearSelection();
                    //establecer visibilidad o ancho de columnas segun corresponda
                    //AtribGrupo.Columns[0].Width = 0;
                    //AtribGrupo.Columns[1].Width = 0;
                    AtribGrupo.Columns[0].Width = 200;
                    AtribGrupo.Columns[1].Width = 355;
                    AtribGrupo.Columns[2].Width = 70;
                    //foreach(ColumnHeader columna in AtribGrupo.Columns)
                    //    columna.SortMode = DataGridViewColumnSortMode.NotSortable;
                    //AtribGrupo.Sort(AtribGrupo.Columns[0], SortOrder.Ascending);
                    //AtribGrupo.AllowUserToResizeColumns = false;
                    //AtribGrupo.AllowUserToResizeRows = false;

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
                string ValMostrar = string.Empty;
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
                    ValMostrar = TGvalor.Text.Trim();
                    if (String.IsNullOrEmpty(ValMostrar))
                    {
                        P.Mensaje.Advert("No ha ingresado un valor para " + SelAttrib.name);
                        TGvalor.Focus();
                        return;
                    }
                    if(SelAttrib.tipo == "FOTO")
                    {
                        //si es foto reviso que sea una direccion valida
                        if (!File.Exists(ValMostrar))
                        {
                            P.Mensaje.Info("Archivo indicado para la foto no existe");
                            TGvalor.Focus();
                            TGvalor.SelectionStart = 0;
                            TGvalor.SelectionLength = TGvalor.Text.Length;
                            return;
                        }
                    }
                    else
                        ValMostrar = ValMostrar.ToUpper();  
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
                    nuevo_atributo.ValorGuardado = ValMostrar;
                    if (SelAttrib.tipo == "FOTO")
                        ValMostrar = Path.GetFileName(ValMostrar);
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
            string mensaje;
            string detalle, Antig, nueva_foto, val_foto, solo_foto;
            int lote_art, nuevo, estaba, malo, elimina, atributo;
            //RespuestaAccion resultado;
            bool disponible, Copiar, mostrar;
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
                    atributo = elem.CodigoAtributo;
                    Copiar = false;
                    mostrar = elem.Mostrar;
                    var tengo_atrib = P.Consultas.inventario.GetDetailAttrByLote(lote_art, atributo);
                    if (tengo_atrib.id == 0)
                    {   //no existe el atributo previamente guardado
                        if (elem.tipo == "FOTO")
                        {
                            //generamos el nuevo nombre para la foto
                            val_foto = P.Auxiliar.foto_name();
                            //no existe la foto, por lo tanto podemos crearlo
                            string extension = Path.GetExtension(elem.ValorGuardado);
                            solo_foto = val_foto + "." + extension;
                            nueva_foto = P.Auxiliar.dirFotos + solo_foto;
                            detalle = solo_foto;
                            Copiar = true;
                        }
                        else
                        {
                            detalle = elem.ValorGuardado;
                        }
                    }
                    else
                    {   //ya tenia este atributo establecido
                        if (elem.tipo == "FOTO")
                        {
                            Antig = tengo_atrib.detalle;
                            if(elem.ValorGuardado.Substring(0,3) == "XX:")
                            {
                                //foto viene desde el servidor cargada, no necesito crearla de nuevo
                                detalle = elem.ValorGuardado.Substring(3);
                                Copiar = false;
                            }
                            else
                            {
                                //foto nueva ingresada
                                //generamos el nuevo nombre para la foto
                                val_foto = P.Auxiliar.foto_name();
                                //no existe la foto, por lo tanto podemos crearlo
                                string extension = Path.GetExtension(elem.ValorGuardado);
                                solo_foto = val_foto + "." + extension;
                                nueva_foto = P.Auxiliar.dirFotos + solo_foto;
                                detalle = solo_foto;
                                Copiar = true;
                            }
                        }
                        else
                        {
                            detalle = elem.ValorGuardado;
                        }
                    }

                    var resultado = P.Consultas.inventario.INGRESO_ATRIB_LOTE(lote_art, atributo, detalle, mostrar);
                    switch(resultado.codigo)
                    {
                        case 1:    //nuevo
                            nuevo = nuevo + 1;
                            if (elem.tipo == "FOTO")
                            {
                                //copio la foto al server una vez que ya deje el registro en la BD
                                File.Copy(elem.ValorGuardado, nueva_foto);
                                elem.ValorGuardado = "XX:" + detalle;
                            }
                            break;
                        case 0:      //existente
                            estaba = estaba + 1;
                            if (elem.tipo == "FOTO")
                            {
                                if (Copiar)
                                {
                                    //copio la foto al server una vez que ya deje el registro en la BD
                                    File.Copy(elem.ValorGuardado, nueva_foto);
                                    elem.ValorGuardado = "XX:" + detalle;
                                    if (Antig != detalle)   //borro antigua
                                        File.Delete(P.Auxiliar.dirFotos + Antig);
                                }
                            }
                            break;
                        default:     //error
                            malo = malo + 1;
                            break;
                    }
                    Application.DoEvents();
                }
            }
            //una vez que recorri toda la grilla (o estaba vacia), debo eliminar de la base de datos aquellos que no esten listados
            elimina = 0;
            var currentValues = P.Consultas.inventario.GetDetailArtByLote(lote_art);
            foreach(SV_ARTICLE_DETAIL fila in currentValues)
            {
                atributo = fila.cod_atrib;
                detalle = fila.detalle;
                disponible = false;
                foreach(var linea in AtribGrupo.Objects)
                {
                    var l = (Procesos.Estructuras.DisplayArticDetail)linea;
                    if(l.CodigoAtributo == atributo)
                        disponible = true;
                }
                if (!disponible)
                {
                    //ya no esta disponible en la grilla, entonces lo sacamos de la BD tb
                    P.Consultas.inventario.BORRAR_ATRIBUTOxLOTE(lote_art, atributo);
                    elimina = elimina + 1;
                }
                Application.DoEvents();
            }

            if ((nuevo == 0) && (estaba == 0) && (elimina == 0) && (malo == 0))
            {
                P.Mensaje.Advert("No hay registros para ingresar al detalle por grupo");
                return;
            }

            mensaje = "Proceso completado :" + Env.NewLine + Env.NewLine;
            if (nuevo != 0)
                mensaje = mensaje + "   Nuevos  " + nuevo.ToString() + Env.NewLine;
            if (estaba != 0)
                mensaje = mensaje + "   Actualizados  " + estaba.ToString() + Env.NewLine;
            if (elimina != 0)
                mensaje = mensaje + "   Eliminados  " + elimina.ToString() + Env.NewLine;
            if (malo != 0)
                mensaje = mensaje + "   Fallidos  " + malo.ToString() + Env.NewLine;
            _padre.RenovarPanel();
            P.Mensaje.Info(mensaje);
        }
    }
}
