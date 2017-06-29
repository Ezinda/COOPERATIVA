using CooperativaProduccion.Helpers;
using CooperativaProduccion.ViewModels;
using DesktopEntities.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.DataAccess
{
    public class BlendManager : IBlendManager
    {
        private CooperativaProduccionEntities _context;

        private List<BlendViewModel> _blendsListados;

        //private List<ProduccionCorrida> _corridasCache;

        public BlendManager()
        {
            _context = new CooperativaProduccionEntities();
        }

        private List<ProduccionBlend> _CargarTablaDeBlends()
        {
            var productos = _context.Vw_Producto
                .Select(x => new
                {
                    Id = x.ID,
                    Nombre = x.DESCRIPCION
                })
                .OrderBy(x => x.Nombre)
                .ToList();

            int ordenDeProduccion = 1;
            var blends = new List<ProduccionBlend>();

            foreach (var producto in productos)
            {
                blends.Add(new ProduccionBlend()
                {
                    Id = Guid.NewGuid(),
                    ProductoId = producto.Id,
                    Descripcion = producto.Nombre,
                    OrdenProduccion = ordenDeProduccion
                });
                
                ordenDeProduccion++;
            }

            _context.ProduccionBlend.AddRange(blends);
            _context.SaveChanges();

            return blends;
        }

        private bool _ExisteMuestra(Guid productoId, long numeroDeCaja, int campania)
        {
            return _context.ProduccionMuestra
                .Where(x =>
                    x.ProduccionBlend.ProductoId == productoId && x.ProduccionBlend.Periodo == campania
                    && x.Caja == numeroDeCaja && x.Fecha.Year == campania)
                .Any();
        }

        private CajaData _GetCaja(Guid productoId, long numeroDeCaja, int campania)
        {
            return _context.Caja
                .Where(x => x.ProductoId == productoId && x.NumeroCaja == numeroDeCaja && x.Campaña == campania)
                .Select(x => new CajaData() { Id = x.Id, NumeroCaja = x.NumeroCaja })
                .Single();
        }

        private List<CajaData> _GetCajas(Guid productoId, IEnumerable<long> numcajas, int campania)
        {
            return _context.Caja
                .Where(x => x.ProductoId == productoId && numcajas.Contains(x.NumeroCaja) && x.Campaña == campania)
                .Select(x => new CajaData() { Id = x.Id, NumeroCaja = x.NumeroCaja })
                .ToList();
        }

        private ProduccionCorrida _GetCorrida(Guid realBlendId, DateTime fecha, bool crearRegistro)
        {
            var primerDiaDeAnio = new DateTime(fecha.Year, 1, 1);
            //var realBlendId = _GetDbBlendId(productoId, fecha.Year);
            var corrida = _context.ProduccionCorrida
                .Where(x => x.ProductoId == realBlendId && x.Fecha == fecha)
                .FirstOrDefault();

            if (corrida == null)
            {
                long ultimoNumeroDeCorrida;

                try
                {
                    ultimoNumeroDeCorrida = _context.ProduccionCorrida
                        .Where(x => x.ProductoId == realBlendId && x.Fecha >= primerDiaDeAnio && x.Fecha < fecha)
                        .Max(x => x.NumeroCorrida);
                }
                catch
                {
                    if (crearRegistro)
                    {
                        var numeroPrimerCorrida = 1;
                        corrida = new ProduccionCorrida() { Id = Guid.NewGuid(), ProductoId = realBlendId, Fecha = fecha, NumeroCorrida = numeroPrimerCorrida };

                        _context.ProduccionCorrida.Add(corrida);
                        _context.SaveChanges();
                    }
                    else
                    {
                        var numeroPrimerCorrida = 1;
                        corrida = new ProduccionCorrida() { Id = Guid.NewGuid(), ProductoId = realBlendId, Fecha = fecha, NumeroCorrida = numeroPrimerCorrida };
                    }

                    return corrida;
                }

                if (crearRegistro)
                {
                    var numeroSiguienteCorrida = ultimoNumeroDeCorrida + 1;
                    corrida = new ProduccionCorrida() { Id = Guid.NewGuid(), ProductoId = realBlendId, Fecha = fecha, NumeroCorrida = numeroSiguienteCorrida };

                    _context.ProduccionCorrida.Add(corrida);
                    _context.SaveChanges();
                }
                else
                {
                    var numeroSiguienteCorrida = ultimoNumeroDeCorrida + 1;
                    corrida = new ProduccionCorrida() { Id = Guid.NewGuid(), ProductoId = realBlendId, Fecha = fecha, NumeroCorrida = numeroSiguienteCorrida };
                }

                return corrida;
            }
            else
            {
                return corrida;
            }
        }

        public List<BlendViewModel> ListarBlends()
        {
            if (_blendsListados == null)
            {
                _blendsListados =
                    //_context.ProduccionBlend
                    //.OrderBy(x => x.Descripcion)
                    //.Select(x => new BlendViewModel()
                    //{
                    //    Id = x.Id,
                    //    Descripcion = x.Descripcion,
                    //    OrdenProduccion = x.OrdenProduccion
                    //})
                    //.ToList();
                    _context.Vw_Producto
                    .OrderBy(x => x.DESCRIPCION)
                    .Select(x => new BlendViewModel()
                    {
                        Id = x.ID,
                        Descripcion = x.DESCRIPCION
                    })
                    .ToList();

                //if (_blendsListados.Count == 0)
                //{
                //    _blendsListados = _CargarTablaDeBlends()
                //        .OrderBy(x => x.Descripcion)
                //        .Select(x => new BlendViewModel()
                //        {
                //            Id = x.Id,
                //            Descripcion = x.Descripcion,
                //            OrdenProduccion = x.OrdenProduccion
                //        })
                //        .ToList();
                //}
            }

            return _blendsListados;
        }

        public List<BlendDePeriodoViewModel> ListarOrdenesDeProduccion(int periodo)
        {
            var blends = ListarBlends();
            var ordenes = _context.ProduccionBlend
                .Where(x => x.Periodo == periodo)
                .ToList();

            var result = new List<BlendDePeriodoViewModel>();

            foreach (var blend in blends)
            {
                var orden = ordenes.Where(x => x.ProductoId == blend.Id).SingleOrDefault();

                result.Add(new BlendDePeriodoViewModel()
                {
                    Id = blend.Id,
                    Descripcion = blend.Descripcion,
                    Periodo = periodo,
                    OrdenDeProduccion = orden != null ? orden.OrdenProduccion : 0
                });
            }

            return result;
        }

        public void ActualizarOrdenesDeProduccion(int periodo, List<BlendDePeriodoViewModel> blends)
        {
            var ordenes = _context.ProduccionBlend
                .Where(x => x.Periodo == periodo)
                .ToList();

            // Acualizacion de registros existentes
            var flagSeActualizo = false;

            foreach (var ordenExistente in ordenes)
            {
                var blend = blends.Where(x => x.Id == ordenExistente.ProductoId).SingleOrDefault();

                if (blend != null)
                {
                    flagSeActualizo = true;

                    if (blend.OrdenDeProduccion != 0)
                    {
                        ordenExistente.OrdenProduccion = blend.OrdenDeProduccion;
                    }
                    
                    ordenExistente.Descripcion = blend.Descripcion;
                    _context.Entry(ordenExistente).State = EntityState.Modified;
                }
            }

            if (flagSeActualizo)
            {
                _context.SaveChanges();
            }

            // Creacion de numero de ordenes no existentes
            var flagSeCreo = false;

            foreach (var blend in blends)
            {
                if (!ordenes.Where(x => x.ProductoId == blend.Id).Any() && blend.OrdenDeProduccion != 0)
                {
                    flagSeCreo = true;
                    _context.ProduccionBlend.Add(new ProduccionBlend()
                    {
                        Id = Guid.NewGuid(),
                        ProductoId = blend.Id,
                        Descripcion = String.Empty,
                        Periodo = periodo,
                        OrdenProduccion = blend.OrdenDeProduccion
                    });
                }
            }

            if (flagSeCreo)
            {
                _context.SaveChanges();
            }
        }

        public BlendViewModel GetBlend(Guid blendId)
        {
            if (_blendsListados == null)
            {
                return _context.ProduccionBlend
                    .Select(x => new BlendViewModel()
                    {
                        Id = x.ProductoId ?? Guid.Empty,
                        Descripcion = x.Descripcion,
                        //OrdenProduccion = x.OrdenProduccion
                    })
                    .Single();
            }
            else
            {
                return _blendsListados.Where(x => x.Id == blendId).Single();
            }
        }

        public int GetOrdenProduccion(int periodo, Guid blendId)
        {
            //if (_blendsListados == null)
            //{
            //    _blendsListados = ListarBlends();
            //}
            //
            //return _blendsListados.Where(x => x.Id == blendId).Select(x => x.OrdenProduccion).Single();

            var orden = _context.ProduccionBlend
                .Where(x => x.Periodo == periodo && x.ProductoId == blendId)
                .SingleOrDefault();

            if (orden == null)
            {
                int maximo;

                try
                {
                    maximo = _context.ProduccionBlend
                        .Where(x => x.Periodo == periodo)
                        .Max(x => x.OrdenProduccion);
                }
                catch
                {
                    maximo = 0;
                }

                int proximo;

                if (maximo == 0)
                {
                    proximo = 1;
                }
                else
                {
                    proximo = maximo + 1;
                }

                var producto = _context.Vw_Producto
                    .Where(x => x.ID == blendId)
                    .Select(x => x.DESCRIPCION)
                    .Single();

                orden = new ProduccionBlend()
                {
                    Id = Guid.NewGuid(),
                    ProductoId = blendId,
                    Descripcion = producto,
                    Periodo = periodo,
                    OrdenProduccion = proximo
                };

                _context.ProduccionBlend.Add(orden);
                _context.SaveChanges();
            }

            return orden.OrdenProduccion;
        }

        public long GetSiguienteCorrida(Guid blendId, DateTime fecha)
        {
            return _GetCorrida(_GetDbBlendId(blendId, fecha.Year), fecha, false).NumeroCorrida;
        }

        public void AddMuestra(MuestraViewModel muestra)
        {
            var detalles = new List<ProduccionMuestraDetalle>();

            foreach (var linea in muestra.Lineas)
	        {
	        	 detalles.Add(new ProduccionMuestraDetalle() { Id = Guid.NewGuid(), Tamanio = linea.Tamanio, Kilos = linea.kilos, Porcentaje = linea.Porcentaje });
	        }

            CajaData caja;

            try
            {
                caja = _GetCaja(muestra.Blend.Id, muestra.Caja, muestra.Fecha.Year);
            }
            catch
            {
                throw new Exception("No existe Caja");
            }

            try
            {
                if (_ExisteMuestra(muestra.Blend.Id, muestra.Caja, muestra.Fecha.Year))
                    throw new Exception("Muestra repetida");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var dBblendId = _GetDbBlendId(muestra.Blend, muestra.Fecha.Year);

            var row = new ProduccionMuestra()
            {
                Id = Guid.NewGuid(),
                ProductoId = dBblendId,
                Fecha = muestra.Fecha,
                Corrida = muestra.Corrida,
                CorridaId = _GetCorrida(dBblendId, muestra.Fecha, true).Id,
                Hora = muestra.Hora,
                Caja = muestra.Caja,
                CajaId = caja.Id,
                ProduccionMuestraDetalle = detalles,
                PesoMuestra = Convert.ToInt32(muestra.PesoMuestra),
                TotalSobreUnMedio = muestra.TotalSobreUnMedio,
                Observaciones = muestra.Observaciones
            };

            _context.ProduccionMuestra.Add(row);
            _context.SaveChanges();
        }

        public MuestraViewModel GetMuestra(Guid muestraId)
        {
            var muestra = _context.ProduccionMuestra
                .Include(x => x.ProduccionBlend)
                .Include(x => x.ProduccionMuestraDetalle)
                .Where(x => x.Id == muestraId)
                .Select(x => new MuestraViewModel()
                {
                    _Id = x.Id,
                    Blend = new BlendViewModel() { Id = x.ProduccionBlend.ProductoId ?? Guid.Empty, Descripcion = x.ProduccionBlend.Descripcion },
                    Corrida = x.Corrida,
                    Fecha = x.Fecha,
                    Hora = x.Hora,
                    Caja = x.Caja,
                    Lineas = x.ProduccionMuestraDetalle.Select(y => new LineaDetalleMuestraViewModel()
                    {
                        Tamanio = y.Tamanio,
                        kilos = y.Kilos,
                        Porcentaje = y.Porcentaje
                    }).ToList(),
                    Observaciones = x.Observaciones,
                    PesoMuestra = x.PesoMuestra,
                    TotalSobreUnMedio = x.TotalSobreUnMedio
                })
                .Single();

            return muestra;
        }

        public void ModifyMuestra(Guid muestraId, MuestraViewModel muestravm)
        {
            var dBMuestra = _context.ProduccionMuestra
                .Include(x => x.ProduccionMuestraDetalle)
                .Single(x => x.Id == muestraId);

            var detalles = new List<ProduccionMuestraDetalle>();

            foreach (var linea in muestravm.Lineas)
            {
                var dBDetalle = dBMuestra.ProduccionMuestraDetalle
                    .Where(x => x.Tamanio == linea.Tamanio)
                    .SingleOrDefault();

                if (dBDetalle != null)
                {
                    dBDetalle.Kilos = linea.kilos;
                    dBDetalle.Porcentaje = linea.Porcentaje;

                    _context.Entry(dBDetalle).State = EntityState.Modified;
                }
                else
                {
                    detalles.Add(new ProduccionMuestraDetalle()
                    {
                        Id = Guid.NewGuid(),
                        Tamanio = linea.Tamanio,
                        Kilos = linea.kilos,
                        Porcentaje = linea.Porcentaje
                    });
                }
            }

            CajaData caja;

            try
            {
                caja = _GetCaja(muestravm.Blend.Id, muestravm.Caja, muestravm.Fecha.Year);
            }
            catch
            {
                throw new Exception("No existe Caja");
            }

            try
            {
                if (_ExisteMuestra(muestravm.Blend.Id, muestravm.Caja, muestravm.Fecha.Year))
                    throw new Exception("Muestra repetida");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var dBblendId = _GetDbBlendId(muestravm.Blend, muestravm.Fecha.Year);
            
            dBMuestra.ProductoId = dBblendId;
            dBMuestra.Fecha = muestravm.Fecha;
            dBMuestra.Corrida = muestravm.Corrida;
            dBMuestra.CorridaId = _GetCorrida(dBblendId, muestravm.Fecha, true).Id;
            dBMuestra.Hora = muestravm.Hora;
            dBMuestra.Caja = muestravm.Caja;
            dBMuestra.CajaId = caja.Id;

            if (detalles.Count != 0)
            {
                if (dBMuestra.ProduccionMuestraDetalle == null)
                {
                    dBMuestra.ProduccionMuestraDetalle = detalles;
                }
                else
                {
                    foreach (var item in detalles)
	                {
                        dBMuestra.ProduccionMuestraDetalle.Add(item);
	                }
                }
            }
            
            dBMuestra.PesoMuestra = Convert.ToInt32(muestravm.PesoMuestra);
            dBMuestra.TotalSobreUnMedio = muestravm.TotalSobreUnMedio;
            dBMuestra.Observaciones = muestravm.Observaciones;

            _context.Entry(dBMuestra).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteMuestra(Guid muestraId)
        {
            var detalle = _context.ProduccionMuestraDetalle.Where(x => x.MuestraId == muestraId)
                .ToList();

            if (detalle.Count != 0)
            {
                _context.ProduccionMuestraDetalle.RemoveRange(detalle);
                _context.SaveChanges();
            }

            var muestra = _context.ProduccionMuestra.Find(muestraId);
            _context.ProduccionMuestra.Remove(muestra);
            _context.SaveChanges();
        }

        public List<MuestraViewModel> ListarMuestras(Guid blendId, DateTime desde, DateTime hasta)
        {
            return _context.ProduccionMuestra
                    .Include(x => x.ProduccionBlend)
                    .Where(x => x.ProduccionBlend.ProductoId == blendId && x.Fecha >= desde && x.Fecha <= hasta)
                    .Select(x => new MuestraViewModel()
                    {
                        _Id = x.Id,
                        Blend = new BlendViewModel() { Id = x.ProduccionBlend.ProductoId ?? Guid.Empty, Descripcion = x.ProduccionBlend.Descripcion },
                        Corrida = x.Corrida,
                        Fecha = x.Fecha,
                        Hora = x.Hora,
                        Caja = x.Caja,
                        //Lineas = null,
                        Observaciones = x.Observaciones,
                        PesoMuestra = x.PesoMuestra,
                        TotalSobreUnMedio = x.TotalSobreUnMedio
                    })
                    .ToList();
        }

        public List<MuestraViewModel> ListarMuestrasConDetalle(DateTime desde, DateTime hasta)
        {
            var muestras = _context.ProduccionMuestra
                .Include(x => x.ProduccionBlend)
                .Include(x => x.ProduccionMuestraDetalle)
                .Where(x => x.Fecha >= desde && x.Fecha <= hasta)
                .ToList();
            var result = new List<MuestraViewModel>();

            muestras.ForEach(x => result.Add(new MuestraViewModel()
            {
                _Id = x.Id,
                Blend = new BlendViewModel() { Id = x.ProduccionBlend.ProductoId ?? Guid.Empty, Descripcion = x.ProduccionBlend.Descripcion },
                Corrida = x.Corrida,
                Fecha = x.Fecha,
                Hora = x.Hora,
                Caja = x.Caja,
                Lineas = x.ProduccionMuestraDetalle.Select(y => new LineaDetalleMuestraViewModel()
                {
                    Tamanio = y.Tamanio,
                    kilos = y.Kilos,
                    Porcentaje = y.Porcentaje
                }).ToList(),
                Observaciones = x.Observaciones,
                PesoMuestra = x.PesoMuestra,
                TotalSobreUnMedio = x.TotalSobreUnMedio
            }));

            result = result.OrderBy(x => x.Blend.Descripcion)
                .ThenBy(x => x.Fecha)
                .ThenBy(x => x.Hora).ToList();

            return result;
        }

        public List<MuestraViewModel> ListarMuestrasConDetalle(Guid blendId, DateTime desde, DateTime hasta)
        {
            var muestras = _context.ProduccionMuestra
                .Include(x => x.ProduccionBlend)
                .Include(x => x.ProduccionMuestraDetalle)
                .Where(x => x.ProduccionBlend.ProductoId == blendId && x.Fecha >= desde && x.Fecha <= hasta)
                .ToList();
            var result = new List<MuestraViewModel>();

            muestras.ForEach(x => result.Add(new MuestraViewModel()
            {
                _Id = x.Id,
                Blend = new BlendViewModel() { Id = x.ProduccionBlend.ProductoId ?? Guid.Empty, Descripcion = x.ProduccionBlend.Descripcion },
                Corrida = x.Corrida,
                Fecha = x.Fecha,
                Hora = x.Hora,
                Caja = x.Caja,
                Lineas = x.ProduccionMuestraDetalle.Select(y => new LineaDetalleMuestraViewModel()
                {
                    Tamanio = y.Tamanio,
                    kilos = y.Kilos,
                    Porcentaje = y.Porcentaje
                }).ToList(),
                Observaciones = x.Observaciones,
                PesoMuestra = x.PesoMuestra,
                TotalSobreUnMedio = x.TotalSobreUnMedio
            }));

            return result;
        }

        public List<MuestraViewModel> ListarMuestrasConDetalle(Guid[] blendId, DateTime desde, DateTime hasta)
        {
            var muestras = _context.ProduccionMuestra
                .Include(x => x.ProduccionBlend)
                .Include(x => x.ProduccionMuestraDetalle)
                .Where(x => blendId.Contains(x.ProduccionBlend.ProductoId.Value) && x.Fecha >= desde && x.Fecha <= hasta)
                .ToList();
            var result = new List<MuestraViewModel>();

            muestras.ForEach(x => result.Add(new MuestraViewModel()
            {
                _Id = x.Id,
                Blend = new BlendViewModel() { Id = x.ProduccionBlend.ProductoId ?? Guid.Empty, Descripcion = x.ProduccionBlend.Descripcion },
                Corrida = x.Corrida,
                Fecha = x.Fecha,
                Hora = x.Hora,
                Caja = x.Caja,
                Lineas = x.ProduccionMuestraDetalle.Select(y => new LineaDetalleMuestraViewModel()
                {
                    Tamanio = y.Tamanio,
                    kilos = y.Kilos,
                    Porcentaje = y.Porcentaje
                }).ToList(),
                Observaciones = x.Observaciones,
                PesoMuestra = x.PesoMuestra,
                TotalSobreUnMedio = x.TotalSobreUnMedio
            }));

            result = result.OrderBy(x => x.Blend.Descripcion)
                .ThenBy(x => x.Fecha)
                .ThenBy(x => x.Hora).ToList();

            return result;
        }

        public void AddControlTemperatura(ControlDeTemperaturaViewModel control)
        {
            var numcajas = control.Lineas.Select(x => x.Caja).Distinct().ToList();

            List<CajaData> cajas;

            try
            {
                cajas = _GetCajas(control.Blend.Id, numcajas, control.Fecha.Year);

                if (cajas.Count != numcajas.Count)
                {
                    throw new Exception("No existe Caja");
                }
            }
            catch
            {
                throw new Exception("No existe Caja");
            }

            var detalles = new List<ProduccionTemperaturaDetalle>();

            foreach (var linea in control.Lineas)
            {
                detalles.Add(new ProduccionTemperaturaDetalle()
                {
                    Id = Guid.NewGuid(),
                    Hora = linea.Hora,
                    Caja = linea.Caja,
                    CajaId = cajas.Where(x => x.NumeroCaja == linea.Caja).Single().Id,
                    TemperaturaEmpaque = linea.TemperaturaEmpaque,
                    TemperaturaAmbiente = linea.TemperaturaAmbiente,
                    Observaciones = linea.Observaciones
                });
            }

            var dBblendId = _GetDbBlendId(control.Blend, control.Fecha.Year);

            var row = new ProduccionTemperatura()
            {
                Id = Guid.NewGuid(),
                Fecha = control.Fecha,
                ProductoId = dBblendId,
                Corrida = control.Corrida,
                CorridaId = _GetCorrida(dBblendId, control.Fecha, true).Id,
                Minimo = control.Minimo,
                Meta = control.Meta,
                Maximo = control.Maximo,
                ProduccionTemperaturaDetalle = detalles
            };

            _context.ProduccionTemperatura.Add(row);
            _context.SaveChanges();
        }

        public ControlDeTemperaturaViewModel GetControlTemperatura(Guid controlId)
        {
            var control = _context.ProduccionTemperatura
                .Include(x => x.ProduccionTemperaturaDetalle)
                .Single(x => x.Id == controlId);

            var detalle = new List<LineaDetalleControlDeTempraturaViewModel>();

            foreach (var item in control.ProduccionTemperaturaDetalle)
            {
                detalle.Add(new LineaDetalleControlDeTempraturaViewModel()
                {
                    Hora = item.Hora,
                    Caja = item.Caja,
                    TemperaturaEmpaque = item.TemperaturaEmpaque,
                    TemperaturaAmbiente = item.TemperaturaAmbiente,
                    Observaciones = item.Observaciones
                });
            }

            return new ControlDeTemperaturaViewModel()
            {
                Fecha = control.Fecha,
                Blend = _context.ProduccionBlend
                    .Where(x => x.Id == control.ProductoId)
                    .Select(x => new BlendViewModel()
                    {
                        Id = x.ProductoId ?? Guid.Empty,
                        Descripcion = x.Descripcion,
                        //OrdenProduccion = x.OrdenProduccion
                    })
                    .Single(),
                Corrida = control.Corrida,
                Minimo = control.Minimo,
                Meta = control.Meta,
                Maximo = control.Maximo,
                Lineas = detalle
            };
        }

        public void ModifyControlTemperatura(Guid controlId, ControlDeTemperaturaViewModel control)
        {
            var dBControl = _context.ProduccionTemperatura
                .Include(x => x.ProduccionTemperaturaDetalle)
                .Single(x => x.Id == controlId);

            var numcajas = control.Lineas.Select(x => x.Caja).Distinct().ToList();

            List<CajaData> cajas;

            try
            {
                cajas = _GetCajas(control.Blend.Id, numcajas, control.Fecha.Year);

                if (cajas.Count != numcajas.Count)
                {
                    throw new Exception("No existe Caja");
                }
            }
            catch
            {
                throw new Exception("No existe Caja");
            }

            if (dBControl.ProduccionTemperaturaDetalle != null && dBControl.ProduccionTemperaturaDetalle.Count() > 0)
            {
                _context.ProduccionTemperaturaDetalle.RemoveRange(dBControl.ProduccionTemperaturaDetalle);
            }

            var detalles = new List<ProduccionTemperaturaDetalle>();

            foreach (var linea in control.Lineas)
            {
                detalles.Add(new ProduccionTemperaturaDetalle()
                {
                    Id = Guid.NewGuid(),
                    Hora = linea.Hora,
                    Caja = linea.Caja,
                    CajaId = cajas.Where(x => x.NumeroCaja == linea.Caja).Single().Id,
                    TemperaturaEmpaque = linea.TemperaturaEmpaque,
                    TemperaturaAmbiente = linea.TemperaturaAmbiente,
                    Observaciones = linea.Observaciones
                });
            }

            var dBblendId = _GetDbBlendId(control.Blend, control.Fecha.Year);
            
            dBControl.Fecha = control.Fecha;
            dBControl.ProductoId = dBblendId;
            dBControl.Corrida = control.Corrida;
            dBControl.CorridaId = _GetCorrida(dBblendId, control.Fecha, true).Id;
            dBControl.Minimo = control.Minimo;
            dBControl.Meta = control.Meta;
            dBControl.Maximo = control.Maximo;
            dBControl.ProduccionTemperaturaDetalle = detalles;

            _context.Entry(dBControl).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteControlTemperatura(Guid controlId)
        {
            var dBControl = _context.ProduccionTemperatura
                .Include(x => x.ProduccionTemperaturaDetalle)
                .Single(x => x.Id == controlId);

            if (_context.ProduccionTemperaturaDetalle != null && _context.ProduccionTemperaturaDetalle.Count() > 0)
            {
                _context.ProduccionTemperaturaDetalle.RemoveRange(dBControl.ProduccionTemperaturaDetalle);
            }

            _context.ProduccionTemperatura.Remove(dBControl);

            _context.SaveChanges();
        }

        public List<ControlDeTemperaturaViewModel> ListarControlesDeTemperatura(Guid blendId, DateTime desde, DateTime hasta)
        {
            var controles = _context.ProduccionTemperatura
                    .Include(x => x.ProduccionBlend)
                    .Include(x => x.ProduccionTemperaturaDetalle)
                    .Where(x => x.ProduccionBlend.ProductoId == blendId && x.Fecha >= desde && x.Fecha <= hasta)
                    .ToList();

            var result = new List<ControlDeTemperaturaViewModel>();

            foreach (var control in controles)
            {
                var detalles = new List<LineaDetalleControlDeTempraturaViewModel>();

                foreach (var detalle in control.ProduccionTemperaturaDetalle)
                {
                    detalles.Add(new LineaDetalleControlDeTempraturaViewModel()
                    {
                        Hora = detalle.Hora,
                        Caja = detalle.Caja,
                        TemperaturaEmpaque = detalle.TemperaturaEmpaque,
                        TemperaturaAmbiente = detalle.TemperaturaAmbiente,
                        Observaciones = detalle.Observaciones
                    });
                }

                result.Add(new ControlDeTemperaturaViewModel()
                {
                    _Id = control.Id,
                    Blend = new BlendViewModel() { Id = control.ProduccionBlend.ProductoId ?? Guid.Empty, Descripcion = control.ProduccionBlend.Descripcion },
                    Fecha = control.Fecha,
                    Corrida = control.Corrida,
                    Minimo = control.Minimo,
                    Meta = control.Meta,
                    Maximo = control.Maximo,
                    Lineas = detalles
                });
            }

            return result;
        }

        public void AddControlHumedad(ControlDeHumedadViewModel control)
        {
            var numcajas = control.Lineas.Select(x => x.Caja).Distinct().ToList();

            List<CajaData> cajas;

            try
            {
                cajas = _GetCajas(control.Blend.Id, numcajas, control.Fecha.Year);

                if (cajas.Count != numcajas.Count)
                {
                    throw new Exception("No existe Caja");
                }
            }
            catch
            {
                throw new Exception("No existe Caja");
            }

            var detalles = new List<ProduccionHumedadDetalle>();

            foreach (var linea in control.Lineas)
            {
                detalles.Add(new ProduccionHumedadDetalle()
                {
                    Id = Guid.NewGuid(),
                    Hora = linea.Hora,
                    Caja = linea.Caja,
                    CajaId = cajas.Where(x => x.NumeroCaja == linea.Caja).Single().Id,
                    TemperaturaEmpaque = linea.TemperaturaEmpaque,
                    Capsula = Convert.ToInt32(linea.Capsula),
                    HoraEntrada = linea.HoraEntrada,
                    HoraSalida = linea.HoraSalida,
                    Humedad = linea.Humedad
                });
            }

            var dBblendId = _GetDbBlendId(control.Blend, control.Fecha.Year);

            var row = new ProduccionHumedad()
            {
                Id = Guid.NewGuid(),
                Fecha = control.Fecha,
                ProductoId = dBblendId,
                Corrida = control.Corrida,
                CorridaId = _GetCorrida(dBblendId, control.Fecha, true).Id,
                ProduccionHumedadDetalle = detalles
            };

            _context.ProduccionHumedad.Add(row);
            _context.SaveChanges();
        }

        public ControlDeHumedadViewModel GetControlHumedad(Guid controlId)
        {
            var control = _context.ProduccionHumedad
                .Include(x => x.ProduccionHumedadDetalle)
                .Single(x => x.Id == controlId);

            var detalle = new List<LineaDetalleControlDeHumedadViewModel>();

            foreach (var item in control.ProduccionHumedadDetalle)
            {
                detalle.Add(new LineaDetalleControlDeHumedadViewModel()
                {
                    Hora = item.Hora,
                    Caja = item.Caja,
                    TemperaturaEmpaque = item.TemperaturaEmpaque,
                    Capsula = item.Capsula,
                    HoraEntrada = item.HoraEntrada,
                    HoraSalida = item.HoraSalida,
                    Humedad = item.Humedad
                });
            }

            return new ControlDeHumedadViewModel()
            {
                _Id = control.Id,
                Fecha = control.Fecha,
                Blend = _context.ProduccionBlend
                    .Where(x => x.Id == control.ProductoId)
                    .Select(x => new BlendViewModel()
                    {
                        Id = x.ProductoId ?? Guid.Empty,
                        Descripcion = x.Descripcion,
                        //OrdenProduccion = x.OrdenProduccion
                    })
                    .Single(),
                Corrida = control.Corrida,
                Lineas = detalle
            };
        }

        public void ModifyControlHumedad(Guid controlId, ControlDeHumedadViewModel control)
        {
            var dBControl = _context.ProduccionHumedad
                .Include(x => x.ProduccionHumedadDetalle)
                .Single(x => x.Id == controlId);

            var numcajas = control.Lineas.Select(x => x.Caja).Distinct().ToList();

            List<CajaData> cajas;

            try
            {
                cajas = _GetCajas(control.Blend.Id, numcajas, control.Fecha.Year);

                if (cajas.Count != numcajas.Count)
                {
                    throw new Exception("No existe Caja");
                }
            }
            catch
            {
                throw new Exception("No existe Caja");
            }

            if (dBControl.ProduccionHumedadDetalle != null && dBControl.ProduccionHumedadDetalle.Count() > 0)
            {
                _context.ProduccionHumedadDetalle.RemoveRange(dBControl.ProduccionHumedadDetalle);
            }

            var detalles = new List<ProduccionHumedadDetalle>();

            foreach (var linea in control.Lineas)
            {
                detalles.Add(new ProduccionHumedadDetalle()
                {
                    Id = Guid.NewGuid(),
                    Hora = linea.Hora,
                    Caja = linea.Caja,
                    CajaId = cajas.Where(x => x.NumeroCaja == linea.Caja).Single().Id,
                    TemperaturaEmpaque = linea.TemperaturaEmpaque,
                    Capsula = Convert.ToInt32(linea.Capsula),
                    HoraEntrada = linea.HoraEntrada,
                    HoraSalida = linea.HoraSalida,
                    Humedad = linea.Humedad
                });
            }

            var dBblendId = _GetDbBlendId(control.Blend, control.Fecha.Year);

            dBControl.Fecha = control.Fecha;
            dBControl.ProductoId = dBblendId;
            dBControl.Corrida = control.Corrida;
            dBControl.CorridaId = _GetCorrida(dBblendId, control.Fecha, true).Id;
            dBControl.ProduccionHumedadDetalle = detalles;

            _context.Entry(dBControl).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteControlHumedad(Guid controlId)
        {
            var dBControl = _context.ProduccionHumedad
                .Include(x => x.ProduccionHumedadDetalle)
                .Single(x => x.Id == controlId);

            if (_context.ProduccionHumedadDetalle != null && _context.ProduccionHumedadDetalle.Count() > 0)
            {
                _context.ProduccionHumedadDetalle.RemoveRange(dBControl.ProduccionHumedadDetalle);
            }

            _context.ProduccionHumedad.Remove(dBControl);

            _context.SaveChanges();
        }

        private Guid _GetDbBlendId(Guid blendId, int year)
        {
            return _context.ProduccionBlend.Where(x => x.ProductoId == blendId && x.Periodo == year).Select(x => x.Id).Single();
        }

        private Guid _GetDbBlendId(BlendViewModel blend, int year)
        {
            var id = _context.ProduccionBlend.Where(x => x.ProductoId == blend.Id && x.Periodo == year).Select(x => x.Id).FirstOrDefault();

            if (id != Guid.Empty)
            {
                return id;
            }
            else
            {
                GetOrdenProduccion(year, blend.Id);

                return _context.ProduccionBlend.Where(x => x.ProductoId == blend.Id && x.Periodo == year).Select(x => x.Id).Single();
            }
        }

        public List<ControlDeHumedadViewModel> ListarControlesDeHumedad(Guid blendId, DateTime fecha)
        {
            var controles = _context.ProduccionHumedad
                    .Include(x => x.ProduccionBlend)
                    .Include(x => x.ProduccionHumedadDetalle)
                    .Where(x => x.ProduccionBlend.ProductoId == blendId && x.Fecha == fecha)
                    .ToList();

            var result = new List<ControlDeHumedadViewModel>();

            foreach (var control in controles)
            {
                var detalles = new List<LineaDetalleControlDeHumedadViewModel>();

                foreach (var detalle in control.ProduccionHumedadDetalle)
                {
                    detalles.Add(new LineaDetalleControlDeHumedadViewModel()
                    {
                        Hora = detalle.Hora,
                        Caja = detalle.Caja,
                        TemperaturaEmpaque = detalle.TemperaturaEmpaque,
                        Capsula = detalle.Capsula,
                        HoraEntrada = detalle.HoraEntrada,
                        HoraSalida = detalle.HoraSalida,
                        Humedad = detalle.Humedad
                    });
                }

                result.Add(new ControlDeHumedadViewModel()
                {
                    _Id = control.Id,
                    Blend = new BlendViewModel() { Id = control.ProduccionBlend.ProductoId ?? Guid.Empty, Descripcion = control.ProduccionBlend.Descripcion },
                    Fecha = control.Fecha,
                    Corrida = control.Corrida,
                    Lineas = detalles
                });
            }

            return result;
        }

        public List<ControlDeHumedadViewModel> ListarControlesDeHumedad(Guid[] blendId, DateTime desde, DateTime hasta)
        {
            var controles = _context.ProduccionHumedad
                    .Include(x => x.ProduccionBlend)
                    .Include(x => x.ProduccionHumedadDetalle)
                    .Where(x => blendId.Contains(x.ProduccionBlend.ProductoId.Value) && x.Fecha >= desde && x.Fecha <= hasta)
                    .ToList();

            var result = new List<ControlDeHumedadViewModel>();

            foreach (var control in controles)
            {
                var detalles = new List<LineaDetalleControlDeHumedadViewModel>();

                foreach (var detalle in control.ProduccionHumedadDetalle)
                {
                    detalles.Add(new LineaDetalleControlDeHumedadViewModel()
                    {
                        Hora = detalle.Hora,
                        Caja = detalle.Caja,
                        TemperaturaEmpaque = detalle.TemperaturaEmpaque,
                        Capsula = detalle.Capsula,
                        HoraEntrada = detalle.HoraEntrada,
                        HoraSalida = detalle.HoraSalida,
                        Humedad = detalle.Humedad
                    });
                }

                detalles = detalles.OrderBy(x => x.Hora).ToList();

                result.Add(new ControlDeHumedadViewModel()
                {
                    _Id = control.Id,
                    Blend = new BlendViewModel() { Id = control.ProduccionBlend.ProductoId ?? Guid.Empty, Descripcion = control.ProduccionBlend.Descripcion },
                    Fecha = control.Fecha,
                    Corrida = control.Corrida,
                    Lineas = detalles
                });
            }

            result = result.OrderBy(x => x.Blend.Descripcion)
                .ThenBy(x => x.Fecha)
                .ToList();

            return result;
        }

        public void AddControlNicotina(ControlDeNicotinaViewModel control)
        {
            List<long> numcajas = new List<long>();
            numcajas.AddRange(control.Lineas.Select(x => x.CajaDesde).Distinct().ToList());
            numcajas.AddRange(control.Lineas.Select(x => x.CajaHasta).Distinct().ToList());
            var numcajasDistinct = numcajas.Distinct();

            List<CajaData> cajas;

            try
            {
                cajas = _GetCajas(control.Blend.Id, numcajasDistinct, control.Fecha.Year);

                if (cajas.Count != numcajasDistinct.Count())
                {
                    throw new Exception("No existe Caja");
                }
            }
            catch
            {
                throw new Exception("No existe Caja");
            }

            var detalles = new List<ProduccionNicotinaDetalle>();

            var dBblendId = _GetDbBlendId(control.Blend, control.Fecha.Year);

            foreach (var linea in control.Lineas)
            {
                detalles.Add(new ProduccionNicotinaDetalle()
                {
                    Id = Guid.NewGuid(),
                    CajaDesde = linea.CajaDesde,
                    CajaHasta = linea.CajaHasta,
                    PorcentajeHumedad = linea.PorcentajeHumedad,
                    Valor1 = linea.Valor1,
                    Valor2 = linea.Valor2,
                    PorcentajeALC = linea.PorcentajeALC,
                    PorcentajeNicotina = linea.PorcentajeNicotina
                });
            }

            var row = new ProduccionNicotina()
            {
                Id = Guid.NewGuid(),
                Fecha = control.Fecha,
                ProductoId = dBblendId,
                Corrida = control.Corrida,
                CorridaId = _GetCorrida(dBblendId, control.Fecha, true).Id,
                Hora = control.Hora,
                ProduccionNicotinaDetalle = detalles,
            };

            _context.ProduccionNicotina.Add(row);
            _context.SaveChanges();
        }

        public ControlDeNicotinaViewModel GetControlNicotina(Guid controlId)
        {
            var control = _context.ProduccionNicotina
                .Include(x => x.ProduccionBlend)
                .Include(x => x.ProduccionNicotinaDetalle)
                .Where(x => x.Id == controlId)
                .Single();

            return new ControlDeNicotinaViewModel()
            {
                _Id = control.Id,
                Fecha = control.Fecha,
                Blend = new BlendViewModel() { Id = control.ProduccionBlend.ProductoId ?? Guid.Empty, Descripcion = control.ProduccionBlend.Descripcion },
                Corrida = control.Corrida,
                Hora = control.Hora,
                Lineas = control.ProduccionNicotinaDetalle.Select(x => new LineaDetalleControlDeNicotinaViewModel()
                {
                    CajaDesde = x.CajaDesde,
                    CajaHasta = x.CajaHasta,
                    PorcentajeHumedad = x.PorcentajeHumedad,
                    Valor1 = x.Valor1,
                    Valor2 = x.Valor2,
                    PorcentajeALC = x.PorcentajeALC,
                    PorcentajeNicotina = x.PorcentajeNicotina
                }).ToList()
            };
        }

        public void ModifyControlNicotina(Guid controlId, ControlDeNicotinaViewModel control)
        {
            var dBControl = _context.ProduccionNicotina
                .Include(x => x.ProduccionNicotinaDetalle)
                .Single(x => x.Id == controlId);

            List<long> numcajas = new List<long>();
            numcajas.AddRange(control.Lineas.Select(x => x.CajaDesde).Distinct().ToList());
            numcajas.AddRange(control.Lineas.Select(x => x.CajaHasta).Distinct().ToList());
            var numcajasDistinct = numcajas.Distinct();

            List<CajaData> cajas;

            try
            {
                cajas = _GetCajas(control.Blend.Id, numcajasDistinct, control.Fecha.Year);

                if (cajas.Count != numcajasDistinct.Count())
                {
                    throw new Exception("No existe Caja");
                }
            }
            catch
            {
                throw new Exception("No existe Caja");
            }

            if (dBControl.ProduccionNicotinaDetalle != null && dBControl.ProduccionNicotinaDetalle.Count() > 0)
            {
                _context.ProduccionNicotinaDetalle.RemoveRange(dBControl.ProduccionNicotinaDetalle);
            }

            var detalles = new List<ProduccionNicotinaDetalle>();

            foreach (var linea in control.Lineas)
            {
                detalles.Add(new ProduccionNicotinaDetalle()
                {
                    Id = Guid.NewGuid(),
                    CajaDesde = linea.CajaDesde,
                    CajaHasta = linea.CajaHasta,
                    PorcentajeHumedad = linea.PorcentajeHumedad,
                    Valor1 = linea.Valor1,
                    Valor2 = linea.Valor2,
                    PorcentajeALC = linea.PorcentajeALC,
                    PorcentajeNicotina = linea.PorcentajeNicotina
                });
            }

            var dBblendId = _GetDbBlendId(control.Blend, control.Fecha.Year);

            dBControl.Fecha = control.Fecha;
            dBControl.ProductoId = dBblendId;
            dBControl.Corrida = control.Corrida;
            dBControl.CorridaId = _GetCorrida(dBblendId, control.Fecha, true).Id;
            dBControl.Hora = control.Hora;
            dBControl.ProduccionNicotinaDetalle = detalles;

            _context.Entry(dBControl).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteControlNicotina(Guid controlId)
        {
            var dBControl = _context.ProduccionNicotina
                .Include(x => x.ProduccionNicotinaDetalle)
                .Single(x => x.Id == controlId);

            if (_context.ProduccionNicotinaDetalle != null && _context.ProduccionNicotinaDetalle.Count() > 0)
            {
                _context.ProduccionNicotinaDetalle.RemoveRange(dBControl.ProduccionNicotinaDetalle);
            }

            _context.ProduccionNicotina.Remove(dBControl);

            _context.SaveChanges();
        }

        public List<ControlDeNicotinaViewModel> ListarControlesDeNicotina(Guid blendId, DateTime desde, DateTime hasta)
        {
            var controles = _context.ProduccionNicotina
                    .Include(x => x.ProduccionBlend)
                    .Include(x => x.ProduccionNicotinaDetalle)
                    .Where(x => x.ProduccionBlend.ProductoId == blendId && x.Fecha >= desde && x.Fecha <= hasta)
                    .ToList();

            var result = new List<ControlDeNicotinaViewModel>();

            foreach (var control in controles)
            {
                result.Add(new ControlDeNicotinaViewModel()
                {
                    _Id = control.Id,
                    Fecha = control.Fecha,
                    Blend = new BlendViewModel() { Id = control.ProduccionBlend.ProductoId ?? Guid.Empty, Descripcion = control.ProduccionBlend.Descripcion },
                    Corrida = control.Corrida,
                    Hora = control.Hora,
                    Lineas = control.ProduccionNicotinaDetalle.Select(x => new LineaDetalleControlDeNicotinaViewModel()
                    {
                        CajaDesde = x.CajaDesde,
                        CajaHasta = x.CajaHasta,
                        PorcentajeHumedad = x.PorcentajeHumedad,
                        Valor1 = x.Valor1,
                        Valor2 = x.Valor2,
                        PorcentajeALC = x.PorcentajeALC,
                        PorcentajeNicotina = x.PorcentajeNicotina
                    }).ToList()
                });
            }

            return result;
        }

        public List<RendimientoViewModel> ListarDatosDeRendimiento(Guid[] blendsId, DateTime desde, DateTime hasta)
        {
            var periodoDesde = desde.Year;
            var periodoHasta = hasta.Year;

            var blends = _context.ProduccionBlend
                    .Where(x => blendsId.Contains(x.ProductoId.Value) && x.Periodo >= periodoDesde && x.Periodo <= periodoHasta)
                    .Select(x => new BlendDePeriodoViewModel()
                    {
                        Id = x.ProductoId ?? Guid.Empty,
                        Descripcion = x.Descripcion,
                        Periodo = x.Periodo.Value,
                        OrdenDeProduccion = x.OrdenProduccion
                    })
                    .ToList();

            if (blends.Count == 0)
            {
                return new List<RendimientoViewModel>();
            }

            var corridas = _context.ProduccionCorrida
                .Where(x => blendsId.Contains(x.ProduccionBlend.ProductoId.Value) && x.Fecha >= desde && x.Fecha <= hasta)
                .Select(x => new
                {
                    x.ProduccionBlend.ProductoId,
                    x.Fecha,
                    x.NumeroCorrida
                })
                .ToList();

            if (corridas.Count == 0)
            {
                return new List<RendimientoViewModel>();
            }

            var cajas = _context.Caja
                .Where(x => x.Fecha >= desde && x.Fecha <= hasta && blendsId.Contains(x.ProductoId))
                .ToList();

            var fardos = _context.FardoEnProduccion
                .Where(x => x.Fecha >= desde && x.Fecha <= hasta && blendsId.Contains(x.ProductoId.Value))
                .GroupBy(x => new { x.ProductoId, x.Fecha })
                .Select(g => new
                {
                    ProductoId = g.Key.ProductoId,
                    Fecha = g.Key.Fecha,
                    Kilos = g.Sum(x => x.Kilos)
                })
                .ToList();

            var result = new List<RendimientoViewModel>();
            BlendDePeriodoViewModel currentBlendVM = null;

            foreach (var productoId in blendsId)
	        {
                for (DateTime fechaItem = desde; fechaItem <= hasta; fechaItem = fechaItem.AddDays(1))
                {
                    var cajasDeFecha = cajas.Where(x => x.Fecha == fechaItem && x.ProductoId == productoId).ToList();

                    if (cajasDeFecha.Count == 0)
                    {
                        continue;
                    }

                    if (currentBlendVM == null || currentBlendVM.Periodo != fechaItem.Year || currentBlendVM.Id != productoId)
                    {
                        try
                        {
                            currentBlendVM = blends.Where(x => x.Id == productoId && x.Periodo == fechaItem.Year).Single();
                        }
                        catch
                        {
                            fechaItem = new DateTime(fechaItem.Year, 12, 31);

                            continue;
                        }
                    }

                    var corrida = corridas.Where(x => x.Fecha == fechaItem && x.ProductoId == productoId).Select(x => x.NumeroCorrida).FirstOrDefault();

                    result.Add(new RendimientoViewModel()
                    {
                        Blend = currentBlendVM,
                        BlendDescripcion = currentBlendVM.Descripcion,
                        Fecha = fechaItem,
                        OrdenDeProduccion = currentBlendVM.OrdenDeProduccion,
                        Corrida = corrida,
                        Tara = cajasDeFecha[0].Tara,
                        PrimeraCaja = cajasDeFecha.Min(x => x.NumeroCaja),
                        UltimaCaja = cajasDeFecha.Max(x => x.NumeroCaja),
                        NumeroCajas = cajasDeFecha.Count(),
                        Kilos = Convert.ToInt64(fardos.Where(x => x.ProductoId == productoId && x.Fecha == fechaItem).Select(x => x.Kilos).FirstOrDefault()),
                    });
                }
	        }

            return result;
        }

        class CajaData
        {
            public Guid Id { get; set; }

            public long NumeroCaja { get; set; }
        }
    }
}
