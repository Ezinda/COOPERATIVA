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
    
    public partial class Usuario
    {
        public System.Guid Id { get; set; }
        public string Usuario1 { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Nullable<bool> Preingreso { get; set; }
        public Nullable<bool> Pesada { get; set; }
        public Nullable<bool> ReimpresionRomaneo { get; set; }
        public Nullable<bool> ResumenRomaneo { get; set; }
        public Nullable<bool> ResumenCompra { get; set; }
        public Nullable<bool> ResumenClaseMes { get; set; }
        public Nullable<bool> ResumenClaseTrimestre { get; set; }
        public Nullable<bool> Reclasificacion { get; set; }
        public Nullable<bool> GestionReclasificacion { get; set; }
        public Nullable<bool> Seguridad { get; set; }
        public Nullable<bool> Configuracion { get; set; }
        public Nullable<bool> Liquidar { get; set; }
        public Nullable<bool> LiquidacionSubirAfip { get; set; }
        public Nullable<bool> LiquidacionImprimir { get; set; }
        public Nullable<bool> GenerarOrdenPago { get; set; }
        public Nullable<bool> GestionCata { get; set; }
        public Nullable<bool> GestionCaja { get; set; }
        public Nullable<bool> GenerarOrdenVenta { get; set; }
        public Nullable<bool> GenerarRemitoElectronico { get; set; }
        public Nullable<bool> GestionarListaPrecio { get; set; }
    }
}
