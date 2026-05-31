using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.interfaces;
using Service;
using Service.Entidades;
using Service.Excepciones;
using Service.Excepciones.CrearUsuario;
using Service.Excepciones.Login;
using Service.Interfaces;

namespace BLL
{
    internal class GestorUsuarioBLL_83KI : IGestorUsuario_83KI
    {
        private const int IntentosPermitidos = 3;
        private const int MinutosParaReiniciarIntentos = 30;

        private readonly IUsuarioDAL_83KI _dal;
        private readonly IRolDAL_83KI _rolDal;
        private readonly IEncriptador_83KI _encriptador;
        private readonly ISessionManager_83KI _sessionManager;
        private readonly IBitacoraManager_83KI _bitacora;
        private readonly IGestorIdioma_83KI _gestorIdioma;

        public GestorUsuarioBLL_83KI(IUsuarioDAL_83KI dal, IEncriptador_83KI encriptador, ISessionManager_83KI sessionManager, IBitacoraManager_83KI bitacora)
            : this(dal, encriptador, sessionManager, bitacora, new RolDAL_83KI(), new GestorIdioma_83KI())
        {
        }

        public GestorUsuarioBLL_83KI(IUsuarioDAL_83KI dal, IEncriptador_83KI encriptador, ISessionManager_83KI sessionManager, IBitacoraManager_83KI bitacora, IRolDAL_83KI rolDal, IGestorIdioma_83KI gestorIdioma)
        {
            _dal = dal;
            _encriptador = encriptador;
            _sessionManager = sessionManager;
            _bitacora = bitacora;
            _rolDal = rolDal;
            _gestorIdioma = gestorIdioma;
        }

        public void Login(string userName, string contrasena)
        {
            if (_sessionManager.UsuarioActivo != null)
            {
                throw new UsuarioActivoActualmenteException_83KI();
            }

            var usuario = _dal.ObtenerPorUserName(userName) ?? throw new UsuarioNoExisteException_83KI() ;

            if (!usuario.Activo)
            {
                throw new UsuarioDeshabilitadoException_83KI();
            }

            if (usuario.Bloqueado)
            {
                throw new UsuarioBloqueadoException_83KI();
            }

            ReiniciarIntentosSiCorresponde(usuario);

            string hash = _encriptador.HashContrasena(contrasena);

            if (usuario.Contrasena != hash)
            {
                RegistrarIntentoFallido(usuario);

                if (SuperoIntentosPermitidos(usuario))
                {
                    BloquearPorIntentosFallidos(usuario);
                    throw new UsuarioBloqueadoException_83KI();
                }

                throw new ContrasenaInvalidaException_83KI($"Intento {usuario.IntentosRealizados} de {IntentosPermitidos}.");
            }

            usuario.AsignarRol(ObtenerRolConPermisos(usuario.Rol));
            _sessionManager.IniciarSesion(usuario);
            ReiniciarIntentosFallidos(usuario);
            _gestorIdioma.CambiarIdioma(usuario.IdiomaId);
            _bitacora.RegistrarEvento(
                BitacoraEvento_83KI.CrearNuevo(
                    $"Login exitoso: {usuario.UserName}",
                    Criticidad.Bajo,
                    Modulo.Usuarios,
                    userName
                )
            );
        }

        private Rol_83KI ObtenerRolConPermisos(Rol_83KI rol)
        {
            Rol_83KI rolConPermisos = _rolDal.ObtenerRolesConPermisos()
                .FirstOrDefault(r => r.CodigoRol == rol.CodigoRol);

            if (rolConPermisos == null)
            {
                throw new InvalidOperationException("El rol del usuario no existe o no pudo cargarse con permisos.");
            }

            return rolConPermisos;
        }

        private void ReiniciarIntentosSiCorresponde(Usuario_83KI usuario)
        {
            if (!usuario.FechaUltimoIntento.HasValue)
            {
                return;
            }

            TimeSpan tiempoDesdeUltimoIntento = DateTime.Now - usuario.FechaUltimoIntento.Value;

            if (tiempoDesdeUltimoIntento.TotalMinutes >= MinutosParaReiniciarIntentos)
            {
                ReiniciarIntentosFallidos(usuario);
            }
        }

        private void RegistrarIntentoFallido(Usuario_83KI usuario)
        {
            usuario.RegistrarIntentoFallido(DateTime.Now);
            _dal.ActualizarIntentosFallidos(usuario);
        }

        private bool SuperoIntentosPermitidos(Usuario_83KI usuario)
        {
            return usuario.IntentosRealizados > IntentosPermitidos;
        }

        private void BloquearPorIntentosFallidos(Usuario_83KI usuario)
        {
            usuario.Bloquear();
            _dal.BloquearUsuario(usuario);
            _bitacora.RegistrarEvento(
                BitacoraEvento_83KI.CrearNuevo(
                    $"Usuario bloqueado por intentos fallidos: {usuario.UserName}",
                    Criticidad.Alto,
                    Modulo.Usuarios,
                    usuario.UserName
                )
            );
        }

