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
    
    public partial class OrdenVentaDetalle
    {
        public System.Guid Id { get; set; }
        public System.Guid OrdenVentaId { get; set; }
        public System.Guid ProductoId { get; set; }
        public Nullable<int> Campaña { get; set; }
        public Nullable<long> DesdeCaja { get; set; }
        public Nullable<long> HastaCaja { get; set; }
    
        public virtual OrdenVenta OrdenVenta { get; set; }
    }
}
