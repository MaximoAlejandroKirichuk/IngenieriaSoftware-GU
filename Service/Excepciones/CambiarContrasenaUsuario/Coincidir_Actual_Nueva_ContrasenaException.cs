using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Excepciones.CambiarContrasenaUsuario
{
    public class Coincidir_Actual_Nueva_ContrasenaException : Exception
    {
        public override string Message => "La nueva contraseña y su confirmación deben coincidir.";
    }
}
