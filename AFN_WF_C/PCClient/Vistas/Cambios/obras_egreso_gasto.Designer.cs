namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    partial class obras_egreso_gasto
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
            this._Label7 = new System.Windows.Forms.Label();
            this.btFindEntrada = new System.Windows.Forms.Button();
            this.btn_out_temp = new System.Windows.Forms.Button();
            this.btn_in_temp = new System.Windows.Forms.Button();
            this.btn_guardar = new System.Windows.Forms.Button();
            this._Label6 = new System.Windows.Forms.Label();
            this.btn_quitar = new System.Windows.Forms.PictureBox();
            this.btn_adjuntar = new System.Windows.Forms.Button();
            this.Efecha = new System.Windows.Forms.DateTimePicker();
            this._Label5 = new System.Windows.Forms.Label();
            this.EmontoSel = new System.Windows.Forms.TextBox();
            this._Label4 = new System.Windows.Forms.Label();
            this.EmontoMax = new System.Windows.Forms.TextBox();
            this._Label3 = new System.Windows.Forms.Label();
            this.Edesc = new System.Windows.Forms.TextBox();
            this._Label2 = new System.Windows.Forms.Label();
            this.Ecod = new System.Windows.Forms.TextBox();
            this.salidaAF = new System.Windows.Forms.DataGridView();
            this.Tsaldos = new System.Windows.Forms.DataGridView();
            this._Label1 = new System.Windows.Forms.Label();
            this.btn_edit = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btn_quitar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.salidaAF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tsaldos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_edit)).BeginInit();
            this.SuspendLayout();
            // 
            // _Label7
            // 
            this._Label7.AutoSize = true;
            this._Label7.Location = new System.Drawing.Point(20, 173);
            this._Label7.Name = "_Label7";
            this._Label7.Size = new System.Drawing.Size(52, 16);
            this._Label7.TabIndex = 43;
            this._Label7.Text = "Codigo";
            // 
            // btFindEntrada
            // 
            this.btFindEntrada.Location = new System.Drawing.Point(593, 165);
            this.btFindEntrada.Name = "btFindEntrada";
            this.btFindEntrada.Size = new System.Drawing.Size(84, 44);
            this.btFindEntrada.TabIndex = 42;
            this.btFindEntrada.Text = "Buscar Entrada";
            this.btFindEntrada.UseVisualStyleBackColor = true;
            this.btFindEntrada.Click += new System.EventHandler(this.btFindEntrada_Click);
            // 
            // btn_out_temp
            // 
            this.btn_out_temp.Location = new System.Drawing.Point(391, 434);
            this.btn_out_temp.Name = "btn_out_temp";
            this.btn_out_temp.Size = new System.Drawing.Size(83, 48);
            this.btn_out_temp.TabIndex = 40;
            this.btn_out_temp.Text = "Cargar Borrador";
            this.btn_out_temp.UseVisualStyleBackColor = true;
            this.btn_out_temp.Click += new System.EventHandler(this.btn_out_temp_Click);
            // 
            // btn_in_temp
            // 
            this.btn_in_temp.Location = new System.Drawing.Point(303, 434);
            this.btn_in_temp.Name = "btn_in_temp";
            this.btn_in_temp.Size = new System.Drawing.Size(83, 48);
            this.btn_in_temp.TabIndex = 39;
            this.btn_in_temp.Text = "Guardar Borrador";
            this.btn_in_temp.UseVisualStyleBackColor = true;
            this.btn_in_temp.Click += new System.EventHandler(this.btn_in_temp_Click);
            // 
            // btn_guardar
            // 
            this.btn_guardar.Location = new System.Drawing.Point(155, 434);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(83, 48);
            this.btn_guardar.TabIndex = 38;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // _Label6
            // 
            this._Label6.AutoSize = true;
            this._Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Label6.ForeColor = System.Drawing.Color.Red;
            this._Label6.Location = new System.Drawing.Point(12, 275);
            this._Label6.Name = "_Label6";
            this._Label6.Size = new System.Drawing.Size(137, 20);
            this._Label6.TabIndex = 37;
            this._Label6.Text = "Salida a Gastos";
            // 
            // btn_quitar
            // 
            this.btn_quitar.Image = global::AFN_WF_C.Properties.Resources.remove1;
            this.btn_quitar.Location = new System.Drawing.Point(652, 266);
            this.btn_quitar.Name = "btn_quitar";
            this.btn_quitar.Size = new System.Drawing.Size(25, 23);
            this.btn_quitar.TabIndex = 36;
            this.btn_quitar.TabStop = false;
            this.btn_quitar.Click += new System.EventHandler(this.btn_quitar_Click);
            // 
            // btn_adjuntar
            // 
            this.btn_adjuntar.Location = new System.Drawing.Point(475, 233);
            this.btn_adjuntar.Name = "btn_adjuntar";
            this.btn_adjuntar.Size = new System.Drawing.Size(87, 25);
            this.btn_adjuntar.TabIndex = 35;
            this.btn_adjuntar.Text = "Adjuntar";
            this.btn_adjuntar.UseVisualStyleBackColor = true;
            this.btn_adjuntar.Click += new System.EventHandler(this.btn_adjuntar_Click);
            // 
            // Efecha
            // 
            this.Efecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Efecha.Location = new System.Drawing.Point(342, 235);
            this.Efecha.Name = "Efecha";
            this.Efecha.Size = new System.Drawing.Size(115, 22);
            this.Efecha.TabIndex = 34;
            // 
            // _Label5
            // 
            this._Label5.AutoSize = true;
            this._Label5.Location = new System.Drawing.Point(339, 219);
            this._Label5.Name = "_Label5";
            this._Label5.Size = new System.Drawing.Size(107, 16);
            this._Label5.TabIndex = 33;
            this._Label5.Text = "Fecha de Salida";
            // 
            // EmontoSel
            // 
            this.EmontoSel.Location = new System.Drawing.Point(182, 235);
            this.EmontoSel.Name = "EmontoSel";
            this.EmontoSel.Size = new System.Drawing.Size(142, 22);
            this.EmontoSel.TabIndex = 32;
            this.EmontoSel.Enter += new System.EventHandler(this.EmontoSel_GotFocus);
            this.EmontoSel.Leave += new System.EventHandler(this.EmontoSel_LostFocus);
            // 
            // _Label4
            // 
            this._Label4.AutoSize = true;
            this._Label4.Location = new System.Drawing.Point(179, 219);
            this._Label4.Name = "_Label4";
            this._Label4.Size = new System.Drawing.Size(100, 16);
            this._Label4.TabIndex = 31;
            this._Label4.Text = "Monto Utilizado";
            // 
            // EmontoMax
            // 
            this.EmontoMax.Location = new System.Drawing.Point(32, 236);
            this.EmontoMax.Name = "EmontoMax";
            this.EmontoMax.Size = new System.Drawing.Size(135, 22);
            this.EmontoMax.TabIndex = 30;
            // 
            // _Label3
            // 
            this._Label3.AutoSize = true;
            this._Label3.Location = new System.Drawing.Point(40, 219);
            this._Label3.Name = "_Label3";
            this._Label3.Size = new System.Drawing.Size(113, 16);
            this._Label3.TabIndex = 29;
            this._Label3.Text = "Monto Disponible";
            // 
            // Edesc
            // 
            this.Edesc.Location = new System.Drawing.Point(117, 189);
            this.Edesc.Name = "Edesc";
            this.Edesc.Size = new System.Drawing.Size(425, 22);
            this.Edesc.TabIndex = 28;
            // 
            // _Label2
            // 
            this._Label2.AutoSize = true;
            this._Label2.Location = new System.Drawing.Point(135, 173);
            this._Label2.Name = "_Label2";
            this._Label2.Size = new System.Drawing.Size(80, 16);
            this._Label2.TabIndex = 27;
            this._Label2.Text = "Descripción";
            // 
            // Ecod
            // 
            this.Ecod.Location = new System.Drawing.Point(16, 189);
            this.Ecod.Name = "Ecod";
            this.Ecod.Size = new System.Drawing.Size(88, 22);
            this.Ecod.TabIndex = 25;
            // 
            // salidaAF
            // 
            this.salidaAF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.salidaAF.Location = new System.Drawing.Point(16, 295);
            this.salidaAF.Name = "salidaAF";
            this.salidaAF.Size = new System.Drawing.Size(661, 125);
            this.salidaAF.TabIndex = 24;
            this.salidaAF.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.salidaAF_CellClick);
            // 
            // Tsaldos
            // 
            this.Tsaldos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tsaldos.Location = new System.Drawing.Point(16, 30);
            this.Tsaldos.Name = "Tsaldos";
            this.Tsaldos.Size = new System.Drawing.Size(661, 129);
            this.Tsaldos.TabIndex = 23;
            this.Tsaldos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tsaldos_CellClick);
            // 
            // _Label1
            // 
            this._Label1.AutoSize = true;
            this._Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Label1.ForeColor = System.Drawing.Color.Green;
            this._Label1.Location = new System.Drawing.Point(12, 7);
            this._Label1.Name = "_Label1";
            this._Label1.Size = new System.Drawing.Size(167, 20);
            this._Label1.TabIndex = 22;
            this._Label1.Text = "Entradas con Saldo";
            // 
            // btn_edit
            // 
            this.btn_edit.Image = global::AFN_WF_C.Properties.Resources.eject;
            this.btn_edit.Location = new System.Drawing.Point(618, 266);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(25, 23);
            this.btn_edit.TabIndex = 44;
            this.btn_edit.TabStop = false;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // obras_egreso_gasto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(697, 502);
            this.Controls.Add(this.btn_edit);
            this.Controls.Add(this._Label7);
            this.Controls.Add(this.btFindEntrada);
            this.Controls.Add(this.btn_out_temp);
            this.Controls.Add(this.btn_in_temp);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this._Label6);
            this.Controls.Add(this.btn_quitar);
            this.Controls.Add(this.btn_adjuntar);
            this.Controls.Add(this.Efecha);
            this.Controls.Add(this._Label5);
            this.Controls.Add(this.EmontoSel);
            this.Controls.Add(this._Label4);
            this.Controls.Add(this.EmontoMax);
            this.Controls.Add(this._Label3);
            this.Controls.Add(this.Edesc);
            this.Controls.Add(this._Label2);
            this.Controls.Add(this.Ecod);
            this.Controls.Add(this.salidaAF);
            this.Controls.Add(this.Tsaldos);
            this.Controls.Add(this._Label1);
            this.Name = "obras_egreso_gasto";
            this.Text = "Obra en Construcción hacia Gastos";
            this.Load += new System.EventHandler(this.obras_egreso_gasto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_quitar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.salidaAF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tsaldos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_edit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label _Label7;
        internal System.Windows.Forms.Button btFindEntrada;
        internal System.Windows.Forms.Button btn_out_temp;
        internal System.Windows.Forms.Button btn_in_temp;
        internal System.Windows.Forms.Button btn_guardar;
        internal System.Windows.Forms.Label _Label6;
        internal System.Windows.Forms.PictureBox btn_quitar;
        internal System.Windows.Forms.Button btn_adjuntar;
        internal System.Windows.Forms.DateTimePicker Efecha;
        private System.Windows.Forms.Label _Label5;
        internal System.Windows.Forms.TextBox EmontoSel;
        internal System.Windows.Forms.Label _Label4;
        internal System.Windows.Forms.TextBox EmontoMax;
        internal System.Windows.Forms.Label _Label3;
        internal System.Windows.Forms.TextBox Edesc;
        internal System.Windows.Forms.Label _Label2;
        internal System.Windows.Forms.TextBox Ecod;
        internal System.Windows.Forms.DataGridView salidaAF;
        internal System.Windows.Forms.DataGridView Tsaldos;
        internal System.Windows.Forms.Label _Label1;
        internal System.Windows.Forms.PictureBox btn_edit;
    }
}
