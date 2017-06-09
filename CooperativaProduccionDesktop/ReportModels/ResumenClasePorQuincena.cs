using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ReportModels
{
    public class ResumenClasePorQuincena
    {
        public string Clase { get; set; }
        public string Tabaco { get; set; }
        public decimal Quincena01 { get; set; }
        public decimal Quincena02 { get; set; }
        public decimal Quincena03 { get; set; }
        public decimal Quincena04 { get; set; }
        public decimal TotalKilos { get; set; }
        public decimal PrecioPorKilo { get; set; }
    }
}
