using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Entidades.Procesos
{
    public class FacturaPago
    {
        public int IdFacturaPago { get; set; }
        public int IdFactura { get; set; }

        public string TipoPago { get; set; }
        public decimal Monto { get; set; }

        public string NumeroTarjeta { get; set; }
        public string BancoTarjeta { get; set; }
        public string TipoTarjeta { get; set; }

        public string BancoTransferencia { get; set; }
        public string NumeroTransferencia { get; set; }

        public string NumeroSinpe { get; set; }
        public string ReferenciaSinpe { get; set; }
    }
}
