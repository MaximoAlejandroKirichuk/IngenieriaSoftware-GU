using System;
using System.Collections.Generic;
using Service.Entidades;
namespace DAL.interfaces
{
    public interface IBitacoraDAL_83KI
    {
        void Registrar(BitacoraEvento_83KI evento);

        // Para la interfaz de Auditoría con el filtro de 3 días
        IEnumerable<BitacoraEvento_83KI> Consultar(DateTime desde, DateTime hasta, string modulo = null, int? criticidad = null);
    }
}
