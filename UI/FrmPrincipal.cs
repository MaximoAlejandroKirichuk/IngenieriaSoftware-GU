using Service;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmPrincipal : Form
    {
        private readonly IGestorUsuario_83KI _gestorUsuario;
        private readonly IGestorRol_83KI _gestorRol;
        private bool _logoutConfirmado;

        public FrmPrincipal(IGestorUsuario_83KI gestorUsuario, IGestorRol_83KI gestorRol)
        {
            InitializeComponent();
            AplicarPermisos();
            _gestorUsuario = gestorUsuario;
            _gestorRol = gestorRol;
        }

        private void AplicarPermisos()
        {
            var usuario = SessionManager_83KI.Instancia.UsuarioActivo;
            adminToolStripMenuItem.Visible = usuario.Rol.PuedeGestionarAdmin;
        }
        private void menuCerrarSesion_Click(object sender, EventArgs e)
        {
            ConfirmarLogoutYCerrar();
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_logoutConfirmado)
            {
                return;
            }

            if (e.CloseReason == CloseReason.UserClosing)
            {
                bool logoutAprobado = ConfirmarLogoutYCerrar();

                if (logoutAprobado == false)
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
        }

        private bool ConfirmarLogoutYCerrar()
        {
            var confirmacion = MessageBox.Show(
                "¿Desea cerrar la sesión actual?",
                "Cerrar sesión",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.OK)
            {
                return false;
            }

            _gestorUsuario.Logout();
            _logoutConfirmado = true;
            DialogResult = DialogResult.Retry;
            Close();
            return true;
        }

        private void gestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var gestion = new FrmGestionUsuarios(_gestorUsuario, _gestorRol))
            {
                gestion.ShowDialog(this);
            }
        }

        private void gestionDeFamiliasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var gestion = new FrmGestionFamilias_83KI(_gestorRol))
            {
                gestion.ShowDialog(this);
            }
        }

        private void gestionDeRolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var gestion = new FrmGestionRoles_83KI(_gestorRol))
            {
                gestion.ShowDialog(this);
            }
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cambiarContrasena = new FrmCambiarContrasena(_gestorUsuario);
            DialogResult resultado = cambiarContrasena.ShowDialog(this);

            if(resultado == DialogResult.OK)
            {
                ForzarLogout(); 
            }
        }

        private void ForzarLogout()
        {
            MessageBox.Show(
                "Contraseña modificada correctamente. Por seguridad, la sesión se cerrará.",
                "Cambio de Contraseña",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            _gestorUsuario.Logout();
            _logoutConfirmado = true;
            DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
        }

        private void menuSesion_Click(object sender, EventArgs e)
        {

        }

        private void bitacoraEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var bitacoraEventos = new FrmBitacoraEventos(ServiceFactory_83KI.GetConsultaBitacoraEventos()))
            {
                bitacoraEventos.ShowDialog(this);
            }
        }
    }
}
