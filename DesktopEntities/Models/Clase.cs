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
    
    public partial class Clase
    {
        public System.Guid Id { get; set; }
        public System.Guid ClaseId { get; set; }
        public decimal Valor { get; set; }
        public System.DateTime FechaModificacion { get; set; }
        public bool Vigente { get; set; }
        public Nullable<int> Orden { get; set; }
    }
}
