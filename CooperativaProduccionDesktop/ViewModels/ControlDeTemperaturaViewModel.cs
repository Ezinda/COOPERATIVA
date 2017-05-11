using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ViewModels
{
    public class ControlDeTemperaturaViewModel
    {
        public Guid _Id { get; set; }

        public DateTime Fecha { get; set; }

        public BlendViewModel Blend { get; set; }

        public long Corrida { get; set; }

        public string Minimo { get; set; }

        public string Meta { get; set; }

        public string Maximo { get; set; }

        public List<LineaDetalleControlDeTempraturaViewModel> Lineas { get; set; }
    }

    public class LineaDetalleControlDeTempraturaViewModel
    {
        public TimeSpan Hora { get; set; }

        public long Caja { get; set; }

        public decimal TemperaturaEmpaque { get; set; }

        public decimal TemperaturaAmbiente { get; set; }

        public string Observaciones { get; set; }
    }
}
