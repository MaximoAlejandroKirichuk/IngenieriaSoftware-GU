using Service.Entidades;
using Service.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmGestionRoles_83KI : Form
    {
        private readonly IGestorRol_83KI _gestorRol;

        public FrmGestionRoles_83KI(IGestorRol_83KI gestorRol)
        {
            InitializeComponent();
            _gestorRol = gestorRol;
        }

        private void FrmGestionRoles_83KI_Load(object sender, EventArgs e)
        {
            AplicarPermisos();
            CargarDatos();
        }

        private void AplicarPermisos()
        {
            bool puedeVerRoles = PermisosUi_83KI.Tiene(PermisoSistema_83KI.VerRoles);
            bool puedeVerPermisosEfectivos = PermisosUi_83KI.Tiene(PermisoSistema_83KI.VerPermisosEfectivosRol);
            bool puedeAgregarFamilia = PermisosUi_83KI.Tiene(PermisoSistema_83KI.AgregarFamiliaRol);
            bool puedeAsignarPatente = PermisosUi_83KI.Tiene(PermisoSistema_83KI.AsignarPatenteRol);
            bool puedeGestionarRoles = PermisosUi_83KI.Tiene(PermisoSistema_83KI.GestionRoles);

            lblRoles.Visible = puedeVerRoles;
            lstRoles.Visible = puedeVerRoles;
            txtNombreRol.Visible = puedeGestionarRoles;
            btnCrearRol.Visible = puedeGestionarRoles;
            lblFamiliasRol.Visible = puedeVerRoles;
            lstFamiliasRol.Visible = puedeVerRoles;
            lblPatentesFamilia.Visible = puedeVerPermisosEfectivos;
            treeContenidoFamilia.Visible = puedeVerPermisosEfectivos;
            lblPatentesRol.Visible = puedeVerPermisosEfectivos;
            lstPatentesRol.Visible = puedeVerPermisosEfectivos;
            cmbFamiliasDisponibles.Visible = puedeAgregarFamilia;
            btnAgregarFamilia.Visible = puedeAgregarFamilia;
            btnQuitarFamilia.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.QuitarFamiliaRol);
            cmbPatentesDisponibles.Visible = puedeAsignarPatente;
            btnAsignarPatente.Visible = puedeAsignarPatente;
            btnQuitarPatente.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.QuitarPatenteRol);
        }

        private void CargarDatos()
        {
            if (!PermisosUi_83KI.Tiene(PermisoSistema_83KI.VerRoles))
            {
                lstRoles.DataSource = null;
                lstFamiliasRol.DataSource = null;
                treeContenidoFamilia.Nodes.Clear();
                lstPatentesRol.DataSource = null;
                return;
            }

            lstRoles.DataSource = null;
            lstRoles.DisplayMember = nameof(Rol_83KI.Nombre);
            lstRoles.ValueMember = nameof(Rol_83KI.CodigoRol);
            lstRoles.DataSource = _gestorRol.ObtenerRolesConPermisos().ToList();

            CargarFamiliasDisponibles();
            CargarPatentesDisponibles();
            CargarFamiliasDelRol();
        }

        private Rol_83KI ObtenerRolSeleccionado()
        {
            return lstRoles.SelectedItem as Rol_83KI;
        }

        private Familia_83KI ObtenerFamiliaSeleccionada()
        {
            return lstFamiliasRol.SelectedItem as Familia_83KI;
        }

        private Patente_83KI ObtenerPatenteSeleccionadaDelRol()
        {
            return lstPatentesRol.SelectedItem as Patente_83KI;
        }

        private void lstRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFamiliasDelRol();
            CargarPatentesDelRol();
        }

        private void lstFamiliasRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarContenidoFamilia();
        }

        private void CargarFamiliasDelRol()
        {
            Rol_83KI rol = ObtenerRolSeleccionado();
            lstFamiliasRol.DataSource = null;
            lstFamiliasRol.DisplayMember = nameof(Familia_83KI.Nombre);
            lstFamiliasRol.ValueMember = nameof(Familia_83KI.CodigoFamilia);
            lstFamiliasRol.DataSource = rol == null
                ? null
                : rol.Hijos.OfType<Familia_83KI>().OrderBy(f => f.Nombre).ToList();

            CargarContenidoFamilia();
            CargarPatentesDelRol();
        }

        private void CargarPatentesDelRol()
        {
            lstPatentesRol.DataSource = null;

            if (!PermisosUi_83KI.Tiene(PermisoSistema_83KI.VerPermisosEfectivosRol))
            {
                return;
            }

            Rol_83KI rol = ObtenerRolSeleccionado();
            lstPatentesRol.DisplayMember = nameof(Patente_83KI.Nombre);
            lstPatentesRol.ValueMember = nameof(Patente_83KI.CodigoPatente);
            lstPatentesRol.DataSource = rol == null
                ? null
                : rol.ObtenerPatentes().OrderBy(p => p.Nombre).ToList();
        }

        private void CargarContenidoFamilia()
        {
            treeContenidoFamilia.Nodes.Clear();

            if (!PermisosUi_83KI.Tiene(PermisoSistema_83KI.VerPermisosEfectivosRol))
            {
                return;
            }

            Familia_83KI familia = ObtenerFamiliaSeleccionada();

            if (familia == null)
            {
                return;
            }

            TreeNode raiz = CrearNodo(familia);
            treeContenidoFamilia.Nodes.Add(raiz);
            raiz.ExpandAll();
        }

        private TreeNode CrearNodo(ComponentePermiso_83KI componente)
        {
            TreeNode nodo = new TreeNode(componente.Nombre) { Tag = componente };

            foreach (ComponentePermiso_83KI hijo in componente.Hijos.OrderBy(h => h.Nombre))
            {
                nodo.Nodes.Add(CrearNodo(hijo));
            }

            return nodo;
        }

        private void CargarFamiliasDisponibles()
        {
            cmbFamiliasDisponibles.DataSource = null;
            cmbFamiliasDisponibles.DisplayMember = nameof(Familia_83KI.Nombre);
            cmbFamiliasDisponibles.ValueMember = nameof(Familia_83KI.CodigoFamilia);
            cmbFamiliasDisponibles.DataSource = _gestorRol.ObtenerFamilias().ToList();
        }

        private void CargarPatentesDisponibles()
        {
            cmbPatentesDisponibles.DataSource = null;
            cmbPatentesDisponibles.DisplayMember = nameof(Patente_83KI.Nombre);
            cmbPatentesDisponibles.ValueMember = nameof(Patente_83KI.CodigoPatente);
            cmbPatentesDisponibles.DataSource = _gestorRol.ObtenerPatentes().ToList();
        }

        private void btnCrearRol_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreRol.Text.Trim();

            EjecutarOperacion(() =>
            {
                _gestorRol.CrearRol(nombre);
                txtNombreRol.Clear();
            });
        }

        private void btnAgregarFamilia_Click(object sender, EventArgs e)
        {
            Rol_83KI rol = ObtenerRolSeleccionado();
            Familia_83KI familia = cmbFamiliasDisponibles.SelectedItem as Familia_83KI;

            if (rol == null || familia == null)
            {
                return;
            }

            EjecutarOperacion(() => _gestorRol.AsignarFamiliaARol(rol.CodigoRol, familia.CodigoFamilia));
        }

        private void btnAsignarPatente_Click(object sender, EventArgs e)
        {
            Rol_83KI rol = ObtenerRolSeleccionado();
            Patente_83KI patente = cmbPatentesDisponibles.SelectedItem as Patente_83KI;

            if (rol == null || patente == null)
            {
                return;
            }

            EjecutarOperacion(() => _gestorRol.AsignarPatenteARol(rol.CodigoRol, patente.CodigoPatente));
        }

        private void btnQuitarPatente_Click(object sender, EventArgs e)
        {
            Rol_83KI rol = ObtenerRolSeleccionado();
            Patente_83KI patente = ObtenerPatenteSeleccionadaDelRol();

            if (rol == null || patente == null)
            {
                return;
            }

            bool patenteDirecta = rol.Hijos.OfType<Patente_83KI>()
                .Any(p => p.CodigoPatente == patente.CodigoPatente);

            if (!patenteDirecta)
            {
                MessageBox.Show(
                    "La patente seleccionada pertenece a una familia del rol. Para quitarla, quite la familia o modifique su contenido.",
                    "Gestion de roles",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            EjecutarOperacion(() => _gestorRol.QuitarPatenteDeRol(rol.CodigoRol, patente.CodigoPatente));
        }

        private void btnQuitarFamilia_Click(object sender, EventArgs e)
        {
            Rol_83KI rol = ObtenerRolSeleccionado();
            Familia_83KI familia = ObtenerFamiliaSeleccionada();

            if (rol == null || familia == null)
            {
                return;
            }

            EjecutarOperacion(() => _gestorRol.QuitarFamiliaDeRol(rol.CodigoRol, familia.CodigoFamilia));
        }

        private void EjecutarOperacion(Action operacion)
        {
            try
            {
                operacion();
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Gestion de roles", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
