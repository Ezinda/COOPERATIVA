using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ReportModels
{
    public class RegistroRenglon
    {
        public long NumeroRomaneo { get; set; }
        
        public string Clase { get; set; }
        public decimal PesoFardoEnKilos { get; set; }
        public long CodigoTrazabilidadInterno { get; set; }
    }
}
