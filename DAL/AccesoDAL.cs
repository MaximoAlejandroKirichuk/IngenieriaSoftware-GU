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
        internal class AccesoDAL
        {
         
            //TODO: REVISAR
            private SqlConnection _conexion = new SqlConnection("tu_string_conexion");

            public DataTable Leer(string consulta, SqlParameter[] parametros = null)
            {
                DataTable tabla = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(consulta, _conexion))
                {
                    if (parametros != null) da.SelectCommand.Parameters.AddRange(parametros);
                    da.Fill(tabla);
                }
                return tabla;
            }

            public int Escribir(string consulta, SqlParameter[] parametros)
            {
                using (SqlCommand cmd = new SqlCommand(consulta, _conexion))
                {
                    cmd.Parameters.AddRange(parametros);
                    _conexion.Open();
                    int filas = cmd.ExecuteNonQuery();
                    _conexion.Close();
                    return filas;
                }
            }
        }
    }
}
