namespace AFN_WF_C.PCClient.Vistas.Busquedas
{
    partial class manager_det_articulo
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
            this.btn_clear = new System.Windows.Forms.Button();
            this.mark_total = new System.Windows.Forms.Label();
            this.mark_actual = new System.Windows.Forms.Label();
            this.btn_top = new System.Windows.Forms.Button();
            this.btn_less = new System.Windows.Forms.Button();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.LBdescrip = new System.Windows.Forms.Label();
            this.DG_articulos = new System.Windows.Forms.DataGridView();
            this.TB_cod_lote = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.RBclear = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.DG_articulos)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_clear
            // 
            this.btn_clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_clear.Location = new System.Drawing.Point(243, 13);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(55, 30);
            this.btn_clear.TabIndex = 14;
            this.btn_clear.Text = "Button1";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // mark_total
            // 
            this.mark_total.AutoSize = true;
            this.mark_total.Location = new System.Drawing.Point(366, 91);
            this.mark_total.Name = "mark_total";
            this.mark_total.Size = new System.Drawing.Size(49, 16);
            this.mark_total.TabIndex = 19;
            this.mark_total.Text = "Label3";
            // 
            // mark_actual
            // 
            this.mark_actual.AutoSize = true;
            this.mark_actual.Location = new System.Drawing.Point(307, 91);
            this.mark_actual.Name = "mark_actual";
            this.mark_actual.Size = new System.Drawing.Size(49, 16);
            this.mark_actual.TabIndex = 18;
            this.mark_actual.Text = "Label2";
            // 
            // btn_top
            // 
            this.btn_top.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_top.Location = new System.Drawing.Point(364, 13);
            this.btn_top.Name = "btn_top";
            this.btn_top.Size = new System.Drawing.Size(55, 30);
            this.btn_top.TabIndex = 16;
            this.btn_top.Text = "Sel Sup";
            this.btn_top.UseVisualStyleBackColor = true;
            this.btn_top.Click += new System.EventHandler(this.btn_top_Click);
            // 
            // btn_less
            // 
            this.btn_less.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_less.Location = new System.Drawing.Point(304, 13);
            this.btn_less.Name = "btn_less";
            this.btn_less.Size = new System.Drawing.Size(55, 30);
            this.btn_less.TabIndex = 15;
            this.btn_less.Text = "Button2";
            this.btn_less.UseVisualStyleBackColor = true;
            this.btn_less.Click += new System.EventHandler(this.btn_less_Click);
            // 
            // btn_guardar
            // 
            this.btn_guardar.Location = new System.Drawing.Point(304, 53);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(115, 30);
            this.btn_guardar.TabIndex = 17;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // LBdescrip
            // 
            this.LBdescrip.Location = new System.Drawing.Point(8, 52);
            this.LBdescrip.Name = "LBdescrip";
            this.LBdescrip.Size = new System.Drawing.Size(290, 55);
            this.LBdescrip.TabIndex = 13;
            this.LBdescrip.Text = "Label2";
            // 
            // DG_articulos
            // 
            this.DG_articulos.AllowUserToAddRows = false;
            this.DG_articulos.AllowUserToDeleteRows = false;
            this.DG_articulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_articulos.Location = new System.Drawing.Point(20, 116);
            this.DG_articulos.Name = "DG_articulos";
            this.DG_articulos.ReadOnly = true;
            this.DG_articulos.Size = new System.Drawing.Size(387, 419);
            this.DG_articulos.TabIndex = 20;
            this.DG_articulos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DG_articulos_CellClick);
            // 
            // TB_cod_lote
            // 
            this.TB_cod_lote.Location = new System.Drawing.Point(114, 13);
            this.TB_cod_lote.Name = "TB_cod_lote";
            this.TB_cod_lote.Size = new System.Drawing.Size(79, 22);
            this.TB_cod_lote.TabIndex = 12;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(8, 16);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(100, 16);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "Código de Lote";
            // 
            // RBclear
            // 
            this.RBclear.AutoSize = true;
            this.RBclear.Location = new System.Drawing.Point(273, 208);
            this.RBclear.Name = "RBclear";
            this.RBclear.Size = new System.Drawing.Size(26, 20);
            this.RBclear.TabIndex = 21;
            this.RBclear.TabStop = true;
            this.RBclear.Text = "\r\n";
            this.RBclear.UseVisualStyleBackColor = true;
            // 
            // manager_det_articulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(437, 558);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.mark_total);
            this.Controls.Add(this.mark_actual);
            this.Controls.Add(this.btn_top);
            this.Controls.Add(this.btn_less);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this.LBdescrip);
            this.Controls.Add(this.DG_articulos);
            this.Controls.Add(this.TB_cod_lote);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.RBclear);
            this.Name = "manager_det_articulo";
            this.Load += new System.EventHandler(this.manager_det_articulo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DG_articulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btn_clear;
        internal System.Windows.Forms.Label mark_total;
        internal System.Windows.Forms.Label mark_actual;
        internal System.Windows.Forms.Button btn_top;
        internal System.Windows.Forms.Button btn_less;
        internal System.Windows.Forms.Button btn_guardar;
        internal System.Windows.Forms.Label LBdescrip;
        internal System.Windows.Forms.DataGridView DG_articulos;
        internal System.Windows.Forms.TextBox TB_cod_lote;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.RadioButton RBclear;
    }
}
