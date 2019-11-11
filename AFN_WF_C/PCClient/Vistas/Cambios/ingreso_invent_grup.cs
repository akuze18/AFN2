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
    public partial class ingreso_invent_grup : UserControl
    {
        private ingreso _padre;
        private TabPage _page;

        public ingreso_invent_grup()
        {
            InitializeComponent();
        }

        private void ingreso_invent_grup_Load(object sender, EventArgs e)
        {
            _padre = P.Auxiliar.FindPadre(this);
            _page = P.Auxiliar.FindPage(this);
        }
        public void load_data()
        {

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
            completar_informacion(situacion);
        }

        private void habilitar_controles(ingreso.cod_situacion situacion)
        {
            switch (situacion)
            {
                case ingreso.cod_situacion.nuevo:
                    _page.Enabled = false;
                    break;

                case ingreso.cod_situacion.editable:
                case ingreso.cod_situacion.activo:
                    _page.Enabled = true;
                    //habilitar hoja detalle por lote
                    P.Auxiliar.ActivarF(cbGatrib);
                    P.Auxiliar.ActivarF(TGvalor);
                    P.Auxiliar.ActivarF(cbGvalor);
                    P.Auxiliar.ActivarF(btn_addGA);
                    P.Auxiliar.ActivarF(btn_lessGA);
                    P.Auxiliar.ActivarF(btn_detallexG);
                    break;
            }
        }
        private void completar_informacion(ingreso.cod_situacion situacion)
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
                    AtribGrupo.DataSource = TGproc;
                    //traer los valores que correspondan para el lote, si los hubiera
                    //colchon = base.lista_atributo_inicial(artic.Text)
                    var Ldata = new List<object>();
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
    }
}
