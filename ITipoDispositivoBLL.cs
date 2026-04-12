using SVDE.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.BLL
{
    public interface ITipoDispositivoBLL
    {
        List<TipoDispositivo> Listar();
        TipoDispositivo ObtenerPorId(int idTipoDispositivo);
        bool Guardar(TipoDispositivo entidad);
        bool Eliminar(int idTipoDispositivo);
    }
}
