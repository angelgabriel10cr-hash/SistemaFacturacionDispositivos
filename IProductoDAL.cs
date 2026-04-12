using SVDE.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.DAL
{
    public interface IProductoDAL
    {
        List<Producto> Listar();
        Producto ObtenerPorId(int idProducto);
        bool Insertar(Producto entidad);
        bool Actualizar(Producto entidad);
        bool Eliminar(int idProducto);
    }
}
