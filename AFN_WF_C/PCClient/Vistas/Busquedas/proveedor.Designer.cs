namespace AFN_WF_C.PCClient.Vistas.Busquedas
{
    partial class proveedor
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
            this.MosResult2 = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Lresultado = new System.Windows.Forms.Label();
            this.btn_marcar = new System.Windows.Forms.Button();
            this.btn_buscar = new System.Windows.Forms.Button();
            this.cboZona = new System.Windows.Forms.ComboBox();
            this.Tcodigo = new System.Windows.Forms.TextBox();
            this.Tdescrip = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MosResult2)).BeginInit();
            this.SuspendLayout();
            // 
            // MosResult2
            // 
            this.MosResult2.AllColumns.Add(this.olvColumn1);
            this.MosResult2.AllColumns.Add(this.olvColumn2);
            this.MosResult2.AllColumns.Add(this.olvColumn3);
            this.MosResult2.CellEditUseWholeCell = false;
            this.MosResult2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3});
            this.MosResult2.Cursor = System.Windows.Forms.Cursors.Default;
            this.MosResult2.Location = new System.Drawing.Point(19, 119);
            this.MosResult2.Name = "MosResult2";
            this.MosResult2.Size = new System.Drawing.Size(520, 150);
            this.MosResult2.TabIndex = 20;
            this.MosResult2.UseCompatibleStateImageBehavior = false;
            this.MosResult2.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "COD";
            this.olvColumn1.Text = "RUT";
            this.olvColumn1.Width = 92;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "VENDNAME";
            this.olvColumn2.Text = "NOMBRE";
            this.olvColumn2.Width = 140;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "PARVENDID";
            this.olvColumn3.Text = "PARVENDID";
            this.olvColumn3.Width = 142;
            // 
            // Lresultado
            // 
            this.Lresultado.AutoSize = true;
            this.Lresultado.Location = new System.Drawing.Point(52, 103);
            this.Lresultado.Name = "Lresultado";
            this.Lresultado.Size = new System.Drawing.Size(83, 16);
            this.Lresultado.TabIndex = 18;
            this.Lresultado.Text = "Resultados :";
            // 
            // btn_marcar
            // 
            this.btn_marcar.Location = new System.Drawing.Point(427, 93);
            this.btn_marcar.Name = "btn_marcar";
            this.btn_marcar.Size = new System.Drawing.Size(83, 23);
            this.btn_marcar.TabIndex = 17;
            this.btn_marcar.Text = "Seleccionar";
            this.btn_marcar.UseVisualStyleBackColor = true;
            this.btn_marcar.Click += new System.EventHandler(this.btn_marcar_Click);
            // 
            // btn_buscar
            // 
            this.btn_buscar.Location = new System.Drawing.Point(304, 64);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(75, 23);
            this.btn_buscar.TabIndex = 16;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = true;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // cboZona
            // 
            this.cboZona.Enabled = false;
            this.cboZona.FormattingEnabled = true;
            this.cboZona.Location = new System.Drawing.Point(89, 66);
            this.cboZona.Name = "cboZona";
            this.cboZona.Size = new System.Drawing.Size(177, 24);
            this.cboZona.TabIndex = 15;
            // 
            // Tcodigo
            // 
            this.Tcodigo.Location = new System.Drawing.Point(89, 40);
            this.Tcodigo.Name = "Tcodigo";
            this.Tcodigo.Size = new System.Drawing.Size(177, 22);
            this.Tcodigo.TabIndex = 14;
            // 
            // Tdescrip
            // 
            this.Tdescrip.Location = new System.Drawing.Point(89, 14);
            this.Tdescrip.Name = "Tdescrip";
            this.Tdescrip.Size = new System.Drawing.Size(290, 22);
            this.Tdescrip.TabIndex = 13;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(16, 66);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(45, 16);
            this.Label3.TabIndex = 12;
            this.Label3.Text = "Zona :";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(16, 40);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(43, 16);
            this.Label2.TabIndex = 11;
            this.Label2.Text = "RUT :";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(16, 14);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(63, 16);
            this.Label1.TabIndex = 10;
            this.Label1.Text = "Nombre :";
            // 
            // proveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(557, 287);
            this.Controls.Add(this.MosResult2);
            this.Controls.Add(this.Lresultado);
            this.Controls.Add(this.btn_marcar);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.cboZona);
            this.Controls.Add(this.Tcodigo);
            this.Controls.Add(this.Tdescrip);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Name = "proveedor";
            this.Text = "Buscar un proveedor";
            this.Load += new System.EventHandler(this.bus_prov_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MosResult2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Lresultado;
        internal System.Windows.Forms.Button btn_marcar;
        internal System.Windows.Forms.Button btn_buscar;
        internal System.Windows.Forms.ComboBox cboZona;
        internal System.Windows.Forms.TextBox Tcodigo;
        internal System.Windows.Forms.TextBox Tdescrip;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        private BrightIdeasSoftware.ObjectListView MosResult2;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
    }
}
