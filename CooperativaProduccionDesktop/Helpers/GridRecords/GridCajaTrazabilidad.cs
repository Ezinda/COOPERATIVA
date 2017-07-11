using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridCajaTrazabilidad
    {
        public Guid Id { get; set; }
        public int Campaña { get; set; }
        public long NumLote { get; set; }
        public long NumCaja { get; set; }
        public string Producto { get; set; }
        public decimal Tara { get; set; }
        public decimal Neto { get; set; }
        public decimal Bruto { get; set; }
        public long? Cata { get; set; }
        public Guid? OrdenVentaId { get; set; }
        public long? NumOrden { get; set; }
        public string Fecha { get; set; }

        public List<GridProduccionTrazabilidadDetalle> ProduccionTrazabilidadDetalle { get; set; }

        public List<GridRomaneoTrazabilidadDetalle> RomaneoTrazabilidadDetalle { get; set; }
    }
}
