using Service.Entidades;
using Service.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmGestionFamilias_83KI : Form
    {
        private readonly IGestorRol_83KI _gestorRol;
        private bool _cargandoDatos;

        public FrmGestionFamilias_83KI(IGestorRol_83KI gestorRol)
        {
            InitializeComponent();
            _gestorRol = gestorRol;
        }

        private void FrmGestionFamilias_83KI_Load(object sender, EventArgs e)
        {
            rdbSeleccionarFamilia.Checked = true;
            CargarDatos();
            ActualizarModoFamilia();
            ActualizarBotonQuitar();
        }

        private void CargarDatos()
        {
            _cargandoDatos = true;
            CargarFamilias();
            CargarPatentes();
            CargarFamiliasDisponibles();
            _cargandoDatos = false;
            CargarArbolFamilia();
        }

        private void CargarFamilias()
        {
            cmbFamilias.DataSource = null;
            cmbFamilias.DisplayMember = nameof(Familia_83KI.Nombre);
            cmbFamilias.ValueMember = nameof(Familia_83KI.CodigoFamilia);
            cmbFamilias.DataSource = _gestorRol.ObtenerFamilias().ToList();
        }

        private void CargarPatentes()
        {
            lstPatentesDisponibles.DataSource = null;
            lstPatentesDisponibles.DisplayMember = nameof(Patente_83KI.Nombre);
            lstPatentesDisponibles.ValueMember = nameof(Patente_83KI.CodigoPatente);
            lstPatentesDisponibles.DataSource = _gestorRol.ObtenerPatentes().ToList();
        }

        private void CargarFamiliasDisponibles()
        {
            Familia_83KI seleccionada = ObtenerFamiliaSeleccionada();
            var familias = _gestorRol.ObtenerFamilias()
                .Where(f => seleccionada == null || f.CodigoFamilia != seleccionada.CodigoFamilia)
                .ToList();

            cmbFamiliasDisponibles.DataSource = null;
            cmbFamiliasDisponibles.DisplayMember = nameof(Familia_83KI.Nombre);
            cmbFamiliasDisponibles.ValueMember = nameof(Familia_83KI.CodigoFamilia);
            cmbFamiliasDisponibles.DataSource = familias;
        }

        private void CargarArbolFamilia()
        {
            treeFamilia.Nodes.Clear();
            Familia_83KI familia = ObtenerFamiliaSeleccionada();

            if (familia == null)
            {
                txtNombreFamilia.Text = string.Empty;
                ActualizarBotonQuitar();
                return;
            }

            TreeNode raiz = CrearNodo(familia);
            treeFamilia.Nodes.Add(raiz);
            raiz.ExpandAll();
            ActualizarBotonQuitar();
        }

        private TreeNode CrearNodo(ComponentePermiso_83KI componente)
        {
            TreeNode nodo = new TreeNode(componente.Nombre) { Tag = componente };

            foreach (ComponentePermiso_83KI hijo in componente.Hijos)
            {
                nodo.Nodes.Add(CrearNodo(hijo));
            }

            return nodo;
        }

        private Familia_83KI ObtenerFamiliaSeleccionada()
        {
            return cmbFamilias.SelectedItem as Familia_83KI;
        }

        private void cmbFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoDatos)
            {
                return;
            }

            CargarFamiliasDisponibles();
            CargarArbolFamilia();
        }

        private void rdbSeleccionarFamilia_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarModoFamilia();
        }

        private void btnAgregarPatente_Click(object sender, EventArgs e)
        {
            AgregarPatenteSeleccionada();
        }

        private void AgregarPatenteSeleccionada()
        {
            Familia_83KI familia = ObtenerFamiliaSeleccionada();
            Patente_83KI patente = lstPatentesDisponibles.SelectedItem as Patente_83KI;

            if (familia == null || patente == null)
            {
                return;
            }

            EjecutarOperacion(() => _gestorRol.AsignarPatenteAFamilia(familia.CodigoFamilia, patente.CodigoPatente));
        }

        private void btnCrearFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                Familia_83KI familiaCreada = _gestorRol.CrearFamilia(txtNombreFamilia.Text);
                rdbSeleccionarFamilia.Checked = true;
                CargarDatos();
                cmbFamilias.SelectedValue = familiaCreada.CodigoFamilia;
                txtNombreFamilia.Clear();
                ActualizarModoFamilia();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Gestion de familias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminarFamilia_Click(object sender, EventArgs e)
        {
            Familia_83KI familia = ObtenerFamiliaSeleccionada();

            if (familia == null)
            {
                return;
            }

            EjecutarOperacion(() => _gestorRol.EliminarFamilia(familia.CodigoFamilia));
        }

        private void btnAgregarFamilia_Click(object sender, EventArgs e)
        {
            Familia_83KI padre = ObtenerFamiliaSeleccionada();
            Familia_83KI hija = cmbFamiliasDisponibles.SelectedItem as Familia_83KI;

            if (padre == null || hija == null)
            {
                return;
            }

            EjecutarOperacion(() => _gestorRol.AsignarFamiliaAFamilia(padre.CodigoFamilia, hija.CodigoFamilia));
        }

        private void btnQuitarSeleccion_Click(object sender, EventArgs e)
        {
            Familia_83KI familia = ObtenerFamiliaSeleccionada();

            if (familia == null || treeFamilia.SelectedNode == null || treeFamilia.SelectedNode.Parent == null)
            {
                return;
            }

            ComponentePermiso_83KI componente = treeFamilia.SelectedNode.Tag as ComponentePermiso_83KI;
            Familia_83KI familiaPadre = treeFamilia.SelectedNode.Parent.Tag as Familia_83KI;

            if (familiaPadre == null)
            {
                return;
            }

            if (componente is Patente_83KI patente)
            {
                EjecutarOperacion(() => _gestorRol.QuitarPatenteDeFamilia(familiaPadre.CodigoFamilia, patente.CodigoPatente));
            }
            else if (componente is Familia_83KI familiaHija)
            {
                EjecutarOperacion(() => _gestorRol.QuitarFamiliaDeFamilia(familiaPadre.CodigoFamilia, familiaHija.CodigoFamilia));
            }
        }

        private void treeFamilia_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ActualizarBotonQuitar();
        }

        private void ActualizarModoFamilia()
        {
            bool seleccionarExistente = rdbSeleccionarFamilia.Checked;

            cmbFamilias.Visible = seleccionarExistente;
            btnEliminarFamilia.Visible = seleccionarExistente;
            treeFamilia.Visible = seleccionarExistente;
            btnQuitarSeleccion.Visible = seleccionarExistente;
            lblPatentes.Visible = seleccionarExistente;
            lstPatentesDisponibles.Visible = seleccionarExistente;
            btnAgregarPatente.Visible = seleccionarExistente;
            lblFamiliasDisponibles.Visible = seleccionarExistente;
            cmbFamiliasDisponibles.Visible = seleccionarExistente;
            btnAgregarFamilia.Visible = seleccionarExistente;
            btnAgregarPatente.Enabled = seleccionarExistente;

            txtNombreFamilia.Visible = !seleccionarExistente;
            btnCrearFamilia.Visible = !seleccionarExistente;
            lblFamilia.Text = seleccionarExistente ? "Familia existente" : "Nombre de la nueva familia";

            if (!seleccionarExistente)
            {
                treeFamilia.Nodes.Clear();
                txtNombreFamilia.Focus();
            }
            else
            {
                CargarArbolFamilia();
            }
        }

        private void ActualizarBotonQuitar()
        {
            btnQuitarSeleccion.Enabled = false;
            btnQuitarSeleccion.Text = "Seleccionar permiso o subfamilia";

            if (treeFamilia.SelectedNode == null || treeFamilia.SelectedNode.Parent == null)
            {
                return;
            }

            if (treeFamilia.SelectedNode.Tag is Patente_83KI)
            {
                btnQuitarSeleccion.Text = "Quitar patente";
                btnQuitarSeleccion.Enabled = true;
            }
            else if (treeFamilia.SelectedNode.Tag is Familia_83KI)
            {
                btnQuitarSeleccion.Text = "Quitar subfamilia";
                btnQuitarSeleccion.Enabled = true;
            }
        }

        private void EjecutarOperacion(Action operacion)
        {
            try
            {
                operacion();
                CargarDatos();
                ActualizarModoFamilia();
                ActualizarBotonQuitar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Gestion de familias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
