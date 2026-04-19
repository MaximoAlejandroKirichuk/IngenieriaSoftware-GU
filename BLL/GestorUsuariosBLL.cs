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
using Microsoft.Win32;
namespace BLL
{
    public class GestorUsuarioBLL : IGestorUsuario
    {
        private readonly IUsuarioDAL _dal;
        private readonly IEncriptador _encriptador;
        private readonly ISessionManager _sessionManager;
        private readonly IBitacoraManager _bitacora;
        private const int NumeroIntentos = 3;
        public GestorUsuarioBLL(IUsuarioDAL dal, IEncriptador encriptador, ISessionManager sessionManager, IBitacoraManager bitacora)
        {
            _dal = dal;
            _encriptador = encriptador;
            _sessionManager = sessionManager;
            _bitacora = bitacora;
        }

        public void Login(string email, string contrasena)
        {
            var usuario = _dal.ObtenerPorMail(email);

            if (_sessionManager.UsuarioActivo != null)
                throw new UsuarioActivoActualmenteException();

            if (usuario == null)
            {
                _bitacora.RegistrarEvento($"Intento de login fallido: {email} no existe", 2, "Seguridad", email);
                throw new UsuarioNoExisteException();
            }
            string hash = _encriptador.HashContrasena(contrasena);

            if (usuario.Contrasena != hash)
            {
                usuario.Intentos++;
                if (usuario.Intentos >= NumeroIntentos)
                {
                    usuario.Bloqueado = true;
                    _dal.BloquearUsuario(usuario);
                    // REGISTRO DE BLOQUEO (Criticidad 1)
                    _bitacora.RegistrarEvento($"Usuario bloqueado: {email}", 1, "Usuario", email);
                    throw new UsuarioBloqueadoException();
                }

                _dal.ActualizarIntentos(usuario);
                throw new ContrasenaInvalidaException($"Intento {usuario.Intentos} de 3.");
            }

            // LOGIN EXITOSO
            _sessionManager.IniciarSesion(usuario);
            usuario.Intentos = 0; // Reseteamos intentos
            _dal.ActualizarIntentos(usuario);
            //REGISTRO DE LOGIN(Criticidad 1)
            _bitacora.RegistrarEvento($"Login exitoso: {usuario.Email}", 1, "Usuario", email);
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
