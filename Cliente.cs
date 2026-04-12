using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Entidades.Maestros
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public int IdProvincia { get; set; }
        public string DireccionExacta { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string NombreCompleto { get; set; }

        /* public string NombreCompleto
         {
             get { return $"{Nombre} {Apellido1} {Apellido2}".Trim(); }
         }
        */
    }
}
