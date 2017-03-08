using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ReportModels
{
    public class RegistroEncabezado
    {
        public Guid _ID { get; set; }

        public string CuitProductor { get; set; }
        public string RazonSocialProductor { get; set; }
        public string CalleProductor { get; set; }
        //public string NroPuertaProductor { get; set; }
        //public string PisoProductor { get; set; }
        //public string OficinaDptoLocalProductor { get; set; }
        //public string SectorProductor { get; set; }
        //public string TorreProductor { get; set; }
        //public string ManzanaProductor { get; set; }
        //public string CodigoPostalProductor { get; set; }
        //public string LocalidadProductor { get; set; }
        public string CodigoProvinciaProductor { get; set; }

        //public string CodigoProvinciaTabacoProductor { get; set; }
        //public string LocalidadTabacoProductor { get; set; }

        public DateTime FechaRomaneo { get; set; }
        public long NumeroRomaneo { get; set; }
        public string VariedadTabaco { get; set; }
        
        public long PuntoDeVentaFacturaLiquidacion { get; set; }
        public long NumeroFacturaLiquidacion { get; set; }
        public string CodigoTipoComprobante { get; set; }
        public DateTime FechaFacturaLiquidacionDI { get; set; }

        public decimal ImporteNetoGravado { get; set; }
    }
}
