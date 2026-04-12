using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Entidades.Maestros
{
    public class Modelo
    {
        public int IdModelo { get; set; }
        public string CodigoModelo { get; set; }
        public string Descripcion { get; set; }
        public int IdMarca { get; set; }
        public bool Estado { get; set; }

        public string NombreMarca { get; set; }
    }
}

