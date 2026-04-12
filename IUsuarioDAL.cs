using SVDE.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Interfaces.DAL
{
    public interface IUsuarioDAL
    {
        Usuario ValidarAcceso(string nombreUsuario, string clave);
    }
}
