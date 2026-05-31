using Service.Entidades;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface ISessionManager_83KI
    {
        void IniciarSesion(Usuario_83KI usuario);
        void CerrarSesion();
        IEnumerable<Patente_83KI> ObtenerPermisos();
        bool TienePermiso(PermisoSistema_83KI permiso);
        Usuario_83KI UsuarioActivo { get; }
    }
}
