using DAL.DAL;
using DAL.interfaces;
using Service.Entidades;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class RolDAL_83KI : IRolDAL_83KI
    {
        private readonly AccesoDAL_83KI _accesoDAL = new AccesoDAL_83KI();

        public IEnumerable<Rol_83KI> ObtenerRoles()
        {
            string query = "SELECT CodigoRol, Nombre FROM Roles ORDER BY Nombre";
            DataSet ds = _accesoDAL.Leer(query);
            List<Rol_83KI> roles = new List<Rol_83KI>();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    roles.Add(MapearRol(row));
                }
            }

            return roles;
        }

        private Rol_83KI MapearRol(DataRow row)
        {
            return new Rol_83KI(
                Convert.ToInt32(row["CodigoRol"]),
                ObtenerTexto(row, "Nombre")
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
