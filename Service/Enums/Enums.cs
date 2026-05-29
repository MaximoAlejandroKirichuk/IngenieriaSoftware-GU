using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entidades
{
    public enum Modulo
    {
        Usuarios,
        Seguridad
    }

    public enum Criticidad
    {
        Alto = 1,
        Medio = 2,
        Bajo = 3
    }
}
