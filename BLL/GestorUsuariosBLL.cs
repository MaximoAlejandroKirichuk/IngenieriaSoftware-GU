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
using BLL.Excepciones.Login;
namespace BLL
{
    public class GestorUsuarioBLL : IGestorUsuario
    {
        private readonly IUsuarioDAL _dal;
        private readonly IEncriptador _encriptador;
        private readonly ISessionManager _sessionManager;
        public GestorUsuarioBLL(IUsuarioDAL dal, IEncriptador encriptador, ISessionManager sessionManager)
        {
            _dal = dal;
            _encriptador = encriptador;
            _sessionManager = sessionManager;
        }

        public void Login(string email, string contrasena)
        {
            string hash = _encriptador.HashContrasena(contrasena);
            var usuario = _dal.ObtenerPorMail(email);
            
            if (usuario == null)
                throw new UsuarioNoExisteException();
            
            if (usuario.Contrasena != hash)
            //TODO: falta agregar el contador de errores
                throw new ContrasenaInvalidaException();
            _sessionManager.IniciarSesion(usuario);

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
