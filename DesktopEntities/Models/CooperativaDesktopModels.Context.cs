﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CooperativaProduccionEntities : DbContext
    {
        public CooperativaProduccionEntities()
            : base("name=CooperativaProduccionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Caja> Caja { get; set; }
        public virtual DbSet<Cata> Cata { get; set; }
        public virtual DbSet<Clase> Clase { get; set; }
        public virtual DbSet<Contador> Contador { get; set; }
        public virtual DbSet<Movimiento> Movimiento { get; set; }
        public virtual DbSet<OrdenPago> OrdenPago { get; set; }
        public virtual DbSet<OrdenVenta> OrdenVenta { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<PesadaDetalle> PesadaDetalle { get; set; }
        public virtual DbSet<Preingreso> Preingreso { get; set; }
        public virtual DbSet<PreingresoDetalle> PreingresoDetalle { get; set; }
        public virtual DbSet<Remito> Remito { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vw_Clase> Vw_Clase { get; set; }
        public virtual DbSet<Vw_Cliente> Vw_Cliente { get; set; }
        public virtual DbSet<Vw_Movimiento> Vw_Movimiento { get; set; }
        public virtual DbSet<Vw_OrdenPago> Vw_OrdenPago { get; set; }
        public virtual DbSet<Vw_Pesada> Vw_Pesada { get; set; }
        public virtual DbSet<Vw_Preingreso> Vw_Preingreso { get; set; }
        public virtual DbSet<Vw_Producto> Vw_Producto { get; set; }
        public virtual DbSet<Vw_Productor> Vw_Productor { get; set; }
        public virtual DbSet<Vw_ResumenPesada> Vw_ResumenPesada { get; set; }
        public virtual DbSet<Vw_ResumenPesadaPorClase> Vw_ResumenPesadaPorClase { get; set; }
        public virtual DbSet<Vw_ResumenRomaneoPorClase> Vw_ResumenRomaneoPorClase { get; set; }
        public virtual DbSet<Vw_Romaneo> Vw_Romaneo { get; set; }
        public virtual DbSet<Pesada> Pesada { get; set; }
    }
}
