using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridLiquidacionDetalle
    {
        public string Clase { get; set; }
        public Nullable<int> Fardos { get; set; }
        public Nullable<double> Kilos { get; set; }
        public Nullable<decimal> ClasePrecio { get; set; }
        public Nullable<decimal> Total { get; set; }
    }
}
