using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridRomaneo
    {
        public System.Guid PesadaId { get; set; } 
        public Nullable<System.DateTime> FechaRomaneo { get; set; }
        public string nrofet { get; set; }
        public string NOMBRE { get; set; }
        public string Provincia { get; set; }
        public string Tabaco { get; set; }
        public List<GridRomaneoDetalle> Detalle { get; set; }
    }
}
