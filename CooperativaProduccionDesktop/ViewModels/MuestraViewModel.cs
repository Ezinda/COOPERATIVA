using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ViewModels
{
    public class MuestraViewModel
    {
        public Guid _Id { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public BlendViewModel Blend { get; set; }

        public long Caja { get; set; }

        public long Corrida { get; set; }

        public List<LineaDetalleMuestraViewModel> Lineas { get; set; }

        public decimal TotalSobreUnMedio { get; set; }

        public double PesoMuestra { get; set; }

        public string Observaciones { get; set; }
    }

    public class LineaDetalleMuestraViewModel
    {
        public string Tamanio { get; set; }

        public int kilos { get; set; }

        public decimal Porcentaje { get; set; }
    }
}
