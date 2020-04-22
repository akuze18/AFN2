using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using P = AFN_WF_C.PCClient.Procesos;
using AD = System.DirectoryServices;

namespace AFN_WF_C.PCClient.Vistas
{
    public partial class welcome : PCClient.FormBase
    {
        public welcome()
        {
            InitializeComponent();
        }

        #region Menu Consultas
        private void smConsulta01_Click(object sender, EventArgs e)
        {
            //Ficha Ingreso
            var finder = new Busquedas.articulo();
            //finder.set_criterios(estado: busqueda.tipo_estado.soloActivos);
            DialogResult result = finder.ShowDialogFrom(this);
            if (result == DialogResult.OK)
            {
                P.Reportes.get_ficha_ingreso(finder.codigo);
                //MessageBox.Show("eligio el articulo:" + finder.codigo.ToString() + " - parte:" + finder.parte.ToString());
            }
            finder = null;
        }
        private void smConsulta02_Click(object sender, EventArgs e)
        {
            //Ficha Baja
            var finder = new Busquedas.articulo();
            finder.set_criterios(estado: Busquedas.tipo_estado.soloActivos, vigencias: Busquedas.tipo_vigencia.bajas);
            DialogResult result = finder.ShowDialogFrom(this);
            if (result == DialogResult.OK)
            {
                P.Reportes.get_ficha_baja(finder.full_data);
            }
            finder = null;
        }
        private void smConsulta03_Click(object sender, EventArgs e)
        {
            //Ficha Cambio
            var boxArt = new Busquedas.articulo();
            boxArt.set_criterios(Busquedas.tipo_vigencia.todos, Busquedas.tipo_estado.soloActivos);
            var encontro = boxArt.ShowDialogFrom(this);
            if (encontro == DialogResult.OK)
            {
                int codigo = boxArt.codigo;
                int parte = boxArt.parte;

                var boxLista = new Busquedas.lista_cambios(codigo, parte);
                boxLista.ShowDialogFrom(this);
            }

        }
        private void smConsulta06_Click(object sender, EventArgs e)
        {
            //Obras en Construccion
            var box = new Consultas.saldos_obc();
            box.ShowFrom(this);
        }
        #endregion

        #region Menu Cambios

        private void smCambio01_Click(object sender, EventArgs e)
        {
            var box = new Vistas.Cambios.ingreso();
            box.ShowFrom(this);
        }
        private void smCambio02_Click(object sender, EventArgs e)
        {
            P.Mensaje.NoMigrated();
        }
        private void smCambio03_Click(object sender, EventArgs e)
        {
            P.Mensaje.NoMigrated();
        }
        private void smCambio04_Click(object sender, EventArgs e)
        {
            var box = new Cambios.venta();
            box.ShowFrom(this);
        }
        private void smCambio05_Click(object sender, EventArgs e)
        {
            //P.Mensaje.NoMigrated();
            var box = new Cambios.venta_precio();
            box.ShowFrom(this);
        }
        private void smCambio06_Click(object sender, EventArgs e)
        {
            var box = new Cambios.castigo();
            box.ShowFrom(this);
        }
        private void smCambio07_Click(object sender, EventArgs e)
        {
            var box = new Cambios.traspaso();
            box.ShowFrom(this);
        }
        private void smCambio08_Click(object sender, EventArgs e)
        {
            var box = new Cambios.obras_ingreso();
            box.ShowFrom(this);
        }
        private void smCambio09_Click(object sender, EventArgs e)
        {
            var box = new Cambios.obras_egreso_af();
            box.ShowFrom(this);
        }
        private void smCambio10_Click(object sender, EventArgs e)
        {
            var box = new Cambios.obras_egreso_gasto();
            box.ShowFrom(this);
        }

        #endregion

        #region Menu Procesos
        private void smProceso01_Click(object sender, EventArgs e)
        {
            var box = new Vistas.Acciones.ManagerBatch();
            box.ShowFrom(this);
        }
        #endregion

        #region Menu Reporte
        private void smReporte01_Click(object sender, EventArgs e)
        {
            var box = new Reportes.vigentes();
            box.ShowFrom(this);
        }
        private void smReporte02_Click(object sender, EventArgs e)
        {
            var box = new Reportes.bajas();
            box.ShowFrom(this);
        }
        private void smReporte03_Click(object sender, EventArgs e)
        {
            var box = new Reportes.cuadro_movimiento();
            box.ShowFrom(this);
        }
        private void smReporte04_Click(object sender, EventArgs e)
        {
            var box = new Reportes.fixed_assets();
            box.ShowFrom(this);
        }
        private void smReporte05_Click(object sender, EventArgs e)
        {
            var box = new Reportes.contabilizar();
            box.ShowFrom(this);
        }
        #endregion

        #region Menu Inventario

        private void smInventario01_Click(object sender, EventArgs e)
        {
            P.Mensaje.NoMigrated();
        }
        private void smInventario02_Click(object sender, EventArgs e)
        {
            P.Mensaje.NoMigrated();
        }
        private void smInventario03_Click(object sender, EventArgs e)
        {
            P.Mensaje.NoMigrated();
        }
        private void smInventario04_Click(object sender, EventArgs e)
        {
            P.Mensaje.NoMigrated();
        }

        #endregion

        #region Menu Sistema

