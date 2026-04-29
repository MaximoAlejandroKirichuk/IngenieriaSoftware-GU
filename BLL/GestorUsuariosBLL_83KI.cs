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
using System.Text.RegularExpressions;
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

        public void Login(string userName, string contrasena)
        {
            if (_sessionManager.UsuarioActivo != null)
                throw new UsuarioActivoActualmenteException_83KI();
  
            var usuario = _dal.ObtenerPorUserName(userName) ?? throw new UsuarioNoExisteException_83KI();

            if (!usuario.Activo)
                throw new UsuarioDeshabilitadoException_83KI();

            if (usuario.Bloqueado) 
                throw new UsuarioBloqueadoException_83KI();

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
            _bitacora.RegistrarEvento(
                new BitacoraEvento_83KI(
                    $"Login exitoso: {usuario.UserName}",
                    3,
                    Modulo.Usuarios,
                    userName
                )
            );
        }
        public void Logout()
        {
            var usuario = _sessionManager.UsuarioActivo;
            if (usuario != null)
            {
                _sessionManager.CerrarSesion();
                _bitacora.RegistrarEvento(
                    new BitacoraEvento_83KI(
                        $"Logout exitoso: {usuario.UserName}",
                        3,
                        Modulo.Usuarios,
                        usuario.UserName
                    )
                );
            }
        }

        public void CambiarContrasenaUsuarioActual(string contrasenaActual, string nuevaContrasena)
        {
            var usuarioActivo = _sessionManager.UsuarioActivo ?? throw new UsuarioNoAutenticadoException_83KI();
            CambiarContrasena(usuarioActivo.UserName, contrasenaActual, nuevaContrasena);
        }

        private void CambiarContrasena(string userName, string contrasenaActual, string nuevaContrasena)
        {
            var usuario = _dal.ObtenerPorUserName(userName) ?? throw new UsuarioNoExisteException_83KI();
            string hashContrasenaActual = _encriptador.HashContrasena(contrasenaActual);

            if (usuario.Contrasena != hashContrasenaActual)
            {
                throw new ContrasenaInvalidaException_83KI("La contraseña actual ingresada no coincide con la registrada.");
            }

            usuario.Contrasena = _encriptador.HashContrasena(nuevaContrasena);
            _dal.ActualizarContrasena(usuario);

            if (_sessionManager.UsuarioActivo != null && _sessionManager.UsuarioActivo.UserName == usuario.UserName)
            {
                _sessionManager.UsuarioActivo.Contrasena = usuario.Contrasena;
            }

            _bitacora.RegistrarEvento(
                new BitacoraEvento_83KI(
                    $"Contraseña modificada: {usuario.UserName}",
                    2,
                    Modulo.Usuarios,
                    usuario.UserName
                )
            );
        }

        public void ModificarRolUsuario(RolUsuario Rol)
        {
            throw new NotImplementedException();
        }

        public void ModificarUsuario(int dni, string email, RolUsuario rol)
        {
            var usuarioActivo = _sessionManager.UsuarioActivo ?? throw new UsuarioNoAutenticadoException_83KI();

            if (usuarioActivo.RolUsuario != RolUsuario.Admin)
            {
                throw new InvalidOperationException("Solo un administrador puede modificar usuarios.");
            }
           
            if (_dal.ExisteEmailParaOtroUsuario(email, dni))
            {
                throw new EmailRegistradoException_83KI();
            }

            //una persona no puede modificarse a si misma el rol. 
            //el email si
            bool esAutoedicion = usuarioActivo.DNI == dni;
            if (esAutoedicion && usuarioActivo.RolUsuario != rol)
            {
                throw new InvalidOperationException("No puede modificar su propio rol.");
            }

            _dal.ModificarUsuario(dni, email, rol);

            if (esAutoedicion)
            {
                _sessionManager.UsuarioActivo.Email = email;
            }

            _bitacora.RegistrarEvento(
                new BitacoraEvento_83KI(
                    $"Usuario modificado: DNI {dni}. Email: {email}. Rol: {rol}. Actor: {usuarioActivo.UserName}",
                    2,
                    Modulo.Usuarios,
                    usuarioActivo.UserName
                )
            );
        }

        public void BloqueoCuentaUsuario(Usuario_83KI usuario)
        {
            usuario.Bloqueado = true;
            _dal.BloquearUsuario(usuario);
            // REGISTRO DE BLOQUEO (Criticidad 1)
            _bitacora.RegistrarEvento(
                new BitacoraEvento_83KI(
                    $"Usuario bloqueado: {usuario.UserName}",
                    1,
                    Modulo.Usuarios,
                    usuario.UserName
                )
            );
            throw new UsuarioBloqueadoException_83KI();
        }
        
        public void CrearUsuario(Usuario_83KI usuario)
        {
            if (_dal.ExisteDni(usuario.DNI)) throw new DniRegistradoException_83KI();
            if (_dal.ExisteEmail(usuario.Email)) throw new EmailRegistradoException_83KI();

            string contrasenaPorDefecto = EstablecerContrasenaPorDefecto(usuario.Nombre, usuario.DNI);

            //TODO: PREGUNTAR SI ESTO ESTA BIEN
            usuario.Contrasena = _encriptador.HashContrasena(contrasenaPorDefecto);

            _dal.CrearUsuario(usuario);
            _bitacora.RegistrarEvento(
                new BitacoraEvento_83KI(
                    $"Nuevo usuario creado: {usuario.UserName} (Rol: {usuario.RolUsuario})",
                    1,
                    Modulo.Usuarios,
                    usuario.UserName
                )
            );
        }
        private string EstablecerContrasenaPorDefecto(string nombre, int dni)
        {
            // % 1000 obtiene el resto de la división = equivale a quedarse con los últimos 3 dígitos del DNI
            // Ej: 12345678 % 1000 = 678

            // :D3 formatea el número a 3 dígitos, agregando ceros a la izquierda si hace falta
            // Ej: 5 → "005", 45 → "045"

            return $"{nombre}{dni % 1000:D3}";
        }
        public IEnumerable<Usuario_83KI> ObtenerUsuarios()
        {
            return _dal.ObtenerUsuarios();
        }

        public void DesbloquearCuenta(Usuario_83KI usuario)
        {
            //asi se resetea el estado del usuario.
            usuario.Intentos = 0;
            usuario.Bloqueado = false;

            string contrasenaPorDefecto = EstablecerContrasenaPorDefecto(usuario.Nombre, usuario.DNI);
            usuario.Contrasena = _encriptador.HashContrasena(contrasenaPorDefecto);
            _dal.DesbloquearCuenta(usuario);
            _bitacora.RegistrarEvento(
                new BitacoraEvento_83KI(
                    $"Usuario desbloqueado: {usuario.UserName} (Rol: {usuario.RolUsuario})",
                    1,
                    Modulo.Usuarios,
                    usuario.UserName
                )
            );
        }

        public void HabilitarUsuario(int dni)
        {
            CambiarEstadoUsuario(dni, true);
        }

        public void DeshabilitarUsuario(int dni)
        {
            CambiarEstadoUsuario(dni, false);
        }

        private void CambiarEstadoUsuario(int dni, bool activo)
        {
            var usuarioActivo = _sessionManager.UsuarioActivo ?? throw new UsuarioNoAutenticadoException_83KI();

            if (usuarioActivo.RolUsuario != RolUsuario.Admin)
            {
                throw new InvalidOperationException("Solo un administrador puede gestionar usuarios.");
            }

            if (usuarioActivo.DNI == dni && !activo)
            {
                throw new InvalidOperationException("No puede deshabilitar su propio usuario.");
            }

            var usuarioGestionado = _dal.ObtenerPorDni(dni);
            if (usuarioGestionado == null)
            {
                throw new UsuarioNoExisteException_83KI();
            }

            if (usuarioGestionado.Activo == activo)
            {
                string mensajeEstado = activo ? "ya esta habilitado." : "ya esta deshabilitado.";
                throw new InvalidOperationException($"El usuario seleccionado {mensajeEstado}");
            }

            _dal.ActualizarEstadoActivo(dni, activo);

            string accion = activo ? "habilitado" : "deshabilitado";
            _bitacora.RegistrarEvento(
                new BitacoraEvento_83KI(
                    $"Usuario {accion}: DNI {dni}. Actor: {usuarioActivo.UserName}",
                    2,
                    Modulo.Usuarios,
                    usuarioActivo.UserName
                )
            );
        }
    }
}
