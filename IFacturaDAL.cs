using SVDE.Entidades.Procesos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.DAL
{
    public interface IFacturaDAL
    {
        int InsertarFactura(Factura factura);
        void InsertarFacturaDetalle(FacturaDetalle detalle);
        void InsertarFacturaPago(FacturaPago pago);
        void ConfirmarFactura(int idFactura);
        List<Factura> ListarFacturas();
        int GuardarFacturaCompleta(Factura factura);
    }
}
