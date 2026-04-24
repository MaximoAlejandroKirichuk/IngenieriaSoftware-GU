using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.interfaces
{
    public interface IUsuarioDAL_83KI
    {
        Usuario_83KI ObtenerPorUserName(string userName);
        void ActualizarIntentos(Usuario_83KI usuario); // Incrementa o resetea a 0
        void BloquearUsuario(Usuario_83KI usuario);
        void CrearUsuario(Usuario_83KI usuario);
        bool ExisteDni(int dni);
        bool ExisteEmail(string email);
        void DesbloquearCuenta(Usuario_83KI usuario);
        IEnumerable<Usuario_83KI> ObtenerUsuarios();
    }
}
