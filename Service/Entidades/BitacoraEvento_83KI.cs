using System;

namespace Service.Entidades
{
    public class BitacoraEvento_83KI
    {
        public int Id { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Descripcion { get; private set; }
        public int Criticidad { get; private set; } // 1 a 5 como en el Excel
        public Modulo Modulo { get; private set; }    // "Usuario"
        public string Username { get; private set; }

        private BitacoraEvento_83KI()
        {
        }

        public static BitacoraEvento_83KI CrearNuevo(string descripcion, int criticidad, Modulo modulo, string username)
        {
            return new BitacoraEvento_83KI
            {
                Fecha = DateTime.UtcNow,
                Descripcion = ValidarTextoObligatorio(descripcion, nameof(descripcion)),
                Criticidad = ValidarCriticidad(criticidad),
                Modulo = ValidarModulo(modulo),
                Username = ValidarTextoObligatorio(username, nameof(username))
            };
        }

        public static BitacoraEvento_83KI ReconstruirDesdePersistencia(
            int id,
            DateTime fecha,
            string descripcion,
            int criticidad,
            Modulo modulo,
            string username)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "El id de bitacora no puede ser negativo.");
            }

            return new BitacoraEvento_83KI
            {
                Id = id,
                Fecha = fecha,
                Descripcion = ValidarTextoObligatorio(descripcion, nameof(descripcion)),
                Criticidad = ValidarCriticidad(criticidad),
                Modulo = ValidarModulo(modulo),
                Username = ValidarTextoObligatorio(username, nameof(username))
            };
        }

        private static string ValidarTextoObligatorio(string valor, string nombreParametro)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new ArgumentException("El valor es obligatorio.", nombreParametro);
            }

            return valor.Trim();
        }

        private static int ValidarCriticidad(int criticidad)
        {
            if (criticidad < 1 || criticidad > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(criticidad), "La criticidad debe estar entre 1 y 5.");
            }

            return criticidad;
        }

        private static Modulo ValidarModulo(Modulo modulo)
        {
            if (!Enum.IsDefined(typeof(Modulo), modulo))
            {
                throw new ArgumentException("El modulo no es valido.", nameof(modulo));
            }

            return modulo;
        }
    }
}
