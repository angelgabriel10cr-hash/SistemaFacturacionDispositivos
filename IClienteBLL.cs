using SVDE.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.BLL
{
    public interface IClienteBLL
    {
        List<Cliente> Listar();
        Cliente ObtenerPorId(int idCliente);
        bool Guardar(Cliente entidad);
        bool Eliminar(int idCliente);
    }
}
