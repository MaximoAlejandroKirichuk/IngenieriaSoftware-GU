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

            lblRoles.Visible = puedeVerRoles;
            lstRoles.Visible = puedeVerRoles;
            lblFamiliasRol.Visible = puedeVerRoles;
            lstFamiliasRol.Visible = puedeVerRoles;
            lblPatentesFamilia.Visible = puedeVerPermisosEfectivos;
            lstPatentesFamilia.Visible = puedeVerPermisosEfectivos;
            cmbFamiliasDisponibles.Visible = puedeAgregarFamilia;
            btnAgregarFamilia.Visible = puedeAgregarFamilia;
            btnQuitarFamilia.Visible = PermisosUi_83KI.Tiene(PermisoSistema_83KI.QuitarFamiliaRol);
        }

        private void CargarDatos()
        {
            if (!PermisosUi_83KI.Tiene(PermisoSistema_83KI.VerRoles))
            {
                lstRoles.DataSource = null;
                lstFamiliasRol.DataSource = null;
                lstPatentesFamilia.DataSource = null;
                return;
            }

            lstRoles.DataSource = null;
            lstRoles.DisplayMember = nameof(Rol_83KI.Nombre);
            lstRoles.ValueMember = nameof(Rol_83KI.CodigoRol);
            lstRoles.DataSource = _gestorRol.ObtenerRolesConPermisos().ToList();

            CargarFamiliasDisponibles();
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

        private void lstRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFamiliasDelRol();
        }

        private void lstFamiliasRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPatentesDeFamilia();
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

            CargarPatentesDeFamilia();
        }

        private void CargarPatentesDeFamilia()
        {
            if (!PermisosUi_83KI.Tiene(PermisoSistema_83KI.VerPermisosEfectivosRol))
            {
                lstPatentesFamilia.DataSource = null;
                return;
            }

            Familia_83KI familia = ObtenerFamiliaSeleccionada();
            lstPatentesFamilia.DataSource = null;
            lstPatentesFamilia.DisplayMember = nameof(Patente_83KI.Nombre);
            lstPatentesFamilia.ValueMember = nameof(Patente_83KI.CodigoPatente);
            lstPatentesFamilia.DataSource = familia == null
                ? null
                : familia.ObtenerPatentes().OrderBy(p => p.Nombre).ToList();
        }

        private void CargarFamiliasDisponibles()
        {
            cmbFamiliasDisponibles.DataSource = null;
            cmbFamiliasDisponibles.DisplayMember = nameof(Familia_83KI.Nombre);
            cmbFamiliasDisponibles.ValueMember = nameof(Familia_83KI.CodigoFamilia);
            cmbFamiliasDisponibles.DataSource = _gestorRol.ObtenerFamilias().ToList();
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
