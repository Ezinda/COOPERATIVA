//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DesktopEntities.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vw_Pesada
    {
        public System.Guid PesadaId { get; set; }
        public System.Guid PesadaDetalleId { get; set; }
        public Nullable<long> NumFardo { get; set; }
        public Nullable<long> ContadorFardo { get; set; }
        public string Clase { get; set; }
        public decimal Valor { get; set; }
        public Nullable<double> Kilos { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<System.DateTime> FechaRomaneo { get; set; }
        public Nullable<int> PuntoVentaRomaneo { get; set; }
        public Nullable<long> NumRomaneo { get; set; }
        public Nullable<double> TotalKg { get; set; }
        public Nullable<decimal> ImporteBruto { get; set; }
        public string Productor { get; set; }
        public string Fet { get; set; }
        public string Cuit { get; set; }
        public string Provincia { get; set; }
        public string Localidad { get; set; }
        public string IVA { get; set; }
        public Nullable<System.DateTime> FechainternaLiquidacion { get; set; }
        public Nullable<int> PuntoVentaLiquidacion { get; set; }
        public Nullable<long> NumInternoLiquidacion { get; set; }
        public string condIva { get; set; }
        public string NumAfipLiquidacion { get; set; }
        public Nullable<System.DateTime> FechaAfipLiquidacion { get; set; }
        public Nullable<System.Guid> ID_PRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
    }
}
