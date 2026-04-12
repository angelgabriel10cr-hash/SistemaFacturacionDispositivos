using SVDE.DAL1.Conexion;
using SVDE.Entidades.Maestros;
using SVDE.Interfaces.DAL;
using SVDE.Utilitarios1.Configuracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace SVDE.DAL1.Repositorios
{
    public class ProductoDAL : IProductoDAL
    {
        private readonly ConexionSql _conexion;
        private string cadenaConexion;

        public ProductoDAL()
        {
            _conexion = new ConexionSql();
        }

        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Producto_Listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Producto entidad = new Producto
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                CodigoInterno = dr["CodigoInterno"].ToString(),
                                CodigoBarras = dr["CodigoBarras"].ToString(),
                                TipoDispositivo = dr["TipoDispositivo"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Modelo = dr["Modelo"].ToString(),
                                Color = dr["Color"].ToString(),
                                NombreComercial = dr["NombreComercial"].ToString(),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                                Stock = Convert.ToInt32(dr["Stock"])
                            };

                            lista.Add(entidad);
                        }
                    }
                }
            }

            return lista;
        }

        public Producto ObtenerPorId(int idProducto)
        {
            Producto entidad = null;

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Producto_ObtenerPorId", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            entidad = new Producto
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                CodigoInterno = dr["CodigoInterno"].ToString(),
                                CodigoBarras = dr["CodigoBarras"].ToString(),
                                IdTipoDispositivo = Convert.ToInt32(dr["IdTipoDispositivo"]),
                                IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                IdModelo = Convert.ToInt32(dr["IdModelo"]),
                                IdColor = Convert.ToInt32(dr["IdColor"]),
                                NombreComercial = dr["NombreComercial"].ToString(),
                                Caracteristicas = dr["Caracteristicas"] == DBNull.Value ? string.Empty : dr["Caracteristicas"].ToString(),
                                DocumentoEspecificacion = dr["DocumentoEspecificacion"] == DBNull.Value ? null : (byte[])dr["DocumentoEspecificacion"],
                                NombreDocumentoEspecificacion = dr["NombreDocumentoEspecificacion"] == DBNull.Value ? string.Empty : dr["NombreDocumentoEspecificacion"].ToString(),
                                ExtensionDocumentoEspecificacion = dr["ExtensionDocumentoEspecificacion"] == DBNull.Value ? string.Empty : dr["ExtensionDocumentoEspecificacion"].ToString(),
                                Fotografia = dr["Fotografia"] == DBNull.Value ? null : (byte[])dr["Fotografia"],
                                PrecioCosto = Convert.ToDecimal(dr["PrecioCosto"]),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                StockMinimo = Convert.ToInt32(dr["StockMinimo"]),
                                GarantiaMeses = Convert.ToInt32(dr["GarantiaMeses"]),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                TipoDispositivo = dr["TipoDispositivo"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                Modelo = dr["Modelo"].ToString(),
                                Color = dr["Color"].ToString()
                            };
                        }
                    }
                }
            }

            return entidad;
        }

        public bool Insertar(Producto entidad)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Producto_Insertar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CodigoInterno", entidad.CodigoInterno);
                    cmd.Parameters.AddWithValue("@CodigoBarras", entidad.CodigoBarras);
                    cmd.Parameters.AddWithValue("@IdTipoDispositivo", entidad.IdTipoDispositivo);
                    cmd.Parameters.AddWithValue("@IdMarca", entidad.IdMarca);
                    cmd.Parameters.AddWithValue("@IdModelo", entidad.IdModelo);
                    cmd.Parameters.AddWithValue("@IdColor", entidad.IdColor);
                    cmd.Parameters.AddWithValue("@NombreComercial", entidad.NombreComercial);
                    cmd.Parameters.AddWithValue("@Caracteristicas",
                        string.IsNullOrWhiteSpace(entidad.Caracteristicas) ? (object)DBNull.Value : entidad.Caracteristicas);

                    cmd.Parameters.Add("@DocumentoEspecificacion", SqlDbType.VarBinary).Value =
                        entidad.DocumentoEspecificacion == null ? (object)DBNull.Value : entidad.DocumentoEspecificacion;

                    cmd.Parameters.AddWithValue("@NombreDocumentoEspecificacion",
                        string.IsNullOrWhiteSpace(entidad.NombreDocumentoEspecificacion) ? (object)DBNull.Value : entidad.NombreDocumentoEspecificacion);

                    cmd.Parameters.AddWithValue("@ExtensionDocumentoEspecificacion",
                        string.IsNullOrWhiteSpace(entidad.ExtensionDocumentoEspecificacion) ? (object)DBNull.Value : entidad.ExtensionDocumentoEspecificacion);

                    cmd.Parameters.Add("@Fotografia", SqlDbType.VarBinary).Value =
                        entidad.Fotografia == null ? (object)DBNull.Value : entidad.Fotografia;

                    cmd.Parameters.AddWithValue("@PrecioCosto", entidad.PrecioCosto);
                    cmd.Parameters.AddWithValue("@PrecioVenta", entidad.PrecioVenta);
                    cmd.Parameters.AddWithValue("@Stock", entidad.Stock);
                    cmd.Parameters.AddWithValue("@StockMinimo", entidad.StockMinimo);
                    cmd.Parameters.AddWithValue("@GarantiaMeses", entidad.GarantiaMeses);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool Actualizar(Producto entidad)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Producto_Actualizar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdProducto", entidad.IdProducto);
                    cmd.Parameters.AddWithValue("@CodigoInterno", entidad.CodigoInterno);
                    cmd.Parameters.AddWithValue("@CodigoBarras", entidad.CodigoBarras);
                    cmd.Parameters.AddWithValue("@IdTipoDispositivo", entidad.IdTipoDispositivo);
                    cmd.Parameters.AddWithValue("@IdMarca", entidad.IdMarca);
                    cmd.Parameters.AddWithValue("@IdModelo", entidad.IdModelo);
                    cmd.Parameters.AddWithValue("@IdColor", entidad.IdColor);
                    cmd.Parameters.AddWithValue("@NombreComercial", entidad.NombreComercial);
                    cmd.Parameters.AddWithValue("@Caracteristicas", string.IsNullOrWhiteSpace(entidad.Caracteristicas) ? (object)DBNull.Value : entidad.Caracteristicas);
                    cmd.Parameters.Add("@DocumentoEspecificacion", SqlDbType.VarBinary).Value = DBNull.Value;
                    cmd.Parameters.AddWithValue("@PrecioCosto", entidad.PrecioCosto);
                    cmd.Parameters.AddWithValue("@PrecioVenta", entidad.PrecioVenta);
                    cmd.Parameters.AddWithValue("@Stock", entidad.Stock);
                    cmd.Parameters.AddWithValue("@StockMinimo", entidad.StockMinimo);
                    cmd.Parameters.AddWithValue("@GarantiaMeses", entidad.GarantiaMeses);
                    cmd.Parameters.AddWithValue("@Estado", entidad.Estado);
                    cmd.Parameters.Add("@Fotografia", SqlDbType.VarBinary).Value =
                     entidad.Fotografia == null ? (object)DBNull.Value : entidad.Fotografia;
                    cmd.Parameters.Add("@DocumentoEspecificacion", SqlDbType.VarBinary).Value =
    entidad.DocumentoEspecificacion == null ? (object)DBNull.Value : entidad.DocumentoEspecificacion;

                    cmd.Parameters.AddWithValue("@NombreDocumentoEspecificacion",
                        string.IsNullOrWhiteSpace(entidad.NombreDocumentoEspecificacion) ? (object)DBNull.Value : entidad.NombreDocumentoEspecificacion);

                    cmd.Parameters.AddWithValue("@ExtensionDocumentoEspecificacion",
                        string.IsNullOrWhiteSpace(entidad.ExtensionDocumentoEspecificacion) ? (object)DBNull.Value : entidad.ExtensionDocumentoEspecificacion);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool Eliminar(int idProducto)
        {
            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Producto_Eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }
        public List<Producto> BuscarProductosFacturacion(string filtro)
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection cn = _conexion.ObtenerConexion())
            {
                string sql = @"
            SELECT IdProducto, CodigoInterno, CodigoBarras, NombreComercial, PrecioVenta, Stock
            FROM Producto
            WHERE (
                    CodigoInterno LIKE @Filtro
                    OR CodigoBarras LIKE @Filtro
                    OR NombreComercial LIKE @Filtro
                  )
            ORDER BY NombreComercial";

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Producto p = new Producto
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                CodigoInterno = dr["CodigoInterno"].ToString(),
                                CodigoBarras = dr["CodigoBarras"].ToString(),
                                NombreComercial = dr["NombreComercial"].ToString(),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                                Stock = Convert.ToInt32(dr["Stock"])
                            };

                            lista.Add(p);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
