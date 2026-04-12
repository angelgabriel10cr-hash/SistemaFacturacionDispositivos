using System.Collections.Generic;
using SVDE.Entidades.Maestros;
using SVDE.Interfaces.BLL;
using SVDE.Interfaces.DAL;
using SVDE.DAL.Repositorios;

namespace SVDE.BLL.Servicios
{
    public class MarcaBLL : IMarcaBLL
    {
        private readonly IMarcaDAL _marcaDAL;

        public MarcaBLL()
        {
            _marcaDAL = new MarcaDAL();
        }

        public List<Marca> Listar()
        {
            return _marcaDAL.Listar();
        }

        public Marca ObtenerPorId(int idMarca)
        {
            return _marcaDAL.ObtenerPorId(idMarca);
        }

        public bool Guardar(Marca entidad)
        {
            if (entidad.IdMarca == 0)
                return _marcaDAL.Insertar(entidad);
            else
                return _marcaDAL.Actualizar(entidad);
        }

        public bool Eliminar(int idMarca)
        {
            return _marcaDAL.Eliminar(idMarca);
        }
    }
}