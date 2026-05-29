using Service.DTOs;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IConsultaBitacoraEventos_83KI
    {
        IEnumerable<BitacoraEventoVista_83KI> Consultar(FiltroBitacoraEventos_83KI filtro);
    }
}
