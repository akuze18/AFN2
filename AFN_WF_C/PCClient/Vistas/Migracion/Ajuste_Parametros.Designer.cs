namespace AFN_WF_C.PCClient.Vistas.Migracion
{
    partial class Ajuste_Parametros
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
            this.label7 = new System.Windows.Forms.Label();
            this.CbSistema = new System.Windows.Forms.ComboBox();
            this.BtnGuardar = new System.Windows.Forms.Button();
            this.NumParamValue = new System.Windows.Forms.NumericUpDown();
            this.TbParamName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LParametros = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label4 = new System.Windows.Forms.Label();
            this.CbTHead = new System.Windows.Forms.ComboBox();
            this.CbParte = new System.Windows.Forms.ComboBox();
            this.TbCodigoLote = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumParamValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LParametros)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Sistema";
            // 
            // CbSistema
            // 
            this.CbSistema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbSistema.FormattingEnabled = true;
            this.CbSistema.Location = new System.Drawing.Point(127, 115);
            this.CbSistema.Name = "CbSistema";
            this.CbSistema.Size = new System.Drawing.Size(203, 21);
            this.CbSistema.TabIndex = 7;
            this.CbSistema.SelectedIndexChanged += new System.EventHandler(this.CbTHead_SelectedIndexChanged);
            // 
            // BtnGuardar
            // 
            this.BtnGuardar.Location = new System.Drawing.Point(360, 328);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.Size = new System.Drawing.Size(64, 47);
            this.BtnGuardar.TabIndex = 14;
            this.BtnGuardar.Text = "Guardar";
            this.BtnGuardar.UseVisualStyleBackColor = true;
            // 
            // NumParamValue
            // 
            this.NumParamValue.DecimalPlaces = 3;
            this.NumParamValue.Location = new System.Drawing.Point(142, 355);
            this.NumParamValue.Maximum = new decimal(new int[] {
            276447231,
            23283,
            0,
            0});
            this.NumParamValue.Minimum = new decimal(new int[] {
            276447231,
            23283,
            0,
            -2147483648});
            this.NumParamValue.Name = "NumParamValue";
            this.NumParamValue.Size = new System.Drawing.Size(167, 20);
            this.NumParamValue.TabIndex = 13;
            this.NumParamValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TbParamName
            // 
            this.TbParamName.Enabled = false;
            this.TbParamName.Location = new System.Drawing.Point(142, 328);
            this.TbParamName.Name = "TbParamName";
            this.TbParamName.Size = new System.Drawing.Size(167, 20);
            this.TbParamName.TabIndex = 11;
            this.TbParamName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 356);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Valor";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 328);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Parametro";
            // 
            // LParametros
            // 
            this.LParametros.AllColumns.Add(this.olvColumn1);
            this.LParametros.AllColumns.Add(this.olvColumn2);
            this.LParametros.CellEditUseWholeCell = false;
            this.LParametros.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.LParametros.Cursor = System.Windows.Forms.Cursors.Default;
            this.LParametros.FullRowSelect = true;
            this.LParametros.Location = new System.Drawing.Point(36, 186);
            this.LParametros.MultiSelect = false;
            this.LParametros.Name = "LParametros";
            this.LParametros.ShowGroups = false;
            this.LParametros.Size = new System.Drawing.Size(578, 123);
            this.LParametros.TabIndex = 9;
            this.LParametros.UseCompatibleStateImageBehavior = false;
            this.LParametros.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "name";
            this.olvColumn1.Text = "Parametro";
            this.olvColumn1.Width = 174;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "value";
            this.olvColumn2.Text = "Valor";
            this.olvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn2.Width = 200;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Parametros";
            // 
            // CbTHead
            // 
            this.CbTHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbTHead.FormattingEnabled = true;
            this.CbTHead.Location = new System.Drawing.Point(126, 85);
            this.CbTHead.Name = "CbTHead";
            this.CbTHead.Size = new System.Drawing.Size(203, 21);
            this.CbTHead.TabIndex = 5;
            this.CbTHead.SelectedIndexChanged += new System.EventHandler(this.CbTHead_SelectedIndexChanged);
            // 
            // CbParte
            // 
            this.CbParte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbParte.FormattingEnabled = true;
            this.CbParte.Location = new System.Drawing.Point(126, 54);
            this.CbParte.Name = "CbParte";
            this.CbParte.Size = new System.Drawing.Size(203, 21);
            this.CbParte.TabIndex = 3;
            this.CbParte.SelectedIndexChanged += new System.EventHandler(this.CbParte_SelectedIndexChanged);
            // 
            // TbCodigoLote
            // 
            this.TbCodigoLote.Location = new System.Drawing.Point(127, 20);
            this.TbCodigoLote.Name = "TbCodigoLote";
            this.TbCodigoLote.Size = new System.Drawing.Size(144, 20);
            this.TbCodigoLote.TabIndex = 1;
            this.TbCodigoLote.Leave += new System.EventHandler(this.TbCodigoLote_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Transaccion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Parte";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Codigo Lote";
            // 
            // Ajuste_Parametros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(648, 396);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.CbSistema);
            this.Controls.Add(this.BtnGuardar);
            this.Controls.Add(this.NumParamValue);
            this.Controls.Add(this.TbParamName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LParametros);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CbTHead);
            this.Controls.Add(this.CbParte);
            this.Controls.Add(this.TbCodigoLote);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Ajuste_Parametros";
            this.Text = "Mantenedor de Parametros";
            this.Load += new System.EventHandler(this.Ajuste_Parametros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumParamValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LParametros)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TbCodigoLote;
        private System.Windows.Forms.ComboBox CbParte;
        private System.Windows.Forms.ComboBox CbTHead;
        private System.Windows.Forms.Label label4;
        private BrightIdeasSoftware.ObjectListView LParametros;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TbParamName;
        private System.Windows.Forms.NumericUpDown NumParamValue;
        private System.Windows.Forms.Button BtnGuardar;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.ComboBox CbSistema;
        private System.Windows.Forms.Label label7;
    }
}
