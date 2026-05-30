namespace Service.Entidades
{
    public class Familia_83KI : ComponentePermiso_83KI
    {
        public int CodigoFamilia => Codigo;

        public Familia_83KI(int codigoFamilia, string nombre)
            : base(codigoFamilia, nombre)
        {
        }
    }
}
