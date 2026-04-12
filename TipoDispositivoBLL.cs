using System.Collections.Generic;
using SVDE.Entidades.Maestros;
using SVDE.Interfaces.BLL;
using SVDE.Interfaces.DAL;
using SVDE.DAL.Repositorios;

namespace SVDE.BLL.Servicios
{
    public class TipoDispositivoBLL : ITipoDispositivoBLL
    {
        private readonly ITipoDispositivoDAL _tipoDAL;

        public TipoDispositivoBLL()
        {
            _tipoDAL = new TipoDispositivoDAL();
        }

        public List<TipoDispositivo> Listar()
        {
            return _tipoDAL.Listar();
        }

        public TipoDispositivo ObtenerPorId(int idTipoDispositivo)
        {
            return _tipoDAL.ObtenerPorId(idTipoDispositivo);
        }

        public bool Guardar(TipoDispositivo entidad)
        {
            if (entidad.IdTipoDispositivo == 0)
                return _tipoDAL.Insertar(entidad);
            else
                return _tipoDAL.Actualizar(entidad);
        }

        public bool Eliminar(int idTipoDispositivo)
        {
            return _tipoDAL.Eliminar(idTipoDispositivo);
        }
    }
}