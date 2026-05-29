using System;

namespace Service.Excepciones.Login
{
    public class UsuarioDeshabilitadoException_83KI : Exception
    {
        public override string Message => "El usuario se encuentra deshabilitado. Contacte a un administrador.";
    }
}
