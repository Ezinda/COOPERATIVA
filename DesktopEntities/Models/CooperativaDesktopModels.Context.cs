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
    
        public virtual DbSet<Clase> Clase { get; set; }
        public virtual DbSet<Preingreso> Preingreso { get; set; }
        public virtual DbSet<PreingresoDetalle> PreingresoDetalle { get; set; }
        public virtual DbSet<Productor> Productor { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vw_Preingreso> Vw_Preingreso { get; set; }
        public virtual DbSet<Pesada> Pesada { get; set; }
        public virtual DbSet<Vw_Pesada> Vw_Pesada { get; set; }
        public virtual DbSet<PesadaDetalle> PesadaDetalle { get; set; }
        public virtual DbSet<Movimiento> Movimiento { get; set; }
    }
}
