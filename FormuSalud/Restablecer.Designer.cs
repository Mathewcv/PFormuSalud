namespace FormuSalud
{
    partial class Restablecer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picture = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOlvidaste = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.txtbContraseña = new System.Windows.Forms.TextBox();
            this.txtbDocumento = new System.Windows.Forms.TextBox();
            this.lblContraseña = new System.Windows.Forms.Label();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // picture
            // 
            this.picture.Image = global::FormuSalud.Properties.Resources.logo_soloblanco_Photoroom;
            this.picture.Location = new System.Drawing.Point(560, 227);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(228, 211);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture.TabIndex = 11;
            this.picture.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(41, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(417, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Si olvidaste tu contraseña comprueba tu identidad para restablecerla\r\n";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOlvidaste
            // 
            this.lblOlvidaste.AutoSize = true;
            this.lblOlvidaste.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOlvidaste.Location = new System.Drawing.Point(78, 34);
            this.lblOlvidaste.Name = "lblOlvidaste";
            this.lblOlvidaste.Size = new System.Drawing.Size(361, 31);
            this.lblOlvidaste.TabIndex = 12;
            this.lblOlvidaste.Text = "¿Olvidaste tú Contraseña?";
            this.lblOlvidaste.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Location = new System.Drawing.Point(53, 327);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(188, 41);
            this.btnConfirmar.TabIndex = 18;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // txtbContraseña
            // 
            this.txtbContraseña.Location = new System.Drawing.Point(53, 280);
            this.txtbContraseña.Name = "txtbContraseña";
            this.txtbContraseña.Size = new System.Drawing.Size(188, 20);
            this.txtbContraseña.TabIndex = 17;
            // 
            // txtbDocumento
            // 
            this.txtbDocumento.Location = new System.Drawing.Point(53, 201);
            this.txtbDocumento.Name = "txtbDocumento";
            this.txtbDocumento.Size = new System.Drawing.Size(188, 20);
            this.txtbDocumento.TabIndex = 16;
            // 
            // lblContraseña
            // 
            this.lblContraseña.AutoSize = true;
            this.lblContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContraseña.Location = new System.Drawing.Point(50, 247);
            this.lblContraseña.Name = "lblContraseña";
            this.lblContraseña.Size = new System.Drawing.Size(135, 16);
            this.lblContraseña.TabIndex = 15;
            this.lblContraseña.Text = "Nueva Contraseña";
            // 
            // lblDocumento
            // 
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumento.Location = new System.Drawing.Point(50, 172);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(85, 16);
            this.lblDocumento.TabIndex = 14;
            this.lblDocumento.Text = "Documento";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(93, 385);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(107, 41);
            this.btnCancelar.TabIndex = 19;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // Restablecer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(221)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtbContraseña);
            this.Controls.Add(this.txtbDocumento);
            this.Controls.Add(this.lblContraseña);
            this.Controls.Add(this.lblDocumento);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblOlvidaste);
            this.Controls.Add(this.picture);
            this.Name = "Restablecer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reestablecer";
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOlvidaste;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.TextBox txtbContraseña;
        private System.Windows.Forms.TextBox txtbDocumento;
        private System.Windows.Forms.Label lblContraseña;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.Button btnCancelar;
    }
}