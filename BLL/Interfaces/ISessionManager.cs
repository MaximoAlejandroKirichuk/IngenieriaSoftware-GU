using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISessionManager
    {
        void IniciarSesion(Usuario usuario);
        void CerrarSesion();
        Usuario UsuarioActivo { get; }
    }
}
