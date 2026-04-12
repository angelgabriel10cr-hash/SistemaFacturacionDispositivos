using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Entidades.Procesos
{
    public class FacturaDetalle
    {
        public int IdFacturaDetalle { get; set; }
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }

        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotalLinea { get; set; }
    }
}
