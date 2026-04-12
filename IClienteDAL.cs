using SVDE.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.DAL
{
    public interface IClienteDAL
    {
        List<Cliente> Listar();
        Cliente ObtenerPorId(int idCliente);
        Cliente ObtenerPorIdentificacion(string identificacion);
        bool Insertar(Cliente entidad);
        bool Actualizar(Cliente entidad);
        bool Eliminar(int idCliente);
    }
}
