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
        public FrmPrincipal(IGestorUsuario_83KI gestorUsuario)
        {
            InitializeComponent();
            _gestorUsuario = gestorUsuario;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var gestion = new FrmGestionUsuarios(_gestorUsuario);
            gestion.Show();
        }

        private void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            var cambiarContrasena = new FrmCambiarContrasena(_gestorUsuario);
            cambiarContrasena.ShowDialog(this);
        }
    }
}
