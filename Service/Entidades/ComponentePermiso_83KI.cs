using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Entidades
{
    public abstract class ComponentePermiso_83KI
    {
        private readonly List<ComponentePermiso_83KI> _hijos = new List<ComponentePermiso_83KI>();

        public int Codigo { get; private set; }
        public string Nombre { get; private set; }
        public IEnumerable<ComponentePermiso_83KI> Hijos => _hijos.AsReadOnly();

        protected ComponentePermiso_83KI(int codigo, string nombre)
        {
            if (codigo <= 0)
            {
                throw new ArgumentException("El codigo debe ser valido.", nameof(codigo));
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));
            }

            Codigo = codigo;
            Nombre = nombre.Trim();
        }

        public virtual void Agregar(ComponentePermiso_83KI componente)
        {
            if (componente == null)
            {
                throw new ArgumentException("El componente es obligatorio.", nameof(componente));
            }

            if (ReferenceEquals(this, componente) || componente.Contiene(this))
            {
                throw new InvalidOperationException("No se puede asignar una familia a si misma ni generar ciclos.");
            }

            if (_hijos.Any(h => h.GetType() == componente.GetType() && h.Codigo == componente.Codigo))
            {
                throw new InvalidOperationException("El componente ya se encuentra asignado.");
            }

            if (componente is Patente_83KI patente && ContienePatente(patente.Codigo))
            {
                throw new InvalidOperationException("La patente ya se encuentra asignada.");
            }

            var patentesActuales = ObtenerPatentes().Select(p => p.Codigo).ToList();
            var patentesNuevas = componente.ObtenerPatentes().Select(p => p.Codigo).ToList();

            if (patentesActuales.Intersect(patentesNuevas).Any())
            {
                throw new InvalidOperationException("La asignacion duplicaria permisos indirectos.");
            }

            _hijos.Add(componente);
        }

        public virtual void Remover(ComponentePermiso_83KI componente)
        {
            if (componente == null)
            {
                return;
            }

            ComponentePermiso_83KI existente = _hijos.FirstOrDefault(h => h.GetType() == componente.GetType() && h.Codigo == componente.Codigo);

            if (existente != null)
            {
                _hijos.Remove(existente);
            }
        }

        public virtual bool Contiene(ComponentePermiso_83KI componente)
        {
            if (componente == null)
            {
                return false;
            }

            if (GetType() == componente.GetType() && Codigo == componente.Codigo)
            {
                return true;
            }

            return _hijos.Any(h => h.Contiene(componente));
        }

        public virtual IEnumerable<Patente_83KI> ObtenerPatentes()
        {
            return _hijos.SelectMany(h => h.ObtenerPatentes())
                .GroupBy(p => p.Codigo)
                .Select(g => g.First());
        }

        public void CargarHijoDesdePersistencia(ComponentePermiso_83KI componente)
        {
            if (componente != null && !_hijos.Any(h => h.GetType() == componente.GetType() && h.Codigo == componente.Codigo))
            {
                _hijos.Add(componente);
            }
        }

        protected bool ContienePatente(int codigoPatente)
        {
            return ObtenerPatentes().Any(p => p.Codigo == codigoPatente);
        }

        public virtual void Operacion()
        {
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
