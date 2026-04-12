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
    public class ProvinciaBLL : IProvinciaBLL
    {
        private readonly IProvinciaDAL _provinciaDAL;

        public ProvinciaBLL()
        {
            _provinciaDAL = new ProvinciaDAL();
        }

        public List<Provincia> Listar()
        {
            return _provinciaDAL.Listar();
        }
    }
}