        private void smSistemaTest_Click(object sender, EventArgs e)
        {
            //Microsoft.VisualBasic.Interaction.InputBox("conexion actual","",Procesos.consultas.coneccion());
            //MessageBox.Show(Procesos.consultas.revisar().ToString());
            //Procesos.Migracion.TestSave();
            var x = Procesos.Consultas.tipo_cambio.YEN(new DateTime(2019,11,24));
            Procesos.Mensaje.Info(x.ToString());
            MessageBox.Show("Fin Test");
        }
        private void smSistema01_Click(object sender, EventArgs e)
        {
            P.Mensaje.NoMigrated();
        }
        private void smSistema02_Click(object sender, EventArgs e)
        {
            P.Mensaje.NoMigrated();
        }
        private void smSistema03_Click(object sender, EventArgs e)
        {
            P.Mensaje.NoMigrated();
        }
        private void smSistema04_Click(object sender, EventArgs e)
        {
            P.Mensaje.NoMigrated();
        }
        private void smSistema05_Click(object sender, EventArgs e)
        {
            var menu = new Sistema.Depreciar();
            menu.ShowFrom(this);
        }
        
        #endregion

        #region Migracion
        private void smMigracion01_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 22; i++)
            {
                var x = Procesos.Migracion.CargaTransacciones(i);
                if (x.codigo != 1)
                {
                    MessageBox.Show(x.descripcion,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    break;
                }

            }
            P.Mensaje.Info("Importacion de datos desde AFN1 ha terminado");
        }
        private void smMigracion02_Click(object sender, EventArgs e)
        {
            for (var i = 11; i <= 11; i++)
            {
                DialogResult accion = MessageBox.Show("Desea Procesar el mes " + i.ToString() + "?", "", MessageBoxButtons.YesNoCancel);
                if (accion == DialogResult.Yes)
                    Procesos.Migracion.CargaDepreciacion(2019, i);

                if (accion == DialogResult.Cancel)
                    break;
            }
            P.Mensaje.Info("Finaliza proceso carga de depreciacion");
        }
        private void smMigracion03_Click(object sender, EventArgs e)
        {
            Procesos.Migracion.agregar_credito();
            P.Mensaje.Info("Proceso Credito Terminado");
        }
        private void smMigracion04_Click(object sender, EventArgs e)
        {
            var ventana = new Migracion.Ajuste_Parametros();
            ventana.ShowFrom(this);
        }
        private void smMigracion05_Click(object sender, EventArgs e)
        {
            Procesos.Migracion.corregir_bajas();
            P.Mensaje.Info("Proceso Correccion Bajas Terminado");
        }
        private void smMigracion06_Click(object sender, EventArgs e)
        {
            Procesos.Migracion.CargarDatosOBC();
            P.Mensaje.Info("Proceso Carga OBC Terminado");
        }
        private void smMigracion07_Click(object sender, EventArgs e)
        {
            Procesos.Migracion.SincronizarAFN1();
            P.Mensaje.Info("Proceso Sincronización Terminado");
        }
        #endregion

        private void welcome_Load(object sender, EventArgs e)
        {
            string strNameUsuario, strUsuarioRed;
            strUsuarioRed= P.Auxiliar.getUser();
            strNameUsuario = string.Empty;
            try
            {
                var entry = new AD.DirectoryEntry("LDAP://nhfoodschile.cl/OU=NHFOODSCHILE,DC=nhfoodschile,DC=cl");  //CN=Contenedor, OU= Unidad Organizativa
                var Dsearch = new AD.DirectorySearcher(entry);
                Dsearch.Filter = "(samaccountname=" + strUsuarioRed + ")";
                foreach(AD.SearchResult sResultSet in Dsearch.FindAll())
                {
                    //Login Name
                    strNameUsuario = GetProperty(sResultSet, "name");
                    //'Dim sama As String = GetProperty(sResultSet, "samaccountname")
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            Panels1.Text = strNameUsuario;
            Panels2.Text = Today.ToString("dd MMMM yyyy");
            Panels3.Text = P.Auxiliar.servidor;
            Panels4.Text = P.Auxiliar.base_dato;

            //colchon =  P.Consultas.periodo_contable.PERIODO_FISCAL
            //For Each row As DataRow In colchon.Rows
            //    'per.Text = "Periodo " + row("year1").ToString + "-" + CStr(row("year1") + 1)
            //    Sistema2.Text = "Carga de Saldos Iniciales " + CStr(row("year1") + 1)
            //    Sistema2.Tag = row("year1").ToString
            //Next
            //colchon = Nothing
            
            //genero evento Resize para actualizar la posicion de los elementos del panel
            //set_MinSize();
            //Checkeo directorios
            foreach(string directorio in P.Auxiliar.dirAll)
            {
                if( ! System.IO.Directory.Exists(directorio))
                    System.IO.Directory.CreateDirectory(directorio);
            }
            //copio en local archivos necesarios
            Properties.Resources.logo_nippon.Save(P.Auxiliar.fileLogo);    //logo Empresa
            //System.IO.File.WriteAllBytes(P.Auxiliar.fileFontBarcode, Properties.Resources.FRE3OF9X);   //Fuente de Codigo Barra
            //System.IO.File.WriteAllBytes(P.Auxiliar.fileFontLabel, Properties.Resources.BrowalliaUPC);   //Fuente de Letras Etiqueta
        
        }
        #region Funciones Privadas
        private string GetProperty(AD.SearchResult searchResult, string PropertyName)
        {
            string resultado;
            string otroValor;
            resultado = string.Empty;
            foreach(string propertyKey in searchResult.Properties.PropertyNames)
            {
                AD.ResultPropertyValueCollection valueCollection = searchResult.Properties[propertyKey];
                foreach (var propertyValue in valueCollection)
                {
                    if( propertyKey == PropertyName){
                        resultado = propertyValue.ToString();
                    }
                    else{
                        otroValor = propertyValue.ToString();
                    }
                }

            }
            return resultado;
        }
        #endregion

        private void smProceso02_Click(object sender, EventArgs e)
        {
            var box = new Acciones.depreciar();
            box.ShowFrom(this);
        }

        
    }
}
