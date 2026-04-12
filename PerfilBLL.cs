using SVDE.DAL1.Repositorios;
using SVDE.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.BLL.Servicios
{
    public class PerfilBLL
    {
        private readonly PerfilDAL _perfilDAL;

        public PerfilBLL()
        {
            _perfilDAL = new PerfilDAL();
        }

        public List<Perfil> Listar()
        {
            return _perfilDAL.Listar();
        }
    }
}
