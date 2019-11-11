namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    partial class ingreso_ifrs
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_IFRS = new System.Windows.Forms.Button();
            this.Frame2 = new System.Windows.Forms.GroupBox();
            this.DataIFRS = new System.Windows.Forms.DataGridView();
            this.xt = new System.Windows.Forms.TextBox();
            this.cboMetod = new System.Windows.Forms.ComboBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.Tval_resI = new System.Windows.Forms.TextBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.TvuI = new System.Windows.Forms.TextBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.Frame2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataIFRS)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_IFRS
            // 
            this.btn_IFRS.Image = global::AFN_WF_C.Properties.Resources._32_next;
            this.btn_IFRS.Location = new System.Drawing.Point(337, 291);
            this.btn_IFRS.Name = "btn_IFRS";
            this.btn_IFRS.Size = new System.Drawing.Size(86, 55);
            this.btn_IFRS.TabIndex = 17;
            this.btn_IFRS.Text = "Guardar";
            this.btn_IFRS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_IFRS.UseVisualStyleBackColor = true;
            this.btn_IFRS.Click += new System.EventHandler(this.btn_IFRS_Click);
            // 
            // Frame2
            // 
            this.Frame2.Controls.Add(this.DataIFRS);
            this.Frame2.Location = new System.Drawing.Point(85, 61);
            this.Frame2.Name = "Frame2";
            this.Frame2.Size = new System.Drawing.Size(588, 213);
            this.Frame2.TabIndex = 16;
            this.Frame2.TabStop = false;
            this.Frame2.Text = "Valorización Inicial";
            // 
            // DataIFRS
            // 
            this.DataIFRS.AllowUserToAddRows = false;
            this.DataIFRS.AllowUserToDeleteRows = false;
            this.DataIFRS.AllowUserToResizeColumns = false;
            this.DataIFRS.AllowUserToResizeRows = false;
            this.DataIFRS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataIFRS.Location = new System.Drawing.Point(42, 30);
            this.DataIFRS.Name = "DataIFRS";
            this.DataIFRS.ReadOnly = true;
            this.DataIFRS.RowHeadersVisible = false;
            this.DataIFRS.Size = new System.Drawing.Size(513, 156);
            this.DataIFRS.TabIndex = 20;
            this.DataIFRS.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataIFRS_CellDoubleClick);
            // 
            // xt
            // 
            this.xt.Location = new System.Drawing.Point(702, 145);
            this.xt.Name = "xt";
            this.xt.Size = new System.Drawing.Size(14, 20);
            this.xt.TabIndex = 15;
            this.xt.Text = "xt";
            this.xt.Visible = false;
            // 
            // cboMetod
            // 
            this.cboMetod.FormattingEnabled = true;
            this.cboMetod.Location = new System.Drawing.Point(394, 20);
            this.cboMetod.Name = "cboMetod";
            this.cboMetod.Size = new System.Drawing.Size(149, 21);
            this.cboMetod.TabIndex = 12;
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(270, 21);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(118, 13);
            this.Label20.TabIndex = 11;
            this.Label20.Text = "Método de Valorización";
            // 
            // Tval_resI
            // 
            this.Tval_resI.Location = new System.Drawing.Point(646, 17);
            this.Tval_resI.Name = "Tval_resI";
            this.Tval_resI.Size = new System.Drawing.Size(112, 20);
            this.Tval_resI.TabIndex = 14;
            this.Tval_resI.Enter += new System.EventHandler(this.Tval_resI_GotFocus);
            this.Tval_resI.Leave += new System.EventHandler(this.Tval_resI_LostFocus);
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(565, 20);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(75, 13);
            this.Label19.TabIndex = 13;
            this.Label19.Text = "Valor Residual";
            // 
            // TvuI
            // 
            this.TvuI.Location = new System.Drawing.Point(134, 20);
            this.TvuI.Name = "TvuI";
            this.TvuI.Size = new System.Drawing.Size(111, 20);
            this.TvuI.TabIndex = 10;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(20, 23);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(103, 13);
            this.Label18.TabIndex = 9;
            this.Label18.Text = "Vida Util IFRS (días)";
            // 
            // ingreso_ifrs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_IFRS);
            this.Controls.Add(this.Frame2);
            this.Controls.Add(this.xt);
            this.Controls.Add(this.cboMetod);
            this.Controls.Add(this.Label20);
            this.Controls.Add(this.Tval_resI);
            this.Controls.Add(this.Label19);
            this.Controls.Add(this.TvuI);
            this.Controls.Add(this.Label18);
            this.Name = "ingreso_ifrs";
            this.Size = new System.Drawing.Size(826, 386);
            this.Load += new System.EventHandler(this.ingreso_ifrs_Load);
            this.Frame2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataIFRS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btn_IFRS;
        internal System.Windows.Forms.GroupBox Frame2;
        internal System.Windows.Forms.DataGridView DataIFRS;
        internal System.Windows.Forms.TextBox xt;
        internal System.Windows.Forms.ComboBox cboMetod;
        internal System.Windows.Forms.Label Label20;
        internal System.Windows.Forms.TextBox Tval_resI;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.TextBox TvuI;
        internal System.Windows.Forms.Label Label18;

    }
}
