using BLL.Interfaces;
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
        private bool _logoutConfirmado;

        public FrmPrincipal(IGestorUsuario_83KI gestorUsuario)
        {
            InitializeComponent();
            _gestorUsuario = gestorUsuario;
        }


        private void btnCambiarContrasena_Click(object sender, EventArgs e)
        {

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
                e.Cancel = !ConfirmarLogoutYCerrar();
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
            var gestion = new FrmGestionUsuarios(_gestorUsuario);
            gestion.Show();
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cambiarContrasena = new FrmCambiarContrasena(_gestorUsuario);
            cambiarContrasena.ShowDialog(this);
        }
    }
}
