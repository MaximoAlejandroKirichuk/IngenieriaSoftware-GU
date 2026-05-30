using DAL.interfaces;
using Service.Entidades;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Rol_83KI> ObtenerRolesConPermisos()
        {
            return _rolDal.ObtenerRolesConPermisos();
        }

        public IEnumerable<Familia_83KI> ObtenerFamilias()
        {
            return _rolDal.ObtenerFamilias();
        }

        public IEnumerable<Patente_83KI> ObtenerPatentes()
        {
            return _rolDal.ObtenerPatentes();
        }

        public Familia_83KI CrearFamilia(string nombre)
        {
            string nombreNormalizado = ValidarNombre(nombre);

            if (_rolDal.ObtenerFamilias().Any(f => string.Equals(f.Nombre, nombreNormalizado, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Ya existe una familia con ese nombre.");
            }

            return _rolDal.CrearFamilia(nombreNormalizado);
        }

        public void EliminarFamilia(int codigoFamilia)
        {
            if (_rolDal.FamiliaTieneDependencias(codigoFamilia))
            {
                throw new InvalidOperationException("No se puede eliminar la familia porque tiene dependencias.");
            }

            _rolDal.EliminarFamilia(codigoFamilia);
        }

        public void AsignarPatenteAFamilia(int codigoFamilia, int codigoPatente)
        {
            Familia_83KI familia = ObtenerFamilia(codigoFamilia);
            Patente_83KI patente = ObtenerPatente(codigoPatente);
            familia.Agregar(patente);
            _rolDal.AsignarPatenteAFamilia(codigoFamilia, codigoPatente);
        }

        public void QuitarPatenteDeFamilia(int codigoFamilia, int codigoPatente)
        {
            _rolDal.QuitarPatenteDeFamilia(codigoFamilia, codigoPatente);
        }

        public void AsignarFamiliaAFamilia(int codigoFamiliaPadre, int codigoFamiliaHija)
        {
            Familia_83KI familiaPadre = ObtenerFamilia(codigoFamiliaPadre);
            Familia_83KI familiaHija = ObtenerFamilia(codigoFamiliaHija);
            familiaPadre.Agregar(familiaHija);
            _rolDal.AsignarFamiliaAFamilia(codigoFamiliaPadre, codigoFamiliaHija);
        }

        public void QuitarFamiliaDeFamilia(int codigoFamiliaPadre, int codigoFamiliaHija)
        {
            _rolDal.QuitarFamiliaDeFamilia(codigoFamiliaPadre, codigoFamiliaHija);
        }

        public void AsignarPatenteARol(int codigoRol, int codigoPatente)
        {
            Rol_83KI rol = ObtenerRol(codigoRol);
            Patente_83KI patente = ObtenerPatente(codigoPatente);
            rol.Agregar(patente);
            _rolDal.AsignarPatenteARol(codigoRol, codigoPatente);
        }

        public void QuitarPatenteDeRol(int codigoRol, int codigoPatente)
        {
            _rolDal.QuitarPatenteDeRol(codigoRol, codigoPatente);
        }

        public void AsignarFamiliaARol(int codigoRol, int codigoFamilia)
        {
            Rol_83KI rol = ObtenerRol(codigoRol);
            Familia_83KI familia = ObtenerFamilia(codigoFamilia);
            rol.Agregar(familia);
            _rolDal.AsignarFamiliaARol(codigoRol, codigoFamilia);
        }

        public void QuitarFamiliaDeRol(int codigoRol, int codigoFamilia)
        {
            _rolDal.QuitarFamiliaDeRol(codigoRol, codigoFamilia);
        }

        private string ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));
            }

            return nombre.Trim();
        }

        private Rol_83KI ObtenerRol(int codigoRol)
        {
            Rol_83KI rol = _rolDal.ObtenerRolesConPermisos().FirstOrDefault(r => r.CodigoRol == codigoRol);

            if (rol == null)
            {
                throw new InvalidOperationException("El rol seleccionado no existe.");
            }

            return rol;
        }

        private Familia_83KI ObtenerFamilia(int codigoFamilia)
        {
            Familia_83KI familia = _rolDal.ObtenerFamilias().FirstOrDefault(f => f.CodigoFamilia == codigoFamilia);

            if (familia == null)
            {
                throw new InvalidOperationException("La familia seleccionada no existe.");
            }

            return familia;
        }

        private Patente_83KI ObtenerPatente(int codigoPatente)
        {
            Patente_83KI patente = _rolDal.ObtenerPatentes().FirstOrDefault(p => p.CodigoPatente == codigoPatente);

            if (patente == null)
            {
                throw new InvalidOperationException("La patente seleccionada no existe.");
            }

            return patente;
        }
    }
}
