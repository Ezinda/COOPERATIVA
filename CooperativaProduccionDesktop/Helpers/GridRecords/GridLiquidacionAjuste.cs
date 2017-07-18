using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers.GridRecords
{
    public class GridLiquidacionAjuste
    {
        public Guid ProductorId { get; set; }
        public string Productor { get; set; }
        public string Cuit { get; set; }
        public string Fet { get; set; }
        public string Provincia { get; set; }
        public string Letra { get; set; }
        public decimal ImporteBruto { get; set; }
        public decimal ImporteNeto { get; set; }
        public decimal Ajuste { get; set; }
        public decimal IvaPorcentaje { get; set; }
        public decimal IvaCalculadoAjuste { get; set; }
        public decimal TotalAjuste { get; set; }
        public string Tabaco { get; set; }
        public DateTime FechaInternaLiquidacion { get; set; }
        public int PuntoVentaLiquidacion { get; set; }
        public long NumeroInternoLiquidacion { get; set; }
        public string CondicionIva { get; set; }
    }
}
