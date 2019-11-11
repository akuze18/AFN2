﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using P = AFN_WF_C.PCClient.Procesos;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    public partial class ingreso : AFN_WF_C.PCClient.FormBase
    {
        public enum cod_situacion{
            nuevo = 0,
            editable = 1,
            activo = 2
        }
        public cod_situacion cual_sit;
        public string fuente;
        public int codigo_artic;

        public ingreso()
        {
            InitializeComponent();
        }

        private void ingreso_Load(object sender, EventArgs e)
        {
            seleccionar_pestaña(paso1);
            //pasos.DrawMode = TabDrawMode.Fixed;
            pasos.ItemSize = new Size((pasos.Width-5) / pasos.TabCount, 0);
            artic.Enabled = false;
            TFulldescrip.Enabled = false;
            CkEstado.Enabled = false;

            ficha_basica.load_data();
            ficha_ifrs.load_data();

            //configuracion estética
            foreach(TabPage pestaña in pasos.TabPages)
                pestaña.BackColor = this.BackColor;

            iniciar_formulario();
        }

        /// <summary>
        /// Proceso para limpiar los valores de todo el formulario y dejarlo es estado NUEVO
        /// </summary>
        public void iniciar_formulario()
        {     
            //fuera de pasos
            cargar();
            //paso1
            ficha_basica.limpiar();
            //'paso2
            ficha_ifrs.limpiar();
            //paso3
            ficha_grupo.limpiar();
            //paso4
            ficha_articulo.limpiar();


            ficha_basica.cargar(cual_sit,fuente);
            ficha_ifrs.cargar(cual_sit);
            ficha_grupo.cargar(cual_sit);
            ficha_articulo.cargar(cual_sit);
            
            seleccionar_pestaña(paso1);
        }

        #region funciones de gestion de formulario
        private cod_situacion determina_situacion(int codigo, bool activado)
        {
            if (codigo <= 0)
            {
                //no hay elemento seleccionado
                return ingreso.cod_situacion.nuevo;
            }
            else
            {
                if (activado)
                    return ingreso.cod_situacion.activo;
                else
                    return ingreso.cod_situacion.editable;
            }
        }

        private void cargar()
        {
            cargar(0, string.Empty, "REG", false);
        }
        private void cargar(int codigo, string descripcion, string ingreso, bool estado)
        {
            cual_sit = determina_situacion(codigo, estado);
            
            if(cual_sit == cod_situacion.nuevo)
                artic.Text = string.Empty;
            else
                artic.Text = codigo.ToString();

            codigo_artic = codigo;
            TFulldescrip.Text = descripcion;
            fuente = ingreso;
            CkEstado.Checked = estado;
        }

        public void resultado_busqueda(int codigo, bool estado)
        {
            SINGLE_DETAIL data_fin = P.Consultas.data_ingreso_financiero(codigo);
            bool IFRS = P.Consultas.check_ingreso_ifrs(codigo);

            cargar(codigo, data_fin.descripcion, data_fin.origen.code, estado);

            ficha_basica.cargar(cual_sit, fuente, data_fin, IFRS);
            ficha_ifrs.cargar(cual_sit, IFRS);
            ficha_grupo.cargar(cual_sit);
            ficha_articulo.cargar(cual_sit);
            seleccionar_pestaña(paso1, paso3);
        }
        #endregion

        private void btn_modif_Click(Object sender, EventArgs e)// Handles btn_modif.Click
        {
            var box = new Busquedas.articulo();
            box.set_criterios(Busquedas.tipo_vigencia.todos, Busquedas.tipo_estado.noBorrados);
            DialogResult result = box.DialogFrom(this);
            if (result == DialogResult.OK)
            {
                resultado_busqueda(box.codigo,box.activado);
            }
            box = null;
            
        }
        private void btn_new_Click(Object sender, EventArgs e)// Handles btn_new.Click
        {
            iniciar_formulario();
        }

        #region Estilos Seleccion TabPage

        private void seleccionar_pestaña(TabPage pestaña, TabPage pestaña_op)
        {
            foreach( TabPage sheet in pasos.TabPages)
                pasos.SelectedTab = sheet;
        
            pasos.SelectedTab = pestaña_op;
            pasos.SelectedTab = pestaña;
        }
        private void seleccionar_pestaña(TabPage pestaña)
        {
            foreach (TabPage sheet in pasos.TabPages)
                pasos.SelectedTab = sheet;
        
            pasos.SelectedTab = pestaña;
        }

        private void pasos_Selecting(Object sender, TabControlCancelEventArgs e)
        {
            TabControl contenedor = (TabControl)sender;
            TabPage pestaña = contenedor.SelectedTab;
            e.Cancel = !pestaña.Enabled;
        }

        private void TabControl1_DrawItem(Object sender, DrawItemEventArgs e)// Handles pasos.DrawItem
        {

            //Firstly we'll define some parameters.
            TabPage CurrentTab = pasos.TabPages[e.Index];
            Rectangle ItemRect = pasos.GetTabRect(e.Index);
            var FillBrush = new SolidBrush(System.Drawing.SystemColors.InactiveCaption);
            var TextBrush = new SolidBrush(System.Drawing.SystemColors.InactiveCaptionText);
            var sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            //If we are currently painting the Selected TabItem we'll 
            //change the brush colors and inflate the rectangle.
            if( (e.State == DrawItemState.Selected) ){
                FillBrush.Color = Color.ForestGreen;
                TextBrush.Color = Color.Black;
                ItemRect.Inflate(2, 2);
            } else if(CurrentTab.Enabled){
                FillBrush.Color = Color.LightGreen;
                TextBrush.Color = Color.Gray;
                ItemRect.Inflate(2, 2);
            }

            //Set up rotation for left and right aligned tabs
            if (pasos.Alignment == TabAlignment.Left || pasos.Alignment == TabAlignment.Right){
                Single RotateAngle = 90;
                if(pasos.Alignment == TabAlignment.Left)  
                    RotateAngle = 270;
                var cp = new PointF(ItemRect.Left + (ItemRect.Width / 2), ItemRect.Top + (ItemRect.Height / 2));
                e.Graphics.TranslateTransform(cp.X, cp.Y);
                e.Graphics.RotateTransform(RotateAngle);
                ItemRect = new Rectangle(-(ItemRect.Height / 2), -(ItemRect.Width / 2), ItemRect.Height, ItemRect.Width);
            }

            //Next we'll paint the TabItem with our Fill Brush
            e.Graphics.FillRectangle(FillBrush, ItemRect);

            //Now draw the text.
            e.Graphics.DrawString(CurrentTab.Text, e.Font, TextBrush, ItemRect, sf);    //RectangleF.op_Implicit(ItemRect)

            //Reset any Graphics rotation
            e.Graphics.ResetTransform();

            //Finally, we should Dispose of our brushes.
            FillBrush.Dispose();
            TextBrush.Dispose();

        }
        #endregion
    }
}
