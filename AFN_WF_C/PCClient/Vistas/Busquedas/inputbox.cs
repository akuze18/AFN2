using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AFN_WF_C.PCClient.Vistas.Busquedas
{
    public partial class inputbox : Form
    {
        public string TextoIngresado;

        public inputbox()
        {
            InitializeComponent();
        }

        public inputbox(string Title, string Prompt, string DefaultResponse = "")
        {
            InitializeComponent();
            this.Text = Title;
            label1.Text = Prompt;
            textBox1.Text = DefaultResponse;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextoIngresado = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

 

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            var ExitOkKeys = new List<Keys>() { Keys.Enter };
            var ExitCancelKeys = new List<Keys>() { Keys.Escape };
            if (ExitOkKeys.Contains(e.KeyCode))
            {
                button1_Click(sender,null);
            }
            if (ExitCancelKeys.Contains(e.KeyCode))
            {
                button2_Click(sender, null);
            }
        }
    }
}
