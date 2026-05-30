namespace UI
{
    partial class FrmGestionFamilias_83KI
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
            this.rdbSeleccionarFamilia = new System.Windows.Forms.RadioButton();
            this.rdbCrearFamilia = new System.Windows.Forms.RadioButton();
            this.lblFamilia = new System.Windows.Forms.Label();
            this.cmbFamilias = new System.Windows.Forms.ComboBox();
            this.txtNombreFamilia = new System.Windows.Forms.TextBox();
            this.btnCrearFamilia = new System.Windows.Forms.Button();
            this.btnEliminarFamilia = new System.Windows.Forms.Button();
            this.treeFamilia = new System.Windows.Forms.TreeView();
            this.btnQuitarSeleccion = new System.Windows.Forms.Button();
            this.lblPatentes = new System.Windows.Forms.Label();
            this.lstPatentesDisponibles = new System.Windows.Forms.ListBox();
            this.btnAgregarPatente = new System.Windows.Forms.Button();
            this.lblFamiliasDisponibles = new System.Windows.Forms.Label();
            this.cmbFamiliasDisponibles = new System.Windows.Forms.ComboBox();
            this.btnAgregarFamilia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rdbSeleccionarFamilia
            // 
            this.rdbSeleccionarFamilia.AutoSize = true;
            this.rdbSeleccionarFamilia.Location = new System.Drawing.Point(28, 24);
            this.rdbSeleccionarFamilia.Name = "rdbSeleccionarFamilia";
            this.rdbSeleccionarFamilia.Size = new System.Drawing.Size(215, 25);
            this.rdbSeleccionarFamilia.TabIndex = 0;
            this.rdbSeleccionarFamilia.TabStop = true;
            this.rdbSeleccionarFamilia.Text = "Seleccionar familia existente";
            this.rdbSeleccionarFamilia.UseVisualStyleBackColor = true;
            this.rdbSeleccionarFamilia.CheckedChanged += new System.EventHandler(this.rdbSeleccionarFamilia_CheckedChanged);
            // 
            // rdbCrearFamilia
            // 
            this.rdbCrearFamilia.AutoSize = true;
            this.rdbCrearFamilia.Location = new System.Drawing.Point(28, 54);
            this.rdbCrearFamilia.Name = "rdbCrearFamilia";
            this.rdbCrearFamilia.Size = new System.Drawing.Size(118, 25);
            this.rdbCrearFamilia.TabIndex = 1;
            this.rdbCrearFamilia.TabStop = true;
            this.rdbCrearFamilia.Text = "Crear familia";
            this.rdbCrearFamilia.UseVisualStyleBackColor = true;
            this.rdbCrearFamilia.CheckedChanged += new System.EventHandler(this.rdbSeleccionarFamilia_CheckedChanged);
            // 
            // lblFamilia
            // 
            this.lblFamilia.AutoSize = true;
            this.lblFamilia.Location = new System.Drawing.Point(24, 92);
            this.lblFamilia.Name = "lblFamilia";
            this.lblFamilia.Size = new System.Drawing.Size(62, 21);
            this.lblFamilia.TabIndex = 2;
            this.lblFamilia.Text = "Familia";
            // 
            // cmbFamilias
            // 
            this.cmbFamilias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFamilias.FormattingEnabled = true;
            this.cmbFamilias.Location = new System.Drawing.Point(28, 118);
            this.cmbFamilias.Name = "cmbFamilias";
            this.cmbFamilias.Size = new System.Drawing.Size(318, 29);
            this.cmbFamilias.TabIndex = 3;
            this.cmbFamilias.SelectedIndexChanged += new System.EventHandler(this.cmbFamilias_SelectedIndexChanged);
            // 
            // txtNombreFamilia
            // 
            this.txtNombreFamilia.Location = new System.Drawing.Point(28, 118);
            this.txtNombreFamilia.Name = "txtNombreFamilia";
            this.txtNombreFamilia.Size = new System.Drawing.Size(318, 29);
            this.txtNombreFamilia.TabIndex = 4;
            // 
            // btnCrearFamilia
            // 
            this.btnCrearFamilia.Location = new System.Drawing.Point(28, 160);
            this.btnCrearFamilia.Name = "btnCrearFamilia";
            this.btnCrearFamilia.Size = new System.Drawing.Size(318, 34);
            this.btnCrearFamilia.TabIndex = 5;
            this.btnCrearFamilia.Text = "Crear familia";
            this.btnCrearFamilia.UseVisualStyleBackColor = true;
            this.btnCrearFamilia.Click += new System.EventHandler(this.btnCrearFamilia_Click);
            // 
            // btnEliminarFamilia
            // 
            this.btnEliminarFamilia.Location = new System.Drawing.Point(28, 160);
            this.btnEliminarFamilia.Name = "btnEliminarFamilia";
            this.btnEliminarFamilia.Size = new System.Drawing.Size(318, 34);
            this.btnEliminarFamilia.TabIndex = 6;
            this.btnEliminarFamilia.Text = "Eliminar familia";
            this.btnEliminarFamilia.UseVisualStyleBackColor = true;
            this.btnEliminarFamilia.Click += new System.EventHandler(this.btnEliminarFamilia_Click);
            // 
            // treeFamilia
            // 
            this.treeFamilia.Location = new System.Drawing.Point(28, 214);
            this.treeFamilia.Name = "treeFamilia";
            this.treeFamilia.Size = new System.Drawing.Size(318, 250);
            this.treeFamilia.TabIndex = 7;
            this.treeFamilia.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeFamilia_AfterSelect);
            // 
            // btnQuitarSeleccion
            // 
            this.btnQuitarSeleccion.Location = new System.Drawing.Point(28, 478);
            this.btnQuitarSeleccion.Name = "btnQuitarSeleccion";
            this.btnQuitarSeleccion.Size = new System.Drawing.Size(318, 34);
            this.btnQuitarSeleccion.TabIndex = 8;
            this.btnQuitarSeleccion.Text = "Seleccionar permiso o subfamilia";
            this.btnQuitarSeleccion.UseVisualStyleBackColor = true;
            this.btnQuitarSeleccion.Click += new System.EventHandler(this.btnQuitarSeleccion_Click);
            // 
            // lblPatentes
            // 
            this.lblPatentes.AutoSize = true;
            this.lblPatentes.Location = new System.Drawing.Point(404, 24);
            this.lblPatentes.Name = "lblPatentes";
            this.lblPatentes.Size = new System.Drawing.Size(150, 21);
            this.lblPatentes.TabIndex = 7;
            this.lblPatentes.Text = "Patentes disponibles";
            // 
            // lstPatentesDisponibles
            // 
            this.lstPatentesDisponibles.FormattingEnabled = true;
            this.lstPatentesDisponibles.ItemHeight = 21;
            this.lstPatentesDisponibles.Location = new System.Drawing.Point(408, 50);
            this.lstPatentesDisponibles.Name = "lstPatentesDisponibles";
            this.lstPatentesDisponibles.Size = new System.Drawing.Size(318, 214);
            this.lstPatentesDisponibles.TabIndex = 8;
            // 
            // btnAgregarPatente
            // 
            this.btnAgregarPatente.Location = new System.Drawing.Point(408, 278);
            this.btnAgregarPatente.Name = "btnAgregarPatente";
            this.btnAgregarPatente.Size = new System.Drawing.Size(318, 34);
            this.btnAgregarPatente.TabIndex = 9;
            this.btnAgregarPatente.Text = "Agregar patente a familia";
            this.btnAgregarPatente.UseVisualStyleBackColor = true;
            this.btnAgregarPatente.Click += new System.EventHandler(this.btnAgregarPatente_Click);
            // 
            // lblFamiliasDisponibles
            // 
            this.lblFamiliasDisponibles.AutoSize = true;
            this.lblFamiliasDisponibles.Location = new System.Drawing.Point(404, 350);
            this.lblFamiliasDisponibles.Name = "lblFamiliasDisponibles";
            this.lblFamiliasDisponibles.Size = new System.Drawing.Size(151, 21);
            this.lblFamiliasDisponibles.TabIndex = 12;
            this.lblFamiliasDisponibles.Text = "Familias disponibles";
            // 
            // cmbFamiliasDisponibles
            // 
            this.cmbFamiliasDisponibles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFamiliasDisponibles.FormattingEnabled = true;
            this.cmbFamiliasDisponibles.Location = new System.Drawing.Point(408, 376);
            this.cmbFamiliasDisponibles.Name = "cmbFamiliasDisponibles";
            this.cmbFamiliasDisponibles.Size = new System.Drawing.Size(318, 29);
            this.cmbFamiliasDisponibles.TabIndex = 13;
            // 
            // btnAgregarFamilia
            // 
            this.btnAgregarFamilia.Location = new System.Drawing.Point(408, 418);
            this.btnAgregarFamilia.Name = "btnAgregarFamilia";
            this.btnAgregarFamilia.Size = new System.Drawing.Size(318, 34);
            this.btnAgregarFamilia.TabIndex = 14;
            this.btnAgregarFamilia.Text = "Agregar subfamilia";
            this.btnAgregarFamilia.UseVisualStyleBackColor = true;
            this.btnAgregarFamilia.Click += new System.EventHandler(this.btnAgregarFamilia_Click);
            // 
            // FrmGestionFamilias_83KI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(764, 552);
            this.Controls.Add(this.btnAgregarFamilia);
            this.Controls.Add(this.cmbFamiliasDisponibles);
            this.Controls.Add(this.lblFamiliasDisponibles);
            this.Controls.Add(this.btnAgregarPatente);
            this.Controls.Add(this.lstPatentesDisponibles);
            this.Controls.Add(this.lblPatentes);
            this.Controls.Add(this.btnQuitarSeleccion);
            this.Controls.Add(this.treeFamilia);
            this.Controls.Add(this.btnEliminarFamilia);
            this.Controls.Add(this.btnCrearFamilia);
            this.Controls.Add(this.txtNombreFamilia);
            this.Controls.Add(this.cmbFamilias);
            this.Controls.Add(this.lblFamilia);
            this.Controls.Add(this.rdbCrearFamilia);
            this.Controls.Add(this.rdbSeleccionarFamilia);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Name = "FrmGestionFamilias_83KI";
            this.Text = "Gestion de familias";
            this.Load += new System.EventHandler(this.FrmGestionFamilias_83KI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.RadioButton rdbSeleccionarFamilia;
        private System.Windows.Forms.RadioButton rdbCrearFamilia;
        private System.Windows.Forms.Label lblFamilia;
        private System.Windows.Forms.ComboBox cmbFamilias;
        private System.Windows.Forms.TextBox txtNombreFamilia;
        private System.Windows.Forms.Button btnCrearFamilia;
        private System.Windows.Forms.Button btnEliminarFamilia;
        private System.Windows.Forms.TreeView treeFamilia;
        private System.Windows.Forms.Button btnQuitarSeleccion;
        private System.Windows.Forms.Label lblPatentes;
        private System.Windows.Forms.ListBox lstPatentesDisponibles;
        private System.Windows.Forms.Button btnAgregarPatente;
        private System.Windows.Forms.Label lblFamiliasDisponibles;
        private System.Windows.Forms.ComboBox cmbFamiliasDisponibles;
        private System.Windows.Forms.Button btnAgregarFamilia;
    }
}
