using CooperativaProduccion.ViewModels;
using DesktopEntities.Models;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CooperativaProduccion.Helpers
{
    public class PagosManager : IPagosManager
    {
        public LiquidacionesParaOPViewModel ListarLiquidacionesParaPagarEnPorcentaje(decimal porcentajeporpagar)
        {
            Expression<Func<Vw_Romaneo, bool>> pred = x => true;
            var entities = _GetEntitiesLiquidaciones(pred, porcentajeporpagar, LiquidacionesQueryType.PorPorcentaje);
            var result = _GetLiquidaciones(entities);

            return result;
        }

        public LiquidacionesParaOPViewModel ListarLiquidacionesParaPagarEnPorcentaje(decimal porcentajeporpagar, DateTime desde, DateTime hasta)
        {
            Expression<Func<Vw_Romaneo, bool>> pred = x =>
                x.FechaAfipLiquidacion.Value >= desde
                && x.FechaAfipLiquidacion.Value <= hasta;

            var entities = _GetEntitiesLiquidaciones(pred, porcentajeporpagar, LiquidacionesQueryType.PorPorcentaje);
            var result = _GetLiquidaciones(entities);

            return result;
        }

        public LiquidacionesParaOPViewModel ListarLiquidacionesParaPagarEnKilos(decimal kilos)
        {
            Expression<Func<Vw_Romaneo, bool>> pred = x => true;
            var entities = _GetEntitiesLiquidaciones(pred, kilos, LiquidacionesQueryType.PorKilo);
            var result = _GetLiquidaciones(entities);

            return result;
        }

        public LiquidacionesParaOPViewModel ListarLiquidacionesParaPagarEnKilos(decimal kilos, DateTime desde, DateTime hasta)
        {
            Expression<Func<Vw_Romaneo, bool>> pred = x =>
                x.FechaAfipLiquidacion.Value >= desde
                && x.FechaAfipLiquidacion.Value <= hasta;

            var entities = _GetEntitiesLiquidaciones(pred, kilos, LiquidacionesQueryType.PorKilo);
            var result = _GetLiquidaciones(entities);

            return result;
        }

        public long GetNumeroDeOrden()
        {
            using (var context = new CooperativaProduccionEntities())
            {
                var ultimo = context.OrdenPago.Select(p => p.NumOrdenPago).DefaultIfEmpty(0).Max();
                
                return ultimo + 1;
            }
        }

        public long GetNumeroInternoDeOrden()
        {
            using (var context = new CooperativaProduccionEntities())
            {
                var ultimo = context.OrdenPago.Select(p => p.NumIntOrdenPago).DefaultIfEmpty(0).Max();

                return ultimo + 1;
            }
        }

        public void GenerarOrdenDePago(OrdenDePagoViewModel ordenvm)
        {
            var ordenid = Guid.NewGuid();
            var idspesadasdeorden = new List<Guid>();
            //var conceptosdepesadas = new Dictionary<Guid, Guid>();

            using (var context = new CooperativaProduccionEntities())
            {
                var conceptos = new List<ConceptoDeOrdenDePago>();

                foreach (var item in ordenvm.Items)
                {
                    var concepto = new ConceptoDeOrdenDePago();

                    concepto.Id = Guid.NewGuid();
                    concepto.PesadaId = item.PesadaId;
                    concepto.ProductorId = item.ProductorId;

                    concepto.Kilos = item.KilosAfectados;
                    concepto.Subtotal = item.ImportePorPagar;

                    var retencionIIBB = item.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionIIBB).Single();
                    var retencionEEAOC = item.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionEEAOC).Single();
                    var retencionSaludPublica = item.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionSaludPublica).Single();
                    var retencionGADM = item.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionGADM).Single();
                    var retencionGCIAS = item.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionGCIAS).Single();
                    var retencionRiego = item.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionRiego).Single();

                    concepto.RetencionIIBB = retencionIIBB.Importe;
                    concepto.RetencionEEAOC = retencionEEAOC.Importe;
                    concepto.RetencionSaludPublica = retencionSaludPublica.Importe;
                    concepto.RetencionGADM = retencionGADM.Importe;
                    concepto.RetencionGanancias = retencionGCIAS.Importe;
                    concepto.RetencionRiego = retencionRiego.Importe;

                    concepto.Neto = item.NetoPorPagar;

                    idspesadasdeorden.Add(item.PesadaId);
                    //conceptosdepesadas.Add(concepto.Id, item.PesadaId);
                    conceptos.Add(concepto);
                }

                var numerodeorden = this.GetNumeroDeOrden();
                var numerointernodeorden = this.GetNumeroInternoDeOrden();

                var orden = new OrdenPago();

                orden.Id = ordenid;
                orden.NumOrdenPago = numerodeorden;
                orden.NumIntOrdenPago = numerointernodeorden;
                orden.Fecha = ordenvm.FechaDePago;
                orden.Detalle = ordenvm.Observaciones;

                var retenciondeordenIIBB = ordenvm.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionIIBB).Single();
                var retenciondeordenEEAOC = ordenvm.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionEEAOC).Single();
                var retenciondeordenSaludPublica = ordenvm.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionSaludPublica).Single();
                var retenciondeordenGADM = ordenvm.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionGADM).Single();
                var retenciondeordenGCIAS = ordenvm.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionGCIAS).Single();
                var retenciondeordenRiego = ordenvm.RetencionesAplicadas.Where(x => x.Nombre == RetencionTypes.RetencionRiego).Single();

                orden.IIBB = retenciondeordenIIBB.Importe;
                orden.EEAOC = retenciondeordenEEAOC.Importe;
                orden.SaludPublica = retenciondeordenSaludPublica.Importe;
                orden.GADM = retenciondeordenGADM.Importe;
                orden.Ganancias = retenciondeordenGCIAS.Importe;
                orden.Riego = retenciondeordenRiego.Importe;

                orden.ConceptoDeOrdenDePago = conceptos;

                orden.Neto = ordenvm.ImporteNeto;

                context.OrdenPago.Add(orden);
                context.SaveChanges();
            }

            // UNA LIQUIDACION PUEDE TENER MUCHAS ORDENES

            //using (var context = new CooperativaProduccionEntities())
            //{
            //    var liquidaciones = context.Pesada
            //        .Where(x => idspesadasdeorden.ContainsKey(x.Id))
            //        //.Where(x => conceptosdepesadas.ContainsKey(x.Id))
            //        .ToList();
            //
            //    foreach (var item in liquidaciones)
	        //    {
            //        item.OrdenPagoId = ordenid;
            //        //item.OrdenPagoId = conceptosdepesadas[item.Id];
            //
            //        context.Entry(item).State = System.Data.Entity.EntityState.Modified;
	        //    }
            //
            //    context.SaveChanges();
            //}
        }

        public OrdenesDePagoDetalleViewModel ListarOrdenesDePago()
        {
            Expression<Func<Vw_OrdenPago, bool>> pred = x => true;

            var entities = _GetEntitiesOrdenesDePago(pred);
            var result = _GetViewModelOrdenesDePagos(entities);

            return result;
        }

        public OrdenesDePagoDetalleViewModel ListarOrdenesDePago(DateTime desde, DateTime hasta)
        {
            Expression<Func<Vw_OrdenPago, bool>> pred = x => true;

            pred = pred.And(x => x.Fecha >= desde && x.Fecha <= hasta);

            var entities = _GetEntitiesOrdenesDePago(pred);
            var result = _GetViewModelOrdenesDePagos(entities);

            return result;
        }

        public OrdenesDePagoDetalleViewModel ListarOrdenesDePagoDeProductor(Guid productorid)
        {
            Expression<Func<Vw_OrdenPago, bool>> pred = x => true;

            pred = pred.And(x => x.ProductorId == productorid);

            var entities = _GetEntitiesOrdenesDePago(pred);
            var result = _GetViewModelOrdenesDePagos(entities);

            return result;
        }

        public OrdenesDePagoDetalleViewModel ListarOrdenesDePagoDeProductor(Guid productorid, DateTime desde, DateTime hasta)
        {
            Expression<Func<Vw_OrdenPago, bool>> pred = x => true;

            pred = pred.And(x => x.Fecha >= desde && x.Fecha <= hasta);
            pred = pred.And(x => x.ProductorId == productorid);

            var entities = _GetEntitiesOrdenesDePago(pred);
            var result = _GetViewModelOrdenesDePagos(entities);

            return result;
        }

        public OrdenesDePagoDetalleViewModel GetOrdenDePago(Guid id)
        {
            var entitie = _GetEntitieOrdenDePago(id);
            var list = new List<Vw_OrdenPago>()
            {
                entitie
            };

            var result = _GetViewModelOrdenesDePagos(list);

            return result;
        }

        public List<ConceptoDescripcionDeOrdenDePagoViewModel> ListarConceptosDeOrdenDePago(Guid ordendepagoid)
        {
            var entities = _GetEntitiesConceptoDetalle(ordendepagoid);

            var list = new List<ConceptoDescripcionDeOrdenDePagoViewModel>();
            
            foreach (var item in entities)
	        {
                var conceptovm = new ConceptoDescripcionDeOrdenDePagoViewModel()
                {
                    Id = item.Id,
                    PesadaId = item.PesadaId,
                    ProductorId = item.ProductorId.Value,
                    Fecha = item.FechaInternaDeLiquidacion ?? new DateTime(),
                    TipoDeFactura = item.Letra,
                    PuntoDeVenta = item.PuntoDeVentaDeLiquidacion ?? 0,
                    NumeroDeLiquidacion = item.NumeroInternoDeLiquidacion ?? 0,
                    Kilos = item.Kilos,
                    ImportePorPagar = item.ImportePorPagar,
                    NetoPorPagar = item.NetoPorPagar,
                    RestaPorPagar = item.ImportePorPagar - Convert.ToDecimal(item.Pagado),
                };

                list.Add(conceptovm);
	        }

            return list;
        }

        private List<Vw_ConceptoDeOrdenDePago> _GetEntitiesConceptoDetalle(Guid ordendepagoid)
        {
            using (var context = new CooperativaProduccionEntities())
            {
                var list = context.Vw_ConceptoDeOrdenDePago
                    .Where(x => x.OrdenPagoId == ordendepagoid)
                    .ToList();

                return list;
            }
        }

        private List<ConceptoDeOrdenDePago> _GetEntitiesConceptos(Guid ordendepagoid)
        {
            using (var context = new CooperativaProduccionEntities())
            {
                var list = context.ConceptoDeOrdenDePago
                    .Where(x => x.OrdenPagoId == ordendepagoid)
                    .ToList();

                return list;
            }
        }

        private LiquidacionesParaOPViewModel _GetLiquidaciones(List<Vw_Romaneo> entities)
        {
            decimal coeficiente = decimal.Parse("1.21");

            // VALORES DE PRUEBA
            //decimal iva = decimal.Parse("21");
            //decimal gcias = decimal.Parse("0.02");
            //decimal iibb = decimal.Parse("0.0175");
            //decimal coeficientegral = decimal.Parse("0.5");

            var ordenes = new List<LiquidacionParaOPViewModel>();

            foreach (var x in entities)
            {
                var orden = new LiquidacionParaOPViewModel()
                {
                    PesadaId = x.PesadaId,
                    ProductorId = x.ProductorId.Value,
                    NumeroDeLiquidacionInterno = x.NumInternoLiquidacion,
                    NumeroDeLiquidacionDeAFIP = x.NumAfipLiquidacion,
                    FechaDeLiquidacionDeAFIP = x.FechaAfipLiquidacion,
                    NombreDeProductor = x.NOMBRE,
                    NumeroDeFET = x.nrofet,
                    TotalEnKilos = x.TotalKg,
                    ImporteBrutoSinIVA = x.ImporteBruto.Value,
                    ImportePorPagar = decimal.Round(Convert.ToDecimal(x.Importeporpagar.Value), 2, MidpointRounding.AwayFromZero), // decimal.Round((x.ImporteBruto.Value * porcentajeporpagar) / 100, 2, MidpointRounding.AwayFromZero),
                    NetoPorPagar = 0.0m
                };

                var retencionesAplicadas = new List<RetencionAplicadaDetalleViewModel>()
                    {
                        new RetencionAplicadaDetalleViewModel()
                        {
                            Nombre = RetencionTypes.RetencionGCIAS,
                            Porcentaje = x.GANANCIA.Value,
                            Base = Convert.ToDecimal(x.BASEGANANCIA),
                            UsaBase = true,
                            Importe = _CalcularImporteDeRetencion(
                                Convert.ToDecimal(x.BASEGANANCIA),
                                coeficiente,
                                x.GANANCIA.Value)
                        },
                        new RetencionAplicadaDetalleViewModel()
                        {
                            Nombre = RetencionTypes.RetencionIIBB,
                            Porcentaje = x.IIBB.Value,
                            UsaBase = false,
                            Importe = _CalcularImporteDeRetencion(
                                orden.ImportePorPagar,
                                coeficiente,
                                x.IIBB.Value)
                        },
                        new RetencionAplicadaDetalleViewModel()
                        {
                            Nombre = RetencionTypes.RetencionSaludPublica,
                            Porcentaje = x.SP.Value,
                            Base = 0.0m,
                            UsaBase = false,
                            Importe = _CalcularImporteDeRetencion(
                                orden.ImportePorPagar,
                                coeficiente,
                                x.SP.Value)
                        },
                        new RetencionAplicadaDetalleViewModel()
                        {
                            Nombre = RetencionTypes.RetencionEEAOC,
                            Porcentaje = x.EEAOC.Value,
                            Base = 0.0m,
                            UsaBase = false,
                            Importe = _CalcularImporteDeRetencion(
                                orden.ImportePorPagar,
                                coeficiente,
                                x.EEAOC.Value)
                        },
                        new RetencionAplicadaDetalleViewModel()
                        {
                            Nombre = RetencionTypes.RetencionRiego,
                            Porcentaje = x.RIEGO.Value,
                            Base = 0.0m,
                            UsaBase = false,
                            Importe = _CalcularImporteDeRetencion(
                                orden.ImportePorPagar,
                                coeficiente,
                                x.RIEGO.Value)
                        },
                        new RetencionAplicadaDetalleViewModel()
                        {
                            Nombre = RetencionTypes.RetencionGADM,
                            Porcentaje = x.GADM.Value,
                            Base = 0.0m,
                            UsaBase = false,
                            Importe = _CalcularImporteDeRetencion(
                                orden.ImportePorPagar,
                                coeficiente,
                                x.GADM.Value)
                        }
                    };

                orden.RetencionesAplicadas = retencionesAplicadas;

                var neto = orden.ImportePorPagar;

                foreach (var item in orden.RetencionesAplicadas)
                {
                    neto -= item.Importe;
                }

                orden.NetoPorPagar = neto;

                ordenes.Add(orden);
            }

            ordenes = ordenes.OrderByDescending(x => x.FechaDeLiquidacionDeAFIP)
                .ThenBy(x => x.NumeroDeFET)
                .ToList();

            var result = new LiquidacionesParaOPViewModel()
            {
                CoeficienteDeRetenciones = coeficiente,
                Retenciones = new List<RetencionDetalleViewModel>()
                    {
                        new RetencionDetalleViewModel()
                        {
                            Nombre = RetencionTypes.RetencionGCIAS,
                            Descripcion = String.Empty
                        },
                        new RetencionDetalleViewModel()
                        {
                            Nombre = RetencionTypes.RetencionIIBB,
                            Descripcion = String.Empty
                        },
                        new RetencionDetalleViewModel()
                        {
                            Nombre = RetencionTypes.RetencionSaludPublica,
                            Descripcion = String.Empty
                        },
                        new RetencionDetalleViewModel()
                        {
                            Nombre = RetencionTypes.RetencionEEAOC,
                            Descripcion = String.Empty
                        },
                        new RetencionDetalleViewModel()
                        {
                            Nombre = RetencionTypes.RetencionRiego,
                            Descripcion = String.Empty
                        },
                        new RetencionDetalleViewModel()
                        {
                            Nombre = RetencionTypes.RetencionGADM,
                            Descripcion = String.Empty
                        }
                    },
                Items = ordenes
            };

            return result;
        }

        private List<Vw_Romaneo> _GetEntitiesLiquidaciones(Expression<Func<Vw_Romaneo, bool>> pred, decimal valor, LiquidacionesQueryType type)
        {
            using (var context = new CooperativaProduccionEntities())
            {
                var calculadordepago = context.ParamPagos.Single();

                if (type == LiquidacionesQueryType.PorPorcentaje)
                {
                    calculadordepago.porciento = valor;
                    calculadordepago.porkilo = 0;
                }
                else if (type == LiquidacionesQueryType.PorKilo)
                {
                    calculadordepago.porciento = 0;
                    calculadordepago.porkilo = valor;
                }
                
                context.Entry(calculadordepago).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

            using (var context = new CooperativaProduccionEntities())
            {
                var entities = context.Vw_Romaneo
                    .Where(x =>
                        x.OrdenPagoId == null
                        && x.RomaneoPendiente == false)
                    .Where(pred)
                    .OrderByDescending(x => x.FechaAfipLiquidacion)
                    .ThenBy(x => x.nrofet)
                    .ToList();

                return entities;
            }
        }

        private Vw_OrdenPago _GetEntitieOrdenDePago(Guid id)
        {
            Vw_OrdenPago entitie;

            using (var context = new CooperativaProduccionEntities())
            {
                entitie = context.Vw_OrdenPago
                    .Where(x => x.OrdenPagoId == id)
                    .Single();
            }

            return entitie;
        }

        private List<Vw_OrdenPago> _GetEntitiesOrdenesDePago(Expression<Func<Vw_OrdenPago, bool>> pred)
        {
            List<Vw_OrdenPago> entities;

            using (var context = new CooperativaProduccionEntities())
            {
                entities = context.Vw_OrdenPago
                    .Where(pred)
                    .OrderByDescending(x => x.Fecha)
                    .ThenBy(x => x.nrofet)
                    .ToList();
            }

            return entities;
        }

        private OrdenesDePagoDetalleViewModel _GetViewModelOrdenesDePagos(List<Vw_OrdenPago> entities)
        {
            var retenciones = new String[]
            {
                RetencionTypes.RetencionIIBB,
                RetencionTypes.RetencionEEAOC,
                RetencionTypes.RetencionSaludPublica,
                RetencionTypes.RetencionGADM,
                RetencionTypes.RetencionGCIAS,
                RetencionTypes.RetencionRiego
                
            };

            var retencionesvm = new List<RetencionViewModel>();

            foreach (var item in retenciones)
            {
                var retencionvm = new RetencionViewModel()
                {
                    Nombre = item
                };

                retencionesvm.Add(retencionvm);
            }

            var items = new List<OrdenDePagoDetalleViewModel>();

            foreach (var x in entities)
            {
                var aplicadasvm = new List<RetencionAplicadaViewModel>();

                foreach (var ret in retenciones)
                {
                    RetencionAplicadaViewModel aplicadavm;

                    switch (ret)
                    {
                        case RetencionTypes.RetencionIIBB:
                            aplicadavm = new RetencionAplicadaViewModel()
                            {
                                Nombre = RetencionTypes.RetencionIIBB,
                                Importe = x.IIBB.Value
                            };
                            break;
                        case (RetencionTypes.RetencionEEAOC):
                            aplicadavm = new RetencionAplicadaViewModel()
                            {
                                Nombre = RetencionTypes.RetencionEEAOC,
                                Importe = x.EEAOC.Value
                            };
                            break;
                        case RetencionTypes.RetencionSaludPublica:
                            aplicadavm = new RetencionAplicadaViewModel()
                            {
                                Nombre = RetencionTypes.RetencionSaludPublica,
                                Importe = x.SaludPublica.Value
                            };
                            break;
                        case RetencionTypes.RetencionGADM:
                            aplicadavm = new RetencionAplicadaViewModel()
                            {
                                Nombre = RetencionTypes.RetencionGADM,
                                Importe = x.GADM.Value
                            };
                            break;
                        case RetencionTypes.RetencionGCIAS:
                            aplicadavm = new RetencionAplicadaViewModel()
                            {
                                Nombre = RetencionTypes.RetencionGCIAS,
                                Importe = x.Ganancias.Value
                            };
                            break;
                        case RetencionTypes.RetencionRiego:
                            aplicadavm = new RetencionAplicadaViewModel()
                            {
                                Nombre = RetencionTypes.RetencionRiego,
                                Importe = x.Riego.Value
                            };
                            break;
                        default:
                            throw new InvalidOperationException("No se reconoce la retencion asignada: " + ret);
                    }

                    aplicadasvm.Add(aplicadavm);
                }

                var ordenvm = new OrdenDePagoDetalleViewModel()
                {
                    Id = x.OrdenPagoId,
                    ProductorId = x.ProductorId,
                    Fecha = x.Fecha,
                    Productor = x.NOMBRE,
                    FET = x.nrofet,
                    CUIT = x.CUIT,
                    ImportePorPagar = x.Subtotal.Value,
                    RetencionesAplicadas = aplicadasvm,
                    ImporteNeto = x.Neto
                };

                items.Add(ordenvm);
            }

            var ordenesvm = new OrdenesDePagoDetalleViewModel()
            {
                Retenciones = retencionesvm,
                Items = items
            };

            return ordenesvm;
        }

        private decimal _CalcularImporteDeRetencion(decimal importe, decimal coeficiente, decimal porcentajederetencionentero)
        {
            var porcentajederetenciondecimal = (porcentajederetencionentero * 1.0m) / 100;

            var retencioncalculada = (importe / coeficiente) * porcentajederetenciondecimal;
            var retencioncalculadaredondeada = decimal.Round(retencioncalculada, 2, MidpointRounding.AwayFromZero);

            return retencioncalculadaredondeada;
        }

        enum LiquidacionesQueryType
        {
            PorPorcentaje,
            PorKilo
        }
    }

    public interface IPagosManager
    {
        LiquidacionesParaOPViewModel ListarLiquidacionesParaPagarEnPorcentaje(decimal porcentajeporpagar);

        LiquidacionesParaOPViewModel ListarLiquidacionesParaPagarEnPorcentaje(decimal porcentajeporpagar, DateTime desde, DateTime hasta);

        LiquidacionesParaOPViewModel ListarLiquidacionesParaPagarEnKilos(decimal kilos);

        LiquidacionesParaOPViewModel ListarLiquidacionesParaPagarEnKilos(decimal kilos, DateTime desde, DateTime hasta);

        long GetNumeroDeOrden();

        long GetNumeroInternoDeOrden();

        void GenerarOrdenDePago(OrdenDePagoViewModel ordendepagovm);

        OrdenesDePagoDetalleViewModel ListarOrdenesDePago();

        OrdenesDePagoDetalleViewModel ListarOrdenesDePago(DateTime desde, DateTime hasta);

        OrdenesDePagoDetalleViewModel ListarOrdenesDePagoDeProductor(Guid productorid);

        OrdenesDePagoDetalleViewModel ListarOrdenesDePagoDeProductor(Guid productorid, DateTime desde, DateTime hasta);

        OrdenesDePagoDetalleViewModel GetOrdenDePago(Guid _ordendepagoid);

        List<ConceptoDescripcionDeOrdenDePagoViewModel> ListarConceptosDeOrdenDePago(Guid ordendepagoid);
    }
}