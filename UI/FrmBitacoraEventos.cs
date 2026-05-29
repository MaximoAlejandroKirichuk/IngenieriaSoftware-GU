using Service.DTOs;
using Service.Entidades;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UI.Exportacion;

namespace UI
{
    public partial class FrmBitacoraEventos : Form
    {
        private const string OpcionTodos = "Todos";
        private readonly IConsultaBitacoraEventos_83KI _consultaBitacoraEventos;
        private readonly IBitacoraEventosExporter_83KI _exporter;
        private readonly List<BitacoraEventoVista_83KI> _eventosVisibles = new List<BitacoraEventoVista_83KI>();
        private bool _actualizandoGrilla;

        public FrmBitacoraEventos(IConsultaBitacoraEventos_83KI consultaBitacoraEventos)
            : this(consultaBitacoraEventos, new BitacoraEventosPdfExporter_83KI())
        {
        }

        public FrmBitacoraEventos(IConsultaBitacoraEventos_83KI consultaBitacoraEventos, IBitacoraEventosExporter_83KI exporter)
        {
            InitializeComponent();
            _consultaBitacoraEventos = consultaBitacoraEventos;
            _exporter = exporter;
        }

        private void FrmBitacoraEventos_Load(object sender, EventArgs e)
        {
            ConfigurarFechas();
            CargarCombos();
            RestaurarFiltrosIniciales();
            CargarEventos();
        }

        private void ConfigurarFechas()
        {
            dtpFechaInicio.MaxDate = DateTime.Today;
            dtpFechaFin.MaxDate = DateTime.Today;
        }

        private void CargarCombos()
        {
            cmbModulo.Items.Clear();
            cmbModulo.Items.Add(OpcionTodos);
            cmbModulo.Items.Add(Modulo.Usuarios);
            cmbModulo.SelectedIndex = 0;
            CargarEventosPorModulo();

            cmbCriticidad.Items.Clear();
            cmbCriticidad.Items.Add(string.Empty);
            foreach (Criticidad criticidad in Enum.GetValues(typeof(Criticidad)))
            {
                cmbCriticidad.Items.Add(criticidad);
            }
            cmbCriticidad.SelectedIndex = 0;
        }

        private void RestaurarFiltrosIniciales()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtLogin.Clear();
            cmbModulo.SelectedIndex = 0;
            cmbEvento.SelectedIndex = 0;
            cmbCriticidad.SelectedIndex = 0;

            dtpFechaInicio.Value = DateTime.Today.AddDays(-3);
            dtpFechaFin.Value = DateTime.Today;
        }

        private void CargarEventosPorModulo()
        {
            cmbEvento.Items.Clear();
            cmbEvento.Items.Add(OpcionTodos);

            if (cmbModulo.SelectedItem is Modulo)
            {
                Modulo modulo = (Modulo)cmbModulo.SelectedItem;
                foreach (EventoBitacoraOpcion_83KI evento in EventoBitacoraCatalogo_83KI.ObtenerPorModulo(modulo))
                {
                    cmbEvento.Items.Add(evento);
                }
            }

            cmbEvento.SelectedIndex = 0;
        }

        private void CargarEventos()
        {
            FiltroBitacoraEventos_83KI filtro = ObtenerFiltroDesdeUI();
            if (filtro.FechaDesde.Date > DateTime.Today || filtro.FechaHasta.Date > DateTime.Today.AddDays(1).AddTicks(-1))
            {
                MessageBox.Show("No se pueden seleccionar fechas futuras.");
                RestaurarFechasValidas();
                return;
            }

            if (filtro.FechaDesde.Date > filtro.FechaHasta.Date)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha fin.");
                return;
            }

