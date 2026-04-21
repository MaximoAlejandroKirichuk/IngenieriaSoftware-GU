using BE;
using DAL.DAL;
using DAL.interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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
            string consulta = "UPDATE Usuarios SET Bloqueado = 1, Intentos = 3 WHERE DNI = @dni";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@dni", usuario.DNI)
            };

            _accesoDAL.Escribir(consulta, parametros);
        }

        public void CrearUsuario(Usuario usuario)
        {
            // Nota: No incluimos ID si es Identity/Autonumérico en SQL
            string consulta = @"INSERT INTO Usuarios (Nombre, Apellido, DNI, Email, Rol, Password) 
                        VALUES (@nombre, @apellido, @dni, @email, @rol, @pass)";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@nombre", usuario.Nombre),
                new SqlParameter("@apellido", usuario.Apellido),
                new SqlParameter("@dni",usuario.DNI),
                new SqlParameter("@email", usuario.Email),
                new SqlParameter("@rol", usuario.RolUsuario.ToString()), // Guardamos el nombre del Enum
                new SqlParameter("@pass", usuario.Contrasena)
            };

            _accesoDAL.Escribir(consulta, parametros);
        }

        public bool ExisteDni(int dni)
        {
            string query = "SELECT COUNT(1) AS Total FROM Usuarios WHERE DNI = @dni";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@dni", dni)
            };

            DataSet ds = _accesoDAL.Leer(query, parametros);

            // Verificamos si tiene datos y accedemos a la primera tabla, primera fila
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int total = Convert.ToInt32(ds.Tables[0].Rows[0]["Total"]);
                return total > 0;
            }

            return false;
        }

        public bool ExisteEmail(string email)
        {
            string query = "SELECT COUNT(1) AS Total FROM Usuarios WHERE Email = @email";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@dni",email)
            };

            DataSet ds = _accesoDAL.Leer(query, parametros);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int total = Convert.ToInt32(ds.Tables[0].Rows[0]["Total"]);
                return total > 0;
            }
            return false;
        }

        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            string query = "SELECT * FROM Usuarios";
            DataSet ds = _accesoDAL.Leer(query);
            List<Usuario> listaTemporal = new List<Usuario>();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    listaTemporal.Add(new Usuario
                    {
                        DNI = Convert.ToInt32(row["DNI"]),
                        Nombre = row["Nombre"].ToString(),
                        Apellido = row["Apellido"].ToString(),
                        Email = row["Email"].ToString(),
                        Bloqueado = Convert.ToBoolean(row["Bloqueado"]),
                        Intentos = Convert.ToInt32(row["Intentos"])
                    });
                }
            }
            return listaTemporal;
        }
    }
}

