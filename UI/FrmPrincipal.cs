using Service;
using Service.Entidades;
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
    public partial class FrmPrincipal : Form, IObservadorIdioma
    {
        private readonly IGestorUsuario_83KI _gestorUsuario;
        private readonly IGestorRol_83KI _gestorRol;
        private readonly IGestorIdioma_83KI _gestorIdioma;
        private bool _logoutConfirmado;

        public FrmPrincipal(IGestorUsuario_83KI gestorUsuario, IGestorRol_83KI gestorRol)
        {
            InitializeComponent();
            _gestorUsuario = gestorUsuario;
            _gestorRol = gestorRol;
            _gestorIdioma = ServiceFactory_83KI.GetGestorIdioma();
            AplicarPermisos();
            _gestorIdioma.Suscribir(this);
        }

        private void AplicarPermisos()
        {
            gestionDeUsuariosToolStripMenuItem.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.GestionUsuarios);
            gestionDeFamiliasToolStripMenuItem.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.GestionFamilias);
            gestionDeRolesToolStripMenuItem.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.GestionRoles);
            bitacoraEventosToolStripMenuItem.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.ConsultarBitacoraEventos);
            adminToolStripMenuItem.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.GestionAdmin); 

            menuCerrarSesion.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.CerrarSesion);
            iniciarSesionToolStripMenuItem.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.ReLogin);
            cambiarContraseñaToolStripMenuItem.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.CambiarContrasena);
            reToolStripMenuItem.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.Ayuda);
        }

        public void ActualizarIdioma(IIdioma idioma)
        {
            Text = Texto("FrmPrincipal.Titulo");
            menuSesion.Text = Texto("FrmPrincipal.Usuario");
            menuCerrarSesion.Text = Texto("FrmPrincipal.CerrarSesion");
            iniciarSesionToolStripMenuItem.Text = Texto("FrmPrincipal.IniciarSesion");
            cambiarContraseñaToolStripMenuItem.Text = Texto("FrmPrincipal.CambiarContrasena");
            adminToolStripMenuItem.Text = Texto("FrmPrincipal.Admin");
            gestionDeUsuariosToolStripMenuItem.Text = Texto("FrmPrincipal.GestionUsuarios");
            gestionDeFamiliasToolStripMenuItem.Text = Texto("FrmPrincipal.GestionFamilias");
            gestionDeRolesToolStripMenuItem.Text = Texto("FrmPrincipal.GestionRoles");
            bitacoraEventosToolStripMenuItem.Text = Texto("FrmPrincipal.BitacoraEventos");
            reToolStripMenuItem.Text = Texto("FrmPrincipal.Ayuda");
            menuIdioma.Text = Texto("FrmPrincipal.Idioma");
            espanolToolStripMenuItem.Text = Texto("FrmPrincipal.Espanol");
            inglesToolStripMenuItem.Text = Texto("FrmPrincipal.Ingles");

            string idiomaActualId = idioma?.Id ?? GestorIdioma_83KI.IdiomaPorDefecto;
            espanolToolStripMenuItem.Checked = idiomaActualId == "es-AR";
            inglesToolStripMenuItem.Checked = idiomaActualId == "en-US";
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _gestorIdioma.Desuscribir(this);
            base.OnFormClosed(e);
        }

        private string Texto(string clave)
        {
            //Este método busca una traducción en el idioma actual.
            return _gestorIdioma.ObtenerTexto(clave);
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
                Texto("FrmPrincipal.ConfirmarCerrarSesionMensaje"),
                Texto("FrmPrincipal.ConfirmarCerrarSesionTitulo"),
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
                Texto("FrmPrincipal.CambioContrasenaMensaje"),
                Texto("FrmPrincipal.CambioContrasenaTitulo"),
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

        private void espanolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _gestorUsuario.CambiarIdiomaUsuarioActual("es-AR");
        }

        private void inglesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _gestorUsuario.CambiarIdiomaUsuarioActual("en-US");
        }
    }
}
