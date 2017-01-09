//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DesktopEntities.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pesada
    {
        public System.Guid Id { get; set; }
        public Nullable<long> NumPesada { get; set; }
        public Nullable<System.Guid> PreingresoId { get; set; }
        public Nullable<System.Guid> ProductorId { get; set; }
        public Nullable<int> TotalFardo { get; set; }
        public Nullable<double> TotalKg { get; set; }
        public Nullable<decimal> ImporteBruto { get; set; }
        public Nullable<decimal> PrecioPromedio { get; set; }
        public Nullable<System.DateTime> FechaRomaneo { get; set; }
        public string NumRomaneo { get; set; }
        public Nullable<bool> RomaneoPendiente { get; set; }
        public Nullable<System.DateTime> FechaInternaLiquidacion { get; set; }
        public Nullable<long> NumInternoLiquidacion { get; set; }
        public string condIva { get; set; }
        public Nullable<decimal> IvaPorcentaje { get; set; }
        public Nullable<decimal> IvaCalculado { get; set; }
        public Nullable<decimal> ImporteNeto { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string NumAfipLiquidacion { get; set; }
        public Nullable<System.DateTime> FechaAfipLiquidacion { get; set; }
        public string Cae { get; set; }
        public Nullable<System.DateTime> FechaVtoCae { get; set; }
        public Nullable<System.Guid> OrdenPagoId { get; set; }
    }
}
