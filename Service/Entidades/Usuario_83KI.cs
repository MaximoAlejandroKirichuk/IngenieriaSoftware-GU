using System;
using System.Text.RegularExpressions;

namespace Service.Entidades
{
    public class Usuario_83KI
    {
        private const int DniMinimoValido = 1000000;
        private const string PatronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        //pk
        public int DNI { get; private set; }
        public string Apellido { get; private set; }
        public string Nombre { get; private set; }
        public RolUsuario RolUsuario { get; private set; }
        public string Contrasena { get; private set; }
        public bool Activo { get; private set; }
        public bool Bloqueado { get; private set; }
        public string Email { get; private set; }

        public string UserName => $"{DNI}{Nombre}";

        private Usuario_83KI()
        {
        }

        public static Usuario_83KI CrearNuevo(string nombre, string apellido, int dni, string email, RolUsuario rolUsuario, string contrasenaHash)
        {
            return new Usuario_83KI
            {
                DNI = ValidarDni(dni),
                Nombre = ValidarTextoObligatorio(nombre, nameof(nombre)),
                Apellido = ValidarTextoObligatorio(apellido, nameof(apellido)),
                Email = ValidarEmail(email),
                RolUsuario = ValidarRol(rolUsuario),
                Contrasena = ValidarContrasena(contrasenaHash),
                Activo = true,
                Bloqueado = false,
            };
        }

        public static Usuario_83KI ReconstruirDesdePersistencia(
            int dni,
            string nombre,
            string apellido,
            string email,
            string contrasenaHash,
            RolUsuario rolUsuario,
            bool activo,
            bool bloqueado
            )
        {

            return new Usuario_83KI
            {
                DNI = ValidarDni(dni),
                Nombre = ValidarTextoObligatorio(nombre, nameof(nombre)),
                Apellido = ValidarTextoObligatorio(apellido, nameof(apellido)),
                Email = ValidarEmail(email),
                Contrasena = ValidarContrasena(contrasenaHash),
                RolUsuario = ValidarRol(rolUsuario),
                Activo = activo,
                Bloqueado = bloqueado,
            };
        }

        public void Bloquear()
        {
            Bloqueado = true;
        }

        public void Desbloquear(string contrasenaHash)
        {
            CambiarContrasena(contrasenaHash);
            Bloqueado = false;
        }

        public static string EstablecerContrasenaPorDefecto(string apellido, int dni)
        {
            string apellidoNormalizado = ValidarTextoObligatorio(apellido, nameof(apellido));
            int dniValidado = ValidarDni(dni);

            return $"{dniValidado}{apellidoNormalizado}";
        }

        public void CambiarContrasena(string contrasenaHash)
        {
            Contrasena = ValidarContrasena(contrasenaHash);
        }

        public void ModificarEmailYRol(string email, RolUsuario rolUsuario)
        {
            Email = ValidarEmail(email);
            RolUsuario = ValidarRol(rolUsuario);
        }

        public void ModificarEmail(string email)
        {
            Email = ValidarEmail(email);
        }

        public void Habilitar()
        {
            Activo = true;
        }

        public void Deshabilitar()
        {
            Activo = false;
        }

        private static int ValidarDni(int dni)
        {
            if (dni < DniMinimoValido)
            {
                throw new ArgumentException("El DNI debe ser un numero valido.", nameof(dni));
            }

            return dni;
        }

        private static string ValidarTextoObligatorio(string valor, string nombreParametro)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new ArgumentException("El valor es obligatorio.", nombreParametro);
            }

            return valor.Trim();
        }

        private static string ValidarEmail(string email)
        {
            string emailNormalizado = ValidarTextoObligatorio(email, nameof(email));

            if (!Regex.IsMatch(emailNormalizado, PatronEmail))
            {
                throw new ArgumentException("El formato del email no es valido.", nameof(email));
            }

            return emailNormalizado;
        }

        private static string ValidarContrasena(string contrasenaHash)
        {
            //luki: el name of ayuda a que las excepciones digan que parametro fallo.
            return ValidarTextoObligatorio(contrasenaHash, nameof(contrasenaHash));
        }

        private static RolUsuario ValidarRol(RolUsuario rolUsuario)
        {
            if (!Enum.IsDefined(typeof(RolUsuario), rolUsuario))
            {
                throw new ArgumentException("El rol de usuario no es valido.", nameof(rolUsuario));
            }

            return rolUsuario;
        }
    }
}
