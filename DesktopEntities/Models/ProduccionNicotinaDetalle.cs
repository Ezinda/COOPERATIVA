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
    
    public partial class ProduccionNicotinaDetalle
    {
        public System.Guid Id { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.Guid ProductoId { get; set; }
        public long Corrida { get; set; }
        public System.Guid CorridaId { get; set; }
        public System.TimeSpan Hora { get; set; }
        public long CajaDesde { get; set; }
        public long CajaHasta { get; set; }
        public decimal PorcentajeHumedad { get; set; }
        public decimal Valor1 { get; set; }
        public decimal Valor2 { get; set; }
        public decimal PorcentajeALC { get; set; }
        public decimal PorcentajeNicotina { get; set; }
    
        public virtual ProduccionBlend ProduccionBlend { get; set; }
    }
}
