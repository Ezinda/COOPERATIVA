using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ViewModels
{
    public class ControlDeHumedadViewModel
    {
        public Guid _Id { get; set; }

        public DateTime Fecha { get; set; }

        public BlendViewModel Blend { get; set; }

        public long Corrida { get; set; }

        public List<LineaDetalleControlDeHumedadViewModel> Lineas { get; set; }
    }

    public class LineaDetalleControlDeHumedadViewModel
    {
        public TimeSpan Hora { get; set; }

        public long Caja { get; set; }

        public decimal TemperaturaEmpaque { get; set; }

        public long Capsula { get; set; }

        public TimeSpan HoraEntrada { get; set; }

        public TimeSpan HoraSalida { get; set; }

        public decimal Humedad { get; set; }
    }
}
