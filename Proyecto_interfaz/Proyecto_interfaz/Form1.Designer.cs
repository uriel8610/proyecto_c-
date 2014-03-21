namespace Proyecto_interfaz
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Ingresar = new System.Windows.Forms.Button();
            this.User = new System.Windows.Forms.Label();
            this.Usuario = new System.Windows.Forms.TextBox();
            this.Pswd = new System.Windows.Forms.Label();
            this.Contraseña = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(195, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 86);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Ingresar
            // 
            this.Ingresar.Location = new System.Drawing.Point(195, 129);
            this.Ingresar.Name = "Ingresar";
            this.Ingresar.Size = new System.Drawing.Size(90, 25);
            this.Ingresar.TabIndex = 1;
            this.Ingresar.Text = "Ingresar";
            this.Ingresar.UseVisualStyleBackColor = true;
            this.Ingresar.Click += new System.EventHandler(this.Ingresar_Click);
            // 
            // User
            // 
            this.User.AutoSize = true;
            this.User.Location = new System.Drawing.Point(11, 12);
            this.User.Name = "User";
            this.User.Size = new System.Drawing.Size(43, 13);
            this.User.TabIndex = 2;
            this.User.Text = "Usuario";
            // 
            // Usuario
            // 
            this.Usuario.Location = new System.Drawing.Point(10, 28);
            this.Usuario.Name = "Usuario";
            this.Usuario.Size = new System.Drawing.Size(167, 20);
            this.Usuario.TabIndex = 3;
            // 
            // Pswd
            // 
            this.Pswd.AutoSize = true;
            this.Pswd.Location = new System.Drawing.Point(8, 68);
            this.Pswd.Name = "Pswd";
            this.Pswd.Size = new System.Drawing.Size(61, 13);
            this.Pswd.TabIndex = 4;
            this.Pswd.Text = "Contraseña";
            // 
            // Contraseña
            // 
            this.Contraseña.Location = new System.Drawing.Point(11, 84);
            this.Contraseña.Name = "Contraseña";
            this.Contraseña.Size = new System.Drawing.Size(166, 20);
            this.Contraseña.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 165);
            this.Controls.Add(this.Contraseña);
            this.Controls.Add(this.Pswd);
            this.Controls.Add(this.Usuario);
            this.Controls.Add(this.User);
            this.Controls.Add(this.Ingresar);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Iniciar Sesión";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Ingresar;
        private System.Windows.Forms.Label User;
        private System.Windows.Forms.TextBox Usuario;
        private System.Windows.Forms.Label Pswd;
        private System.Windows.Forms.MaskedTextBox Contraseña;
    }
}

