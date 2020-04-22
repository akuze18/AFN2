namespace AFN_WF_C.PCClient.Vistas.Sistema
{
    partial class Depreciar
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.inMonth = new System.Windows.Forms.ComboBox();
            this.inYear = new System.Windows.Forms.NumericUpDown();
            this.btn_generar = new System.Windows.Forms.Button();
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 2);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.inMonth);
            this.splitContainer1.Panel1.Controls.Add(this.inYear);
            this.splitContainer1.Panel1.Controls.Add(this.btn_generar);
            this.splitContainer1.Panel1MinSize = 30;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.objectListView1);
            this.splitContainer1.Size = new System.Drawing.Size(801, 373);
            this.splitContainer1.SplitterDistance = 43;
            this.splitContainer1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Año";
            // 
            // inMonth
            // 
            this.inMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inMonth.FormattingEnabled = true;
            this.inMonth.Location = new System.Drawing.Point(223, 6);
            this.inMonth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.inMonth.Name = "inMonth";
            this.inMonth.Size = new System.Drawing.Size(183, 24);
            this.inMonth.TabIndex = 2;
            // 
            // inYear
            // 
            this.inYear.Location = new System.Drawing.Point(61, 7);
            this.inYear.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.inYear.Name = "inYear";
            this.inYear.Size = new System.Drawing.Size(84, 22);
            this.inYear.TabIndex = 1;
            // 
            // btn_generar
            // 
            this.btn_generar.Location = new System.Drawing.Point(628, 4);
            this.btn_generar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_generar.Name = "btn_generar";
            this.btn_generar.Size = new System.Drawing.Size(151, 28);
            this.btn_generar.TabIndex = 0;
            this.btn_generar.Text = "Generar";
            this.btn_generar.UseVisualStyleBackColor = true;
            this.btn_generar.Click += new System.EventHandler(this.btn_generar_Click);
            // 
            // objectListView1
            // 
            this.objectListView1.CellEditUseWholeCell = false;
            this.objectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListView1.FullRowSelect = true;
            this.objectListView1.Location = new System.Drawing.Point(0, 0);
            this.objectListView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.objectListView1.MultiSelect = false;
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.ShowFilterMenuOnRightClick = false;
            this.objectListView1.ShowGroups = false;
            this.objectListView1.Size = new System.Drawing.Size(801, 326);
            this.objectListView1.TabIndex = 1;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.UseFiltering = true;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            // 
            // Depreciar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(807, 377);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "Depreciar";
            this.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_generar;
        private BrightIdeasSoftware.ObjectListView objectListView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.NumericUpDown inYear;
        private System.Windows.Forms.ComboBox inMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
