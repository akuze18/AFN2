namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    partial class ingreso_financiero
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
            this.cboGestion = new System.Windows.Forms.ComboBox();
            this.Label21 = new System.Windows.Forms.Label();
            this.ckDepre = new System.Windows.Forms.CheckBox();
            this.TxtPrecioTotal = new System.Windows.Forms.TextBox();
            this.Label34 = new System.Windows.Forms.Label();
            this.cboSubzona = new System.Windows.Forms.ComboBox();
            this.btn_Bprov = new System.Windows.Forms.PictureBox();
            this.btn_act = new System.Windows.Forms.Button();
            this.btn_elim = new System.Windows.Forms.Button();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.ckIFRS = new System.Windows.Forms.CheckBox();
            this.Fderecho = new System.Windows.Forms.GroupBox();
            this.derC2 = new System.Windows.Forms.RadioButton();
            this.derC1 = new System.Windows.Forms.RadioButton();
            this.Label17 = new System.Windows.Forms.Label();
            this.Tdoc = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.TvuF = new System.Windows.Forms.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.Tprecio_compra = new System.Windows.Forms.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.Tcantidad = new System.Windows.Forms.TextBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.cbFecha_ing = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Tfecha_compra = new System.Windows.Forms.DateTimePicker();
            this.Label11 = new System.Windows.Forms.Label();
            this.cboProveedor = new System.Windows.Forms.ComboBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.cboCateg = new System.Windows.Forms.ComboBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.cboClase = new System.Windows.Forms.ComboBox();
            this.cboSubclase = new System.Windows.Forms.ComboBox();
            this.cboConsist = new System.Windows.Forms.ComboBox();
            this.cboZona = new System.Windows.Forms.ComboBox();
            this.Tdescrip = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Bprov)).BeginInit();
            this.Fderecho.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboGestion
            // 
            this.cboGestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGestion.FormattingEnabled = true;
            this.cboGestion.Location = new System.Drawing.Point(82, 130);
            this.cboGestion.Name = "cboGestion";
            this.cboGestion.Size = new System.Drawing.Size(140, 21);
            this.cboGestion.TabIndex = 55;
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(12, 133);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(43, 13);
            this.Label21.TabIndex = 54;
            this.Label21.Text = "Gestion";
            // 
            // ckDepre
            // 
            this.ckDepre.AutoSize = true;
            this.ckDepre.Checked = true;
            this.ckDepre.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckDepre.Location = new System.Drawing.Point(214, 315);
            this.ckDepre.Name = "ckDepre";
            this.ckDepre.Size = new System.Drawing.Size(72, 17);
            this.ckDepre.TabIndex = 77;
            this.ckDepre.Text = "Depreciar";
            this.ckDepre.UseVisualStyleBackColor = true;
            // 
            // TxtPrecioTotal
            // 
            this.TxtPrecioTotal.Location = new System.Drawing.Point(612, 232);
            this.TxtPrecioTotal.Name = "TxtPrecioTotal";
            this.TxtPrecioTotal.Size = new System.Drawing.Size(140, 20);
            this.TxtPrecioTotal.TabIndex = 67;
            this.TxtPrecioTotal.TextChanged += new System.EventHandler(this.PrecioOrCantidad_TextChanged);
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.Location = new System.Drawing.Point(542, 235);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(64, 13);
            this.Label34.TabIndex = 66;
            this.Label34.Text = "Precio Total";
            // 
            // cboSubzona
            // 
            this.cboSubzona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubzona.FormattingEnabled = true;
            this.cboSubzona.Location = new System.Drawing.Point(342, 63);
            this.cboSubzona.Name = "cboSubzona";
            this.cboSubzona.Size = new System.Drawing.Size(140, 21);
            this.cboSubzona.TabIndex = 45;
            // 
            // btn_Bprov
            // 
            this.btn_Bprov.Image = global::AFN_WF_C.Properties.Resources.find;
            this.btn_Bprov.Location = new System.Drawing.Point(612, 157);
            this.btn_Bprov.Name = "btn_Bprov";
            this.btn_Bprov.Size = new System.Drawing.Size(25, 26);
            this.btn_Bprov.TabIndex = 76;
            this.btn_Bprov.TabStop = false;
            this.btn_Bprov.Click += new System.EventHandler(this.btn_Bprov_Click);
            // 
            // btn_act
            // 
            this.btn_act.Image = global::AFN_WF_C.Properties.Resources._32_lock;
            this.btn_act.Location = new System.Drawing.Point(621, 315);
            this.btn_act.Margin = new System.Windows.Forms.Padding(0);
            this.btn_act.Name = "btn_act";
            this.btn_act.Size = new System.Drawing.Size(53, 45);
            this.btn_act.TabIndex = 80;
            this.btn_act.Text = "Activar";
            this.btn_act.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_act.UseVisualStyleBackColor = true;
            this.btn_act.Click += new System.EventHandler(this.btn_act_Click);
            // 
            // btn_elim
            // 
            this.btn_elim.Image = global::AFN_WF_C.Properties.Resources._32_remove;
            this.btn_elim.Location = new System.Drawing.Point(529, 315);
            this.btn_elim.Margin = new System.Windows.Forms.Padding(0);
            this.btn_elim.Name = "btn_elim";
            this.btn_elim.Size = new System.Drawing.Size(53, 45);
            this.btn_elim.TabIndex = 79;
            this.btn_elim.Text = "Eliminar";
            this.btn_elim.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_elim.UseVisualStyleBackColor = true;
            this.btn_elim.Click += new System.EventHandler(this.btn_elim_Click);
            // 
            // btn_guardar
            // 
            this.btn_guardar.Image = global::AFN_WF_C.Properties.Resources._32_next;
            this.btn_guardar.Location = new System.Drawing.Point(429, 315);
            this.btn_guardar.Margin = new System.Windows.Forms.Padding(0);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(53, 45);
            this.btn_guardar.TabIndex = 78;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // ckIFRS
            // 
            this.ckIFRS.AutoSize = true;
            this.ckIFRS.Location = new System.Drawing.Point(82, 315);
            this.ckIFRS.Name = "ckIFRS";
            this.ckIFRS.Size = new System.Drawing.Size(97, 17);
            this.ckIFRS.TabIndex = 75;
            this.ckIFRS.Text = "Ingresa a IFRS";
            this.ckIFRS.UseVisualStyleBackColor = true;
            // 
            // Fderecho
            // 
            this.Fderecho.Controls.Add(this.derC2);
            this.Fderecho.Controls.Add(this.derC1);
            this.Fderecho.Location = new System.Drawing.Point(614, 262);
            this.Fderecho.Margin = new System.Windows.Forms.Padding(0);
            this.Fderecho.Name = "Fderecho";
            this.Fderecho.Padding = new System.Windows.Forms.Padding(0);
            this.Fderecho.Size = new System.Drawing.Size(113, 28);
            this.Fderecho.TabIndex = 74;
            this.Fderecho.TabStop = false;
            // 
            // derC2
            // 
            this.derC2.AutoSize = true;
            this.derC2.Location = new System.Drawing.Point(65, 8);
            this.derC2.Name = "derC2";
            this.derC2.Size = new System.Drawing.Size(39, 17);
            this.derC2.TabIndex = 1;
            this.derC2.TabStop = true;
            this.derC2.Text = "No";
            this.derC2.UseVisualStyleBackColor = true;
            // 
            // derC1
            // 
            this.derC1.AutoSize = true;
            this.derC1.Location = new System.Drawing.Point(7, 8);
            this.derC1.Name = "derC1";
            this.derC1.Size = new System.Drawing.Size(34, 17);
            this.derC1.TabIndex = 0;
            this.derC1.TabStop = true;
            this.derC1.Text = "Si";
            this.derC1.UseVisualStyleBackColor = true;
            // 
            // Label17
            // 
            this.Label17.Location = new System.Drawing.Point(542, 267);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(56, 27);
            this.Label17.TabIndex = 73;
            this.Label17.Text = "Derecho Credito";
            // 
            // Tdoc
            // 
            this.Tdoc.Location = new System.Drawing.Point(342, 267);
            this.Tdoc.Name = "Tdoc";
            this.Tdoc.Size = new System.Drawing.Size(140, 20);
            this.Tdoc.TabIndex = 72;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(262, 267);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(77, 13);
            this.Label16.TabIndex = 71;
            this.Label16.Text = "Nº Documento";
            // 
            // TvuF
            // 
            this.TvuF.Location = new System.Drawing.Point(82, 267);
            this.TvuF.Name = "TvuF";
            this.TvuF.Size = new System.Drawing.Size(140, 20);
            this.TvuF.TabIndex = 70;
            // 
            // Label15
            // 
            this.Label15.Location = new System.Drawing.Point(12, 267);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(72, 35);
            this.Label15.TabIndex = 69;
            this.Label15.Text = "Vida Util (Meses)";
            // 
            // Tprecio_compra
            // 
            this.Tprecio_compra.Location = new System.Drawing.Point(342, 232);
            this.Tprecio_compra.Name = "Tprecio_compra";
            this.Tprecio_compra.Size = new System.Drawing.Size(140, 20);
            this.Tprecio_compra.TabIndex = 65;
            this.Tprecio_compra.TextChanged += new System.EventHandler(this.PrecioOrCantidad_TextChanged);
            this.Tprecio_compra.Enter += new System.EventHandler(this.Tprecio_compra_Enter);
            this.Tprecio_compra.Leave += new System.EventHandler(this.Tprecio_compra_Leave);
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(262, 232);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(76, 13);
            this.Label14.TabIndex = 64;
            this.Label14.Text = "Precio Unitario";
            // 
            // Tcantidad
            // 
            this.Tcantidad.Location = new System.Drawing.Point(82, 232);
            this.Tcantidad.Name = "Tcantidad";
            this.Tcantidad.Size = new System.Drawing.Size(140, 20);
            this.Tcantidad.TabIndex = 63;
            this.Tcantidad.TextChanged += new System.EventHandler(this.PrecioOrCantidad_TextChanged);
            this.Tcantidad.Leave += new System.EventHandler(this.Tcantidad_Leave);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(12, 232);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(49, 13);
            this.Label13.TabIndex = 62;
            this.Label13.Text = "Cantidad";
            // 
            // cbFecha_ing
            // 
            this.cbFecha_ing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFecha_ing.FormattingEnabled = true;
            this.cbFecha_ing.Location = new System.Drawing.Point(342, 197);
            this.cbFecha_ing.Name = "cbFecha_ing";
            this.cbFecha_ing.Size = new System.Drawing.Size(140, 21);
            this.cbFecha_ing.TabIndex = 61;
            // 
            // Label12
            // 
            this.Label12.Location = new System.Drawing.Point(262, 197);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(77, 35);
            this.Label12.TabIndex = 60;
            this.Label12.Text = "Periodo Contable";
            // 
            // Tfecha_compra
            // 
            this.Tfecha_compra.CustomFormat = "dd-MM-yyyy";
            this.Tfecha_compra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Tfecha_compra.Location = new System.Drawing.Point(82, 197);
            this.Tfecha_compra.Name = "Tfecha_compra";
            this.Tfecha_compra.Size = new System.Drawing.Size(130, 20);
            this.Tfecha_compra.TabIndex = 59;
            this.Tfecha_compra.ValueChanged += new System.EventHandler(this.Tfecha_compra_ValueChanged);
            // 
            // Label11
            // 
            this.Label11.Location = new System.Drawing.Point(12, 197);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(72, 35);
            this.Label11.TabIndex = 58;
            this.Label11.Text = "Fecha Adquisición";
            // 
            // cboProveedor
            // 
            this.cboProveedor.DropDownHeight = 93;
            this.cboProveedor.FormattingEnabled = true;
            this.cboProveedor.IntegralHeight = false;
            this.cboProveedor.Location = new System.Drawing.Point(82, 162);
            this.cboProveedor.MaxDropDownItems = 7;
            this.cboProveedor.Name = "cboProveedor";
            this.cboProveedor.Size = new System.Drawing.Size(500, 21);
            this.cboProveedor.TabIndex = 57;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(12, 162);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(56, 13);
            this.Label10.TabIndex = 56;
            this.Label10.Text = "Proveedor";
            // 
            // cboCateg
            // 
            this.cboCateg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCateg.FormattingEnabled = true;
            this.cboCateg.Location = new System.Drawing.Point(612, 64);
            this.cboCateg.Name = "cboCateg";
            this.cboCateg.Size = new System.Drawing.Size(140, 21);
            this.cboCateg.TabIndex = 47;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(542, 64);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(52, 13);
            this.Label9.TabIndex = 46;
            this.Label9.Text = "Categoria";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(262, 97);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(33, 13);
            this.Label8.TabIndex = 50;
            this.Label8.Text = "Clase";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(262, 64);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(49, 13);
            this.Label7.TabIndex = 44;
            this.Label7.Text = "Subzona";
            // 
            // cboClase
            // 
            this.cboClase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClase.FormattingEnabled = true;
            this.cboClase.Location = new System.Drawing.Point(342, 97);
            this.cboClase.Name = "cboClase";
            this.cboClase.Size = new System.Drawing.Size(140, 21);
            this.cboClase.TabIndex = 51;
            this.cboClase.SelectedIndexChanged += new System.EventHandler(this.cboClase_SelectedIndexChanged);
            // 
            // cboSubclase
            // 
            this.cboSubclase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubclase.FormattingEnabled = true;
            this.cboSubclase.Location = new System.Drawing.Point(612, 97);
            this.cboSubclase.Name = "cboSubclase";
            this.cboSubclase.Size = new System.Drawing.Size(140, 21);
            this.cboSubclase.TabIndex = 53;
            this.cboSubclase.SelectedIndexChanged += new System.EventHandler(this.cboSubclase_SelectedIndexChanged);
            // 
            // cboConsist
            // 
            this.cboConsist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConsist.FormattingEnabled = true;
            this.cboConsist.Location = new System.Drawing.Point(82, 97);
            this.cboConsist.Name = "cboConsist";
            this.cboConsist.Size = new System.Drawing.Size(140, 21);
            this.cboConsist.TabIndex = 49;
            this.cboConsist.SelectedIndexChanged += new System.EventHandler(this.cboConsist_SelectedIndexChanged);
            // 
            // cboZona
            // 
            this.cboZona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboZona.FormattingEnabled = true;
            this.cboZona.Location = new System.Drawing.Point(82, 64);
            this.cboZona.Name = "cboZona";
            this.cboZona.Size = new System.Drawing.Size(140, 21);
            this.cboZona.TabIndex = 43;
            this.cboZona.SelectedIndexChanged += new System.EventHandler(this.cboZona_SelectedIndexChanged);
            // 
            // Tdescrip
            // 
            this.Tdescrip.Location = new System.Drawing.Point(82, 9);
            this.Tdescrip.Multiline = true;
            this.Tdescrip.Name = "Tdescrip";
            this.Tdescrip.Size = new System.Drawing.Size(686, 40);
            this.Tdescrip.TabIndex = 41;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(542, 97);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(51, 13);
            this.Label4.TabIndex = 52;
            this.Label4.Text = "Subclase";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(12, 97);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(28, 13);
            this.Label3.TabIndex = 48;
            this.Label3.Text = "Tipo";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(12, 64);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(32, 13);
            this.Label2.TabIndex = 42;
            this.Label2.Text = "Zona";
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(72, 40);
            this.Label1.TabIndex = 40;
            this.Label1.Text = "Descripción Simple";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ingreso_financiero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboGestion);
            this.Controls.Add(this.Label21);
            this.Controls.Add(this.ckDepre);
            this.Controls.Add(this.TxtPrecioTotal);
            this.Controls.Add(this.Label34);
            this.Controls.Add(this.cboSubzona);
            this.Controls.Add(this.btn_Bprov);
            this.Controls.Add(this.btn_act);
            this.Controls.Add(this.btn_elim);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this.ckIFRS);
            this.Controls.Add(this.Fderecho);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.Tdoc);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.TvuF);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.Tprecio_compra);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.Tcantidad);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.cbFecha_ing);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.Tfecha_compra);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.cboProveedor);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.cboCateg);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.cboClase);
            this.Controls.Add(this.cboSubclase);
            this.Controls.Add(this.cboConsist);
            this.Controls.Add(this.cboZona);
            this.Controls.Add(this.Tdescrip);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Name = "ingreso_financiero";
            this.Size = new System.Drawing.Size(796, 426);
            this.Load += new System.EventHandler(this.ingreso_financiero_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_Bprov)).EndInit();
            this.Fderecho.ResumeLayout(false);
            this.Fderecho.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox cboGestion;
        internal System.Windows.Forms.Label Label21;
        internal System.Windows.Forms.CheckBox ckDepre;
        internal System.Windows.Forms.TextBox TxtPrecioTotal;
        internal System.Windows.Forms.Label Label34;
        internal System.Windows.Forms.ComboBox cboSubzona;
        internal System.Windows.Forms.PictureBox btn_Bprov;
        internal System.Windows.Forms.Button btn_act;
        internal System.Windows.Forms.Button btn_elim;
        internal System.Windows.Forms.Button btn_guardar;
        internal System.Windows.Forms.CheckBox ckIFRS;
        internal System.Windows.Forms.GroupBox Fderecho;
        internal System.Windows.Forms.RadioButton derC2;
        internal System.Windows.Forms.RadioButton derC1;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.TextBox Tdoc;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.TextBox TvuF;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.TextBox Tprecio_compra;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.TextBox Tcantidad;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.ComboBox cbFecha_ing;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.DateTimePicker Tfecha_compra;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.ComboBox cboProveedor;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.ComboBox cboCateg;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.ComboBox cboClase;
        internal System.Windows.Forms.ComboBox cboSubclase;
        internal System.Windows.Forms.ComboBox cboConsist;
        internal System.Windows.Forms.ComboBox cboZona;
        internal System.Windows.Forms.TextBox Tdescrip;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;

    }
}
