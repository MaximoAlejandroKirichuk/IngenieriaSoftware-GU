using System.Collections.Generic;
using System.Data.SqlClient;
using Service.Interfaces;

namespace DAL
{
    /// <summary>
    /// helper dal para comandos de backup y restore de base de datos.
    /// usa composicion sobre herencia para acceder a los helpers internos de AccesoDAL_83KI.
    /// </summary>
    public class RecuperacionDAL_83KI : interfaces.IRecuperacionDAL_83KI
    {
        private readonly DAL.AccesoDAL_83KI _acceso;
        private readonly IProveedorConfiguracionConexion_83KI _settingsProvider;

        public RecuperacionDAL_83KI()
        {
            _acceso = new DAL.AccesoDAL_83KI();
        }

        public RecuperacionDAL_83KI(IProveedorConfiguracionConexion_83KI settingsProvider)
        {
            _settingsProvider = settingsProvider;
            _acceso = new DAL.AccesoDAL_83KI(settingsProvider);
        }

        private string DatabaseName
        {
            get
            {
                if (_settingsProvider != null)
                {
                    var settings = _settingsProvider.Cargar();
                    if (settings != null && !string.IsNullOrWhiteSpace(settings.NombreBaseDatos))
                        return settings.NombreBaseDatos;
                }
                return "GestionUsuarios";
            }
        }

        public void EjecutarBackup(string rutaArchivo)
        {
            string consulta = $"BACKUP DATABASE [{DatabaseName}] TO DISK = @ruta WITH INIT";
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
                "ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
                "RESTORE DATABASE [{0}] FROM DISK = N'{1}' WITH REPLACE; " +
                "ALTER DATABASE [{0}] SET MULTI_USER;",
                DatabaseName, rutaSegura);
            _acceso.EjecutarEnMaster(consulta);
        }
    }
}
