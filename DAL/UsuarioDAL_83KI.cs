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
    public class UsuarioDAL_83KI : IUsuarioDAL_83KI
    {
        private AccesoDAL_83KI _accesoDAL = new AccesoDAL_83KI();

        public Usuario_83KI ObtenerPorUserName(string userName)
        {
            string sql = "SELECT * FROM Usuarios WHERE UserName = @userName";
            var parametros = new List<SqlParameter> { new SqlParameter("@userName", userName) };

            DataSet ds = _accesoDAL.Leer(sql, parametros);

            // Verificamos que el DataSet tenga tablas y que la primera tenga filas
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0]; // Tomamos la primera fila de la primera tabla

                return new Usuario_83KI
                {
                    DNI = (int)Convert.ToInt64(row["DNI"]),
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    Email = row["Email"].ToString(),
                    Contrasena = row["Contrasena"].ToString(),
                    RolUsuario = ParsearRol(row["RolUsuario"]),
                    Bloqueado = Convert.ToBoolean(row["Bloqueado"]),
                    Intentos = Convert.ToInt32(row["Intentos"])
                };
            }
            return null;
        }

        public void ActualizarIntentos(Usuario_83KI usuario)
        {
            string consulta = "UPDATE Usuarios SET Intentos = @intentos WHERE DNI = @dni";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@intentos", usuario.Intentos),
                new SqlParameter("@dni", usuario.DNI)
            };

            _accesoDAL.Escribir(consulta, parametros);
        }

        public void BloquearUsuario(Usuario_83KI usuario)
        {
            string consulta = "UPDATE Usuarios SET Bloqueado = 1, Intentos = 3 WHERE DNI = @dni";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@dni", usuario.DNI)
            };

            _accesoDAL.Escribir(consulta, parametros);
        }

        public void CrearUsuario(Usuario_83KI usuario)
        {
            string consulta = @"INSERT INTO Usuarios (UserName, Nombre, Apellido, DNI, Email, RolUsuario, Contrasena) 
                        VALUES (@userName ,@nombre, @apellido, @dni, @email, @rol, @pass)";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@username", usuario.UserName),
                new SqlParameter("@nombre", usuario.Nombre),
                new SqlParameter("@apellido", usuario.Apellido),
                new SqlParameter("@dni",usuario.DNI),
                new SqlParameter("@email", usuario.Email),
                new SqlParameter("@rol", usuario.RolUsuario.ToString()), // Guardamos el nombre del Enum
                new SqlParameter("@pass", usuario.Contrasena)
            };

            _accesoDAL.Escribir(consulta, parametros);
        }

        public void ModificarUsuario(int dni, string email, RolUsuario rol)
        {
            string consulta = "UPDATE Usuarios SET Email = @email, RolUsuario = @rol WHERE DNI = @dni";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@dni", dni),
                new SqlParameter("@email", email),
                new SqlParameter("@rol", rol.ToString())
            };

            _accesoDAL.Escribir(consulta, parametros);
        }

        public void ActualizarContrasena(Usuario_83KI usuario)
        {
            string consulta = "UPDATE Usuarios SET Contrasena = @contrasena WHERE DNI = @dni";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@dni", usuario.DNI),
                new SqlParameter("@contrasena", usuario.Contrasena)
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
                new SqlParameter("@email",email)
            };

            DataSet ds = _accesoDAL.Leer(query, parametros);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int total = Convert.ToInt32(ds.Tables[0].Rows[0]["Total"]);
                return total > 0;
            }
            return false;
        }

        public bool ExisteEmailParaOtroUsuario(string email, int dni)
        {
            //dni distinto al que estoy mandando <>
            string query = "SELECT COUNT(1) AS Total FROM Usuarios WHERE Email = @email AND DNI <> @dni";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@email", email),
                new SqlParameter("@dni", dni)
            };

            DataSet ds = _accesoDAL.Leer(query, parametros);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int total = Convert.ToInt32(ds.Tables[0].Rows[0]["Total"]);
                return total > 0;
            }

            return false;
        }

        public IEnumerable<Usuario_83KI> ObtenerUsuarios()
        {
            string query = "SELECT * FROM Usuarios";
            DataSet ds = _accesoDAL.Leer(query);
            List<Usuario_83KI> listaTemporal = new List<Usuario_83KI>();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    listaTemporal.Add(new Usuario_83KI
                    {
                        DNI = Convert.ToInt32(row["DNI"]),
                        Nombre = row["Nombre"].ToString(),
                        Apellido = row["Apellido"].ToString(),
                        Email = row["Email"].ToString(),
                        RolUsuario = ParsearRol(row["RolUsuario"]),
                        Bloqueado = Convert.ToBoolean(row["Bloqueado"]),
                        Intentos = Convert.ToInt32(row["Intentos"])
                    });
                }
            }
            return listaTemporal;
        }

        public void DesbloquearCuenta(Usuario_83KI usuario)
        {
            string consulta = "UPDATE Usuarios SET Bloqueado = 0, Intentos = 0, Contrasena = @contrasena WHERE DNI = @dni";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@dni", usuario.DNI),
                new SqlParameter("@contrasena", usuario.Contrasena)
            }; 
            _accesoDAL.Escribir(consulta, parametros);
        }

        public bool EstaBloqueado(int dni)
        {
            string query = "SELECT COUNT(1) AS Total FROM Usuarios WHERE DNI = @dni";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@dni",dni)
            };

            DataSet ds = _accesoDAL.Leer(query, parametros);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int total = Convert.ToInt32(ds.Tables[0].Rows[0]["Total"]);
                return total > 0;
            }
            return false;
        }

        private RolUsuario ParsearRol(object valorRol)
        {
            if (valorRol != null && Enum.TryParse(valorRol.ToString(), true, out RolUsuario rol))
            {
                return rol;
            }

            return RolUsuario.RolSimple;
        }
    }
}

