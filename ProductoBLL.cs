using SVDE.DAL1.Repositorios;
using SVDE.Entidades.Maestros;
using SVDE.Interfaces.BLL;
using SVDE.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.BLL.Servicios
{
    public class ProductoBLL : IProductoBLL
    {
        private readonly IProductoDAL _productoDAL;

        public ProductoBLL()
        {
            _productoDAL = new ProductoDAL();
        }

        public List<Producto> Listar()
        {
            return _productoDAL.Listar();
        }

        public Producto ObtenerPorId(int idProducto)
        {
            return _productoDAL.ObtenerPorId(idProducto);
        }

        public bool Guardar(Producto entidad)
        {
            if (entidad.IdProducto == 0)
                return _productoDAL.Insertar(entidad);
            else
                return _productoDAL.Actualizar(entidad);
        }

        public bool Eliminar(int idProducto)
        {
            return _productoDAL.Eliminar(idProducto);
        }
        private ProductoDAL productoDAL = new ProductoDAL();

        public List<Producto> BuscarProductosFacturacion(string filtro)
        {
            return productoDAL.BuscarProductosFacturacion(filtro);
        }
    }
}
