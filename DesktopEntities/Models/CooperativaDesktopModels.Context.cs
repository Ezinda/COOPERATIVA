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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CooperativaProduccionEntities : DbContext
    {
        public CooperativaProduccionEntities()
            : base("name=CooperativaProduccionEntities")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Caja> Caja { get; set; }
        public virtual DbSet<Cata> Cata { get; set; }
        public virtual DbSet<Clase> Clase { get; set; }
        public virtual DbSet<ConceptoDeOrdenDePago> ConceptoDeOrdenDePago { get; set; }
        public virtual DbSet<Configuracion> Configuracion { get; set; }
        public virtual DbSet<ConfiguracionBlend> ConfiguracionBlend { get; set; }
        public virtual DbSet<Contador> Contador { get; set; }
        public virtual DbSet<FardoEnProduccion> FardoEnProduccion { get; set; }
        public virtual DbSet<Liquidacion> Liquidacion { get; set; }
        public virtual DbSet<Movimiento> Movimiento { get; set; }
        public virtual DbSet<OrdenPago> OrdenPago { get; set; }
        public virtual DbSet<OrdenVenta> OrdenVenta { get; set; }
        public virtual DbSet<OrdenVentaDetalle> OrdenVentaDetalle { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<PagoDetalle> PagoDetalle { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<ParamPagos> ParamPagos { get; set; }
        public virtual DbSet<Pesada> Pesada { get; set; }
        public virtual DbSet<PesadaDetalle> PesadaDetalle { get; set; }
        public virtual DbSet<Preingreso> Preingreso { get; set; }
        public virtual DbSet<PreingresoDetalle> PreingresoDetalle { get; set; }
        public virtual DbSet<ProduccionBlend> ProduccionBlend { get; set; }
        public virtual DbSet<ProduccionCorrida> ProduccionCorrida { get; set; }
        public virtual DbSet<ProduccionHumedad> ProduccionHumedad { get; set; }
        public virtual DbSet<ProduccionHumedadDetalle> ProduccionHumedadDetalle { get; set; }
        public virtual DbSet<ProduccionMuestra> ProduccionMuestra { get; set; }
        public virtual DbSet<ProduccionMuestraDetalle> ProduccionMuestraDetalle { get; set; }
        public virtual DbSet<ProduccionNicotina> ProduccionNicotina { get; set; }
        public virtual DbSet<ProduccionNicotinaDetalle> ProduccionNicotinaDetalle { get; set; }
        public virtual DbSet<ProduccionTemperatura> ProduccionTemperatura { get; set; }
        public virtual DbSet<ProduccionTemperaturaDetalle> ProduccionTemperaturaDetalle { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<Remito> Remito { get; set; }
        public virtual DbSet<Turno> Turno { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vw_Clase> Vw_Clase { get; set; }
        public virtual DbSet<Vw_Cliente> Vw_Cliente { get; set; }
        public virtual DbSet<Vw_ConceptoDeOrdenDePago> Vw_ConceptoDeOrdenDePago { get; set; }
        public virtual DbSet<Vw_Deposito> Vw_Deposito { get; set; }
        public virtual DbSet<Vw_FardoEnProduccionPorHora> Vw_FardoEnProduccionPorHora { get; set; }
        public virtual DbSet<Vw_LiquidacionAjuste> Vw_LiquidacionAjuste { get; set; }
        public virtual DbSet<Vw_Movimiento> Vw_Movimiento { get; set; }
        public virtual DbSet<Vw_OrdenPago> Vw_OrdenPago { get; set; }
        public virtual DbSet<Vw_Pesada> Vw_Pesada { get; set; }
        public virtual DbSet<Vw_pesada_liq> Vw_pesada_liq { get; set; }
        public virtual DbSet<Vw_Preingreso> Vw_Preingreso { get; set; }
        public virtual DbSet<Vw_Producto> Vw_Producto { get; set; }
        public virtual DbSet<Vw_Productor> Vw_Productor { get; set; }
        public virtual DbSet<Vw_Provincia> Vw_Provincia { get; set; }
        public virtual DbSet<Vw_ResumenClasePorFecha> Vw_ResumenClasePorFecha { get; set; }
        public virtual DbSet<Vw_ResumenCompraPorClase> Vw_ResumenCompraPorClase { get; set; }
        public virtual DbSet<Vw_ResumenPesada> Vw_ResumenPesada { get; set; }
        public virtual DbSet<Vw_ResumenPesadaPorClase> Vw_ResumenPesadaPorClase { get; set; }
        public virtual DbSet<Vw_ResumenRomaneoBurley> Vw_ResumenRomaneoBurley { get; set; }
        public virtual DbSet<Vw_ResumenRomaneoPorClase> Vw_ResumenRomaneoPorClase { get; set; }
        public virtual DbSet<Vw_ResumenRomaneoVirginia> Vw_ResumenRomaneoVirginia { get; set; }
        public virtual DbSet<Vw_Romaneo> Vw_Romaneo { get; set; }
        public virtual DbSet<Vw_RomaneoOrdenPago> Vw_RomaneoOrdenPago { get; set; }
        public virtual DbSet<Vw_Stock> Vw_Stock { get; set; }
        public virtual DbSet<Vw_TipoTabaco> Vw_TipoTabaco { get; set; }
        public virtual DbSet<Vw_Transporte> Vw_Transporte { get; set; }
    
        public virtual ObjectResult<ActualizarLiquidacion_Result> ActualizarLiquidacion(Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta)
        {
            var fechaDesdeParameter = fechaDesde.HasValue ?
                new ObjectParameter("FechaDesde", fechaDesde) :
                new ObjectParameter("FechaDesde", typeof(System.DateTime));
    
            var fechaHastaParameter = fechaHasta.HasValue ?
                new ObjectParameter("FechaHasta", fechaHasta) :
                new ObjectParameter("FechaHasta", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ActualizarLiquidacion_Result>("ActualizarLiquidacion", fechaDesdeParameter, fechaHastaParameter);
        }
    
        public virtual int MigrarStock(Nullable<int> campaña, Nullable<bool> warrant)
        {
            var campañaParameter = campaña.HasValue ?
                new ObjectParameter("Campaña", campaña) :
                new ObjectParameter("Campaña", typeof(int));
    
            var warrantParameter = warrant.HasValue ?
                new ObjectParameter("Warrant", warrant) :
                new ObjectParameter("Warrant", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("MigrarStock", campañaParameter, warrantParameter);
        }
    }
}
