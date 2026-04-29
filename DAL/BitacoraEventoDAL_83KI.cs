using DAL.DAL;
using DAL.interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Service.Entidades;

namespace DAL
{
    public class BitacoraEventoDAL_83KI : IBitacoraDAL_83KI
    {
        private readonly AccesoDAL_83KI _acceso = new AccesoDAL_83KI();

        public void Registrar(BitacoraEvento_83KI evento)
        {
            string consulta = "INSERT INTO BitacoraEventos (Criticidad, Descripcion, Fecha, Modulo, Username) " +
                              "VALUES (@crit, @desc, @fecha, @mod, @username)";

            List<SqlParameter> parametros = new List<SqlParameter>
            {
                new SqlParameter("@crit", evento.Criticidad),
                new SqlParameter("@desc", evento.Descripcion),
                new SqlParameter("@fecha", evento.Fecha),
                new SqlParameter("@mod", evento.Modulo),
                new SqlParameter("@username", evento.Username)

            };

            _acceso.Escribir(consulta, parametros);
        }

        public IEnumerable<BitacoraEvento_83KI> Consultar(DateTime desde, DateTime hasta, string modulo = null, int? criticidad = null)
        {
            List<BitacoraEvento_83KI> lista = new List<BitacoraEvento_83KI>();
            string consultaBase = "SELECT * FROM BitacoraEventos WHERE Fecha BETWEEN @desde AND @hasta";

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
                    lista.Add(new BitacoraEvento_83KI
                    {
                        Id = Convert.ToInt32(row["Id"]),
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