            _eventosVisibles.Clear();
            _eventosVisibles.AddRange(_consultaBitacoraEventos.Consultar(filtro));

            
            ActualizarDataGridView();
            
        }

        private FiltroBitacoraEventos_83KI ObtenerFiltroDesdeUI()
        {
            DateTime desde = dtpFechaInicio.Value.Date;
            DateTime hasta = dtpFechaFin.Value.Date;

            return new FiltroBitacoraEventos_83KI
            {
                FechaDesde = desde,
                FechaHasta = hasta.AddDays(1).AddTicks(-1),
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Username = txtLogin.Text,
                Evento = ObtenerTextoCombo(cmbEvento),
                Modulo = cmbModulo.SelectedItem is Modulo ? (Modulo?)cmbModulo.SelectedItem : null,
                Criticidad = cmbCriticidad.SelectedItem is Criticidad ? (Criticidad?)cmbCriticidad.SelectedItem : null
            };
        }

        private string ObtenerTextoCombo(ComboBox combo)
        {
            if (combo.SelectedItem == null || string.Equals(combo.SelectedItem.ToString(), OpcionTodos, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return combo.SelectedItem.ToString();
        }

        private void RestaurarFechasValidas()
        {
            if (dtpFechaInicio.Value.Date > DateTime.Today)
            {
                dtpFechaInicio.Value = DateTime.Today;
            }

            if (dtpFechaFin.Value.Date > DateTime.Today)
            {
                dtpFechaFin.Value = DateTime.Today;
            }
        }

        private void ActualizarDataGridView()
        {
            _actualizandoGrilla = true;
            dgvEventos.DataSource = null;
            dgvEventos.DataSource = _eventosVisibles.ToList();
            ConfigurarGrilla();
            dgvEventos.ClearSelection();
            dgvEventos.CurrentCell = null;
            _actualizandoGrilla = false;
        }
        #region Configrar grilla
        private void ConfigurarGrilla()
        {
            if (dgvEventos.Columns["Id"] != null)
            {
                dgvEventos.Columns["Id"].DisplayIndex = 0;
                dgvEventos.Columns["Id"].Width = 55;
            }

            ConfigurarColumna("Fecha", "Fecha", 125);
            ConfigurarColumna("Username", "Username", 110);
            ConfigurarColumna("Nombre", "Nombre", 110);
            ConfigurarColumna("Apellido", "Apellido", 110);
            ConfigurarColumna("Modulo", "Modulo", 95);
            ConfigurarColumna("Criticidad", "Criticidad", 95);
            ConfigurarColumna("Evento", "Evento", 290);
        }

        private void ConfigurarColumna(string nombre, string titulo, int ancho)
        {
            if (dgvEventos.Columns[nombre] == null)
            {
                return;
            }

            dgvEventos.Columns[nombre].HeaderText = titulo;
            dgvEventos.Columns[nombre].Width = ancho;
        }

        #endregion

        private void dgvEventos_SelectionChanged(object sender, EventArgs e)
        {
            if (_actualizandoGrilla)
            {
                return;
            }

            BitacoraEventoVista_83KI evento = ObtenerEventoSeleccionado();
            if (evento == null)
            {
                return;
            }

            txtNombre.Text = evento.Nombre;
            txtApellido.Text = evento.Apellido;
            txtLogin.Text = evento.Username;
        }

        private BitacoraEventoVista_83KI ObtenerEventoSeleccionado()
        {
            return dgvEventos.CurrentRow == null ? null : dgvEventos.CurrentRow.DataBoundItem as BitacoraEventoVista_83KI;
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            CargarEventos();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            RestaurarFiltrosIniciales();
            CargarEventos();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (_eventosVisibles.Count == 0)
            {
                MessageBox.Show("No hay eventos para exportar.");
                return;
            }

            string mensaje;
            if (!_exporter.PuedeExportar(out mensaje))
            {
                MessageBox.Show(mensaje);
                return;
            }

            using (SaveFileDialog dialogo = new SaveFileDialog())
            {
                dialogo.Title = "Exportar bitacora de eventos";
                dialogo.Filter = "Archivo PDF (*.pdf)|*.pdf";
                dialogo.FileName = "BitacoraEventos_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".pdf";
                dialogo.DefaultExt = "pdf";
                dialogo.AddExtension = true;
                dialogo.OverwritePrompt = true;

                if (dialogo.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    _exporter.Exportar(_eventosVisibles, dialogo.FileName);
                    MessageBox.Show("PDF exportado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo exportar el PDF. " + ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarEventosPorModulo();
        }
    }
}
