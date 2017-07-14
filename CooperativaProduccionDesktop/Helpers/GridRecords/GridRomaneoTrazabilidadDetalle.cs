using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridRomaneoTrazabilidadDetalle
    {
        public Guid Id { get; set; }
        public string Clase { get; set; }
        public Nullable<long> Fardos { get; set; }
        public Nullable<double> Kilos { get; set; }
        public string Reclasificacion { get; set; }
    }
}
