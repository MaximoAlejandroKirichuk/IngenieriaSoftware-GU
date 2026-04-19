using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Excepciones.Login
{
    public class ContrasenaInvalidaException : Exception
    {
        private readonly string _mensajeIntentos;
        public ContrasenaInvalidaException(string mensajeIntentos)
        {
            _mensajeIntentos = mensajeIntentos;
        }
        public override string Message => $"La contraseña es invalida: {_mensajeIntentos}";
    }
}
