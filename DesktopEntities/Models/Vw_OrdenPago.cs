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
    
    public partial class Vw_OrdenPago
    {
        public System.Guid OrdenPagoId { get; set; }
        public Nullable<long> NumIntOrdenPago { get; set; }
        public Nullable<long> NumOrdenPago { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public Nullable<System.Guid> ProductorId { get; set; }
        public string NOMBRE { get; set; }
        public string CUIT { get; set; }
        public string nrofet { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> Ganancias { get; set; }
        public Nullable<decimal> IVA { get; set; }
        public Nullable<decimal> IIBB { get; set; }
        public Nullable<decimal> SaludPublica { get; set; }
        public Nullable<decimal> EEAOC { get; set; }
        public Nullable<decimal> Riego { get; set; }
        public Nullable<decimal> Monotributo { get; set; }
        public Nullable<decimal> Neto { get; set; }
        public string detalle { get; set; }
    }
}
