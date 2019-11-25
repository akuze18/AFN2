using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AFN_WF_C.PCClient.Procesos;

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
            var finder = new Busquedas.articulo();
            //finder.set_criterios(estado: busqueda.tipo_estado.soloActivos);
            DialogResult result = finder.DialogFrom(this);
            if (result == DialogResult.OK)
            {
                Procesos.Reportes.get_ficha_ingreso(finder.codigo);
                MessageBox.Show("eligio el articulo:" + finder.codigo.ToString() + " - parte:" + finder.parte.ToString());
            }
            finder = null;
        }
        private void smConsulta02_Click(object sender, EventArgs e)
        {
            
        }
        private void smConsulta03_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smConsulta06_Click(object sender, EventArgs e)
        {
            var box = new Consultas.saldos_obc();
            box.ShowFrom(this);
        }
        #endregion

        #region Menu Cambios

        private void smCambio01_Click(object sender, EventArgs e)
        {
            var box = new Vistas.Cambios.ingreso();
            box.ShowFrom(this);
            //Mensaje.Info("Opción aun no migrada");
        }
        private void smCambio02_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smCambio03_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smCambio04_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smCambio05_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smCambio06_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smCambio07_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smCambio08_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smCambio09_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smCambio10_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
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
            Mensaje.Info("Opción aun no migrada");
        }
        #endregion

        #region Menu Inventario

        private void smInventario01_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smInventario02_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smInventario03_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smInventario04_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }

        #endregion

        #region Menu Sistema

        private void smSistemaTest_Click(object sender, EventArgs e)
        {
            //Microsoft.VisualBasic.Interaction.InputBox("conexion actual","",Procesos.consultas.coneccion());
            //MessageBox.Show(Procesos.consultas.revisar().ToString());
            Procesos.Migracion.TestSave();
            MessageBox.Show("Fin Test");
        }
        private void smSistema01_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smSistema02_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smSistema03_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
        }
        private void smSistema04_Click(object sender, EventArgs e)
        {
            Mensaje.Info("Opción aun no migrada");
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
            Mensaje.Info("Importacion de datos desde AFN1 ha terminado");
        }
        private void smMigracion02_Click(object sender, EventArgs e)
        {
            for (var i = 10; i <= 10; i++)
            {
                DialogResult accion = MessageBox.Show("Desea Procesar el mes " + i.ToString() + "?", "", MessageBoxButtons.YesNoCancel);
                if (accion == DialogResult.Yes)
                    Procesos.Migracion.CargaDepreciacion(2019, i);

                if (accion == DialogResult.Cancel)
                    break;
            }
            Mensaje.Info("Finaliza proceso carga de depreciacion");
        }
        private void smMigracion03_Click(object sender, EventArgs e)
        {
            Procesos.Migracion.agregar_credito();
            Mensaje.Info("Proceso Credito Terminado");
        }
        private void smMigracion04_Click(object sender, EventArgs e)
        {
            var ventana = new Migracion.Ajuste_Parametros();
            ventana.ShowFrom(this);
        }
        private void smMigracion05_Click(object sender, EventArgs e)
        {
            Procesos.Migracion.corregir_bajas();
            Mensaje.Info("Proceso Correccion Bajas Terminado");
        }
        private void smMigracion06_Click(object sender, EventArgs e)
        {
            Procesos.Migracion.CargarDatosOBC();
            Mensaje.Info("Proceso Carga OBC Terminado");
        }
        private void smMigracion07_Click(object sender, EventArgs e)
        {
            Procesos.Migracion.SincronizarAFN1();
            Mensaje.Info("Proceso Sincronización Terminado");
        }
        #endregion

    }
}
