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

            private readonly string _stringConnection = "Data Source=mk\\MSSQLSERVER02;Initial Catalog=GestionUsuarios;Integrated Security=True;";

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
                            // El Fill se encarga de abrir y cerrar la conexión solo si es necesario
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
                            return cmd.ExecuteScalar(); // Devuelve solo la primera columna de la primera fila
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
                            // Por las dudas, aunque el using se encarga, el finally asegura el cierre
                            if (conn.State == ConnectionState.Open) conn.Close();
                        }
                    }
                }
            }
        }
    }
}
