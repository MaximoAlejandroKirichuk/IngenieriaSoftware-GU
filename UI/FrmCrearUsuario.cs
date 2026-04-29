using Service.Entidades;
using Service.Excepciones.CrearUsuario;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmCrearUsuario : Form
    {
        private readonly IGestorUsuario_83KI _usuarioService;
        public FrmCrearUsuario(IGestorUsuario_83KI gestorUsuario)
        {
            InitializeComponent();
            _usuarioService = gestorUsuario;
        }


        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            if (!ValidarDatosFormato()) return;
            try
            {
                var nuevoUsuario = ObtenerDatos();
                _usuarioService.CrearUsuario(nuevoUsuario);
                MessageBox.Show("Usuario registrado con éxito.");
                this.DialogResult = DialogResult.OK;
            }

            catch (DniRegistradoException_83KI ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (EmailRegistradoException_83KI ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Usuario_83KI ObtenerDatos()
        {
            int.TryParse(txt_Dni.Text, out int dni);

            Enum.TryParse(comboBox1.SelectedItem?.ToString(), true, out RolUsuario rol);

            return new Usuario_83KI
            {
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                DNI = dni,
                Email = txtEmail.Text.Trim(),
                RolUsuario = rol
            };
        }


        private bool ValidarDatosFormato()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txt_Dni.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return false;
            }

            string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(txtEmail.Text, patronEmail))
            {
                MessageBox.Show("El formato del Email no es válido.");
                return false;
            }

            if (!long.TryParse(txt_Dni.Text, out _) || txt_Dni.Text.Length < 7)
            {
                MessageBox.Show("El DNI debe ser un número válido.");
                return false;
            }

            return true;
        }
        public void FormDesign(Panel panel, Button btn)
        {
            //colores de los botones, txt, etc
            BackColor = Color.FromArgb(70, 130, 180);
            txtNombre.BackColor = Color.FromArgb(240, 240, 240);
            txtApellido.BackColor = Color.FromArgb(240, 240, 240);
            txtEmail.BackColor = Color.FromArgb(240, 240, 240);
            txt_Dni.BackColor = Color.FromArgb(240, 240, 240);
            comboBox1.BackColor = Color.FromArgb(240, 240, 240);
            btnCrearUsuario.BackColor = Color.FromArgb(240, 240, 240);

            //bordes del panel
            GraphicsPath path = new GraphicsPath();
            int radio = 30;

            path.StartFigure();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(panel.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(panel.Width - radio, panel.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, panel.Height - radio, radio, radio, 90, 90);
            path.CloseFigure();

            panel.Region = new Region(path);

            //color de boton
            btn.BackColor = Color.FromArgb(70, 130, 180);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            path.Reset();
            int radioBtn = btn.Height;
            path.StartFigure();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(btn.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(btn.Width - radio, btn.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, btn.Height - radio, radio, radio, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);
        }
        private void FrmCrearUsuario_Load(object sender, EventArgs e)
        {
            FormDesign(panelCrearUsu, btnCrearUsuario); 
        }
    }
}
