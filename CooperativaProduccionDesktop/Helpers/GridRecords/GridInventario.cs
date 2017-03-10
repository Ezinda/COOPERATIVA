using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridInventario
    {
        public string Deposito { get; set; }
        public string TipoTabaco { get; set; }
        public string Unidad { get; set; }
        public Nullable<double> Ingreso { get; set; }
        public Nullable<double> Egreso { get; set; }
        public Nullable<double> Saldo { get; set; }
        public List<GridInventarioDetalle> Detalle { get; set; }
    }
}
