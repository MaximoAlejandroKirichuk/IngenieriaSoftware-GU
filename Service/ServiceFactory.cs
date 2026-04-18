using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL;
using BLL.Interfaces;
using DAL.interfaces;

namespace Service
{
    public static class ServiceFactory
    {
        // Usamos Lazy para crear los objetos solo cuando se necesitan
        private static IGestorUsuario _gestorUsuario;

        public static IGestorUsuario GetGestorUsuario()
        {
            if (_gestorUsuario == null)
            {
                // La Factory sabe que el BLL necesita una DAL para funcionar
                // Inyectamos la DAL en el constructor del BLL
                IUsuarioDAL datos = new UsuarioDAL();
                IEncriptador encriptador = new Encriptador();
                ISessionManager sessionManager = SessionManager.Instancia;
                _gestorUsuario = new GestorUsuarioBLL(datos, encriptador, sessionManager);
            }
            return _gestorUsuario;
        }
    }
}
