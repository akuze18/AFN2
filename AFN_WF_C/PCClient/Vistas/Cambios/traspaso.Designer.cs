namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    partial class traspaso
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
            this.btn_consulta = new System.Windows.Forms.Button();
            this.btn_fin = new System.Windows.Forms.Button();
            this.lista_cambiar = new System.Windows.Forms.DataGridView();
            this.Label11 = new System.Windows.Forms.Label();
            this.TsubZact = new System.Windows.Forms.TextBox();
            this.TzonaAct = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.cboSubzona = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.cboZona = new System.Windows.Forms.ComboBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Tvalor = new System.Windows.Forms.TextBox();
            this.Dcambio = new System.Windows.Forms.DateTimePicker();
            this.cboCant = new System.Windows.Forms.ComboBox();
            this.Tarticulo = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.cod_art = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btn_detalle_cantidad = new System.Windows.Forms.PictureBox();
            this.btn_remove = new System.Windows.Forms.PictureBox();
            this.btn_add = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.lista_cambiar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_detalle_cantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_remove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_add)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_consulta
            // 
            this.btn_consulta.Location = new System.Drawing.Point(549, 30);
            this.btn_consulta.Name = "btn_consulta";
            this.btn_consulta.Size = new System.Drawing.Size(72, 34);
            this.btn_consulta.TabIndex = 56;
            this.btn_consulta.Text = "Buscar";
            this.btn_consulta.UseVisualStyleBackColor = true;
            this.btn_consulta.Click += new System.EventHandler(this.btn_consulta_Click);
            // 
            // btn_fin
            // 
            this.btn_fin.Location = new System.Drawing.Point(269, 380);
            this.btn_fin.Name = "btn_fin";
            this.btn_fin.Size = new System.Drawing.Size(84, 31);
            this.btn_fin.TabIndex = 51;
            this.btn_fin.Text = "Guardar";
            this.btn_fin.UseVisualStyleBackColor = true;
            this.btn_fin.Click += new System.EventHandler(this.btn_fin_Click);
            // 
            // lista_cambiar
            // 
            this.lista_cambiar.AllowUserToAddRows = false;
            this.lista_cambiar.AllowUserToDeleteRows = false;
            this.lista_cambiar.Location = new System.Drawing.Point(31, 231);
            this.lista_cambiar.MultiSelect = false;
            this.lista_cambiar.Name = "lista_cambiar";
            this.lista_cambiar.ReadOnly = true;
            this.lista_cambiar.Size = new System.Drawing.Size(590, 143);
            this.lista_cambiar.TabIndex = 50;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(34, 211);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(190, 16);
            this.Label11.TabIndex = 49;
            this.Label11.Text = "Articulos listos para Traspasar";
            // 
            // TsubZact
            // 
            this.TsubZact.Location = new System.Drawing.Point(450, 155);
            this.TsubZact.Name = "TsubZact";
            this.TsubZact.Size = new System.Drawing.Size(171, 22);
            this.TsubZact.TabIndex = 46;
            // 
            // TzonaAct
            // 
            this.TzonaAct.Location = new System.Drawing.Point(136, 155);
            this.TzonaAct.Name = "TzonaAct";
            this.TzonaAct.Size = new System.Drawing.Size(174, 22);
            this.TzonaAct.TabIndex = 45;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(336, 155);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(101, 16);
            this.Label10.TabIndex = 44;
            this.Label10.Text = "Subzona Actual";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(34, 155);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(79, 16);
            this.Label9.TabIndex = 43;
            this.Label9.Text = "Zona Actual";
            // 
            // cboSubzona
            // 
            this.cboSubzona.FormattingEnabled = true;
            this.cboSubzona.Location = new System.Drawing.Point(450, 126);
            this.cboSubzona.Name = "cboSubzona";
            this.cboSubzona.Size = new System.Drawing.Size(171, 24);
            this.cboSubzona.TabIndex = 42;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(336, 129);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(110, 16);
            this.Label8.TabIndex = 41;
            this.Label8.Text = "Subzona Destino";
            // 
            // cboZona
            // 
            this.cboZona.FormattingEnabled = true;
            this.cboZona.Location = new System.Drawing.Point(136, 126);
            this.cboZona.Name = "cboZona";
            this.cboZona.Size = new System.Drawing.Size(174, 24);
            this.cboZona.TabIndex = 40;
            this.cboZona.SelectedIndexChanged += new System.EventHandler(this.cboZona_SelectedIndexChanged);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(34, 129);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(88, 16);
            this.Label7.TabIndex = 39;
            this.Label7.Text = "Zona Destino";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(313, 72);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(73, 16);
            this.Label6.TabIndex = 38;
            this.Label6.Text = "Valor Libro";
            // 
            // Tvalor
            // 
            this.Tvalor.Location = new System.Drawing.Point(398, 69);
            this.Tvalor.Name = "Tvalor";
            this.Tvalor.Size = new System.Drawing.Size(110, 22);
            this.Tvalor.TabIndex = 37;
            // 
            // Dcambio
            // 
            this.Dcambio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dcambio.Location = new System.Drawing.Point(136, 98);
            this.Dcambio.Name = "Dcambio";
            this.Dcambio.Size = new System.Drawing.Size(114, 22);
            this.Dcambio.TabIndex = 36;
            // 
            // cboCant
            // 
            this.cboCant.FormattingEnabled = true;
            this.cboCant.Location = new System.Drawing.Point(136, 69);
            this.cboCant.Name = "cboCant";
            this.cboCant.Size = new System.Drawing.Size(114, 24);
            this.cboCant.TabIndex = 35;
            this.cboCant.SelectedIndexChanged += new System.EventHandler(this.cboCant_SelectedIndexChanged);
            // 
            // Tarticulo
            // 
            this.Tarticulo.Location = new System.Drawing.Point(136, 42);
            this.Tarticulo.Name = "Tarticulo";
            this.Tarticulo.Size = new System.Drawing.Size(355, 22);
            this.Tarticulo.TabIndex = 34;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(34, 98);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(46, 16);
            this.Label5.TabIndex = 33;
            this.Label5.Text = "Fecha";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(34, 69);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(62, 16);
            this.Label4.TabIndex = 32;
            this.Label4.Text = "Cantidad";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(34, 42);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(52, 16);
            this.Label3.TabIndex = 31;
            this.Label3.Text = "Artículo";
            // 
            // cod_art
            // 
            this.cod_art.Location = new System.Drawing.Point(243, 16);
            this.cod_art.Name = "cod_art";
            this.cod_art.Size = new System.Drawing.Size(100, 13);
            this.cod_art.TabIndex = 30;
            this.cod_art.Text = "Label2";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(15, 16);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(200, 16);
            this.Label1.TabIndex = 29;
            this.Label1.Text = "Seleccione Artículo a Traspasar";
            // 
            // btn_detalle_cantidad
            // 
            this.btn_detalle_cantidad.Image = global::AFN_WF_C.Properties.Resources.eject;
            this.btn_detalle_cantidad.InitialImage = null;
            this.btn_detalle_cantidad.Location = new System.Drawing.Point(256, 69);
            this.btn_detalle_cantidad.Name = "btn_detalle_cantidad";
            this.btn_detalle_cantidad.Size = new System.Drawing.Size(24, 24);
            this.btn_detalle_cantidad.TabIndex = 57;
            this.btn_detalle_cantidad.TabStop = false;
            this.btn_detalle_cantidad.Click += new System.EventHandler(this.btn_detalle_cantidad_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.Image = global::AFN_WF_C.Properties.Resources.remove;
            this.btn_remove.Location = new System.Drawing.Point(465, 191);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(35, 35);
            this.btn_remove.TabIndex = 48;
            this.btn_remove.TabStop = false;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btn_add
            // 
            this.btn_add.Image = global::AFN_WF_C.Properties.Resources.Add;
            this.btn_add.Location = new System.Drawing.Point(409, 191);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(36, 36);
            this.btn_add.TabIndex = 47;
            this.btn_add.TabStop = false;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // traspaso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(661, 422);
            this.Controls.Add(this.btn_detalle_cantidad);
            this.Controls.Add(this.btn_consulta);
            this.Controls.Add(this.btn_fin);
            this.Controls.Add(this.lista_cambiar);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.TsubZact);
            this.Controls.Add(this.TzonaAct);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.cboSubzona);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.cboZona);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Tvalor);
            this.Controls.Add(this.Dcambio);
            this.Controls.Add(this.cboCant);
            this.Controls.Add(this.Tarticulo);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.cod_art);
            this.Controls.Add(this.Label1);
            this.Name = "traspaso";
            this.Load += new System.EventHandler(this.form_cambio_Load);
            this.Resize += new System.EventHandler(this.form_cambio_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.lista_cambiar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_detalle_cantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_remove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_add)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox btn_detalle_cantidad;
        internal System.Windows.Forms.Button btn_consulta;
        internal System.Windows.Forms.Button btn_fin;
        internal System.Windows.Forms.DataGridView lista_cambiar;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.PictureBox btn_remove;
        internal System.Windows.Forms.PictureBox btn_add;
        internal System.Windows.Forms.TextBox TsubZact;
        internal System.Windows.Forms.TextBox TzonaAct;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.ComboBox cboSubzona;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.ComboBox cboZona;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox Tvalor;
        internal System.Windows.Forms.DateTimePicker Dcambio;
        internal System.Windows.Forms.ComboBox cboCant;
        internal System.Windows.Forms.TextBox Tarticulo;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label cod_art;
        internal System.Windows.Forms.Label Label1;

    }
}
