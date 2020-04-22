using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;

using P = AFN_WF_C.PCClient.Procesos;
using V = AFN_WF_C.ServiceProcess.PublicData;
using ACode;

namespace AFN_WF_C.PCClient.Vistas.Reportes
{
    public partial class contabilizar : AFN_WF_C.PCClient.FormBase
    {
        List<V.DETAIL_ACCOUNT> colchon;

        public contabilizar()
        {
            InitializeComponent();
        }

        private void contabilizar_Load(object sender, EventArgs e)
        {
            cb_year.Items.AddRange(P.Consultas.arr.years);
            cb_year.SelectedIndex = 0;
            cb_year.Tag = Label1.Text;

            cb_month.Items.AddRange(P.Consultas.arr.meses);
            cb_month.SelectedIndex = Today.Month - 1;
            cb_month.Tag = Label1.Text;
        }
        private new bool validar_formulario(){
            if(cb_year.SelectedIndex == -1 )
            {
                P.Mensaje.Advert("Debe seleccionar un periodo para mostrar");
                cb_year.Focus();
                return false;
            }
            if(cb_month.SelectedIndex == -1 )
            {
                P.Mensaje.Advert("Debe seleccionar un periodo para mostrar");
                cb_month.Focus();
                return false;
            }
            if(string.IsNullOrEmpty(Tubicacion.Text.Trim()))
            {
                P.Mensaje.Advert("Debe indicar una ubicación para guardar la salida");
                Tubicacion.Focus();
                return false;
            }
            else{
                int ini_archivo;
                string dir_save;
                ini_archivo = Tubicacion.Text.LastIndexOf("\\");    // Strings.InStrRev(Tubicacion.Text, "\\");
                dir_save = Tubicacion.Text.Substring(0,ini_archivo);    //Strings.Left(Tubicacion.Text, ini_archivo)
                if(! System.IO.Directory.Exists(dir_save) ){
                    P.Mensaje.Advert("Directorio ingresado no corresponde");
                    Tubicacion.Focus();
                    return false;
                }
            }
            return true;
        }

        delegate V.GENERIC_VALUE getAsyncCombo();

        V.GENERIC_VALUE getYear()
        {
            if (cb_year.InvokeRequired)
            {
                var fn = new getAsyncCombo(getYear);
                return (V.GENERIC_VALUE) this.Invoke(fn);
            }
            else
            {
                return (V.GENERIC_VALUE)cb_year.SelectedItem;
            }
        }

        V.GENERIC_VALUE getMonth()
        {
            if (cb_month.InvokeRequired)
            {
                var fn = new getAsyncCombo(getMonth);
                return (V.GENERIC_VALUE)this.Invoke(fn);
            }
            else
            {
                return (V.GENERIC_VALUE)cb_month.SelectedItem;
            }
        }

        private Vperiodo selected_period
        {
            get
            {
                var year = getYear();
                var month = getMonth();
                return new Vperiodo(year.id,month.id);
            }
        }

        //funciones de controles del formulario
        private void btn_ubicar_Click(Object sender, EventArgs e) //Handles btn_ubicar.Click
        {
            string nombre_archivo;
            dialogo.Title = "Elija una ubicación para guardar el archivo";
            dialogo.Filter = "Archivo de Texto|*.txt";
            nombre_archivo = "CONTABILIZAR" + selected_period.lastDB + ".txt";
            dialogo.FileName = nombre_archivo;
            dialogo.ShowDialog();
            if(dialogo.FileName != ""){
                Tubicacion.Text = dialogo.FileName;
            }
        }
        private void btn_calcular_Click(Object sender, EventArgs e) //Handles btn_calcular.Click
        {
            if(validar_formulario()){
                generar_contab();
            }
        }

