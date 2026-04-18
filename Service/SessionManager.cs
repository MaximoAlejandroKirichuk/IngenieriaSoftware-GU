using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BLL.Interfaces;
namespace Service
{
    public class SessionManager : ISessionManager
    {
        private static SessionManager _instancia;
        public Usuario UsuarioActivo { get; private set; }
        private SessionManager() { }

        public static SessionManager Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new SessionManager();
                }
                return _instancia;
            }
        }
        public void IniciarSesion(Usuario usuario)
        {
            UsuarioActivo = usuario;
        }
        public void CerrarSesion()
        {
            UsuarioActivo = null;
        }
    }
}
