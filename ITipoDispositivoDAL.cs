using SVDE.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.DAL
{
    public interface ITipoDispositivoDAL
    {
        List<TipoDispositivo> Listar();
        TipoDispositivo ObtenerPorId(int idTipoDispositivo);
        bool Insertar(TipoDispositivo entidad);
        bool Actualizar(TipoDispositivo entidad);
        bool Eliminar(int idTipoDispositivo);
    }
}
