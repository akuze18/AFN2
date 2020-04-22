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

namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    public partial class obras_ingreso : AFN_WF_C.PCClient.FormBase
    {
        Color color_btn;

        public obras_ingreso()
        {
            InitializeComponent();
        }

        private void obras_ingreso_Load(object sender, EventArgs e)
        {
            DateTime tmp_fecha;
            tmp_fecha = Today.AddMonths(-6);
            ACode.Vperiodo tmp_per = new ACode.Vperiodo(tmp_fecha.Year,tmp_fecha.Month);
            
            Tfecha_compra.Value = Today;
            Tfecha_compra.CustomFormat = "dd-MM-yyyy";
            Tfecha_compra.MaxDate = Today;
            Tfecha_compra.MinDate = tmp_per.first;

            Tfecha_conta.Value = Today;
            Tfecha_conta.CustomFormat = "dd-MM-yyyy";
            Tfecha_conta.MaxDate = Today;
            Tfecha_conta.MinDate = tmp_per.first;
            
            //zonas
            cboZona.Items.AddRange(P.Consultas.zonas.SearchList().ToArray());
            cboZona.SelectedIndex = -1;
            
            //proveedor
            cboProveedor.Items.AddRange(P.Consultas.proveedores.listar().ToArray());
            cboProveedor.SelectedIndex = -1;
            
            color_btn = btn_guardar.BackColor;
        }

        private void btn_Bprov_Click(Object sender, EventArgs e)// Handles btn_Bprov.Click
        {
            var box = new Busquedas.proveedor();
            var seleccion = box.ShowDialogFrom(this);
            if (seleccion == DialogResult.OK)
            {
                cboProveedor.SelectedItem = box.codigo;
            }
        }

        private void Tcredito_GotFocus(Object sender, EventArgs e)// Handles Tcredito.GotFocus
        {
            string procesar;
            int valor;
            procesar = Tcredito.Text;
            procesar = procesar.Replace(",", "");
            if(int.TryParse(procesar,out valor))
                Tcredito.Text = valor.ToString("#");
            else
                Tcredito.Text = string.Empty;
        }
        private void Tcredito_LostFocus(Object sender, EventArgs e)// Handles Tcredito.LostFocus
        {
            if (Tcredito.Text != string.Empty ) {
                string procesar;
                int valor;
                procesar = Tcredito.Text;
                procesar = procesar.Replace(",", "");
                if (int.TryParse(procesar,out valor) ) {
                    Tcredito.Text = valor.ToString("#,##0");
                }
                else
                {
                    P.Mensaje.Info("Solo puede ingresar números en la cantidad");
                    Tcredito.Focus();
                    Tcredito.Text = string.Empty; 
                }
            }
        }

        private void btn_limpiar_Click(Object sendert, EventArgs e) //Handles btn_limpiar.Click
        {
            cboZona.SelectedIndex = -1;
            Tcredito.Text = string.Empty;
            Tdoc.Text = string.Empty;
            cboProveedor.SelectedIndex = -1;
            Tdescrip.Text = string.Empty;
            btn_guardar.BackColor = color_btn;
        }

        private void btn_guardar_Click(Object sender, EventArgs e) //Handles btn_guardar.Click
        {
            //validar información ingresada
            if (Tfecha_compra.Value.ToString() == string.Empty ) {
                P.Mensaje.Advert("Debe indicar la fecha de ingreso por Obra en Construcción");
                Tfecha_compra.Focus();
                return;
            }
            if (Tfecha_conta.Value.ToString() == string.Empty ) {
                P.Mensaje.Advert("Debe indicar la fecha de contabilizacion de Obra en Construcción");
                Tfecha_conta.Focus();
                return;
            }
            if (cboZona.SelectedIndex == -1 ) {
                P.Mensaje.Advert("Debe indicar la zona de la Obra en Construcción");
                cboZona.Focus();
                return;
            }
            if (Tcredito.Text == string.Empty ) {
                P.Mensaje.Advert("Debe indicar el monto de Obra en Construcción");
                Tcredito.Focus();
                return;
            }
            if (Tdoc.Text == string.Empty ) {
                DialogResult eleccion;
                eleccion = P.Mensaje.Confirmar("Desea continuar sin indicar el Nº de documento de Obra en Construcción");
                if (eleccion != DialogResult.Yes ) {   //no marco SI
                    Tdoc.Focus();
                    return;
                }
            }
            if (Tdescrip.Text == string.Empty ) {
                P.Mensaje.Advert("Debe indicar la descripción o referencia de la Obra en Construcción");
                Tdescrip.Focus();
                return;
            }
            //fin validación
            //inicio preparar datos para ingresar
            string documento, proveedor, descp;
            decimal credit_amo;
            DateTime fechaC, fechaGL;
            PD.GENERIC_VALUE zona;
            fechaC = Tfecha_compra.Value;
            fechaGL = Tfecha_conta.Value;
            zona = (PD.GENERIC_VALUE) cboZona.SelectedItem;
            descp = Tdescrip.Text;
            if( ! decimal.TryParse(Tcredito.Text, out credit_amo))
            {
                P.Mensaje.Advert("Monto ingresado no es valido");
                Tcredito.Focus();
                return;
            }
            
            if (Tdoc.Text == string.Empty ) {
                documento = P.Consultas.documentos.defaultDocument;
            }else{
                documento = Tdoc.Text;
            }
            if (cboProveedor.SelectedIndex == -1 ) {
                proveedor = P.Consultas.documentos.defaultProveed;
            }else{
                proveedor = ((PD.SV_PROVEEDOR)cboProveedor.SelectedItem).COD;
            }
            //Valido que no se haya ingresado duplicado comprobando el documento, proveedor y zona
            int contar;
            contar = P.Consultas.documentos.buscar_entrada_obc(documento, proveedor, zona.id);
            if (contar > 0 ) {
                P.Mensaje.Error("La entrada indicada ya existe en el sistema");
                return;
            }
            //ingreso en obra_cons
            PD.RespuestaAccion mRS;
            mRS = P.Consultas.obc.INGRESO_OBC(fechaC, zona.id, proveedor, documento, descp, credit_amo, fechaGL);
            if (mRS.codigo < 0)
            {
                P.Mensaje.Error(mRS.descripcion);
            }
            else
            {
                //fin ingreso en obra_cons
                P.Mensaje.Info("Registro de Obra en Construccion ingresado correctamente");
                btn_guardar.BackColor = Color.OrangeRed;
            }
        }

        //si produce un cambio en cualquier control, el boton vuelve a su color por defecto
        private void defaultColorButton(Object sender, EventArgs e) //Handles cboZona.SelectedIndexChanged
        {
            btn_guardar.BackColor = color_btn;
            if (sender.GetType() == typeof(DateTimePicker))
            {
                var campo_fecha = (DateTimePicker)(sender);
                if (campo_fecha.Value.Month == 1 && campo_fecha.Value.Day == 1 )
                    campo_fecha.Value = new DateTime(campo_fecha.Value.Year, 1, 2);
            }
        }
    }
}
