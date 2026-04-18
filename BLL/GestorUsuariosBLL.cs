using BE;
using BLL.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.interfaces;
using DAL;
namespace BLL
{
    public class GestorUsuarioBLL : IGestorUsuario
    {
        private readonly IUsuarioDAL _dal;
        private readonly IEncriptador _encriptador;
        public GestorUsuarioBLL(IUsuarioDAL dal, IEncriptador encriptador, )
        {
            _dal = dal;
            _encriptador = encriptador;
        }

        public void Login(string email, string contrasena)
        {
            string hash = _encriptador.HashContrasena(contrasena);
            var usuario = _dal.OptenerPorMail(email);

            if (usuario != null && usuario.Contrasena == hash)
            {
                SessionManager.Login(usuario);
            }
            else
            {
                throw new Exception("Credenciales inválidas");
            }
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void CambioContrasena(string email, string contrasenaActual, string nuevaContrasena)
        {
            throw new NotImplementedException();
        }

        public void ModificarRolUsuario(RolUsuario Rol)
        {
            throw new NotImplementedException();
        }

        public void BloqueoCuentaUsuario()
        {
            throw new NotImplementedException();
        }
    }
}
