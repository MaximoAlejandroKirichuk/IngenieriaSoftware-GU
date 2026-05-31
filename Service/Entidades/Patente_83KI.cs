using System;
using System.Collections.Generic;

namespace Service.Entidades
{
    public class Patente_83KI : ComponentePermiso_83KI
    {
        public int CodigoPatente => Codigo;

        public Patente_83KI(int codigoPatente, string nombre)
            : base(codigoPatente, nombre)
        {
        }

        public override void Agregar(ComponentePermiso_83KI componente)
        {
            throw new InvalidOperationException("Una patente no puede contener otros permisos.");
        }

        public override void Remover(ComponentePermiso_83KI componente)
        {
            throw new InvalidOperationException("Una patente no puede contener otros permisos.");
        }

        public override IEnumerable<Patente_83KI> ObtenerPatentes()
        {
            //Retorna un IEnumerable que contiene un único elemento: la instancia misma de la clase donde está este método.
            yield return this;
        }
    }
}
