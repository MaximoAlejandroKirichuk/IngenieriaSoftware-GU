using BLL;
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
    public partial class FrmGestionUsuarios : Form
    {
        public FrmGestionUsuarios()
        {
            InitializeComponent();
        }
        private readonly IGestorUsuario _gestorUsuario;
        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                IGestorUsuario gestorUsuario = Service.ServiceFactory.GetGestorUsuario();
                FrmCrearUsuario frmCrearUsuario = new FrmCrearUsuario(gestorUsuario);
                //todo: que pasa si no agrego nada? se actualice igual
                ActualizarDatos();
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
        }

        private void btnDesbloquearusuario_Click(object sender, EventArgs e)
        {

        }
    }
}
