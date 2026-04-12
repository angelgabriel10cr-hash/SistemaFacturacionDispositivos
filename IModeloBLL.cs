using SVDE.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.BLL
{
    public interface IModeloBLL
    {
        List<Modelo> Listar();
        Modelo ObtenerPorId(int idModelo);
        bool Guardar(Modelo entidad);
        bool Eliminar(int idModelo);
        List<Modelo> ListarPorMarca(int idMarca);
    }
}
