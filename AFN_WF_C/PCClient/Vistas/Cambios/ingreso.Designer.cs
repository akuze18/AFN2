namespace AFN_WF_C.PCClient.Vistas.Cambios
{
    partial class ingreso
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
            this.pasos = new System.Windows.Forms.TabControl();
            this.paso1 = new System.Windows.Forms.TabPage();
            this.ficha_basica = new AFN_WF_C.PCClient.Vistas.Cambios.ingreso_financiero();
            this.paso2 = new System.Windows.Forms.TabPage();
            this.ficha_ifrs = new AFN_WF_C.PCClient.Vistas.Cambios.ingreso_ifrs();
            this.paso3 = new System.Windows.Forms.TabPage();
            this.paso4 = new System.Windows.Forms.TabPage();
            this.CkEstado = new System.Windows.Forms.CheckBox();
            this.TFulldescrip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.artic = new System.Windows.Forms.TextBox();
            this.ficha_grupo = new AFN_WF_C.PCClient.Vistas.Cambios.ingreso_invent_grup();
            this.btn_modif = new System.Windows.Forms.Button();
            this.btn_new = new System.Windows.Forms.Button();
            this.ficha_articulo = new AFN_WF_C.PCClient.Vistas.Cambios.ingreso_invent_articulo();
            this.pasos.SuspendLayout();
            this.paso1.SuspendLayout();
            this.paso2.SuspendLayout();
            this.paso3.SuspendLayout();
            this.paso4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pasos
            // 
            this.pasos.Controls.Add(this.paso1);
            this.pasos.Controls.Add(this.paso2);
            this.pasos.Controls.Add(this.paso3);
            this.pasos.Controls.Add(this.paso4);
            this.pasos.ItemSize = new System.Drawing.Size(260, 21);
            this.pasos.Location = new System.Drawing.Point(19, 116);
            this.pasos.Name = "pasos";
            this.pasos.SelectedIndex = 0;
            this.pasos.Size = new System.Drawing.Size(1058, 485);
            this.pasos.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.pasos.TabIndex = 7;
            this.pasos.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabControl1_DrawItem);
            this.pasos.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.pasos_Selecting);
            // 
            // paso1
            // 
            this.paso1.Controls.Add(this.ficha_basica);
            this.paso1.Location = new System.Drawing.Point(4, 25);
            this.paso1.Name = "paso1";
            this.paso1.Padding = new System.Windows.Forms.Padding(3);
            this.paso1.Size = new System.Drawing.Size(1050, 456);
            this.paso1.TabIndex = 0;
            this.paso1.Text = "Ficha Básica";
            this.paso1.UseVisualStyleBackColor = true;
            // 
            // ficha_basica
            // 
            this.ficha_basica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ficha_basica.Location = new System.Drawing.Point(3, 3);
            this.ficha_basica.Margin = new System.Windows.Forms.Padding(4);
            this.ficha_basica.Name = "ficha_basica";
            this.ficha_basica.Size = new System.Drawing.Size(1044, 450);
            this.ficha_basica.TabIndex = 0;
            // 
            // paso2
            // 
            this.paso2.Controls.Add(this.ficha_ifrs);
            this.paso2.Location = new System.Drawing.Point(4, 25);
            this.paso2.Name = "paso2";
            this.paso2.Padding = new System.Windows.Forms.Padding(3);
            this.paso2.Size = new System.Drawing.Size(1050, 456);
            this.paso2.TabIndex = 1;
            this.paso2.Text = "Ficha IFRS";
            this.paso2.UseVisualStyleBackColor = true;
            // 
            // ficha_ifrs
            // 
            this.ficha_ifrs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ficha_ifrs.Location = new System.Drawing.Point(3, 3);
            this.ficha_ifrs.Margin = new System.Windows.Forms.Padding(4);
            this.ficha_ifrs.Name = "ficha_ifrs";
            this.ficha_ifrs.Size = new System.Drawing.Size(1044, 450);
            this.ficha_ifrs.TabIndex = 0;
            // 
            // paso3
            // 
            this.paso3.Controls.Add(this.ficha_grupo);
            this.paso3.Location = new System.Drawing.Point(4, 25);
            this.paso3.Name = "paso3";
            this.paso3.Padding = new System.Windows.Forms.Padding(3);
            this.paso3.Size = new System.Drawing.Size(1050, 456);
            this.paso3.TabIndex = 2;
            this.paso3.Text = "Descripción por Grupo";
            this.paso3.UseVisualStyleBackColor = true;
            // 
            // paso4
            // 
            this.paso4.Controls.Add(this.ficha_articulo);
            this.paso4.Location = new System.Drawing.Point(4, 25);
            this.paso4.Name = "paso4";
            this.paso4.Padding = new System.Windows.Forms.Padding(3);
            this.paso4.Size = new System.Drawing.Size(1050, 456);
            this.paso4.TabIndex = 3;
            this.paso4.Text = "Descripción por Articulo";
            this.paso4.UseVisualStyleBackColor = true;
            // 
            // CkEstado
            // 
            this.CkEstado.AutoSize = true;
            this.CkEstado.Location = new System.Drawing.Point(606, 36);
            this.CkEstado.Name = "CkEstado";
            this.CkEstado.Size = new System.Drawing.Size(80, 20);
            this.CkEstado.TabIndex = 4;
            this.CkEstado.Text = "Activado";
            this.CkEstado.UseVisualStyleBackColor = true;
            // 
            // TFulldescrip
            // 
            this.TFulldescrip.Location = new System.Drawing.Point(136, 36);
            this.TFulldescrip.Multiline = true;
            this.TFulldescrip.Name = "TFulldescrip";
            this.TFulldescrip.Size = new System.Drawing.Size(439, 59);
            this.TFulldescrip.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Descripcion Completa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Codigo Lote";
            // 
            // artic
            // 
            this.artic.Location = new System.Drawing.Point(23, 36);
            this.artic.Name = "artic";
            this.artic.Size = new System.Drawing.Size(83, 22);
            this.artic.TabIndex = 0;
            // 
            // ficha_grupo
            // 
            this.ficha_grupo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ficha_grupo.Location = new System.Drawing.Point(3, 3);
            this.ficha_grupo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ficha_grupo.Name = "ficha_grupo";
            this.ficha_grupo.Size = new System.Drawing.Size(1044, 450);
            this.ficha_grupo.TabIndex = 0;
            // 
            // btn_modif
            // 
            this.btn_modif.Image = global::AFN_WF_C.Properties.Resources._32_find;
            this.btn_modif.Location = new System.Drawing.Point(819, 47);
            this.btn_modif.Name = "btn_modif";
            this.btn_modif.Size = new System.Drawing.Size(64, 48);
            this.btn_modif.TabIndex = 6;
            this.btn_modif.Text = "Buscar";
            this.btn_modif.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_modif.UseVisualStyleBackColor = true;
            this.btn_modif.Click += new System.EventHandler(this.btn_modif_Click);
            // 
            // btn_new
            // 
            this.btn_new.Image = global::AFN_WF_C.Properties.Resources._32_add;
            this.btn_new.Location = new System.Drawing.Point(737, 47);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(64, 48);
            this.btn_new.TabIndex = 5;
            this.btn_new.Text = "Limpiar";
            this.btn_new.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // ficha_articulo
            // 
            this.ficha_articulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ficha_articulo.Location = new System.Drawing.Point(3, 3);
            this.ficha_articulo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ficha_articulo.Name = "ficha_articulo";
            this.ficha_articulo.Size = new System.Drawing.Size(1044, 450);
            this.ficha_articulo.TabIndex = 0;
            // 
            // ingreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1096, 621);
            this.Controls.Add(this.pasos);
            this.Controls.Add(this.btn_modif);
            this.Controls.Add(this.btn_new);
            this.Controls.Add(this.CkEstado);
            this.Controls.Add(this.TFulldescrip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.artic);
            this.Name = "ingreso";
            this.Text = "Mantenedor de Bienes de Activo Fijo";
            this.Load += new System.EventHandler(this.ingreso_Load);
            this.pasos.ResumeLayout(false);
            this.paso1.ResumeLayout(false);
            this.paso2.ResumeLayout(false);
            this.paso3.ResumeLayout(false);
            this.paso4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox artic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TFulldescrip;
        private System.Windows.Forms.CheckBox CkEstado;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_modif;
        private System.Windows.Forms.TabControl pasos;
        private System.Windows.Forms.TabPage paso1;
        private System.Windows.Forms.TabPage paso2;
        private System.Windows.Forms.TabPage paso3;
        private System.Windows.Forms.TabPage paso4;
        private ingreso_financiero ficha_basica;
        private ingreso_ifrs ficha_ifrs;
        private ingreso_invent_grup ficha_grupo;
        private ingreso_invent_articulo ficha_articulo;
    }
}
