using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridTurno
    {
        public System.Guid Id { get; set; } 
        public string FechaSolicitud { get; set; }
        public string FechaTurno { get; set; }
        public string nrofet { get; set; }
        public string NOMBRE { get; set; }
        public string CUIT { get; set; }
        public string Provincia { get; set; }
        public string Telefono { get; set; }
        public string Kilos { get; set; }
        
    }
}
