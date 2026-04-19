using BE;
using DAL.DAL;
using DAL.interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioDAL : IUsuarioDAL
    {
        private AccesoDAL _accesoDAL = new AccesoDAL();

        public Usuario ObtenerPorMail(string mail)
        {
            string sql = "SELECT * FROM Usuarios WHERE Email = @mail";
            var parametros = new List<SqlParameter> { new SqlParameter("@mail", mail) };

            DataSet ds = _accesoDAL.Leer(sql, parametros);

            // Verificamos que el DataSet tenga tablas y que la primera tenga filas
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0]; // Tomamos la primera fila de la primera tabla

                return new Usuario
                {
                    DNI = (int)Convert.ToInt64(row["DNI"]),
                    Email = row["Email"].ToString(),
                    Contrasena = row["Contrasena"].ToString(),
                    Bloqueado = Convert.ToBoolean(row["Bloqueado"]),
                    Intentos = Convert.ToInt32(row["Intentos"])
                };
            }
            return null;
        }

        public void ActualizarIntentos(Usuario usuario)
        {
            string consulta = "UPDATE Usuarios SET Intentos = @intentos WHERE DNI = @dni";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@intentos", usuario.Intentos),
                new SqlParameter("@dni", usuario.DNI)
            };

            _accesoDAL.Escribir(consulta, parametros);
        }

        public void BloquearUsuario(Usuario usuario)
        {
            // Aprovechamos y actualizamos el bit de Bloqueado y los Intentos a 3
            string consulta = "UPDATE Usuarios SET Bloqueado = 1, Intentos = 3 WHERE DNI = @dni";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@dni", usuario.DNI)
            };

            _accesoDAL.Escribir(consulta, parametros);
        }
    }
}

