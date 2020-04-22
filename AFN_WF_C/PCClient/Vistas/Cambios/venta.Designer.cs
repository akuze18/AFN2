namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    partial class venta
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
            this.lista_vender = new System.Windows.Forms.DataGridView();
            this.btn_fin = new System.Windows.Forms.Button();
            this.Label7 = new System.Windows.Forms.Label();
            this.btn_remove = new System.Windows.Forms.PictureBox();
            this.btn_add = new System.Windows.Forms.PictureBox();
            this.btn_consulta = new System.Windows.Forms.Button();
            this.Tvalor = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Dventa = new System.Windows.Forms.DateTimePicker();
            this.cboCant = new System.Windows.Forms.ComboBox();
            this.Tarticulo = new System.Windows.Forms.TextBox();
            this.cod_art = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btn_detalle_cantidad = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.lista_vender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_remove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_add)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_detalle_cantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // lista_vender
            // 
            this.lista_vender.AllowUserToAddRows = false;
            this.lista_vender.AllowUserToDeleteRows = false;
            this.lista_vender.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lista_vender.Location = new System.Drawing.Point(16, 182);
            this.lista_vender.Name = "lista_vender";
            this.lista_vender.ReadOnly = true;
            this.lista_vender.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lista_vender.Size = new System.Drawing.Size(601, 149);
            this.lista_vender.TabIndex = 35;
            this.lista_vender.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lista_vender_CellClick);
            // 
            // btn_fin
            // 
            this.btn_fin.Location = new System.Drawing.Point(285, 337);
            this.btn_fin.Name = "btn_fin";
            this.btn_fin.Size = new System.Drawing.Size(74, 37);
            this.btn_fin.TabIndex = 34;
            this.btn_fin.Text = "Guardar";
            this.btn_fin.UseVisualStyleBackColor = true;
            this.btn_fin.Click += new System.EventHandler(this.btn_fin_Click);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(31, 151);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(169, 16);
            this.Label7.TabIndex = 32;
            this.Label7.Text = "Artículos listos para vender";
            // 
            // btn_remove
            // 
            this.btn_remove.Image = global::AFN_WF_C.Properties.Resources.remove;
            this.btn_remove.Location = new System.Drawing.Point(472, 117);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(34, 38);
            this.btn_remove.TabIndex = 31;
            this.btn_remove.TabStop = false;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btn_add
            // 
            this.btn_add.Image = global::AFN_WF_C.Properties.Resources.Add;
            this.btn_add.Location = new System.Drawing.Point(418, 117);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(34, 38);
            this.btn_add.TabIndex = 30;
            this.btn_add.TabStop = false;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_consulta
            // 
            this.btn_consulta.Location = new System.Drawing.Point(522, 36);
            this.btn_consulta.Name = "btn_consulta";
            this.btn_consulta.Size = new System.Drawing.Size(58, 30);
            this.btn_consulta.TabIndex = 29;
            this.btn_consulta.Text = "Buscar";
            this.btn_consulta.UseVisualStyleBackColor = true;
            this.btn_consulta.Click += new System.EventHandler(this.btn_consulta_Click);
            // 
            // Tvalor
            // 
            this.Tvalor.Location = new System.Drawing.Point(370, 83);
            this.Tvalor.Name = "Tvalor";
            this.Tvalor.Size = new System.Drawing.Size(100, 22);
            this.Tvalor.TabIndex = 28;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(297, 83);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(73, 16);
            this.Label6.TabIndex = 27;
            this.Label6.Text = "Valor Libro";
            // 
            // Dventa
            // 
            this.Dventa.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dventa.Location = new System.Drawing.Point(130, 117);
            this.Dventa.Name = "Dventa";
            this.Dventa.Size = new System.Drawing.Size(121, 22);
            this.Dventa.TabIndex = 26;
            // 
            // cboCant
            // 
            this.cboCant.FormattingEnabled = true;
            this.cboCant.Location = new System.Drawing.Point(130, 83);
            this.cboCant.Name = "cboCant";
            this.cboCant.Size = new System.Drawing.Size(121, 24);
            this.cboCant.TabIndex = 25;
            this.cboCant.SelectedIndexChanged += new System.EventHandler(this.cboCant_SelectedIndexChanged);
            // 
            // Tarticulo
            // 
            this.Tarticulo.Location = new System.Drawing.Point(130, 53);
            this.Tarticulo.Name = "Tarticulo";
            this.Tarticulo.Size = new System.Drawing.Size(340, 22);
            this.Tarticulo.TabIndex = 24;
            // 
            // cod_art
            // 
            this.cod_art.Location = new System.Drawing.Point(200, 15);
            this.cod_art.Name = "cod_art";
            this.cod_art.Size = new System.Drawing.Size(108, 13);
            this.cod_art.TabIndex = 23;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(55, 117);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(46, 16);
            this.Label4.TabIndex = 22;
            this.Label4.Text = "Fecha";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(55, 83);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(62, 16);
            this.Label3.TabIndex = 21;
            this.Label3.Text = "Cantidad";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(54, 53);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(52, 16);
            this.Label2.TabIndex = 20;
            this.Label2.Text = "Artículo";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(181, 16);
            this.Label1.TabIndex = 19;
            this.Label1.Text = "Seleccione Artículo a Vender";
            // 
            // btn_detalle_cantidad
            // 
            this.btn_detalle_cantidad.Image = global::AFN_WF_C.Properties.Resources.eject;
            this.btn_detalle_cantidad.Location = new System.Drawing.Point(257, 83);
            this.btn_detalle_cantidad.Name = "btn_detalle_cantidad";
            this.btn_detalle_cantidad.Size = new System.Drawing.Size(25, 24);
            this.btn_detalle_cantidad.TabIndex = 46;
            this.btn_detalle_cantidad.TabStop = false;
            this.btn_detalle_cantidad.Click += new System.EventHandler(this.btn_detalle_cantidad_Click);
            // 
            // venta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(633, 386);
            this.Controls.Add(this.btn_detalle_cantidad);
            this.Controls.Add(this.lista_vender);
            this.Controls.Add(this.btn_fin);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.btn_consulta);
            this.Controls.Add(this.Tvalor);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Dventa);
            this.Controls.Add(this.cboCant);
            this.Controls.Add(this.Tarticulo);
            this.Controls.Add(this.cod_art);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Name = "venta";
            this.Text = "Venta de Activo Fijo";
            this.Load += new System.EventHandler(this.venta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lista_vender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_remove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_add)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_detalle_cantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btn_fin;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.PictureBox btn_remove;
        internal System.Windows.Forms.PictureBox btn_add;
        internal System.Windows.Forms.Button btn_consulta;
        internal System.Windows.Forms.TextBox Tvalor;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.DateTimePicker Dventa;
        internal System.Windows.Forms.ComboBox cboCant;
        internal System.Windows.Forms.TextBox Tarticulo;
        internal System.Windows.Forms.Label cod_art;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.DataGridView lista_vender;
        internal System.Windows.Forms.PictureBox btn_detalle_cantidad;
    }
}
