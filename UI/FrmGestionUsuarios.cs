using BE;
using BLL.Interfaces;
using System;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmGestionUsuarios : Form
    {
        private readonly IGestorUsuario_83KI _gestorUsuario;
        public FrmGestionUsuarios(IGestorUsuario_83KI gestorUsuario)
        {
            InitializeComponent();
            _gestorUsuario = gestorUsuario;
        }
        private void FrmGestionUsuarios_Load(object sender, EventArgs e)
        {
            ActualizarDatos();

        }
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmCrearUsuario frmCrearUsuario = new FrmCrearUsuario(_gestorUsuario))
                {
                    if (frmCrearUsuario.ShowDialog(this) == DialogResult.OK)
                    {
                        ActualizarDatos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ActualizarDatos()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = _gestorUsuario.ObtenerUsuarios();
            ConfigurarGrilla();
        }

        private void ConfigurarGrilla()
        {
            if (dgvUsuarios.Columns["Contrasena"] != null)
            {
                dgvUsuarios.Columns["Contrasena"].Visible = false;
            }

            if (dgvUsuarios.Columns["UserName"] != null)
            {
                dgvUsuarios.Columns["UserName"].Visible = false;
            }

            if (dgvUsuarios.Columns["Nombre"] != null)
            {
                dgvUsuarios.Columns["Nombre"].DisplayIndex = 0;
            }

            if (dgvUsuarios.Columns["Apellido"] != null)
            {
                dgvUsuarios.Columns["Apellido"].DisplayIndex = 1;
            }

            if (dgvUsuarios.Columns["DNI"] != null)
            {
                dgvUsuarios.Columns["DNI"].DisplayIndex = 2;
            }

            if (dgvUsuarios.Columns["Email"] != null)
            {
                dgvUsuarios.Columns["Email"].DisplayIndex = 3;
            }

            if (dgvUsuarios.Columns["RolUsuario"] != null)
            {
                dgvUsuarios.Columns["RolUsuario"].HeaderText = "Rol";
                dgvUsuarios.Columns["RolUsuario"].DisplayIndex = 4;
            }
        }

        private void btnDesbloquearusuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.CurrentRow != null)
                {
                    Usuario_83KI usuarioElegido = (Usuario_83KI)dgvUsuarios.CurrentRow.DataBoundItem;
                    _gestorUsuario.DesbloquearCuenta(usuarioElegido);
                    ActualizarDatos();
                    MessageBox.Show("Usuario desbloqueado con éxito.");
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un usuario de la lista.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar desbloquear: " + ex.Message);
            }
        }

        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario_83KI usuarioElegido = ObtenerUsuarioSeleccionado();
                if (usuarioElegido == null)
                {
                    MessageBox.Show("Por favor, seleccione un usuario de la lista.");
                    return;
                }

                using (FrmModificarUsuario frmModificarUsuario = new FrmModificarUsuario(_gestorUsuario, usuarioElegido))
                {
                    if (frmModificarUsuario.ShowDialog(this) == DialogResult.OK)
                    {
                        ActualizarDatos();
                        MessageBox.Show("Usuario modificado con éxito.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar modificar: " + ex.Message);
            }
        }

        private Usuario_83KI ObtenerUsuarioSeleccionado()
        {
            return dgvUsuarios.CurrentRow?.DataBoundItem as Usuario_83KI;
        }

    }
}
