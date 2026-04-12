using SVDE.Entidades.Procesos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.BLL
{
    public interface IFacturaBLL
    {
        int GuardarFactura(Factura factura, List<FacturaDetalle> detalles, List<FacturaPago> pagos);
        List<Factura> ListarFacturas();
        Factura ObtenerPorId(int idFactura);
    }
}
