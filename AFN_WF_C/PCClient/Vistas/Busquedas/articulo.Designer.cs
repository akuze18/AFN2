namespace AFN_WF_C.PCClient.Vistas.Busquedas
{
    partial class articulo
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
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cboZona = new System.Windows.Forms.ComboBox();
            this.Tdescrip = new System.Windows.Forms.TextBox();
            this.Tcodigo = new System.Windows.Forms.TextBox();
            this.MosResult = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btn_marcar = new System.Windows.Forms.Button();
            this.btn_buscar = new System.Windows.Forms.Button();
            this.Lresultado = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Fhasta = new System.Windows.Forms.DateTimePicker();
            this.Fdesde = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MosResult)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "fila";
            this.olvColumn5.IsVisible = false;
            this.olvColumn5.Text = "rowindx";
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "parte";
            this.olvColumn6.IsVisible = false;
            this.olvColumn6.Text = "parte";
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "status";
            this.olvColumn7.IsVisible = false;
            this.olvColumn7.Text = "estado";
            // 
            // cboZona
            // 
            this.cboZona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboZona.FormattingEnabled = true;
            this.cboZona.Location = new System.Drawing.Point(119, 90);
            this.cboZona.Name = "cboZona";
            this.cboZona.Size = new System.Drawing.Size(263, 21);
            this.cboZona.TabIndex = 10;
            this.cboZona.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboZona_KeyUp);
            // 
            // Tdescrip
            // 
            this.Tdescrip.Location = new System.Drawing.Point(119, 55);
            this.Tdescrip.Name = "Tdescrip";
            this.Tdescrip.Size = new System.Drawing.Size(263, 20);
            this.Tdescrip.TabIndex = 9;
            // 
            // Tcodigo
            // 
            this.Tcodigo.Location = new System.Drawing.Point(119, 23);
            this.Tcodigo.Name = "Tcodigo";
            this.Tcodigo.Size = new System.Drawing.Size(159, 20);
            this.Tcodigo.TabIndex = 8;
            // 
            // MosResult
            // 
            this.MosResult.AllColumns.Add(this.olvColumn1);
            this.MosResult.AllColumns.Add(this.olvColumn2);
            this.MosResult.AllColumns.Add(this.olvColumn3);
            this.MosResult.AllColumns.Add(this.olvColumn4);
            this.MosResult.AllColumns.Add(this.olvColumn5);
            this.MosResult.AllColumns.Add(this.olvColumn6);
            this.MosResult.AllColumns.Add(this.olvColumn7);
            this.MosResult.CellEditUseWholeCell = false;
            this.MosResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.MosResult.Cursor = System.Windows.Forms.Cursors.Default;
            this.MosResult.FullRowSelect = true;
            this.MosResult.Location = new System.Drawing.Point(23, 165);
            this.MosResult.MultiSelect = false;
            this.MosResult.Name = "MosResult";
            this.MosResult.ShowGroups = false;
            this.MosResult.Size = new System.Drawing.Size(624, 259);
            this.MosResult.TabIndex = 7;
            this.MosResult.UseCompatibleStateImageBehavior = false;
            this.MosResult.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "cod_articulo";
            this.olvColumn1.Text = "Artículo";
            this.olvColumn1.Width = 87;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "cantidad";
            this.olvColumn2.Text = "Cantidad";
            this.olvColumn2.Width = 100;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "zona";
            this.olvColumn3.Text = "Zona";
            this.olvColumn3.Width = 101;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "dsc_extra";
            this.olvColumn4.Text = "Descripción Artículo";
            this.olvColumn4.Width = 311;
            // 
            // btn_marcar
            // 
            this.btn_marcar.Location = new System.Drawing.Point(405, 130);
            this.btn_marcar.Name = "btn_marcar";
            this.btn_marcar.Size = new System.Drawing.Size(75, 23);
            this.btn_marcar.TabIndex = 6;
            this.btn_marcar.Text = "Seleccionar";
            this.btn_marcar.UseVisualStyleBackColor = true;
            this.btn_marcar.Click += new System.EventHandler(this.btn_marcar_Click);
            // 
            // btn_buscar
            // 
            this.btn_buscar.Location = new System.Drawing.Point(290, 130);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(75, 23);
            this.btn_buscar.TabIndex = 5;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = true;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // Lresultado
            // 
            this.Lresultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lresultado.Location = new System.Drawing.Point(58, 147);
            this.Lresultado.Name = "Lresultado";
            this.Lresultado.Size = new System.Drawing.Size(180, 15);
            this.Lresultado.TabIndex = 4;
            this.Lresultado.Text = "Resultados : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Fhasta);
            this.groupBox1.Controls.Add(this.Fdesde);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(421, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fecha Compra";
            // 
            // Fhasta
            // 
            this.Fhasta.Checked = false;
            this.Fhasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fhasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Fhasta.Location = new System.Drawing.Point(72, 63);
            this.Fhasta.Name = "Fhasta";
            this.Fhasta.ShowCheckBox = true;
            this.Fhasta.Size = new System.Drawing.Size(111, 20);
            this.Fhasta.TabIndex = 3;
            // 
            // Fdesde
            // 
            this.Fdesde.Checked = false;
            this.Fdesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fdesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Fdesde.Location = new System.Drawing.Point(73, 32);
            this.Fdesde.Name = "Fdesde";
            this.Fdesde.ShowCheckBox = true;
            this.Fdesde.Size = new System.Drawing.Size(110, 20);
            this.Fdesde.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Hasta";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Desde";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Zona";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Descripción";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Código Lote";
            // 
            // articulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(670, 447);
            this.Controls.Add(this.cboZona);
            this.Controls.Add(this.Tdescrip);
            this.Controls.Add(this.Tcodigo);
            this.Controls.Add(this.MosResult);
            this.Controls.Add(this.btn_marcar);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.Lresultado);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "articulo";
            this.Text = "Buscador de Artículo";
            this.Load += new System.EventHandler(this.articulo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MosResult)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Lresultado;
        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.Button btn_marcar;
        private BrightIdeasSoftware.ObjectListView MosResult;
        private System.Windows.Forms.DateTimePicker Fhasta;
        private System.Windows.Forms.DateTimePicker Fdesde;
        private System.Windows.Forms.TextBox Tcodigo;
        private System.Windows.Forms.TextBox Tdescrip;
        private System.Windows.Forms.ComboBox cboZona;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
    }
}
