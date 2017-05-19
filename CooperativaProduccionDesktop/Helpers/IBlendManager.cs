using CooperativaProduccion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers
{
    public class BlendManagerInMemory : IBlendManager
    {
        private List<BlendViewModel> _dataSourceBlend;
        private List<MuestraViewModel> _dataSourceMuestra;

        public BlendManagerInMemory()
        {
            _dataSourceBlend = new List<BlendViewModel>()
            {
                new BlendViewModel() { Id = Guid.NewGuid(), Descripcion = "Blend1"},
                new BlendViewModel() { Id = Guid.NewGuid(), Descripcion = "Blend2"},
                new BlendViewModel() { Id = Guid.NewGuid(), Descripcion = "Blend3"},
                new BlendViewModel() { Id = Guid.NewGuid(), Descripcion = "Blend4"},
                new BlendViewModel() { Id = Guid.NewGuid(), Descripcion = "Blend5"},
                new BlendViewModel() { Id = Guid.NewGuid(), Descripcion = "Blend6"},
            };

            var fecha = DateTime.Now;
            var blend = _dataSourceBlend.First();

            _dataSourceMuestra = new List<MuestraViewModel>()
            {
                new MuestraViewModel() { Fecha = fecha.Date, Hora = fecha.TimeOfDay, Blend = blend, Caja = 1, Corrida = 1, Lineas = null, TotalSobreUnMedio = 0, PesoMuestra = 0},
                new MuestraViewModel() { Fecha = fecha.Date, Hora = fecha.TimeOfDay, Blend = blend, Caja = 2, Corrida = 1, Lineas = null, TotalSobreUnMedio = 0, PesoMuestra = 0},
                new MuestraViewModel() { Fecha = fecha.Date, Hora = fecha.TimeOfDay, Blend = blend, Caja = 3, Corrida = 1, Lineas = null, TotalSobreUnMedio = 0, PesoMuestra = 0},
                new MuestraViewModel() { Fecha = fecha.Date, Hora = fecha.TimeOfDay, Blend = blend, Caja = 4, Corrida = 1, Lineas = null, TotalSobreUnMedio = 0, PesoMuestra = 0},
                new MuestraViewModel() { Fecha = fecha.Date, Hora = fecha.TimeOfDay, Blend = blend, Caja = 5, Corrida = 1, Lineas = null, TotalSobreUnMedio = 0, PesoMuestra = 0},
                new MuestraViewModel() { Fecha = fecha.Date, Hora = fecha.TimeOfDay, Blend = blend, Caja = 6, Corrida = 1, Lineas = null, TotalSobreUnMedio = 0, PesoMuestra = 0},
                new MuestraViewModel() { Fecha = fecha.Date, Hora = fecha.TimeOfDay, Blend = blend, Caja = 7, Corrida = 1, Lineas = null, TotalSobreUnMedio = 0, PesoMuestra = 0}
            };
        }

        public List<BlendViewModel> ListarBlends()
        {
            return _dataSourceBlend;
        }

        public List<BlendDePeriodoViewModel> ListarOrdenesDeProduccion(int periodo)
        {
            return new List<BlendDePeriodoViewModel>();
        }

        public void ActualizarOrdenesDeProduccion(int periodo, List<BlendDePeriodoViewModel> blends)
        {
        }

        public BlendViewModel GetBlend(Guid blendId)
        {
            return _dataSourceBlend.Where(x => x.Id == blendId).Single();
        }

        public int GetOrdenProduccion(int periodo, Guid blendId)
        {
            return 1;
        }

        public List<MuestraViewModel> ListarMuestras(Guid blendId, DateTime desde, DateTime hasta)
        {
            return _dataSourceMuestra.Where(x => x.Blend.Id == blendId && x.Fecha >= desde && x.Fecha <= hasta).ToList();
        }

        public List<MuestraViewModel> ListarMuestrasConDetalle(Guid blendId, DateTime desde, DateTime hasta)
        {
            return _dataSourceMuestra.Where(x => x.Blend.Id == blendId && x.Fecha >= desde && x.Fecha <= hasta).ToList();
        }

        public void AddMuestra(MuestraViewModel muestra)
        {
            _dataSourceMuestra.Add(muestra);
        }

        public void ModifyMuestra(MuestraViewModel muestra)
        {
            var oldMuestra = _dataSourceMuestra.Where(x => x._Id == muestra._Id).Single();
            _dataSourceMuestra.Remove(oldMuestra);
            
            _dataSourceMuestra.Add(muestra);
        }

        public long GetSiguienteCorrida(Guid blendId, DateTime fecha)
        {
            var corrida = _dataSourceMuestra
                .Where(x => x.Blend.Id == blendId && x.Fecha == fecha)
                .Select(x => x.Corrida)
                .FirstOrDefault();

            if (corrida == 0)
            {
                try
                {
                    corrida = _dataSourceMuestra
                        .Where(x => x.Blend.Id == blendId && x.Fecha < fecha)
                        .Max(x => x.Corrida);
                }
                catch
                {
                    return 1;
                }

                return corrida + 1;
            }
            else
            {
                return corrida;
            }
        }

        public void AddControlTemperatura(ControlDeTemperaturaViewModel control)
        {
            ;
        }

        public List<ControlDeTemperaturaViewModel> ListarControlesDeTemperatura(Guid blendId, DateTime desde, DateTime hasta)
        {
            return new List<ControlDeTemperaturaViewModel>();
        }

        public void AddControlHumedad(ControlDeHumedadViewModel control)
        {
            ;
        }

        public List<ControlDeHumedadViewModel> ListarControlesDeHumedad(Guid blendId, DateTime fecha)
        {
            return new List<ControlDeHumedadViewModel>();
        }

        public void AddControlNicotina(ControlDeNicotinaViewModel control)
        {
            ;
        }

        public List<ControlDeNicotinaViewModel> ListarControlesDeNicotina(Guid blendId, DateTime fecha)
        {
            return new List<ControlDeNicotinaViewModel>();
        }
    }

    public interface IBlendManager
    {
        List<BlendViewModel> ListarBlends();

        List<BlendDePeriodoViewModel> ListarOrdenesDeProduccion(int periodo);

        void ActualizarOrdenesDeProduccion(int periodo, List<BlendDePeriodoViewModel> blends);

        BlendViewModel GetBlend(Guid blendId);

        int GetOrdenProduccion(int periodo, Guid blendId);

        long GetSiguienteCorrida(Guid blendId, DateTime fecha);

        void AddMuestra(MuestraViewModel muestra);

        void ModifyMuestra(MuestraViewModel muestra);

        List<MuestraViewModel> ListarMuestras(Guid blendId, DateTime desde, DateTime hasta);

        List<MuestraViewModel> ListarMuestrasConDetalle(Guid blendId, DateTime desde, DateTime hasta);

        void AddControlTemperatura(ControlDeTemperaturaViewModel control);

        List<ControlDeTemperaturaViewModel> ListarControlesDeTemperatura(Guid blendId, DateTime desde, DateTime hasta);

        void AddControlHumedad(ControlDeHumedadViewModel control);

        List<ControlDeHumedadViewModel> ListarControlesDeHumedad(Guid blendId, DateTime fecha);

        void AddControlNicotina(ControlDeNicotinaViewModel control);

        List<ControlDeNicotinaViewModel> ListarControlesDeNicotina(Guid blendId, DateTime fecha);
    }
}
