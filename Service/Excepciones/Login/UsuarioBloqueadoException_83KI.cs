using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Excepciones
{
    public class UsuarioBloqueadoException_83KI : Exception
    {
        public override string Message => "La cuenta está bloqueada. Contacte a un administrador.";
    }
}
