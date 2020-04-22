namespace AFN_WF_C.PCClient.Vistas.Busquedas
{
    partial class lista_cambios
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
            this.detalle = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btn_imprimir = new System.Windows.Forms.Button();
            this.Label11 = new System.Windows.Forms.Label();
            this.Tarticulo = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.btn_consulta = new System.Windows.Forms.Button();
            this.cod_art = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.detalle)).BeginInit();
            this.SuspendLayout();
            // 
            // detalle
            // 
            this.detalle.AllColumns.Add(this.olvColumn1);
            this.detalle.AllColumns.Add(this.olvColumn2);
            this.detalle.AllColumns.Add(this.olvColumn4);
            this.detalle.AllColumns.Add(this.olvColumn5);
            this.detalle.AllColumns.Add(this.olvColumn3);
            this.detalle.AllColumns.Add(this.olvColumn6);
            this.detalle.AllColumns.Add(this.olvColumn7);
            this.detalle.AllColumns.Add(this.olvColumn8);
            this.detalle.CellEditUseWholeCell = false;
            this.detalle.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn3,
            this.olvColumn6,
            this.olvColumn7,
            this.olvColumn8});
            this.detalle.Cursor = System.Windows.Forms.Cursors.Default;
            this.detalle.FullRowSelect = true;
            this.detalle.Location = new System.Drawing.Point(15, 107);
            this.detalle.MultiSelect = false;
            this.detalle.Name = "detalle";
            this.detalle.ShowGroups = false;
            this.detalle.Size = new System.Drawing.Size(671, 146);
            this.detalle.TabIndex = 42;
            this.detalle.UseCompatibleStateImageBehavior = false;
            this.detalle.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "cod_articulo";
            this.olvColumn1.Text = "Codigo";
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "parte";
            this.olvColumn2.Text = "Parte";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "fecha_inicio";
            this.olvColumn4.AspectToStringFormat = "{0:d}";
            this.olvColumn4.Text = "Fecha Cambio";
            this.olvColumn4.Width = 115;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "cantidad";
            this.olvColumn5.Text = "Cantidad Cambio";
            this.olvColumn5.Width = 126;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "zona";
            this.olvColumn3.Text = "Zona Actual";
            this.olvColumn3.Width = 130;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "subzona";
            this.olvColumn6.Text = "Subzona Actual";
            this.olvColumn6.Width = 156;
            // 
            // btn_imprimir
            // 
            this.btn_imprimir.Location = new System.Drawing.Point(573, 47);
            this.btn_imprimir.Name = "btn_imprimir";
            this.btn_imprimir.Size = new System.Drawing.Size(63, 34);
            this.btn_imprimir.TabIndex = 41;
            this.btn_imprimir.Text = "Imprimir";
            this.btn_imprimir.UseVisualStyleBackColor = true;
            this.btn_imprimir.Click += new System.EventHandler(this.btn_imprimir_Click);
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(12, 87);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(131, 16);
            this.Label11.TabIndex = 39;
            this.Label11.Text = "Cambios del Artículo";
            // 
            // Tarticulo
            // 
            this.Tarticulo.Enabled = false;
            this.Tarticulo.Location = new System.Drawing.Point(81, 47);
            this.Tarticulo.Name = "Tarticulo";
            this.Tarticulo.Size = new System.Drawing.Size(355, 22);
            this.Tarticulo.TabIndex = 38;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(13, 47);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(57, 16);
            this.Label3.TabIndex = 37;
            this.Label3.Text = "Nombre";
            // 
            // btn_consulta
            // 
            this.btn_consulta.Location = new System.Drawing.Point(482, 47);
            this.btn_consulta.Name = "btn_consulta";
            this.btn_consulta.Size = new System.Drawing.Size(63, 34);
            this.btn_consulta.TabIndex = 36;
            this.btn_consulta.Text = "Buscar";
            this.btn_consulta.UseVisualStyleBackColor = true;
            this.btn_consulta.Click += new System.EventHandler(this.btn_consulta_Click);
            // 
            // cod_art
            // 
            this.cod_art.Location = new System.Drawing.Point(157, 25);
            this.cod_art.Name = "cod_art";
            this.cod_art.Size = new System.Drawing.Size(100, 13);
            this.cod_art.TabIndex = 35;
            this.cod_art.Text = "Label2";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 22);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(123, 16);
            this.Label1.TabIndex = 34;
            this.Label1.Text = "Seleccione Artículo";
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "zona_anterior";
            this.olvColumn7.Text = "Zona Anterior";
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "subzona_anterior";
            this.olvColumn8.Text = "Subzona Anterior";
            // 
            // lista_cambios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(703, 264);
            this.Controls.Add(this.detalle);
            this.Controls.Add(this.btn_imprimir);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.Tarticulo);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.btn_consulta);
            this.Controls.Add(this.cod_art);
            this.Controls.Add(this.Label1);
            this.Name = "lista_cambios";
            this.Text = "Ficha de Cambio de Zona";
            this.Load += new System.EventHandler(this.lista_cambios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.detalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btn_imprimir;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.TextBox Tarticulo;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button btn_consulta;
        internal System.Windows.Forms.Label cod_art;
        internal System.Windows.Forms.Label Label1;
        private BrightIdeasSoftware.ObjectListView detalle;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
    }
}
