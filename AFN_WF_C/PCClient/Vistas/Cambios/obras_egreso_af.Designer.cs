namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    partial class obras_egreso_af
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
            this.MaskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.btFindEntrada = new System.Windows.Forms.Button();
            this._Label30 = new System.Windows.Forms.Label();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.LvalorAF = new System.Windows.Forms.TextBox();
            this._Label7 = new System.Windows.Forms.Label();
            this.Tcantidad = new System.Windows.Forms.TextBox();
            this._Label6 = new System.Windows.Forms.Label();
            this._Label5 = new System.Windows.Forms.Label();
            this._Label4 = new System.Windows.Forms.Label();
            this._Label3 = new System.Windows.Forms.Label();
            this.btn_adjuntar = new System.Windows.Forms.Button();
            this.EmontoSel = new System.Windows.Forms.TextBox();
            this.EmontoMax = new System.Windows.Forms.TextBox();
            this.Edesc = new System.Windows.Forms.TextBox();
            this.Ecod = new System.Windows.Forms.TextBox();
            this._Label2 = new System.Windows.Forms.Label();
            this.salidaAF = new System.Windows.Forms.DataGridView();
            this.Tsaldos = new System.Windows.Forms.DataGridView();
            this._Label1 = new System.Windows.Forms.Label();
            this.btn_quitar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.salidaAF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tsaldos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_quitar)).BeginInit();
            this.SuspendLayout();
            // 
            // MaskedTextBox1
            // 
            this.MaskedTextBox1.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.MaskedTextBox1.HidePromptOnLeave = true;
            this.MaskedTextBox1.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.MaskedTextBox1.Location = new System.Drawing.Point(410, 327);
            this.MaskedTextBox1.Mask = "###,###,###,###,###.00";
            this.MaskedTextBox1.Name = "MaskedTextBox1";
            this.MaskedTextBox1.Size = new System.Drawing.Size(110, 22);
            this.MaskedTextBox1.TabIndex = 40;
            this.MaskedTextBox1.Visible = false;
            // 
            // btFindEntrada
            // 
            this.btFindEntrada.Location = new System.Drawing.Point(718, 266);
            this.btFindEntrada.Name = "btFindEntrada";
            this.btFindEntrada.Size = new System.Drawing.Size(64, 46);
            this.btFindEntrada.TabIndex = 39;
            this.btFindEntrada.Text = "Buscar Entrada";
            this.btFindEntrada.UseVisualStyleBackColor = true;
            this.btFindEntrada.Click += new System.EventHandler(this.btFindEntrada_Click);
            // 
            // _Label30
            // 
            this._Label30.AutoSize = true;
            this._Label30.Location = new System.Drawing.Point(106, 268);
            this._Label30.Name = "_Label30";
            this._Label30.Size = new System.Drawing.Size(52, 16);
            this._Label30.TabIndex = 38;
            this._Label30.Text = "Codigo";
            // 
            // btn_guardar
            // 
            this.btn_guardar.Location = new System.Drawing.Point(514, 541);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(65, 35);
            this.btn_guardar.TabIndex = 37;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // LvalorAF
            // 
            this.LvalorAF.Location = new System.Drawing.Point(319, 563);
            this.LvalorAF.Name = "LvalorAF";
            this.LvalorAF.Size = new System.Drawing.Size(134, 22);
            this.LvalorAF.TabIndex = 36;
            // 
            // _Label7
            // 
            this._Label7.AutoSize = true;
            this._Label7.Location = new System.Drawing.Point(172, 563);
            this._Label7.Name = "_Label7";
            this._Label7.Size = new System.Drawing.Size(139, 16);
            this._Label7.TabIndex = 35;
            this._Label7.Text = "Valor Total Activo Fijo";
            // 
            // Tcantidad
            // 
            this.Tcantidad.Location = new System.Drawing.Point(319, 526);
            this.Tcantidad.Name = "Tcantidad";
            this.Tcantidad.Size = new System.Drawing.Size(80, 22);
            this.Tcantidad.TabIndex = 34;
            // 
            // _Label6
            // 
            this._Label6.AutoSize = true;
            this._Label6.Location = new System.Drawing.Point(168, 529);
            this._Label6.Name = "_Label6";
            this._Label6.Size = new System.Drawing.Size(141, 16);
            this._Label6.TabIndex = 33;
            this._Label6.Text = "Cantidad de Artículos :";
            // 
            // _Label5
            // 
            this._Label5.AutoSize = true;
            this._Label5.Location = new System.Drawing.Point(266, 311);
            this._Label5.Name = "_Label5";
            this._Label5.Size = new System.Drawing.Size(100, 16);
            this._Label5.TabIndex = 27;
            this._Label5.Text = "Monto Utilizado";
            // 
            // _Label4
            // 
            this._Label4.AutoSize = true;
            this._Label4.Location = new System.Drawing.Point(117, 311);
            this._Label4.Name = "_Label4";
            this._Label4.Size = new System.Drawing.Size(113, 16);
            this._Label4.TabIndex = 25;
            this._Label4.Text = "Monto Disponible";
            // 
            // _Label3
            // 
            this._Label3.AutoSize = true;
            this._Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Label3.ForeColor = System.Drawing.Color.Red;
            this._Label3.Location = new System.Drawing.Point(22, 358);
            this._Label3.Name = "_Label3";
            this._Label3.Size = new System.Drawing.Size(175, 20);
            this._Label3.TabIndex = 30;
            this._Label3.Text = "Salidas al Activo Fijo";
            // 
            // btn_adjuntar
            // 
            this.btn_adjuntar.Location = new System.Drawing.Point(551, 325);
            this.btn_adjuntar.Name = "btn_adjuntar";
            this.btn_adjuntar.Size = new System.Drawing.Size(74, 24);
            this.btn_adjuntar.TabIndex = 29;
            this.btn_adjuntar.Text = "Adjuntar";
            this.btn_adjuntar.UseVisualStyleBackColor = true;
            this.btn_adjuntar.Click += new System.EventHandler(this.btn_adjuntar_Click);
            // 
            // EmontoSel
            // 
            this.EmontoSel.Location = new System.Drawing.Point(269, 327);
            this.EmontoSel.Name = "EmontoSel";
            this.EmontoSel.Size = new System.Drawing.Size(113, 22);
            this.EmontoSel.TabIndex = 28;
            this.EmontoSel.Enter += new System.EventHandler(this.EmontoSel_GotFocus);
            this.EmontoSel.Leave += new System.EventHandler(this.EmontoSel_LostFocus);
            // 
            // EmontoMax
            // 
            this.EmontoMax.Location = new System.Drawing.Point(120, 327);
            this.EmontoMax.Name = "EmontoMax";
            this.EmontoMax.Size = new System.Drawing.Size(131, 22);
            this.EmontoMax.TabIndex = 26;
            // 
            // Edesc
            // 
            this.Edesc.Location = new System.Drawing.Point(192, 284);
            this.Edesc.Name = "Edesc";
            this.Edesc.Size = new System.Drawing.Size(413, 22);
            this.Edesc.TabIndex = 24;
            // 
            // Ecod
            // 
            this.Ecod.Location = new System.Drawing.Point(100, 284);
            this.Ecod.Name = "Ecod";
            this.Ecod.Size = new System.Drawing.Size(86, 22);
            this.Ecod.TabIndex = 22;
            // 
            // _Label2
            // 
            this._Label2.AutoSize = true;
            this._Label2.Location = new System.Drawing.Point(189, 268);
            this._Label2.Name = "_Label2";
            this._Label2.Size = new System.Drawing.Size(80, 16);
            this._Label2.TabIndex = 23;
            this._Label2.Text = "Descripción";
            // 
            // salidaAF
            // 
            this.salidaAF.AllowUserToAddRows = false;
            this.salidaAF.AllowUserToDeleteRows = false;
            this.salidaAF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.salidaAF.Location = new System.Drawing.Point(13, 381);
            this.salidaAF.Name = "salidaAF";
            this.salidaAF.ReadOnly = true;
            this.salidaAF.Size = new System.Drawing.Size(769, 134);
            this.salidaAF.TabIndex = 31;
            // 
            // Tsaldos
            // 
            this.Tsaldos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tsaldos.Location = new System.Drawing.Point(13, 33);
            this.Tsaldos.Name = "Tsaldos";
            this.Tsaldos.Size = new System.Drawing.Size(769, 227);
            this.Tsaldos.TabIndex = 21;
            this.Tsaldos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tsaldos_CellClick);
            // 
            // _Label1
            // 
            this._Label1.AutoSize = true;
            this._Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Label1.ForeColor = System.Drawing.Color.Green;
            this._Label1.Location = new System.Drawing.Point(9, 10);
            this._Label1.Name = "_Label1";
            this._Label1.Size = new System.Drawing.Size(167, 20);
            this._Label1.TabIndex = 20;
            this._Label1.Text = "Entradas con Saldo";
            // 
            // btn_quitar
            // 
            this.btn_quitar.Image = global::AFN_WF_C.Properties.Resources.remove1;
            this.btn_quitar.Location = new System.Drawing.Point(757, 355);
            this.btn_quitar.Name = "btn_quitar";
            this.btn_quitar.Size = new System.Drawing.Size(25, 23);
            this.btn_quitar.TabIndex = 32;
            this.btn_quitar.TabStop = false;
            this.btn_quitar.Click += new System.EventHandler(this.btn_quitar_Click);
            // 
            // obras_egreso_af
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(797, 605);
            this.Controls.Add(this.MaskedTextBox1);
            this.Controls.Add(this.btFindEntrada);
            this.Controls.Add(this._Label30);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this.LvalorAF);
            this.Controls.Add(this._Label7);
            this.Controls.Add(this.Tcantidad);
            this.Controls.Add(this._Label6);
            this.Controls.Add(this.btn_quitar);
            this.Controls.Add(this._Label5);
            this.Controls.Add(this._Label4);
            this.Controls.Add(this._Label3);
            this.Controls.Add(this.btn_adjuntar);
            this.Controls.Add(this.EmontoSel);
            this.Controls.Add(this.EmontoMax);
            this.Controls.Add(this.Edesc);
            this.Controls.Add(this.Ecod);
            this.Controls.Add(this._Label2);
            this.Controls.Add(this.salidaAF);
            this.Controls.Add(this.Tsaldos);
            this.Controls.Add(this._Label1);
            this.Name = "obras_egreso_af";
            this.Text = "Obras en Construcción a Activo Fijo";
            this.Load += new System.EventHandler(this.obras_egreso_af_Load);
            ((System.ComponentModel.ISupportInitialize)(this.salidaAF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tsaldos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_quitar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.MaskedTextBox MaskedTextBox1;
        internal System.Windows.Forms.Button btFindEntrada;
        internal System.Windows.Forms.Label _Label30;
        internal System.Windows.Forms.Button btn_guardar;
        internal System.Windows.Forms.TextBox LvalorAF;
        internal System.Windows.Forms.Label _Label7;
        internal System.Windows.Forms.TextBox Tcantidad;
        internal System.Windows.Forms.Label _Label6;
        internal System.Windows.Forms.PictureBox btn_quitar;
        internal System.Windows.Forms.Label _Label5;
        internal System.Windows.Forms.Label _Label4;
        internal System.Windows.Forms.Label _Label3;
        internal System.Windows.Forms.Button btn_adjuntar;
        internal System.Windows.Forms.TextBox EmontoSel;
        internal System.Windows.Forms.TextBox EmontoMax;
        internal System.Windows.Forms.TextBox Edesc;
        internal System.Windows.Forms.TextBox Ecod;
        internal System.Windows.Forms.Label _Label2;
        internal System.Windows.Forms.DataGridView salidaAF;
        internal System.Windows.Forms.DataGridView Tsaldos;
        internal System.Windows.Forms.Label _Label1;
    }
}
