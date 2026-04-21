using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.interfaces
{
    public interface IUsuarioDAL
    {
        Usuario ObtenerPorMail(string mail);
        void ActualizarIntentos(Usuario usuario); // Incrementa o resetea a 0
        void BloquearUsuario(Usuario usuario);
        void CrearUsuario(Usuario usuario);
        bool ExisteDni(int dni);
        bool ExisteEmail(string email);
        IEnumerable<Usuario> ObtenerUsuarios();
    }
}
