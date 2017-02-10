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
    
    public partial class ConceptoDeOrdenDePago
    {
        public ConceptoDeOrdenDePago()
        {
            this.PagoDetalle = new HashSet<PagoDetalle>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid OrdenPagoId { get; set; }
        public System.Guid PesadaId { get; set; }
        public System.Guid ProductorId { get; set; }
        public decimal Kilos { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Neto { get; set; }
        public decimal RetencionIIBB { get; set; }
        public decimal RetencionEEAOC { get; set; }
        public decimal RetencionSaludPublica { get; set; }
        public decimal RetencionGADM { get; set; }
        public decimal RetencionGanancias { get; set; }
        public decimal RetencionRiego { get; set; }
    
        public virtual OrdenPago OrdenPago { get; set; }
        public virtual Pesada Pesada { get; set; }
        public virtual ICollection<PagoDetalle> PagoDetalle { get; set; }
    }
}