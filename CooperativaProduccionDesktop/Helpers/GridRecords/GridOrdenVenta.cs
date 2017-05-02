using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridOrdenVenta
    {
        public System.Guid Id { get; set; }
        public System.DateTime Fecha { get; set; }
        public long NumOperacion { get; set; }
        public long NumOrden { get; set; }
        public string Cliente { get; set; }
        public string Pendiente { get; set; }

        public List<GridOrdenVentaDetalle> OrdenVentaDetalle { get; set; }
    }
}
