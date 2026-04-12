using SVDE.Entidades.Procesos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SVDE.BLL.Servicios
{
    
        public class FacturaXML
        {
            public string GenerarXML(Factura factura)
            {
                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FacturasXML");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                string ruta = Path.Combine(carpeta, "Factura_" + factura.NumeroFactura + ".xml");

                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    OmitXmlDeclaration = true
                };

                using (XmlWriter writer = XmlWriter.Create(ruta, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Factura");

                    writer.WriteElementString("NumeroFactura", factura.NumeroFactura);
                    writer.WriteElementString("Fecha", factura.FechaFactura.ToString("yyyy-MM-dd HH:mm:ss"));
                    writer.WriteElementString("SubTotal", factura.SubTotal.ToString("0.00"));
                    writer.WriteElementString("Impuesto", factura.Impuesto.ToString("0.00"));
                    writer.WriteElementString("Total", factura.Total.ToString("0.00"));
                    writer.WriteElementString("TipoCambio", factura.TipoCambio.ToString("0.00"));
                    writer.WriteElementString("TotalDolares", factura.TotalDolares.ToString("0.00"));
                    writer.WriteElementString("Estado", factura.Estado ?? "");
                    writer.WriteElementString("Observaciones", factura.Observaciones ?? "");

                    writer.WriteStartElement("Cliente");
                    writer.WriteElementString("IdCliente", factura.IdCliente.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Detalle");

                    foreach (var item in factura.ListaDetalle)
                    {
                        writer.WriteStartElement("Linea");
                        writer.WriteElementString("IdProducto", item.IdProducto.ToString());
                        writer.WriteElementString("CodigoProducto", item.CodigoProducto ?? "");
                        writer.WriteElementString("NombreProducto", item.NombreProducto ?? "");
                        writer.WriteElementString("Cantidad", item.Cantidad.ToString());
                        writer.WriteElementString("PrecioUnitario", item.PrecioUnitario.ToString("0.00"));
                        writer.WriteElementString("SubTotalLinea", item.SubTotalLinea.ToString("0.00"));
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();

                    writer.WriteStartElement("Pagos");

                    foreach (var pago in factura.ListaPagos)
                    {
                        writer.WriteStartElement("Pago");
                        writer.WriteElementString("TipoPago", pago.TipoPago ?? "");
                        writer.WriteElementString("Monto", pago.Monto.ToString("0.00"));
                        writer.WriteElementString("NumeroTarjeta", pago.NumeroTarjeta ?? "");
                        writer.WriteElementString("BancoTarjeta", pago.BancoTarjeta ?? "");
                        writer.WriteElementString("TipoTarjeta", pago.TipoTarjeta ?? "");
                        writer.WriteElementString("BancoTransferencia", pago.BancoTransferencia ?? "");
                        writer.WriteElementString("NumeroTransferencia", pago.NumeroTransferencia ?? "");
                        writer.WriteElementString("NumeroSinpe", pago.NumeroSinpe ?? "");
                        writer.WriteElementString("ReferenciaSinpe", pago.ReferenciaSinpe ?? "");
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                return ruta;
            }
        }
    }
