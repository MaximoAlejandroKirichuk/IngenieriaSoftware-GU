using Service.Interfaces;
using Service.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SessionManager_83KI : ISessionManager_83KI
    {
        private static SessionManager_83KI _instancia;
        public Usuario_83KI UsuarioActivo { get; private set; }
        private SessionManager_83KI() { }

        public static SessionManager_83KI Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new SessionManager_83KI();
                }
                return _instancia;
            }
        }
        public void IniciarSesion(Usuario_83KI usuario)
        {
            UsuarioActivo = usuario;
        }
        public void CerrarSesion()
        {
            UsuarioActivo = null;
        }

        public IEnumerable<Patente_83KI> ObtenerPermisos()
        {
            if (UsuarioActivo == null || UsuarioActivo.Rol == null)
            {
                return Enumerable.Empty<Patente_83KI>();
            }

            return UsuarioActivo.Rol.ObtenerPatentes().ToList();
        }

        public bool TienePermiso(PermisoSistema_83KI permiso)
        {
            int codigoPermiso = (int)permiso;
            return ObtenerPermisos().Any(p => p.CodigoPatente == codigoPermiso);
        }
    }
}
