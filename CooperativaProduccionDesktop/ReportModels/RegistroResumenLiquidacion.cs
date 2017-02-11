using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ReportModels
{
    public class RegistroResumenLiquidacion
    {
        public string FechaInternaLiquidacion { get; set; }
        public string NumInternoLiquidacion { get; set; }
        public string NOMBRE { get; set; }
        public string CUIT { get; set; }
        public string nrofet { get; set; }
        public string IVA { get; set; }
        public string IvaCalculado { get; set; }
        public string TotalKg { get; set; }
        public string ImporteBruto { get; set; }
        public string ImporteNeto { get; set; }
        public string Importeporpagar { get; set; }
        public string PuntoVentaLiquidacion { get; set; }
        public string Letra { get; set; }
        public string Provincia { get; set; }
        public string NumAfipLiquidacion { get; set; }
        public string FechaAfipLiquidacion { get; set; }
        public string Tabaco { get; set; }
        public string TC { get; set; }
    }
}
