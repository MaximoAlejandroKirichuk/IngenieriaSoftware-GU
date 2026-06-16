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
            this.btnEliminarRol = new System.Windows.Forms.Button();
            this.lblTipoComponente = new System.Windows.Forms.Label();
            this.rbtnTipoFamilia = new System.Windows.Forms.RadioButton();
            this.rbtnTipoPatente = new System.Windows.Forms.RadioButton();
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
            this.cmbFamiliasDisponibles.SelectedIndexChanged += new System.EventHandler(this.cmbFamiliasDisponibles_SelectedIndexChanged);
            // 
            // btnAgregarFamilia
            // 
            this.btnAgregarFamilia.Location = new System.Drawing.Point(290, 460);
            this.btnAgregarFamilia.Name = "btnAgregarFamilia";
            this.btnAgregarFamilia.Size = new System.Drawing.Size(115, 34);
            this.btnAgregarFamilia.TabIndex = 7;
            this.btnAgregarFamilia.Text = "Agregar";
            this.btnAgregarFamilia.UseVisualStyleBackColor = true;
            this.btnAgregarFamilia.Click += new System.EventHandler(this.btnAgregarFamilia_Click);
            // 
            // btnQuitarFamilia
            // 
            this.btnQuitarFamilia.Location = new System.Drawing.Point(415, 460);
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
            // lblTipoComponente
            // 
            this.lblTipoComponente.AutoSize = true;
            this.lblTipoComponente.Location = new System.Drawing.Point(28, 404);
            this.lblTipoComponente.Name = "lblTipoComponente";
            this.lblTipoComponente.Size = new System.Drawing.Size(170, 21);
            this.lblTipoComponente.TabIndex = 17;
            this.lblTipoComponente.Text = "Tipo de componente";
            // 
            // rbtnTipoFamilia
            // 
            this.rbtnTipoFamilia.AutoSize = true;
            this.rbtnTipoFamilia.Checked = true;
            this.rbtnTipoFamilia.Location = new System.Drawing.Point(28, 428);
            this.rbtnTipoFamilia.Name = "rbtnTipoFamilia";
            this.rbtnTipoFamilia.Size = new System.Drawing.Size(80, 25);
            this.rbtnTipoFamilia.TabIndex = 18;
            this.rbtnTipoFamilia.TabStop = true;
            this.rbtnTipoFamilia.Text = "Familia";
            this.rbtnTipoFamilia.UseVisualStyleBackColor = true;
            this.rbtnTipoFamilia.CheckedChanged += new System.EventHandler(this.rbtnTipoFamilia_CheckedChanged);
            // 
            // rbtnTipoPatente
            // 
            this.rbtnTipoPatente.AutoSize = true;
            this.rbtnTipoPatente.Location = new System.Drawing.Point(130, 428);
            this.rbtnTipoPatente.Name = "rbtnTipoPatente";
            this.rbtnTipoPatente.Size = new System.Drawing.Size(75, 25);
            this.rbtnTipoPatente.TabIndex = 19;
            this.rbtnTipoPatente.Text = "Patente";
            this.rbtnTipoPatente.UseVisualStyleBackColor = true;
            this.rbtnTipoPatente.CheckedChanged += new System.EventHandler(this.rbtnTipoPatente_CheckedChanged);
            // 
            // btnCrearRol
            // 
            this.btnCrearRol.Location = new System.Drawing.Point(28, 460);
            this.btnCrearRol.Name = "btnCrearRol";
            this.btnCrearRol.Size = new System.Drawing.Size(115, 34);
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
            this.cmbPatentesDisponibles.SelectedIndexChanged += new System.EventHandler(this.cmbPatentesDisponibles_SelectedIndexChanged);
            // 
            // btnAsignarPatente
            // 
            this.btnAsignarPatente.Location = new System.Drawing.Point(834, 460);
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
            this.btnQuitarPatente.Location = new System.Drawing.Point(959, 460);
            this.btnQuitarPatente.Name = "btnQuitarPatente";
            this.btnQuitarPatente.Size = new System.Drawing.Size(115, 34);
            this.btnQuitarPatente.TabIndex = 15;
            this.btnQuitarPatente.Text = "Quitar";
            this.btnQuitarPatente.UseVisualStyleBackColor = true;
            this.btnQuitarPatente.Click += new System.EventHandler(this.btnQuitarPatente_Click);
            // 
            // btnEliminarRol
            // 
            this.btnEliminarRol.Location = new System.Drawing.Point(155, 460);
            this.btnEliminarRol.Name = "btnEliminarRol";
            this.btnEliminarRol.Size = new System.Drawing.Size(115, 34);
            this.btnEliminarRol.TabIndex = 16;
            this.btnEliminarRol.Text = "Eliminar rol";
            this.btnEliminarRol.UseVisualStyleBackColor = true;
            this.btnEliminarRol.Click += new System.EventHandler(this.btnEliminarRol_Click);
            // 
            // FrmGestionRoles_83KI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(1106, 520);
            this.Controls.Add(this.btnQuitarPatente);
            this.Controls.Add(this.btnEliminarRol);
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
            this.Controls.Add(this.rbtnTipoPatente);
            this.Controls.Add(this.rbtnTipoFamilia);
            this.Controls.Add(this.lblTipoComponente);
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
        private System.Windows.Forms.Button btnEliminarRol;
        private System.Windows.Forms.Label lblTipoComponente;
        private System.Windows.Forms.RadioButton rbtnTipoFamilia;
        private System.Windows.Forms.RadioButton rbtnTipoPatente;
    }
}
