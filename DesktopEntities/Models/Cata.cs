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
    
    public partial class Cata
    {
        public Cata()
        {
            this.Caja = new HashSet<Caja>();
        }
    
        public System.Guid Id { get; set; }
        public Nullable<long> Lote { get; set; }
        public Nullable<long> NumCata { get; set; }
        public Nullable<long> NumOrden { get; set; }
        public Nullable<long> NumCaja { get; set; }
        public Nullable<System.Guid> OrdenVentaId { get; set; }
        public Nullable<System.Guid> CajaId { get; set; }
    
        public virtual ICollection<Caja> Caja { get; set; }
    }
}
