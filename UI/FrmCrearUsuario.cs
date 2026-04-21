using BE;
using BLL.Excepciones.CrearUsuario;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}
