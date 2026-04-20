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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.frm_lbl_nombre = new System.Windows.Forms.Label();
            this.frm_lbl_apellido = new System.Windows.Forms.Label();
            this.frm_lbl_email = new System.Windows.Forms.Label();
            this.btnCrearUsuario = new System.Windows.Forms.Button();
            this.frm_lbl_dni = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(130, 159);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(130, 198);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(130, 245);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 2;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(133, 289);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 3;
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
            this.btnCrearUsuario.Location = new System.Drawing.Point(196, 389);
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
            this.frm_lbl_dni.Size = new System.Drawing.Size(35, 13);
            this.frm_lbl_dni.TabIndex = 9;
            this.frm_lbl_dni.Text = "label4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(130, 323);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "label5";
            // 
            // FrmCrearUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.frm_lbl_dni);
            this.Controls.Add(this.btnCrearUsuario);
            this.Controls.Add(this.frm_lbl_email);
            this.Controls.Add(this.frm_lbl_apellido);
            this.Controls.Add(this.frm_lbl_nombre);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "FrmCrearUsuario";
            this.Text = "FrmCrearUsuario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label frm_lbl_nombre;
        private System.Windows.Forms.Label frm_lbl_apellido;
        private System.Windows.Forms.Label frm_lbl_email;
        private System.Windows.Forms.Button btnCrearUsuario;
        private System.Windows.Forms.Label frm_lbl_dni;
        private System.Windows.Forms.Label label5;
    }
}