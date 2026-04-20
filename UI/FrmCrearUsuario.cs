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
    public partial class FrmCrearUsuario : Form
    {
        private readonly IGestorUsuario _gestorUsuario;
        public FrmCrearUsuario(IGestorUsuario gestorUsuario)
        {
            InitializeComponent();
            _gestorUsuario = gestorUsuario;
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            string 
        }

        private void ValidarDatos()
        {

        }
    }
}
