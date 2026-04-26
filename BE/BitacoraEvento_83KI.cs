using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BitacoraEvento_83KI
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public int Criticidad { get; set; } // 1 a 5 como en el Excel
        public Modulo Modulo { get; set; }    // "Usuario"
        public string Username { get; set; }
        
        // Constructor vacío para la DAL
        public BitacoraEvento_83KI() { }
        // Constructor para crear eventos rápido desde la BLL
        public BitacoraEvento_83KI(string descripcion, int criticidad, Modulo modulo, string username)
        {
            Fecha = DateTime.UtcNow;
            Descripcion = descripcion;
            Criticidad = criticidad;
            Modulo = modulo;
            Username = username;
        }


    }
}
