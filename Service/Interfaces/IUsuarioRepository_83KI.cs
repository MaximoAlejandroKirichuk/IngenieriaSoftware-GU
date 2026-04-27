using BE;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IUsuarioRepository_83KI
    {
        Usuario_83KI ObtenerPorUserName(string userName);
        void ActualizarIntentos(Usuario_83KI usuario);
        void BloquearUsuario(Usuario_83KI usuario);
        void CrearUsuario(Usuario_83KI usuario);
        bool ExisteDni(int dni);
        bool ExisteEmail(string email);
        bool EstaBloqueado(int dni);
        void DesbloquearCuenta(Usuario_83KI usuario);
        IEnumerable<Usuario_83KI> ObtenerUsuarios();
    }
}
