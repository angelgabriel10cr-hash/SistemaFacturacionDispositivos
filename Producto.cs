using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDE.Entidades.Maestros
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string CodigoInterno { get; set; }
        public string CodigoBarras { get; set; }
        public int IdTipoDispositivo { get; set; }
        public int IdMarca { get; set; }
        public int IdModelo { get; set; }
        public int IdColor { get; set; }
        public string NombreComercial { get; set; }
        public string Caracteristicas { get; set; }
        public byte[] DocumentoEspecificacion { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public int GarantiaMeses { get; set; }
        public bool Estado { get; set; }

        public string TipoDispositivo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        
        public string NombreDocumentoEspecificacion { get; set; }
        public string ExtensionDocumentoEspecificacion { get; set; }
        public byte[] Fotografia { get; set; }

    }
}
