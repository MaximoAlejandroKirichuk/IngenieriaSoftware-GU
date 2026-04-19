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
    public class BitacoraEventoDAL : IBitacoraDAL
    {
        private readonly AccesoDAL _acceso = new AccesoDAL();

        public void Registrar(BitacoraEvento evento)
        {
            string consulta = "INSERT INTO EVENTOS (Criticidad, Descripcion, DNI, Fecha, Modulo) " +
                              "VALUES (@crit, @desc, @dni, @fecha, @mod)";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@crit", evento.Criticidad),
                new SqlParameter("@desc", evento.Descripcion),
                new SqlParameter("@dni", evento.DNI),
                new SqlParameter("@fecha", evento.Fecha),
                new SqlParameter("@mod", evento.Modulo)
            };

            _acceso.Escribir(consulta, parametros);
        }

        public IEnumerable<BitacoraEvento> Consultar(DateTime desde, DateTime hasta, string modulo = null, int? criticidad = null)
        {
            List<BitacoraEvento> lista = new List<BitacoraEvento>();
            string consultaBase = "SELECT * FROM EVENTOS WHERE Fecha BETWEEN @desde AND @hasta";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@desde", desde),
                new SqlParameter("@hasta", hasta)
            };

            if (!string.IsNullOrEmpty(modulo))
            {
                consultaBase += " AND Modulo = @mod";
                parametros.Add(new SqlParameter("@mod", modulo));
            }

            DataSet ds = _acceso.Leer(consultaBase, parametros);

            if (ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lista.Add(new BitacoraEvento
                    {
                        Id = Convert.ToInt32(row["Id"]), // Si tenés el ID en la tabla
                        DNI = Convert.ToInt64(row["DNI"]),
                        Fecha = Convert.ToDateTime(row["Fecha"]),
                        Descripcion = row["Descripcion"].ToString(),
                        Criticidad = Convert.ToInt32(row["Criticidad"]),
                        //es un enum
                        //Modulo = row["Modulo"].ToString()
                    });
                }
            }

            return lista;
        }
    }
}