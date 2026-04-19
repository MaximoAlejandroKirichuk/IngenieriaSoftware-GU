using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BitacoraEvento
    {
        public int Id { get; set; }
        public long DNI { get; set; } // Usamos DNI porque es tu PK
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public int Criticidad { get; set; } // 1 a 5 como en el Excel
        public string Modulo { get; set; }    // "Usuario"
        public string EmailUsuario { get; set; }
        // Constructor para crear eventos rápido desde la BLL

        // Constructor vacío para la DAL
        public BitacoraEvento() { }
        public BitacoraEvento(string descripcion, int criticidad, string modulo, string email)
        {
            Fecha = DateTime.Now;
            Descripcion = descripcion;
            Criticidad = criticidad;
            Modulo = modulo;
            EmailUsuario = email;

        }
    }
}
