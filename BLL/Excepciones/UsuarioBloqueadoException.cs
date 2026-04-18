using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Excepciones
{
    public class UsuarioBloqueadoException : Exception
    {
        public override string Message => "La cuenta está bloqueada. Contacte a un administrador.";
    }
}
