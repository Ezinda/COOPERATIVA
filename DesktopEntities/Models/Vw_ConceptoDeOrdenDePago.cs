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
    
    public partial class Vw_ConceptoDeOrdenDePago
    {
        public System.Guid Id { get; set; }
        public System.Guid OrdenPagoId { get; set; }
        public System.Guid PesadaId { get; set; }
        public Nullable<System.Guid> ProductorId { get; set; }
        public Nullable<System.DateTime> FechaInternaDeLiquidacion { get; set; }
        public Nullable<System.DateTime> FechaDeLiquidacionDeAFIP { get; set; }
        public string Letra { get; set; }
        public Nullable<int> PuntoDeVentaDeLiquidacion { get; set; }
        public Nullable<long> NumeroInternoDeLiquidacion { get; set; }
        public string NumeroDeLiquidacionDeAFIP { get; set; }
        public decimal Kilos { get; set; }
        public decimal ImportePorPagar { get; set; }
        public decimal NetoPorPagar { get; set; }
        public Nullable<decimal> Pagado { get; set; }
        public decimal RetencionIIBB { get; set; }
        public decimal RetencionEEAOC { get; set; }
        public decimal RetencionSaludPublica { get; set; }
        public decimal RetencionGADM { get; set; }
        public decimal RetencionGanancias { get; set; }
        public decimal RetencionRiego { get; set; }
    }
}
