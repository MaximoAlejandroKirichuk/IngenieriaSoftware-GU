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
            if (_sessionManager.UsuarioActivo != null)
                throw new UsuarioActivoActualmenteException();
            
            var usuario = _dal.ObtenerPorMail(email);

            if (usuario == null)
            {
                _bitacora.RegistrarEvento($"Intento de login fallido: {email} no existe", 2, Modulo.Seguridad, email);
                throw new UsuarioNoExisteException();
            }

            string hash = _encriptador.HashContrasena(contrasena);

            if (usuario.Contrasena != hash)
            {
                usuario.Intentos++;
                if (usuario.Intentos >= NumeroIntentos)
                {
                    BloqueoCuentaUsuario(usuario);
                }

                _dal.ActualizarIntentos(usuario);
                throw new ContrasenaInvalidaException($"Intento {usuario.Intentos} de 3.");
            }

            // LOGIN EXITOSO
            _sessionManager.IniciarSesion(usuario);
            usuario.Intentos = 0; // Reseteamos intentos
            _dal.ActualizarIntentos(usuario);
            //REGISTRO DE LOGIN(Criticidad 1)
            _bitacora.RegistrarEvento($"Login exitoso: {usuario.Email}", 1, Modulo.Usuarios, email);
        }
        public void Logout()
        {
            var usuario = _sessionManager.UsuarioActivo;
            if (usuario != null)
            {
                _sessionManager.CerrarSesion();
                _bitacora.RegistrarEvento($"Login exitoso: {usuario.Email}", 1, Modulo.Usuarios, usuario.Email);
            }
        }

        public void CambioContrasena(string email, string contrasenaActual, string nuevaContrasena)
        {
            throw new NotImplementedException();
        }

        public void ModificarRolUsuario(RolUsuario Rol)
        {
            throw new NotImplementedException();
        }

        public void BloqueoCuentaUsuario(Usuario usuario)
        {
            usuario.Bloqueado = true;
            _dal.BloquearUsuario(usuario);
            // REGISTRO DE BLOQUEO (Criticidad 1)
            _bitacora.RegistrarEvento($"Usuario bloqueado: {usuario.Email}", 1, Modulo.Usuarios, usuario.Email);
            throw new UsuarioBloqueadoException();
        }

        public void CrearUsuario(Usuario usuario)
        {
            //validaciones de que no puede haber una persona con el mismo mail y/o dni
            _dal.CrearUsuario(usuario);
            _bitacora.RegistrarEvento($"Usuario bloqueado: {usuario.Email}", 1, Modulo.Usuarios, usuario.Email);
        }
    }
}
