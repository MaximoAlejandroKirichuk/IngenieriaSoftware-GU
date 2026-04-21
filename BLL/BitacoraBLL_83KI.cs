using BE;
using BLL.Interfaces;
using DAL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BitacoraBLL_83KI : IBitacoraManager_83KI
    {
        private readonly IBitacoraDAL_83KI _bitacoraDAL;
        public BitacoraBLL_83KI(IBitacoraDAL_83KI bitacora)
        {
            _bitacoraDAL = bitacora;
        }

        public IEnumerable<BitacoraEvento_83KI> ListarEventos(DateTime desde, DateTime hasta)
        {
            return _bitacoraDAL.Consultar(desde, hasta);
        }

        public void RegistrarEvento(string descripcion, int criticidad, Modulo modulo, string email)
        {
            BitacoraEvento_83KI evento = new BitacoraEvento_83KI(descripcion, criticidad, modulo, email);
            _bitacoraDAL.Registrar(evento);
        }

    }
}
