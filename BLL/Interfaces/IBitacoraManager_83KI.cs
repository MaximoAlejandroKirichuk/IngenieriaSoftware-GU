using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBitacoraManager_83KI
    {
        void RegistrarEvento(string descripcion, int criticidad, Modulo modulo, string userName, int dni);

        // metodo de consulta en GUI (últimos 3 días por defecto)
        IEnumerable<BitacoraEvento_83KI> ListarEventos(DateTime desde, DateTime hasta);
    }
}
