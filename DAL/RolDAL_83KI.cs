using DAL.DAL;
using DAL.interfaces;
using Service.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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

        public IEnumerable<Rol_83KI> ObtenerRolesConPermisos()
        {
            Dictionary<int, Rol_83KI> roles = ObtenerRoles().ToDictionary(r => r.CodigoRol);
            Dictionary<int, Familia_83KI> familias = ObtenerFamilias().ToDictionary(f => f.CodigoFamilia);
            Dictionary<int, Patente_83KI> patentes = ObtenerPatentes().ToDictionary(p => p.CodigoPatente);

            CargarRelacionesFamilias(familias, patentes);
            CargarRelacionesRoles(roles, familias, patentes);

            return roles.Values.OrderBy(r => r.Nombre).ToList();
        }

        public IEnumerable<Familia_83KI> ObtenerFamilias()
        {
            string query = "SELECT CodigoFamilia, Nombre FROM Familias ORDER BY Nombre";
            DataSet ds = _accesoDAL.Leer(query);
            Dictionary<int, Familia_83KI> familias = new Dictionary<int, Familia_83KI>();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Familia_83KI familia = MapearFamilia(row);
                    familias[familia.CodigoFamilia] = familia;
                }
            }

            Dictionary<int, Patente_83KI> patentes = ObtenerPatentes().ToDictionary(p => p.CodigoPatente);
            CargarRelacionesFamilias(familias, patentes);
            return familias.Values.OrderBy(f => f.Nombre).ToList();
        }

        public IEnumerable<Patente_83KI> ObtenerPatentes()
        {
            string query = "SELECT CodigoPatente, Nombre FROM Patentes ORDER BY Nombre";
            DataSet ds = _accesoDAL.Leer(query);
            List<Patente_83KI> patentes = new List<Patente_83KI>();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    patentes.Add(MapearPatente(row));
                }
            }

            return patentes;
        }

        public Rol_83KI CrearRol(string nombre)
        {
            string consulta = @"INSERT INTO Roles (Nombre)
                                OUTPUT INSERTED.CodigoRol
                                VALUES (@nombre)";
            object codigo = _accesoDAL.LeerEscalar(consulta, new List<SqlParameter> { new SqlParameter("@nombre", nombre) });
            return new Rol_83KI(Convert.ToInt32(codigo), nombre);
        }

        public Familia_83KI CrearFamilia(string nombre)
        {
            string consulta = @"INSERT INTO Familias (Nombre)
                                OUTPUT INSERTED.CodigoFamilia
                                VALUES (@nombre)";
            object codigo = _accesoDAL.LeerEscalar(consulta, new List<SqlParameter> { new SqlParameter("@nombre", nombre) });
            return new Familia_83KI(Convert.ToInt32(codigo), nombre);
        }

        public Rol_83KI CrearRolConComponentes(string nombre, List<int> codigosPatentes, List<int> codigosFamilias)
        {
            Rol_83KI rol = null;

            _accesoDAL.EjecutarTransaccion((conn, tran) =>
            {
                // Crear el rol
                string consultaRol = @"INSERT INTO Roles (Nombre)
                                       OUTPUT INSERTED.CodigoRol
                                       VALUES (@nombre)";
                object codigo = AccesoDAL_83KI.LeerEscalarTransaccional(conn, tran, consultaRol,
                    new List<SqlParameter> { new SqlParameter("@nombre", nombre) });
                int codigoRol = Convert.ToInt32(codigo);
                rol = new Rol_83KI(codigoRol, nombre);

                // Asignar patentes
                if (codigosPatentes != null)
                {
                    foreach (int codigoPatente in codigosPatentes)
                    {
                        AccesoDAL_83KI.EscribirTransaccional(conn, tran,
                            "INSERT INTO RolPatente (CodigoRol, CodigoPatente) VALUES (@codigoRol, @codigoPatente)",
                            new List<SqlParameter>
                            {
                                new SqlParameter("@codigoRol", codigoRol),
                                new SqlParameter("@codigoPatente", codigoPatente)
                            });
                    }
                }

                // Asignar familias
                if (codigosFamilias != null)
                {
                    foreach (int codigoFamilia in codigosFamilias)
                    {
                        AccesoDAL_83KI.EscribirTransaccional(conn, tran,
                            "INSERT INTO RolFamilia (CodigoRol, CodigoFamilia) VALUES (@codigoRol, @codigoFamilia)",
                            new List<SqlParameter>
                            {
                                new SqlParameter("@codigoRol", codigoRol),
                                new SqlParameter("@codigoFamilia", codigoFamilia)
                            });
                    }
                }
            });

            return rol;
        }

        public Familia_83KI CrearFamiliaConComponentes(string nombre, List<int> codigosPatentes, List<int> codigosFamilias)
        {
            Familia_83KI familia = null;

            _accesoDAL.EjecutarTransaccion((conn, tran) =>
            {
                // Crear la familia
                string consultaFamilia = @"INSERT INTO Familias (Nombre)
                                           OUTPUT INSERTED.CodigoFamilia
                                           VALUES (@nombre)";
                object codigo = AccesoDAL_83KI.LeerEscalarTransaccional(conn, tran, consultaFamilia,
                    new List<SqlParameter> { new SqlParameter("@nombre", nombre) });
                int codigoFamilia = Convert.ToInt32(codigo);
                familia = new Familia_83KI(codigoFamilia, nombre);

                // Asignar patentes
                if (codigosPatentes != null)
                {
                    foreach (int codigoPatente in codigosPatentes)
                    {
                        AccesoDAL_83KI.EscribirTransaccional(conn, tran,
                            "INSERT INTO FamiliaPatente (CodigoFamilia, CodigoPatente) VALUES (@codigoFamilia, @codigoPatente)",
                            new List<SqlParameter>
                            {
                                new SqlParameter("@codigoFamilia", codigoFamilia),
                                new SqlParameter("@codigoPatente", codigoPatente)
                            });
                    }
                }

                // Asignar subfamilias
                if (codigosFamilias != null)
                {
                    foreach (int codigoFamiliaHija in codigosFamilias)
                    {
                        AccesoDAL_83KI.EscribirTransaccional(conn, tran,
                            "INSERT INTO FamiliaFamilia (CodigoFamiliaPadre, CodigoFamiliaHija) VALUES (@codigoFamiliaPadre, @codigoFamiliaHija)",
                            new List<SqlParameter>
                            {
                                new SqlParameter("@codigoFamiliaPadre", codigoFamilia),
                                new SqlParameter("@codigoFamiliaHija", codigoFamiliaHija)
                            });
                    }
                }
            });

            return familia;
        }

        public void EliminarFamilia(int codigoFamilia)
        {
            _accesoDAL.Escribir(
                "DELETE FROM FamiliaPatente WHERE CodigoFamilia = @codigoFamilia",
                new List<SqlParameter> { new SqlParameter("@codigoFamilia", codigoFamilia) });
            _accesoDAL.Escribir(
                "DELETE FROM FamiliaFamilia WHERE CodigoFamiliaPadre = @codigoFamilia OR CodigoFamiliaHija = @codigoFamilia",
                new List<SqlParameter> { new SqlParameter("@codigoFamilia", codigoFamilia) });
            _accesoDAL.Escribir(
                "DELETE FROM Familias WHERE CodigoFamilia = @codigoFamilia",
                new List<SqlParameter> { new SqlParameter("@codigoFamilia", codigoFamilia) });
        }

        public bool FamiliaAsignadaARol(int codigoFamilia)
        {
            string consulta = "SELECT COUNT(1) FROM RolFamilia WHERE CodigoFamilia = @codigoFamilia";

            object total = _accesoDAL.LeerEscalar(
                consulta,
                new List<SqlParameter> { new SqlParameter("@codigoFamilia", codigoFamilia) });

            return Convert.ToInt32(total) > 0;
        }

        public bool FamiliaEsSubfamiliaDeOtra(int codigoFamilia)
        {
            string consulta = "SELECT COUNT(1) FROM FamiliaFamilia WHERE CodigoFamiliaHija = @codigoFamilia";

            object total = _accesoDAL.LeerEscalar(
                consulta,
                new List<SqlParameter> { new SqlParameter("@codigoFamilia", codigoFamilia) });

            return Convert.ToInt32(total) > 0;
        }

        public bool RolTieneUsuarios(int codigoRol)
        {
            string consulta = "SELECT COUNT(1) FROM Usuarios WHERE CodigoRol = @codigoRol";
            object total = _accesoDAL.LeerEscalar(
                consulta,
                new List<SqlParameter> { new SqlParameter("@codigoRol", codigoRol) });
            return Convert.ToInt32(total) > 0;
        }

        public void EliminarRol(int codigoRol)
        {
            _accesoDAL.Escribir(
                "DELETE FROM RolPatente WHERE CodigoRol = @codigoRol",
                new List<SqlParameter> { new SqlParameter("@codigoRol", codigoRol) });
            _accesoDAL.Escribir(
                "DELETE FROM RolFamilia WHERE CodigoRol = @codigoRol",
                new List<SqlParameter> { new SqlParameter("@codigoRol", codigoRol) });
            _accesoDAL.Escribir(
                "DELETE FROM Roles WHERE CodigoRol = @codigoRol",
                new List<SqlParameter> { new SqlParameter("@codigoRol", codigoRol) });
        }

        public void AsignarPatenteAFamilia(int codigoFamilia, int codigoPatente)
        {
            InsertarRelacion(
                "INSERT INTO FamiliaPatente (CodigoFamilia, CodigoPatente) VALUES (@codigoFamilia, @codigoPatente)",
                new SqlParameter("@codigoFamilia", codigoFamilia),
                new SqlParameter("@codigoPatente", codigoPatente));
        }

        public void QuitarPatenteDeFamilia(int codigoFamilia, int codigoPatente)
        {
            BorrarRelacion(
                "DELETE FROM FamiliaPatente WHERE CodigoFamilia = @codigoFamilia AND CodigoPatente = @codigoPatente",
                new SqlParameter("@codigoFamilia", codigoFamilia),
                new SqlParameter("@codigoPatente", codigoPatente));
        }

        public void AsignarFamiliaAFamilia(int codigoFamiliaPadre, int codigoFamiliaHija)
        {
            InsertarRelacion(
                "INSERT INTO FamiliaFamilia (CodigoFamiliaPadre, CodigoFamiliaHija) VALUES (@codigoFamiliaPadre, @codigoFamiliaHija)",
                new SqlParameter("@codigoFamiliaPadre", codigoFamiliaPadre),
                new SqlParameter("@codigoFamiliaHija", codigoFamiliaHija));
        }

        public void QuitarFamiliaDeFamilia(int codigoFamiliaPadre, int codigoFamiliaHija)
        {
            BorrarRelacion(
                "DELETE FROM FamiliaFamilia WHERE CodigoFamiliaPadre = @codigoFamiliaPadre AND CodigoFamiliaHija = @codigoFamiliaHija",
                new SqlParameter("@codigoFamiliaPadre", codigoFamiliaPadre),
                new SqlParameter("@codigoFamiliaHija", codigoFamiliaHija));
        }

        public void AsignarPatenteARol(int codigoRol, int codigoPatente)
        {
            InsertarRelacion(
                "INSERT INTO RolPatente (CodigoRol, CodigoPatente) VALUES (@codigoRol, @codigoPatente)",
                new SqlParameter("@codigoRol", codigoRol),
                new SqlParameter("@codigoPatente", codigoPatente));
        }

        public void QuitarPatenteDeRol(int codigoRol, int codigoPatente)
        {
            BorrarRelacion(
                "DELETE FROM RolPatente WHERE CodigoRol = @codigoRol AND CodigoPatente = @codigoPatente",
                new SqlParameter("@codigoRol", codigoRol),
                new SqlParameter("@codigoPatente", codigoPatente));
        }

        public void AsignarFamiliaARol(int codigoRol, int codigoFamilia)
        {
            InsertarRelacion(
                "INSERT INTO RolFamilia (CodigoRol, CodigoFamilia) VALUES (@codigoRol, @codigoFamilia)",
                new SqlParameter("@codigoRol", codigoRol),
                new SqlParameter("@codigoFamilia", codigoFamilia));
        }

        public void QuitarFamiliaDeRol(int codigoRol, int codigoFamilia)
        {
            BorrarRelacion(
                "DELETE FROM RolFamilia WHERE CodigoRol = @codigoRol AND CodigoFamilia = @codigoFamilia",
                new SqlParameter("@codigoRol", codigoRol),
                new SqlParameter("@codigoFamilia", codigoFamilia));
        }

        private Rol_83KI MapearRol(DataRow row)
        {
            return new Rol_83KI(
                Convert.ToInt32(row["CodigoRol"]),
                ObtenerTexto(row, "Nombre")
            );
        }

        private Familia_83KI MapearFamilia(DataRow row)
        {
            return new Familia_83KI(
                Convert.ToInt32(row["CodigoFamilia"]),
                ObtenerTexto(row, "Nombre")
            );
        }

        private Patente_83KI MapearPatente(DataRow row)
        {
            return new Patente_83KI(
                Convert.ToInt32(row["CodigoPatente"]),
                ObtenerTexto(row, "Nombre")
            );
        }

        private void CargarRelacionesFamilias(Dictionary<int, Familia_83KI> familias, Dictionary<int, Patente_83KI> patentes)
        {
            DataSet dsPatentes = _accesoDAL.Leer("SELECT CodigoFamilia, CodigoPatente FROM FamiliaPatente");

            if (dsPatentes != null && dsPatentes.Tables.Count > 0)
            {
                foreach (DataRow row in dsPatentes.Tables[0].Rows)
                {
                    int codigoFamilia = Convert.ToInt32(row["CodigoFamilia"]);
                    int codigoPatente = Convert.ToInt32(row["CodigoPatente"]);

                    if (familias.ContainsKey(codigoFamilia) && patentes.ContainsKey(codigoPatente))
                    {
                        familias[codigoFamilia].CargarHijoDesdePersistencia(patentes[codigoPatente]);
                    }
                }
            }

            DataSet dsFamilias = _accesoDAL.Leer("SELECT CodigoFamiliaPadre, CodigoFamiliaHija FROM FamiliaFamilia");

            if (dsFamilias != null && dsFamilias.Tables.Count > 0)
            {
                foreach (DataRow row in dsFamilias.Tables[0].Rows)
                {
                    int codigoPadre = Convert.ToInt32(row["CodigoFamiliaPadre"]);
                    int codigoHija = Convert.ToInt32(row["CodigoFamiliaHija"]);

                    if (familias.ContainsKey(codigoPadre) && familias.ContainsKey(codigoHija))
                    {
                        familias[codigoPadre].CargarHijoDesdePersistencia(familias[codigoHija]);
                    }
                }
            }
        }

        private void CargarRelacionesRoles(Dictionary<int, Rol_83KI> roles, Dictionary<int, Familia_83KI> familias, Dictionary<int, Patente_83KI> patentes)
        {
            DataSet dsPatentes = _accesoDAL.Leer("SELECT CodigoRol, CodigoPatente FROM RolPatente");

            if (dsPatentes != null && dsPatentes.Tables.Count > 0)
            {
                foreach (DataRow row in dsPatentes.Tables[0].Rows)
                {
                    int codigoRol = Convert.ToInt32(row["CodigoRol"]);
                    int codigoPatente = Convert.ToInt32(row["CodigoPatente"]);

                    if (roles.ContainsKey(codigoRol) && patentes.ContainsKey(codigoPatente))
                    {
                        roles[codigoRol].CargarPatenteDesdePersistencia(patentes[codigoPatente]);
                    }
                }
            }

            DataSet dsFamilias = _accesoDAL.Leer("SELECT CodigoRol, CodigoFamilia FROM RolFamilia");

            if (dsFamilias != null && dsFamilias.Tables.Count > 0)
            {
                foreach (DataRow row in dsFamilias.Tables[0].Rows)
                {
                    int codigoRol = Convert.ToInt32(row["CodigoRol"]);
                    int codigoFamilia = Convert.ToInt32(row["CodigoFamilia"]);

                    if (roles.ContainsKey(codigoRol) && familias.ContainsKey(codigoFamilia))
                    {
                        roles[codigoRol].CargarFamiliaDesdePersistencia(familias[codigoFamilia]);
                    }
                }
            }
        }

        private void InsertarRelacion(string consulta, params SqlParameter[] parametros)
        {
            _accesoDAL.Escribir(consulta, parametros.ToList());
        }

        private void BorrarRelacion(string consulta, params SqlParameter[] parametros)
        {
            _accesoDAL.Escribir(consulta, parametros.ToList());
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
