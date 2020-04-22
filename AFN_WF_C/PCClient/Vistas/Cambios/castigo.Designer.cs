namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    partial class castigo
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
            this.btn_fin = new System.Windows.Forms.Button();
            this.lista_castigar = new System.Windows.Forms.DataGridView();
            this.Label7 = new System.Windows.Forms.Label();
            this.CheckI = new System.Windows.Forms.CheckBox();
            this.CheckT = new System.Windows.Forms.CheckBox();
            this.CheckF = new System.Windows.Forms.CheckBox();
            this.btn_consulta = new System.Windows.Forms.Button();
            this.Tvalor = new System.Windows.Forms.TextBox();
            this.Dcastigo = new System.Windows.Forms.DateTimePicker();
            this.cboCant = new System.Windows.Forms.ComboBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Tarticulo = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.cod_art = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btn_detalle_cantidad = new System.Windows.Forms.PictureBox();
            this.btn_remove = new System.Windows.Forms.PictureBox();
            this.btn_add = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.lista_castigar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_detalle_cantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_remove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_add)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_fin
            // 
            this.btn_fin.Location = new System.Drawing.Point(263, 323);
            this.btn_fin.Name = "btn_fin";
            this.btn_fin.Size = new System.Drawing.Size(76, 31);
            this.btn_fin.TabIndex = 41;
            this.btn_fin.Text = "Guardar";
            this.btn_fin.UseVisualStyleBackColor = true;
            this.btn_fin.Click += new System.EventHandler(this.btn_fin_Click);
            // 
            // lista_castigar
            // 
            this.lista_castigar.AllowUserToAddRows = false;
            this.lista_castigar.AllowUserToDeleteRows = false;
            this.lista_castigar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lista_castigar.Location = new System.Drawing.Point(26, 162);
            this.lista_castigar.MultiSelect = false;
            this.lista_castigar.Name = "lista_castigar";
            this.lista_castigar.ReadOnly = true;
            this.lista_castigar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lista_castigar.Size = new System.Drawing.Size(565, 152);
            this.lista_castigar.TabIndex = 40;
            this.lista_castigar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lista_castigar_CellContentClick);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(40, 139);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(177, 16);
            this.Label7.TabIndex = 39;
            this.Label7.Text = "Articulos listos para Castigar";
            // 
            // CheckI
            // 
            this.CheckI.AutoSize = true;
            this.CheckI.Checked = true;
            this.CheckI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckI.Enabled = false;
            this.CheckI.Location = new System.Drawing.Point(495, 126);
            this.CheckI.Name = "CheckI";
            this.CheckI.Size = new System.Drawing.Size(57, 20);
            this.CheckI.TabIndex = 38;
            this.CheckI.Text = "IFRS";
            this.CheckI.UseVisualStyleBackColor = true;
            // 
            // CheckT
            // 
            this.CheckT.AutoSize = true;
            this.CheckT.Checked = true;
            this.CheckT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckT.Location = new System.Drawing.Point(495, 103);
            this.CheckT.Name = "CheckT";
            this.CheckT.Size = new System.Drawing.Size(64, 20);
            this.CheckT.TabIndex = 37;
            this.CheckT.Text = "Tribut.";
            this.CheckT.UseVisualStyleBackColor = true;
            // 
            // CheckF
            // 
            this.CheckF.AutoSize = true;
            this.CheckF.Checked = true;
            this.CheckF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckF.Enabled = false;
            this.CheckF.Location = new System.Drawing.Point(495, 80);
            this.CheckF.Name = "CheckF";
            this.CheckF.Size = new System.Drawing.Size(70, 20);
            this.CheckF.TabIndex = 36;
            this.CheckF.Text = "Financ.";
            this.CheckF.UseVisualStyleBackColor = true;
            // 
            // btn_consulta
            // 
            this.btn_consulta.Location = new System.Drawing.Point(510, 27);
            this.btn_consulta.Name = "btn_consulta";
            this.btn_consulta.Size = new System.Drawing.Size(73, 32);
            this.btn_consulta.TabIndex = 35;
            this.btn_consulta.Text = "Buscar";
            this.btn_consulta.UseVisualStyleBackColor = true;
            this.btn_consulta.Click += new System.EventHandler(this.btn_consulta_Click);
            // 
            // Tvalor
            // 
            this.Tvalor.Location = new System.Drawing.Point(385, 75);
            this.Tvalor.Name = "Tvalor";
            this.Tvalor.Size = new System.Drawing.Size(100, 22);
            this.Tvalor.TabIndex = 32;
            // 
            // Dcastigo
            // 
            this.Dcastigo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dcastigo.Location = new System.Drawing.Point(109, 109);
            this.Dcastigo.Name = "Dcastigo";
            this.Dcastigo.Size = new System.Drawing.Size(121, 22);
            this.Dcastigo.TabIndex = 31;
            // 
            // cboCant
            // 
            this.cboCant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCant.FormattingEnabled = true;
            this.cboCant.Location = new System.Drawing.Point(109, 74);
            this.cboCant.Name = "cboCant";
            this.cboCant.Size = new System.Drawing.Size(121, 24);
            this.cboCant.TabIndex = 30;
            this.cboCant.SelectedIndexChanged += new System.EventHandler(this.cboCant_SelectedIndexChanged);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(295, 80);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(73, 16);
            this.Label6.TabIndex = 29;
            this.Label6.Text = "Valor Libro";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(40, 109);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(46, 16);
            this.Label5.TabIndex = 28;
            this.Label5.Text = "Fecha";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(40, 77);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(62, 16);
            this.Label4.TabIndex = 27;
            this.Label4.Text = "Cantidad";
            // 
            // Tarticulo
            // 
            this.Tarticulo.Location = new System.Drawing.Point(109, 43);
            this.Tarticulo.Name = "Tarticulo";
            this.Tarticulo.Size = new System.Drawing.Size(376, 22);
            this.Tarticulo.TabIndex = 26;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(40, 46);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(52, 16);
            this.Label3.TabIndex = 25;
            this.Label3.Text = "Artículo";
            // 
            // cod_art
            // 
            this.cod_art.Location = new System.Drawing.Point(216, 9);
            this.cod_art.Name = "cod_art";
            this.cod_art.Size = new System.Drawing.Size(84, 17);
            this.cod_art.TabIndex = 24;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(23, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(187, 16);
            this.Label1.TabIndex = 23;
            this.Label1.Text = "Seleccione Artículo a Castigar";
            // 
            // btn_detalle_cantidad
            // 
            this.btn_detalle_cantidad.Image = global::AFN_WF_C.Properties.Resources.eject;
            this.btn_detalle_cantidad.Location = new System.Drawing.Point(236, 73);
            this.btn_detalle_cantidad.Name = "btn_detalle_cantidad";
            this.btn_detalle_cantidad.Size = new System.Drawing.Size(25, 24);
            this.btn_detalle_cantidad.TabIndex = 45;
            this.btn_detalle_cantidad.TabStop = false;
            this.btn_detalle_cantidad.Click += new System.EventHandler(this.btn_detalle_cantidad_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.Image = global::AFN_WF_C.Properties.Resources.remove;
            this.btn_remove.Location = new System.Drawing.Point(385, 109);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(32, 36);
            this.btn_remove.TabIndex = 34;
            this.btn_remove.TabStop = false;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btn_add
            // 
            this.btn_add.Image = global::AFN_WF_C.Properties.Resources.Add;
            this.btn_add.Location = new System.Drawing.Point(331, 109);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(35, 36);
            this.btn_add.TabIndex = 33;
            this.btn_add.TabStop = false;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // castigo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(624, 361);
            this.Controls.Add(this.btn_detalle_cantidad);
            this.Controls.Add(this.btn_fin);
            this.Controls.Add(this.lista_castigar);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.CheckI);
            this.Controls.Add(this.CheckT);
            this.Controls.Add(this.CheckF);
            this.Controls.Add(this.btn_consulta);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.Tvalor);
            this.Controls.Add(this.Dcastigo);
            this.Controls.Add(this.cboCant);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Tarticulo);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.cod_art);
            this.Controls.Add(this.Label1);
            this.Name = "castigo";
            this.Text = "Castigo de Activo Fijo";
            this.Load += new System.EventHandler(this.castigo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lista_castigar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_detalle_cantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_remove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_add)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox btn_detalle_cantidad;
        internal System.Windows.Forms.Button btn_fin;
        internal System.Windows.Forms.DataGridView lista_castigar;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.CheckBox CheckI;
        internal System.Windows.Forms.CheckBox CheckT;
        internal System.Windows.Forms.CheckBox CheckF;
        internal System.Windows.Forms.Button btn_consulta;
        internal System.Windows.Forms.PictureBox btn_remove;
        internal System.Windows.Forms.PictureBox btn_add;
        internal System.Windows.Forms.TextBox Tvalor;
        internal System.Windows.Forms.DateTimePicker Dcastigo;
        internal System.Windows.Forms.ComboBox cboCant;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox Tarticulo;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label cod_art;
        internal System.Windows.Forms.Label Label1;
    }
}
