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
    
    public partial class Vw_Preingreso
    {
        public System.Guid PreIngresoId { get; set; }
        public string Transporte { get; set; }
        public string Chofer { get; set; }
        public string Patente { get; set; }
        public string NumRemito { get; set; }
        public string Observaciones { get; set; }
        public Nullable<int> NumeroPreingreso { get; set; }
        public System.Guid PreIngresoDetalleId { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.TimeSpan Hora { get; set; }
        public Nullable<bool> Estado { get; set; }
        public Nullable<System.Guid> ProductorId { get; set; }
        public string FET { get; set; }
        public string Nombre { get; set; }
        public string Cuit { get; set; }
        public string Provincia { get; set; }
    }
}
