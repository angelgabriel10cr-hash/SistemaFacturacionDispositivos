using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Entidades.Seguridad
{
    public static class SesionUsuario
    {
        public static int IdUsuario { get; set; }
        public static int IdPerfil { get; set; }
        public static string NombreUsuario { get; set; }
        public static string NombreCompleto { get; set; }
        public static string NombrePerfil { get; set; }
        public static string Correo { get; set; }

        public static void Limpiar()
        {
            IdUsuario = 0;
            IdPerfil = 0;
            NombreUsuario = string.Empty;
            NombreCompleto = string.Empty;
            NombrePerfil = string.Empty;
            Correo = string.Empty;
        }
    }
}
