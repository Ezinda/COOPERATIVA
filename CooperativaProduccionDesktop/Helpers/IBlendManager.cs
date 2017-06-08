using CooperativaProduccion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.Helpers
{
    public interface IBlendManager
    {
        List<BlendViewModel> ListarBlends();

        List<BlendDePeriodoViewModel> ListarOrdenesDeProduccion(int periodo);

        void ActualizarOrdenesDeProduccion(int periodo, List<BlendDePeriodoViewModel> blends);

        BlendViewModel GetBlend(Guid blendId);

        int GetOrdenProduccion(int periodo, Guid blendId);

        long GetSiguienteCorrida(Guid blendId, DateTime fecha);

        void AddMuestra(MuestraViewModel muestra);

        MuestraViewModel GetMuestra(Guid _muestraId);

        void ModifyMuestra(Guid muestraId, MuestraViewModel muestra);

        void DeleteMuestra(Guid muestraId);

        List<MuestraViewModel> ListarMuestras(Guid blendId, DateTime desde, DateTime hasta);

        List<MuestraViewModel> ListarMuestrasConDetalle(DateTime desde, DateTime hasta);

        List<MuestraViewModel> ListarMuestrasConDetalle(Guid blendId, DateTime desde, DateTime hasta);

        List<MuestraViewModel> ListarMuestrasConDetalle(Guid[] blendId, DateTime desde, DateTime hasta);

        void AddControlTemperatura(ControlDeTemperaturaViewModel control);

        ControlDeTemperaturaViewModel GetControlTemperatura(Guid controlId);

        void ModifyControlTemperatura(Guid controlId, ControlDeTemperaturaViewModel control);

        void DeleteControlTemperatura(Guid controlId);

        List<ControlDeTemperaturaViewModel> ListarControlesDeTemperatura(Guid blendId, DateTime desde, DateTime hasta);

        void AddControlHumedad(ControlDeHumedadViewModel control);

        ControlDeHumedadViewModel GetControlHumedad(Guid _controlId);

        void ModifyControlHumedad(Guid controlId, ControlDeHumedadViewModel control);

        void DeleteControlHumedad(Guid _controlId);

        List<ControlDeHumedadViewModel> ListarControlesDeHumedad(Guid blendId, DateTime fecha);

        List<ControlDeHumedadViewModel> ListarControlesDeHumedad(Guid[] blendId, DateTime desde, DateTime hasta);

        void AddControlNicotina(ControlDeNicotinaViewModel control);

        ControlDeNicotinaViewModel GetControlNicotina(Guid controlId);

        void ModifyControlNicotina(Guid controlId, ControlDeNicotinaViewModel control);

        void DeleteControlNicotina(Guid controlId);

        List<ControlDeNicotinaViewModel> ListarControlesDeNicotina(Guid blendId, DateTime fecha);
    }
}
