using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridRomaneo
    {
        public System.Guid PesadaId { get; set; } 
        public string FechaRomaneo { get; set; }
        public string NumRomaneo { get; set; }
        public string NOMBRE { get; set; }
        public string CUIT { get; set; }
        public string nrofet { get; set; }
        public string Provincia { get; set; }
        public string TotalKg { get; set; }
        public string ImporteBruto { get; set; }
        public string Tabaco { get; set; }
        
        public List<GridRomaneoDetalle> Detalle { get; set; }

        public List<GridRomaneoResumenCompraDetalle> ResumenCompraDetalle { get; set; }
    }
}
