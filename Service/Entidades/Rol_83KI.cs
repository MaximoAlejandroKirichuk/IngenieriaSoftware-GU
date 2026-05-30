using System;

namespace Service.Entidades
{
    public class Rol_83KI : ComponentePermiso_83KI
    {
        private const int CodigoAdministrador = 1;

        public int CodigoRol => Codigo;

        //Campos Calculados
        //devuelve true si es que el administrador tiene un CodigoRol igual a CodigoAdmnistrador 
        public bool EsAdministrador => CodigoRol == CodigoAdministrador;
        //devuelve true si es que puede gestionar los formularios admin
        public bool PuedeGestionarAdmin => EsAdministrador;
        public Rol_83KI(int codigoRol, string nombre)
            : base(codigoRol, nombre)
        {
        }
    }
}
