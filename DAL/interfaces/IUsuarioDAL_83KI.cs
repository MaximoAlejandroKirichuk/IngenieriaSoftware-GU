using Service.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.interfaces
{
    public interface IUsuarioDAL_83KI
    {
        Usuario_83KI ObtenerPorDni(int dni);
        Usuario_83KI ObtenerPorUserName(string userName);
        void ActualizarIntentos(Usuario_83KI usuario); // Incrementa o resetea a 0
        void BloquearUsuario(Usuario_83KI usuario);
        void CrearUsuario(Usuario_83KI usuario);
        void ModificarUsuario(int dni, string email, RolUsuario rol);
        void ActualizarContrasena(Usuario_83KI usuario);
        bool ExisteDni(int dni);
        bool ExisteEmail(string email);
        bool ExisteEmailParaOtroUsuario(string email, int dni);
        bool EstaBloqueado(int dni);
        void ActualizarEstadoActivo(int dni, bool activo);
        void DesbloquearCuenta(Usuario_83KI usuario);
        IEnumerable<Usuario_83KI> ObtenerUsuarios();
    }
}
