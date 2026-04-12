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
    public class ColorBLL : IColorBLL
    {
        private readonly IColorDAL _colorDAL;

        public ColorBLL()
        {
            _colorDAL = new ColorDAL();
        }

        public List<Color> Listar()
        {
            return _colorDAL.Listar();
        }
    }
}
