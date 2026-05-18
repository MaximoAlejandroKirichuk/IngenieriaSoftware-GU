using DAL.interfaces;
using Service.Entidades;
using Service.Interfaces;
using System.Collections.Generic;

namespace BLL
{
    internal class GestorRolBLL_83KI : IGestorRol_83KI
    {
        private readonly IRolDAL_83KI _rolDal;

        public GestorRolBLL_83KI(IRolDAL_83KI rolDal)
        {
            _rolDal = rolDal;
        }

        public IEnumerable<Rol_83KI> ObtenerRoles()
        {
            return _rolDal.ObtenerRoles();
        }
    }
}
