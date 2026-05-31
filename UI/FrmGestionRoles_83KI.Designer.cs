namespace UI
{
    partial class FrmGestionRoles_83KI
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblRoles = new System.Windows.Forms.Label();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.lblFamiliasRol = new System.Windows.Forms.Label();
            this.lstFamiliasRol = new System.Windows.Forms.ListBox();
            this.lblPatentesFamilia = new System.Windows.Forms.Label();
            this.treeContenidoFamilia = new System.Windows.Forms.TreeView();
            this.cmbFamiliasDisponibles = new System.Windows.Forms.ComboBox();
            this.btnAgregarFamilia = new System.Windows.Forms.Button();
            this.btnQuitarFamilia = new System.Windows.Forms.Button();
            this.txtNombreRol = new System.Windows.Forms.TextBox();
            this.btnCrearRol = new System.Windows.Forms.Button();
            this.cmbPatentesDisponibles = new System.Windows.Forms.ComboBox();
            this.btnAsignarPatente = new System.Windows.Forms.Button();
            this.lblPatentesRol = new System.Windows.Forms.Label();
            this.lstPatentesRol = new System.Windows.Forms.ListBox();
            this.btnQuitarPatente = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblRoles
            // 
            this.lblRoles.AutoSize = true;
            this.lblRoles.Location = new System.Drawing.Point(24, 24);
            this.lblRoles.Name = "lblRoles";
            this.lblRoles.Size = new System.Drawing.Size(48, 21);
            this.lblRoles.TabIndex = 0;
            this.lblRoles.Text = "Roles";
            // 
            // lstRoles
            // 
            this.lstRoles.FormattingEnabled = true;
            this.lstRoles.ItemHeight = 21;
            this.lstRoles.Location = new System.Drawing.Point(28, 52);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(220, 298);
            this.lstRoles.TabIndex = 1;
            this.lstRoles.SelectedIndexChanged += new System.EventHandler(this.lstRoles_SelectedIndexChanged);
            // 
            // lblFamiliasRol
            // 
            this.lblFamiliasRol.AutoSize = true;
            this.lblFamiliasRol.Location = new System.Drawing.Point(286, 24);
            this.lblFamiliasRol.Name = "lblFamiliasRol";
            this.lblFamiliasRol.Size = new System.Drawing.Size(123, 21);
            this.lblFamiliasRol.TabIndex = 2;
            this.lblFamiliasRol.Text = "Familias del rol";
            // 
            // lstFamiliasRol
            // 
            this.lstFamiliasRol.FormattingEnabled = true;
            this.lstFamiliasRol.ItemHeight = 21;
            this.lstFamiliasRol.Location = new System.Drawing.Point(290, 52);
            this.lstFamiliasRol.Name = "lstFamiliasRol";
            this.lstFamiliasRol.Size = new System.Drawing.Size(240, 298);
            this.lstFamiliasRol.TabIndex = 3;
            this.lstFamiliasRol.SelectedIndexChanged += new System.EventHandler(this.lstFamiliasRol_SelectedIndexChanged);
            // 
            // lblPatentesFamilia
            // 
            this.lblPatentesFamilia.AutoSize = true;
            this.lblPatentesFamilia.Location = new System.Drawing.Point(568, 24);
            this.lblPatentesFamilia.Name = "lblPatentesFamilia";
            this.lblPatentesFamilia.Size = new System.Drawing.Size(170, 21);
            this.lblPatentesFamilia.TabIndex = 4;
            this.lblPatentesFamilia.Text = "Contenido de la familia";
            // 
            // treeContenidoFamilia
            // 
            this.treeContenidoFamilia.Location = new System.Drawing.Point(572, 52);
            this.treeContenidoFamilia.Name = "treeContenidoFamilia";
            this.treeContenidoFamilia.Size = new System.Drawing.Size(240, 298);
            this.treeContenidoFamilia.TabIndex = 5;
            // 
            // cmbFamiliasDisponibles
            // 
            this.cmbFamiliasDisponibles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFamiliasDisponibles.FormattingEnabled = true;
            this.cmbFamiliasDisponibles.Location = new System.Drawing.Point(290, 366);
            this.cmbFamiliasDisponibles.Name = "cmbFamiliasDisponibles";
            this.cmbFamiliasDisponibles.Size = new System.Drawing.Size(240, 29);
            this.cmbFamiliasDisponibles.TabIndex = 6;
            // 
            // btnAgregarFamilia
            // 
            this.btnAgregarFamilia.Location = new System.Drawing.Point(290, 410);
            this.btnAgregarFamilia.Name = "btnAgregarFamilia";
            this.btnAgregarFamilia.Size = new System.Drawing.Size(115, 34);
            this.btnAgregarFamilia.TabIndex = 7;
            this.btnAgregarFamilia.Text = "Agregar";
            this.btnAgregarFamilia.UseVisualStyleBackColor = true;
            this.btnAgregarFamilia.Click += new System.EventHandler(this.btnAgregarFamilia_Click);
            // 
            // btnQuitarFamilia
            // 
            this.btnQuitarFamilia.Location = new System.Drawing.Point(415, 410);
            this.btnQuitarFamilia.Name = "btnQuitarFamilia";
            this.btnQuitarFamilia.Size = new System.Drawing.Size(115, 34);
            this.btnQuitarFamilia.TabIndex = 8;
            this.btnQuitarFamilia.Text = "Quitar";
            this.btnQuitarFamilia.UseVisualStyleBackColor = true;
            this.btnQuitarFamilia.Click += new System.EventHandler(this.btnQuitarFamilia_Click);
            // 
            // txtNombreRol
            // 
            this.txtNombreRol.Location = new System.Drawing.Point(28, 366);
            this.txtNombreRol.Name = "txtNombreRol";
            this.txtNombreRol.Size = new System.Drawing.Size(220, 29);
            this.txtNombreRol.TabIndex = 9;
            // 
            // btnCrearRol
            // 
            this.btnCrearRol.Location = new System.Drawing.Point(28, 410);
            this.btnCrearRol.Name = "btnCrearRol";
            this.btnCrearRol.Size = new System.Drawing.Size(220, 34);
            this.btnCrearRol.TabIndex = 10;
            this.btnCrearRol.Text = "Crear rol";
            this.btnCrearRol.UseVisualStyleBackColor = true;
            this.btnCrearRol.Click += new System.EventHandler(this.btnCrearRol_Click);
            // 
            // cmbPatentesDisponibles
            // 
            this.cmbPatentesDisponibles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatentesDisponibles.FormattingEnabled = true;
            this.cmbPatentesDisponibles.Location = new System.Drawing.Point(834, 366);
            this.cmbPatentesDisponibles.Name = "cmbPatentesDisponibles";
            this.cmbPatentesDisponibles.Size = new System.Drawing.Size(240, 29);
            this.cmbPatentesDisponibles.TabIndex = 11;
            // 
            // btnAsignarPatente
            // 
            this.btnAsignarPatente.Location = new System.Drawing.Point(834, 410);
            this.btnAsignarPatente.Name = "btnAsignarPatente";
            this.btnAsignarPatente.Size = new System.Drawing.Size(115, 34);
            this.btnAsignarPatente.TabIndex = 12;
            this.btnAsignarPatente.Text = "Asignar";
            this.btnAsignarPatente.UseVisualStyleBackColor = true;
            this.btnAsignarPatente.Click += new System.EventHandler(this.btnAsignarPatente_Click);
            // 
            // lblPatentesRol
            // 
            this.lblPatentesRol.AutoSize = true;
            this.lblPatentesRol.Location = new System.Drawing.Point(830, 24);
            this.lblPatentesRol.Name = "lblPatentesRol";
            this.lblPatentesRol.Size = new System.Drawing.Size(119, 21);
            this.lblPatentesRol.TabIndex = 13;
            this.lblPatentesRol.Text = "Patentes del rol";
            // 
            // lstPatentesRol
            // 
            this.lstPatentesRol.FormattingEnabled = true;
            this.lstPatentesRol.ItemHeight = 21;
            this.lstPatentesRol.Location = new System.Drawing.Point(834, 52);
            this.lstPatentesRol.Name = "lstPatentesRol";
            this.lstPatentesRol.Size = new System.Drawing.Size(240, 298);
            this.lstPatentesRol.TabIndex = 14;
            // 
            // btnQuitarPatente
            // 
            this.btnQuitarPatente.Location = new System.Drawing.Point(959, 410);
            this.btnQuitarPatente.Name = "btnQuitarPatente";
            this.btnQuitarPatente.Size = new System.Drawing.Size(115, 34);
            this.btnQuitarPatente.TabIndex = 15;
            this.btnQuitarPatente.Text = "Quitar";
            this.btnQuitarPatente.UseVisualStyleBackColor = true;
            this.btnQuitarPatente.Click += new System.EventHandler(this.btnQuitarPatente_Click);
            // 
            // FrmGestionRoles_83KI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(1106, 472);
            this.Controls.Add(this.btnQuitarPatente);
            this.Controls.Add(this.lstPatentesRol);
            this.Controls.Add(this.lblPatentesRol);
            this.Controls.Add(this.btnAsignarPatente);
            this.Controls.Add(this.cmbPatentesDisponibles);
            this.Controls.Add(this.btnCrearRol);
            this.Controls.Add(this.txtNombreRol);
            this.Controls.Add(this.btnQuitarFamilia);
            this.Controls.Add(this.btnAgregarFamilia);
            this.Controls.Add(this.cmbFamiliasDisponibles);
            this.Controls.Add(this.treeContenidoFamilia);
            this.Controls.Add(this.lblPatentesFamilia);
            this.Controls.Add(this.lstFamiliasRol);
            this.Controls.Add(this.lblFamiliasRol);
            this.Controls.Add(this.lstRoles);
            this.Controls.Add(this.lblRoles);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Name = "FrmGestionRoles_83KI";
            this.Text = "Gestion de roles";
            this.Load += new System.EventHandler(this.FrmGestionRoles_83KI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblRoles;
        private System.Windows.Forms.ListBox lstRoles;
        private System.Windows.Forms.Label lblFamiliasRol;
        private System.Windows.Forms.ListBox lstFamiliasRol;
        private System.Windows.Forms.Label lblPatentesFamilia;
        private System.Windows.Forms.TreeView treeContenidoFamilia;
        private System.Windows.Forms.ComboBox cmbFamiliasDisponibles;
        private System.Windows.Forms.Button btnAgregarFamilia;
        private System.Windows.Forms.Button btnQuitarFamilia;
        private System.Windows.Forms.TextBox txtNombreRol;
        private System.Windows.Forms.Button btnCrearRol;
        private System.Windows.Forms.ComboBox cmbPatentesDisponibles;
        private System.Windows.Forms.Button btnAsignarPatente;
        private System.Windows.Forms.Label lblPatentesRol;
        private System.Windows.Forms.ListBox lstPatentesRol;
        private System.Windows.Forms.Button btnQuitarPatente;
    }
}
