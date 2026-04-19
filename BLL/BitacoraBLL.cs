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
    public class BitacoraBLL : IBitacoraManager
    {
        private readonly IBitacoraDAL _bitacoraDAL;
        public BitacoraBLL(IBitacoraDAL bitacora)
        {
            _bitacoraDAL = bitacora;
        }

        public IEnumerable<BitacoraEvento> ListarEventos(DateTime desde, DateTime hasta)
        {
            return _bitacoraDAL.Consultar(desde, hasta);
        }

        public void RegistrarEvento(string descripcion, int criticidad, string modulo, string email)
        {
            BitacoraEvento evento = new BitacoraEvento(descripcion, criticidad, modulo, email);
            _bitacoraDAL.Registrar(evento);
        }

    }
}
