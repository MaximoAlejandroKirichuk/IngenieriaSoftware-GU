namespace UI
{
    partial class FrmCrearUsuario
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
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txt_Dni = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.frm_lbl_nombre = new System.Windows.Forms.Label();
            this.frm_lbl_apellido = new System.Windows.Forms.Label();
            this.frm_lbl_email = new System.Windows.Forms.Label();
            this.btnCrearUsuario = new System.Windows.Forms.Button();
            this.frm_lbl_dni = new System.Windows.Forms.Label();
            this.frm_lbl_rol = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(130, 159);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(100, 20);
            this.txtNombre.TabIndex = 0;
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(130, 198);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(100, 20);
            this.txtApellido.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(130, 245);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 20);
            this.txtEmail.TabIndex = 2;
            // 
            // txt_Dni
            // 
            this.txt_Dni.Location = new System.Drawing.Point(133, 289);
            this.txt_Dni.Name = "txt_Dni";
            this.txt_Dni.Size = new System.Drawing.Size(100, 20);
            this.txt_Dni.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(130, 339);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // frm_lbl_nombre
            // 
            this.frm_lbl_nombre.AutoSize = true;
            this.frm_lbl_nombre.Location = new System.Drawing.Point(130, 140);
            this.frm_lbl_nombre.Name = "frm_lbl_nombre";
            this.frm_lbl_nombre.Size = new System.Drawing.Size(44, 13);
            this.frm_lbl_nombre.TabIndex = 5;
            this.frm_lbl_nombre.Text = "Nombre";
            // 
            // frm_lbl_apellido
            // 
            this.frm_lbl_apellido.AutoSize = true;
            this.frm_lbl_apellido.Location = new System.Drawing.Point(130, 182);
            this.frm_lbl_apellido.Name = "frm_lbl_apellido";
            this.frm_lbl_apellido.Size = new System.Drawing.Size(44, 13);
            this.frm_lbl_apellido.TabIndex = 6;
            this.frm_lbl_apellido.Text = "Apellido";
            // 
            // frm_lbl_email
            // 
            this.frm_lbl_email.AutoSize = true;
            this.frm_lbl_email.Location = new System.Drawing.Point(127, 229);
            this.frm_lbl_email.Name = "frm_lbl_email";
            this.frm_lbl_email.Size = new System.Drawing.Size(32, 13);
            this.frm_lbl_email.TabIndex = 7;
            this.frm_lbl_email.Text = "Email";
            // 
            // btnCrearUsuario
            // 
            this.btnCrearUsuario.Location = new System.Drawing.Point(177, 383);
            this.btnCrearUsuario.Name = "btnCrearUsuario";
            this.btnCrearUsuario.Size = new System.Drawing.Size(96, 23);
            this.btnCrearUsuario.TabIndex = 8;
            this.btnCrearUsuario.Text = "Crear Usuario";
            this.btnCrearUsuario.UseVisualStyleBackColor = true;
            this.btnCrearUsuario.Click += new System.EventHandler(this.btnCrearUsuario_Click);
            // 
            // frm_lbl_dni
            // 
            this.frm_lbl_dni.AutoSize = true;
            this.frm_lbl_dni.Location = new System.Drawing.Point(130, 273);
            this.frm_lbl_dni.Name = "frm_lbl_dni";
            this.frm_lbl_dni.Size = new System.Drawing.Size(23, 13);
            this.frm_lbl_dni.TabIndex = 9;
            this.frm_lbl_dni.Text = "Dni";
            // 
            // frm_lbl_rol
            // 
            this.frm_lbl_rol.AutoSize = true;
            this.frm_lbl_rol.Location = new System.Drawing.Point(130, 323);
            this.frm_lbl_rol.Name = "frm_lbl_rol";
            this.frm_lbl_rol.Size = new System.Drawing.Size(23, 13);
            this.frm_lbl_rol.TabIndex = 10;
            this.frm_lbl_rol.Text = "Rol";
            // 
            // FrmCrearUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.frm_lbl_rol);
            this.Controls.Add(this.frm_lbl_dni);
            this.Controls.Add(this.btnCrearUsuario);
            this.Controls.Add(this.frm_lbl_email);
            this.Controls.Add(this.frm_lbl_apellido);
            this.Controls.Add(this.frm_lbl_nombre);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txt_Dni);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.txtNombre);
            this.Name = "FrmCrearUsuario";
            this.Text = "FrmCrearUsuario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txt_Dni;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label frm_lbl_nombre;
        private System.Windows.Forms.Label frm_lbl_apellido;
        private System.Windows.Forms.Label frm_lbl_email;
        private System.Windows.Forms.Button btnCrearUsuario;
        private System.Windows.Forms.Label frm_lbl_dni;
        private System.Windows.Forms.Label frm_lbl_rol;
    }
}