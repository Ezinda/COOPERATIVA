using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ViewModels
{
    public class ControlDeNicotinaViewModel
    {
        public Guid _Id { get; set; }

        public DateTime Fecha { get; set; }

        public BlendViewModel Blend { get; set; }

        public long Corrida { get; set; }

        public TimeSpan Hora { get; set; }

        public List<LineaDetalleControlDeNicotinaViewModel> Lineas { get; set; }
    }

    public class LineaDetalleControlDeNicotinaViewModel
    {
        public long CajaDesde { get; set; }

        public long CajaHasta { get; set; }

        public decimal PorcentajeHumedad { get; set; }

        public decimal Valor1 { get; set; }

        public decimal Valor2 { get; set; }

        public decimal PorcentajeALC { get; set; }

        public decimal PorcentajeNicotina { get; set; }
    }
}
