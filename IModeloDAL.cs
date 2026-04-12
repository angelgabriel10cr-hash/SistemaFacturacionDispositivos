using SVDE.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.DAL
{
    public interface IModeloDAL
    {
        List<Modelo> Listar();
        Modelo ObtenerPorId(int idModelo);
        bool Insertar(Modelo entidad);
        bool Actualizar(Modelo entidad);
        bool Eliminar(int idModelo);
    }
}
