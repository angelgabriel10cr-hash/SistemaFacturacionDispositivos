
using SVDE.DAL1.Conexion;
using SVDE.Entidades.Maestros;
using SVDE.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SVDE.DAL.Repositorios
{
    public class MarcaDAL : IMarcaDAL
    {
        private readonly ConexionSql _conexion;

        public MarcaDAL()
        {
            _conexion = new ConexionSql();
        }

        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Marca_Listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Marca entidad = new Marca
                            {
                                IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                CodigoMarca = dr["CodigoMarca"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };

                            lista.Add(entidad);
                        }
                    }
                }
            }

            return lista;
        }

        public Marca ObtenerPorId(int idMarca)
        {
            Marca entidad = null;

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Marca_ObtenerPorId", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdMarca", idMarca);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            entidad = new Marca
                            {
                                IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                CodigoMarca = dr["CodigoMarca"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };
                        }
                    }
                }
            }

            return entidad;
        }

        public bool Insertar(Marca entidad)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Marca_Insertar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoMarca", entidad.CodigoMarca);
                    cmd.Parameters.AddWithValue("@Descripcion", entidad.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", entidad.Estado);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool Actualizar(Marca entidad)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Marca_Actualizar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdMarca", entidad.IdMarca);
                    cmd.Parameters.AddWithValue("@CodigoMarca", entidad.CodigoMarca);
                    cmd.Parameters.AddWithValue("@Descripcion", entidad.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", entidad.Estado);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool Eliminar(int idMarca)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Marca_Eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdMarca", idMarca);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }
    }
}