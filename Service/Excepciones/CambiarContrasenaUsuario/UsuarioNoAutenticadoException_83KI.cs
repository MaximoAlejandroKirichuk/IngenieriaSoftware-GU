using System;

namespace Service.Excepciones.Login
{
    public class UsuarioNoAutenticadoException_83KI : Exception
    {
        public override string Message => "No hay un usuario autenticado para realizar esta operación.";
    }
}
