using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CooperativaProduccion.ReportModels
{
    public class ProduccionRegistroDatosDeRendimientoLinea
    {
        public string Empresa { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public string Blend { get; set; }

        public long OrdenProduccion { get; set; }

        public long Corrida { get; set; }

        public long CajaReferente { get; set; }

        // +1

        public decimal Brab { get; set; }

        // +4

        public decimal Temp { get; set; }

        // +4
    }
}
