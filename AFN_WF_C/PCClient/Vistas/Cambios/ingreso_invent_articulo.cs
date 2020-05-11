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
    public partial class ingreso_invent_articulo : UserControl
    {
        private ingreso _padre;
        private TabPage _page;

        public ingreso_invent_articulo()
        {
            InitializeComponent();
        }
        private void ingreso_invent_articulo_Load(object sender, EventArgs e)
        {
            _padre = P.Auxiliar.FindPadre(this);
            _page = P.Auxiliar.FindPage(this);
        }

        public void limpiar()
        {
            cblistaArticulo.DataSource = null;  //.Items.Clear()
            cbAvalor.DataSource = null;         //.Items.Clear()
            cbAvalor.Visible = false;
            TAvalor.Text = string.Empty;
            TAvalor.Visible = true;
            btn_buscaA.Visible = false;
            //AtribArticulo.DataSource = null;
        }

        public void cargar(ingreso.cod_situacion situacion)
        {
            habilitar_controles(situacion);
        }
        public void cargar(ingreso.cod_situacion situacion, List<object> articulos)
        {
            habilitar_controles(situacion);
            completar_informacion(situacion, articulos);
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
            //habilitar hoja detalle por articulo
            P.Auxiliar.ActivarF(cblistaArticulo, AllowEdit);
            P.Auxiliar.ActivarF(cbAatrib, AllowEdit);
            P.Auxiliar.ActivarF(TAvalor, AllowEdit);
            P.Auxiliar.ActivarF(cbAvalor, AllowEdit);
            P.Auxiliar.ActivarF(btn_addDA, AllowEdit);
            P.Auxiliar.ActivarF(btn_lessDA, AllowEdit);
            P.Auxiliar.ActivarF(btn_detallexA, AllowEdit);
        }
        private void completar_informacion(ingreso.cod_situacion situacion, List<object> articulos)
        {
            switch (situacion)
            {

                case ingreso.cod_situacion.editable:
                case ingreso.cod_situacion.activo:
                    
                    DataTable TAproc = new DataTable();
                    //cargar combo codigos de los artículos (paso4)
                    //colchon = base.ARTICULO_INVENTARIO(artic.Text);
                    cblistaArticulo.Items.Clear();
                    cblistaArticulo.Items.AddRange(articulos.ToArray());
                    cblistaArticulo.SelectedIndex = -1;
                    

                    //controles que cambian según sea el atributo que se selecciona
                    
                    cbAvalor.Visible = false;
                    TAvalor.Visible = true;
                    btn_buscaA.Visible = false;      //para atributos de foto
                    //agrego columnas a grilla resultado
                    //TAproc = base.lista_atributos_paso4
                    AtribArticulo.DataSource = TAproc;
                    

                    break;

                case ingreso.cod_situacion.nuevo:
                    //no se carga nada
                    break;
            }
        }

        private void btn_imprimir1_Click(object sender, EventArgs e)
        {

        }
    }
}
