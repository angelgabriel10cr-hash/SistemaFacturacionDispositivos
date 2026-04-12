using SVDE.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.BLL
{
    public interface IMarcaBLL
    {
        List<Marca> Listar();
        Marca ObtenerPorId(int idMarca);
        bool Guardar(Marca entidad);
        bool Eliminar(int idMarca);
    }
}
