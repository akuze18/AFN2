namespace AFN_WF_C.PCClient.Vistas.Reportes
{
    partial class vigentes
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_detalle = new System.Windows.Forms.Button();
            this.listaReporte = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_year = new System.Windows.Forms.ComboBox();
            this.cb_clase = new System.Windows.Forms.ComboBox();
            this.cb_zona = new System.Windows.Forms.ComboBox();
            this.cb_acum = new System.Windows.Forms.ComboBox();
            this.cb_month = new System.Windows.Forms.ComboBox();
            this.groupVisualizar = new System.Windows.Forms.GroupBox();
            this.button_det_inv = new System.Windows.Forms.Button();
            this.button_resumen_z = new System.Windows.Forms.Button();
            this.button_resumen_c = new System.Windows.Forms.Button();
            this.groupVisualizar.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Periodo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 198);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Acumulado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 151);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Zona";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Clase";
            // 
            // button_detalle
            // 
            this.button_detalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_detalle.Location = new System.Drawing.Point(31, 23);
            this.button_detalle.Margin = new System.Windows.Forms.Padding(4);
            this.button_detalle.Name = "button_detalle";
            this.button_detalle.Size = new System.Drawing.Size(100, 60);
            this.button_detalle.TabIndex = 10;
            this.button_detalle.Text = "Detalle";
            this.button_detalle.UseVisualStyleBackColor = true;
            this.button_detalle.Click += new System.EventHandler(this.button_detalle_Click);
            // 
            // listaReporte
            // 
            this.listaReporte.FormattingEnabled = true;
            this.listaReporte.ItemHeight = 16;
            this.listaReporte.Location = new System.Drawing.Point(493, 69);
            this.listaReporte.Margin = new System.Windows.Forms.Padding(4);
            this.listaReporte.Name = "listaReporte";
            this.listaReporte.Size = new System.Drawing.Size(299, 148);
            this.listaReporte.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(476, 25);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Reporte";
            // 
            // cb_year
            // 
            this.cb_year.FormattingEnabled = true;
            this.cb_year.Location = new System.Drawing.Point(181, 63);
            this.cb_year.Margin = new System.Windows.Forms.Padding(4);
            this.cb_year.Name = "cb_year";
            this.cb_year.Size = new System.Drawing.Size(71, 24);
            this.cb_year.TabIndex = 1;
            // 
            // cb_clase
            // 
            this.cb_clase.FormattingEnabled = true;
            this.cb_clase.Location = new System.Drawing.Point(181, 107);
            this.cb_clase.Margin = new System.Windows.Forms.Padding(4);
            this.cb_clase.Name = "cb_clase";
            this.cb_clase.Size = new System.Drawing.Size(241, 24);
            this.cb_clase.TabIndex = 3;
            // 
            // cb_zona
            // 
            this.cb_zona.FormattingEnabled = true;
            this.cb_zona.Location = new System.Drawing.Point(181, 151);
            this.cb_zona.Margin = new System.Windows.Forms.Padding(4);
            this.cb_zona.Name = "cb_zona";
            this.cb_zona.Size = new System.Drawing.Size(241, 24);
            this.cb_zona.TabIndex = 5;
            // 
            // cb_acum
            // 
            this.cb_acum.FormattingEnabled = true;
            this.cb_acum.Location = new System.Drawing.Point(181, 197);
            this.cb_acum.Margin = new System.Windows.Forms.Padding(4);
            this.cb_acum.Name = "cb_acum";
            this.cb_acum.Size = new System.Drawing.Size(241, 24);
            this.cb_acum.TabIndex = 7;
            // 
            // cb_month
            // 
            this.cb_month.FormattingEnabled = true;
            this.cb_month.Location = new System.Drawing.Point(261, 63);
            this.cb_month.Margin = new System.Windows.Forms.Padding(4);
            this.cb_month.Name = "cb_month";
            this.cb_month.Size = new System.Drawing.Size(161, 24);
            this.cb_month.TabIndex = 11;
            // 
            // groupVisualizar
            // 
            this.groupVisualizar.Controls.Add(this.button_det_inv);
            this.groupVisualizar.Controls.Add(this.button_resumen_z);
            this.groupVisualizar.Controls.Add(this.button_resumen_c);
            this.groupVisualizar.Controls.Add(this.button_detalle);
            this.groupVisualizar.Location = new System.Drawing.Point(132, 251);
            this.groupVisualizar.Margin = new System.Windows.Forms.Padding(4);
            this.groupVisualizar.Name = "groupVisualizar";
            this.groupVisualizar.Padding = new System.Windows.Forms.Padding(4);
            this.groupVisualizar.Size = new System.Drawing.Size(505, 106);
            this.groupVisualizar.TabIndex = 12;
            this.groupVisualizar.TabStop = false;
            this.groupVisualizar.Text = "Visualizar";
            // 
            // button_det_inv
            // 
            this.button_det_inv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_det_inv.Location = new System.Drawing.Point(376, 23);
            this.button_det_inv.Margin = new System.Windows.Forms.Padding(4);
            this.button_det_inv.Name = "button_det_inv";
            this.button_det_inv.Size = new System.Drawing.Size(100, 60);
            this.button_det_inv.TabIndex = 13;
            this.button_det_inv.Text = "Detalle c/ Inventario";
            this.button_det_inv.UseVisualStyleBackColor = true;
            this.button_det_inv.Click += new System.EventHandler(this.button_det_inv_Click);
            // 
            // button_resumen_z
            // 
            this.button_resumen_z.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_resumen_z.Location = new System.Drawing.Point(261, 23);
            this.button_resumen_z.Margin = new System.Windows.Forms.Padding(4);
            this.button_resumen_z.Name = "button_resumen_z";
            this.button_resumen_z.Size = new System.Drawing.Size(100, 60);
            this.button_resumen_z.TabIndex = 12;
            this.button_resumen_z.Text = "Resumen por Zona";
            this.button_resumen_z.UseVisualStyleBackColor = true;
            this.button_resumen_z.Click += new System.EventHandler(this.button_resumen_z_Click);
            // 
            // button_resumen_c
            // 
            this.button_resumen_c.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_resumen_c.Location = new System.Drawing.Point(145, 23);
            this.button_resumen_c.Margin = new System.Windows.Forms.Padding(4);
            this.button_resumen_c.Name = "button_resumen_c";
            this.button_resumen_c.Size = new System.Drawing.Size(100, 60);
            this.button_resumen_c.TabIndex = 11;
            this.button_resumen_c.Text = "Resumen por Clase";
            this.button_resumen_c.UseVisualStyleBackColor = true;
            this.button_resumen_c.Click += new System.EventHandler(this.button_resumen_c_Click);
            // 
            // vigentes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(852, 385);
            this.Controls.Add(this.groupVisualizar);
            this.Controls.Add(this.cb_month);
            this.Controls.Add(this.cb_acum);
            this.Controls.Add(this.cb_zona);
            this.Controls.Add(this.cb_clase);
            this.Controls.Add(this.cb_year);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listaReporte);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "vigentes";
            this.Padding = new System.Windows.Forms.Padding(36, 31, 36, 31);
            this.Text = "Activo Fijo en Estado Vigente";
            this.Load += new System.EventHandler(this.vigentes_Load);
            this.groupVisualizar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_detalle;
        private System.Windows.Forms.ListBox listaReporte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_year;
        private System.Windows.Forms.ComboBox cb_clase;
        private System.Windows.Forms.ComboBox cb_zona;
        private System.Windows.Forms.ComboBox cb_acum;
        private System.Windows.Forms.ComboBox cb_month;
        private System.Windows.Forms.GroupBox groupVisualizar;
        private System.Windows.Forms.Button button_resumen_z;
        private System.Windows.Forms.Button button_resumen_c;
        private System.Windows.Forms.Button button_det_inv;
    }
}
