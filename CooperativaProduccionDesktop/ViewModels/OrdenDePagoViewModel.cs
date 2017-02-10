using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ViewModels
{
    public class LiquidacionesParaOPViewModel
    {
        public decimal CoeficienteDeRetenciones { get; set; }

        public List<RetencionDetalleViewModel> Retenciones { get; set; }

        public List<LiquidacionParaOPViewModel> Items { get; set; }
    }

    public class LiquidacionParaOPViewModel
    {
        public Guid PesadaId { get; set; }

        public Guid ProductorId { get; set; }

        public Nullable<long> NumeroDeLiquidacionInterno { get; set; }

        public string NumeroDeLiquidacionDeAFIP { get; set; } // su valor es igual al de NUMEROCOMPROBANTE

        public Nullable<DateTime> FechaDeLiquidacionDeAFIP { get; set; }

        public string NombreDeProductor { get; set; }

        public string NumeroDeFET { get; set; }

        public Nullable<double> TotalEnKilos { get; set; }

        public decimal ImporteBrutoSinIVA { get; set; }

        public decimal ImportePorPagar { get; set; }

        public List<RetencionAplicadaDetalleViewModel> RetencionesAplicadas { get; set; }

        public decimal NetoPorPagar { get; set; }
    }

    //OrdenPago ordenPago;
    //ordenPago = new OrdenPago();
    //ordenPago.Id = Guid.NewGuid();
    //ordenPago.NumIntOrdenPago = ContadorNumeroInternoOP();
    //ordenPago.NumOrdenPago = Int64.Parse(txtNumeroOP.Text);
    //Guid ProductorId = new Guid(gridViewLiquidacion.GetRowCellValue(i, "PRODUCTORID").ToString());
    //ordenPago.ProductorId = ProductorId;
    //ordenPago.Fecha = dpFechaPago.Value.Date;
    //decimal afectar = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "AFECTAR").ToString());
    //ordenPago.Subtotal = afectar;

    //decimal gcias = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "GCIAS").ToString());
    //ordenPago.Ganancias = gcias;
    //decimal iva = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "IVA").ToString());
    //ordenPago.IVA = iva;
    //decimal iibb = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "IIBB").ToString());
    //ordenPago.IIBB = iibb;
    //decimal salud = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "SaludPublica").ToString());
    //ordenPago.SaludPublica = salud;
    //decimal eeaoc = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "EEAOC").ToString());
    //ordenPago.EEAOC = eeaoc;
    //decimal riego = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "Riego").ToString());
    //ordenPago.Riego = riego;
    //decimal monotributo = decimal.Parse(gridViewLiquidacion.GetRowCellValue(i, "Monotributo").ToString());
    //ordenPago.Monotributo = monotributo;
    
    //ordenPago.Neto = afectar + gcias + iva + iibb + salud + eeaoc + riego + monotributo;
    //ordenPago.Detalle = txtObservaciones.Text;

    //public class OrdenesDePagoViewModel
    //{
    //    public List<RetencionViewModel> Retenciones { get; set; }
    //
    //    public List<OrdenDePagoViewModel> Items { get; set; }
    //}
    //
    //public class OrdenDePagoViewModel
    //{
    //    public Guid Id { get; set; }
    //
    //    public long NumeroDeOrden { get; set; }
    //
    //    public long NumeroInternoDeOrden { get; set; }
    //
    //    public Guid PesadaId { get; set; }
    //
    //    public Guid ProductorId { get; set; }
    //
    //    public DateTime Fecha { get; set; }
    //
    //    public decimal ImportePorPagar { get; set; }
    //
    //    public List<RetencionAplicadaViewModel> RetencionesAplicadas { get; set; }
    //
    //    public decimal NetoPorPagar { get; set; }
    //
    //    public string Observaciones { get; set; }
    //}

    public class OrdenesDePagoDetalleViewModel
    {
        public List<RetencionViewModel> Retenciones { get; set; }

        public List<OrdenDePagoDetalleViewModel> Items { get; set; }
    }

    public class OrdenDePagoDetalleViewModel
    {
        public Guid Id { get; set; }

        public long NumeroDeOrden { get; set; }

        public long NumeroInternoDeOrden { get; set; }

        public Guid PesadaId { get; set; }

        public Guid ProductorId { get; set; }

        public string Productor { get; set; }

        public string FET { get; set; }

        public string CUIT { get; set; }

        public DateTime Fecha { get; set; }

        public decimal ImportePorPagar { get; set; }

        public List<RetencionAplicadaViewModel> RetencionesAplicadas { get; set; }

        public decimal NetoPorPagar { get; set; }

        public string Observaciones { get; set; }

        public decimal? ImporteNeto { get; set; }
    }


    public class OrdenDePagoViewModel
    {
        public Guid Id { get; set; }

        public long NumeroDeOrden { get; set; }

        public long NumeroInternoDeOrden { get; set; }

        public DateTime FechaDePago { get; set; }

        public List<RetencionAplicadaViewModel> RetencionesAplicadas { get; set; }

        public List<ConceptoDeOrdenDePagoViewModel> Items { get; set; }

        public decimal ImporteNeto { get; set; }

        public string Observaciones { get; set; }
    }

    /// <summary>
    /// Un Concepto de OP es una liquidación o parte de la misma
    /// asignada a una Orden de Pago
    /// </summary>
    public class ConceptoDeOrdenDePagoViewModel
    {
        public Guid Id { get; set; }

        public Guid PesadaId { get; set; }
        
        public Guid ProductorId { get; set; }

        public decimal KilosAfectados { get; set; }

        public decimal ImportePorPagar { get; set; }
        
        public List<RetencionAplicadaViewModel> RetencionesAplicadas { get; set; }
        
        public decimal NetoPorPagar { get; set; }
    }

    public class ConceptoDescripcionDeOrdenDePagoViewModel
    {
        public Guid Id { get; set; }

        public Guid PesadaId { get; set; }

        public Guid ProductorId { get; set; }

        public DateTime Fecha { get; set; }

        public string TipoDeFactura { get; set; }

        public int PuntoDeVenta { get; set; }

        public long NumeroDeLiquidacion { get; set; }

        public decimal Kilos { get; set; }

        public decimal ImportePorPagar { get; set; }

        public decimal NetoPorPagar { get; set; } // Importe

        public decimal RestaPorPagar { get; set; } // Saldo
    }
}
