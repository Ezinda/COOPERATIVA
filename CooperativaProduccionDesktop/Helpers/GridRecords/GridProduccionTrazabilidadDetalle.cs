using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridProduccionTrazabilidadDetalle
    {
        public System.DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Fardo { get; set; }
        public double Kilos { get; set; }
        public string Clase { get; set; }
        public string Tabaco { get; set; }
        public string Blend { get; set; }

    }
}
