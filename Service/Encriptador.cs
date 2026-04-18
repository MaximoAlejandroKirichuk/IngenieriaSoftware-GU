using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Encriptador : IEncriptador
    {
        public string HashContrasena(string contrasena)
        {
            // instancia del algoritmo SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convierto el string en un arreglo de bytes (UTF8)
                byte[] bytes = Encoding.UTF8.GetBytes(contrasena);

                // Calculo el hash (devuelve un arreglo de 32 bytes)
                byte[] hash = sha256.ComputeHash(bytes);

                // Convierto los bytes a una cadena hexadecimal legible
                StringBuilder result = new StringBuilder();
                foreach (byte b in hash)
                {
                    result.Append(b.ToString("x2"));
                }

                return result.ToString();
            }
        }
    }
}

