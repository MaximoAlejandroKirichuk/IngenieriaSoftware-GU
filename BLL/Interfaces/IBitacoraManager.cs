using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBitacoraManager
    {
        void RegistrarEvento(string descripcion, int criticidad, string modulo, string email);

        // metodo de consulta en GUI (últimos 3 días por defecto)
        IEnumerable<BitacoraEvento> ListarEventos(DateTime desde, DateTime hasta);
    }
}
