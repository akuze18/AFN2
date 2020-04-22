namespace AFN_WF_C.PCClient.Vistas.Acciones
{
    partial class ManagerBatch
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDeleteOne = new System.Windows.Forms.Button();
            this.panelC = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnActiveAll = new System.Windows.Forms.Button();
            this.panelB = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnActiveSel = new System.Windows.Forms.Button();
            this.panelA = new System.Windows.Forms.Panel();
            this.DGLotes = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lbTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGLotes)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(27, 284);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(100, 0, 100, 0);
            this.panel2.Size = new System.Drawing.Size(499, 20);
            this.panel2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panelC);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panelB);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panelA);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(27, 304);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(499, 60);
            this.panel1.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnDeleteOne);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(350, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(100, 0, 100, 0);
            this.panel3.Size = new System.Drawing.Size(104, 60);
            this.panel3.TabIndex = 7;
            // 
            // btnDeleteOne
            // 
            this.btnDeleteOne.Location = new System.Drawing.Point(0, 7);
            this.btnDeleteOne.Name = "btnDeleteOne";
            this.btnDeleteOne.Size = new System.Drawing.Size(103, 46);
            this.btnDeleteOne.TabIndex = 4;
            this.btnDeleteOne.Text = "Eliminar Seleccionado";
            this.btnDeleteOne.UseVisualStyleBackColor = true;
            this.btnDeleteOne.Click += new System.EventHandler(this.btnDeleteOne_Click);
            // 
            // panelC
            // 
            this.panelC.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelC.Location = new System.Drawing.Point(285, 0);
            this.panelC.Name = "panelC";
            this.panelC.Size = new System.Drawing.Size(65, 60);
            this.panelC.TabIndex = 12;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnActiveAll);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(172, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(113, 60);
            this.panel7.TabIndex = 11;
            // 
            // btnActiveAll
            // 
            this.btnActiveAll.Location = new System.Drawing.Point(4, 7);
            this.btnActiveAll.Name = "btnActiveAll";
            this.btnActiveAll.Size = new System.Drawing.Size(103, 46);
            this.btnActiveAll.TabIndex = 3;
            this.btnActiveAll.Text = "Activar Todo";
            this.btnActiveAll.UseVisualStyleBackColor = true;
            this.btnActiveAll.Click += new System.EventHandler(this.btnActiveAll_Click);
            // 
            // panelB
            // 
            this.panelB.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelB.Location = new System.Drawing.Point(140, 0);
            this.panelB.Name = "panelB";
            this.panelB.Size = new System.Drawing.Size(32, 60);
            this.panelB.TabIndex = 10;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnActiveSel);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(32, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(108, 60);
            this.panel5.TabIndex = 8;
            // 
            // btnActiveSel
            // 
            this.btnActiveSel.Location = new System.Drawing.Point(3, 7);
            this.btnActiveSel.Name = "btnActiveSel";
            this.btnActiveSel.Size = new System.Drawing.Size(103, 46);
            this.btnActiveSel.TabIndex = 2;
            this.btnActiveSel.Text = "Activar Seleccionado";
            this.btnActiveSel.UseVisualStyleBackColor = true;
            this.btnActiveSel.Click += new System.EventHandler(this.btnActiveSel_Click);
            // 
            // panelA
            // 
            this.panelA.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelA.Location = new System.Drawing.Point(0, 0);
            this.panelA.Name = "panelA";
            this.panelA.Size = new System.Drawing.Size(32, 60);
            this.panelA.TabIndex = 9;
            // 
            // DGLotes
            // 
            this.DGLotes.AllColumns.Add(this.olvColumn1);
            this.DGLotes.AllColumns.Add(this.olvColumn3);
            this.DGLotes.AllColumns.Add(this.olvColumn2);
            this.DGLotes.CellEditUseWholeCell = false;
            this.DGLotes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn3,
            this.olvColumn2});
            this.DGLotes.Cursor = System.Windows.Forms.Cursors.Default;
            this.DGLotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGLotes.FullRowSelect = true;
            this.DGLotes.Location = new System.Drawing.Point(27, 41);
            this.DGLotes.MultiSelect = false;
            this.DGLotes.Name = "DGLotes";
            this.DGLotes.ShowGroups = false;
            this.DGLotes.Size = new System.Drawing.Size(499, 323);
            this.DGLotes.TabIndex = 1;
            this.DGLotes.UseCompatibleStateImageBehavior = false;
            this.DGLotes.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "id";
            this.olvColumn1.Text = "Codigo Lote";
            this.olvColumn1.Width = 99;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "purchase_date";
            this.olvColumn3.AspectToStringFormat = "{0:d}";
            this.olvColumn3.DisplayIndex = 2;
            this.olvColumn3.Text = "Fecha Compra";
            this.olvColumn3.Width = 119;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "descrip";
            this.olvColumn2.DisplayIndex = 1;
            this.olvColumn2.Text = "Descripcion";
            this.olvColumn2.Width = 246;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Location = new System.Drawing.Point(27, 25);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(105, 16);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Lotes sin Activar";
            // 
            // ManagerBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(553, 389);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DGLotes);
            this.Controls.Add(this.lbTitle);
            this.Name = "ManagerBatch";
            this.Text = "Mantenedor de Lotes";
            this.Load += new System.EventHandler(this.ManagerBatch_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGLotes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private BrightIdeasSoftware.ObjectListView DGLotes;
        private System.Windows.Forms.Button btnActiveSel;
        private System.Windows.Forms.Button btnActiveAll;
        private System.Windows.Forms.Button btnDeleteOne;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelC;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panelB;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panelA;
        private System.Windows.Forms.Panel panel2;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
    }
}
