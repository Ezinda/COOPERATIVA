using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridOrdenVentaDetalle
    {
        public System.Guid Id { get; set; }
        public string Producto { get; set; }
        public Nullable<long> DesdeCaja { get; set; }
        public Nullable<long> HastaCaja { get; set; }
    }
}
