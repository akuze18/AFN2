namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    partial class ingreso_invent_grup
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
            this.ckMostrar = new System.Windows.Forms.CheckBox();
            this.btn_buscaG = new System.Windows.Forms.Button();
            this.AtribGrupo = new System.Windows.Forms.DataGridView();
            this.btn_imprimir = new System.Windows.Forms.Button();
            this.btn_detallexG = new System.Windows.Forms.Button();
            this.cbGvalor = new System.Windows.Forms.ComboBox();
            this.TGvalor = new System.Windows.Forms.TextBox();
            this.Label30 = new System.Windows.Forms.Label();
            this.cbGatrib = new System.Windows.Forms.ComboBox();
            this.Label29 = new System.Windows.Forms.Label();
            this.btn_lessGA = new System.Windows.Forms.Button();
            this.btn_addGA = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AtribGrupo)).BeginInit();
            this.SuspendLayout();
            // 
            // ckMostrar
            // 
            this.ckMostrar.AutoSize = true;
            this.ckMostrar.Location = new System.Drawing.Point(407, 67);
            this.ckMostrar.Name = "ckMostrar";
            this.ckMostrar.Size = new System.Drawing.Size(133, 17);
            this.ckMostrar.TabIndex = 18;
            this.ckMostrar.Text = "Mostrar en descripción";
            this.ckMostrar.UseVisualStyleBackColor = true;
            // 
            // btn_buscaG
            // 
            this.btn_buscaG.Image = global::AFN_WF_C.Properties.Resources.find;
            this.btn_buscaG.Location = new System.Drawing.Point(609, 25);
            this.btn_buscaG.Name = "btn_buscaG";
            this.btn_buscaG.Size = new System.Drawing.Size(31, 31);
            this.btn_buscaG.TabIndex = 17;
            this.btn_buscaG.UseVisualStyleBackColor = true;
            // 
            // AtribGrupo
            // 
            this.AtribGrupo.AllowUserToAddRows = false;
            this.AtribGrupo.AllowUserToDeleteRows = false;
            this.AtribGrupo.AllowUserToResizeColumns = false;
            this.AtribGrupo.AllowUserToResizeRows = false;
            this.AtribGrupo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AtribGrupo.Location = new System.Drawing.Point(25, 144);
            this.AtribGrupo.MultiSelect = false;
            this.AtribGrupo.Name = "AtribGrupo";
            this.AtribGrupo.ReadOnly = true;
            this.AtribGrupo.RowHeadersWidth = 20;
            this.AtribGrupo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AtribGrupo.Size = new System.Drawing.Size(752, 195);
            this.AtribGrupo.TabIndex = 23;
            // 
            // btn_imprimir
            // 
            this.btn_imprimir.Location = new System.Drawing.Point(504, 99);
            this.btn_imprimir.Name = "btn_imprimir";
            this.btn_imprimir.Size = new System.Drawing.Size(75, 31);
            this.btn_imprimir.TabIndex = 22;
            this.btn_imprimir.Text = "Visualizar";
            this.btn_imprimir.UseVisualStyleBackColor = true;
            // 
            // btn_detallexG
            // 
            this.btn_detallexG.Location = new System.Drawing.Point(403, 99);
            this.btn_detallexG.Name = "btn_detallexG";
            this.btn_detallexG.Size = new System.Drawing.Size(75, 31);
            this.btn_detallexG.TabIndex = 21;
            this.btn_detallexG.Text = "Guardar";
            this.btn_detallexG.UseVisualStyleBackColor = true;
            // 
            // cbGvalor
            // 
            this.cbGvalor.FormattingEnabled = true;
            this.cbGvalor.Location = new System.Drawing.Point(404, 31);
            this.cbGvalor.Name = "cbGvalor";
            this.cbGvalor.Size = new System.Drawing.Size(175, 21);
            this.cbGvalor.TabIndex = 16;
            // 
            // TGvalor
            // 
            this.TGvalor.Location = new System.Drawing.Point(404, 31);
            this.TGvalor.Name = "TGvalor";
            this.TGvalor.Size = new System.Drawing.Size(175, 20);
            this.TGvalor.TabIndex = 15;
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label30.Location = new System.Drawing.Point(358, 31);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(40, 15);
            this.Label30.TabIndex = 14;
            this.Label30.Text = "Valor";
            // 
            // cbGatrib
            // 
            this.cbGatrib.FormattingEnabled = true;
            this.cbGatrib.Location = new System.Drawing.Point(115, 31);
            this.cbGatrib.Name = "cbGatrib";
            this.cbGatrib.Size = new System.Drawing.Size(156, 21);
            this.cbGatrib.TabIndex = 13;
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label29.Location = new System.Drawing.Point(52, 31);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(56, 15);
            this.Label29.TabIndex = 12;
            this.Label29.Text = "Atributo";
            // 
            // btn_lessGA
            // 
            this.btn_lessGA.Image = global::AFN_WF_C.Properties.Resources.remove;
            this.btn_lessGA.Location = new System.Drawing.Point(265, 99);
            this.btn_lessGA.Margin = new System.Windows.Forms.Padding(0);
            this.btn_lessGA.Name = "btn_lessGA";
            this.btn_lessGA.Size = new System.Drawing.Size(36, 39);
            this.btn_lessGA.TabIndex = 20;
            this.btn_lessGA.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_lessGA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_lessGA.UseVisualStyleBackColor = true;
            // 
            // btn_addGA
            // 
            this.btn_addGA.Image = global::AFN_WF_C.Properties.Resources.Add;
            this.btn_addGA.Location = new System.Drawing.Point(212, 99);
            this.btn_addGA.Margin = new System.Windows.Forms.Padding(0);
            this.btn_addGA.Name = "btn_addGA";
            this.btn_addGA.Size = new System.Drawing.Size(36, 39);
            this.btn_addGA.TabIndex = 19;
            this.btn_addGA.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_addGA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_addGA.UseVisualStyleBackColor = true;
            // 
            // ingreso_invent_grup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ckMostrar);
            this.Controls.Add(this.btn_buscaG);
            this.Controls.Add(this.AtribGrupo);
            this.Controls.Add(this.btn_imprimir);
            this.Controls.Add(this.btn_detallexG);
            this.Controls.Add(this.cbGvalor);
            this.Controls.Add(this.TGvalor);
            this.Controls.Add(this.Label30);
            this.Controls.Add(this.cbGatrib);
            this.Controls.Add(this.Label29);
            this.Controls.Add(this.btn_lessGA);
            this.Controls.Add(this.btn_addGA);
            this.Name = "ingreso_invent_grup";
            this.Size = new System.Drawing.Size(826, 386);
            this.Load += new System.EventHandler(this.ingreso_invent_grup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AtribGrupo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox ckMostrar;
        internal System.Windows.Forms.Button btn_buscaG;
        internal System.Windows.Forms.DataGridView AtribGrupo;
        internal System.Windows.Forms.Button btn_imprimir;
        internal System.Windows.Forms.Button btn_detallexG;
        internal System.Windows.Forms.ComboBox cbGvalor;
        internal System.Windows.Forms.TextBox TGvalor;
        internal System.Windows.Forms.Label Label30;
        internal System.Windows.Forms.ComboBox cbGatrib;
        internal System.Windows.Forms.Label Label29;
        internal System.Windows.Forms.Button btn_lessGA;
        internal System.Windows.Forms.Button btn_addGA;
    }
}
