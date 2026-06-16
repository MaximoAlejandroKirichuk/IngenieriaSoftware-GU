using Service.Entidades;
using Service.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmGestionFamilias_83KI : Form, IObservadorIdioma
    {
        private readonly IGestorRol_83KI _gestorRol;
        private readonly IGestorIdioma_83KI _gestorIdioma;
        private bool _cargandoDatos;

        public FrmGestionFamilias_83KI(IGestorRol_83KI gestorRol)
        {
            InitializeComponent();
            _gestorRol = gestorRol;
            _gestorIdioma = Service.ServiceFactory_83KI.GetGestorIdioma();
            _gestorIdioma.Suscribir(this);
        }

        private void FrmGestionFamilias_83KI_Load(object sender, EventArgs e)
        {
            rdbSeleccionarFamilia.Checked = PermisosUi_83KI.Tiene(PermisoSistema_83KI.VerFamilias);
            rdbCrearFamilia.Checked = !rdbSeleccionarFamilia.Checked && PermisosUi_83KI.Tiene(PermisoSistema_83KI.CrearFamilia);
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

            if (!PermisosUi_83KI.Tiene(PermisoSistema_83KI.VerFamilias))
            {
                ActualizarBotonQuitar();
                return;
            }

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

            Familia_83KI familia = componente as Familia_83KI;

            if (familia == null)
            {
                return nodo;
            }

            foreach (ComponentePermiso_83KI hijo in familia.Hijos.OrderBy(h => h.Nombre))
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

        private void lstPatentesDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoDatos)
            {
                return;
            }

            ActualizarBotonCrearFamilia();
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
            string nombre = txtNombreFamilia.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                IdiomaUiHelper_83KI.MostrarAdvertencia(this, "Errores.NombreObligatorio", "FrmGestionFamilias.Titulo");
                return;
            }

            Patente_83KI patenteSeleccionada = lstPatentesDisponibles.SelectedItem as Patente_83KI;

            if (patenteSeleccionada == null)
            {
                IdiomaUiHelper_83KI.MostrarAdvertencia(this, "Errores.FamiliaSinPatente", "FrmGestionFamilias.Titulo");
                return;
            }

            try
            {
                Familia_83KI familiaCreada = _gestorRol.CrearFamilia(nombre, patenteSeleccionada.CodigoPatente);
                rdbSeleccionarFamilia.Checked = true;
                CargarDatos();
                cmbFamilias.SelectedValue = familiaCreada.CodigoFamilia;
                txtNombreFamilia.Clear();
                ActualizarModoFamilia();
            }
            catch (Exception ex)
            {
                IdiomaUiHelper_83KI.MostrarError(this, ex, "FrmGestionFamilias.Titulo", MessageBoxIcon.Warning);
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
            bool puedeVerFamilias = PermisosUi_83KI.Tiene(PermisoSistema_83KI.VerFamilias);
            bool puedeCrearFamilia = PermisosUi_83KI.Tiene(PermisoSistema_83KI.CrearFamilia);
            bool puedeAgregarPatente = PermisosUi_83KI.Tiene(PermisoSistema_83KI.AgregarPatenteFamilia);
            bool puedeAgregarSubfamilia = PermisosUi_83KI.Tiene(PermisoSistema_83KI.AgregarSubfamilia);
            bool seleccionarExistente = rdbSeleccionarFamilia.Checked && puedeVerFamilias;
            bool crearFamilia = rdbCrearFamilia.Checked && puedeCrearFamilia;

            rdbSeleccionarFamilia.Visible = puedeVerFamilias;
            rdbCrearFamilia.Visible = puedeCrearFamilia;
            lblFamilia.Visible = seleccionarExistente || crearFamilia;
            cmbFamilias.Visible = seleccionarExistente;
            btnEliminarFamilia.Visible = seleccionarExistente && PermisosUi_83KI.Tiene(PermisoSistema_83KI.EliminarFamilia);
            treeFamilia.Visible = seleccionarExistente;
            btnQuitarSeleccion.Visible = seleccionarExistente && PermisosUi_83KI.TieneAlguno(
                PermisoSistema_83KI.QuitarPatenteFamilia,
                PermisoSistema_83KI.QuitarSubfamilia);
            lblPatentes.Visible = (seleccionarExistente && puedeAgregarPatente) || crearFamilia;
            lstPatentesDisponibles.Visible = (seleccionarExistente && puedeAgregarPatente) || crearFamilia;
            btnAgregarPatente.Visible = seleccionarExistente && puedeAgregarPatente;
            lblFamiliasDisponibles.Visible = seleccionarExistente && puedeAgregarSubfamilia;
            cmbFamiliasDisponibles.Visible = seleccionarExistente && puedeAgregarSubfamilia;
            btnAgregarFamilia.Visible = seleccionarExistente && puedeAgregarSubfamilia;
            btnAgregarPatente.Enabled = seleccionarExistente && puedeAgregarPatente;

            txtNombreFamilia.Visible = crearFamilia;
            btnCrearFamilia.Visible = crearFamilia;
            lblFamilia.Text = seleccionarExistente
                ? IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.FamiliaExistente")
                : IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.NombreNuevaFamilia");

            if (crearFamilia)
            {
                lblPatentes.Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.PatenteParaNuevaFamilia");
                bool patenteSeleccionada = lstPatentesDisponibles.SelectedItem is Patente_83KI;
                btnCrearFamilia.Enabled = patenteSeleccionada;
            }

            if (!seleccionarExistente)
            {
                treeFamilia.Nodes.Clear();
                if (crearFamilia)
                {
                    txtNombreFamilia.Focus();
                }
            }
            else
            {
                lblPatentes.Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.PatentesDisponibles");
                btnCrearFamilia.Enabled = true;
                CargarArbolFamilia();
            }
        }

        private void ActualizarBotonCrearFamilia()
        {
            if (!rdbCrearFamilia.Checked)
            {
                return;
            }

            bool patenteSeleccionada = lstPatentesDisponibles.SelectedItem is Patente_83KI;
            btnCrearFamilia.Enabled = patenteSeleccionada;
        }

        private void ActualizarBotonQuitar()
        {
            btnQuitarSeleccion.Visible = rdbSeleccionarFamilia.Checked && PermisosUi_83KI.TieneAlguno(
                PermisoSistema_83KI.QuitarPatenteFamilia,
                PermisoSistema_83KI.QuitarSubfamilia);
            btnQuitarSeleccion.Enabled = false;
            btnQuitarSeleccion.Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.SeleccionarPermisoOSubfamilia");

            if (treeFamilia.SelectedNode == null || treeFamilia.SelectedNode.Parent == null)
            {
                return;
            }

            if (treeFamilia.SelectedNode.Tag is Patente_83KI)
            {
                btnQuitarSeleccion.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.QuitarPatenteFamilia);
                btnQuitarSeleccion.Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.QuitarPatente");
                btnQuitarSeleccion.Enabled = btnQuitarSeleccion.Visible;
            }
            else if (treeFamilia.SelectedNode.Tag is Familia_83KI)
            {
                btnQuitarSeleccion.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.QuitarSubfamilia);
                btnQuitarSeleccion.Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.QuitarSubfamilia");
                btnQuitarSeleccion.Enabled = btnQuitarSeleccion.Visible;
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
                IdiomaUiHelper_83KI.MostrarError(this, ex, "FrmGestionFamilias.Titulo", MessageBoxIcon.Warning);
            }
        }

        public void ActualizarIdioma(IIdioma idioma)
        {
            Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.Titulo");
            rdbSeleccionarFamilia.Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.SeleccionarFamiliaExistente");
            rdbCrearFamilia.Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.CrearFamiliaModo");
            btnCrearFamilia.Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.CrearFamilia");
            btnEliminarFamilia.Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.EliminarFamilia");
            lblPatentes.Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.PatentesDisponibles");
            btnAgregarPatente.Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.AgregarPatente");
            lblFamiliasDisponibles.Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.FamiliasDisponibles");
            btnAgregarFamilia.Text = IdiomaUiHelper_83KI.Texto("FrmGestionFamilias.AgregarSubfamilia");
            ActualizarModoFamilia();
            ActualizarBotonQuitar();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _gestorIdioma.Desuscribir(this);
            base.OnFormClosed(e);
        }
    }
}
