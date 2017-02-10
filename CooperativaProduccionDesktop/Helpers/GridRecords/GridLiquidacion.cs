using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridLiquidacion
    {
        public System.Guid PesadaId { get; set; }
        public Nullable<System.DateTime> fechaInternaLiquidacion { get; set; }
        public Nullable<long> numInternoLiquidacion { get; set; }
        public string NOMBRE { get; set; }
        public string CUIT { get; set; }
        public string nrofet { get; set; }
        public string Provincia { get; set; }
        public string Letra { get; set; }
        public Nullable<double> Totalkg { get; set; }
        public Nullable<decimal> ImporteBruto { get; set; }
        public Nullable<System.DateTime> fechaAfipLiquidacion { get; set; }
        public string numAfipLiquidacion { get; set; }
        public string cae { get; set; }
        public Nullable<System.DateTime> fechaVtoCae { get; set; }
        public List<GridLiquidacionDetalle> Detalle { get; set; }
    }
}
