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
    
    public partial class ProduccionMuestraDetalle
    {
        public System.Guid Id { get; set; }
        public System.Guid MuestraId { get; set; }
        public string Tamanio { get; set; }
        public int Kilos { get; set; }
        public decimal Porcentaje { get; set; }
    
        public virtual ProduccionMuestra ProduccionMuestra { get; set; }
    }
}
