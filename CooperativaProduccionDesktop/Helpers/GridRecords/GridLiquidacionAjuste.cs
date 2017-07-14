using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridLiquidacionAjuste
    {
        public Nullable<long> numInternoLiquidacion { get; set; }
        public string PRODUCTOR { get; set; }
        public string CUIT { get; set; }
        public string FET { get; set; }
        public string PROVINCIA { get; set; }
        public string LETRA { get; set; }
        public Nullable<decimal> BRUTOSINIVA { get; set; }
        public Nullable<decimal> Ajuste { get; set; }
        public string TABACO { get; set; }
        public int PuntoVentaLiquidacion { get; set; }
        public DateTime FechaInternaLiquidacion { get; set; }
        public long NumInternoLiquidacion { get; set; }
        public string condIva { get; set; }
        public decimal IvaPorcentaje { get; set; }
        public decimal ImporteNeto { get; set; }
        public decimal IvaCalculado { get; set; }
        public decimal Total { get; set; }
     
    }
}
