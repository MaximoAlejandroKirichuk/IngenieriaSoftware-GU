using System;

namespace Service.Entidades
{
    public class Rol_83KI
    {
        private const int CodigoAdministrador = 1;

        public int CodigoRol { get; private set; }
        public string Nombre { get; private set; }

        //Campos Calculados
        //devuelve true si es que el administrador tiene un CodigoRol igual a CodigoAdmnistrador 
        public bool EsAdministrador => CodigoRol == CodigoAdministrador;
        //devuelve true si es que puede gestionar los formularios admin
        public bool PuedeGestionarAdmin => EsAdministrador;
        private Rol_83KI()
        {
        }

        public Rol_83KI(int codigoRol, string nombre)
        {
            if (codigoRol <= 0)
            {
                throw new ArgumentException("El codigo de rol debe ser valido.", nameof(codigoRol));
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre del rol es obligatorio.", nameof(nombre));
            }

            CodigoRol = codigoRol;
            Nombre = nombre.Trim();
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
