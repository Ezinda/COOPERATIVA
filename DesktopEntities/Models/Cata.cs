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
    
    public partial class Cata
    {
        public System.Guid Id { get; set; }
        public Nullable<long> Lote { get; set; }
        public Nullable<long> NumCata { get; set; }
        public Nullable<long> NumOrden { get; set; }
        public Nullable<long> NumCaja { get; set; }
        public Nullable<System.Guid> OrdenVentaId { get; set; }
        public Nullable<System.Guid> CajaId { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
    
        public virtual Caja Caja { get; set; }
        public virtual OrdenVenta OrdenVenta { get; set; }
    }
}
