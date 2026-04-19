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
                

                IUsuarioDAL datos = new UsuarioDAL();
                IBitacoraDAL bitacoraDAL = new BitacoraEventoDAL();

                IEncriptador encriptador = new Encriptador();
                ISessionManager sessionManager = SessionManager.Instancia;
                IBitacoraManager bitacoraManager = new BitacoraBLL(bitacoraDAL);

                _gestorUsuario = new GestorUsuarioBLL(datos, encriptador, sessionManager, bitacoraManager);
            }
            return _gestorUsuario;
        }
    }
}
