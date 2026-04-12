using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Entidades.Procesos
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public string NumeroFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaFactura { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }

        public decimal TipoCambio { get; set; }
        public decimal TotalDolares { get; set; }

        public string Estado { get; set; }
        public string Observaciones { get; set; }

        public byte[] FirmaCliente { get; set; }
        public string XmlFactura { get; set; }

        public List<FacturaDetalle> ListaDetalle { get; set; }
        public List<FacturaPago> ListaPagos { get; set; }

        public Factura()
        {
            ListaDetalle = new List<FacturaDetalle>();
            ListaPagos = new List<FacturaPago>();
        }
    }
}
