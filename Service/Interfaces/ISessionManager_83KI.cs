using BE;

namespace Service.Interfaces
{
    internal interface ISessionManager_83KI
    {
        void IniciarSesion(Usuario_83KI usuario);
        void CerrarSesion();
        Usuario_83KI UsuarioActivo { get; }
    }
}
