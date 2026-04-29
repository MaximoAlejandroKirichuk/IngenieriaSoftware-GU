using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL;
using Service.Interfaces;
using DAL.interfaces;


namespace Service
{
    public static class ServiceFactory_83KI
    {
        // Usamos Lazy para crear los objetos solo cuando se necesitan
        private static IGestorUsuario_83KI _gestorUsuario;

        public static IGestorUsuario_83KI GetGestorUsuario()
        {
            if (_gestorUsuario == null)
            {
                //necesito para consulta y escriba con la base de datos
                IUsuarioDAL_83KI datos = new UsuarioDAL_83KI();
                IBitacoraDAL_83KI bitacoraDAL = new BitacoraEventoDAL_83KI();


                IEncriptador_83KI encriptador = new Encriptador_83KI();
                ISessionManager_83KI sessionManager = SessionManager_83KI.Instancia;
                IBitacoraManager_83KI bitacoraManager = new BitacoraBLL_83KI(bitacoraDAL);

                _gestorUsuario = new GestorUsuarioBLL_83KI(datos, encriptador, sessionManager, bitacoraManager);
            }
            return _gestorUsuario;
        }

        
    }
}
