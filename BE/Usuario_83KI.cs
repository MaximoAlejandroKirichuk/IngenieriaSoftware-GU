using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Usuario_83KI
    {
        //pk
        public int DNI { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public RolUsuario RolUsuario { get; set; }
        public string Contrasena { get; set; }
        public bool Activo { get; set; } = true;
        public bool Bloqueado { get; set; }
        public int Intentos { get; set; }
        public string Email { get; set; }

        public string UserName => $"{DNI}{Nombre}";
    }
}
