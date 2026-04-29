using BE;
using System;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IBitacoraRepository_83KI
    {
        void Registrar(BitacoraEvento_83KI evento);
        IEnumerable<BitacoraEvento_83KI> Consultar(DateTime desde, DateTime hasta, string modulo = null, int? criticidad = null);
    }
}
