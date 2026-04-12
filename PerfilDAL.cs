using SVDE.DAL1.Conexion;
using SVDE.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.DAL1.Repositorios
{
    public class PerfilDAL
    {
        private readonly ConexionSql _conexion;

        public PerfilDAL()
        {
            _conexion = new ConexionSql();
        }

        public List<Perfil> Listar()
        {
            List<Perfil> lista = new List<Perfil>();

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Perfil_Listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Perfil
                            {
                                IdPerfil = (int)dr["IdPerfil"],
                                NombrePerfil = dr["NombrePerfil"].ToString(),
                                Estado = (bool)dr["Estado"]
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}
