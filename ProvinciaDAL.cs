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
    public class ProvinciaDAL : IProvinciaDAL
    {
        private readonly ConexionSql _conexion;

        public ProvinciaDAL()
        {
            _conexion = new ConexionSql();
        }

        public List<Provincia> Listar()
        {
            List<Provincia> lista = new List<Provincia>();

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Provincia_Listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Provincia provincia = new Provincia
                            {
                                IdProvincia = Convert.ToInt32(dr["IdProvincia"]),
                                NombreProvincia = dr["NombreProvincia"].ToString()
                            };

                            lista.Add(provincia);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
