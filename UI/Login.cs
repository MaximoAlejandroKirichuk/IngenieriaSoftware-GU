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
    public partial class Login : Form
    {
        private const int MaxIntentosLogin = 3;
        private readonly IGestorUsuario_83KI _gestor;
        private int _intentosFallidosLogin;

        public Login()
        {
            _gestor = Service.ServiceFactory_83KI.GetGestorUsuario();
            InitializeComponent();
        }


        private void Login_Load(object sender, EventArgs e)
        {
            LoginDesignConfig(pictureBox1);
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
                _intentosFallidosLogin = 0;
                Hide();

                using (var formPrincipal = new FrmPrincipal(_gestor))
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (UsuarioNoExisteException_83KI ex)
            {
                Show();
                MessageBox.Show(ex.Message);
            }

            catch (UsuarioBloqueadoException_83KI ex)
            {
                Show();
                MessageBox.Show(ex.Message, "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch (UsuarioDeshabilitadoException_83KI ex)
            {
                Show();
                MessageBox.Show(ex.Message, "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            
            catch (ContrasenaInvalidaException_83KI)
            {
                Show();
                _intentosFallidosLogin++;

                if (_intentosFallidosLogin >= MaxIntentosLogin)
                {
                    _gestor.BloquearUsuarioPorUserName(userName);
                    MessageBox.Show("Usuario bloqueado por superar la cantidad maxima de intentos.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                MessageBox.Show($"La contraseña es invalida. Intento {_intentosFallidosLogin} de {MaxIntentosLogin}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoginDesignConfig(PictureBox pic)  //diseño de la interfaz
        {
            BackColor = Color.FromArgb(70, 130, 180);
            txt_userName.BackColor = Color.FromArgb(240, 240, 240);
            txt_Contrasena.BackColor = Color.FromArgb(240, 240, 240);
            //diseño imagen
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pic.Width, pic.Height);
            pic.Region = new Region(path);
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
    }
}
