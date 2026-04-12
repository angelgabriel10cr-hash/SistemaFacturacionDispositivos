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
    public class UsuarioDAL
    {
        private readonly ConexionSql _conexion;

        public UsuarioDAL()
        {
            _conexion = new ConexionSql();
        }

        public Usuario ObtenerPorNombreUsuario(string nombreUsuario)
        {
            Usuario usuario = null;

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_Login", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                IdPerfil = Convert.ToInt32(dr["IdPerfil"]),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                ClaveHash = dr["ClaveHash"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                                NombrePerfil = dr["NombrePerfil"].ToString()
                            };
                        }
                    }
                }
            }

            return usuario;
        }

        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_Listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                IdPerfil = Convert.ToInt32(dr["IdPerfil"]),
                                NombrePerfil = dr["NombrePerfil"].ToString(),
                                NombreUsuario = dr["NombreUsuario"].ToString(),
                                ClaveHash = dr["ClaveHash"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"])
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public bool Insertar(Usuario usuario)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_Insertar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdPerfil", usuario.IdPerfil);
                    cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                    cmd.Parameters.AddWithValue("@ClaveHash", usuario.ClaveHash);
                    cmd.Parameters.AddWithValue("@NombreCompleto", usuario.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Correo", usuario.Correo);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool Actualizar(Usuario usuario)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_Actualizar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@IdPerfil", usuario.IdPerfil);
                    cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                    cmd.Parameters.AddWithValue("@ClaveHash", usuario.ClaveHash);
                    cmd.Parameters.AddWithValue("@NombreCompleto", usuario.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@Estado", usuario.Estado);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool CambiarEstado(int idUsuario, bool estado)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_CambiarEstado", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }
        public bool ExisteNombreUsuario(string nombreUsuario)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_Existe", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);

                    cn.Open();

                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());

                    return cantidad > 0;
                }
            }
        }
    }
}
