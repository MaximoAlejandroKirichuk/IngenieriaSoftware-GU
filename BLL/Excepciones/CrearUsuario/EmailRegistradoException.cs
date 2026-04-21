using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Excepciones.CrearUsuario
{
    public class EmailRegistradoException : Exception
    {
        public override string Message => "Email ya registrado a un usuario.";
    }
}
