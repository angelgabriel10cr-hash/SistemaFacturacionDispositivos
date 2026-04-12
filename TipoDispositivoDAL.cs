
using SVDE.DAL1.Conexion;
using SVDE.Entidades.Maestros;
using SVDE.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SVDE.DAL.Repositorios
{
    public class TipoDispositivoDAL : ITipoDispositivoDAL
    {
        private readonly ConexionSql _conexion;

        public TipoDispositivoDAL()
        {
            _conexion = new ConexionSql();
        }

        public List<TipoDispositivo> Listar()
        {
            List<TipoDispositivo> lista = new List<TipoDispositivo>();

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_TipoDispositivo_Listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TipoDispositivo entidad = new TipoDispositivo
                            {
                                IdTipoDispositivo = Convert.ToInt32(dr["IdTipoDispositivo"]),
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

        public TipoDispositivo ObtenerPorId(int idTipoDispositivo)
        {
            TipoDispositivo entidad = null;

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_TipoDispositivo_ObtenerPorId", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdTipoDispositivo", idTipoDispositivo);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            entidad = new TipoDispositivo
                            {
                                IdTipoDispositivo = Convert.ToInt32(dr["IdTipoDispositivo"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };
                        }
                    }
                }
            }

            return entidad;
        }

        public bool Insertar(TipoDispositivo entidad)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_TipoDispositivo_Insertar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Descripcion", entidad.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", entidad.Estado);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool Actualizar(TipoDispositivo entidad)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_TipoDispositivo_Actualizar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdTipoDispositivo", entidad.IdTipoDispositivo);
                    cmd.Parameters.AddWithValue("@Descripcion", entidad.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", entidad.Estado);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool Eliminar(int idTipoDispositivo)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_TipoDispositivo_Eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdTipoDispositivo", idTipoDispositivo);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }
    }
}