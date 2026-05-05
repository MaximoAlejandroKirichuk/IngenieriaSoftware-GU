using Service.Entidades;
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

        public Usuario_83KI ObtenerPorDni(int dni)
        {
            string sql = "SELECT * FROM Usuarios WHERE DNI = @dni";
            var parametros = new List<SqlParameter> { new SqlParameter("@dni", dni) };

            DataSet ds = _accesoDAL.Leer(sql, parametros);
            return MapearUsuario(ds);
        }

        public Usuario_83KI ObtenerPorUserName(string userName)
        {
            string sql = "SELECT * FROM Usuarios WHERE UserName = @userName";
            var parametros = new List<SqlParameter> { new SqlParameter("@userName", userName) };

            DataSet ds = _accesoDAL.Leer(sql, parametros);
            return MapearUsuario(ds);
        }



        public void BloquearUsuario(Usuario_83KI usuario)
        {
            string consulta = "UPDATE Usuarios SET Bloqueado = 1 WHERE DNI = @dni";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@dni", usuario.DNI)
            };

            _accesoDAL.Escribir(consulta, parametros);
        }

        public void CrearUsuario(Usuario_83KI usuario)
        {
            string consulta = @"INSERT INTO Usuarios (UserName, Nombre, Apellido, DNI, Email, RolUsuario, Contrasena, Activo) 
                        VALUES (@userName ,@nombre, @apellido, @dni, @email, @rol, @pass, @activo)";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@username", usuario.UserName),
                new SqlParameter("@nombre", usuario.Nombre),
                new SqlParameter("@apellido", usuario.Apellido),
                new SqlParameter("@dni",usuario.DNI),
                new SqlParameter("@email", usuario.Email),
                new SqlParameter("@rol", usuario.RolUsuario.ToString()), // Guardamos el nombre del Enum
                new SqlParameter("@pass", usuario.Contrasena),
                new SqlParameter("@activo", usuario.Activo)
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
                    listaTemporal.Add(MapearUsuario(row));
                }
            }
            return listaTemporal;
        }

        public void DesbloquearCuenta(Usuario_83KI usuario)
        {
            string consulta = "UPDATE Usuarios SET Bloqueado = 0, Contrasena = @contrasena WHERE DNI = @dni";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@dni", usuario.DNI),
                new SqlParameter("@contrasena", usuario.Contrasena)
            }; 
            _accesoDAL.Escribir(consulta, parametros);
        }

        public void ActualizarEstadoActivo(int dni, bool activo)
        {
            string consulta = "UPDATE Usuarios SET Activo = @activo WHERE DNI = @dni";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@dni", dni),
                new SqlParameter("@activo", activo)
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

        private Usuario_83KI MapearUsuario(DataSet ds)
        {
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            return MapearUsuario(ds.Tables[0].Rows[0]);
        }

        private Usuario_83KI MapearUsuario(DataRow row)
        {
            return Usuario_83KI.ReconstruirDesdePersistencia(
                (int)Convert.ToInt64(row["DNI"]),
                ObtenerTexto(row, "Nombre"),
                ObtenerTexto(row, "Apellido"),
                ObtenerTexto(row, "Email"),
                ObtenerTexto(row, "Contrasena"),
                ParsearRol(row["RolUsuario"]),
                Convert.ToBoolean(row["Activo"]),
                Convert.ToBoolean(row["Bloqueado"])
            );
        }

        private string ObtenerTexto(DataRow row, string columna)
        {
            if (!row.Table.Columns.Contains(columna) || row[columna] == DBNull.Value)
            {
                return string.Empty;
            }

            return row[columna].ToString();
        }
    }
}

