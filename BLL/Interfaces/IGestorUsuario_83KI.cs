using BE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGestorUsuario_83KI
    {
        void Login(string email, string contrasena);
        void Logout();
        void CrearUsuario(Usuario_83KI usuario);
        void CambiarContrasenaUsuarioActual(string contrasenaActual, string nuevaContrasena);
        void ModificarRolUsuario(RolUsuario Rol);
        void BloqueoCuentaUsuario(Usuario_83KI usuario);
        void DesbloquearCuenta(Usuario_83KI usuario);
        IEnumerable<Usuario_83KI> ObtenerUsuarios();
    }
}
