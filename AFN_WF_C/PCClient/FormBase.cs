using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Configuration;

using AFN_WF_C.PCClient.Procesos;


namespace AFN_WF_C.PCClient 
{
    public partial class FormBase : Form
    {
        private Form _origen;
        //private bool _upgrading;

        public FormBase()
        {
            InitializeComponent();
            _origen = null;
            //_upgrading = false;
            this.BackColor = Properties.Settings.Default.BackgroundColor;
        }

        public void ShowFrom(Form origen)
        {
            _origen = origen;
            _origen.Hide();
            this.Show();
        }

        public DialogResult ShowDialogFrom(Form origen)
        {
            _origen = origen;
            _origen.Hide();
            return this.ShowDialog();
        }
        private void FormBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_origen != null) // && !_upgrading
                _origen.Show();
        }

        protected DateTime Today
        {
            get { return DateTime.Today; }
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

        protected bool ChangeOrigen(Form NuevoOrigen)
        {
            try
            {
                if (_origen != null && NuevoOrigen != null)
                {
                    _origen.Close();
                }
                _origen = NuevoOrigen;
                return true;
            }
            catch (Exception e)
            {
                Mensaje.Error(e.StackTrace);
                return false;
            }
        }
        
        new public Form Parent
        {
            get { return _origen; }
        }
    }
}
