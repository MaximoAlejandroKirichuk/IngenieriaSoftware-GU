using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Excepciones.CrearUsuario
{
    public class DniRegistradoException_83KI: Exception
    {
        public override string Message => "El DNI ya esta registrado a un usuario";
    }
}
