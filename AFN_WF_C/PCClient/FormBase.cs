using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Configuration;


namespace AFN_WF_C.PCClient 
{
    public partial class FormBase : Form
    {
        private Form _origen;

        public FormBase()
        {
            InitializeComponent();
            _origen = null;
            this.BackColor = Properties.Settings.Default.BackgroundColor;
        }

        public void ShowFrom(Form origen)
        {
            _origen = origen;
            _origen.Hide();
            this.Show();
        }

        public DialogResult DialogFrom(Form origen)
        {
            _origen = origen;
            _origen.Hide();
            return this.ShowDialog();
        }
        private void FormBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_origen != null)
                _origen.Show();
        }

        protected DateTime Today
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// Ultimo día del mes anterior a hoy
        /// </summary>
        protected DateTime LastDayPM
        {
            get { return new DateTime(Today.AddMonths(-1).Year, Today.AddMonths(-1).Month, 1).AddMonths(1).AddDays(-1); }
        }

        /// <summary>
        /// Ultimo dia del mes en curso
        /// </summary>
        protected DateTime LastDayTM
        {
            get { return new DateTime(Today.Year, Today.Month, 1).AddMonths(1).AddDays(-1); }
        }

        private void FormBase_Load(object sender, EventArgs e)
        {
            foreach (Control cnt in this.Controls)
            {
                if (cnt.GetType() == typeof(ComboBox))
                    ((ComboBox)cnt).DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        protected bool validar_formulario()
        {
            foreach (Control cnt in this.Controls)
            {
                if (cnt.Enabled)
                {
                    if (cnt.GetType() == typeof(ComboBox))
                    {
                        var combo = (ComboBox)cnt;
                        if (combo.SelectedIndex < 0)
                        {
                            Mensaje.Advert("Debe seleccionar una opción para " + combo.Tag.ToString());
                            combo.Focus();
                            return false;
                        }
                    }
                    if (cnt.GetType() == typeof(ListBox))
                    {
                        var listbox = (ListBox)cnt;
                        if (listbox.SelectedIndex < 0)
                        {
                            Mensaje.Advert("Debe seleccionar una opción para " + listbox.Tag.ToString());
                            listbox.Focus();
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        protected class Mensaje
        {
            static string titulo = "ACTIVO FIJO NH FOODS";
            public static void Info(string mensaje)
            {
                MessageBox.Show(mensaje, titulo,MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

            public static void Error(string mensaje)
            {
                MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            public static void Advert(string mensaje)
            {
                MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
