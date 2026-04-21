using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Excepciones.Login
{
    public class UsuarioNoExisteException_83KI : Exception
    {
        public override string Message => "El usuario no existe";
    }
}