        private void ReiniciarIntentosFallidos(Usuario_83KI usuario)
        {
            usuario.ReiniciarIntentosFallidos();
            _dal.ReiniciarIntentosFallidos(usuario);
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
                        Criticidad.Bajo,
                        Modulo.Usuarios,
                        usuario.UserName
                    )
                );
            }
        }

        public void BloquearUsuarioPorUserName(string userName)
        {
            ValidarPermiso(PermisoSistema_83KI.BloquearUsuario);

            var usuario = _dal.ObtenerPorUserName(userName) ?? throw new UsuarioNoExisteException_83KI();

            if (usuario.Bloqueado)
            {
                throw new UsuarioBloqueadoException_83KI();
            }

            usuario.Bloquear();
            _dal.BloquearUsuario(usuario);
            _bitacora.RegistrarEvento(
                BitacoraEvento_83KI.CrearNuevo(
                    $"Usuario bloqueado: {usuario.UserName}",
                    Criticidad.Alto,
                    Modulo.Usuarios,
                    usuario.UserName
                )
            );
        }

        public void CambiarContrasenaUsuarioActual(string contrasenaActual, string nuevaContrasena)
        {
            var usuarioActivo = _sessionManager.UsuarioActivo ?? throw new UsuarioNoAutenticadoException_83KI();
            CambiarContrasena(usuarioActivo.UserName, contrasenaActual, nuevaContrasena);
        }

        public void CambiarIdiomaUsuarioActual(string idiomaId)
        {
            var usuarioActivo = _sessionManager.UsuarioActivo ?? throw new UsuarioNoAutenticadoException_83KI();
            _gestorIdioma.CambiarIdioma(idiomaId);

            string idiomaAplicado = _gestorIdioma.IdiomaActual.Id;
            _dal.ActualizarIdioma(usuarioActivo.DNI, idiomaAplicado);
            usuarioActivo.CambiarIdioma(idiomaAplicado);
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
                    Criticidad.Alto,
                    Modulo.Usuarios,
                    usuario.UserName
                )
            );
        }

        public void ModificarUsuario(int dni, string email, Rol_83KI rol)
        {
            var usuarioActivo = _sessionManager.UsuarioActivo ?? throw new UsuarioNoAutenticadoException_83KI();
            ValidarPermiso(PermisoSistema_83KI.ModificarUsuario);

            var usuarioModificado = _dal.ObtenerPorDni(dni) ?? throw new UsuarioNoExisteException_83KI();
            usuarioModificado.ModificarEmailYRol(email, rol);

            if (_dal.ExisteEmailParaOtroUsuario(usuarioModificado.Email, usuarioModificado.DNI))
            {
                throw new EmailRegistradoException_83KI();
            }

            bool esAutoedicion = usuarioActivo.DNI == usuarioModificado.DNI;
            if (esAutoedicion && usuarioActivo.Rol.CodigoRol != usuarioModificado.Rol.CodigoRol)
            {
                throw new InvalidOperationException("No puede modificar su propio rol.");
            }

            _dal.ModificarUsuario(usuarioModificado.DNI, usuarioModificado.Email, usuarioModificado.Rol);

            if (esAutoedicion)
            {
                _sessionManager.UsuarioActivo.ModificarEmail(usuarioModificado.Email);
            }

            _bitacora.RegistrarEvento(
                BitacoraEvento_83KI.CrearNuevo(
                    $"Usuario modificado: DNI {usuarioModificado.DNI}. Email: {usuarioModificado.Email}. Rol: {usuarioModificado.Rol}. Actor: {usuarioActivo.UserName}",
                    Criticidad.Alto,
                    Modulo.Usuarios,
                    usuarioActivo.UserName
                )
            );
        }

        public void CrearUsuario(string nombre, string apellido, int dni, string email, Rol_83KI rol)
        {
            ValidarPermiso(PermisoSistema_83KI.CrearUsuario);

            if (_dal.ExisteDni(dni))
            {
                throw new DniRegistradoException_83KI();
            }

            if (_dal.ExisteEmail(email))
            {
                throw new EmailRegistradoException_83KI();
            }

            string contrasenaPorDefecto = Usuario_83KI.EstablecerContrasenaPorDefecto(apellido, dni);
            string contrasenaHash = _encriptador.HashContrasena(contrasenaPorDefecto);

            Usuario_83KI usuario = Usuario_83KI.CrearNuevo(nombre, apellido, dni, email, rol, contrasenaHash);

            _dal.CrearUsuario(usuario);

            _bitacora.RegistrarEvento(
                BitacoraEvento_83KI.CrearNuevo(
                    $"Nuevo usuario creado: {usuario.UserName} (Rol: {usuario.Rol})",
                    Criticidad.Alto,
                    Modulo.Usuarios,
                    usuario.UserName
                )
            );
        }

        public IEnumerable<Usuario_83KI> ObtenerUsuarios()
        {
            return _dal.ObtenerUsuarios();
        }

        public void DesbloquearCuenta(int dni)
        {
            ValidarPermiso(PermisoSistema_83KI.DesbloquearUsuario);

            var usuario = _dal.ObtenerPorDni(dni) ?? throw new UsuarioNoExisteException_83KI();
            string contrasenaPorDefecto = Usuario_83KI.EstablecerContrasenaPorDefecto(usuario.Apellido, usuario.DNI);
            usuario.Desbloquear(_encriptador.HashContrasena(contrasenaPorDefecto));
            _dal.DesbloquearCuenta(usuario);
            _bitacora.RegistrarEvento(
                BitacoraEvento_83KI.CrearNuevo(
                    $"Usuario desbloqueado: {usuario.UserName} (Rol: {usuario.Rol})",
                    Criticidad.Alto,
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
            //ACA VALIDO QUE NO ME PUEDO DESHABILITAR A MI MISMO.
            var usuarioActivo = _sessionManager.UsuarioActivo ?? throw new UsuarioNoAutenticadoException_83KI();
            ValidarPermiso(activo ? PermisoSistema_83KI.HabilitarUsuario : PermisoSistema_83KI.DeshabilitarUsuario);

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
                    Criticidad.Alto,
                    Modulo.Usuarios,
                    usuarioActivo.UserName
                )
            );
        }

        private void ValidarPermiso(PermisoSistema_83KI permiso)
        {
            if (!_sessionManager.TienePermiso(permiso))
            {
                throw new InvalidOperationException("No tiene permisos para realizar esta accion.");
            }
        }

    }
}
