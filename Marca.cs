using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Entidades.Maestros
{
    public class Marca
    {
        public int IdMarca { get; set; }
        public string CodigoMarca { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
