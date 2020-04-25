using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PD = AFN_WF_C.ServiceProcess.PublicData;
using P = AFN_WF_C.PCClient.Procesos;

namespace AFN_WF_C.PCClient.Vistas.Busquedas
{
    public partial class lista_cambios : AFN_WF_C.PCClient.FormBase
    {
        private int _codigo;
        private int _parte;

        public lista_cambios()
        {
            _codigo = 0;
            _parte = 0;
            InitializeComponent();
        }

        public lista_cambios(int codigo, int parte)
        {
            _codigo = codigo;
            _parte = parte;
            InitializeComponent();
        }

        private void lista_cambios_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void load_data()
        {
            if (_codigo != 0)   // && _parte != 0
            {

                PD.SV_SYSTEM DefSistema = P.Consultas.sistema.Default();
                ACode.Vperiodo periodo = new ACode.Vperiodo(Today.Year, Today.Month);
                DateTime desde = DateTime.MinValue;
                DateTime hasta = DateTime.MaxValue;
                List<PD.DETAIL_MOVEMENT> resultado = P.Consultas.listar_cambios(DefSistema, periodo,_codigo,_parte, desde, hasta, null, null);
                if (resultado.Count > 0)
                {
                    cod_art.Text = _codigo.ToString();
                    Tarticulo.Text = resultado[0].desc_breve;
                }
                else
                    empty_fields();
                detalle.SetObjects(resultado);
            }
            else
                empty_fields();
            
        }

        private void empty_fields()
        {
            /*Si no hay detalle de cambios, debo terminar el proceso*/
            P.Mensaje.Advert("Lote seleccionado no posee traspasos");
            this.Close();
            //cod_art.Text = string.Empty;
            //Tarticulo.Text = string.Empty;
        }

        private void btn_consulta_Click(object sender, EventArgs e)
        {
            var boxArt = new Busquedas.articulo();
            boxArt.set_criterios(Busquedas.tipo_vigencia.todos, Busquedas.tipo_estado.soloActivos);
            var encontro = boxArt.ShowDialogFrom(this);
            if (encontro == DialogResult.OK)
            {
                _codigo = boxArt.codigo;
                _parte = boxArt.parte;
                load_data();
            }
        }

        private void btn_imprimir_Click(object sender, EventArgs e)
        {
            if(detalle.SelectedObject != null)
            {
                PD.DETAIL_MOVEMENT fila = (PD.DETAIL_MOVEMENT) detalle.SelectedObject;
                var res = P.Reportes.get_ficha_cambio(fila);
                if (res.codigo > 0)
                    P.Mensaje.Info("Reporte generado");
                else
                    P.Mensaje.Error(res.descripcion);
            }
        }
        
    }
}
