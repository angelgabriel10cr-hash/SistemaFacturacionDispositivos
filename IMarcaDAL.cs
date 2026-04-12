using SVDE.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.DAL
{
    public interface IMarcaDAL
    {
        List<Marca> Listar();
        Marca ObtenerPorId(int idMarca);
        bool Insertar(Marca entidad);
        bool Actualizar(Marca entidad);
        bool Eliminar(int idMarca);
    }
}
