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
    
    public partial class Vw_Movimiento
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> TransaccionId { get; set; }
        public string Documento { get; set; }
        public Nullable<System.Guid> DepositoId { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Unidad { get; set; }
        public Nullable<double> Ingreso { get; set; }
        public Nullable<double> Egreso { get; set; }
        public System.Guid CajaId { get; set; }
        public long NumeroCaja { get; set; }
        public long LoteCaja { get; set; }
        public System.DateTime FechaCaja { get; set; }
        public System.Guid ProductoId { get; set; }
        public string Cliente { get; set; }
        public string NumeroDocumento { get; set; }
    }
}
