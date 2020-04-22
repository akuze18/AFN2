namespace AFN_WF_C.PCClient.Vistas.Reportes
{
    partial class bajas
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
            this.lb_reporte = new System.Windows.Forms.ListBox();
            this.cb_desde_y = new System.Windows.Forms.ComboBox();
            this.cb_hasta_y = new System.Windows.Forms.ComboBox();
            this.cb_desde_m = new System.Windows.Forms.ComboBox();
            this.cb_hasta_m = new System.Windows.Forms.ComboBox();
            this.cb_situacion = new System.Windows.Forms.ComboBox();
            this.btn_generar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_acum = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Desde";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hasta";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 149);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Situacion";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(523, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Reportes Disponibles";
            // 
            // lb_reporte
            // 
            this.lb_reporte.FormattingEnabled = true;
            this.lb_reporte.ItemHeight = 16;
            this.lb_reporte.Location = new System.Drawing.Point(527, 54);
            this.lb_reporte.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lb_reporte.Name = "lb_reporte";
            this.lb_reporte.Size = new System.Drawing.Size(323, 148);
            this.lb_reporte.TabIndex = 11;
            // 
            // cb_desde_y
            // 
            this.cb_desde_y.FormattingEnabled = true;
            this.cb_desde_y.Location = new System.Drawing.Point(165, 59);
            this.cb_desde_y.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_desde_y.Name = "cb_desde_y";
            this.cb_desde_y.Size = new System.Drawing.Size(83, 24);
            this.cb_desde_y.TabIndex = 1;
            // 
            // cb_hasta_y
            // 
            this.cb_hasta_y.FormattingEnabled = true;
            this.cb_hasta_y.Location = new System.Drawing.Point(165, 101);
            this.cb_hasta_y.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_hasta_y.Name = "cb_hasta_y";
            this.cb_hasta_y.Size = new System.Drawing.Size(83, 24);
            this.cb_hasta_y.TabIndex = 4;
            // 
            // cb_desde_m
            // 
            this.cb_desde_m.FormattingEnabled = true;
            this.cb_desde_m.Location = new System.Drawing.Point(269, 59);
            this.cb_desde_m.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_desde_m.Name = "cb_desde_m";
            this.cb_desde_m.Size = new System.Drawing.Size(165, 24);
            this.cb_desde_m.TabIndex = 2;
            // 
            // cb_hasta_m
            // 
            this.cb_hasta_m.FormattingEnabled = true;
            this.cb_hasta_m.Location = new System.Drawing.Point(269, 101);
            this.cb_hasta_m.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_hasta_m.Name = "cb_hasta_m";
            this.cb_hasta_m.Size = new System.Drawing.Size(165, 24);
            this.cb_hasta_m.TabIndex = 5;
            // 
            // cb_situacion
            // 
            this.cb_situacion.FormattingEnabled = true;
            this.cb_situacion.Location = new System.Drawing.Point(165, 149);
            this.cb_situacion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_situacion.Name = "cb_situacion";
            this.cb_situacion.Size = new System.Drawing.Size(269, 24);
            this.cb_situacion.TabIndex = 7;
            // 
            // btn_generar
            // 
            this.btn_generar.Location = new System.Drawing.Point(371, 268);
            this.btn_generar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_generar.Name = "btn_generar";
            this.btn_generar.Size = new System.Drawing.Size(121, 28);
            this.btn_generar.TabIndex = 12;
            this.btn_generar.Text = "Visualizar";
            this.btn_generar.UseVisualStyleBackColor = true;
            this.btn_generar.Click += new System.EventHandler(this.btn_generar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 207);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Acumulado";
            // 
            // cb_acum
            // 
            this.cb_acum.FormattingEnabled = true;
            this.cb_acum.Location = new System.Drawing.Point(165, 201);
            this.cb_acum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cb_acum.Name = "cb_acum";
            this.cb_acum.Size = new System.Drawing.Size(269, 24);
            this.cb_acum.TabIndex = 9;
            // 
            // bajas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(924, 325);
            this.Controls.Add(this.cb_acum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_generar);
            this.Controls.Add(this.cb_situacion);
            this.Controls.Add(this.cb_hasta_m);
            this.Controls.Add(this.cb_desde_m);
            this.Controls.Add(this.cb_hasta_y);
            this.Controls.Add(this.cb_desde_y);
            this.Controls.Add(this.lb_reporte);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "bajas";
            this.Padding = new System.Windows.Forms.Padding(36, 31, 36, 31);
            this.Text = "Activo Fijo en Estado de Baja";
            this.Load += new System.EventHandler(this.bajas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lb_reporte;
        private System.Windows.Forms.ComboBox cb_desde_y;
        private System.Windows.Forms.ComboBox cb_hasta_y;
        private System.Windows.Forms.ComboBox cb_desde_m;
        private System.Windows.Forms.ComboBox cb_hasta_m;
        private System.Windows.Forms.ComboBox cb_situacion;
        private System.Windows.Forms.Button btn_generar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_acum;
    }
}
