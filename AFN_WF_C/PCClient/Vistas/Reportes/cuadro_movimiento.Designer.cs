﻿namespace AFN_WF_C.PCClient.Vistas.Reportes
{
    partial class cuadro_movimiento
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
            this.btn_generar = new System.Windows.Forms.Button();
            this.cb_year = new System.Windows.Forms.ComboBox();
            this.cb_month = new System.Windows.Forms.ComboBox();
            this.cb_tipo = new System.Windows.Forms.ComboBox();
            this.cb_acum = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_sistema = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Periodo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tipo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 133);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Acumulado";
            // 
            // btn_generar
            // 
            this.btn_generar.Location = new System.Drawing.Point(206, 234);
            this.btn_generar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_generar.Name = "btn_generar";
            this.btn_generar.Size = new System.Drawing.Size(123, 46);
            this.btn_generar.TabIndex = 3;
            this.btn_generar.Text = "Visualizar";
            this.btn_generar.UseVisualStyleBackColor = true;
            this.btn_generar.Click += new System.EventHandler(this.btn_generar_Click);
            // 
            // cb_year
            // 
            this.cb_year.FormattingEnabled = true;
            this.cb_year.Location = new System.Drawing.Point(141, 36);
            this.cb_year.Margin = new System.Windows.Forms.Padding(4);
            this.cb_year.Name = "cb_year";
            this.cb_year.Size = new System.Drawing.Size(93, 24);
            this.cb_year.TabIndex = 4;
            // 
            // cb_month
            // 
            this.cb_month.FormattingEnabled = true;
            this.cb_month.Location = new System.Drawing.Point(263, 37);
            this.cb_month.Margin = new System.Windows.Forms.Padding(4);
            this.cb_month.Name = "cb_month";
            this.cb_month.Size = new System.Drawing.Size(232, 24);
            this.cb_month.TabIndex = 5;
            // 
            // cb_tipo
            // 
            this.cb_tipo.FormattingEnabled = true;
            this.cb_tipo.Location = new System.Drawing.Point(142, 79);
            this.cb_tipo.Margin = new System.Windows.Forms.Padding(4);
            this.cb_tipo.Name = "cb_tipo";
            this.cb_tipo.Size = new System.Drawing.Size(353, 24);
            this.cb_tipo.TabIndex = 6;
            // 
            // cb_acum
            // 
            this.cb_acum.FormattingEnabled = true;
            this.cb_acum.Location = new System.Drawing.Point(143, 127);
            this.cb_acum.Margin = new System.Windows.Forms.Padding(4);
            this.cb_acum.Name = "cb_acum";
            this.cb_acum.Size = new System.Drawing.Size(353, 24);
            this.cb_acum.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 176);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Reporte";
            // 
            // cb_sistema
            // 
            this.cb_sistema.FormattingEnabled = true;
            this.cb_sistema.Location = new System.Drawing.Point(146, 176);
            this.cb_sistema.Margin = new System.Windows.Forms.Padding(4);
            this.cb_sistema.Name = "cb_sistema";
            this.cb_sistema.Size = new System.Drawing.Size(353, 24);
            this.cb_sistema.TabIndex = 9;
            // 
            // cuadro_movimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(532, 298);
            this.Controls.Add(this.cb_sistema);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cb_acum);
            this.Controls.Add(this.cb_tipo);
            this.Controls.Add(this.cb_month);
            this.Controls.Add(this.cb_year);
            this.Controls.Add(this.btn_generar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "cuadro_movimiento";
            this.Padding = new System.Windows.Forms.Padding(36, 31, 36, 31);
            this.Text = "Reporte Cuadro Movimiento";
            this.Load += new System.EventHandler(this.cuadro_movimiento_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_generar;
        private System.Windows.Forms.ComboBox cb_year;
        private System.Windows.Forms.ComboBox cb_month;
        private System.Windows.Forms.ComboBox cb_tipo;
        private System.Windows.Forms.ComboBox cb_acum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_sistema;
    }
}
