namespace AFN_WF_C.PCClient.Vistas.Reportes
{
    partial class contabilizar
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
            this.btn_calcular = new System.Windows.Forms.Button();
            this.btn_ubicar = new System.Windows.Forms.Button();
            this.Tubicacion = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.cb_month = new System.Windows.Forms.ComboBox();
            this.cb_year = new System.Windows.Forms.ComboBox();
            this.dialogo = new System.Windows.Forms.SaveFileDialog();
            this.BGQuery = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btn_calcular
            // 
            this.btn_calcular.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_calcular.Location = new System.Drawing.Point(146, 124);
            this.btn_calcular.Name = "btn_calcular";
            this.btn_calcular.Size = new System.Drawing.Size(65, 39);
            this.btn_calcular.TabIndex = 11;
            this.btn_calcular.Text = "Emitir";
            this.btn_calcular.UseVisualStyleBackColor = true;
            this.btn_calcular.Click += new System.EventHandler(this.btn_calcular_Click);
            // 
            // btn_ubicar
            // 
            this.btn_ubicar.Image = global::AFN_WF_C.Properties.Resources.find;
            this.btn_ubicar.Location = new System.Drawing.Point(319, 66);
            this.btn_ubicar.Name = "btn_ubicar";
            this.btn_ubicar.Size = new System.Drawing.Size(34, 32);
            this.btn_ubicar.TabIndex = 10;
            this.btn_ubicar.UseVisualStyleBackColor = true;
            this.btn_ubicar.Click += new System.EventHandler(this.btn_ubicar_Click);
            // 
            // Tubicacion
            // 
            this.Tubicacion.Location = new System.Drawing.Point(98, 73);
            this.Tubicacion.Name = "Tubicacion";
            this.Tubicacion.Size = new System.Drawing.Size(215, 22);
            this.Tubicacion.TabIndex = 8;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(23, 73);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(69, 16);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "Ubicacion";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(23, 30);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(56, 16);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "Periodo";
            // 
            // cb_month
            // 
            this.cb_month.FormattingEnabled = true;
            this.cb_month.Location = new System.Drawing.Point(164, 29);
            this.cb_month.Margin = new System.Windows.Forms.Padding(4);
            this.cb_month.Name = "cb_month";
            this.cb_month.Size = new System.Drawing.Size(189, 24);
            this.cb_month.TabIndex = 13;
            // 
            // cb_year
            // 
            this.cb_year.FormattingEnabled = true;
            this.cb_year.Location = new System.Drawing.Point(98, 29);
            this.cb_year.Margin = new System.Windows.Forms.Padding(4);
            this.cb_year.Name = "cb_year";
            this.cb_year.Size = new System.Drawing.Size(58, 24);
            this.cb_year.TabIndex = 12;
            // 
            // BGQuery
            // 
            this.BGQuery.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGQuery_DoWork);
            this.BGQuery.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGQuery_RunWorkerCompleted);
            // 
            // contabilizar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(377, 191);
            this.Controls.Add(this.cb_month);
            this.Controls.Add(this.cb_year);
            this.Controls.Add(this.btn_calcular);
            this.Controls.Add(this.btn_ubicar);
            this.Controls.Add(this.Tubicacion);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Name = "contabilizar";
            this.Text = "Contabilización de Activo Fijo";
            this.Load += new System.EventHandler(this.contabilizar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btn_calcular;
        internal System.Windows.Forms.Button btn_ubicar;
        internal System.Windows.Forms.TextBox Tubicacion;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.ComboBox cb_month;
        private System.Windows.Forms.ComboBox cb_year;
        private System.Windows.Forms.SaveFileDialog dialogo;
        private System.ComponentModel.BackgroundWorker BGQuery;
    }
}
