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
    public class ModeloDAL : IModeloDAL
    {
        private readonly ConexionSql _conexion;

        public ModeloDAL()
        {
            _conexion = new ConexionSql();
        }

        public List<Modelo> Listar()
        {
            List<Modelo> lista = new List<Modelo>();

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Modelo_Listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Modelo entidad = new Modelo
                            {
                                IdModelo = Convert.ToInt32(dr["IdModelo"]),
                                CodigoModelo = dr["CodigoModelo"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                NombreMarca = dr["NombreMarca"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };

                            lista.Add(entidad);
                        }
                    }
                }
            }

            return lista;
        }

        public Modelo ObtenerPorId(int idModelo)
        {
            Modelo entidad = null;

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Modelo_ObtenerPorId", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdModelo", idModelo);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            entidad = new Modelo
                            {
                                IdModelo = Convert.ToInt32(dr["IdModelo"]),
                                CodigoModelo = dr["CodigoModelo"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };
                        }
                    }
                }
            }

            return entidad;
        }

        public bool Insertar(Modelo entidad)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Modelo_Insertar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoModelo", entidad.CodigoModelo);
                    cmd.Parameters.AddWithValue("@Descripcion", entidad.Descripcion);
                    cmd.Parameters.AddWithValue("@IdMarca", entidad.IdMarca);
                    cmd.Parameters.AddWithValue("@Estado", entidad.Estado);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool Actualizar(Modelo entidad)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Modelo_Actualizar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdModelo", entidad.IdModelo);
                    cmd.Parameters.AddWithValue("@CodigoModelo", entidad.CodigoModelo);
                    cmd.Parameters.AddWithValue("@Descripcion", entidad.Descripcion);
                    cmd.Parameters.AddWithValue("@IdMarca", entidad.IdMarca);
                    cmd.Parameters.AddWithValue("@Estado", entidad.Estado);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool Eliminar(int idModelo)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Modelo_Eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdModelo", idModelo);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }
    }
}
