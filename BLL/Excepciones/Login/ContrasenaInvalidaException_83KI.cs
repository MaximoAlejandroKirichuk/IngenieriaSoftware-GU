using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Excepciones.Login
{
    public class ContrasenaInvalidaException_83KI : Exception
    {
        private readonly string _mensajeIntentos;
        public ContrasenaInvalidaException_83KI(string mensajeIntentos)
        {
            _mensajeIntentos = mensajeIntentos;
        }
        public override string Message => $"La contraseña es invalida: {_mensajeIntentos}";
    }
}
