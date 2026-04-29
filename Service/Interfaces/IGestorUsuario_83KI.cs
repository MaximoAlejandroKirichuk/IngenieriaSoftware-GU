using Service.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IGestorUsuario_83KI
    {
        void Login(string email, string contrasena);
        void Logout();
        void CrearUsuario(Usuario_83KI usuario);
        void ModificarUsuario(int dni, string email, RolUsuario rol);
        void CambiarContrasenaUsuarioActual(string contrasenaActual, string nuevaContrasena);
        void ModificarRolUsuario(RolUsuario Rol);
        void BloqueoCuentaUsuario(Usuario_83KI usuario);
        void DesbloquearCuenta(Usuario_83KI usuario);
        void HabilitarUsuario(int dni);
        void DeshabilitarUsuario(int dni);
        IEnumerable<Usuario_83KI> ObtenerUsuarios();
    }
}
