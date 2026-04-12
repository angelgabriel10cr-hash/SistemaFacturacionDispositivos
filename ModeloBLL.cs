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
    public class ModeloBLL : IModeloBLL
    {
        private readonly IModeloDAL _modeloDAL;

        public ModeloBLL()
        {
            _modeloDAL = new ModeloDAL();
        }

        public List<Modelo> Listar()
        {
            return _modeloDAL.Listar();
        }

        public Modelo ObtenerPorId(int idModelo)
        {
            return _modeloDAL.ObtenerPorId(idModelo);
        }

        public bool Guardar(Modelo entidad)
        {
            if (entidad.IdModelo == 0)
                return _modeloDAL.Insertar(entidad);
            else
                return _modeloDAL.Actualizar(entidad);
        }

        public bool Eliminar(int idModelo)
        {
            return _modeloDAL.Eliminar(idModelo);
        }
        public List<Modelo> ListarPorMarca(int idMarca)
        {
            List<Modelo> lista = _modeloDAL.Listar();
            return lista.FindAll(x => x.IdMarca == idMarca && x.Estado);
        }
    }
}
