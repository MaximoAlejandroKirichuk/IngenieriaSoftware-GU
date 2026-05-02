using System;
using System.Collections.Generic;
using DAL.interfaces;
using Service.Entidades;
using Service.Excepciones;
using Service.Excepciones.CrearUsuario;
using Service.Excepciones.Login;
using Service.Interfaces;

namespace BLL
{
    internal class GestorUsuarioBLL_83KI : IGestorUsuario_83KI
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
            {
                throw new UsuarioActivoActualmenteException_83KI();
            }

            var usuario = _dal.ObtenerPorUserName(userName) ?? throw new UsuarioNoExisteException_83KI();

            if (!usuario.Activo)
            {
                throw new UsuarioDeshabilitadoException_83KI();
            }

            if (usuario.Bloqueado)
            {
                throw new UsuarioBloqueadoException_83KI();
            }

            string hash = _encriptador.HashContrasena(contrasena);

            if (usuario.Contrasena != hash)
            {
                usuario.RegistrarIntentoFallido();
                if (usuario.Intentos >= NumeroIntentos)
                {
                    BloquearCuentaUsuario(usuario);
                }

                _dal.ActualizarIntentos(usuario);
                throw new ContrasenaInvalidaException_83KI($"Intento {usuario.Intentos} de {NumeroIntentos}.");
            }

            _sessionManager.IniciarSesion(usuario);
            usuario.ReiniciarIntentos();
            _dal.ActualizarIntentos(usuario);
            _bitacora.RegistrarEvento(
                BitacoraEvento_83KI.CrearNuevo(
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
                    BitacoraEvento_83KI.CrearNuevo(
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

            usuario.CambiarContrasena(_encriptador.HashContrasena(nuevaContrasena));
            _dal.ActualizarContrasena(usuario);

            if (_sessionManager.UsuarioActivo != null && _sessionManager.UsuarioActivo.UserName == usuario.UserName)
            {
                _sessionManager.UsuarioActivo.CambiarContrasena(usuario.Contrasena);
            }

            _bitacora.RegistrarEvento(
                BitacoraEvento_83KI.CrearNuevo(
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

            var usuarioModificado = _dal.ObtenerPorDni(dni) ?? throw new UsuarioNoExisteException_83KI();
            usuarioModificado.ModificarEmailYRol(email, rol);

            if (_dal.ExisteEmailParaOtroUsuario(usuarioModificado.Email, usuarioModificado.DNI))
            {
                throw new EmailRegistradoException_83KI();
            }

            bool esAutoedicion = usuarioActivo.DNI == usuarioModificado.DNI;
            if (esAutoedicion && usuarioActivo.RolUsuario != usuarioModificado.RolUsuario)
            {
                throw new InvalidOperationException("No puede modificar su propio rol.");
            }

            _dal.ModificarUsuario(usuarioModificado.DNI, usuarioModificado.Email, usuarioModificado.RolUsuario);

            if (esAutoedicion)
            {
                _sessionManager.UsuarioActivo.ModificarEmail(usuarioModificado.Email);
            }

            _bitacora.RegistrarEvento(
                BitacoraEvento_83KI.CrearNuevo(
                    $"Usuario modificado: DNI {usuarioModificado.DNI}. Email: {usuarioModificado.Email}. Rol: {usuarioModificado.RolUsuario}. Actor: {usuarioActivo.UserName}",
                    2,
                    Modulo.Usuarios,
                    usuarioActivo.UserName
                )
            );
        }

        public void CrearUsuario(string nombre, string apellido, int dni, string email, RolUsuario rol)
        {
            string contrasenaPorDefecto = Usuario_83KI.EstablecerContrasenaPorDefecto(apellido, dni);
            string contrasenaHash = _encriptador.HashContrasena(contrasenaPorDefecto);
            Usuario_83KI usuario = Usuario_83KI.CrearNuevo(nombre, apellido, dni, email, rol, contrasenaHash);

            if (_dal.ExisteDni(usuario.DNI))
            {
                throw new DniRegistradoException_83KI();
            }

            if (_dal.ExisteEmail(usuario.Email))
            {
                throw new EmailRegistradoException_83KI();
            }

            _dal.CrearUsuario(usuario);
            _bitacora.RegistrarEvento(
                BitacoraEvento_83KI.CrearNuevo(
                    $"Nuevo usuario creado: {usuario.UserName} (Rol: {usuario.RolUsuario})",
                    1,
                    Modulo.Usuarios,
                    usuario.UserName
                )
            );
        }

        private void BloquearCuentaUsuario(Usuario_83KI usuario)
        {
            usuario.Bloquear();
            _dal.BloquearUsuario(usuario);
            _bitacora.RegistrarEvento(
                BitacoraEvento_83KI.CrearNuevo(
                    $"Usuario bloqueado: {usuario.UserName}",
                    1,
                    Modulo.Usuarios,
                    usuario.UserName
                )
            );
            throw new UsuarioBloqueadoException_83KI();
        }

        public IEnumerable<Usuario_83KI> ObtenerUsuarios()
        {
            return _dal.ObtenerUsuarios();
        }

        public void DesbloquearCuenta(int dni)
        {
            var usuario = _dal.ObtenerPorDni(dni) ?? throw new UsuarioNoExisteException_83KI();
            string contrasenaPorDefecto = Usuario_83KI.EstablecerContrasenaPorDefecto(usuario.Apellido, usuario.DNI);
            usuario.Desbloquear(_encriptador.HashContrasena(contrasenaPorDefecto));
            _dal.DesbloquearCuenta(usuario);
            _bitacora.RegistrarEvento(
                BitacoraEvento_83KI.CrearNuevo(
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

            if (activo)
            {
                usuarioGestionado.Habilitar();
            }
            else
            {
                usuarioGestionado.Deshabilitar();
            }

            _dal.ActualizarEstadoActivo(usuarioGestionado.DNI, usuarioGestionado.Activo);

            string accion = activo ? "habilitado" : "deshabilitado";
            _bitacora.RegistrarEvento(
                BitacoraEvento_83KI.CrearNuevo(
                    $"Usuario {accion}: DNI {dni}. Actor: {usuarioActivo.UserName}",
                    2,
                    Modulo.Usuarios,
                    usuarioActivo.UserName
                )
            );
        }
    }
}
