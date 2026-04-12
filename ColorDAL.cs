using SVDE.DAL1.Conexion;
using SVDE.Entidades.Maestros;
using SVDE.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.DAL1.Repositorios
{
    public class ColorDAL : IColorDAL
    {
        private readonly ConexionSql _conexion;

        public ColorDAL()
        {
            _conexion = new ConexionSql();
        }

        public List<Color> Listar()
        {
            List<Color> lista = new List<Color>();

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Color_Listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Color entidad = new Color
                            {
                                IdColor = Convert.ToInt32(dr["IdColor"]),
                                Descripcion = dr["Descripcion"].ToString()
                            };

                            lista.Add(entidad);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
