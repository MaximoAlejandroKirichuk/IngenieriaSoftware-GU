using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Service.Excepciones.Login;
using Service.Interfaces;
using Service.Excepciones;

namespace UI
{
    public partial class Login : Form, IObservadorIdioma
    {
        private readonly IGestorUsuario_83KI _gestor;
        private readonly IGestorRol_83KI _gestorRol;
        private readonly IGestorIdioma_83KI _gestorIdioma;

        public Login()
        {
            _gestor = Service.ServiceFactory_83KI.GetGestorUsuario();
            _gestorRol = Service.ServiceFactory_83KI.GetGestorRol();
            _gestorIdioma = Service.ServiceFactory_83KI.GetGestorIdioma();
            InitializeComponent();
            _gestorIdioma.Suscribir(this);
        }


        private void Login_Load(object sender, EventArgs e)
        {
            LoginDesignConfig();
            RedondearPanel(panelLogin);
            ButtonDesing(btnLogin);

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            var userName = txt_userName.Text.Trim();
            var contrasena = txt_Contrasena.Text.Trim();

            try
            {
                _gestor.Login(userName, contrasena);

                var usuarioActivo = Service.SessionManager_83KI.Instancia.UsuarioActivo;

                if (usuarioActivo != null)
                {
                    //aca se hace la contraseña base para compararla
                    string contrasenaPorDefecto = Service.Entidades.Usuario_83KI.EstablecerContrasenaPorDefecto(usuarioActivo.Apellido, usuarioActivo.DNI);

                    bool usaContrasenaPorDefecto = (contrasena == contrasenaPorDefecto);

                    if (usaContrasenaPorDefecto == true)
                    {
                        MostrarAdvertenciaYForzarCambio();
                    }
                }

                Hide();

                using (var formPrincipal = new FrmPrincipal(_gestor, _gestorRol))
                {
                    var resultado = formPrincipal.ShowDialog(this);

                    if (resultado == DialogResult.Retry)
                    {
                        txt_Contrasena.Clear();
                        txt_Contrasena.Focus();
                        Show();
                        return;
                    }
                }
                Close();
            }
            catch (UsuarioActivoActualmenteException_83KI ex)
            {
                Show();
                IdiomaUiHelper_83KI.MostrarError(this, ex, "Comun.Error", MessageBoxIcon.Warning);
            }
            catch (UsuarioNoExisteException_83KI ex)
            {
                Show();
                IdiomaUiHelper_83KI.MostrarError(this, ex, "Comun.Error", MessageBoxIcon.Warning);
            }
            catch (UsuarioBloqueadoException_83KI ex)
            {
                Show();
                IdiomaUiHelper_83KI.MostrarError(this, ex, "Comun.Seguridad", MessageBoxIcon.Stop);
            }
            catch (UsuarioDeshabilitadoException_83KI ex)
            {
                Show();
                IdiomaUiHelper_83KI.MostrarError(this, ex, "Comun.Usuarios", MessageBoxIcon.Stop);
            }
            catch (ContrasenaInvalidaException_83KI ex)
            {
                Show();
                IdiomaUiHelper_83KI.MostrarError(this, ex, "Comun.Error", MessageBoxIcon.Warning);
            }
        }

        private void MostrarAdvertenciaYForzarCambio()
        {
            IdiomaUiHelper_83KI.MostrarAdvertencia(
                this,
                "Login.ContrasenaDefaultMensaje",
                "Login.ContrasenaDefaultTitulo");

            using (var frmCambio = new FrmCambiarContrasena(_gestor))
            {
                var resultadoCambio = frmCambio.ShowDialog(this);

                if (resultadoCambio == DialogResult.OK)
                {
                    IdiomaUiHelper_83KI.MostrarInformacion(this, "Login.ContrasenaActualizada", "Comun.Informacion");
                }
            }
        }


        public void LoginDesignConfig()  //diseño de la interfaz
        {
            BackColor = Color.FromArgb(70, 130, 180);
            txt_userName.BackColor = Color.FromArgb(240, 240, 240);
            txt_Contrasena.BackColor = Color.FromArgb(240, 240, 240);
            //diseño imagen
            GraphicsPath path = new GraphicsPath();
        }
        private void RedondearPanel(Panel panel) //diseño del panel
        {
            GraphicsPath path = new GraphicsPath();
            int radio = 30;

            path.StartFigure();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(panel.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(panel.Width - radio, panel.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, panel.Height - radio, radio, radio, 90, 90);
            path.CloseFigure();

            panel.Region = new Region(path);
        }
        public void ButtonDesing(Button btn) //diseño del boton 
        {
            btn.BackColor = Color.FromArgb(70, 130, 180);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            GraphicsPath path = new GraphicsPath();
            int radio = 20;

            path.StartFigure();
            path.AddArc(0, 0, radio, radio, 180, 90);
            path.AddArc(btn.Width - radio, 0, radio, radio, 270, 90);
            path.AddArc(btn.Width - radio, btn.Height - radio, radio, radio, 0, 90);
            path.AddArc(0, btn.Height - radio, radio, radio, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);
        }

        public void ActualizarIdioma(IIdioma idioma)
        {
            Text = IdiomaUiHelper_83KI.Texto("Login.Titulo");
            lbl_Email.Text = IdiomaUiHelper_83KI.Texto("Login.Username");
            lbl_Contrasena.Text = IdiomaUiHelper_83KI.Texto("Login.Contrasena");
            btnLogin.Text = IdiomaUiHelper_83KI.Texto("Login.Ingresar");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _gestorIdioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
