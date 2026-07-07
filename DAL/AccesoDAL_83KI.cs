using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    namespace DAL
    {
        internal class AccesoDAL_83KI
        {

            private readonly string _stringConnection = "Data Source=MK\\MSSQLSERVER02;Initial Catalog=GestionUsuarios;Integrated Security=True;";

            // cadena de conexion a master para RESTORE DATABASE (la base destino se vuelve inaccesible).
            // se deriva de la cadena principal reemplazando el catalogo.
            private string _masterConnectionString
            {
                get
                {
                    var builder = new System.Data.SqlClient.SqlConnectionStringBuilder(_stringConnection);
                    builder.InitialCatalog = "master";
                    return builder.ConnectionString;
                }
            }

            internal void EjecutarTransaccion(Action<SqlConnection, SqlTransaction> operacion)
            {
                using (SqlConnection conn = new SqlConnection(_stringConnection))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            operacion(conn, tran);
                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }
            }

            internal static object LeerEscalarTransaccional(SqlConnection conn, SqlTransaction tran, string consulta, List<SqlParameter> parametros = null)
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conn, tran))
                {
                    if (parametros != null)
                    {
                        cmd.Parameters.AddRange(parametros.ToArray());
                    }

                    return cmd.ExecuteScalar();
                }
            }

            internal static int EscribirTransaccional(SqlConnection conn, SqlTransaction tran, string consulta, List<SqlParameter> parametros)
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conn, tran))
                {
                    if (parametros != null)
                    {
                        cmd.Parameters.AddRange(parametros.ToArray());
                    }

                    return cmd.ExecuteNonQuery();
                }
            }

            public DataSet Leer(string consulta, List<SqlParameter> parametros = null)
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_stringConnection))
                {
                    using (SqlCommand cmd = new SqlCommand(consulta, conn))
                    {
                        if (parametros != null)
                        {
                            cmd.Parameters.AddRange(parametros.ToArray());
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        try
                        {
                            // el fill se encarga de abrir y cerrar la conexion solo si es necesario
                            da.Fill(ds);
                        }
                        catch (SqlException ex)
                        {
                            throw new Exception("Error de lectura en la base de datos", ex);
                        }
                    }
                }
                return ds;
            }

            public object LeerEscalar(string consulta, List<SqlParameter> parametros = null)
            {
                using (SqlConnection conn = new SqlConnection(_stringConnection))
                {
                    using (SqlCommand cmd = new SqlCommand(consulta, conn))
                    {
                        if (parametros != null)
                        {
                            cmd.Parameters.AddRange(parametros.ToArray());
                        }

                        try
                        {
                            conn.Open();
                            return cmd.ExecuteScalar(); // devuelve solo la primera columna de la primera fila
                        }
                        catch (SqlException ex)
                        {
                            throw new Exception("Error al obtener valor escalar", ex);
                        }
                    }
                }
            }

            public int Escribir(string consulta, List<SqlParameter> parametros)
            {
                using (SqlConnection conn = new SqlConnection(_stringConnection))
                {
                    using (SqlCommand cmd = new SqlCommand(consulta, conn))
                    {
                        if (parametros != null)
                        {
                            cmd.Parameters.AddRange(parametros.ToArray());
                        }

                        try
                        {
                            conn.Open();
                            return cmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            throw new Exception("Error al escribir en la base de datos", ex);
                        }
                        finally
                        {
                            // por las dudas, aunque el using se encarga, el finally asegura el cierre
                            if (conn.State == ConnectionState.Open) conn.Close();
                        }
                    }
                }
            }

            /// <summary>
            /// ejecuta un comando contra la base master.
            /// se usa para RESTORE DATABASE ya que la base destino se vuelve inaccesible.
            /// </summary>
            internal void EjecutarEnMaster(string consulta)
            {
                using (SqlConnection conn = new SqlConnection(_masterConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(consulta, conn))
                    {
                        cmd.CommandTimeout = 300; // el restore puede tardar
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            /// <summary>
            /// lee datos dentro de una transaccion existente y devuelve un DataSet.
            /// </summary>
            internal static DataSet LeerTransaccional(
                SqlConnection conn,
                SqlTransaction tran,
                string consulta,
                List<SqlParameter> parametros = null)
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand(consulta, conn, tran))
                {
                    if (parametros != null)
                        cmd.Parameters.AddRange(parametros.ToArray());

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
                return ds;
            }
        }
    }
}
