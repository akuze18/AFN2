namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    partial class obras_ingreso
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
            this.Tfecha_conta = new System.Windows.Forms.DateTimePicker();
            this.Label7 = new System.Windows.Forms.Label();
            this.btn_limpiar = new System.Windows.Forms.Button();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.Tdescrip = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.btn_Bprov = new System.Windows.Forms.PictureBox();
            this.Tdoc = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.cboZona = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Tcredito = new System.Windows.Forms.TextBox();
            this.cboProveedor = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Tfecha_compra = new System.Windows.Forms.DateTimePicker();
            this.Label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Bprov)).BeginInit();
            this.SuspendLayout();
            // 
            // Tfecha_conta
            // 
            this.Tfecha_conta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Tfecha_conta.Location = new System.Drawing.Point(119, 51);
            this.Tfecha_conta.Name = "Tfecha_conta";
            this.Tfecha_conta.Size = new System.Drawing.Size(143, 22);
            this.Tfecha_conta.TabIndex = 21;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(15, 51);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(92, 16);
            this.Label7.TabIndex = 20;
            this.Label7.Text = "Fecha Contab";
            // 
            // btn_limpiar
            // 
            this.btn_limpiar.Location = new System.Drawing.Point(316, 245);
            this.btn_limpiar.Name = "btn_limpiar";
            this.btn_limpiar.Size = new System.Drawing.Size(75, 33);
            this.btn_limpiar.TabIndex = 32;
            this.btn_limpiar.Text = "Nuevo";
            this.btn_limpiar.UseVisualStyleBackColor = true;
            this.btn_limpiar.Click += new System.EventHandler(this.btn_limpiar_Click);
            // 
            // btn_guardar
            // 
            this.btn_guardar.Location = new System.Drawing.Point(224, 244);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(75, 33);
            this.btn_guardar.TabIndex = 31;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // Tdescrip
            // 
            this.Tdescrip.Location = new System.Drawing.Point(119, 169);
            this.Tdescrip.Multiline = true;
            this.Tdescrip.Name = "Tdescrip";
            this.Tdescrip.Size = new System.Drawing.Size(480, 59);
            this.Tdescrip.TabIndex = 30;
            this.Tdescrip.TextChanged += new System.EventHandler(this.defaultColorButton);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(15, 169);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(80, 16);
            this.Label6.TabIndex = 29;
            this.Label6.Text = "Descripcion";
            // 
            // btn_Bprov
            // 
            this.btn_Bprov.Image = global::AFN_WF_C.Properties.Resources.find;
            this.btn_Bprov.Location = new System.Drawing.Point(573, 127);
            this.btn_Bprov.Name = "btn_Bprov";
            this.btn_Bprov.Size = new System.Drawing.Size(26, 24);
            this.btn_Bprov.TabIndex = 27;
            this.btn_Bprov.TabStop = false;
            this.btn_Bprov.Click += new System.EventHandler(this.btn_Bprov_Click);
            // 
            // Tdoc
            // 
            this.Tdoc.Location = new System.Drawing.Point(428, 86);
            this.Tdoc.Name = "Tdoc";
            this.Tdoc.Size = new System.Drawing.Size(171, 22);
            this.Tdoc.TabIndex = 25;
            this.Tdoc.TextChanged += new System.EventHandler(this.defaultColorButton);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(325, 89);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(95, 16);
            this.Label5.TabIndex = 24;
            this.Label5.Text = "Nº Documento";
            // 
            // cboZona
            // 
            this.cboZona.FormattingEnabled = true;
            this.cboZona.Location = new System.Drawing.Point(428, 15);
            this.cboZona.Name = "cboZona";
            this.cboZona.Size = new System.Drawing.Size(171, 24);
            this.cboZona.TabIndex = 19;
            this.cboZona.SelectedIndexChanged += new System.EventHandler(this.defaultColorButton);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(325, 15);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(39, 16);
            this.Label4.TabIndex = 18;
            this.Label4.Text = "Zona";
            // 
            // Tcredito
            // 
            this.Tcredito.Location = new System.Drawing.Point(118, 86);
            this.Tcredito.Name = "Tcredito";
            this.Tcredito.Size = new System.Drawing.Size(144, 22);
            this.Tcredito.TabIndex = 23;
            this.Tcredito.TextChanged += new System.EventHandler(this.defaultColorButton);
            this.Tcredito.Enter += new System.EventHandler(this.Tcredito_GotFocus);
            this.Tcredito.Leave += new System.EventHandler(this.Tcredito_LostFocus);
            // 
            // cboProveedor
            // 
            this.cboProveedor.FormattingEnabled = true;
            this.cboProveedor.Location = new System.Drawing.Point(119, 127);
            this.cboProveedor.Name = "cboProveedor";
            this.cboProveedor.Size = new System.Drawing.Size(448, 24);
            this.cboProveedor.TabIndex = 28;
            this.cboProveedor.TextChanged += new System.EventHandler(this.defaultColorButton);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(15, 130);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(72, 16);
            this.Label3.TabIndex = 26;
            this.Label3.Text = "Proveedor";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(15, 89);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(45, 16);
            this.Label2.TabIndex = 22;
            this.Label2.Text = "Monto";
            // 
            // Tfecha_compra
            // 
            this.Tfecha_compra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Tfecha_compra.Location = new System.Drawing.Point(119, 15);
            this.Tfecha_compra.Name = "Tfecha_compra";
            this.Tfecha_compra.Size = new System.Drawing.Size(143, 22);
            this.Tfecha_compra.TabIndex = 17;
            this.Tfecha_compra.ValueChanged += new System.EventHandler(this.defaultColorButton);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(15, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(94, 16);
            this.Label1.TabIndex = 16;
            this.Label1.Text = "Fecha Factura";
            // 
            // obras_ingreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(624, 288);
            this.Controls.Add(this.Tfecha_conta);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.btn_limpiar);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this.Tdescrip);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.btn_Bprov);
            this.Controls.Add(this.Tdoc);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.cboZona);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Tcredito);
            this.Controls.Add(this.cboProveedor);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Tfecha_compra);
            this.Controls.Add(this.Label1);
            this.Name = "obras_ingreso";
            this.Text = "Entrada de Obra en Construccion";
            this.Load += new System.EventHandler(this.obras_ingreso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_Bprov)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker Tfecha_conta;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Button btn_limpiar;
        internal System.Windows.Forms.Button btn_guardar;
        internal System.Windows.Forms.TextBox Tdescrip;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.PictureBox btn_Bprov;
        internal System.Windows.Forms.TextBox Tdoc;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox cboZona;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox Tcredito;
        internal System.Windows.Forms.ComboBox cboProveedor;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.DateTimePicker Tfecha_compra;
        internal System.Windows.Forms.Label Label1;
    }
}
