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
using BLL.Excepciones.CrearUsuario;
namespace BLL
{
    public class GestorUsuarioBLL_83KI : IGestorUsuario_83KI
    {
        private readonly IUsuarioDAL_83KI _dal;
        private readonly IEncriptador_83KI _encriptador;
        private readonly ISessionManager_83KI _sessionManager;
        private readonly IBitacoraManager_83KI _bitacora;
        private const int NumeroIntentos = 3;
        public GestorUsuarioBLL_83KI(IUsuarioDAL_83KI dal, IEncriptador_83KI encriptador, ISessionManager_83KI sessionManager, IBitacoraManager_83KI bitacora)
        {
            _dal = dal;
            _encriptador = encriptador;
            _sessionManager = sessionManager;
            _bitacora = bitacora;
        }

        public void Login(string email, string contrasena)
        {
            if (_sessionManager.UsuarioActivo != null)
                throw new UsuarioActivoActualmenteException_83KI();
            
            
            var usuario = _dal.ObtenerPorMail(email);

            if (usuario == null)
            {
                _bitacora.RegistrarEvento($"Intento de login fallido: {email} no existe", 2, Modulo.Seguridad, email);
                throw new UsuarioNoExisteException_83KI();
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
                throw new ContrasenaInvalidaException_83KI($"Intento {usuario.Intentos} de 3.");
            }

            // LOGIN EXITOSO
            _sessionManager.IniciarSesion(usuario);
            usuario.Intentos = 0; // Reseteamos intentos
            _dal.ActualizarIntentos(usuario);
            //REGISTRO DE LOGIN(Criticidad 3)
            _bitacora.RegistrarEvento($"Login exitoso: {usuario.Email}", 3, Modulo.Usuarios, email);
        }
        public void Logout()
        {
            var usuario = _sessionManager.UsuarioActivo;
            if (usuario != null)
            {
                _sessionManager.CerrarSesion();
                _bitacora.RegistrarEvento($"Login exitoso: {usuario.Email}", 3, Modulo.Usuarios, usuario.Email);
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

        public void BloqueoCuentaUsuario(Usuario_83KI usuario)
        {
            usuario.Bloqueado = true;
            _dal.BloquearUsuario(usuario);
            // REGISTRO DE BLOQUEO (Criticidad 1)
            _bitacora.RegistrarEvento($"Usuario bloqueado: {usuario.Email}", 1, Modulo.Usuarios, usuario.Email);
            throw new UsuarioBloqueadoException_83KI();
        }

        public void CrearUsuario(Usuario_83KI usuario)
        {
            if (_dal.ExisteDni(usuario.DNI)) throw new DniRegistradoException_83KI();
            if (_dal.ExisteEmail(usuario.Email)) throw new EmailRegistradoException_83KI();
            
            usuario.Contrasena = _encriptador.HashContrasena(usuario.DNI.ToString());

            _dal.CrearUsuario(usuario);
            _bitacora.RegistrarEvento(
                $"Nuevo usuario creado: {usuario.Email} (Rol: {usuario.RolUsuario})",
                1,
                Modulo.Usuarios,
                usuario.Email
            );
        }
        public IEnumerable<Usuario_83KI> ObtenerUsuarios()
        {
            return _dal.ObtenerUsuarios();
        }

        public void DesbloquearCuenta(Usuario_83KI usuario)
        {
            //todo: falta logica aca
            _dal.DesbloquearCuenta(usuario);
        }
    }
}
