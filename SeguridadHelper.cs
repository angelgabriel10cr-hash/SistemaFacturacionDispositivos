using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Entidades.Seguridad
{
    public static class SeguridadHelper
    {
        public static string GenerarHashSHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static bool VerificarHash(string textoPlano, string hashGuardado)
        {
            string hashTextoPlano = GenerarHashSHA256(textoPlano);
            return hashTextoPlano == hashGuardado;
        }
    }
}
