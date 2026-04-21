using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Excepciones.CrearUsuario
{
    public class EmailRegistradoException_83KI : Exception
    {
        public override string Message => "Email ya registrado a un usuario.";
    }
}
