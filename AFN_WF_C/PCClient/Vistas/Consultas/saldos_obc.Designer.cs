namespace AFN_WF_C.PCClient.Vistas.Consultas
{
    partial class saldos_obc
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_resultado = new System.Windows.Forms.ComboBox();
            this.cb_year = new System.Windows.Forms.ComboBox();
            this.cb_month = new System.Windows.Forms.ComboBox();
            this.cb_moneda = new System.Windows.Forms.ComboBox();
            this.cb_acum = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Resultado";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Periodo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Moneda";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Acumulado";
            // 
            // cb_resultado
            // 
            this.cb_resultado.FormattingEnabled = true;
            this.cb_resultado.Location = new System.Drawing.Point(177, 32);
            this.cb_resultado.Name = "cb_resultado";
            this.cb_resultado.Size = new System.Drawing.Size(258, 24);
            this.cb_resultado.TabIndex = 4;
            // 
            // cb_year
            // 
            this.cb_year.FormattingEnabled = true;
            this.cb_year.Location = new System.Drawing.Point(177, 73);
            this.cb_year.Name = "cb_year";
            this.cb_year.Size = new System.Drawing.Size(81, 24);
            this.cb_year.TabIndex = 5;
            // 
            // cb_month
            // 
            this.cb_month.FormattingEnabled = true;
            this.cb_month.Location = new System.Drawing.Point(274, 73);
            this.cb_month.Name = "cb_month";
            this.cb_month.Size = new System.Drawing.Size(161, 24);
            this.cb_month.TabIndex = 6;
            // 
            // cb_moneda
            // 
            this.cb_moneda.FormattingEnabled = true;
            this.cb_moneda.Location = new System.Drawing.Point(177, 112);
            this.cb_moneda.Name = "cb_moneda";
            this.cb_moneda.Size = new System.Drawing.Size(258, 24);
            this.cb_moneda.TabIndex = 7;
            // 
            // cb_acum
            // 
            this.cb_acum.FormattingEnabled = true;
            this.cb_acum.Location = new System.Drawing.Point(177, 154);
            this.cb_acum.Name = "cb_acum";
            this.cb_acum.Size = new System.Drawing.Size(258, 24);
            this.cb_acum.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(186, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 42);
            this.button1.TabIndex = 9;
            this.button1.Text = "Consultar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // saldos_obc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(490, 295);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cb_acum);
            this.Controls.Add(this.cb_moneda);
            this.Controls.Add(this.cb_month);
            this.Controls.Add(this.cb_year);
            this.Controls.Add(this.cb_resultado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "saldos_obc";
            this.Padding = new System.Windows.Forms.Padding(36, 31, 36, 31);
            this.Text = "Consultas Obras en Contrucción";
            this.Load += new System.EventHandler(this.saldos_obc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_resultado;
        private System.Windows.Forms.ComboBox cb_year;
        private System.Windows.Forms.ComboBox cb_month;
        private System.Windows.Forms.ComboBox cb_moneda;
        private System.Windows.Forms.ComboBox cb_acum;
        private System.Windows.Forms.Button button1;
    }
}
