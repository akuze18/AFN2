namespace AFN_WF_C.PCClient.Vistas.Busquedas
{
    partial class obc_borrador
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btn_procAll = new System.Windows.Forms.Button();
            this.btn_toProcess = new System.Windows.Forms.Button();
            this.btn_toEdit = new System.Windows.Forms.Button();
            this.btn_toClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.objectListView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_toClear);
            this.splitContainer1.Panel2.Controls.Add(this.btn_procAll);
            this.splitContainer1.Panel2.Controls.Add(this.btn_toProcess);
            this.splitContainer1.Panel2.Controls.Add(this.btn_toEdit);
            this.splitContainer1.Size = new System.Drawing.Size(379, 342);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 1;
            // 
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.olvColumn1);
            this.objectListView1.AllColumns.Add(this.olvColumn3);
            this.objectListView1.AllColumns.Add(this.olvColumn2);
            this.objectListView1.CellEditUseWholeCell = false;
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn3,
            this.olvColumn2});
            this.objectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListView1.FullRowSelect = true;
            this.objectListView1.HasCollapsibleGroups = false;
            this.objectListView1.HideSelection = false;
            this.objectListView1.Location = new System.Drawing.Point(0, 0);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.ShowGroups = false;
            this.objectListView1.Size = new System.Drawing.Size(302, 342);
            this.objectListView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.objectListView1.TabIndex = 0;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "codigo";
            this.olvColumn1.Text = "Entrada";
            this.olvColumn1.Width = 72;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "saldo";
            this.olvColumn3.AspectToStringFormat = "{0:C}";
            this.olvColumn3.DisplayIndex = 2;
            this.olvColumn3.Text = "Monto";
            this.olvColumn3.Width = 72;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "descripcion";
            this.olvColumn2.DisplayIndex = 1;
            this.olvColumn2.Text = "Descripcion";
            this.olvColumn2.Width = 128;
            // 
            // btn_procAll
            // 
            this.btn_procAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_procAll.Location = new System.Drawing.Point(7, 274);
            this.btn_procAll.Name = "btn_procAll";
            this.btn_procAll.Size = new System.Drawing.Size(63, 40);
            this.btn_procAll.TabIndex = 2;
            this.btn_procAll.Text = "Procesar Todos";
            this.btn_procAll.UseVisualStyleBackColor = true;
            this.btn_procAll.Click += new System.EventHandler(this.btn_procAll_Click);
            // 
            // btn_toProcess
            // 
            this.btn_toProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_toProcess.Location = new System.Drawing.Point(7, 150);
            this.btn_toProcess.Name = "btn_toProcess";
            this.btn_toProcess.Size = new System.Drawing.Size(63, 40);
            this.btn_toProcess.TabIndex = 1;
            this.btn_toProcess.Text = "Procesar";
            this.btn_toProcess.UseVisualStyleBackColor = true;
            this.btn_toProcess.Click += new System.EventHandler(this.btn_toProcess_Click);
            // 
            // btn_toEdit
            // 
            this.btn_toEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_toEdit.Location = new System.Drawing.Point(7, 43);
            this.btn_toEdit.Name = "btn_toEdit";
            this.btn_toEdit.Size = new System.Drawing.Size(63, 40);
            this.btn_toEdit.TabIndex = 0;
            this.btn_toEdit.Text = "Editar";
            this.btn_toEdit.UseVisualStyleBackColor = true;
            this.btn_toEdit.Click += new System.EventHandler(this.btn_toEdit_Click);
            // 
            // btn_toClear
            // 
            this.btn_toClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_toClear.Location = new System.Drawing.Point(7, 94);
            this.btn_toClear.Name = "btn_toClear";
            this.btn_toClear.Size = new System.Drawing.Size(63, 40);
            this.btn_toClear.TabIndex = 3;
            this.btn_toClear.Text = "Eliminar";
            this.btn_toClear.UseVisualStyleBackColor = true;
            // 
            // obc_borrador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(379, 342);
            this.Controls.Add(this.splitContainer1);
            this.Name = "obc_borrador";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Text = "Borradores Disponibles";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView objectListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btn_toEdit;
        private System.Windows.Forms.Button btn_procAll;
        private System.Windows.Forms.Button btn_toProcess;
        private System.Windows.Forms.Button btn_toClear;

    }
}
