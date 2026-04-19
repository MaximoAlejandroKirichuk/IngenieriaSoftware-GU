using BLL.Excepciones;
using BLL.Excepciones.Login;
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
    public partial class Login : Form
    {
        private readonly IGestorUsuario _gestor;
        public Login()
        {
            _gestor = Service.ServiceFactory.GetGestorUsuario();
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var email = txt_email.Text;
                var contrasena = txt_Contrasena.Text;
                _gestor.Login(email, contrasena);
            }
            catch(UsuarioActivoActualmenteException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (UsuarioBloqueadoException ex)
            {
                MessageBox.Show(ex.Message, "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            
            catch (ContrasenaInvalidaException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
