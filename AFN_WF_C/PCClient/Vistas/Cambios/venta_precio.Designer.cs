namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    partial class venta_precio
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
            this.detalle_venta = new BrightIdeasSoftware.ObjectListView();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.TprecioExt = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Lestado_doc = new System.Windows.Forms.Label();
            this.DTfecha = new System.Windows.Forms.DateTimePicker();
            this.Tdocumento = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_add_new = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.detalle_venta)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // detalle_venta
            // 
            this.detalle_venta.CellEditUseWholeCell = false;
            this.detalle_venta.Cursor = System.Windows.Forms.Cursors.Default;
            this.detalle_venta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detalle_venta.Location = new System.Drawing.Point(0, 0);
            this.detalle_venta.Name = "detalle_venta";
            this.detalle_venta.Size = new System.Drawing.Size(690, 132);
            this.detalle_venta.TabIndex = 18;
            this.detalle_venta.UseCompatibleStateImageBehavior = false;
            this.detalle_venta.View = System.Windows.Forms.View.Details;
            this.detalle_venta.DoubleClick += new System.EventHandler(this.detalle_venta_DoubleClick);
            this.detalle_venta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.detalle_venta_KeyUp);
            // 
            // btn_guardar
            // 
            this.btn_guardar.Location = new System.Drawing.Point(296, 35);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(80, 34);
            this.btn_guardar.TabIndex = 17;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // TprecioExt
            // 
            this.TprecioExt.Location = new System.Drawing.Point(533, 27);
            this.TprecioExt.Name = "TprecioExt";
            this.TprecioExt.Size = new System.Drawing.Size(128, 22);
            this.TprecioExt.TabIndex = 16;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(540, 8);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(111, 16);
            this.Label4.TabIndex = 15;
            this.Label4.Text = "Total Documento";
            // 
            // Lestado_doc
            // 
            this.Lestado_doc.AutoSize = true;
            this.Lestado_doc.Location = new System.Drawing.Point(280, 13);
            this.Lestado_doc.Name = "Lestado_doc";
            this.Lestado_doc.Size = new System.Drawing.Size(49, 16);
            this.Lestado_doc.TabIndex = 13;
            this.Lestado_doc.Text = "Label3";
            // 
            // DTfecha
            // 
            this.DTfecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTfecha.Location = new System.Drawing.Point(106, 39);
            this.DTfecha.Name = "DTfecha";
            this.DTfecha.Size = new System.Drawing.Size(159, 22);
            this.DTfecha.TabIndex = 12;
            // 
            // Tdocumento
            // 
            this.Tdocumento.Location = new System.Drawing.Point(105, 10);
            this.Tdocumento.Name = "Tdocumento";
            this.Tdocumento.Size = new System.Drawing.Size(160, 22);
            this.Tdocumento.TabIndex = 11;
            this.Tdocumento.TextChanged += new System.EventHandler(this.Tdocumento_TextChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(9, 39);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(77, 16);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "Fecha Doc.";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(9, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(77, 16);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "Documento";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.Tdocumento);
            this.panel1.Controls.Add(this.Lestado_doc);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Controls.Add(this.DTfecha);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(15, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(690, 77);
            this.panel1.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_add_new);
            this.panel2.Controls.Add(this.btn_guardar);
            this.panel2.Controls.Add(this.Label4);
            this.panel2.Controls.Add(this.TprecioExt);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(15, 224);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(690, 84);
            this.panel2.TabIndex = 20;
            // 
            // btn_add_new
            // 
            this.btn_add_new.Location = new System.Drawing.Point(12, 8);
            this.btn_add_new.Name = "btn_add_new";
            this.btn_add_new.Size = new System.Drawing.Size(154, 27);
            this.btn_add_new.TabIndex = 14;
            this.btn_add_new.Text = "Agregar Articulo";
            this.btn_add_new.UseVisualStyleBackColor = true;
            this.btn_add_new.Click += new System.EventHandler(this.btn_add_new_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.detalle_venta);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(15, 92);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(690, 132);
            this.panel3.TabIndex = 21;
            // 
            // venta_precio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(720, 323);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "venta_precio";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Text = "Ingreso de factura de venta de Activo Fijo";
            this.Load += new System.EventHandler(this.venta_precio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.detalle_venta)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btn_guardar;
        internal System.Windows.Forms.TextBox TprecioExt;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Lestado_doc;
        internal System.Windows.Forms.DateTimePicker DTfecha;
        internal System.Windows.Forms.TextBox Tdocumento;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        private BrightIdeasSoftware.ObjectListView detalle_venta;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_add_new;
    }
}
