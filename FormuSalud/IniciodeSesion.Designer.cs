namespace FormuSalud
{
    partial class IniciodeSesion
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IniciodeSesion));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.lblContraseña = new System.Windows.Forms.Label();
            this.lblRol = new System.Windows.Forms.Label();
            this.cbRol = new System.Windows.Forms.ComboBox();
            this.txtbDocumento = new System.Windows.Forms.TextBox();
            this.txtbContraseña = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.lklRegistrarse = new System.Windows.Forms.LinkLabel();
            this.picture = new System.Windows.Forms.PictureBox();
            this.linkRestablecer = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(89, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bienvenid@";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(269, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bienvenido de vuelta, por favor inicia sesión";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDocumento
            // 
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumento.Location = new System.Drawing.Point(55, 152);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(85, 16);
            this.lblDocumento.TabIndex = 2;
            this.lblDocumento.Text = "Documento";
            // 
            // lblContraseña
            // 
            this.lblContraseña.AutoSize = true;
            this.lblContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContraseña.Location = new System.Drawing.Point(55, 227);
            this.lblContraseña.Name = "lblContraseña";
            this.lblContraseña.Size = new System.Drawing.Size(86, 16);
            this.lblContraseña.TabIndex = 3;
            this.lblContraseña.Text = "Contraseña";
            // 
            // lblRol
            // 
            this.lblRol.AutoSize = true;
            this.lblRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRol.Location = new System.Drawing.Point(385, 152);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(31, 16);
            this.lblRol.TabIndex = 4;
            this.lblRol.Text = "Rol";
            // 
            // cbRol
            // 
            this.cbRol.FormattingEnabled = true;
            this.cbRol.Items.AddRange(new object[] {
            "Paciente",
            "Medico",
            "Recepcionista"});
            this.cbRol.Location = new System.Drawing.Point(388, 180);
            this.cbRol.Name = "cbRol";
            this.cbRol.Size = new System.Drawing.Size(188, 21);
            this.cbRol.TabIndex = 5;
            // 
            // txtbDocumento
            // 
            this.txtbDocumento.Location = new System.Drawing.Point(58, 181);
            this.txtbDocumento.Name = "txtbDocumento";
            this.txtbDocumento.Size = new System.Drawing.Size(188, 20);
            this.txtbDocumento.TabIndex = 6;
            // 
            // txtbContraseña
            // 
            this.txtbContraseña.Location = new System.Drawing.Point(58, 260);
            this.txtbContraseña.Name = "txtbContraseña";
            this.txtbContraseña.Size = new System.Drawing.Size(188, 20);
            this.txtbContraseña.TabIndex = 7;
            // 
            // btnIngresar
            // 
            this.btnIngresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.btnIngresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIngresar.ForeColor = System.Drawing.Color.White;
            this.btnIngresar.Location = new System.Drawing.Point(58, 345);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(189, 41);
            this.btnIngresar.TabIndex = 8;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = false;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // lklRegistrarse
            // 
            this.lklRegistrarse.AutoSize = true;
            this.lklRegistrarse.Location = new System.Drawing.Point(55, 399);
            this.lklRegistrarse.Name = "lklRegistrarse";
            this.lklRegistrarse.Size = new System.Drawing.Size(204, 13);
            this.lklRegistrarse.TabIndex = 9;
            this.lklRegistrarse.TabStop = true;
            this.lklRegistrarse.Text = "¿No tienes una cuenta? ¡Registrate Aquí!";
            this.lklRegistrarse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklRegistrarse_LinkClicked);
            // 
            // picture
            // 
            this.picture.Image = global::FormuSalud.Properties.Resources.logo_soloblanco_Photoroom;
            this.picture.Location = new System.Drawing.Point(560, 227);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(228, 211);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture.TabIndex = 10;
            this.picture.TabStop = false;
            // 
            // linkRestablecer
            // 
            this.linkRestablecer.AutoSize = true;
            this.linkRestablecer.Location = new System.Drawing.Point(81, 293);
            this.linkRestablecer.Name = "linkRestablecer";
            this.linkRestablecer.Size = new System.Drawing.Size(132, 13);
            this.linkRestablecer.TabIndex = 11;
            this.linkRestablecer.TabStop = true;
            this.linkRestablecer.Text = "¿Olvidaste tu Contraseña?";
            this.linkRestablecer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRestablecer_LinkClicked);
            // 
            // IniciodeSesion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(221)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.linkRestablecer);
            this.Controls.Add(this.picture);
            this.Controls.Add(this.lklRegistrarse);
            this.Controls.Add(this.btnIngresar);
            this.Controls.Add(this.txtbContraseña);
            this.Controls.Add(this.txtbDocumento);
            this.Controls.Add(this.cbRol);
            this.Controls.Add(this.lblRol);
            this.Controls.Add(this.lblContraseña);
            this.Controls.Add(this.lblDocumento);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IniciodeSesion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio de Sesion";
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.Label lblContraseña;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.ComboBox cbRol;
        private System.Windows.Forms.TextBox txtbDocumento;
        private System.Windows.Forms.TextBox txtbContraseña;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.LinkLabel lklRegistrarse;
        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.LinkLabel linkRestablecer;
    }
}

