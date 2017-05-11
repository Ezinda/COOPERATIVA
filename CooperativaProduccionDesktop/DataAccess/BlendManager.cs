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
                blends.Add(new ProduccionBlend() { Id = producto.Id, Descripcion = producto.Nombre, OrdenProduccion = ordenDeProduccion });
                
                ordenDeProduccion++;
            }

            _context.ProduccionBlend.AddRange(blends);
            _context.SaveChanges();

            return blends;
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

        private ProduccionCorrida _GetCorrida(Guid blendId, DateTime fecha, bool crearRegistro)
        {
            var primerDiaDeAnio = new DateTime(fecha.Year, 1, 1);
            var corrida = _context.ProduccionCorrida
                .Where(x => x.ProductoId == blendId && x.Fecha == fecha)
                .FirstOrDefault();

            if (corrida == null)
            {
                long ultimoNumeroDeCorrida;

                try
                {
                    ultimoNumeroDeCorrida = _context.ProduccionCorrida
                        .Where(x => x.ProductoId == blendId && x.Fecha >= primerDiaDeAnio && x.Fecha < fecha)
                        .Max(x => x.NumeroCorrida);
                }
                catch
                {
                    if (crearRegistro)
                    {
                        var numeroPrimerCorrida = 1;
                        corrida = new ProduccionCorrida() { Id = Guid.NewGuid(), ProductoId = blendId, Fecha = fecha, NumeroCorrida = numeroPrimerCorrida };

                        _context.ProduccionCorrida.Add(corrida);
                        _context.SaveChanges();
                    }
                    else
                    {
                        var numeroPrimerCorrida = 1;
                        corrida = new ProduccionCorrida() { Id = Guid.NewGuid(), ProductoId = blendId, Fecha = fecha, NumeroCorrida = numeroPrimerCorrida };
                    }

                    return corrida;
                }

                if (crearRegistro)
                {
                    var numeroSiguienteCorrida = ultimoNumeroDeCorrida + 1;
                    corrida = new ProduccionCorrida() { Id = Guid.NewGuid(), ProductoId = blendId, Fecha = fecha, NumeroCorrida = numeroSiguienteCorrida };

                    _context.ProduccionCorrida.Add(corrida);
                    _context.SaveChanges();
                }
                else
                {
                    var numeroSiguienteCorrida = ultimoNumeroDeCorrida + 1;
                    corrida = new ProduccionCorrida() { Id = Guid.NewGuid(), ProductoId = blendId, Fecha = fecha, NumeroCorrida = numeroSiguienteCorrida };
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
                _blendsListados = _context.ProduccionBlend
                    .OrderBy(x => x.Descripcion)
                    .Select(x => new BlendViewModel()
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        OrdenProduccion = x.OrdenProduccion
                    })
                    .ToList();

                if (_blendsListados.Count == 0)
                {
                    _blendsListados = _CargarTablaDeBlends()
                        .OrderBy(x => x.Descripcion)
                        .Select(x => new BlendViewModel()
                        {
                            Id = x.Id,
                            Descripcion = x.Descripcion,
                            OrdenProduccion = x.OrdenProduccion
                        })
                        .ToList();
                }
            }

            return _blendsListados;
        }

        public BlendViewModel GetBlend(Guid blendId)
        {
            if (_blendsListados == null)
            {
                return _context.ProduccionBlend
                    .Select(x => new BlendViewModel()
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        OrdenProduccion = x.OrdenProduccion
                    })
                    .Single();
            }
            else
            {
                return _blendsListados.Where(x => x.Id == blendId).Single();
            }
        }

        public int GetOrdenProduccion(Guid blendId)
        {
            if (_blendsListados == null)
            {
                _blendsListados = ListarBlends();
            }

            return _blendsListados.Where(x => x.Id == blendId).Select(x => x.OrdenProduccion).Single();
        }

        public long GetSiguienteCorrida(Guid blendId, DateTime fecha)
        {
            return _GetCorrida(blendId, fecha, false).NumeroCorrida;
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

            var row = new ProduccionMuestra()
            {
                Id = Guid.NewGuid(),
                ProductoId = muestra.Blend.Id,
                Fecha = muestra.Fecha,
                Corrida = muestra.Corrida,
                CorridaId = _GetCorrida(muestra.Blend.Id, muestra.Fecha, true).Id,
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

        public void ModifyMuestra(MuestraViewModel muestra)
        {
            //var oldMuestra = _dataSourceMuestra.Where(x => x._Id == muestra._Id).Single();
            //_dataSourceMuestra.Remove(oldMuestra);
            //
            //_dataSourceMuestra.Add(muestra);
        }

        public List<MuestraViewModel> ListarMuestras(Guid blendId, DateTime fecha)
        {
            return _context.ProduccionMuestra
                    .Include(x => x.ProduccionBlend)
                    .Where(x => x.ProductoId == blendId && x.Fecha == fecha)
                    .Select(x => new MuestraViewModel()
                    {
                        _Id = x.Id,
                        Blend = new BlendViewModel() { Id = x.ProduccionBlend.Id, Descripcion = x.ProduccionBlend.Descripcion, OrdenProduccion = x.ProduccionBlend.OrdenProduccion },
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

            var row = new ProduccionTemperatura()
            {
                Id = Guid.NewGuid(),
                Fecha = control.Fecha,
                ProductoId = control.Blend.Id,
                Corrida = control.Corrida,
                CorridaId = _GetCorrida(control.Blend.Id, control.Fecha, true).Id,
                Minimo = control.Minimo,
                Meta = control.Meta,
                Maximo = control.Maximo,
                ProduccionTemperaturaDetalle = detalles
            };

            _context.ProduccionTemperatura.Add(row);
            _context.SaveChanges();
        }

        public List<ControlDeTemperaturaViewModel> ListarControlesDeTemperatura(Guid blendId, DateTime fecha)
        {
            var controles = _context.ProduccionTemperatura
                    .Include(x => x.ProduccionBlend)
                    .Include(x => x.ProduccionTemperaturaDetalle)
                    .Where(x => x.ProductoId == blendId && x.Fecha == fecha)
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
                    Blend = new BlendViewModel() { Id = control.ProduccionBlend.Id, Descripcion = control.ProduccionBlend.Descripcion, OrdenProduccion = control.ProduccionBlend.OrdenProduccion },
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

            var row = new ProduccionHumedad()
            {
                Id = Guid.NewGuid(),
                Fecha = control.Fecha,
                ProductoId = control.Blend.Id,
                Corrida = control.Corrida,
                CorridaId = _GetCorrida(control.Blend.Id, control.Fecha, true).Id,
                ProduccionHumedadDetalle = detalles
            };

            _context.ProduccionHumedad.Add(row);
            _context.SaveChanges();
        }

        public List<ControlDeHumedadViewModel> ListarControlesDeHumedad(Guid blendId, DateTime fecha)
        {
            var controles = _context.ProduccionHumedad
                    .Include(x => x.ProduccionBlend)
                    .Include(x => x.ProduccionHumedadDetalle)
                    .Where(x => x.ProductoId == blendId && x.Fecha == fecha)
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
                    Blend = new BlendViewModel() { Id = control.ProduccionBlend.Id, Descripcion = control.ProduccionBlend.Descripcion, OrdenProduccion = control.ProduccionBlend.OrdenProduccion },
                    Fecha = control.Fecha,
                    Corrida = control.Corrida,
                    Lineas = detalles
                });
            }

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

            foreach (var linea in control.Lineas)
            {
                detalles.Add(new ProduccionNicotinaDetalle()
                {
                    Id = Guid.NewGuid(),
                    Fecha = control.Fecha,
                    ProductoId = control.Blend.Id,
                    Corrida = control.Corrida,
                    CorridaId = _GetCorrida(control.Blend.Id, control.Fecha, true).Id,
                    Hora = control.Hora,
                    CajaDesde = linea.CajaDesde,
                    CajaHasta = linea.CajaHasta,
                    PorcentajeHumedad = linea.PorcentajeHumedad,
                    Valor1 = linea.Valor1,
                    Valor2 = linea.Valor2,
                    PorcentajeALC = linea.PorcentajeALC,
                    PorcentajeNicotina = linea.PorcentajeNicotina
                });
            }

            _context.ProduccionNicotinaDetalle.AddRange(detalles);
            _context.SaveChanges();
        }

        public List<ControlDeNicotinaViewModel> ListarControlesDeNicotina(Guid blendId, DateTime fecha)
        {
            var controles = _context.ProduccionNicotinaDetalle
                    .Include(x => x.ProduccionBlend)
                    .Where(x => x.ProductoId == blendId && x.Fecha == fecha)
                    .ToList();

            var headers = controles.Select(x => new ControlDeNicotinaViewModel()
            {
                Fecha = x.Fecha,
                Blend = new BlendViewModel() { Id = x.ProduccionBlend.Id, Descripcion = x.ProduccionBlend.Descripcion, OrdenProduccion = x.ProduccionBlend.OrdenProduccion },
                Corrida = x.Corrida,
                Hora = x.Hora,
                Lineas = new List<LineaDetalleControlDeNicotinaViewModel>()
            })
            .ToList();

            var result = new List<ControlDeNicotinaViewModel>();

            foreach (var header in headers)
            {
                if (!result.Where(x => x.Fecha == header.Fecha && x.Blend == header.Blend && x.Corrida == header.Corrida && x.Hora == header.Hora).Any())
                {
                    result.Add(header);
                }
            }

            foreach (var control in controles)
            {
                var detalle = new LineaDetalleControlDeNicotinaViewModel()
                {
                    CajaDesde = control.CajaDesde,
                    CajaHasta = control.CajaHasta,
                    PorcentajeHumedad = control.PorcentajeHumedad,
                    Valor1 = control.Valor1,
                    Valor2 = control.Valor2,
                    PorcentajeALC = control.PorcentajeALC,
                    PorcentajeNicotina = control.PorcentajeNicotina
                };

                result.Where(x => x.Fecha == control.Fecha && x.Blend.Id == control.ProductoId && x.Corrida == control.Corrida && x.Hora == control.Hora)
                    .Single()
                    .Lineas.Add(detalle);
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
