using BE;
using System;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmConfirmarEstadoUsuario : Form
    {
        private readonly Usuario_83KI _usuario;

        public FrmConfirmarEstadoUsuario(Usuario_83KI usuario)
        {
            _usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
            InitializeComponent();
            CargarDatos();
        }

        private void CargarDatos()
        {
            txtNombre.Text = _usuario.Nombre;
            txtApellido.Text = _usuario.Apellido;
            txtDni.Text = _usuario.DNI.ToString();
            txtEmail.Text = _usuario.Email;
            txtRol.Text = _usuario.RolUsuario.ToString();
            lblTitulo.Text = _usuario.Activo ? "Confirmar deshabilitacion de usuario" : "Confirmar habilitacion de usuario";
            btnConfirmar.Text = _usuario.Activo ? "Confirmar deshabilitacion" : "Confirmar habilitacion";
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
