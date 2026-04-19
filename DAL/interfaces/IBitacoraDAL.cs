using BE;
using DAL.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.interfaces
{
    public interface IBitacoraDAL
    {
        void Registrar(BitacoraEvento evento);

        // Para la interfaz de Auditoría con el filtro de 3 días
        IEnumerable<BitacoraEvento> Consultar(DateTime desde, DateTime hasta, string modulo = null, int? criticidad = null);
    }
}
