using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// helper dal para comandos de backup y restore de base de datos.
    /// usa composicion sobre herencia para acceder a los helpers internos de AccesoDAL_83KI.
    /// </summary>
    public class RecuperacionDAL_83KI : interfaces.IRecuperacionDAL_83KI
    {
        private readonly DAL.AccesoDAL_83KI _acceso = new DAL.AccesoDAL_83KI();

        public void EjecutarBackup(string rutaArchivo)
        {
            string consulta = "BACKUP DATABASE [GestionUsuarios] TO DISK = @ruta WITH INIT";
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@ruta", rutaArchivo)
            };
            _acceso.Escribir(consulta, parametros);
        }

        public void EjecutarRestore(string rutaArchivo)
        {
            // escapa comillas simples en la ruta del archivo para incrustacion segura en sql
            string rutaSegura = rutaArchivo.Replace("'", "''");
            string consulta = string.Format(
                "ALTER DATABASE [GestionUsuarios] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
                "RESTORE DATABASE [GestionUsuarios] FROM DISK = N'{0}' WITH REPLACE; " +
                "ALTER DATABASE [GestionUsuarios] SET MULTI_USER;",
                rutaSegura);
            _acceso.EjecutarEnMaster(consulta);
        }
    }
}
