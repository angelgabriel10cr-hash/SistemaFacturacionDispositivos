using SVDE.DAL1.Repositorios;
using SVDE.Entidades.Procesos;
using SVDE.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.BLL.Servicios
{
    public class FacturaBLL
    {
        private readonly IFacturaDAL _facturaDAL;
        private const decimal IVA = 0.13m;

        public FacturaBLL()
        {
            _facturaDAL = new FacturaDAL();
        }

        public decimal CalcularSubTotalLinea(int cantidad, decimal precioUnitario)
        {
            return cantidad * precioUnitario;
        }

        public decimal CalcularSubTotalGeneral(List<FacturaDetalle> lista)
        {
            return lista.Sum(x => x.SubTotalLinea);
        }

        public decimal CalcularImpuesto(decimal subtotal)
        {
            return subtotal * IVA;
        }

        public decimal CalcularTotal(decimal subtotal, decimal impuesto)
        {
            return subtotal + impuesto;
        }

        public decimal CalcularTotalDolares(decimal totalColones, decimal tipoCambio)
        {
            if (tipoCambio <= 0)
                return 0;

            return totalColones / tipoCambio;
        }

        public void ValidarFactura(Factura factura)
        {
            if (factura.IdCliente <= 0)
                throw new Exception("Debe seleccionar un cliente.");

            if (factura.IdUsuario <= 0)
                throw new Exception("Debe existir un usuario vendedor.");

            if (factura.ListaDetalle == null || factura.ListaDetalle.Count == 0)
                throw new Exception("Debe agregar productos a la factura.");

            if (factura.ListaPagos == null || factura.ListaPagos.Count == 0)
                throw new Exception("Debe registrar al menos un pago.");

            decimal totalPagos = factura.ListaPagos.Sum(x => x.Monto);
            if (totalPagos != factura.Total)
                throw new Exception("La suma de pagos debe ser igual al total.");
        }

        public int GuardarFacturaCompleta(Factura factura)
        {
            ValidarFactura(factura);
            return _facturaDAL.GuardarFacturaCompleta(factura);
        }

        public List<Factura> ListarFacturas()
        {
            return _facturaDAL.ListarFacturas();
        }
    }
}
