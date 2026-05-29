using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Entidades
{
    public sealed class EventoBitacoraOpcion_83KI
    {
        private readonly string[] _prefijos;

        public EventoBitacoraOpcion_83KI(Modulo modulo, string nombre, params string[] prefijos)
        {
            Modulo = modulo;
            Nombre = nombre;
            _prefijos = prefijos == null || prefijos.Length == 0
                ? new[] { nombre }
                : prefijos;
        }

        public Modulo Modulo { get; private set; }
        public string Nombre { get; private set; }

        public bool CoincideConDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                return false;
            }

            string descripcionNormalizada = descripcion.Trim();
            return _prefijos.Any(prefijo => descripcionNormalizada.StartsWith(prefijo, StringComparison.OrdinalIgnoreCase));
        }

        public override string ToString()
        {
            return Nombre;
        }
    }

    public static class EventoBitacoraCatalogo_83KI
    {
        private static readonly List<EventoBitacoraOpcion_83KI> Eventos = new List<EventoBitacoraOpcion_83KI>
        {
            new EventoBitacoraOpcion_83KI(Modulo.Usuarios, "Login exitoso", "Login exitoso:"),
            new EventoBitacoraOpcion_83KI(Modulo.Usuarios, "Logout exitoso", "Logout exitoso:"),
            new EventoBitacoraOpcion_83KI(Modulo.Usuarios, "Usuario bloqueado", "Usuario bloqueado:"),
            new EventoBitacoraOpcion_83KI(Modulo.Usuarios, "Usuario bloqueado por intentos fallidos", "Usuario bloqueado por intentos fallidos:"),
            new EventoBitacoraOpcion_83KI(Modulo.Usuarios, "Contrase\u00f1a modificada", "Contrase\u00f1a modificada:", "Contrase\u00c3\u00b1a modificada:"),
            new EventoBitacoraOpcion_83KI(Modulo.Usuarios, "Usuario modificado", "Usuario modificado:"),
            new EventoBitacoraOpcion_83KI(Modulo.Usuarios, "Nuevo usuario creado", "Nuevo usuario creado:"),
            new EventoBitacoraOpcion_83KI(Modulo.Usuarios, "Usuario desbloqueado", "Usuario desbloqueado:"),
            new EventoBitacoraOpcion_83KI(Modulo.Usuarios, "Usuario habilitado", "Usuario habilitado:"),
            new EventoBitacoraOpcion_83KI(Modulo.Usuarios, "Usuario deshabilitado", "Usuario deshabilitado:")
        };

        public static IEnumerable<EventoBitacoraOpcion_83KI> ObtenerPorModulo(Modulo modulo)
        {
            return Eventos.Where(evento => evento.Modulo == modulo).ToList();
        }

        public static bool CoincideConEvento(string descripcion, string nombreEvento)
        {
            if (string.IsNullOrWhiteSpace(nombreEvento))
            {
                return true;
            }

            EventoBitacoraOpcion_83KI evento = Eventos.FirstOrDefault(e =>
                string.Equals(e.Nombre, nombreEvento.Trim(), StringComparison.OrdinalIgnoreCase));

            if (evento == null)
            {
                return (descripcion ?? string.Empty).IndexOf(nombreEvento.Trim(), StringComparison.OrdinalIgnoreCase) >= 0;
            }

            return evento.CoincideConDescripcion(descripcion);
        }
    }
}
