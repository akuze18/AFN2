namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    partial class ingreso_invent_articulo
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
            this.ckMostrarA = new System.Windows.Forms.CheckBox();
            this.AtribArticulo = new System.Windows.Forms.DataGridView();
            this.cbAatrib = new System.Windows.Forms.ComboBox();
            this.Label33 = new System.Windows.Forms.Label();
            this.btn_imprimir1 = new System.Windows.Forms.Button();
            this.btn_detallexA = new System.Windows.Forms.Button();
            this.cbAvalor = new System.Windows.Forms.ComboBox();
            this.Label32 = new System.Windows.Forms.Label();
            this.cblistaArticulo = new System.Windows.Forms.ComboBox();
            this.Label31 = new System.Windows.Forms.Label();
            this.TAvalor = new System.Windows.Forms.TextBox();
            this.btn_lessDA = new System.Windows.Forms.Button();
            this.btn_addDA = new System.Windows.Forms.Button();
            this.btn_buscaA = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AtribArticulo)).BeginInit();
            this.SuspendLayout();
            // 
            // ckMostrarA
            // 
            this.ckMostrarA.AutoSize = true;
            this.ckMostrarA.Location = new System.Drawing.Point(414, 78);
            this.ckMostrarA.Name = "ckMostrarA";
            this.ckMostrarA.Size = new System.Drawing.Size(118, 17);
            this.ckMostrarA.TabIndex = 22;
            this.ckMostrarA.Text = "Mostrar en Etiqueta";
            this.ckMostrarA.UseVisualStyleBackColor = true;
            // 
            // AtribArticulo
            // 
            this.AtribArticulo.AllowUserToAddRows = false;
            this.AtribArticulo.AllowUserToDeleteRows = false;
            this.AtribArticulo.AllowUserToResizeColumns = false;
            this.AtribArticulo.AllowUserToResizeRows = false;
            this.AtribArticulo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AtribArticulo.Location = new System.Drawing.Point(35, 158);
            this.AtribArticulo.Name = "AtribArticulo";
            this.AtribArticulo.ReadOnly = true;
            this.AtribArticulo.RowHeadersWidth = 20;
            this.AtribArticulo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AtribArticulo.Size = new System.Drawing.Size(752, 195);
            this.AtribArticulo.TabIndex = 27;
            // 
            // cbAatrib
            // 
            this.cbAatrib.FormattingEnabled = true;
            this.cbAatrib.Location = new System.Drawing.Point(125, 72);
            this.cbAatrib.Name = "cbAatrib";
            this.cbAatrib.Size = new System.Drawing.Size(156, 21);
            this.cbAatrib.TabIndex = 17;
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label33.Location = new System.Drawing.Point(62, 72);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(56, 15);
            this.Label33.TabIndex = 16;
            this.Label33.Text = "Atributo";
            // 
            // btn_imprimir1
            // 
            this.btn_imprimir1.Location = new System.Drawing.Point(514, 113);
            this.btn_imprimir1.Name = "btn_imprimir1";
            this.btn_imprimir1.Size = new System.Drawing.Size(75, 31);
            this.btn_imprimir1.TabIndex = 26;
            this.btn_imprimir1.Text = "Visualizar";
            this.btn_imprimir1.UseVisualStyleBackColor = true;
            this.btn_imprimir1.Click += new System.EventHandler(this.btn_imprimir1_Click);
            // 
            // btn_detallexA
            // 
            this.btn_detallexA.Location = new System.Drawing.Point(413, 113);
            this.btn_detallexA.Name = "btn_detallexA";
            this.btn_detallexA.Size = new System.Drawing.Size(75, 31);
            this.btn_detallexA.TabIndex = 25;
            this.btn_detallexA.Text = "Guardar";
            this.btn_detallexA.UseVisualStyleBackColor = true;
            // 
            // cbAvalor
            // 
            this.cbAvalor.FormattingEnabled = true;
            this.cbAvalor.Location = new System.Drawing.Point(413, 42);
            this.cbAvalor.Name = "cbAvalor";
            this.cbAvalor.Size = new System.Drawing.Size(175, 21);
            this.cbAvalor.TabIndex = 20;
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label32.Location = new System.Drawing.Point(367, 42);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(40, 15);
            this.Label32.TabIndex = 18;
            this.Label32.Text = "Valor";
            // 
            // cblistaArticulo
            // 
            this.cblistaArticulo.FormattingEnabled = true;
            this.cblistaArticulo.Location = new System.Drawing.Point(125, 41);
            this.cblistaArticulo.Name = "cblistaArticulo";
            this.cblistaArticulo.Size = new System.Drawing.Size(156, 21);
            this.cblistaArticulo.TabIndex = 15;
            // 
            // Label31
            // 
            this.Label31.AutoSize = true;
            this.Label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label31.Location = new System.Drawing.Point(62, 42);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(55, 15);
            this.Label31.TabIndex = 14;
            this.Label31.Text = "Articulo";
            // 
            // TAvalor
            // 
            this.TAvalor.Location = new System.Drawing.Point(413, 42);
            this.TAvalor.Name = "TAvalor";
            this.TAvalor.Size = new System.Drawing.Size(175, 20);
            this.TAvalor.TabIndex = 19;
            // 
            // btn_lessDA
            // 
            this.btn_lessDA.Image = global::AFN_WF_C.Properties.Resources.remove;
            this.btn_lessDA.Location = new System.Drawing.Point(275, 113);
            this.btn_lessDA.Name = "btn_lessDA";
            this.btn_lessDA.Size = new System.Drawing.Size(36, 39);
            this.btn_lessDA.TabIndex = 24;
            this.btn_lessDA.UseVisualStyleBackColor = true;
            // 
            // btn_addDA
            // 
            this.btn_addDA.Image = global::AFN_WF_C.Properties.Resources.Add;
            this.btn_addDA.Location = new System.Drawing.Point(222, 113);
            this.btn_addDA.Name = "btn_addDA";
            this.btn_addDA.Size = new System.Drawing.Size(36, 39);
            this.btn_addDA.TabIndex = 23;
            this.btn_addDA.UseVisualStyleBackColor = true;
            // 
            // btn_buscaA
            // 
            this.btn_buscaA.Image = global::AFN_WF_C.Properties.Resources.find;
            this.btn_buscaA.Location = new System.Drawing.Point(618, 36);
            this.btn_buscaA.Name = "btn_buscaA";
            this.btn_buscaA.Size = new System.Drawing.Size(31, 31);
            this.btn_buscaA.TabIndex = 21;
            this.btn_buscaA.UseVisualStyleBackColor = true;
            // 
            // ingreso_invent_articulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ckMostrarA);
            this.Controls.Add(this.AtribArticulo);
            this.Controls.Add(this.cbAatrib);
            this.Controls.Add(this.Label33);
            this.Controls.Add(this.btn_imprimir1);
            this.Controls.Add(this.btn_detallexA);
            this.Controls.Add(this.cbAvalor);
            this.Controls.Add(this.Label32);
            this.Controls.Add(this.cblistaArticulo);
            this.Controls.Add(this.Label31);
            this.Controls.Add(this.TAvalor);
            this.Controls.Add(this.btn_lessDA);
            this.Controls.Add(this.btn_addDA);
            this.Controls.Add(this.btn_buscaA);
            this.Name = "ingreso_invent_articulo";
            this.Size = new System.Drawing.Size(826, 386);
            this.Load += new System.EventHandler(this.ingreso_invent_articulo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AtribArticulo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox ckMostrarA;
        internal System.Windows.Forms.DataGridView AtribArticulo;
        internal System.Windows.Forms.ComboBox cbAatrib;
        internal System.Windows.Forms.Label Label33;
        internal System.Windows.Forms.Button btn_imprimir1;
        internal System.Windows.Forms.Button btn_detallexA;
        internal System.Windows.Forms.ComboBox cbAvalor;
        internal System.Windows.Forms.Label Label32;
        internal System.Windows.Forms.ComboBox cblistaArticulo;
        internal System.Windows.Forms.Label Label31;
        internal System.Windows.Forms.TextBox TAvalor;
        internal System.Windows.Forms.Button btn_lessDA;
        internal System.Windows.Forms.Button btn_addDA;
        internal System.Windows.Forms.Button btn_buscaA;
    }
}
