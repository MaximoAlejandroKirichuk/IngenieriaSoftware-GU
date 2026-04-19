using BE;
using DAL.DAL;
using DAL.interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioDAL : IUsuarioDAL
    {
        private AccesoDAL _accesoDAL = new AccesoDAL();

        public void ActualizarIntentos(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void BloquearUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Usuario ObtenerPorMail(string mail)
        {
            string consulta = "SELECT * FROM Usuarios WHERE Email = @email";
            DataTable dt = _accesoDAL.Leer();

            // Mapeamos el DataTable a una Lista de BE (Transformación)
            List<BE.Usuario> lista = new List<BE.Usuario>();
            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Usuario
                {
                    Email = row["Email"].ToString(),
                    Password = row["Password"].ToString()
                });
            }
            return lista;
        }
    }
}
