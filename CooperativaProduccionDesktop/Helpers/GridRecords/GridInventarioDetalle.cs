using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridInventarioDetalle
    {
        public string Clase { get; set; }
        public Nullable<double> Ingreso { get; set; }
        public Nullable<double> Egreso { get; set; }
        public Nullable<double> Saldo { get; set; }
    }
}
