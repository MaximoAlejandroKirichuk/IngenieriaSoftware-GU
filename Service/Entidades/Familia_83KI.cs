using System.Collections.Generic;

namespace Service.Entidades
{
    public class Familia_83KI : ComponentePermiso_83KI
    {
        public int CodigoFamilia => Codigo;

        public Familia_83KI(int codigoFamilia, string nombre)
            : base(codigoFamilia, nombre)
        {
        }

        public override IEnumerable<Patente_83KI> ObtenerPatentes()
        {
            List<Patente_83KI> patentes = new List<Patente_83KI>();
            HashSet<int> codigosAgregados = new HashSet<int>();

            foreach (ComponentePermiso_83KI componente in Hijos)
            {
                foreach (Patente_83KI patente in componente.ObtenerPatentes())
                {
                    if (codigosAgregados.Add(patente.CodigoPatente))
                    {
                        patentes.Add(patente);
                    }
                }
            }

            return patentes;


        }
    }
}
