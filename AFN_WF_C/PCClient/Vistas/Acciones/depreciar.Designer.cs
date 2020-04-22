namespace AFN_WF_C.PCClient.Vistas.Acciones
{
    partial class depreciar
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
            this.EnvChoose = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.objListZones = new BrightIdeasSoftware.ObjectListView();
            ((System.ComponentModel.ISupportInitialize)(this.objListZones)).BeginInit();
            this.SuspendLayout();
            // 
            // EnvChoose
            // 
            this.EnvChoose.Location = new System.Drawing.Point(25, 23);
            this.EnvChoose.Name = "EnvChoose";
            this.EnvChoose.Size = new System.Drawing.Size(586, 59);
            this.EnvChoose.TabIndex = 1;
            this.EnvChoose.TabStop = false;
            this.EnvChoose.Text = "Ambiente";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(248, 312);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Suspender Depreciacion";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // objListZones
            // 
            this.objListZones.CellEditUseWholeCell = false;
            this.objListZones.Location = new System.Drawing.Point(30, 109);
            this.objListZones.Name = "objListZones";
            this.objListZones.Size = new System.Drawing.Size(244, 170);
            this.objListZones.TabIndex = 2;
            this.objListZones.UseCompatibleStateImageBehavior = false;
            this.objListZones.View = System.Windows.Forms.View.Details;
            // 
            // depreciar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(664, 380);
            this.Controls.Add(this.objListZones);
            this.Controls.Add(this.EnvChoose);
            this.Controls.Add(this.button1);
            this.Name = "depreciar";
            this.Text = "Suspender/Reanudar Depreciacion";
            this.Load += new System.EventHandler(this.depreciar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.objListZones)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox EnvChoose;
        private BrightIdeasSoftware.ObjectListView objListZones;
    }
}
