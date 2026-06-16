using DAL.interfaces;
using Service;
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
        private readonly ISessionManager_83KI _sessionManager;

        public GestorRolBLL_83KI(IRolDAL_83KI rolDal)
            : this(rolDal, SessionManager_83KI.Instancia)
        {
        }

        public GestorRolBLL_83KI(IRolDAL_83KI rolDal, ISessionManager_83KI sessionManager)
        {
            _rolDal = rolDal;
            _sessionManager = sessionManager;
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

        public Rol_83KI CrearRol(string nombre, int codigoComponenteInicial, bool esFamilia)
        {
            ValidarPermiso(PermisoSistema_83KI.GestionRoles);

            string nombreNormalizado = ValidarNombre(nombre);

            if (_rolDal.ObtenerRoles().Any(r => string.Equals(r.Nombre, nombreNormalizado, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Ya existe un rol con ese nombre.");
            }

            
            if (esFamilia)
                ObtenerFamilia(codigoComponenteInicial);
            else
                ObtenerPatente(codigoComponenteInicial);

            Rol_83KI rol = _rolDal.CrearRol(nombreNormalizado);

            if (esFamilia)
                _rolDal.AsignarFamiliaARol(rol.CodigoRol, codigoComponenteInicial);
            else
                _rolDal.AsignarPatenteARol(rol.CodigoRol, codigoComponenteInicial);

            return rol;
        }

        public Familia_83KI CrearFamilia(string nombre, int codigoPatenteInicial)
        {
            ValidarPermiso(PermisoSistema_83KI.CrearFamilia);

            string nombreNormalizado = ValidarNombre(nombre);

            if (_rolDal.ObtenerFamilias().Any(f => string.Equals(f.Nombre, nombreNormalizado, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Ya existe una familia con ese nombre.");
            }

            ObtenerPatente(codigoPatenteInicial);

            Familia_83KI familia = _rolDal.CrearFamilia(nombreNormalizado);
            _rolDal.AsignarPatenteAFamilia(familia.CodigoFamilia, codigoPatenteInicial);

            return familia;
        }

        public void EliminarFamilia(int codigoFamilia)
        {
            ValidarPermiso(PermisoSistema_83KI.EliminarFamilia);

            if (_rolDal.FamiliaAsignadaARol(codigoFamilia))
            {
                throw new InvalidOperationException("No se puede eliminar la familia porque está asignada a uno o más roles.");
            }

            _rolDal.EliminarFamilia(codigoFamilia);
        }

        public void EliminarRol(int codigoRol)
        {
            ValidarPermiso(PermisoSistema_83KI.EliminarRol);

            if (_rolDal.RolTieneUsuarios(codigoRol))
            {
                throw new InvalidOperationException("El rol tiene usuarios asignados.");
            }

            _rolDal.EliminarRol(codigoRol);
        }

        public void AsignarPatenteAFamilia(int codigoFamilia, int codigoPatente)
        {
            ValidarPermiso(PermisoSistema_83KI.AgregarPatenteFamilia);

            Familia_83KI familia = ObtenerFamilia(codigoFamilia);
            Patente_83KI patente = ObtenerPatente(codigoPatente);
            familia.Agregar(patente);
            _rolDal.AsignarPatenteAFamilia(codigoFamilia, codigoPatente);
        }

        public void QuitarPatenteDeFamilia(int codigoFamilia, int codigoPatente)
        {
            ValidarPermiso(PermisoSistema_83KI.QuitarPatenteFamilia);

            Familia_83KI familia = ObtenerFamilia(codigoFamilia);

            bool hayOtrasPatentesDirectas = familia.Hijos.OfType<Patente_83KI>()
                .Any(p => p.CodigoPatente != codigoPatente);
            bool haySubfamiliasConPatentes = familia.Hijos.OfType<Familia_83KI>()
                .Any(f => f.ObtenerPatentes().Any());

            if (!hayOtrasPatentesDirectas && !haySubfamiliasConPatentes)
            {
                throw new InvalidOperationException("La familia debe contener al menos una patente.");
            }

            _rolDal.QuitarPatenteDeFamilia(codigoFamilia, codigoPatente);
        }

        public void AsignarFamiliaAFamilia(int codigoFamiliaPadre, int codigoFamiliaHija)
        {
            ValidarPermiso(PermisoSistema_83KI.AgregarSubfamilia);

            Familia_83KI familiaPadre = ObtenerFamilia(codigoFamiliaPadre);
            Familia_83KI familiaHija = ObtenerFamilia(codigoFamiliaHija);
            familiaPadre.Agregar(familiaHija);
            _rolDal.AsignarFamiliaAFamilia(codigoFamiliaPadre, codigoFamiliaHija);
        }

        public void QuitarFamiliaDeFamilia(int codigoFamiliaPadre, int codigoFamiliaHija)
        {
            ValidarPermiso(PermisoSistema_83KI.QuitarSubfamilia);

            Familia_83KI familiaPadre = ObtenerFamilia(codigoFamiliaPadre);

            bool hayPatentesDirectas = familiaPadre.Hijos.OfType<Patente_83KI>().Any();
            bool hayOtrasSubfamiliasConPatentes = familiaPadre.Hijos.OfType<Familia_83KI>()
                .Any(f => f.CodigoFamilia != codigoFamiliaHija && f.ObtenerPatentes().Any());

            if (!hayPatentesDirectas && !hayOtrasSubfamiliasConPatentes)
            {
                throw new InvalidOperationException("La familia debe contener al menos una patente.");
            }

            _rolDal.QuitarFamiliaDeFamilia(codigoFamiliaPadre, codigoFamiliaHija);
        }

        public void AsignarPatenteARol(int codigoRol, int codigoPatente)
        {
            ValidarPermiso(PermisoSistema_83KI.AsignarPatenteRol);

            Rol_83KI rol = ObtenerRol(codigoRol);
            Patente_83KI patente = ObtenerPatente(codigoPatente);
            rol.AgregarPatente(patente);
            _rolDal.AsignarPatenteARol(codigoRol, codigoPatente);
        }

        public void QuitarPatenteDeRol(int codigoRol, int codigoPatente)
        {
            ValidarPermiso(PermisoSistema_83KI.QuitarPatenteRol);

            Rol_83KI rol = ObtenerRol(codigoRol);

            bool hayOtrasPatentesDirectas = rol.PatentesDirectas
                .Any(p => p.CodigoPatente != codigoPatente);
            bool hayFamilias = rol.Familias.Any();

            if (!hayOtrasPatentesDirectas && !hayFamilias)
            {
                throw new InvalidOperationException("El rol debe contener al menos una patente o familia.");
            }

            _rolDal.QuitarPatenteDeRol(codigoRol, codigoPatente);
        }

        public void AsignarFamiliaARol(int codigoRol, int codigoFamilia)
        {
            ValidarPermiso(PermisoSistema_83KI.AgregarFamiliaRol);

            Rol_83KI rol = ObtenerRol(codigoRol);
            Familia_83KI familia = ObtenerFamilia(codigoFamilia);
            rol.AgregarFamilia(familia);
            _rolDal.AsignarFamiliaARol(codigoRol, codigoFamilia);
        }

        public void QuitarFamiliaDeRol(int codigoRol, int codigoFamilia)
        {
            ValidarPermiso(PermisoSistema_83KI.QuitarFamiliaRol);

            Rol_83KI rol = ObtenerRol(codigoRol);

            bool hayOtrasFamilias = rol.Familias
                .Any(f => f.CodigoFamilia != codigoFamilia);
            bool hayPatentesDirectas = rol.PatentesDirectas.Any();

            if (!hayOtrasFamilias && !hayPatentesDirectas)
            {
                throw new InvalidOperationException("El rol debe contener al menos una patente o familia.");
            }

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

        private void ValidarPermiso(PermisoSistema_83KI permiso)
        {
            if (!_sessionManager.TienePermiso(permiso))
            {
                throw new InvalidOperationException("No tiene permisos para realizar esta accion.");
            }
        }
    }
}
