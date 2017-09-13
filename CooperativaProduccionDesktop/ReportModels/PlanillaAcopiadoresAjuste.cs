using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ReportModels
{
    public class PlanillaAcopiadoresAjuste
    {
        public string Clase { get; set; }
        public string Tabaco { get; set; }
        public decimal Kilos01 { get; set; }
        public decimal Kilos02 { get; set; }
        public decimal Kilos03 { get; set; }
        public decimal Kilos04 { get; set; }
        public decimal Kilos05 { get; set; }
        public decimal TotalKilos { get; set; }
        public decimal PrecioPorKilo { get; set; }
        public decimal PrecioPorKiloAjuste { get; set; }
    }
}
