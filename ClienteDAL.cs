
using SVDE.DAL1.Conexion;
using SVDE.Entidades.Maestros;
using SVDE.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SVDE.DAL.Repositorios
{
    public class ClienteDAL : IClienteDAL
    {
        private readonly ConexionSql _conexion;

        public ClienteDAL()
        {
            _conexion = new ConexionSql();
        }

        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Cliente_Listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Cliente cliente = new Cliente
                            {
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                TipoIdentificacion = dr["TipoIdentificacion"].ToString(),
                                Identificacion = dr["Identificacion"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido1 = dr["Apellido1"].ToString(),
                                Apellido2 = dr["Apellido2"].ToString(),
                                Sexo = dr["Sexo"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                IdProvincia = Convert.ToInt32(dr["IdProvincia"]),
                                DireccionExacta = dr["DireccionExacta"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                            };

                            lista.Add(cliente);
                        }
                    }
                }
            }

            return lista;
        }

        public Cliente ObtenerPorId(int idCliente)
        {
            Cliente cliente = null;

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Cliente_ObtenerPorId", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cliente = new Cliente
                            {
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                TipoIdentificacion = dr["TipoIdentificacion"].ToString(),
                                Identificacion = dr["Identificacion"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido1 = dr["Apellido1"].ToString(),
                                Apellido2 = dr["Apellido2"].ToString(),
                                Sexo = dr["Sexo"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                IdProvincia = Convert.ToInt32(dr["IdProvincia"]),
                                DireccionExacta = dr["DireccionExacta"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                            };
                        }
                    }
                }
            }

            return cliente;
        }

        public Cliente ObtenerPorIdentificacion(string identificacion)
        {
            Cliente cliente = null;

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Cliente_ObtenerPorIdentificacion", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Identificacion", identificacion);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cliente = new Cliente
                            {
                                IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                TipoIdentificacion = dr["TipoIdentificacion"].ToString(),
                                Identificacion = dr["Identificacion"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido1 = dr["Apellido1"].ToString(),
                                Apellido2 = dr["Apellido2"].ToString(),
                                Sexo = dr["Sexo"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                IdProvincia = Convert.ToInt32(dr["IdProvincia"]),
                                DireccionExacta = dr["DireccionExacta"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                            };
                        }
                    }
                }
            }

            return cliente;
        }

        public bool Insertar(Cliente entidad)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Cliente_Insertar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TipoIdentificacion", entidad.TipoIdentificacion);
                    cmd.Parameters.AddWithValue("@Identificacion", entidad.Identificacion);
                    cmd.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido1", entidad.Apellido1);
                    cmd.Parameters.AddWithValue("@Apellido2", string.IsNullOrWhiteSpace(entidad.Apellido2) ? (object)DBNull.Value : entidad.Apellido2);
                    cmd.Parameters.AddWithValue("@Sexo", entidad.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", entidad.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", entidad.Correo);
                    cmd.Parameters.AddWithValue("@DireccionExacta", entidad.DireccionExacta);
                    cmd.Parameters.AddWithValue("@IdProvincia", entidad.IdProvincia);
                    cmd.Parameters.Add("@Fotografia", SqlDbType.VarBinary).Value = DBNull.Value;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool Actualizar(Cliente entidad)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Cliente_Actualizar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCliente", entidad.IdCliente);
                    cmd.Parameters.AddWithValue("@TipoIdentificacion", entidad.TipoIdentificacion);
                    cmd.Parameters.AddWithValue("@Identificacion", entidad.Identificacion);
                    cmd.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido1", entidad.Apellido1);
                    cmd.Parameters.AddWithValue("@Apellido2", string.IsNullOrWhiteSpace(entidad.Apellido2) ? (object)DBNull.Value : entidad.Apellido2);
                    cmd.Parameters.AddWithValue("@Sexo", entidad.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", entidad.Telefono);
                    cmd.Parameters.AddWithValue("@Correo", entidad.Correo);
                    cmd.Parameters.AddWithValue("@DireccionExacta", entidad.DireccionExacta);
                    cmd.Parameters.AddWithValue("@IdProvincia", entidad.IdProvincia);
                    cmd.Parameters.Add("@Fotografia", SqlDbType.VarBinary).Value = DBNull.Value;
                    cmd.Parameters.AddWithValue("@Estado", entidad.Estado);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool Eliminar(int idCliente)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Cliente_Eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }
    }
}
