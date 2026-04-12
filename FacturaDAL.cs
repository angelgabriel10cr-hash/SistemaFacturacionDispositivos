using SVDE.Entidades.Procesos;
using SVDE.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.DAL1.Repositorios
{
    public class FacturaDAL : IFacturaDAL
    {
        private string cadenaConexion;

        public FacturaDAL()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        }

        public int InsertarFactura(Factura factura)
        {
            int idFactura = 0;

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Factura_Insertar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NumeroFactura", factura.NumeroFactura);
                    cmd.Parameters.AddWithValue("@IdCliente", factura.IdCliente);
                    cmd.Parameters.AddWithValue("@IdUsuario", factura.IdUsuario);
                    cmd.Parameters.AddWithValue("@IdImpuesto", 1);
                    cmd.Parameters.AddWithValue("@FechaFactura", factura.FechaFactura);
                    cmd.Parameters.AddWithValue("@SubTotal", factura.SubTotal);
                    cmd.Parameters.AddWithValue("@MontoImpuesto", factura.Impuesto);
                    cmd.Parameters.AddWithValue("@TotalColones", factura.Total);
                    cmd.Parameters.AddWithValue("@TipoCambio", factura.TipoCambio);
                    cmd.Parameters.AddWithValue("@TotalDolares", factura.TotalDolares);
                    cmd.Parameters.AddWithValue("@Estado", factura.Estado);
                    cmd.Parameters.AddWithValue("@Observaciones", (object)factura.Observaciones ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@FirmaCliente", (object)factura.FirmaCliente ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@XmlFactura", string.IsNullOrWhiteSpace(factura.XmlFactura) ? (object)DBNull.Value : factura.XmlFactura);

                   SqlParameter output = new SqlParameter("@IdFactura", SqlDbType.Int);
                   output.Direction = ParameterDirection.Output;
                   cmd.Parameters.Add(output);

                    cn.Open();
                    cmd.ExecuteNonQuery();

                    idFactura = Convert.ToInt32(output.Value);
                }
            }

            return idFactura;
        }

        public void InsertarFacturaDetalle(FacturaDetalle detalle)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_FacturaDetalle_Insertar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdFactura", detalle.IdFactura);
                    cmd.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                    cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@SubTotalLinea", detalle.SubTotalLinea);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertarFacturaPago(FacturaPago pago)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_FacturaPago_Insertar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdFactura", pago.IdFactura);
                    cmd.Parameters.AddWithValue("@TipoPago", pago.TipoPago);
                    cmd.Parameters.AddWithValue("@Monto", pago.Monto);
                    cmd.Parameters.AddWithValue("@NumeroTarjeta", (object)pago.NumeroTarjeta ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BancoTarjeta", (object)pago.BancoTarjeta ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TipoTarjeta", (object)pago.TipoTarjeta ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BancoTransferencia", (object)pago.BancoTransferencia ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NumeroTransferencia", (object)pago.NumeroTransferencia ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NumeroSinpe", (object)pago.NumeroSinpe ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ReferenciaSinpe", (object)pago.ReferenciaSinpe ?? DBNull.Value);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ConfirmarFactura(int idFactura)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Factura_Confirmar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Factura> ListarFacturas()
        {
            List<Factura> lista = new List<Factura>();

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Factura_Listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Factura f = new Factura();
                            f.IdFactura = Convert.ToInt32(dr["IdFactura"]);
                            f.NumeroFactura = dr["NumeroFactura"].ToString();
                            f.FechaFactura = Convert.ToDateTime(dr["FechaFactura"]);
                            f.SubTotal = Convert.ToDecimal(dr["SubTotal"]);
                            f.Impuesto = Convert.ToDecimal(dr["Impuesto"]);
                            f.Total = Convert.ToDecimal(dr["Total"]);
                            f.Estado = dr["Estado"].ToString();

                            lista.Add(f);
                        }
                    }
                }
            }

            return lista;
        }

        public int GuardarFacturaCompleta(Factura factura)
        {
            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                cn.Open();

                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    int idFactura;

                    // =========================
                    // INSERTAR FACTURA
                    // =========================
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_Factura_Insertar", cn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NumeroFactura", factura.NumeroFactura);
                        cmd.Parameters.AddWithValue("@IdCliente", factura.IdCliente);
                        cmd.Parameters.AddWithValue("@IdUsuario", factura.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdImpuesto", 1);
                        cmd.Parameters.AddWithValue("@FechaFactura", factura.FechaFactura);
                        cmd.Parameters.AddWithValue("@SubTotal", factura.SubTotal);
                        cmd.Parameters.AddWithValue("@MontoImpuesto", factura.Impuesto);
                        cmd.Parameters.AddWithValue("@TotalColones", factura.Total);
                        cmd.Parameters.AddWithValue("@TipoCambio", factura.TipoCambio);
                        cmd.Parameters.AddWithValue("@TotalDolares", factura.TotalDolares);
                        cmd.Parameters.AddWithValue("@Estado", factura.Estado);
                        cmd.Parameters.AddWithValue("@Observaciones", (object)factura.Observaciones ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@FirmaCliente", (object)factura.FirmaCliente ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@XmlFactura", (object)factura.XmlFactura ?? DBNull.Value);

                        SqlParameter output = new SqlParameter("@IdFactura", SqlDbType.Int);
                        output.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(output);

                        cmd.ExecuteNonQuery();

                        idFactura = Convert.ToInt32(output.Value);
                    }

                    // =========================
                    // INSERTAR DETALLE
                    // =========================
                    foreach (FacturaDetalle detalle in factura.ListaDetalle)
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.sp_FacturaDetalle_Insertar", cn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            decimal porcentajeImpuesto = 13m;
                            decimal montoImpuesto = detalle.SubTotalLinea * (porcentajeImpuesto / 100m);
                            decimal totalLinea = detalle.SubTotalLinea + montoImpuesto;

                            cmd.Parameters.AddWithValue("@IdFactura", idFactura);
                            cmd.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                            cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                            cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                            cmd.Parameters.AddWithValue("@PorcentajeImpuesto", porcentajeImpuesto);
                            cmd.Parameters.AddWithValue("@MontoImpuesto", montoImpuesto);
                            cmd.Parameters.AddWithValue("@SubtotalLinea", detalle.SubTotalLinea);
                            cmd.Parameters.AddWithValue("@TotalLinea", totalLinea);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    // =========================
                    // INSERTAR PAGOS
                    // =========================
                    foreach (FacturaPago pago in factura.ListaPagos)
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.sp_FacturaPago_Insertar", cn, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            int idMetodoPago = 1;
                            int? idBanco = null;
                            int? idTipoTarjeta = null;
                            string numeroTarjetaMascara = null;
                            string numeroTransferencia = null;
                            string numeroSinpe = null;

                            string tipoPago = (pago.TipoPago ?? "").Trim().ToUpper();

                            if (tipoPago == "TARJETA")
                            {
                                idMetodoPago = 1;
                                idBanco = 1;
                                idTipoTarjeta = 1;

                                if (!string.IsNullOrWhiteSpace(pago.NumeroTarjeta))
                                {
                                    string numero = pago.NumeroTarjeta.Trim();
                                    if (numero.Length >= 4)
                                        numeroTarjetaMascara = "****" + numero.Substring(numero.Length - 4);
                                    else
                                        numeroTarjetaMascara = numero;
                                }
                            }
                            else if (tipoPago == "TRANSFERENCIA")
                            {
                                idMetodoPago = 2;
                                idBanco = 1;
                                numeroTransferencia = pago.NumeroTransferencia;
                            }
                            else if (tipoPago == "SINPE")
                            {
                                idMetodoPago = 3;
                                numeroSinpe = pago.NumeroSinpe;
                            }

                            cmd.Parameters.AddWithValue("@IdFactura", idFactura);
                            cmd.Parameters.AddWithValue("@IdMetodoPago", idMetodoPago);
                            cmd.Parameters.AddWithValue("@IdBanco", (object)idBanco ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@IdTipoTarjeta", (object)idTipoTarjeta ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@NumeroTarjetaMascara", (object)numeroTarjetaMascara ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@NumeroTransferencia", (object)numeroTransferencia ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@NumeroSinpe", (object)numeroSinpe ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@MontoPagado", pago.Monto);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    // =========================
                    // CONFIRMAR FACTURA
                    // =========================
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_Factura_Confirmar", cn, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdFactura", idFactura);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return idFactura;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }


        }
        public void GuardarXMLFactura(int idFactura, string rutaXML)
        {
            string xmlTexto = File.ReadAllText(rutaXML);

            if (xmlTexto.StartsWith("<?xml"))
            {
                int finDeclaracion = xmlTexto.IndexOf("?>");
                if (finDeclaracion >= 0)
                {
                    xmlTexto = xmlTexto.Substring(finDeclaracion + 2).Trim();
                }
            }

            using (SqlConnection cn = new SqlConnection(cadenaConexion))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.sp_Factura_GuardarXML", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdFactura", idFactura);
                    cmd.Parameters.AddWithValue("@XmlFactura", xmlTexto);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

















