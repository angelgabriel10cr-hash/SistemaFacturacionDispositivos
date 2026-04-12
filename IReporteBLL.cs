using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.BLL
{
    public interface IReporteBLL
    {
        DataTable ListarFacturasPorFechas(string fechaInicio, string fechaFin);
        DataTable ListarProductos();
        DataTable ListarClientes();
    }
}
