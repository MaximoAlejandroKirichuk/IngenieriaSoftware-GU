using BLL.Excepciones.Login;
using BLL.Interfaces;
using System;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmCambiarContrasena : Form
    {
        private readonly IGestorUsuario_83KI _gestorUsuario;

        public FrmCambiarContrasena(IGestorUsuario_83KI gestorUsuario)
        {
            InitializeComponent();
            _gestorUsuario = gestorUsuario;
        }

        private void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
            {
                return;
            }

            try
            {
                _gestorUsuario.CambiarContrasenaUsuarioActual(txtContrasenaActual.Text, txtNuevaContrasena.Text);
                MessageBox.Show("Contraseña modificada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ContrasenaInvalidaException_83KI ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (UsuarioNoAutenticadoException_83KI ex)
            {
                MessageBox.Show(ex.Message, "Sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtContrasenaActual.Text) ||
                string.IsNullOrWhiteSpace(txtNuevaContrasena.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmarContrasena.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtNuevaContrasena.Text != txtConfirmarContrasena.Text)
            {
                MessageBox.Show("La nueva contraseña y su confirmación deben coincidir.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
