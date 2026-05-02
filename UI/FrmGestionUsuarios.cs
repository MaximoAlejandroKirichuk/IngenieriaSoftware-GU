using Service.Entidades;
using Service.Interfaces;
using System;
using System.Drawing;
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
            ActualizarBotonesAccion();
        }

        private void ActualizarDatos()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = _gestorUsuario.ObtenerUsuarios();
            ConfigurarGrilla();
        }

        private void ActualizarBotonesAccion()
        {
            Usuario_83KI usuarioSeleccionado = ObtenerUsuarioSeleccionado();
            bool haySeleccion = usuarioSeleccionado != null;

            btnCambiarEstadoUsuario.Enabled = haySeleccion;
            btnDesbloquearUsuario.Enabled = haySeleccion && usuarioSeleccionado.Bloqueado;

            if (!haySeleccion)
            {
                btnCambiarEstadoUsuario.Text = "Gestionar estado";
                btnCambiarEstadoUsuario.BackColor = SystemColors.Control;
                btnCambiarEstadoUsuario.ForeColor = SystemColors.ControlText;
                btnDesbloquearUsuario.BackColor = SystemColors.Control;
                btnDesbloquearUsuario.ForeColor = SystemColors.ControlText;
                return;
            }

            btnCambiarEstadoUsuario.Text = usuarioSeleccionado.Activo ? "Deshabilitar usuario" : "Habilitar usuario";
            btnCambiarEstadoUsuario.BackColor = usuarioSeleccionado.Activo ? Color.IndianRed : Color.DarkSeaGreen;
            btnCambiarEstadoUsuario.ForeColor = Color.Black;
            btnDesbloquearUsuario.BackColor = usuarioSeleccionado.Bloqueado ? Color.DarkSeaGreen : SystemColors.Control;
            btnDesbloquearUsuario.ForeColor = Color.Black;
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
                        ActualizarBotonesAccion();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

            if (dgvUsuarios.Columns["Activo"] != null)
            {
                dgvUsuarios.Columns["Activo"].HeaderText = "Habilitado";
                dgvUsuarios.Columns["Activo"].DisplayIndex = 5;
            }

            if (dgvUsuarios.Columns["Bloqueado"] != null)
            {
                dgvUsuarios.Columns["Bloqueado"].HeaderText = "Bloqueado";
                dgvUsuarios.Columns["Bloqueado"].DisplayIndex = 6;
            }

            if (dgvUsuarios.Columns["Intentos"] != null)
            {
                dgvUsuarios.Columns["Intentos"].DisplayIndex = 7;
            }
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarBotonesAccion();
        }

        private void btnCambiarEstadoUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario_83KI usuarioElegido = ObtenerUsuarioSeleccionado();
                if (usuarioElegido == null)
                {
                    MessageBox.Show("Por favor, seleccione un usuario de la lista.");
                    return;
                }

                using (FrmConfirmarEstadoUsuario frmConfirmacion = new FrmConfirmarEstadoUsuario(usuarioElegido))
                {
                    if (frmConfirmacion.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }
                }

                if (usuarioElegido.Activo)
                {
                    _gestorUsuario.DeshabilitarUsuario(usuarioElegido.DNI);
                    MessageBox.Show("Usuario deshabilitado con exito.");
                }
                else
                {
                    _gestorUsuario.HabilitarUsuario(usuarioElegido.DNI);
                    MessageBox.Show("Usuario habilitado con exito.");
                }

                ActualizarDatos();
                ActualizarBotonesAccion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar gestionar el estado: " + ex.Message);
            }
        }

        private Usuario_83KI ObtenerUsuarioSeleccionado()
        {
            return dgvUsuarios.CurrentRow?.DataBoundItem as Usuario_83KI;
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
                        ActualizarBotonesAccion();
                        MessageBox.Show("Usuario modificado con exito.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar modificar: " + ex.Message);
            }
        }

        private void btnDesbloquearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario_83KI usuarioElegido = ObtenerUsuarioSeleccionado();
                if (usuarioElegido == null)
                {
                    MessageBox.Show("Por favor, seleccione un usuario de la lista.");
                    return;
                }

                if (!usuarioElegido.Bloqueado)
                {
                    MessageBox.Show("El usuario seleccionado no se encuentra bloqueado.");
                    return;
                }

                _gestorUsuario.DesbloquearCuenta(usuarioElegido.DNI);
                ActualizarDatos();
                ActualizarBotonesAccion();
                MessageBox.Show("Usuario desbloqueado con exito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar desbloquear: " + ex.Message);
            }
        }
    }
}
