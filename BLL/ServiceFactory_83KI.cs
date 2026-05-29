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
        // Usamos para crear los objetos solo cuando se necesitan
        private static IGestorUsuario_83KI _gestorUsuario;
        private static IGestorRol_83KI _gestorRol;
        private static IBitacoraManager_83KI _bitacoraManager;
        private static IConsultaBitacoraEventos_83KI _consultaBitacoraEventos;

        public static IGestorUsuario_83KI GetGestorUsuario()
        {
            if (_gestorUsuario == null)
            {
                //necesito para consulta y escriba con la base de datos
                IUsuarioDAL_83KI datos = new UsuarioDAL_83KI();

                IEncriptador_83KI encriptador = new Encriptador_83KI();
                ISessionManager_83KI sessionManager = SessionManager_83KI.Instancia;
                IBitacoraManager_83KI bitacoraManager = GetBitacoraManager();

                _gestorUsuario = new GestorUsuarioBLL_83KI(datos, encriptador, sessionManager, bitacoraManager);
            }
            return _gestorUsuario;
        }

        public static IGestorRol_83KI GetGestorRol()
        {
            if (_gestorRol == null)
            {
                IRolDAL_83KI rolDal = new RolDAL_83KI();
                _gestorRol = new GestorRolBLL_83KI(rolDal);
            }

            return _gestorRol;
        }

        public static IBitacoraManager_83KI GetBitacoraManager()
        {
            if (_bitacoraManager == null)
            {
                IBitacoraDAL_83KI bitacoraDAL = new BitacoraEventoDAL_83KI();
                _bitacoraManager = new BitacoraBLL_83KI(bitacoraDAL);
            }

            return _bitacoraManager;
        }

        public static IConsultaBitacoraEventos_83KI GetConsultaBitacoraEventos()
        {
            if (_consultaBitacoraEventos == null)
            {
                _consultaBitacoraEventos = new ConsultaBitacoraEventos_83KI(GetGestorUsuario(), GetBitacoraManager());
            }

            return _consultaBitacoraEventos;
        }
    }
}
