using SVDE.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.BLL
{
    public interface IProductoBLL
    {
        List<Producto> Listar();
        Producto ObtenerPorId(int idProducto);
        bool Guardar(Producto entidad);
        bool Eliminar(int idProducto);
    }
}
