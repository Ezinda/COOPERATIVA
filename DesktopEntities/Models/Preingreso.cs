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
    
    public partial class Preingreso
    {
        public Preingreso()
        {
            this.PreingresoDetalle = new HashSet<PreingresoDetalle>();
        }
    
        public System.Guid Id { get; set; }
        public string Transporte { get; set; }
        public string Chofer { get; set; }
        public string Patente { get; set; }
        public string NumRemito { get; set; }
        public string Observaciones { get; set; }
        public Nullable<int> NumeroPreingreso { get; set; }
    
        public virtual ICollection<PreingresoDetalle> PreingresoDetalle { get; set; }
    }
}
