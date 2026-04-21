using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Excepciones.Login
{
    public class UsuarioActivoActualmenteException_83KI : Exception
    {
        public override string Message => "El usuario ya inicio sesion en este dispositivo";
    }
}
