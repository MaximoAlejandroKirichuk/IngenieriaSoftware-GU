using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGestorUsuario
    {
        void Login(string email, string contrasena);
        void Logout();
        void CambioContrasena(string email, string contrasenaActual, string nuevaContrasena);
        void ModificarRolUsuario(RolUsuario Rol);
        void BloqueoCuentaUsuario(Usuario usuario);
    }
}
