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
    
    public partial class Vw_Romaneo
    {
        public System.Guid PesadaId { get; set; }
        public Nullable<System.DateTime> FechaRomaneo { get; set; }
        public string NumRomaneo { get; set; }
        public Nullable<System.Guid> ProductorId { get; set; }
        public string NOMBRE { get; set; }
        public string CUIT { get; set; }
        public string nrofet { get; set; }
        public string IVA { get; set; }
        public Nullable<double> Totalkg { get; set; }
        public Nullable<decimal> ImporteBruto { get; set; }
        public Nullable<long> numInternoLiquidacion { get; set; }
        public Nullable<System.DateTime> fechaInternaLiquidacion { get; set; }
        public string Letra { get; set; }
        public string Provincia { get; set; }
        public string numAfipLiquidacion { get; set; }
        public Nullable<System.DateTime> fechaAfipLiquidacion { get; set; }
        public string cae { get; set; }
        public Nullable<System.DateTime> fechaVtoCae { get; set; }
        public Nullable<System.Guid> OrdenPagoId { get; set; }
        public Nullable<bool> RomaneoPendiente { get; set; }
    }
}