        //funciones del proceso
        private void generar_contab()
        {
            P.Auxiliar.bloquearW(this);
            List<V.DETAIL_ACCOUNT> dato_proceso;
            bool estado;
            dato_proceso = calcular_AF_contab();
            estado = imprimir_AF_contab(dato_proceso, 1);
            if(estado){
                P.Mensaje.Info("Proceso terminado");
            }
            P.Auxiliar.desbloquearW(this);
        }
        private List<V.DETAIL_ACCOUNT> calcular_AF_contab() 
        {
            if((colchon != null) ){
                colchon = new List<V.DETAIL_ACCOUNT>();
            }
            DateTime Tini;
            int SegTrans, SegActual;
            Tini = DateTime.Now;
            SegTrans = 0;
            BGQuery.RunWorkerAsync();
            while( BGQuery.IsBusy)
            {
                //Proceso principal espera a subproceso hasta que termine
                SegActual = ((TimeSpan) (DateTime.Now - Tini)).Seconds;// DateTime. DateDiff(DateInterval.Second, Tini, Now);
                if(SegTrans < SegActual ){
                    //avance.continua_proceso(0)
                    SegTrans = SegActual;
                }
                Application.DoEvents();
            }
            return colchon;
        }
        private bool imprimir_AF_contab(List<V.DETAIL_ACCOUNT> dato_proceso, int inicio)
        {
            string Tlinea;
            string Archivo;
            Archivo = Tubicacion.Text;
            try
            {

                var sw = new StreamWriter(Archivo, false, System.Text.Encoding.Unicode);
                string sTab = "\t";
                sw.WriteLine("TIPO ASIENTO" + sTab + "REFERENCIA ASIENTO" + sTab + "CUENTA" + sTab + "DESCRIPCION" + sTab + "DIMENSIÓN DEPTO" + sTab + "DIMENSIÓN LUGAR" + sTab + "DIFERENCIA" + sTab + "FECHA");
                //agrupar detalle
                var totalizado = (from d in dato_proceso
                                  group d
                                  by new
                                  {
                                      d.grupo,
                                      d.NUM_CUENTA,
                                      d.DSC_CUENTA,
                                      d.dim_lugar,
                                      d.dim_depto,
                                      d.periodo
                                  }
                                into grp
                                select new
                                {
                                    grupo = grp.Key.grupo,
                                    NUM_CUENTA = grp.Key.NUM_CUENTA,
                                    DSC_CUENTA = grp.Key.DSC_CUENTA,
                                    dim_lugar = grp.Key.dim_lugar,
                                    dim_depto = grp.Key.dim_depto,
                                    valor_actual = grp.Sum(x => x.valor_actual),
                                    valor_antes = grp.Sum(x => x.valor_antes),
                                    periodo = grp.Key.periodo,
                                }
                                );

                foreach (var fila in totalizado)
                {
                    Tlinea = fila.grupo.code + sTab;    //TIPO ASIENTO
                    Tlinea += fila.grupo.description + sTab; //REFERENCIA ASIENTO
                    Tlinea += fila.NUM_CUENTA + sTab; //CUENTA
                    Tlinea += fila.DSC_CUENTA + sTab;//DESCRIPCION
                    Tlinea += fila.dim_depto + sTab;//DIMENSIÓN DEPTO
                    Tlinea += fila.dim_lugar + sTab;//DIMENSIÓN LUGAR
                    Tlinea += (fila.valor_actual - fila.valor_antes).ToString() + sTab;//DIFERENCIA
                    //for(int j = 0; j < dato_proceso.Columns.Count; j++)
                    //{
                    //    //if(Val(fila.Item(j)) <> 0 And dato_proceso.Columns(j).DataType.Name = "string" ){
                    //    //    nuevo = "//"
                    //    //}else{
                    //    nuevo = string.Empty;
                    //    //}
                    //    string valor = (string) fila[j];
                    //    nuevo = nuevo + valor.Replace("\r", string.Empty);
                    //    Tlinea = Tlinea + nuevo + sTab;
                    //    Application.DoEvents();
                    //}
                    Tlinea += fila.periodo.lastDB;
                    sw.WriteLine(Tlinea);
                }
                sw.Close();
                return true;
            }
            catch(IOException EIO)
            {
                P.Mensaje.Error(EIO.Message);
                return false;
            }
            catch(Exception ex)
            {
                P.Mensaje.Error(ex.Message);
                return false;
            }
        }

        //ejecucion de consultas en segundo plano
        private void BGQuery_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e ) //Handles BGQuery.DoWork
        {
            var period = selected_period;
            colchon = P.Reportes.CONTABILIZAR_GP2013(period);

        }

        //private void btn_calcular_Click_1(object sender, EventArgs e)
        //{
        //    P.Auxiliar.bloquearW(this);

        //    BGQuery.RunWorkerAsync();
        //    //while (BGQuery.IsBusy)
        //    //{

        //    //}

            
        //}

        private void BGQuery_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //P.Auxiliar.desbloquearW(this);
            //P.Mensaje.Info("Termino");
        }

    }
}
